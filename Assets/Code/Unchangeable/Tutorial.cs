using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class TutorialPhase
{
    public GameObject[] PhaseObjects;
    public GameObject GameObjectTarget;
    public GameObject  UICraftSlots;
    public GameObject InventorySlot;
    public int TargetInventoryID = -10;
}

    public class Tutorial : MonoBehaviour
{

    private Constructor Const;
    private Player pl;
    private BlueprintMenu BlueMenu;

    private int Language;

    private int Phase { get; set; }
    public List<TutorialPhase> PhaseParts = new List<TutorialPhase>();



    private GameObject TutorialButton;

    private GameObject TutorialUI;
    private RectTransform TutorialUI_RectTr;
    void Start()
    {
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();

      
        BlueMenu = GameObject.Find("BlueprintMenu").GetComponent<BlueprintMenu>();



        Const.SetBigTip(0);

       


        TutorialButton = GameObject.Find("TutorialButton");
        TutorialUI = GameObject.Find("TutorialUI");
        TutorialUI_RectTr = GameObject.Find("TutorialUI").GetComponent<RectTransform>();





    }

    private void Update()
    {
    
        TutorialUpdate();

    }

    void TutorialUpdate()
    {



        if ((pl.menu.UIColl(TutorialButton) && pl.IM.LeftMouseButtonDown) || pl.IM.OKey)
        {
   
            Const.TipsReminder(Phase);
        }



        if (pl.IM.enter_b  || (pl.menu.UIColl(GameObject.Find("CloseBigTips")) && pl.IM.LeftMouseButtonDown) || Const.TutorialPause == false)
        {
            if (Phase == 0)
            {
                pl.inv.SetFolder(1);
                SetPhase(1);
                print("PHASE 1");
            }
        }

        //----------Build a ground

        if (Const.LastBuildingConstructed == 301 && Phase == 1)
        {
            pl.inv.SetFolder(0);
            SetPhase(2);
        }

        //------Build The wall
        if ((Const.LastBuildingConstructed == 300 ||
            Const.LastBuildingConstructed == 600 ||
            Const.LastBuildingConstructed == 601 ||
            Const.LastBuildingConstructed == 602 ||
            Const.LastBuildingConstructed == 603 ||
            Const.LastBuildingConstructed == 604 ||
            Const.LastBuildingConstructed == 605) && Phase == 2)
        {
            SetPhase(3);
            pl.inv.AddItem(9, 20, -1, new Vector2(9999, 9999));
        }


        //------Build a house
        if ((Const.LastBuildingConstructed == 352 ||
           Const.LastBuildingConstructed == 390 ||
           Const.LastBuildingConstructed == 391 ||
           Const.LastBuildingConstructed == 392 ||
           Const.LastBuildingConstructed == 393 ||
           Const.LastBuildingConstructed == 394 ||
           Const.LastBuildingConstructed == 395) && Phase == 3)
        {
            pl.inv.SetFolder(1);
            SetPhase(4);

            pl.inv.AddItem(9, 20, -1, new Vector2(9999, 9999));
        }

        //------Place dirt
        if (Const.LastBuildingConstructed == 353 && Phase == 4)
        {
            pl.inv.SetFolder(2);
            SetPhase(5);
            pl.inv.AddItem(9, 160, -1, new Vector2(9999, 9999));
        }


        //------Build a plant
        if (Const.LastBuildingConstructed == 1300 && Phase == 5)
        {
            pl.inv.SetFolder(0);
            SetPhase(6);
            pl.inv.AddItem(9, 160, -1, new Vector2(9999, 9999));
        }



         
        //------Buy new item

        if ((pl.inv.LastAddedItem == 4000) && Phase == 6)
        {
            pl.inv.SetFolder(1);
   
            pl.inv.AddItem(9, 20, -1, new Vector2(9999, 9999));
            pl.IM.ActionDelay = Time.fixedTime + 0.1f;

            if (pl.inv.CraftingUIOB != null)
            {
                pl.inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CloseUI();
            }
            pl.inv.showinvent = false;

            SetPhase(7);

            /*if (!Const.TutorialPause && !pl.inv.showinvent)
                pl.inv.StartInventory();
            */

        }


     

        //------Place grass
        if (Const.LastBuildingConstructed == 307 && Phase == 7)
        {
          
            pl.inv.SetFolder(0);
            SetPhase(8);
            pl.inv.AddItem(9, 160, -1, new Vector2(9999, 9999));
        }

        //------Build Lake

        if ((Const.LastBuildingConstructed == 4000 || Const.LastBuildingConstructed == 4001) && Phase == 8)
        {

            SetPhase(9);
            pl.inv.AddItem(9, 20, -1, new Vector2(9999, 9999));
            pl.IM.ActionDelay = Time.fixedTime + 0.1f;

            if (pl.inv.CraftingUIOB != null)
            {
                pl.inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CloseUI();
            }
            pl.inv.showinvent = false;


            if (!Const.TutorialPause && !pl.inv.showinvent)
                pl.inv.StartInventory();


        }



        //------Build a blueprint


        if (BlueMenu.CheckBlueprint(0) && Phase == 9)
        {
            SetPhase(10);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        //------Build protections


        if (Const.LastBuildingConstructed == 370 && Phase == 10)
        {
            SetPhase(11);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        if (Const.LastBuildingConstructed == 375 && Phase == 11)
        {
            SetPhase(12);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        if (Const.LastBuildingConstructed == 398 && Phase == 12)
        {
            SetPhase(13);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        //----------------Ending

        if (Phase >= 13 && pl.IM.ActionDelay < Time.fixedTime)
        {
            if (pl.IM.enter_b || pl.IM.exit_b || (pl.menu.UIColl(GameObject.Find("CloseBigTips")) && pl.IM.LeftMouseButtonDown) || Const.TutorialPause == false)
            {
                //pl.inv.ReduceItemCount(9, pl.inv.GetItem(9).Count );
                Const._menu.FirstStart = 0;
                
                Const._menu.TransitionToTheScene("Main location", false);
                
            }
        }


        if (PhaseParts[Phase].GameObjectTarget != null)
        {
            if (PhaseParts[Phase].GameObjectTarget.name.Contains("Inventory"))
            {
                for (int ii = 0; ii < pl.inv.inventory.Count; ii++)
                {
                    if (pl.inv.inventory[ii].itemID == PhaseParts[Phase].TargetInventoryID)
                        PhaseParts[Phase].InventorySlot = pl.inv.slots[ii];
                }
            }
        }



        if (!pl.inv.crafting)
        {

            if (!pl.inv.showinvent)
            {
                if (PhaseParts[Phase].GameObjectTarget!= null)
                {
                    if (PhaseParts[Phase].GameObjectTarget.GetComponent<RectTransform>() == null)
                    {
                        TutorialUI_RectTr.position = pl.MainCamera.WorldToScreenPoint(PhaseParts[Phase].GameObjectTarget.transform.position) + new Vector3(-100, 0, 0);
                        TutorialUI_RectTr.rotation = Quaternion.Euler(0, 0, 0);

                    }
                    else
                    {
                        TutorialUI_RectTr.position = PhaseParts[Phase].GameObjectTarget.GetComponent<RectTransform>().position + new Vector3(0, 100, 0);
                        TutorialUI_RectTr.rotation = Quaternion.Euler(0, 0, -90);

                    }
                }
                else TutorialUI_RectTr.position = new Vector3(9999, 9999, 0);
            }
            else
            {
                if (PhaseParts[Phase].InventorySlot != null)
                {
                    TutorialUI_RectTr.position = PhaseParts[Phase].InventorySlot.GetComponent<RectTransform>().position + new Vector3(0, 100, 0);
                    TutorialUI_RectTr.rotation = Quaternion.Euler(0,0,-90);

                }
                else TutorialUI_RectTr.position = new Vector3(9999, 9999, 0);
            }


        }
        else
        {
            if (PhaseParts[Phase].UICraftSlots != null)
            {
                TutorialUI_RectTr.position = PhaseParts[Phase].UICraftSlots.GetComponent<RectTransform>().position + new Vector3(0, 100, 0);
                TutorialUI_RectTr.rotation = Quaternion.Euler(0, 0, -90);
            }
            else TutorialUI_RectTr.position = new Vector3(9999, 9999, 0);

        }



        for (int i = 0; i < PhaseParts.Count; i++)
        {
          


            if (i <= Phase)
            {

                for (int j = 0; j < PhaseParts[i].PhaseObjects.Length; j++)
                {
                    ONOFF(true, PhaseParts[i].PhaseObjects[j]);
                    if (PhaseParts[i].PhaseObjects[j]!=null)
                    print("PHASE TRUE " + PhaseParts[i].PhaseObjects[j].name);
                }
            }
            else
            {
            for (int j = 0; j < PhaseParts[i].PhaseObjects.Length; j++)
                ONOFF(false, PhaseParts[i].PhaseObjects[j]);
            }
            

        }

        
        
    }
    public int GetPhase()
    {
        return Phase;

    }

    public void SetPhase(int phase)
    {
        Phase = phase;

        if (Const == null)
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();

        Const.SetBigTip(phase);
        
    }

    void ONOFF(bool TF, GameObject Obj)
    {
        if (Obj != null)
        {
            if (Obj.GetComponent<Enemies>() != null) Obj.GetComponent<Enemies>().enabled = TF;
            if (Obj.GetComponent<SpriteRenderer>() != null) Obj.GetComponent<SpriteRenderer>().enabled = TF;
            if (Obj.GetComponent<Image>() != null) Obj.GetComponent<Image>().enabled = TF;
            if (Obj.GetComponent<TextMeshProUGUI>() != null) Obj.GetComponent<Text>().enabled = TF;
            if (Obj.GetComponent<BoxCollider2D>() != null) Obj.GetComponent<BoxCollider2D>().enabled = TF;
            if (Obj.GetComponent<Scrollbar>() != null) Obj.GetComponent<Scrollbar>().enabled = TF;
            if (Obj.GetComponent<RecipeUnlock>() != null) Obj.GetComponent<RecipeUnlock>().enabled = TF;
            if (Obj.GetComponent<MovementControll>() != null) Obj.GetComponent<MovementControll>().enabled = TF;
            if (Obj.GetComponent<CharacterMove>() != null) Obj.GetComponent<CharacterMove>().enabled = TF;
            if (Obj.GetComponent<StatsControll>() != null) Obj.GetComponent<StatsControll>().enabled = TF;
            if (Obj.GetComponent<GetItem>() != null) Obj.GetComponent<GetItem>().enabled = TF;

            for (int i = 0; i < Obj.transform.childCount; i++)
            {
                if (Obj.transform.GetChild(i).GetComponent<Image>() != null)
                    Obj.transform.GetChild(i).GetComponent<Image>().enabled = TF;

                if (Obj.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    Obj.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = TF;

                if (Obj.transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
                    Obj.transform.GetChild(i).GetComponent<TextMeshProUGUI>().enabled = TF;

                if (Obj.transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
                    Obj.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = TF;

                if (Obj.transform.GetChild(i).GetComponent<Scrollbar>() != null)
                    Obj.transform.GetChild(i).GetComponent<Scrollbar>().enabled = TF;

                for (int ii = 0; ii < Obj.transform.GetChild(i).childCount; ii++)
                {
                    if (Obj.transform.GetChild(i).GetChild(ii).GetComponent<Image>() != null)
                        Obj.transform.GetChild(i).GetChild(ii).GetComponent<Image>().enabled = TF;

                    if (Obj.transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>() != null)
                        Obj.transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>().enabled = TF;

                    if (Obj.transform.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>() != null)
                        Obj.transform.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>().enabled = TF;

                    if (Obj.transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>() != null)
                        Obj.transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>().enabled = TF;

                    if (Obj.transform.GetChild(i).GetChild(ii).GetComponent<Scrollbar>() != null)
                        Obj.transform.GetChild(i).GetChild(ii).GetComponent<Scrollbar>().enabled = TF;

                    for (int iii = 0; iii < Obj.transform.GetChild(i).GetChild(ii).childCount; iii++)
                    {
                        if (Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>() != null)
                            Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>().enabled = TF;

                        if (Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<SpriteRenderer>() != null)
                            Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<SpriteRenderer>().enabled = TF;


                        if (Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>() != null)
                            Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>().enabled = TF;

                        if (Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<BoxCollider2D>() != null)
                            Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<BoxCollider2D>().enabled = TF;

                        if (Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Scrollbar>() != null)
                            Obj.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Scrollbar>().enabled = TF;

                    }
                }

            }
        }
    }



}
