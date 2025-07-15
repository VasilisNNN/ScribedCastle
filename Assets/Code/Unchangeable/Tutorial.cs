using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using static UnityEngine.EventSystems.StandaloneInputModule;

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

    private Constructor Constr;
    private Player pl;
    private BlueprintMenu BlueMenu;

    private int Language;

    private int Phase { get; set; }
    public List<TutorialPhase> PhaseParts = new List<TutorialPhase>();



    private GameObject TutorialButton;

    private GameObject TutorialUI;
    private RectTransform TutorialUI_RectTr;
    private GameObject Merchant;


    [HideInInspector]
    public List<int> TutorialPhaseBigTip = new List<int>();

    private GameObject  TipsPause;
    private TextDatabase textdatabase;
    private MenuCustom _menu;
    private InputMode IM;
    void Awake()
    {
     
        Merchant = GameObject.Find("Merchant");
        Merchant.SetActive(false);

        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;
        textdatabase = InitializeObjects.Textdatabase;
        BlueMenu = GameObject.Find("BlueprintMenu").GetComponent<BlueprintMenu>();
        _menu = Constr.GetComponent<MenuCustom>();
        TipsPause = GameObject.Find("TipsPause");

            TipsPause.SetActive(false);


        TutorialButton = GameObject.Find("TutorialButton");
        TutorialUI = GameObject.Find("TutorialUI");
        TutorialUI_RectTr = GameObject.Find("TutorialUI").GetComponent<RectTransform>();

        IM = pl.IM;



        SetBigTip(0);

       





    }

    private void Update()
    {
    
        TutorialUpdate();

    }

    void TutorialUpdate()
    {
        if (pl.inv.blueprintshow)
        {
            UnsetBigTips();
        }


        if (Constr.TutorialPause)
        {
            if (!IM.joystick)
            {
                if ((pl.GetMouseCollList().Contains(GameObject.Find("CloseBigTips")) && IM.LeftMouseButtonDown) || IM.exit_b || IM.menu_b || IM.enter_b)
                {
                    UnsetBigTips();
                }

            }
            else
            if ((IM.menu_b || IM.exit_b || IM.enter_b) && Constr.TutorialPause && TipsPause.activeInHierarchy && IM.ActionDelay < Time.fixedTime)
            {

                UnsetBigTips();
            }



            

            pl.menu.MenuActionDelay = Time.fixedTime + 0.2f;
        }

        if ((pl.menu.UIColl(TutorialButton) && pl.IM.LeftMouseButtonDown) || pl.IM.OKey)
        {

            TipsReminder(Phase);
        }



        if (pl.IM.enter_b  || (pl.menu.UIColl(GameObject.Find("CloseBigTips")) && pl.IM.LeftMouseButtonDown) || 
            Constr.TutorialPause == false)
        {
            if (Phase == 0)
            {
                pl.inv.SetFolder(1);
                SetPhase(1);
                print("PHASE 1");
            }
        }

        //----------Build a ground

        if (Constr.LastBuildingConstructed == 301 && Phase == 1)
        {
            pl.inv.SetFolder(0);
            pl.inv.AddItem(600, 5,99,pl._transform.position);
            SetPhase(2);
        }

        //------Build The wall
        if ((Constr.LastBuildingConstructed == 600 ||
            Constr.LastBuildingConstructed == 601 ||
            Constr.LastBuildingConstructed == 602 ) && Phase == 2)
        {
            pl.inv.AddItem(352, 1, 99, pl._transform.position); 
            SetPhase(3);
            pl.inv.AddItem(9, 20, -1, new Vector2(9999, 9999));
        }


        //------Build a house
        if ((Constr.LastBuildingConstructed == 352 ) && Phase == 3)
        {
            pl.inv.SetFolder(1);
            SetPhase(4);
            pl.inv.AddItem(353, 20, -1, new Vector2(9999, 9999));
            pl.inv.AddItem(9, 20, -1, new Vector2(9999, 9999));
        }

        //------Place dirt
        if (Constr.LastBuildingConstructed == 353 && Phase == 4)
        {
            pl.inv.SetFolder(2);
            SetPhase(5);
            pl.inv.AddItem(9, 160, -1, new Vector2(9999, 9999));
            pl.inv.AddItem(1300, 5, -1, new Vector2(9999, 9999));
            
        }


        //------Build a plant
        if (Constr.LastBuildingConstructed == 1300 && Phase == 5)
        {
            pl.inv.SetFolder(0);
            SetPhase(6);
            Merchant.SetActive(true);
            pl.inv.AddItem(9, 160, -1, new Vector2(9999, 9999));
        }



         
        //------Buy new item

        if ((pl.inv.LastAddedItem == 307) && Phase == 6)
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
        if (Constr.LastBuildingConstructed == 307 && Phase == 7)
        {
          
            pl.inv.SetFolder(0);
            SetPhase(8);
            pl.inv.AddItem(9, 160, -1, new Vector2(9999, 9999));
        }

        



        //------Build a blueprint


        if (BlueMenu.CheckBlueprint(0) && Phase == 8)
        {
            SetPhase(9);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        //------Build protections


        if (Constr.LastBuildingConstructed == 370 && Phase == 9)
        {
            SetPhase(10);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        if (Constr.LastBuildingConstructed == 375 && Phase == 10)
        {
            SetPhase(11);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        if (Constr.LastBuildingConstructed == 398 && Phase == 11)
        {
            SetPhase(12);

            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
        }


        //----------------Ending

        if (Phase >= 12 && pl.IM.ActionDelay < Time.fixedTime)
        {
            if (pl.IM.enter_b || pl.IM.exit_b || (pl.menu.UIColl(GameObject.Find("CloseBigTips")) &&
                pl.IM.LeftMouseButtonDown) || 
                Constr.TutorialPause == false)
            {
                Constr._menu.FirstStart = 0;

                Constr._menu.TransitionToTheScene("Main location", false);
                
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

        if (Constr == null)
            Constr = InitializeObjects.Constr;

        SetBigTip(phase);
        
    }

    public void SetBigTip(int texttipnum)
    {
        print("SetBigTip 0");

        if (TutorialPhaseBigTip.Contains(texttipnum))
        {
            Constr.TutorialPause = false;
            TipsPause.SetActive(false);
            TutorialPhaseBigTip.Add(texttipnum);

            return;
        }
       
        if (texttipnum <= -1)
        {

            TutorialPhaseBigTip.Add(texttipnum);
            return;
        }
      
        if (_menu.DrawTutorial == 0)
        {
            Constr.TutorialPause = false;
            TipsPause.SetActive(false);
            TutorialPhaseBigTip.Add(texttipnum);
            return;
        }
    

        if (textdatabase.textEN[NumberInData(texttipnum)].line[0].line[0] != "" && !Constr.TutorialPause)
        {
            TipsPause.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = textdatabase.GetFirstLine(texttipnum, _menu.Language);


            Constr.TutorialPause = true;
            TipsPause.SetActive(true);


        }

        TutorialPhaseBigTip.Add(texttipnum);
    }


    public void UnsetBigTips()
    {
        if (!Constr.TutorialPause) return;

        if (pl != null && pl.inv != null && TipsPause != null)
            TipsPause.SetActive(false);
        Constr.TutorialPause = false;

        Constr.OnUIDelay = Time.fixedTime + 0.1f;
        if (IM != null)
            IM.ActionDelay = Time.fixedTime + 0.1f;
        Constr.SetObjectDelay = Time.fixedTime + 1;

    }




    public void TipsReminder(int texttipnum)
    {
        if (_menu.MenuONOFF || pl.inv.showinvent) return;

        TipsPause.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = textdatabase.GetFirstLine(texttipnum, _menu.Language);
        Constr.TutorialPause = true;
        TipsPause.SetActive(true);


    }


    public int NumberInData(int ID)
    {

        int r = 0;
        for (int i = 0; i < textdatabase.textEN.Count; i++)
        {
            if (textdatabase.textEN[i].ID == ID)
            {
                // print("textdatabase.textEN[i].ID" + textdatabase.textEN[i].ID);
                r = i;
            }
            //   else print("ID NOT FOUND!");
        }
        return r;
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
            if (Obj.GetComponent<CharacterPath>() != null) Obj.GetComponent<CharacterPath>().enabled = TF;
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
