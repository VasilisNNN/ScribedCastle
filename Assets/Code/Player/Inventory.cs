using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class Inventory : MonoBehaviour
{


    private List<string> FinalItem = new List<string>();
    public int slotX { get; set; }
    public int slotY;

    private int SlotSlide;



    private float DrawFinalItemTimer, AddMoneyTimer;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> inventoryFolder = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();


    private int craftingslotX = 8;
    private int craftingslotY = 3;

    public bool showinvent { get; set; }
    public float inventjustopenned { get; set; }
    public bool crafting { get; set; }
    public bool blueprintshow { get; set; }

    public ItemDatabase database { get; set; }

    private string tooltip;


    public int XChoise { get; set; }
    public int YChoise { get; set; }

    private Rect ActionRect;
    private float ActionPos, VertDelay;
    // Use this for initialization
    private float slotspace = 0;

    private Player pl;
    public int Money { get; set; }

    private Vector2 ScreenBorder;
    private Texture2D ChoiseTexture;


    private List<string> Picked = new List<string>();
    private List<float> PickedX = new List<float>();
    private List<float> PickedY = new List<float>();

    private List<float> PickedSlide = new List<float>();

    private List<int> PickedStyles = new List<int>();
    private List<float> PickedSpeeds = new List<float>();


    private bool NewQuestBool;
    public float HorDelay { get; set; }
    public float ExitingTimer { get; set; }

    
    private float CardYscroll, CardsWidth;

    private string[] SellTag;


    private float[] DescWidth;
    private MenuCustom _menu;
    public bool Exiting { get; set; }
    public float FadeAlpha { get; set; }
    public string ExitingString { get; set; }
    private Texture2D EnterTexture, EnterTexture_J, MoneyTexture, PhoneMessagePlayer, PhoneMessageLover;
    private int AddedMoney { get; set; }
    public int MaxAddedMoney { get; set; }
    public bool showjournal { get; set; }

    [HideInInspector]
    public AudioClip OpenInventory, OpenCardsInv, PickItem, ClickClip, UIOpen, ShakeClip, TakeItemClip;

    UnityEngine.Audio.AudioMixer aumixer;
    private float masterfloat;
    public bool devmode;

    private float Journal_YStart = 0;
    private Rect[] SlotsRect;
    private float XStart, WidthSlot;
    private InputMode IM;
    private GameObject InventoryUIOB, StatsUI, LogUI, BlueprintMenu, LeftFolder, RightFolder;
    public GameObject CraftingUIOB { get; private set; }
    
    public GameObject Choose { get; private set; }
    public int CurrentItem { get; private set; }

    public int CurrentItemID { get; private set; }
    public int CurrentQuest { get; private set; }

    public bool PauseInventory;
    public List<Quest> Quests = new List<Quest>();
    private QuestDatabase QD;

    [HideInInspector]
    public GameObject CraftingCross, NewQuest;

    public GameObject LeftArrow { get; private set; }
    public GameObject RightArrow { get; private set; }


    public GameObject EscapeInventory { get; private set; }


    private int Quest_YPos;
    private float Quest_YSlider;
    public GameObject ToolTip { get; private set; }
    private GameObject QuestMenu, Controlls;

    private bool DrawINV, DrawInvNo;
    private Constructor _constr;

    public GetItem CurrentCraftingTable;
    private bool CraftingDraw;

    public List<GameObject> NeedItemGameobject = new List<GameObject>();

    [HideInInspector]
    public GameObject InventoryButton, JournalButton;


    public bool ChooseTopSegmentSlot { get; set; }
    public Item BufferItem { get; set; }


    public ItemsSlotsUI VaultUI;
    private bool ShowAch;


    public int LastAddedItem = -1;
    public bool ShootPause;


    private List<GameObject> FolderButtons = new List<GameObject>();

    private int CurrentFolder;

    private string[] BodypartsNames;

    private string LockedString;
    private string BodyPartString;
    private string DamageString;
    private string DurabilityString;
    private string BulletDamageString;
    private string MaxHPString;
    private string VisionString;
    private string StaminaString;
    private string SatietyString;
    private string HPString;
    private string StaminaRecoveryspeedString;
    private string NeedsStaminaString;
    private string DashDurationString;
    private string FoodString;
    private string CostString;
    private string BuildingCostString;

    private Vector3 ShakePos;
    private float ShakeTimer;

    void Awake()
    {

        EscapeInventory = GameObject.Find("EscapeInventory");

        BufferItem = new Item();
        InventoryButton = GameObject.Find("InventoryButton");
        JournalButton = GameObject.Find("JournalButton");
        BlueprintMenu = GameObject.Find("BlueprintMenu");

        CraftingCross = GameObject.Find("CraftingCross");
        StatsUI = GameObject.Find("Canvas").transform.Find("Stats").gameObject;

        if(GameObject.Find("VaultUI")!=null)
        VaultUI = GameObject.Find("VaultUI").GetComponent<ItemsSlotsUI>();

        LogUI = GameObject.Find("Canvas").transform.Find("Log").gameObject;

        CurrentItemID = -1;
        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        _menu = _constr.GetComponent<MenuCustom>();

        QuestMenu = GameObject.Find("QuestMenu");
        NewQuest = GameObject.Find("NewQuest");


        QD = GetComponent<QuestDatabase>();

        EnterTexture = Resources.Load<Texture2D>("Sprites/UI/EButton");
        EnterTexture_J = Resources.Load<Texture2D>("Sprites/UI/EButton_J");

        OpenInventory = Resources.Load<AudioClip>("Sound/UI/Inventory_Open");
        OpenCardsInv = Resources.Load<AudioClip>("Sound/UI/CardDeck_Open");
        PickItem = Resources.Load<AudioClip>("Sound/Items/PickItem");

        UIOpen = Resources.Load<AudioClip>("Sound/UI/UI_Open");

        ClickClip = Resources.Load<AudioClip>("Sound/UI/Click_0");
        ShakeClip = Resources.Load<AudioClip>("Sound/UI/UI_Shake");
        TakeItemClip = Resources.Load<AudioClip>("Sound/UI/Accept");

        Controlls = GameObject.Find("Controlls");

        FadeAlpha = 1;
        masterfloat = -40;

        aumixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/NewAudioMixer");
        aumixer.SetFloat("Master", 0);

    
        SellTag = new string[2] { "Sell", "Продать" };
        DescWidth = new float[2];

        ChoiseTexture = Resources.Load<Texture2D>("Sprites/UI/Choose");
        MoneyTexture = Resources.Load<Texture2D>("Sprites/UI/Money");
        PhoneMessagePlayer = Resources.Load<Texture2D>("Sprites/UI/Noun");
        PhoneMessageLover = Resources.Load<Texture2D>("Sprites/UI/Adjective");


        skin = Resources.Load<GUISkin>("Prefabs/New GUISkin");
        pl = GetComponent<Player>();
        XChoise = 0;
        YChoise = 0;

        if (GameObject.Find("CraftingUI") == null)
        {
            CraftingUIOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/CraftingUI"), GameObject.Find("Canvas").transform);
            CraftingUIOB.name = "CraftingUI";
            CraftingUIOB = GameObject.Find("CraftingUI");

        }
        else CraftingUIOB = GameObject.Find("CraftingUI");



        CraftingUIOB.transform.SetSiblingIndex(8);

        if (GameObject.Find("InventoryUI") == null)
        {
            GameObject INV = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/InventoryUI"), GameObject.Find("Canvas").transform);
            INV.name = "InventoryUI";
            INV = GameObject.Find("InventoryUI");

        }
        else InventoryUIOB = GameObject.Find("InventoryUI");





        FolderButtons.Add(InventoryUIOB.transform.Find("BuildingsFolderButton").gameObject);
        FolderButtons.Add(InventoryUIOB.transform.Find("GrassFolderButton").gameObject);
        FolderButtons.Add(InventoryUIOB.transform.Find("StoneFolderButton").gameObject);


        LeftFolder = InventoryUIOB.transform.Find("LeftFolder").gameObject;
        RightFolder = InventoryUIOB.transform.Find("RightFolder").gameObject;



        ToolTip = InventoryUIOB.transform.Find("ToolTip").gameObject;

        if (GameObject.Find("ItemDatabase") != null)
            database = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        else
        {
            gameObject.AddComponent<ItemDatabase>();
            database = GetComponent<ItemDatabase>();
        }

        LeftArrow = InventoryUIOB.transform.Find("LeftArrow").gameObject;
        RightArrow = InventoryUIOB.transform.Find("RightArrow").gameObject;

    
        
        int craftingslotNUM = craftingslotX * craftingslotY;

     

        Choose = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/ChooseUI"), InventoryUIOB.transform);
        Choose.name = "InvChoose";


        IM = GetComponent<InputMode>();


        RightArrow.SetActive(false);
        LeftArrow.SetActive(false);
   

        ONOFF(NewQuest, false);
        ONOFF(QuestMenu, false);
     
        ONOFF(EscapeInventory, false);
        NewQuestBool = false;
        LoadSlots();


       
    }


     public  void LoadSlots()
    {
        slotX = database.items.Count + 20;
        SlotsRect = new Rect[database.items.Count + 1000];

        slotY = 1;
    
      
        int s = 0;

        for (int x = 0; x < 200; x++)
        {



            GameObject Slot = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Slot"), InventoryUIOB.transform);
            Slot.GetComponent<RectTransform>().position = new Vector2(SlotsRect[s].x, SlotsRect[s].y);
            Slot.name = "Slot" + x;
            slots.Add(Slot);

            print("slots x " + x);

            s++;
        }
   

        DrawInventory(false);
        SetSlots();

        Choose.transform.SetAsLastSibling();

       
    }



    private void Update()
    {
        slotX = database.items.Count + 20;

        if (BlueprintMenu != null) blueprintshow = BlueprintMenu.GetComponent<BlueprintMenu>().showbp;

     
        if (!showinvent && !showjournal && !blueprintshow)
        {
            crafting = false;

            if (pl.Chatting)
            {

                ONOFF(Controlls, false);
                ONOFF(NewQuest, false);
                DrawInvNo = false;
            }
            else
            {
                if (!DrawInvNo)
                {
                    ONOFF(Controlls, true);
                    ONOFF(NewQuest, NewQuestBool);
                    DrawInvNo = true;
                }

            }
        }

        if (pl.IM._vertical > 0.5f || pl.IM._vertical < -0.5f)
        {


            if (pl.inv.CurrentItem < 0)
                pl.inv.CurrentItem = 0;
        }




        if (Quest_YSlider != 0) Quest_YPos = 0;


        Crafting();
        Journal();

        ExitFromInventory();



        if (pl.GetComponent<Achivements>()!=null) ShowAch = pl.GetComponent<Achivements>().ShowAch;

        if(!blueprintshow)
        Start_Close_Inventory();
        
        ONOFF_Inventory();

        SelectFolder();

        SetItemsIntoSlots();

        ChoiseSlotsWithMouse();

        ScrollThroughInventory();
        
        ChooseUIAndTooltipPositions();

        
    }

  
    public void StartInventory()
    {
        if (showinvent) return;

        showinvent = true;
       
        CurrentItem = 0;

        Choose.transform.position = slots[CurrentItem].transform.position;
        PauseInventory = false;
        IM.ActionDelay = Time.fixedTime + 0.2f;
    }


    void Start_Close_Inventory()
    {
        if (!pl.StartLoading && ((_menu.UIColl(InventoryButton) && pl.IM.LeftMouseButtonDown && InventoryButton.GetComponent<Image>().enabled) || pl.IM.inventory_b) && IM.ActionDelay < Time.fixedTime && !showjournal && !pl.menu.MenuONOFF && !pl.Chatting && !ShowAch && !_constr.TutorialPause)
        {
            showinvent = !showinvent;
            
          

            if (!showinvent)
            {
                PlaySoundsPitched(UIOpen,0.8f);
                _constr.DeActivateBuilding();
                PauseInventory = false;
                crafting = false;

                CurrentItem = 0;
                SlotSlide = 0;

                SetSlots();
                //we need to remove buffer item here

                //if()
            }

            _constr.ChooseMouseObject = false;

            if (showinvent && !crafting )
            {
                
                PlaySoundsPitched(UIOpen, 1);
                CurrentItem = 0;
              
                    Choose.transform.position = slots[CurrentItem].transform.position;
                    PauseInventory = false;
                UpdateInvFolder();
            }

            IM.ActionDelay = Time.fixedTime + 0.2f;
        }
    }
    void ONOFF_Inventory()
    {
        if (showinvent)
        {
            if (!DrawINV)
            {

                _constr.DeActivateBuildingNOINV();

           
                ONOFF(GameObject.Find("ButtonsUI"), false);

               
                
                ONOFF(Controlls, false);
                DrawInventory(true);

               // if (_constr != null)
                  //  _constr.UnsetBigTips();



                DrawINV = true;
            }
        }
        else
        {
            if (DrawINV)
            {


                LeftArrow.SetActive(false);
                RightArrow.SetActive(false);

                ONOFF(GameObject.Find("ButtonsUI"), true);
                
                ONOFF(Controlls, true);
                DrawInventory(false);
                DrawINV = false;
            }
        }
    }

    void ScrollThroughInventory()
    {
        if (!showinvent ) return;
        
        if ((pl.IM._horizontal > 0.5f || pl.IM.DPADX > 0)  && HorDelay < Time.fixedTime && IM.ActionDelay < Time.fixedTime)
        {
            if (CurrentItem < inventoryFolder.Count - 1)
            {
                if (!PauseInventory) StopShake();

                CurrentItem++;

                pl.PlaySoundsPitched(ClickClip, 1);



                if (CurrentItem > 12 )
                {
                    SlotSlide++;



                    SetSlots();
                }
            }
            else if(!PauseInventory)Shake(new Vector3(4, 0, 0));

            HorDelay = Time.fixedTime + 0.1f;
        }

        if ((pl.IM._horizontal < -0.5f || pl.IM.DPADX < 0)  && HorDelay < Time.fixedTime)
        {
            if (CurrentItem > 0)
            {
                if (!PauseInventory) StopShake();
                CurrentItem--;

                pl.PlaySoundsPitched(ClickClip, 0.8f);

                if (SlotSlide > 0)
                {
                    SlotSlide--;


                    SetSlots();
                }
            }
            else if (!PauseInventory) Shake(new Vector3(4, 0, 0));


            HorDelay = Time.fixedTime + 0.1f;
        }

        if (PauseInventory)
        {
            LeftArrow.SetActive(false);
            RightArrow.SetActive(false);
            return;
        }


        if (IM.MouseMode) 
        {
            LeftArrow.SetActive(true);
            RightArrow.SetActive(true);
            return;
        }




        if (SlotSlide < 1)
            LeftArrow.SetActive(false);
        else LeftArrow.SetActive(true);

        if (CurrentItem > 12)
            RightArrow.SetActive(true);
        else RightArrow.SetActive(false);
        


    }

    void SelectFolder()
    {


      

        for (int i = 0; i < FolderButtons.Count; i++)
        {
            if (pl.MouseOB.GetComponent<CollList>().coll_obj.Contains(FolderButtons[i]) && (pl.IM.LeftMouseButtonDown || pl.IM.enter_b))
            {
                pl.PlaySoundsPitched(ClickClip, 1);
                FolderButtons[i].transform.Find("NewItemTag").gameObject.SetActive(false);
                CurrentFolder = i;
                CurrentItem = 0;
                PauseInventory = false;
                ChooseTopSegmentSlot = false;
                UpdateInvFolder();
            }
        }


        if (!showinvent ) return;

        if ((pl.IM.LeftTrigger || ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.MouseOB.GetComponent<CollList>().coll_obj.Contains(LeftFolder))) && IM.ActionDelay < Time.fixedTime && CurrentFolder > 0)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f + CurrentFolder * 0.05f);
            CurrentFolder--;
            FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);
            CurrentItem = 0;
            SlotSlide = 0;
            UpdateInvFolder();
            IM.ActionDelay = 0.1f;
        }

        if ((pl.IM.RightTrigger || ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.MouseOB.GetComponent<CollList>().coll_obj.Contains(RightFolder))) && IM.ActionDelay < Time.fixedTime && CurrentFolder < FolderButtons.Count - 1)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f + CurrentFolder * 0.05f);
            CurrentFolder++;
            FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);
            CurrentItem = 0;
            SlotSlide = 0;
            UpdateInvFolder();
            IM.ActionDelay = 0.1f;
        }


        if (inventoryFolder.Count<=0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Structure && inventory[i]._StructureType == Item.StructureType.Building||
                    inventory[i].Structure && inventory[i]._StructureType == Item.StructureType.Protection||
                   inventory[i].Structure && inventory[i]._StructureType == Item.StructureType.Decoration)
                {
                    inventoryFolder.Add(DeepCopyItem(inventory[i].itemID, inventory[i].Count, 999));
                    print("SelectFolder " + i);
                }

            }
            CurrentItem = 0;
        }


        
    }


    void UpdateFolderNumber(int ID)
    {
        if (!pl.inv.GetItemInDatabase(ID).Structure) return;

        if (crafting)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (pl.inv.GetItemInDatabase(ID)._StructureType == Item.StructureType.Building||
                    pl.inv.GetItemInDatabase(ID)._StructureType == Item.StructureType.Protection||
                       pl.inv.GetItemInDatabase(ID)._StructureType == Item.StructureType.Decoration)
                    CurrentFolder = 0;

                if (pl.inv.GetItemInDatabase(ID)._StructureType == Item.StructureType.Tiles)
                    CurrentFolder = 1;

                if (pl.inv.GetItemInDatabase(ID)._StructureType == Item.StructureType.Farms)
                    CurrentFolder = 2;
            }
        }

        UpdateInvFolder();

    }

    public void UpdateInvFolder()
    {
        inventoryFolder = new List<Item>();

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Structure)
            {
                if (CurrentFolder == 0 && 
                    (inventory[i]._StructureType == Item.StructureType.Building ||
                         inventory[i]._StructureType == Item.StructureType.Protection ||
                        inventory[i]._StructureType == Item.StructureType.Decoration))
                 inventoryFolder.Add(DeepCopyItem(inventory[i].itemID, inventory[i].Count, 999));
                
                if (CurrentFolder == 1 && inventory[i]._StructureType == Item.StructureType.Tiles)
                inventoryFolder.Add(DeepCopyItem(inventory[i].itemID, inventory[i].Count, 999));
                
                if (CurrentFolder == 2 && inventory[i]._StructureType == Item.StructureType.Farms)
                 inventoryFolder.Add(DeepCopyItem(inventory[i].itemID, inventory[i].Count, 999));
                
            }
        }

        for (int i = 0; i < FolderButtons.Count; i++)
        {
            if (i != CurrentFolder)
            {
                FolderButtons[i].transform.Find("BG").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                FolderButtons[i].transform.Find("Icon").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                FolderButtons[CurrentFolder].transform.Find("BG").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                FolderButtons[CurrentFolder].transform.Find("Icon").GetComponent<Image>().color = new Color(1, 1, 1, 1);
             }
        }

    }



    void SetItemsIntoSlots()
    {


        for (int x = 0; x < slots.Count; x++)
        {


            if (x <= inventoryFolder.Count - 1)
            {
                if (inventoryFolder[x] != null)
                {
                    if (inventoryFolder[x].itemID > -1)
                    {
                        if (slots[x].transform.Find("Item") == null)
                        {
                            GameObject ItemOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Item"), slots[x].transform);
                            ItemOB.GetComponent<RectTransform>().position = slots[x].GetComponent<RectTransform>().position;

                            ItemOB.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inventoryFolder[x].itemNames[0]);
                            ItemOB.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Image IMG = ItemOB.transform.Find("Status").GetComponent<Image>();

                            SetStatus(inventoryFolder[x], ref IMG);




                            ItemOB.name = "Item";

                            if (inventoryFolder[x].CanStack)
                                slots[x].transform.Find("Item").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + inventoryFolder[x].Count;

                        }
                        else
                        {
                            Image IMG = slots[x].transform.Find("Item").transform.Find("Status").GetComponent<Image>();

                            SetStatus(inventoryFolder[x], ref IMG);

                            slots[x].transform.Find("Item").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inventoryFolder[x].itemNames[0]);
                            slots[x].transform.Find("Item").transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            if (inventoryFolder[x].Count > 0 && inventoryFolder[x].CanStack)
                                slots[x].transform.Find("Item").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + inventoryFolder[x].Count;
                            else slots[x].transform.Find("Item").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";

                        }
                    }


                    if (inventoryFolder[x].itemID == -1 && slots[x].transform.Find("Item") != null)
                    {
                        Destroy(slots[x].transform.Find("Item").gameObject);
                    }
                }
            }
            else if (slots[x].transform.Find("Item") != null)
                Destroy(slots[x].transform.Find("Item").gameObject);




        }
    }


    void ExitFromInventory()
    {
        if (!showinvent && !showjournal) return;
        
        if ((pl.IM.menu_b || pl.IM.exit_b ) && !pl.menu.MenuONOFF)
        {

            PlaySoundsPitched(UIOpen, 0.8f);

            LeftArrow.SetActive(false);
            RightArrow.SetActive(false);

            ONOFF(GameObject.Find("ButtonsUI"), true);
            ONOFF(Controlls, true);
            if (showjournal) DrawJournal(false);
            if (showinvent) DrawInventory(false);
            crafting = false;
            showinvent = false;
            showjournal = false;
            DrawINV = false;
            IM.ActionDelay = Time.fixedTime + 0.1f;
            _menu.MenuActionDelay = Time.fixedTime + 0.1f;
        }

        
    }

    private void OnGUI()
    {
        ShowPicketItems();
    }


    void DrawInventory(bool tf)
    {
      
#if UNITY_SWITCH

        for (int i = 0; i < FolderButtons.Count; i++)
        {
        FolderButtons[i].SetActive(tf);
        FolderButtons[i].GetComponent<RectTransform>().position = new Vector2(FolderButtons[i].GetComponent<RectTransform>().position.x, SlotsRect[i].y + SlotsRect[i].height/1.2f);
        LeftFolder.GetComponent<RectTransform>().position = new Vector2(LeftFolder.GetComponent<RectTransform>().position.x, SlotsRect[i].y + SlotsRect[i].height/ 1.2f);
        RightFolder.GetComponent<RectTransform>().position = new Vector2(RightFolder.GetComponent<RectTransform>().position.x, SlotsRect[i].y + SlotsRect[i].height/ 1.2f);
        }

#endif

#if UNITY_STANDALONE || UNITY_PS4 || UNITY_PS5
        for (int i = 0; i < FolderButtons.Count; i++)
         FolderButtons[i].SetActive(tf);
#endif



        for (int x = 0; x < slots.Count; x++)
        {
            slots[x].SetActive(tf);
        }
        
        Choose.SetActive(tf);

        if (!crafting && tf)
            ONOFF(EscapeInventory, true);

        if(!tf)
        ONOFF(EscapeInventory, false);


        ONOFF(LeftFolder, tf);
        ONOFF(RightFolder, tf);
        
        print("InventoryButton " +tf);
        ONOFF(InventoryButton, !tf);
    }




    public bool CheckItem(int id)
    {
        bool result = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].itemID == id)
                {
                    result = true;
                    break;
                }
                else result = false;
            }
        }
        return result;
    }

    public Item GetItem(int id)
    {
        Item result = null;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                result = inventory[i];
                break;
            }


        }

  


        return result;
    }


    public Item GetItemInDatabase(int id)
    {
        Item result = null;

        for (int i = 0; i < database.items.Count; i++)
        {
            if (database.items[i].itemID == id)
            {
                result = database.items[i];
                return result;
            }


        }

      
        return result;
    }



    public bool CheckEmpty(int ID)
    {
        bool result = false;

        if(inventory.Count < slots.Count) result = true;
        

        return result;

    }



    public void AddItem(int id, int numplus, int durability, Vector2 NamePos)
    {
        if (GetItemInDatabase(id) == null)
            return;

        AddItemToInv(id, numplus, durability, NamePos);



        pl.PlaySoundsPitched(PickItem, 1);

        ADDPickedName("+ " + GetItemInDatabase(id).itemNames[_menu.Language], 0, 0.25f, NamePos);


    }


    public void AddItemNOAUDIO(int id, int numplus, int durability, Vector2 NamePos)
    {
        if (GetItemInDatabase(id) == null)
            return;

        AddItemToInv(id, numplus, durability, NamePos);




        ADDPickedName("+ " + GetItemInDatabase(id).itemNames[_menu.Language], 0, 0.25f, NamePos);


    }
    public void AddItemNOAUDIO_NOPickedNames(int id, int numplus, int durability, Vector2 NamePos)
    {
        if (GetItemInDatabase(id) == null)
            return;

        AddItemToInv(id, numplus, durability, NamePos);

    }

    void AddItemToInv(int id, int numplus, int durability, Vector2 NamePos)
    {
        LastAddedItem = id;
        UpdateFolderNumber(id);
        
        if ((CheckItem(id) && !GetItemInDatabase(id).CanStack) || !CheckItem(id))
        {


           
                inventory.Add(new Item());
                int i = inventory.Count - 1;
                
                // CurrentItem = i;
                inventory[i] = DeepCopyItem(id, numplus, durability);

                if (CurrentFolder != 0)
                {
                    if (inventory[i].Structure && (inventory[i]._StructureType == Item.StructureType.Building||
                         inventory[i]._StructureType == Item.StructureType.Protection ||
                         inventory[i]._StructureType == Item.StructureType.Decoration))
                        FolderButtons[0].transform.Find("NewItemTag").gameObject.SetActive(true);
                }

                if (CurrentFolder != 1)
                {
                    if (inventory[i].Structure && inventory[i]._StructureType == Item.StructureType.Tiles)
                        FolderButtons[1].transform.Find("NewItemTag").gameObject.SetActive(true);
                }

                if (CurrentFolder != 2)
                {
                    if (inventory[i].Structure && inventory[i]._StructureType == Item.StructureType.Farms)
                        FolderButtons[2].transform.Find("NewItemTag").gameObject.SetActive(true);
                }

                UpdateInvFolder();
                    
                    
            

            return;

        }



        if (CheckItem(id) && GetItemInDatabase(id).CanStack)
        { 
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemID == id)
                {
                    inventory[i].Count += numplus;
                    UpdateInvFolder();
                    break;
                }
                
            }
                
        }



    }


    public void ADDPickedName(string text,int stylenumber,float speed, Vector2 Pos)
    {
        PickedX.Add(Pos.x);
        PickedY.Add(Pos.y);

        PickedSlide.Add(1);
        PickedStyles.Add(stylenumber);
        Picked.Add(text);

        PickedSpeeds.Add(speed);
    }


    void ShowPicketItems()
    {
        for (int y = 0; y < PickedY.Count; y++)
        {


            Color color = GUI.color;
            GUI.color = new Color(1, 1, 1, PickedSlide[y]);
          
            GUI.Box(new Rect(pl.MainCamera.WorldToScreenPoint(new Vector3(PickedX[y],0,0)).x - 200f, Screen.height - Camera.main.WorldToScreenPoint(new Vector3(0,PickedY[y], 0)).y + PickedSlide[y] * 100 - 150, 400, 50), Picked[y], skin.customStyles[PickedStyles[y]]);
            PickedSlide[y] -= Time.deltaTime * PickedSpeeds[y];

            if (PickedSlide[y] <= 0)
            {
                Picked.RemoveAt(y);
                PickedY.RemoveAt(y);
                PickedX.RemoveAt(y);
                PickedStyles.RemoveAt(y);
                PickedSlide.RemoveAt(y);
                PickedSpeeds.RemoveAt(y);
            }

            GUI.color = color;
        }
    }


    /*public void ReduceItemCount(string namem,int minusn)
	{
		for (int i = 0; i<inventory.Count; i++) {
			if (inventory [i].itemNames[0] == namem){
				inventory [i] = new Item ();
			}
		}
	}*/

    public void ReduceItemCount(int id, int minusn)
    {
        print("ReduceItemCount " + id);

        for (int i = 0; i < inventory.Count; i++)
        {


            if (inventory[i].itemID == id)
            {

                if (inventory[i].Count > minusn)
                {
                    inventory[i].Count -= minusn;
                    
                }
                else
                {
                    
                    inventory[i] = new Item();

                    for (int j = 0; j < inventoryFolder.Count; j++)
                    {
                        if (inventoryFolder[j].itemID == id)
                        {
                            if (slots[j].transform.Find("Item" + j) != null)
                                Destroy(slots[j].transform.Find("Item" + j).gameObject);
                        }
                    }


                }

                break;
            }
        }

       


    }

    public void RemoveCurrentSlot(int count)
    {
        if (inventory[CurrentItem].Count <= count)
        {
            inventory[CurrentItem] = new Item();

            if (slots[CurrentItem].transform.Find("Item" + CurrentItem) != null)
                Destroy(slots[CurrentItem].transform.Find("Item" + CurrentItem).gameObject);

        }
        else inventory[CurrentItem].Count-= count;

    }



    

    public void SaveInvNULL()
    {

        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i] = new Item();
        }
        //print("Saved Inv");
    }


   


    public int GetInvCount()
    {
        return inventory.Count;
    }

    public int GetAllItemsCount()
    {
        return database.items.Count;
    }

    public Item GetCurrentItem()
    {
        if (CurrentItem > -1 && CurrentItem< inventoryFolder.Count)
            return inventoryFolder[CurrentItem];
        else return new Item();
    }



    public string GetCurrentItemName()
    {
        //print ("slotID"+slots[Ch_pos].itemID);
        if ((XChoise + YChoise * slotX) < inventory.Count)
            return inventory[XChoise + YChoise * slotX].itemNames[0];
        else return null;
    }




    void DrawJournal(bool TF)
    {


        //Quest_YSlider = QuestMenu.transform.Find("Scrollbar").GetComponent<Scrollbar>().value;
        print("Quest_YSlider " + Quest_YSlider);
        ONOFF(QuestMenu, TF);

        for (int i = 0; i < Quests.Count; i++)
        {

            if (QuestMenu.transform.Find("Quest" + i) != null)
            {
                // QuestMenu.transform.Find("Quest" + i).gameObject.SetActive(TF);
                QuestMenu.transform.Find("Quest" + i).Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = Quests[i].Description[0];
                // QuestMenu.transform.Find("Quest" + i).transform.position = new Vector3(QuestMenu.transform.position.x, QuestMenu.transform.position.y + (i * -140f) - 10f + Quest_YPos + Quest_YSlider, 0);

                if (Quests[i].Done)
                {
                    QuestMenu.transform.Find("Quest" + i).Find("QuestMark").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/QuestDone");
                }

            }
            else
            {

                if (Quests[i].Started)
                {


                    GameObject QuestOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/QuestPart"), QuestMenu.transform);
                    QuestOB.transform.position = new Vector3(QuestMenu.transform.position.x, QuestMenu.transform.position.y + (i * -140f) - 10f, 0);
                    QuestOB.name = "Quest" + i;
                    QuestOB.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = Quests[i].Description[0];
                }


                if (Quests[i].Done)
                {
                    QuestMenu.transform.Find("Quest" + i).Find("QuestMark").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/QuestDone");
                }
            }
        }





    }



    public void AddMoney(int max)
    {
        AddedMoney = 0;
        MaxAddedMoney = max;
    }


    void AddMoneyAnim()
    {
        if (MaxAddedMoney > 0)
        {
            if (AddMoneyTimer < Time.fixedTime)
            {
                Money++;
                AddedMoney++;
                MaxAddedMoney--;
                AddMoneyTimer = Time.fixedTime + 0.01f;
            }
        }
    }


    public void ONOFF(GameObject g, bool TF)
    {
        if (g == null) return;
        if (g.transform.parent == _constr.transform) return;

        TurnComponentsONOFF(g, TF);
        ToggleThroughChild(g.transform, TF);
        

    }

    void ToggleThroughChild(Transform parent, bool TF)
    {

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform onoffchild = parent.GetChild(i);
            TurnComponentsONOFF(onoffchild.gameObject, TF);
            ToggleThroughChild(onoffchild, TF);
        }

    }


    void TurnComponentsONOFF(GameObject g, bool TF)
    {
        if (g.GetComponent<DrawIfActive>() != null) return;


        if (g.GetComponent<Image>() != null)
            g.GetComponent<Image>().enabled = TF;

        if (g.GetComponent<Tilemap>() != null)
            g.GetComponent<Tilemap>().enabled = TF;

        if (g.GetComponent<TilemapRenderer>() != null)
            g.GetComponent<TilemapRenderer>().enabled = TF;


        if (g.GetComponent<TextMeshProUGUI>() != null)
            g.GetComponent<TextMeshProUGUI>().enabled = TF;

        if (g.GetComponent<SpriteRenderer>() != null)
            g.GetComponent<SpriteRenderer>().enabled = TF;


        if (g.GetComponent<BoxCollider2D>() != null)
            g.GetComponent<BoxCollider2D>().enabled = TF;

        if (g.GetComponent<Character>() != null)
            g.GetComponent<Character>().enabled = TF;

       if (g.GetComponent<CharacterMove>() != null)
          g.GetComponent<CharacterMove>().enabled = TF;
    }


    public void AddQuestNoNew(int QID)
    {

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == QID && !QD.QuestsEN[i].Started && !Quests.Contains(QD.QuestsEN[i]))
            {
                Quests.Add(QD.QuestsEN[i]);
                Quests[Quests.Count - 1].Started = true;
                QD.QuestsEN[i].Started = true;
            }
        }

    }

    public void AddQuest(string QName)
    {

        ONOFF(NewQuest, true);


        NewQuestBool = true;
        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].NAME == QName && !QD.QuestsEN[i].Started)
            {
                Quests.Add(QD.QuestsEN[i]);
                Quests[Quests.Count - 1].Started = true;
                QD.QuestsEN[i].Started = true;
            }
        }

    }

  
    void Crafting()
    {
        if (crafting && !CraftingDraw)
        {
            ONOFF(EscapeInventory, false);

            print("Crafting start");
            CraftingUIOB.GetComponent<ItemsSlotsUI>().StartUI();
    

            if(VaultUI!=null)
            VaultUI.CloseUI();

           // ONOFF(StatsUI, false);
            ONOFF(LogUI, false);
            CraftingDraw = true;
        }

        if (!crafting && CraftingDraw)
        {
            if(!showinvent)
                ONOFF(EscapeInventory, false);

            CraftingUIOB.GetComponent<ItemsSlotsUI>().CloseUI();
           // ONOFF(StatsUI, true);
            ONOFF(LogUI, true);

            CraftingDraw = false;
        }

        

    }



    public string ToolpitString(Item i)
    {

        //#FF5D5D - red
        //#9DFF99 - green

        string red = "#FF5D5D";
        string green = "#9DFF99";
        string yellow = "#FFF224";

        string s = "";
        string plus = "";

        string stargcolortag = "";
        string endgcolortag = "";

        stargcolortag = "<color=" + yellow + ">";
        endgcolortag = "</color>";

        s = stargcolortag + i.itemNames[_menu.Language] + endgcolortag + "\n" + "\n" + i.itemDesc[_menu.Language] + "\n";


        if (_menu.Language == 0) LockedString = "LOCKED";
        if (_menu.Language == 1) LockedString = "НЕ ВІДКРИТО";
        if (_menu.Language == 2) LockedString = "鍵付き";

        if (_menu.Language == 0) BodyPartString = "Body part: ";
        if (_menu.Language == 1) BodyPartString = "Частина тіла: ";
        if (_menu.Language == 2) BodyPartString = "身体の一部: ";

        if (_menu.Language == 0)
            BodypartsNames = new string[6] { "Head", "Body", "Legs", "Hand", "Eye", "Mutation" };

        if (_menu.Language == 1)
            BodypartsNames = new string[6] { "Голова", "Тіло", "Ноги", "Руки", "Око", "Мутація" };

        if (_menu.Language == 2)
            BodypartsNames = new string[6] { "Head", "Body", "Legs", "Hand", "Eye", "Mutation" };

        if (_menu.Language == 0) DamageString = "Damage: ";
        if (_menu.Language == 1) DamageString = "Пошкодження: ";
        if (_menu.Language == 2) DamageString = "ダメージ: ";


        if (_menu.Language == 0) DurabilityString = "Durability: ";
        if (_menu.Language == 1) DurabilityString = "Міцність: ";
        if (_menu.Language == 2) DurabilityString = "耐久性: ";


        if (_menu.Language == 0) BulletDamageString = "Bullet Damage: ";
        if (_menu.Language == 1) BulletDamageString = "Кульове пошкодження: ";
        if (_menu.Language == 2) BulletDamageString = "弾丸のダメージ: ";

        if (_menu.Language == 0) MaxHPString = "Max HP: ";
        if (_menu.Language == 1) MaxHPString = "Максимальний HP: ";
        if (_menu.Language == 2) MaxHPString = "最大HP: ";


        if (_menu.Language == 0) VisionString = "Vision: ";
        if (_menu.Language == 1) VisionString = "Зір: ";
        if (_menu.Language == 2) VisionString = "ビジョン: ";

        if (_menu.Language == 0) StaminaString = "Stamina: ";
        if (_menu.Language == 1) StaminaString = "Витривалість: ";
        if (_menu.Language == 2) StaminaString = "耐久: ";
        


        if (_menu.Language == 0) SatietyString = "Satiety: ";
        if (_menu.Language == 1) SatietyString = "Cитість: ";
        if (_menu.Language == 2) SatietyString = "満腹感: ";

        if (_menu.Language == 0) HPString = "HP: ";
        if (_menu.Language == 1) HPString = "Здоров'я: ";
        if (_menu.Language == 2) HPString = "健康: ";

        if (_menu.Language == 0) StaminaRecoveryspeedString = "Stamina Recovery speed: ";
        if (_menu.Language == 1) StaminaRecoveryspeedString = "Швидкість відновлення витривалості: ";
        if (_menu.Language == 2) StaminaRecoveryspeedString = "スタミナ回復速度: ";


        if (_menu.Language == 0) NeedsStaminaString = "Needs Stamina: ";
        if (_menu.Language == 1) NeedsStaminaString = "Потребує Витривалості: ";
        if (_menu.Language == 2) NeedsStaminaString = "体力が必要: ";

        if (_menu.Language == 0) DashDurationString = "Dash Duration: ";
        if (_menu.Language == 1) DashDurationString = "Тривалість Деша: ";
        if (_menu.Language == 2) DashDurationString = "ダッシュ時間: ";

        if (_menu.Language == 0) FoodString = "Food";
        if (_menu.Language == 1) FoodString = "Їжа";
        if (_menu.Language == 2) FoodString = "食品";



        if (_menu.Language == 0) CostString = "Cost";
        if (_menu.Language == 1) CostString = "Ціна";
        if (_menu.Language == 2) CostString = "価格だ";



        if (_menu.Language == 0) BuildingCostString = "Cost to build";
        if (_menu.Language == 1) BuildingCostString = "Ціна будування";
        if (_menu.Language == 2) BuildingCostString = "建設費";

        


        if (pl.inv.GetItemInDatabase(i.itemID).Locked)
        {
            s += "<color=" + red + ">" + LockedString + "</color>";
            s += "\n";
        }

        if (i._bodypart != null)
        {
            if (i._bodypart.Length > 0)
            {
                if (i.Vision > 0) plus = "+ ";
                s += BodyPartString;

                for (int j = 0; j < i._bodypart.Length; j++)
                {
                    s += "<color=" + yellow + ">" + BodypartsNames[j] + "</color>";

                    if (j < i._bodypart.Length - 1) s += ", ";

                }

                s += "\n";
            }
        }


        if (i.DamageAmount != 0)
        {
         
            if (i.DamageAmount > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.DamageAmount < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }


            s += DamageString + stargcolortag + plus + i.DamageAmount + endgcolortag;
            s += "\n";
        }


        if (i._type == Item.type.weapon)
        {
           
            if (i.Durability > 1)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";

            }
            else if (i.Durability <= 1)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            s += DurabilityString + stargcolortag + i.Durability + endgcolortag;
            s += "\n";
        }

        if (i.BulletDamageAmount != 0)
        {
            
            if (i.BulletDamageAmount > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.BulletDamageAmount < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            s += BulletDamageString + stargcolortag + plus + i.BulletDamageAmount + endgcolortag;
            s += "\n";
        }


        if (i.MaxHP != 0)
        {
            
            if (i.MaxHP > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.MaxHP < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            if (i.MaxHP > 0) plus = "+ ";
            s += MaxHPString + stargcolortag + plus + i.MaxHP + endgcolortag;
            s += "\n";
        }


        if (i.Vision != 0)
        {
            
            if (i.Vision > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.Vision < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            s += VisionString + stargcolortag + plus + i.Vision + endgcolortag;
            s += "\n";
        }

        if (i.Stamina != 0)
        {
       
            if (i.Stamina > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.Stamina < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            s += StaminaString + stargcolortag + plus + i.Stamina + endgcolortag;
            s += "\n";
        }

        if (i.Satiety != 0)
        {
          
            if (i.Satiety > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.Satiety < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            s += SatietyString + stargcolortag + plus + i.Satiety + endgcolortag;
            s += "\n";
        }

        if (i.HP != 0)
        {
        
            if (i.HP > 0)
            {
                stargcolortag = "<color=" + green + ">";
                endgcolortag = "</color>";
                plus = "+ ";
            }
            else if (i.HP < 0)
            {
                stargcolortag = "<color=" + red + ">";
                endgcolortag = "</color>";
            }

            s += HPString + stargcolortag + plus + i.HP + endgcolortag;
            s += "\n";
        }


        if (crafting && ChooseTopSegmentSlot)
        {
            stargcolortag = "<color=" + yellow + ">";
            endgcolortag = "</color>";


            s += "\n" + CostString + stargcolortag + " " + i.Cost + endgcolortag;
        
        }


        stargcolortag = "<color=" + yellow + ">";
        endgcolortag = "</color>";
            

        s += "\n"+ BuildingCostString + stargcolortag + " " + i.BuildingCost + endgcolortag;
        s += "\n";
        

        if (i.StaminaRecoverySpeed != 0)
        {
            if (i.StaminaRecoverySpeed > 0) plus = "+ ";
            s += StaminaRecoveryspeedString + plus + i.StaminaRecoverySpeed;
            s += "\n";
        }

        if (i.StaminaUse != 0)
        {
            if (i.StaminaUse > 0) plus = "";
            s += NeedsStaminaString + plus + i.StaminaUse;
            s += "\n";
        }

        if (i.DashDuration != 0)
        {
            if (i.DashDuration > 0) plus = "+ ";
            s += DashDurationString + plus + i.DashDuration;
            s += "\n";
        }

        if (i.Food) s += FoodString;



        return s;
    }




    void ChoiseSlotsWithMouse()
    {
       // if (IM.MouseMode) return;

        if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(CraftingCross) && pl.IM.LeftMouseButtonDown)
        {
          //  ONOFF(StatsUI, true);
            ONOFF(GameObject.Find("ButtonsUI"), true);
            ONOFF(Controlls, true);
            if (showjournal) DrawJournal(false);
            if (showinvent) DrawInventory(false);
            crafting = false;

           
            showinvent = false;
            showjournal = false;
            DrawINV = false;

            IM.ActionDelay = Time.fixedTime + 0.1f;
            pl.menu.PlayAudio(ClickClip);
        }



        if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(EscapeInventory) && pl.IM.LeftMouseButtonDown)
        {
            pl.menu.PlayAudio(ClickClip);
            showinvent = false;
 
   
            IM.ActionDelay = Time.fixedTime + 0.2f;
        }



        if (crafting )
        {
          
            SetCurrentCraftingItem();
        }



        for (int i = 0; i < slots.Count; i++)
        {
            CollidingOneOfTheSlots(i);

        }


        if (!MouseCollideWithSlots() && IM.MouseMode && !PauseInventory)
        {
            CurrentItem = -1;

        }



        if (!crafting)
        {
            if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(LeftArrow) && pl.IM.LeftMouseButtonDown)
            {
                PauseInventory = false;
                ChooseTopSegmentSlot = false;


                if(SlotSlide > 0)
                SlotSlide--;

                if (CurrentItem > 0)
                    CurrentItem--;

                SetSlots();

                pl.PlaySoundsPitched(ClickClip, 0.8f);

            }

            if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(RightArrow) && pl.IM.LeftMouseButtonDown)
            {
                if(SlotSlide < inventoryFolder.Count - 5)
                SlotSlide++;

                if (CurrentItem <= craftingslotX * craftingslotY - 1)
                    CurrentItem++;

                pl.PlaySoundsPitched(ClickClip, 1f);
                SetSlots();

            }
        }



    }

    public void AddQuest(int QID)
    {


        ONOFF(NewQuest, true);
        NewQuestBool = true;

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == QID && !QD.QuestsEN[i].Started && !Quests.Contains(QD.QuestsEN[i]))
            {
                Quests.Add(QD.QuestsEN[i]);
                Quests[Quests.Count - 1].Started = true;
                QD.QuestsEN[i].Started = true;
            }
        }

    }


    void Journal()
    {

        if (((pl.MouseOB.GetComponent<MouseController>().UIColl(JournalButton) && pl.IM.LeftMouseButtonDown && JournalButton.GetComponent<Image>().enabled) || pl.IM.journal_b) && IM.ActionDelay < Time.fixedTime && !showinvent && !pl.menu.MenuONOFF && !pl.Chatting && !ShowAch)
        {
            showjournal = !showjournal;


            ONOFF(Controlls, showjournal);
            DrawJournal(showjournal);
            ONOFF(NewQuest, showjournal);
            IM.ActionDelay = Time.fixedTime + 0.1f;

            NewQuestBool = false;

        }


        if (showjournal)
        {


            for (int i = 0; i < Quests.Count; i++)
            {


                if (QuestMenu.transform.Find("Quest" + i) != null)
                {
                    QuestMenu.transform.Find("Quest" + i).Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = Quests[i].Description[0];

                    if (Quests[i].Done)
                    {
                        QuestMenu.transform.Find("Quest" + i).Find("QuestMark").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/QuestDone");
                    }

                }
            }



            if (Quests.Count > 1)
            {
                QuestMenu.transform.Find("Header").SetAsLastSibling();

                if ((pl.IM._vertical < 0 || pl.IM.DPADY < 0) && CurrentQuest > 0 && VertDelay < Time.fixedTime)
                {



                    CurrentQuest--;

                    pl.PlaySoundsPitched(ClickClip, 0.8f);

                    for (int i = 0; i < Quests.Count; i++)
                    {
                        if (QuestMenu.transform.Find("Quest" + i) != null)
                        {
                            QuestMenu.transform.Find("Quest" + i).transform.position = new Vector3(QuestMenu.transform.position.x, QuestMenu.transform.position.y + (i * -140f) - 10f + CurrentQuest * 100, 0);
                        }
                    }

                    VertDelay = Time.fixedTime + 0.1f;
                }

                if ((pl.IM._vertical > 0 || pl.IM.DPADY > 0) && CurrentQuest < Quests.Count - 1 && VertDelay < Time.fixedTime)
                {

                    CurrentQuest++;

                    pl.PlaySoundsPitched(ClickClip, 1);

                    for (int i = 0; i < Quests.Count; i++)
                    {
                        if (QuestMenu.transform.Find("Quest" + i) != null)
                        {
                            QuestMenu.transform.Find("Quest" + i).transform.position = new Vector3(QuestMenu.transform.position.x, QuestMenu.transform.position.y + (i * -140f) - 10f + CurrentQuest * 100, 0);
                        }
                    }

                    VertDelay = Time.fixedTime + 0.1f;
                }



            }
        }
    }

    void ChooseUIAndTooltipPositions()
    {
        if (!showinvent) pl.inv.ToolTip.SetActive(false);
        else pl.inv.ToolTip.SetActive(true);

        if (IM.ActionDelay > Time.fixedTime || PauseInventory) return;
        
        if (!crafting)
        {
            if (CurrentItem > inventoryFolder.Count)
                CurrentItem = inventoryFolder.Count - 1;
        }



        if (CurrentItem > -1)
        {


            if ( showinvent )
            {
                if (CurrentItem > inventoryFolder.Count - 1) CurrentItem = inventoryFolder.Count - 1;

                CurrentItemID = inventoryFolder[CurrentItem].itemID;
                if (GetCurrentItem().itemID > -1)
                {
                
                    ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = ToolpitString(inventoryFolder[CurrentItem]);
              
                   /* if (Choose.transform.position.y < 700)
                        ToolTip.transform.position = Choose.transform.position;
                    else
                        ToolTip.transform.position = new Vector3(Choose.transform.position.x, 700, ToolTip.transform.position.z);
                        */

                }
                else ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";



                Choose.transform.position = slots[CurrentItem].transform.position + pl.inv.ShakeVector();
            }





        }
        else if (!ChooseTopSegmentSlot && !PauseInventory)
        {
            if (!IM.MouseMode)
            {
                Choose.transform.position = new Vector3(99999, 99999, 999);
        
            }
            else

            {
                Choose.transform.position = pl.MouseOB.transform.position + pl.inv.ShakeVector();

            }
            ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
           // ToolTip.transform.position = new Vector3(99999, 99999, ToolTip.transform.position.z);
        }







        if (!crafting)
        {
            if (NeedItemGameobject.Count > 0)
            {
                Destroy(NeedItemGameobject[NeedItemGameobject.Count - 1]);
                NeedItemGameobject.RemoveAt(NeedItemGameobject.Count - 1);
            }
        }

        ToolTip.transform.SetAsLastSibling();

    }











    public void DropItemInSameSpotNOAUDIO(Vector3 DropPos, int count, int[] ItemDrop_ID, int durability)
    {

        DropItemBody(DropPos, count, ItemDrop_ID, durability);
    }


    public void DropItemInSameSpot(Vector3 DropPos, int count, int[] ItemDrop_ID, int durability)
    {
        print("DropItemInSameSpot");
        DropItemBody(DropPos, count, ItemDrop_ID, durability);

        pl.PlaySoundsPitched(PickItem, 0.5f);
    }

    public void DropItemDifferentSpotsNearby(Vector3 DropPos, int count, int[] ItemDrop_ID, int durability)
    {

        DropItemBody(DropPos + new Vector3(Random.Range(-0.5f, 0.6f), Random.Range(-0.5f, 0.6f), 0), count, ItemDrop_ID, durability);

        pl.PlaySoundsPitched(PickItem, 0.5f);
    }

    void CorrectDropPosition(ref Vector3 DropPos, int d)
    {
        if (_constr.DroppedItems[d] == null) return;
        
        if (DropPos != _constr.DroppedItems[d].transform.position) return;

       
        if (_constr.GreyMap.GetTile(_constr.GreyMap.WorldToCell(DropPos)) != null)
        {
           
            DropPos += new Vector3(0.5f, 0, 0);
            
        }

    }

    void SetCurrentCraftingItem()
    {
        int mouseid = -1;

      
        for (int r = 0; r < CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots.Length; r++)
        {
            for (int i = 0; i < CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[r].Slot.Length; i++)
            {
                
                if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[r].Slot[i]) && !pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(CraftingCross))
                {
                    if (CurrentItem != CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[r].items[i].itemID)
                        pl.PlaySoundsPitched(ClickClip, 0.8f);
                
                    mouseid = CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[r].items[i].itemID;
                    CurrentItem = CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[r].items[i].itemID;

                 
                    PauseInventory = true;
           

                    // ChooseTopSegmentSlot = false;

                    break;
                }

           

                if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(CraftingCross))
                {
                    CurrentItem = 0;

                }


            }
        }


        
    }

    void CollidingOneOfTheSlots(int i)
    {
        //if(crafting && !IM.MouseMode) return;
 
        if (!pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(slots[i]) && IM.MouseMode)
            return;

        if (!IM.MouseMode &&  !Choose.GetComponent<CollList>().GetCollList().Contains(slots[i]) )
            return;

        if( i < inventoryFolder.Count &&  inventoryFolder[i].itemID == -1) return;
        if (i >= inventoryFolder.Count) return;




        if (CurrentItem != i)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f);

            if (IM.MouseMode)
            {
                CurrentItem = i;

               /* if (i >= 12)
                {
                    SlotSlide = i - 12;
                    print("mouse slide: " + SlotSlide);
                    SetSlots();
                }

                if (i < 12)
                {
                    SlotSlide = i - 12;
                    print("mouse slide: " + SlotSlide);
                    SetSlots();
                }*/
            }
            PauseInventory = false;
            ChooseTopSegmentSlot = false;
        }


        if (!pl.IM.LeftMouseButtonDown && !pl.IM.enter_b)
        return;

  
        if (BufferItem.itemID > -1)
        {
            if (IM.ActionDelay < Time.fixedTime && !crafting)
            {
                UnSetBufferItem();
                IM.ActionDelay = Time.fixedTime + 0.1f;
            }
        }
       


        if (GetCurrentItem().Structure )
        {

            _constr.SetToPlayerPos();
            if (GetCurrentItem().TargetTileMap == null)
                SetToMouse(GetCurrentItem().ObjectPrefs, 0, 0, GameObject.Find("Floor").GetComponent<Tilemap>(), GetCurrentItem().itemID);
            else SetToMouse(GetCurrentItem().ObjectPrefs, 0, 0, GetCurrentItem().TargetTileMap, GetCurrentItem().itemID);
       
            showinvent = false;
            ONOFF(gameObject, false);
            
       


        }

          
        
    }


    public void SetToMouse(GameObject ObjectPrefs, int SetObList, int NumInList, Tilemap TargetMap, int ID)
    {
        TileBase[] TargetBrush = new TileBase[0];

        if (pl.inv.GetCurrentItem().TargetBrush != null)
            TargetBrush = pl.inv.GetCurrentItem().TargetBrush;

        if (TargetBrush == null)
            TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Isometric/Ground") };
   

        if (_constr.OnUIDelay < Time.fixedTime)
        {
            if (GetComponent<ProgressionDraw>() == null)
            {
                if (GetComponent<AudioSource>() != null)
                    GetComponent<AudioSource>().Play();

          
                SetObjectOnMouse(
                    ObjectPrefs,
                    SetObList,
                    NumInList,
                    TargetBrush,
                    TargetMap, ID, null, null);

            }
            else
            {
                if (GetComponent<ProgressionDraw>().Active)
                {
                    GetComponent<AudioSource>().Play();
                    SetObjectOnMouse(ObjectPrefs, SetObList, NumInList, TargetBrush, TargetMap, ID, GetComponent<ProgressionDraw>().ItemNeeded, GetComponent<ProgressionDraw>().ItemNeededCount);


                }
            }

            _constr.OnUIDelay = Time.fixedTime + 0.1f;
        }
    }

    public void Shake(Vector3 newshake)
    {
        ShakePos = newshake;
        PlaySoundsPitched(ShakeClip, 1);
        HorDelay = Time.fixedTime + 0.1f;
    }

    public void StopShake()
    {
        ShakePos = new Vector3(0, 0, 0);
    }


    public Vector3 ShakeVector()
    {
        if (ShakeTimer < Time.fixedTime)
        {
            if (Mathf.Abs(ShakePos.x) > 0.1f)
            {
                ShakePos.x *= -1;
                ShakePos.x /= 1.2f;
            }
            else ShakePos = new Vector3(0, ShakePos.y, 0);

            if (Mathf.Abs(ShakePos.y) > 0.1f)
            {
                ShakePos.y *= -1;
                ShakePos.y /= 1.2f;
            }
            else ShakePos = new Vector3(ShakePos.x, 0, 0);


            ShakeTimer = Time.fixedTime + 0.05f;
        }
        return ShakePos;
    }

    void SetObjectOnMouse(GameObject n, int c, int numinlist, TileBase[] TargetTile, Tilemap TMAP, int ID, int[] NeedeItems, int[] ItemNeededCounts)
    {
       _constr.MenuNumber = c;

        if (_constr.transform.childCount > 0)
        {
            for (int j = 0; j < _constr.transform.childCount; j++)
                Destroy(_constr.transform.GetChild(j).gameObject);
        }

        if (n.GetComponent<StatsControll>() != null)
            n.GetComponent<StatsControll>().enabled = true;

        if (n.GetComponent<PubObject>() != null)
            n.GetComponent<PubObject>().enabled = true;


        GameObject i = Instantiate<GameObject>(n,
            _constr.transform);

        if (i.GetComponent<StatsControll>() != null)
            i.GetComponent<StatsControll>().enabled = false;

        if (i.GetComponent<PubObject>() != null)
            i.GetComponent<PubObject>().enabled = false;


        if (i.GetComponent<Enemies>() != null)
            i.GetComponent<Enemies>().enabled = false;

        if (i.GetComponent<GenerateMoney>() != null)
            i.GetComponent<GenerateMoney>().enabled = false;

        if (i.GetComponent<GetItem>() != null)
            i.GetComponent<GetItem>().enabled = false;

        if (i.GetComponent<MovementControll>() != null)
            i.GetComponent<MovementControll>().enabled = false;

        if (i.GetComponent<CharacterMove>() != null)
            i.GetComponent<CharacterMove>().enabled = false;

        if (n.GetComponent<Animator>() != null)
            n.GetComponent<Animator>().enabled = false;


        if (i.GetComponent<PathUpdate>() != null)
            i.GetComponent<PathUpdate>().enabled = false;

        i.transform.position = new Vector2(_constr.transform.position.x + 0.5f, _constr.transform.position.y);
        i.name = "Mouse" + n.name;

        i.GetComponent<PubObject>()._TileBase = TargetTile[0];

        _constr.ItemNeeded = NeedeItems;
        _constr.ItemNeededCount = ItemNeededCounts;

        i.GetComponent<PubObject>().MAPS = TMAP;
      
        // i.GetComponent<PubObject>().floors = TMAP;

        i.GetComponent<PubObject>().TrueName.Add(n.name);
        _constr.OnButtonID = ID;

        if (!VaultUI.showthis)
            _constr.ActivateBuilding();
        
        UpdateInvFolder();
        _constr.RandomisedNumber = 0;
        _constr.ChooseMouseObject = false;
     
    }


    public void UnSetBufferItem()
    {


        if (BufferItem.itemID > -1)
        {

            AddItem(pl.inv.BufferItem.itemID, pl.inv.BufferItem.Count, pl.inv.BufferItem.Durability, pl._transform.position);
            PlaySoundsPitched(TakeItemClip, 1);
            BufferItem = new Item();
            //if (showinvent)
            //  pl.menu.ActionDelay = Time.fixedTime + 0.2f;
        }

        //  pl.inv.ONOFF(gameObject, false);

        //  if (crafting) return;


        PauseInventory = false;
        ChooseTopSegmentSlot = false;


    }

    void DropItemBody(Vector3 DropPos, int count, int[] ItemDrop_ID, int durability)
    {

        if (_constr.DroppedItems.Count >= 500)
        {
            _constr.AddLogPart("Cant drop. Too many items on the floor", "Не можна кинути, забагато айтемів на підлозі", "落とせない。床にアイテムが多すぎる", gameObject);
            return;
        }
   


            for (int d = 0; d < _constr.DroppedItems.Count; d++)
            {
                CorrectDropPosition(ref DropPos, d);
            }



        for (int j = 0; j < ItemDrop_ID.Length; j++)
        {

            if (ItemDrop_ID[j] > -1)
            {

                GameObject NewItem = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Item"));
                NewItem.name = "Dropped Item " + _constr.DroppedItems.Count;
                NewItem.transform.position = DropPos + new Vector3(0.5f * j, 0, 0);
                NewItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Items/" + GetItemInDatabase(ItemDrop_ID[j]).itemNames[0]);
                NewItem.GetComponent<GetItem>().item = new int[1] { ItemDrop_ID[j] };
                NewItem.GetComponent<GetItem>().itemcount = new int[1] { count };
                NewItem.GetComponent<GetItem>().durability = new int[1] { durability };
                NewItem.GetComponent<GetItem>().DontSetCounts = true;


                NewItem.GetComponent<StatsControll>().CurrentGrowState = count;
                NewItem.GetComponent<StatsControll>().ItemCount = count;


                _constr.DroppedItems.Add(NewItem);

                if (pl.menu.SL.ObjectsToDestroy.Contains(NewItem.name)) pl.menu.SL.ObjectsToDestroy.Remove(NewItem.name);
            }


        }

    }

    void SetSlots()
    {

        WidthSlot = 120;
        ScreenBorder = new Vector2(WidthSlot/1.5f , WidthSlot/1.5f);
 

        for (int i = 0; i < slots.Count; i++)
        {
#if UNITY_SWITCH
                   
        slotspace = 5;
            Vector3 c = new Vector3(i * WidthSlot/1.2f - SlotSlide * WidthSlot/1.2f, ScreenBorder.y, 0);
            SlotsRect[i] = new Rect( c.x , c.y , WidthSlot - slotspace, WidthSlot  - slotspace);
#endif
#if UNITY_STANDALONE || UNITY_PS5 || UNITY_PS4
            slotspace = 10;
            Vector3 c = new Vector3(i * (WidthSlot + slotspace) - SlotSlide * WidthSlot, ScreenBorder.y, 0);
            SlotsRect[i] = new Rect(c.x , c.y, WidthSlot - slotspace, WidthSlot - slotspace);
#endif


            slots[i].GetComponent<RectTransform>().position = new Vector2(ScreenBorder.x + SlotsRect[i].x, SlotsRect[i].y);
            slots[i].GetComponent<RectTransform>().sizeDelta = new Vector2(WidthSlot, WidthSlot);

        }


        
    }


    public void AddInvSlot(int AddAmount)
    {

        for (int i = slotX; i < slotX + AddAmount; i++)
        {
           
     

            GameObject Slot = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Slot"), InventoryUIOB.transform);
            Slot.GetComponent<RectTransform>().position = new Vector2(SlotsRect[i].x, SlotsRect[i].y);

            if (!showinvent)
                Slot.SetActive(false);
            else Slot.SetActive(true);

            Slot.name = "Slot" + i;
            slots.Add(Slot);


            Choose.transform.SetAsLastSibling();



            SetSlots();
        }


    }

    public void RemoveInvSlot(int RemoveAmount)
    {

        for (int i = 0; i < RemoveAmount; i++)
        {
            // slots.Add(new Item());


            if (inventory[inventory.Count - 1].itemID > -1)
            {
                DropItemInSameSpot(transform.position, inventory[inventory.Count - 1].Count,  new int[1] { inventory[inventory.Count - 1].itemID }, inventory[inventory.Count - 1].Durability);
            }


            Destroy(slots[slots.Count - 1]);

            slots.RemoveAt(inventory.Count - 1);
            inventory.RemoveAt(inventory.Count - 1);
            
            if (CurrentItem > slots.Count - 1) CurrentItem = slots.Count - 1;
        }
    }



    public void SetStatus(Item item, ref Image IMG)
    {
        bool eateble = false;


        if (item.Food || item.Dish || item.Satiety > 0)
            eateble = true;

        if (eateble)
            IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Food");
        else if (item.HP > 0 && !item.Food && !item.Dish)
            IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Heal");
        else if (item._type == Item.type.weapon)
            IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Weapon");
        else if (item.Character)
            IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Character");
        else if (item.Structure)
        {
            if(item._StructureType == Item.StructureType.Building)
            IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Structure");
            if (item._StructureType == Item.StructureType.Decoration)
                IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Decoration");
            if (item._StructureType == Item.StructureType.Farms)
                IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Farm");
            if (item._StructureType == Item.StructureType.Protection)
                IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Protection");
            if (item._StructureType == Item.StructureType.Tiles)
                IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Statuses/Surface");
        }
        else
        {
            IMG.sprite = Resources.Load<Sprite>("Sprites/UI/Transparent");
            IMG.enabled = false;
        }


        IMG.color = new Color(1, 1, 1, 1);

    }

    public Item DeepCopyItem(int id, int count, int durability)
    {
        Item result = new Item();
        Item T = pl.inv.GetItemInDatabase(id);

        if (pl.inv.GetItemInDatabase(id) == null)
        {
           
            return result;
        }


        result.itemID = T.itemID;

        result.itemNames = T.itemNames;
        result.itemDesc = T.itemDesc;
        result._type = T._type;
        result.Cost = T.Cost;

        result.BuildingCost = T.BuildingCost;
        if (result.BuildingCost == 0) result.BuildingCost = T.Cost;

        result.NeedItemsIDs = T.NeedItemsIDs;
        result.NeedItemsCounts = T.NeedItemsCounts;

        result.Count = count;
        result.CanStack = T.CanStack;
        result._bodypart = T._bodypart;
        result.Structure = T.Structure;
        result._StructureType = T._StructureType;
 

        result.TargetTileMap = T.TargetTileMap;
        result.TargetBrush = T.TargetBrush;

        result.ObjectPrefs = T.ObjectPrefs;

     
        result.HP = T.HP;
        result.Satiety = T.Satiety;
        result.MaxHP = T.MaxHP;
        result.DamageAmount = T.DamageAmount;
        result.Durability = durability;
        result.StaminaUse = T.StaminaUse;

        result.StaminaRecoverySpeed = T.StaminaRecoverySpeed;

        result.Food = T.Food;
        result.MagicEffectToCast = T.MagicEffectToCast;

        result.Sniff = T.Sniff;

        result.CanBeDropped = T.CanBeDropped;
        result.CanNOTBeRemovedFromTheBody = T.CanNOTBeRemovedFromTheBody;
        result.LootItem = T.LootItem;
        result.Payment = T.Payment;
        result.Gun = T.Gun;

        return result;
    }

    public void ResetInventory()
    {
        inventory = new List<Item>();

      


    }
    public void DoneQuest(int id)
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (Quests[i].ID == id && !Quests[i].Done)
            {
                Quests[i].Done = true;
            }
        }
    }

    public bool CheckQuestStart(int id)
    {
        bool d = false;

        for (int i = 0; i < Quests.Count; i++)
        {
            if (Quests[i].ID == id && Quests[i].Started)
            {
                d = true;
            }
        }

        return d;
    }


    public Quest GetQuest(int id)
    {
        Quest d = null;

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == id)
            {
                d = QD.QuestsEN[i];
            }
        }

        return d;
    }

    public void SetCurrentFolder()
    {

        if (CurrentFolder > FolderButtons.Count - 1) CurrentFolder = FolderButtons.Count - 1;

        FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);
      
        CurrentItem = 0;
        UpdateInvFolder();

    }


    public void SetFolder(int c)
    {

        if (c > FolderButtons.Count - 1) c = FolderButtons.Count - 1;

            FolderButtons[c].transform.Find("NewItemTag").gameObject.SetActive(false);
            CurrentFolder = c;
            CurrentItem = 0;
            UpdateInvFolder();
        
    }


    public bool CheckQuestDone(int id)
    {
        bool d = false;

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == id && QD.QuestsEN[i].Done)
            {
                d = true;
            }
        }

        return d;
    }

    int GetQuestID(int id)
    {
        int r = 0;
        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == id) r = i;
        }
        return r;
    }

    public void SetAch(string name)
    {
        /*if(SteamAPI.Init())
        SteamUserStats.SetAchievement(name);*/
    }

    public void PlaySoundsPitched(AudioClip AC, float pitch)
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = AC;
            GetComponent<AudioSource>().pitch = pitch;
            GetComponent<AudioSource>().Play();
        }
    }

    public float GetDrawFinalItemTimer()
    {
        return DrawFinalItemTimer;
    }
    public void SetFinalItem(List<string> names)
    {
        DrawFinalItemTimer = Time.fixedTime + 3;
        FinalItem = names;
    }

   
    public bool MouseCollideWithSlots()
    {
        int r = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(slots[i]))
            {
                r++;
            }

        }
        if (r > 0)
            return true;
        else return false;
    }


}