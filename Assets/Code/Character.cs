using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class  Character : MonoBehaviour
{
    public bool Chatting = true;
    public bool ChattingOnColl = false;
    public bool Zombie = false;
    public bool DropsOrgans;
    private GameObject ChattingObject;
    private Player pl;

    private GameObject ChattingUIObject;
    public int DialogID;
    public int DialogID_AfterItem = -1;
    public int DialogID_QuestDone_NoItem = -1;

    private Material StartMaterial, WhiteMaterial;


    private float DamageTime;
    public bool Enemy;
    public Transform _transform;
    public int QuestID = -1;
    public int QuestIDOnDeath = -1;
    public int Quest_End_Item_ID = -1;
    public int Quest_End_Item_Count = 1;
    public bool FinishQuestOnDeath;

    //public bool DestroyQuestItem = false;

    public int DropItem = -1;
    public int DropItemCount = 1;

    private int[] RNDORGANS;


    public bool NotAlive;
    public bool Save;

    public bool DestoryOnWall;

    private AudioClip HitClip;
    public enum SoundType { Soft, Hard, Metal, Wood,Flesh };


    public SoundType _SoundType;


    public bool DestroyOnDialogEnd;

  
    private void Start()
    {
        pl = InitializeObjects.PL;
      
        ChattingOnColl = false;

        if (!NotAlive && Chatting && !Enemy) Save = true;

        if (_SoundType == SoundType.Soft)
        {
            HitClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Flesh/Flesh/Sword_On_Flesh_Flesh_" + Random.Range(1, 5) + "_Short");


        }


        if (_SoundType == SoundType.Hard)
        {
            HitClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Impact/Sword_On_Metal_Impact_3");
        }
        if (_SoundType == SoundType.Metal)
        {
            HitClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Metal/Sword_On_Metal_Metal_1");
        }

        if (_SoundType == SoundType.Wood)
        {
            HitClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Wood/Wood/Sword_On_Wood_Wood_1");
        }
        if (_SoundType == SoundType.Flesh)
        {
            HitClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Flesh/Flesh/Sword_On_Flesh_Flesh_1");

        }


        // 5% chanse
        RNDORGANS = new int[42] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 5, 7, 103, 102 };
        // RNDORGANS = new int[2] { 5, 7 };

        _transform = transform;
       
        if (!Zombie && !NotAlive)
            pl.Characters.Add(gameObject);



        ChattingUIObject = GameObject.Find("Chatting");


        StartMaterial = GetComponent<SpriteRenderer>().material;
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");

        name += SceneManager.GetActiveScene().name;

        ChattingObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/ChattingSine"));
        ChattingObject.name = "ChattingSine";
        ChattingObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 1);
        ChattingObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        pl.menu.ONOFFUI(ChattingObject.transform, false);
        ChattingObject.transform.Find("QuestItem").gameObject.SetActive(false);


    }
    private void Update()
    {
        DamageControll();

        if (pl._gameover)
        {
            pl.Chatting = false;
            pl.ChattingObject = null;
            pl.menu.ONOFFUI(ChattingUIObject.transform, false);

            Chatting = false;
        }

        if (Chatting)DrawChatting();
       
    }

    void DamageControll()
    {

        if (DestoryOnWall)
        {
            if (GetComponent<CollList>() != null)
            {
                for (int i = 0; i < GetComponent<CollList>().GetCollList().Count; i++)
                {
                   
                    if (GetComponent<CollList>().GetCollList()[i].layer == 9)
                        Destroy(gameObject);
                }
            }

        }





     
     
        if (DamageTime > Time.fixedTime)
        {
            GetComponent<SpriteRenderer>().material = WhiteMaterial;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    transform.GetChild(i).GetComponent<SpriteRenderer>().material = WhiteMaterial;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().material = StartMaterial;

            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<SpriteRenderer>()!=null)
                transform.GetChild(i).GetComponent<SpriteRenderer>().material = StartMaterial;
            }
        }


       
    }


    void DrawChatting()
    {
        if (QuestID > -1)
        {
           

            if (pl.journal.CheckQuestStart(QuestID) && Quest_End_Item_ID > -1)
            {
                ChattingObject.transform.Find("QuestItem").gameObject.SetActive(true);

                if ( ChattingObject.transform.Find("QuestItem").gameObject.activeInHierarchy && pl.inv.GetItemInDatabase(Quest_End_Item_ID) != null)
                {
                    ChattingObject.transform.Find("QuestItem").gameObject.GetComponent<SpriteRenderer>().sprite =
                      Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetItemInDatabase(Quest_End_Item_ID).itemNames[0]);

                    
                }
            }
            else ChattingObject.transform.Find("QuestItem").gameObject.SetActive(false);


            if (pl.journal.CheckQuestDone(QuestID))
            ChattingObject.transform.Find("QuestItem").gameObject.SetActive(false);
            
        }
        else ChattingObject.transform.Find("QuestItem").gameObject.SetActive(false);




        UIAlphaAndColor();

        if (QuestID > -1)
        {
            if (pl.journal.CheckQuestDone(QuestID) && DialogID_QuestDone_NoItem>-1)
            {
                if (DialogID_AfterItem > -1) DialogID = DialogID_QuestDone_NoItem;
            }
        }

        if (pl.menu.MenuONOFF || pl.inv.showjournal || pl.inv.blueprintshow || pl.inv.showinvent)
            return;

        if ( (( pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.GetMouseOBCollList().Contains(gameObject)) && Chatting && !pl.Chatting && pl.ChattingObject == null)
        {
            if (QuestID > -1 && Quest_End_Item_ID > -1)
            {

                if (pl.inv.CheckItem(Quest_End_Item_ID))
                {

                    pl.journal.AddQuest(QuestID);

                    if (!pl.journal.CheckQuestDone(QuestID))
                        pl.inv.DropItemDifferentSpotsNearby(transform.position, DropItemCount, new int[1] { DropItem },pl.inv.GetItemInDatabase(DropItem).Durability);


                    pl.journal.DoneQuest(QuestID);

                    if (DialogID_AfterItem > -1) DialogID = DialogID_AfterItem;
                     QuestID = -1;
                    pl.inv.ReduceItemCount(Quest_End_Item_ID, Quest_End_Item_Count);
                    Quest_End_Item_ID = -1;
                }
            }

            pl.Chatting = true;
            pl.ChattingObject = gameObject;
            pl.menu.ONOFFUI(ChattingUIObject.transform, true);
            

            ChattingUIObject.GetComponent<Dialog>().StartDialog(DialogID);

        }

        if (pl.Chatting && !pl.inv.blueprintshow && !pl.inv.showjournal && !pl.inv.showinvent && 
             (pl.IM.exit_b||pl.IM.menu_b) )
        {
            pl.IM.ActionDelay = Time.fixedTime + 0.5f;
            pl.Chatting = false;
            pl.ChattingObject = null;
            pl.menu.ONOFFUI(ChattingUIObject.transform, false);

        }


        if (( pl.GetMouseOBCollList().Contains(gameObject)) && ChattingUIObject.GetComponent<Dialog>().LastLine)
        {
            if (QuestID > -1)
            {
                if (!pl.journal.CheckQuestDone(QuestID))
                {
                    pl.journal.AddQuest(QuestID);

                    ChattingUIObject.GetComponent<Dialog>().LastLine = false;
                }
            }

        }

        if (pl.ChattingObject== gameObject && 
        !ChattingUIObject.GetComponent<Image>().enabled)
        {
            if (pl.Chatting && pl.ChattingObject == gameObject)
            {
                pl.menu.ONOFFUI(ChattingUIObject.transform, false);

            ChattingUIObject.GetComponent<Dialog>().ResetDialog();

            if (DestroyOnDialogEnd)
            {
                Destroy(ChattingObject);
                Destroy(gameObject);


            }

            pl.Chatting = false;
                pl.ChattingObject = null;
            }

        }
        


    }


    void UIAlphaAndColor()
    {
        float uibordershow = 1;

        SpriteRenderer CO_SPRT = ChattingObject.GetComponent<SpriteRenderer>();


        if (!GetComponent<SpriteRenderer>().enabled)
        {

            if (ChattingObject.transform.Find("ENTER_B") != null)
                ChattingObject.transform.Find("ENTER_B").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);


            CO_SPRT.color = new Color(1, 1, 1, 0);
            ChattingObject.transform.Find("QuestItem").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

            return;
        }




        ChattingObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 1);

        Vector3 Mousepos = pl.MouseOB.transform.position;
   
            if (Mathf.Abs(transform.position.x - Mousepos.x) < uibordershow && Mathf.Abs(transform.position.y - Mousepos.y) < uibordershow)
            {
                float maxalpha = 1;
                if (!pl.GetMouseOBCollList().Contains(gameObject)) maxalpha = 0.3f;
                else maxalpha = 1;



            if (CO_SPRT.color.a < maxalpha)
                CO_SPRT.color = new Color(1, 1, 1, CO_SPRT.color.a + 3 * Time.deltaTime);
            else CO_SPRT.color = new Color(1, 1, 1, maxalpha);


                if (ChattingObject.transform.Find("QuestItem").GetComponent<SpriteRenderer>().color.a < 1)
                    ChattingObject.transform.Find("QuestItem").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, CO_SPRT.color.a + 3 * Time.deltaTime);


            }
            else if (CO_SPRT.color.a > 0)
            {
            CO_SPRT.color = new Color(1, 1, 1, CO_SPRT.color.a - 3 * Time.deltaTime);
                ChattingObject.transform.Find("QuestItem").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, CO_SPRT.color.a - 3 * Time.deltaTime);
            }

            if (ChattingObject.transform.Find("ENTER_B") != null)
                ChattingObject.transform.Find("ENTER_B").GetComponent<SpriteRenderer>().color = ChattingObject.GetComponent<SpriteRenderer>().color;


        
       
    }



}
