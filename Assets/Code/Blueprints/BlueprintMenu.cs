using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class BlueprintMenu : MonoBehaviour
{
    public List<Blueprint> BP = new List<Blueprint>();


    private List<GameObject> BluePrintObjects = new List<GameObject>();
    private List<GameObject> BlueFloorObjects = new List<GameObject>();
    private List<GameObject> Rewards = new List<GameObject>();
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject PlayButton;

    private MenuCustom menu;

    private int CurrentBP;
    public bool showbp { get;  set; }
    private Inventory inv;
    private Constructor _constr;
    private Player pl;
    private bool MenuCreated;

    private int BackToNormalNumber;

    private float DisassembleTimer;
    private GameObject BlueprintsButton;

    public AudioClip[] AC;
    public AudioClip[] Assemble;
    public AudioClip[] Constructed;

    private float BPSlotWidth = 1100;
    private bool Assembled;
    private AudioSource AS;

    private float ConstrcuctedDelay;
    private GameObject StatsOBJ;

    private int[] ConstructionStates;

    private Canvas _canvas;
    private float ScrollDelay = 0;
    public int LastBlueprint { get; set; }

    private TextMeshProUGUI BlueprintText;
    private GameObject EscapeBlueprint;

    private BlueprintDatabase BData;
    private Transform BlueprintNameBG, BlueprintBG;
    private TextMeshProUGUI BlueprintNameText, BlueprintDescText;
    private float partWidth = 80;

    private int FadePosition;
    private GameObject FadeCursor;
    void Start()
    {
        BData = gameObject.AddComponent<BlueprintDatabase>();
        BlueprintNameText = GameObject.Find("BlueprintNameText").GetComponent<TextMeshProUGUI>();
        BlueprintDescText = GameObject.Find("BlueprintDescText").GetComponent<TextMeshProUGUI>();

        BlueprintNameBG = GameObject.Find("BlueprintNameBG").GetComponent<Transform>();

        BlueprintBG = GameObject.Find("BlueprintBG").GetComponent<Transform>();

        EscapeBlueprint = GameObject.Find("EscapeBlueprint");

        BlueprintText = transform.Find("BlueprintText").GetComponent<TextMeshProUGUI>();
        LastBlueprint = -1;

        FadeCursor = transform.Find("FadeCursor").gameObject;

        StatsOBJ = GameObject.Find("Stats");
        AS = GetComponent<AudioSource>();

        if (AC.Length<=0)
        AC = new AudioClip[1] { Resources.Load<AudioClip>("Sound/UI/Accept") };

        if (Assemble.Length <= 0)
        Assemble = new AudioClip[1] { Resources.Load<AudioClip>("Sound/UI/Click_0") };


        if (Constructed.Length <= 0)
            Constructed = new AudioClip[1] { Resources.Load<AudioClip>("Sound/UI/Click_0") };
        

        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        menu = GameObject.Find("Constructor").GetComponent<MenuCustom>();
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        pl = GameObject.Find("Player").GetComponent<Player>();



        BlueprintsButton = GameObject.Find("BlueprintsButton");

        for (int i = 0; i < BP.Count; i++)
        {
            BP[i].Unlocked = false;
            BP[i].UpdateBP();
           
        }
        Assembled = true;

        _canvas =  GameObject.Find("Canvas").GetComponent<Canvas>();

        menu.ONOFFUI(transform, false);

    }

    void CleanMenu()
    {
        for (int i = 0; i < BluePrintObjects.Count; i++)
        {
            Destroy(BluePrintObjects[i]);

        }


        for (int i = 0; i < Rewards.Count; i++)
        {
                Destroy(Rewards[i]);

        }
      
        for (int i = 0; i < BlueFloorObjects.Count; i++)
         {
             Destroy(BlueFloorObjects[i]);

         }

       
        Rewards = new List<GameObject>();

        BlueFloorObjects = new List<GameObject>();
        BluePrintObjects = new List<GameObject>();
    }
 
    void CreateMenu()
    {

        for (int i = 0; i < BP.Count; i++)
        {
            
            GameObject BluePr = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/BlueprintBase"), transform);
            Vector2 BluePrPOS = new Vector2(i * 500, 1);
            BluePr.GetComponent<RectTransform>().anchoredPosition = BluePrPOS;
            BluePrintObjects.Add(BluePr);

            GameObject BlueFloor = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/BlueprintFloor"), transform);
      
            BlueFloor.GetComponent<RectTransform>().anchoredPosition = BluePrPOS;
            BlueFloorObjects.Add(BlueFloor);
            BlueFloor.GetComponent<Image>().enabled = false;

          
            for (int ii = 0; ii < BP[i].ObjectList.Count; ii++)
            {
            
             

               

                GameObject BluePrintBrick = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/BlueprintPart"), BluePr.transform);
                BluePrintBrick.GetComponent<RectTransform>().anchoredPosition = new Vector2(BP[i].ObjectList[ii].Place.x * partWidth, BP[i].ObjectList[ii].Place.y * partWidth);
                BluePrintBrick.GetComponent<Image>().sprite = BP[i].ObjectList[ii].Object.GetComponent<SpriteRenderer>().sprite;

                if (BP[i].ObjectList[ii].hasParrent) BluePrintBrick.name += "Child";

                BluePrintBrick.GetComponent<Image>().enabled = false;
                BluePrintBrick.transform.SetSiblingIndex(Mathf.Abs(BP[i].ObjectOrder[ii]));

             
            }
            

            float color =1;
     
            for (int x = -5; x < 6; x++)
            {
                for (int y = -5; y < 6; y++)
                {
                    GameObject BlueFloorPart = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/BlueprintFloor"), BlueFloor.transform);
                    Vector2 BlueFloorPartPOS = new Vector2(x * partWidth , y * (partWidth) );
                    BlueFloorPart.GetComponent<RectTransform>().anchoredPosition = BlueFloorPartPOS;
                    BlueFloorPart.transform.SetAsFirstSibling();


                    for (int ii = 0; ii < BluePr.transform.childCount; ii++)
                    {
                        if (BP[i].ObjectList[ii].Object.GetComponent<StatsControll>() != null &&
                            !BluePr.transform.GetChild(ii).name.Contains("Child") &&
                            ConvertBlueFloorPart_To_BluePrintObjects(BluePr.transform.GetChild(ii).GetComponent<RectTransform>().anchoredPosition) == BlueFloorPart.GetComponent<RectTransform>().anchoredPosition)
                        {
                          
                           
                            TileBase tileBase = BP[i].ObjectList[ii]._TileBase;

                            RuleTile ruleTile = tileBase as RuleTile;

                            BlueFloorPart.GetComponent<Image>().sprite = ruleTile.m_DefaultSprite;

                        }
                    }

                    BlueFloorPart.GetComponent<Image>().enabled = false;
                    BlueFloorPart.GetComponent<Image>().color = new Color(color, color, color, 1);
                    color -= 0.005f;
                }
            }


            BlueFloor.transform.SetAsFirstSibling();
        }


        for (int i = 0; i < BlueFloorObjects.Count; i++)
        {
            GameObject Reward = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/Reward"), BlueFloorObjects[i].transform);
            Vector2 RewardPOS = new Vector2(0, -300);
            Reward.GetComponent<RectTransform>().anchoredPosition = RewardPOS;
            Reward.GetComponent<Image>().enabled = false;
            Reward.transform.Find("Text").GetComponent<TextMeshProUGUI>().enabled = false;
            Rewards.Add(Reward);

        }
     

        LeftArrow.transform.SetAsLastSibling();
     
        RightArrow.transform.SetAsLastSibling();


        LeftArrow.GetComponent<Image>().enabled = false;
        RightArrow.GetComponent<Image>().enabled = false;

        transform.Find("BG").SetAsFirstSibling();
        transform.Find("BG").GetComponent<Image>().enabled = false;
        menu.ONOFFUI(LeftArrow.transform, false);


       // _canvas.ForceUpdateCanvases();
    }




    void Update()
    {
        if (!pl.StartLoading)
        {
            if (!MenuCreated)
            {

                CreateMenu();
                StartBluePrintsDone();
                MenuCreated = true;
            }

            EnableDisableMenu();
            MenuControlls();
            RewardsControl();

            GetRewards();

        }

    }


    void RewardsControl()
    {

     
        for (int i = 0; i < Rewards.Count; i++)
        {
            TextMeshProUGUI TMesh = Rewards[i].transform.Find("Text").GetComponent<TextMeshProUGUI>();


            TMesh.text = "";

            if(menu.Language ==0)
                TMesh.text += "Reward: ";
            if (menu.Language == 1)
                TMesh.text += "Нагорода: ";

            if (menu.Language == 2)
                TMesh.text += "報酬: ";

            for (int j = 0; j < BP[i].Rewards.Length; j++)
                TMesh.text += inv.GetItemInDatabase(BP[i].Rewards[j].itemID).itemNames[menu.Language] + " x" + BP[i].Rewards[j].Count + "\n";
        }
    }



void MoveBlueprints()
    {
        if (BlueFloorObjects.Count < BluePrintObjects.Count) return;

        for (int i = 0; i < BluePrintObjects.Count; i++)
        {
            Vector2 BluePrPOS = new Vector2(Mathf.Lerp(BluePrintObjects[i].GetComponent<RectTransform>().anchoredPosition.x, i * BPSlotWidth - CurrentBP* BPSlotWidth, Time.deltaTime*15), 1);
            BluePrintObjects[i].GetComponent<RectTransform>().anchoredPosition = BluePrPOS;
            BlueFloorObjects[i].GetComponent<RectTransform>().anchoredPosition = BluePrPOS;

        }

    }

    void GetRewards()
    {
        
        for (int i = 0; i < BP.Count; i++)
        {

            if (!BP[i].Unlocked && menu.SL.BPConstructed[i] >0)
            {
                CleanMenu();
                BP[i].Unlocked = true;
                BP[i].UpdateBP();
                CreateMenu();

                BackToNormalNumber = 0;
            }

            if (BlueFloorObjects[i].transform.Find("BluePrintDone") != null)
                BlueFloorObjects[i].transform.Find("BluePrintDone").GetComponent<RectTransform>().anchoredPosition = new Vector2(BlueFloorObjects[i].GetComponent<RectTransform>().anchoredPosition.x + BPSlotWidth / 3, BlueFloorObjects[i].GetComponent<RectTransform>().anchoredPosition.y - BPSlotWidth / 3);

            if (menu.SL.BPConstructed[i] != 0) return;

            if (ReadObject(BP[i].ObjectList)  && BP[i].Rewards.Length>0)
            {
          
                for (int r = 0; r < BP[i].Rewards.Length; r++)
                    inv.AddItem(BP[i].Rewards[r].itemID, BP[i].Rewards[r].Count, inv.GetItemInDatabase(BP[i].Rewards[r].itemID).Durability, inv.transform.position);

               
                if (BlueFloorObjects[i].transform.Find("BluePrintDone") == null)
                {
                    GameObject BluePrintDone = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/BlueprintDone"), BlueFloorObjects[i].transform);
                     BluePrintDone.name = "BluePrintDone";

                }
               
                LastBlueprint = i;
                menu.SL.BPConstructed[i] = 1;

                

            }


         
        }



        
    }


    public void StartBluePrintsDone()
    {
      

        for (int i = 0; i < BP.Count; i++)
        {

            if (BlueFloorObjects[i].transform.Find("BluePrintDone") == null && menu.SL.BPConstructed[i] == 1)
            {
              

                GameObject BluePrintDone = Instantiate(Resources.Load<GameObject>("Prefabs/Blueprints/BlueprintDone"), BlueFloorObjects[i].transform);
                BluePrintDone.name = "BluePrintDone";


            }

        }

    }


    float DisAssembleXPos(RectTransform BrickRT, GameObject BP, int currentchild)
    {
        float result = 0;

        float partW = Mathf.Clamp(Screen.width / BP.transform.childCount/2, 10, partWidth);


        float finaldest = BP.GetComponent<RectTransform>().anchoredPosition.x + partW * currentchild - BP.transform.childCount * (partW / 2);

        result = Mathf.Lerp(BrickRT.anchoredPosition.x, finaldest, Time.deltaTime * 10);

        return result;
    }

    void AnimationDisAssemble()
    {
        if (DisassembleTimer < Time.fixedTime) return;

        int i = CurrentBP;
            for (int ii = 0; ii < BluePrintObjects[i].transform.childCount; ii++)
            {
          
             
     
                GameObject BluePrintBrick = BluePrintObjects[i].transform.GetChild(ii).gameObject;
                RectTransform BrickRT = BluePrintBrick.GetComponent<RectTransform>();

            float XPos = DisAssembleXPos(BrickRT, BluePrintObjects[i], ii); 
                float YPos = Mathf.Lerp(BrickRT.anchoredPosition.y, 300, Time.deltaTime * 10);

            BrickRT.anchoredPosition = new Vector2(XPos, YPos);
            }


            BackToNormalNumber = 0;
        Assembled = false;
    }
    void LayersAnimation()
    {
        Vector2 pos = pl.IM.MousePosition;
        float pw = partWidth;


        if (!pl.IM.MouseMode)
        {
            if (pl.IM._vertical > 0 && FadePosition < 6 && pl.IM.ActionDelay < Time.fixedTime)
            {
                FadePosition++;
                pl.IM.ActionDelay = Time.fixedTime + 0.1f;
            }
            if (pl.IM._vertical < 0 && FadePosition > -6 && pl.IM.ActionDelay < Time.fixedTime)
            {
                FadePosition--;
                pl.IM.ActionDelay = Time.fixedTime + 0.1f;
            }

       
#if UNITY_SWITCH
            pw = partWidth / 2 + partWidth/6;
#endif

            pos = new Vector2(0, BluePrintObjects[CurrentBP].GetComponent<RectTransform>().position.y + FadePosition * pw);

            FadeCursor.GetComponent<RectTransform>().position = 
               
                new Vector2(BluePrintObjects[CurrentBP].GetComponent<RectTransform>().position.x + pw * 5, BluePrintObjects[CurrentBP].GetComponent<RectTransform>().position.y + FadePosition * pw );


        }


        int CountCount = BluePrintObjects[CurrentBP].transform.childCount;

        if (pl.IM.MouseMode)
        {
            FadeCursor.SetActive(false);
            if ((Mathf.Abs(BluePrintObjects[CurrentBP].GetComponent<RectTransform>().position.x - pos.x) > 500) ||
                Mathf.Abs(BluePrintObjects[CurrentBP].GetComponent<RectTransform>().position.y - pos.y) > 300)
            {
                for (int i = 0; i < CountCount; i++)
                    ChangePartColor(i, 1);

                return;
            }
        }
        else
        {
            FadeCursor.SetActive(true);
            if (Mathf.Abs(FadePosition) >= 6)
            {
                for (int i = 0; i < CountCount; i++)
                    ChangePartColor(i, 1);

                return;
            }
        }



        if (DisassembleTimer > Time.fixedTime)
        {
            for (int i = 0; i < CountCount; i++)
                ChangePartColor(i, 1);
            return;
        }
        if (!Assembled) 
        {
            for (int i = 0; i < CountCount; i++)
                ChangePartColor(i, 1);
            return;
        }
        
        for (int i = 0; i < CountCount; i++)
        {
           
            if (!BP[CurrentBP].ObjectList[i].hasParrent)
            {
            
                if (Mathf.Abs(pos.y - BluePrintObjects[CurrentBP].transform.GetChild(i).GetComponent<RectTransform>().position.y) < pw/2)
                    ChangePartColor(i, 1);
                else ChangePartColor(i, 0);
            }
            else
            {

                if (Mathf.Abs((pos.y + BP[CurrentBP].ObjectList[i].orderinParrent * pw) - BluePrintObjects[CurrentBP].transform.GetChild(i).GetComponent<RectTransform>().position.y) < pw/2)
                    ChangePartColor(i, 1);
                else ChangePartColor(i, 0);
            }

        }

    }


    void ChangePartColor(int i, float Alpha)
    {
        BluePrintObjects[CurrentBP].transform.GetChild(i).GetComponent<Image>().color = 
          new Color(1, 1, 1, Mathf.Lerp(BluePrintObjects[CurrentBP].transform.GetChild(i).GetComponent<Image>().color.a, Alpha,Time.deltaTime*10));

    }

    void AnimationBackToNormal()
    {

        if (DisassembleTimer > Time.fixedTime) return;
        
        int i = CurrentBP;
            int ii = BackToNormalNumber;
        if (BluePrintObjects.Count <= 0) return;
        if (BP.Count <= 0) return;
        if (BP[i].ObjectList.Count <= 0) return;

        if (BluePrintObjects[i].transform.childCount > 0)
        {

            RectTransform RT = BluePrintObjects[i].transform.GetChild(BackToNormalNumber).GetComponent<RectTransform>();

            
            float StartXPos = DisAssembleXPos(RT, BluePrintObjects[i], BackToNormalNumber); 
            float StartYPos = 300;

            if (Mathf.Abs(RT.anchoredPosition.x - StartXPos) < 10 && Mathf.Abs(RT.anchoredPosition.y - StartYPos)< 10) menu.PlayAudio(Assemble[Random.Range(0, Assemble.Length)]);

            RT.anchoredPosition =
            new Vector2(Mathf.Lerp(RT.anchoredPosition.x, BP[i].ObjectList[BackToNormalNumber].Place.x * partWidth, Time.deltaTime*10),
                        Mathf.Lerp(RT.anchoredPosition.y, BP[i].ObjectList[BackToNormalNumber].Place.y * partWidth, Time.deltaTime*10));


            float XAbs = Mathf.Abs(RT.anchoredPosition.x - BP[i].ObjectList[BackToNormalNumber].Place.x * partWidth);
            float YAbs = Mathf.Abs(RT.anchoredPosition.y - BP[i].ObjectList[BackToNormalNumber].Place.y * partWidth);

         

            if (XAbs<1 && YAbs < 1 && !Assembled)
            {

                if (BackToNormalNumber < BluePrintObjects[i].transform.childCount - 1)
                {
          
                    //PlayAudio(Constructed[Random.Range(0, Constructed.Length)]);

                    BackToNormalNumber++;
                }
                else Assembled = true;

            }


        }


        
    }

    void MenuControlls()
    {
     

        if (!showbp) return;

      

        if (pl.menu.Language==0)
            BlueprintText.text = "Orders to scribe";

        if (pl.menu.Language ==1)
            BlueprintText.text = "Замовлення для малювання";

        if (pl.menu.Language == 2)
            BlueprintText.text = "";


        if (CurrentBP == 0)
            menu.ONOFFUI(LeftArrow.transform, false);


        if (CurrentBP >= BP.Count - 1)
            menu.ONOFFUI(RightArrow.transform, false);


        if ((menu.UIColl(LeftArrow) && (menu.IM.enter_b || menu.IM.LeftMouseButtonDown)) || (menu.IM._horizontal<0 && menu.IM._horizontalPush && ScrollDelay < Time.fixedTime) || (menu.IM.DPADX<0 && ScrollDelay<Time.fixedTime))
        {
            if (CurrentBP > 0)
            {
             

                menu.ONOFFUI(RightArrow.transform, true);

         
                CurrentBP--;
                pl.PlaySoundsPitched(AC[Random.Range(0, AC.Length)], 0.8f + CurrentBP * (0.2f / BP.Count));

                ResetAllPositions();
                BackToNormalNumber = 0;
                ScrollDelay = Time.fixedTime + 0.2f;
            }
        }


        if ((menu.UIColl(RightArrow) &&( menu.IM.enter_b || menu.IM.LeftMouseButtonDown)) || (menu.IM._horizontal > 0 && menu.IM._horizontalPush && ScrollDelay<Time.fixedTime) || (menu.IM.DPADX > 0 && ScrollDelay < Time.fixedTime))
        {
            if (CurrentBP < BP.Count - 1)
            {
                if (CurrentBP == BP.Count - 2)
                {
                    menu.ONOFFUI(RightArrow.transform, false);
                }

                menu.ONOFFUI(LeftArrow.transform, true);

       
                CurrentBP++;
                pl.PlaySoundsPitched(AC[Random.Range(0, AC.Length)], 0.8f + CurrentBP * (0.2f/ BP.Count));
                ResetAllPositions();
                BackToNormalNumber = 0;

                ScrollDelay = Time.fixedTime + 0.2f;
            }
        }
        //BP[CurrentBP].DatabaseID = CurrentBP;


            MoveBlueprints();

        BlueprintNameText.text = BData.items[BP[CurrentBP].DatabaseID].itemNames[menu.Language];
        BlueprintDescText.text = BData.items[BP[CurrentBP].DatabaseID].itemDesc[menu.Language];

        BlueprintNameBG.SetAsLastSibling();
        BlueprintBG.SetAsLastSibling();
        BlueprintNameText.transform.SetAsLastSibling();
        BlueprintDescText.transform.SetAsLastSibling();
        BlueprintText.transform.SetAsLastSibling();

        if (menu.IM.space_b || (menu.UIColl(PlayButton) && menu.IM.LeftMouseButtonDown) || menu.IM.enter_b)
        {
            menu.PlayAudio(AC[Random.Range(0, AC.Length)]);

            DisassembleTimer = Time.fixedTime + 0.5f;
        }

       
            AnimationDisAssemble();
            AnimationBackToNormal();
        LayersAnimation();


        if ( menu.IM.exit_b || menu.IM.menu_b)
        {

            FadePosition = 6;
            inv.ONOFF(StatsOBJ, true);
            ResetAllPositions();
                menu.ONOFFUI(transform, false);

            inv.PlaySoundsPitched(inv.UIOpen, 0.8f);

            menu.ONOFFUI(GameObject.Find("ButtonsUI").transform, true);
            menu.IM.ActionDelay = Time.fixedTime + 0.1f;
            menu.MenuActionDelay = Time.fixedTime + 0.1f;
            showbp = false;
            

        }


        

      


        
     
    }


    void EnableDisableMenu()
    {
        if (showbp)
        {
            if ((pl.GetMouseCollList().Contains(EscapeBlueprint) && pl.IM.LeftMouseButtonDown) || menu.IM.exit_b)
            {
                FadePosition = 6;
                showbp = false;
                pl.menu.PlayAudio(pl.menu.MenuChooseMove);

                menu.ONOFFUI(transform, false);
                menu.ONOFFUI(GameObject.Find("ButtonsUI").transform, true);
                inv.ONOFF(StatsOBJ, true);

                menu.IM.ActionDelay = Time.fixedTime + 0.2f;
            }
        }


        if ((menu.IM.BKey || (menu.UIColl(BlueprintsButton) && menu.IM.LeftMouseButtonDown && menu.IM.MouseMode)) && !menu.MenuONOFF && !inv.showjournal && menu.IM.ActionDelay < Time.fixedTime)
        {
            showbp = !showbp;

          

            if (showbp)
            {
                if (_constr != null)
                 _constr.UnsetBigTips();


                inv.PlaySoundsPitched(inv.UIOpen, 1);

                menu.ONOFFUI(transform, true);
                menu.ONOFFUI(GameObject.Find("ButtonsUI").transform, false);
                inv.ONOFF(StatsOBJ, false);
                inv.showinvent = false;
                
            }
            else
            {
                inv.PlaySoundsPitched(inv.UIOpen, 0.8f);

                menu.ONOFFUI(transform, false);
                menu.ONOFFUI(GameObject.Find("ButtonsUI").transform, true);
                inv.ONOFF(StatsOBJ, true);
            }

            menu.IM.ActionDelay = Time.fixedTime + 0.1f;
        }
    }



    void ResetAllPositions()
    {

        for (int i = 0; i < BluePrintObjects.Count; i++)
        {
         
            for (int ii = 0; ii < BluePrintObjects[i].transform.childCount; ii++)
            {
          

                if (BP[i].ObjectList.Count == 0) return;
           
                BluePrintObjects[i].transform.GetChild(ii).GetComponent<RectTransform>().anchoredPosition = new Vector2(BP[i].ObjectList[ii].Place.x * partWidth, BP[i].ObjectList[ii].Place.y * partWidth);
              
            }



        }

        Assembled = true;

        DisassembleTimer = -1;
    }



    void SetALLCandidate(int i, ObjectOnBoard OBN, ref List<ObjectOnBoard> AllCandidates)
    {
        if (_constr.OBOnBoard[i].ID != OBN.ID || AllCandidates.Contains(_constr.OBOnBoard[i]))
        {
            return;
        }



        if (_constr.OBOnBoard[i].Object == null) return;
        AllCandidates.Add(_constr.OBOnBoard[i]);
        Transform prnt = _constr.OBOnBoard[i].Object.transform.parent;

        if (prnt == null)
         return;

        if (prnt.GetComponent<StatsControll>() == null && prnt.GetComponent<PubObject>() == null)
            return;

        AllCandidates[AllCandidates.Count - 1].hasParrent = true;
 
        for (int p = 0; p < prnt.transform.childCount; p++)
        {
            if (AllCandidates[AllCandidates.Count - 1].Object == prnt.transform.GetChild(p).gameObject)
                AllCandidates[AllCandidates.Count - 1].orderinParrent = p + 1;
        }
                
            
        
    }



    bool ReadObject(List<ObjectOnBoard> objectList)
    {
        bool result = false;
      
        Vector2 StartPlace = new Vector2(0, 0);
        List<ObjectOnBoard> StartCandidates = new List<ObjectOnBoard>();
        List<ObjectOnBoard> AllCandidates = new List<ObjectOnBoard>();

        for (int i = 0; i < _constr.OBOnBoard.Count; i++)
        {
            if (_constr.OBOnBoard[i].ID == objectList[0].ID)
            {
                StartCandidates.Add(_constr.OBOnBoard[i]);

            }



            for (int ii = 0; ii < objectList.Count; ii++)
            {
                SetALLCandidate(i, objectList[ii], ref AllCandidates);
            }
            
        }

       
        for (int s = 0; s < StartCandidates.Count; s++)
        {

            ConstructionStates = new int[objectList.Count];

            StartPlace = StartCandidates[s].Place;

          
            for (int j = 0; j < objectList.Count; j++)
            {
                Vector2 place = StartPlace + objectList[j].Place;

             
                for (int i = 0; i < AllCandidates.Count; i++)
                {
                 
                    if (AllCandidates[i].ID == objectList[j].ID &&
                            AllCandidates[i].Place - StartPlace ==  objectList[j].Place - objectList[0].Place &&
                            objectList[j].hasParrent == AllCandidates[i].hasParrent && objectList[j].orderinParrent == AllCandidates[i].orderinParrent)
                    {

                
                        ConstructionStates[j] = 1;
                        
                       
                    }
                      
                }

            }



            if (ConstructionStates != null)
            {
                if (ConstructionStates.Sum() == objectList.Count) return true;
            }

        }


        return result;

    }

   



    Vector2 ConvertBlueFloorPart_To_BluePrintObjects(Vector2 ObjectListPlace)
    {
        float width = 50f;
        
        float x = (ObjectListPlace.x*2);
        float y = (ObjectListPlace.y*4);
        

        // Vector2 BlueFloorPartPOS = new Vector3( Mathf.Round(ObjectListPlace.x * 100), Mathf.Round(ObjectListPlace.y * 100));
        
        // Vector2 BlueFloorPartPOS = new Vector2(x * width - y * width, y * (width / 2) + x * (width / 2) + (width / 2));


        Vector2 BlueFloorPartPOS = new Vector2(ObjectListPlace.x , ObjectListPlace.y  - 25f);
      
        return BlueFloorPartPOS;
    }

    public bool CheckBlueprint(int Number)
    {
        if (menu.SL.BPConstructed[Number] == 1) return true;
        else return false;
        
    }


    public void PlayAudio(AudioClip AC)
    {
        AS.clip = AC;
        AS.Play();
    }


}
