using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public int slotX { get; set; }
    public int slotY;

    private int SlotSlide;


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


    public int XChoise { get; set; }
    public int YChoise { get; set; }

    // Use this for initialization
    private float slotspace = 0;

    private Player pl;
    public int Money { get; set; }

    private Vector2 ScreenBorder;


    private List<GameObject> PickedText = new List<GameObject>();
    private List<string> Picked = new List<string>();
    private List<float> PickedX = new List<float>();
    private List<float> PickedY = new List<float>();

    private List<float> PickedSlide = new List<float>();

    private List<float> PickedSpeeds = new List<float>();



    public float HorDelay { get; set; }
    public float ExitingTimer { get; set; }

    private MenuCustom _menu;
    public float FadeAlpha { get; set; }
   
    public bool showjournal { get; set; }

    [HideInInspector]
    public AudioClip OpenInventory, OpenCardsInv, PickItem, ClickClip, UIOpen, ShakeClip, TakeItemClip;


    public bool devmode;

    private Rect[] SlotsRect;
    private float  WidthSlot;
    private InputMode IM;
    private GameObject InventoryUIOB, StatsUI, LogUI, BlueprintMenu, LeftFolder, RightFolder;
    public GameObject CraftingUIOB { get; private set; }
    
    public GameObject Choose { get; private set; }
    public int CurrentItem { get; private set; }

    public int CurrentItemID { get; private set; }
   
    public bool PauseInventory;
  

    [HideInInspector]
    public GameObject CraftingCross;

    public GameObject LeftArrow { get; private set; }
    public GameObject RightArrow { get; private set; }


    public GameObject EscapeInventory { get; private set; }

    public GameObject ToolTip { get; private set; }
    private GameObject Controlls;

    private bool DrawINV, DrawInvNo;
    private Constructor Constr;

    public GetItem CurrentCraftingTable;
    private bool CraftingDraw;

    public List<GameObject> NeedItemGameobject = new List<GameObject>();

    [HideInInspector]
    public GameObject InventoryButton;


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
    private RectTransform CanvasTransform;

    private Tilemap FloorTilemap;

    private Journal JournalOB;
    void Awake()
    {
        JournalOB = gameObject.AddComponent<Journal>();
        FloorTilemap = InitializeObjects.FloorTilemap;
        CanvasTransform = InitializeObjects.CanvasTransform.GetComponent<RectTransform>();

        for (int i = 0; i < 10; i++)
        {
            PickedText.Add(Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/PickedText"), GameObject.Find("Canvas").transform));
            PickedX.Add(9999);
            PickedY.Add(999);
            PickedSlide.Add(0);
            Picked.Add("");
            PickedSpeeds.Add(0);


        }
        BufferItem = new Item();

        EscapeInventory = GameObject.Find("EscapeInventory");
        InventoryButton = GameObject.Find("InventoryButton");
        BlueprintMenu = GameObject.Find("BlueprintMenu");
        CraftingCross = GameObject.Find("CraftingCross");
        StatsUI = CanvasTransform.transform.Find("Stats").gameObject;

        if(GameObject.Find("VaultUI")!=null)
        VaultUI = GameObject.Find("VaultUI").GetComponent<ItemsSlotsUI>();

        LogUI = CanvasTransform.transform.Find("Log").gameObject;

        CurrentItemID = -1;

        Constr = InitializeObjects.Constr;
        pl = GetComponent<Player>();

        _menu = Constr.GetComponent<MenuCustom>();
        database = InitializeObjects.Itemdatabase;
        Controlls = GameObject.Find("Controlls");

        FadeAlpha = 1;

        LoadSounds();
        
        XChoise = 0;
        YChoise = 0;

        if (GameObject.Find("CraftingUI") == null)
        {
            CraftingUIOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/CraftingUI"), CanvasTransform);
            CraftingUIOB.name = "CraftingUI";
            CraftingUIOB = GameObject.Find("CraftingUI");

        }
        else CraftingUIOB = GameObject.Find("CraftingUI");



        CraftingUIOB.transform.SetSiblingIndex(8);

        if (GameObject.Find("InventoryUI") == null)
        {
            GameObject INV = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/InventoryUI"), CanvasTransform);
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

        LeftArrow = InventoryUIOB.transform.Find("LeftArrow").gameObject;
        RightArrow = InventoryUIOB.transform.Find("RightArrow").gameObject;

        Choose = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/ChooseUI"), InventoryUIOB.transform);
        Choose.name = "InvChoose";

        IM = GetComponent<InputMode>();


        RightArrow.SetActive(false);
        LeftArrow.SetActive(false);
   
        ONOFF(EscapeInventory, false);
   
        LoadSlots();


       
    }

    void LoadSounds()
    {
        OpenInventory = Resources.Load<AudioClip>("Sound/UI/Inventory_Open");
        OpenCardsInv = Resources.Load<AudioClip>("Sound/UI/CardDeck_Open");
        PickItem = Resources.Load<AudioClip>("Sound/Items/PickItem");

        UIOpen = Resources.Load<AudioClip>("Sound/UI/UI_Open");

        ClickClip = Resources.Load<AudioClip>("Sound/UI/Click_0");
        ShakeClip = Resources.Load<AudioClip>("Sound/UI/UI_Shake");
        TakeItemClip = Resources.Load<AudioClip>("Sound/UI/Accept");

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


        ShowControlls();


        if (pl.IM._vertical > 0.5f || pl.IM._vertical < -0.5f)
        {
            if (CurrentItem < 0)
                CurrentItem = 0;
        }

   
        Crafting();
  

        ExitFromInventory();
        ShowPicketItems();


        if (pl.GetComponent<Achivements>()!=null) 
            ShowAch = pl.GetComponent<Achivements>().ShowAch;

       
        Start_Close_Inventory();
        
        ONOFF_Inventory();

        SelectFolder();


        for (int x = 0; x < slots.Count; x++)
            SetItemsIntoSlots(x);

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
        if (IM.ActionDelay > Time.fixedTime ||
            showjournal ||
            pl.menu.MenuONOFF ||
            pl.Chatting ||
            ShowAch ||
            Constr.TutorialPause || 
            pl.StartLoading || 
            blueprintshow)
            return;


        if ((_menu.UIColl(InventoryButton) && pl.IM.LeftMouseButtonDown && InventoryButton.GetComponent<Image>().enabled) || pl.IM.inventory_b) 
        {
            showinvent = !showinvent;
            
            if (!showinvent)
            {
                PlaySoundsPitched(UIOpen,0.8f);
                Constr.DeActivateBuilding();
                PauseInventory = false;
                crafting = false;

                CurrentItem = 0;
                SlotSlide = 0;

            }

            Constr.ChooseMouseObject = false;

            if (showinvent && !crafting )
            {
                
                PlaySoundsPitched(UIOpen, 1);
                CurrentItem = 0;
              
                    Choose.transform.position = slots[CurrentItem].transform.position;
                    PauseInventory = false;
  
                UpdateInvFolder();
            }


            SetSlots();


            IM.ActionDelay = Time.fixedTime + 0.2f;
        }
    }



    void ONOFF_Inventory()
    {
        if (!showinvent)
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
            return;
        }

        if (DrawINV) return;

        Constr.DeActivateBuildingNOINV();
        ONOFF(GameObject.Find("ButtonsUI"), false);
        ONOFF(Controlls, false);
        DrawInventory(true);
        DrawINV = true;



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
            if (pl.GetMouseCollList().Contains(FolderButtons[i]) && (pl.IM.LeftMouseButtonDown || pl.IM.enter_b))
            {
                pl.PlaySoundsPitched(ClickClip, 1);
                FolderButtons[i].transform.Find("NewItemTag").gameObject.SetActive(false);
                CurrentFolder = i;
                CurrentItem = 0;
                PauseInventory = false;
                ChooseTopSegmentSlot = false;
                inventoryFolder = new List<Item>();
                UpdateInvFolder();
            }
        }


        if (!showinvent ) return;

        if ((pl.IM.LeftTrigger || ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.GetMouseCollList().Contains(LeftFolder))) && IM.ActionDelay < Time.fixedTime && CurrentFolder > 0)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f + CurrentFolder * 0.05f);
            CurrentFolder--;
            FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);
            CurrentItem = 0;
            SlotSlide = 0;
            inventoryFolder = new List<Item>();
            UpdateInvFolder();

            IM.ActionDelay = 0.1f;
        }

        if ((pl.IM.RightTrigger || ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.GetMouseCollList().Contains(RightFolder))) && IM.ActionDelay < Time.fixedTime && CurrentFolder < FolderButtons.Count - 1)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f + CurrentFolder * 0.05f);
            CurrentFolder++;
            FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);
            CurrentItem = 0;
            SlotSlide = 0;
            inventoryFolder = new List<Item>();
            UpdateInvFolder();
            IM.ActionDelay = 0.1f;
        }


        if (!showinvent) return;


        if (inventoryFolder.Count <= 0)
        {
            CurrentItem = 0;
        }



    }


    void UpdateFolderNumber(int ID)
    {
        if (!GetItemInDatabase(ID).Structure) return;

        if (crafting)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (GetItemInDatabase(ID)._StructureType == Item.StructureType.Building||
                    GetItemInDatabase(ID)._StructureType == Item.StructureType.Protection||
                       GetItemInDatabase(ID)._StructureType == Item.StructureType.Decoration)
                    CurrentFolder = 0;

                if (GetItemInDatabase(ID)._StructureType == Item.StructureType.Tiles)
                    CurrentFolder = 1;

                if (GetItemInDatabase(ID)._StructureType == Item.StructureType.Farms)
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



    void SetItemsIntoSlots(int x)
    {
        if (x > inventoryFolder.Count - 1)
        {
            if (slots[x].transform.Find("Item") != null)
                Destroy(slots[x].transform.Find("Item").gameObject);
            return;
        }
        if (inventoryFolder[x] == null) return;

        if (inventoryFolder[x].itemID == -1 && slots[x].transform.Find("Item") != null)
        {
            Destroy(slots[x].transform.Find("Item").gameObject);
            return;
        }


        if (slots[x].transform.Find("Item") != null)
        {
          

            slots[x].transform.Find("Item").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inventoryFolder[x].itemNames[0]);
            slots[x].transform.Find("Item").transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            if (inventoryFolder[x].Count > 0 && inventoryFolder[x].CanStack)
                slots[x].transform.Find("Item").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + inventoryFolder[x].Count;
            else slots[x].transform.Find("Item").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
            
            return;
        }

        GameObject ItemOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Item"), slots[x].transform);
        ItemOB.GetComponent<RectTransform>().position = slots[x].GetComponent<RectTransform>().position;

        ItemOB.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inventoryFolder[x].itemNames[0]);
        ItemOB.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        ItemOB.name = "Item";

        if (inventoryFolder[x].CanStack)
            slots[x].transform.Find("Item").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + inventoryFolder[x].Count;


    }


    void ExitFromInventory()
    {
        if ((!showinvent && !showjournal) || pl.menu.MenuONOFF) return;
        
        if (!pl.IM.menu_b && !pl.IM.exit_b ) return;
        

        PlaySoundsPitched(UIOpen, 0.8f);

        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);

        ONOFF(GameObject.Find("ButtonsUI"), true);
        ONOFF(Controlls, true);
        if (showjournal) JournalOB.DrawJournal(false);
        if (showinvent) DrawInventory(false);
        crafting = false;
        showinvent = false;
        showjournal = false;
        DrawINV = false;
        IM.ActionDelay = Time.fixedTime + 0.1f;
        _menu.MenuActionDelay = Time.fixedTime + 0.1f;
        

        
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
        Item result = new Item();
        if (database == null) return result;

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

        if (numplus > 1)
            ADDPickedName("+ " + GetItemInDatabase(id).itemNames[_menu.Language] + " x " + numplus,  0.25f, NamePos);
        else
            ADDPickedName("+ " + GetItemInDatabase(id).itemNames[_menu.Language],  0.25f, NamePos);


    }


    public void AddItemNOAUDIO(int id, int numplus, int durability, Vector2 NamePos)
    {
        if (id <= -1) return;

        if (GetItemInDatabase(id) == null)
            return;

        AddItemToInv(id, numplus, durability, NamePos);

        if (numplus > 1)
            ADDPickedName("+ " + GetItemInDatabase(id).itemNames[_menu.Language] + " x " + numplus,  0.25f, NamePos);
        else
            ADDPickedName("+ " + GetItemInDatabase(id).itemNames[_menu.Language],  0.25f, NamePos);

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


    public void ADDPickedName(string text,float speed, Vector2 Pos)
    {
        print("ADDPickedName");
        for (int i = 0; i < PickedText.Count; i++)
        {
            if (PickedSlide[i] <= 0)
            {
                PickedX[i] = Pos.x;
                PickedY[i] = Pos.y;
                PickedSlide[i] = 1;
                Picked[i] = text;
                PickedSpeeds[i] = speed;
                return;
            }
        }
    }


    void ShowPicketItems()
    {
        for (int y = 0; y < PickedText.Count; y++)
        {
            if (PickedSlide[y] > 0)
            {
                PickedText[y].GetComponent<RectTransform>().position =
                  pl.MainCamera.WorldToScreenPoint(new Vector3(PickedX[y], PickedY[y] - PickedSlide[y] * 0.5f + 0.5f));

                PickedText[y].GetComponent<TextMeshProUGUI>().text = Picked[y];


                PickedText[y].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, PickedSlide[y]);

                PickedSlide[y] -= Time.deltaTime * 4 * PickedSpeeds[y];
         
            }


            if (PickedSlide[y] <= 0)
            {
                PickedText[y].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);

                PickedText[y].GetComponent<RectTransform>().position = new Vector3(3000, 999);

                Picked[y] = "";
                PickedY[y] = 9999;
                PickedX[y] = 9999;
                PickedSlide[y] = 0;
                PickedSpeeds[y] = 0;

            }

        }
    }


    public void ReduceItemCount(int id, int minusn)
    {
    

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
        if ((XChoise + YChoise * slotX) < inventory.Count)
            return inventory[XChoise + YChoise * slotX].itemNames[0];
        else return null;
    }





  


    public void ONOFF(GameObject g, bool TF)
    {
        if (g == null) return;
        if (g.transform.parent == Constr.transform) return;

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
        if (g.GetComponent<GamepadUI>() != null && _menu.HideUI && TF)
            return;


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

       /* if (g.GetComponent<Character>() != null)
            g.GetComponent<Character>().enabled = TF;
       */
       if (g.GetComponent<CharacterPath>() != null)
          g.GetComponent<CharacterPath>().enabled = TF;
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

            SetSlots();
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






    void ChoiseSlotsWithMouse()
    {
      

        if (pl.GetMouseCollList().Contains(CraftingCross) && pl.IM.LeftMouseButtonDown)
        {
            if (Constr.ShowChargeUI)
            {
                ONOFF(GameObject.Find("ButtonsUI"), true);
                ONOFF(Controlls, true);
            }


            if (showjournal) JournalOB.DrawJournal(false);
            if (showinvent) DrawInventory(false);
            crafting = false;

           
            showinvent = false;
            showjournal = false;
            DrawINV = false;

            IM.ActionDelay = Time.fixedTime + 0.1f;
            pl.menu.PlayAudio(ClickClip);
        }



        if (pl.GetMouseCollList().Contains(EscapeInventory) && pl.IM.LeftMouseButtonDown)
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



        if (crafting || !pl.IM.LeftMouseButtonDown) return;
        

        if (pl.GetMouseCollList().Contains(LeftArrow) )
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

        if (pl.GetMouseCollList().Contains(RightArrow) )
        {
            if(SlotSlide < inventoryFolder.Count - 5)
            SlotSlide++;

            if (CurrentItem <= craftingslotX * craftingslotY - 1)
                CurrentItem++;

            pl.PlaySoundsPitched(ClickClip, 1f);
            SetSlots();

        }
        



    }

   

  
    void ChooseUIAndTooltipPositions()
    {
        if (!showinvent) ToolTip.SetActive(false);
        else ToolTip.SetActive(true);

        if (IM.ActionDelay > Time.fixedTime || PauseInventory) return;
        
        if (!crafting)
        {
            if (CurrentItem > inventoryFolder.Count)
                CurrentItem = inventoryFolder.Count - 1;
        }



        if (CurrentItem > -1)
        {


            if ( showinvent && inventoryFolder.Count > 0)
            {
                if (CurrentItem > inventoryFolder.Count - 1) CurrentItem = inventoryFolder.Count - 1;

                CurrentItemID = inventoryFolder[CurrentItem].itemID;
                if (GetCurrentItem().itemID > -1)
                {
                
                    ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = TooltipsString(inventoryFolder[CurrentItem]);
              

                }
                else ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";



                Choose.transform.position = slots[CurrentItem].transform.position + ShakeVector();
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
                Choose.transform.position = pl.MouseUI.transform.position + ShakeVector();

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
        if (Constr.DroppedItems[d] == null) return;
        
        if (DropPos != Constr.DroppedItems[d].transform.position) return;

       
        if (Constr.GreyMap.GetTile(Constr.GreyMap.WorldToCell(DropPos)) != null)
        {
           
            DropPos += new Vector3(0.5f, 0, 0);
            
        }

    }

    void SetCurrentCraftingItem()
    {
        int mouseid = -1;

        ItemsSlotsUI CraftingSlots = CraftingUIOB.GetComponent<ItemsSlotsUI>();
        for (int r = 0; r < CraftingSlots.Slots.Length; r++)
        {
            for (int i = 0; i < CraftingSlots.Slots[r].Slot.Length; i++)
            {
                
                if (pl.GetMouseCollList().Contains(CraftingSlots.Slots[r].Slot[i]) && !pl.GetMouseCollList().Contains(CraftingCross))
                {
                    if (!PauseInventory && CurrentItem != CraftingSlots.Slots[r].items[i].itemID)
                        pl.PlaySoundsPitched(ClickClip, 0.8f);
                    
                    mouseid = CraftingSlots.Slots[r].items[i].itemID;
                    CurrentItem = CraftingSlots.Slots[r].items[i].itemID;

                    PauseInventory = true;
           

                    break;
                }

           

                if (pl.GetMouseCollList().Contains(CraftingCross))
                {
                    CurrentItem = 0;

                }


            }
        }


        
    }

    void CollidingOneOfTheSlots(int i)
    {
      
        if (!pl.GetMouseCollList().Contains(slots[i]) && IM.MouseMode)
            return;

        if (!IM.MouseMode &&  !Choose.GetComponent<CollList>().GetCollList().Contains(slots[i]) )
            return;

        if( i < inventoryFolder.Count &&  inventoryFolder[i].itemID == -1) return;
        if (i >= inventoryFolder.Count) return;

        if (CurrentItem != i)
        {
            if (GetCurrentItem().itemID <= -1)
            {
                pl.PlaySoundsPitched(ClickClip, 0.8f);
            }
            if (IM.MouseMode)
            {
                CurrentItem = i;
            }
            PauseInventory = false;
            ChooseTopSegmentSlot = false;
        }


        if (pl.IM.RightMouseButtonDown )
        {
            if (!inventoryFolder[i].CantBeSold)
            {
                AddItem(9, (int)(inventoryFolder[i].Cost / 1.5f), 999,
                    pl.MainCamera.ScreenToWorldPoint(slots[i].transform.position));

                ReduceItemCount(inventoryFolder[i].itemID, 1);
                UpdateInvFolder();

                return;
            }
            else
                _menu.PlayAudio(_menu.ErrorClip);
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

            Constr.SetToPlayerPos();
            if (GetCurrentItem().TargetTileMap == null)
                SetToMouse(GetCurrentItem().ObjectPrefs, 0, 0, FloorTilemap, GetCurrentItem().itemID);
            else SetToMouse(GetCurrentItem().ObjectPrefs, 0, 0, GetCurrentItem().TargetTileMap, GetCurrentItem().itemID);
       
            showinvent = false;
            ONOFF(gameObject, false);
            
       


        }

          
        
    }


    public void SetToMouse(GameObject ObjectPrefs, int SetObList, int NumInList, Tilemap TargetMap, int ID)
    {
        TileBase[] TargetBrush = new TileBase[0];

        if (GetCurrentItem().TargetBrush != null)
            TargetBrush = GetCurrentItem().TargetBrush;

        if (TargetBrush == null)
            TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Isometric/Ground") };


        if (Constr.OnUIDelay >= Time.fixedTime) return;
        
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

        Constr.OnUIDelay = Time.fixedTime + 0.1f;
        
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
        Constr.MenuNumber = c;

        if (Constr.transform.childCount > 0)
        {
            for (int j = 0; j < Constr.transform.childCount; j++)
                Destroy(Constr.transform.GetChild(j).gameObject);
        }

        if (n.GetComponent<StatsControll>() != null)
            n.GetComponent<StatsControll>().enabled = true;

        if (n.GetComponent<PubObject>() != null)
            n.GetComponent<PubObject>().enabled = true;


        GameObject i = Instantiate<GameObject>(n,
            Constr.transform);

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

        if (i.GetComponent<CharacterPath>() != null)
            i.GetComponent<CharacterPath>().enabled = false;

        if (n.GetComponent<Animator>() != null)
            n.GetComponent<Animator>().enabled = false;


        if (i.GetComponent<Character>() != null)
            i.GetComponent<Character>().enabled = false;



        if (i.GetComponent<PathUpdate>() != null)
            i.GetComponent<PathUpdate>().enabled = false;

        i.transform.position = new Vector2(Constr.transform.position.x + 0.5f, Constr.transform.position.y + 0.5f);
        i.name = "Mouse" + n.name;

        i.GetComponent<PubObject>()._TileBase = TargetTile[0];

        Constr.ItemNeeded = NeedeItems;
        Constr.ItemNeededCount = ItemNeededCounts;

        i.GetComponent<PubObject>().MAPS = TMAP;
      
        i.GetComponent<PubObject>().TrueName.Add(n.name);

        Constr.OnButtonID = ID;

        if (!VaultUI.showthis)
            Constr.ActivateBuilding();

        if (Constr.ShowChargeUI)
        {
            ONOFF(GameObject.Find("ButtonsUI"), true);
            ONOFF(Controlls, true);
        }


        if (showjournal) JournalOB.DrawJournal(false);
        if (showinvent) DrawInventory(false);
        crafting = false;


        showinvent = false;
        showjournal = false;
        DrawINV = false;

        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);



        UpdateInvFolder();
        Constr.RandomisedNumber = 0;
        Constr.ChooseMouseObject = false;
     
    }


    public void UnSetBufferItem()
    {


        if (BufferItem.itemID > -1)
        {

            AddItem(BufferItem.itemID, BufferItem.Count, BufferItem.Durability, pl._transform.position);
            PlaySoundsPitched(TakeItemClip, 1);
            BufferItem = new Item();
            //if (showinvent)
            //  pl.menu.ActionDelay = Time.fixedTime + 0.2f;
        }

        

        PauseInventory = false;
        ChooseTopSegmentSlot = false;


    }

    void DropItemBody(Vector3 DropPos, int count, int[] ItemDrop_ID, int durability)
    {

        if (Constr.DroppedItems.Count >= 500)
        {
            Constr.AddLogPart("Cant drop. Too many items on the floor", "Не можна кинути, забагато айтемів на підлозі", "落とせない。床にアイテムが多すぎる", gameObject);
            return;
        }
   


        for (int d = 0; d < Constr.DroppedItems.Count; d++)
        {
            CorrectDropPosition(ref DropPos, d);
        }



        for (int j = 0; j < ItemDrop_ID.Length; j++)
        {

            if (ItemDrop_ID[j] > -1)
            {

                GameObject NewItem = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Item"));
                NewItem.name = "Dropped Item " + Constr.DroppedItems.Count;
                NewItem.transform.position = DropPos + new Vector3(0.5f * j, 0, 0);
                NewItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Items/" + GetItemInDatabase(ItemDrop_ID[j]).itemNames[0]);
                NewItem.GetComponent<GetItem>().item = new int[1] { ItemDrop_ID[j] };
                NewItem.GetComponent<GetItem>().itemcount = new int[1] { count };
                NewItem.GetComponent<GetItem>().durability = new int[1] { durability };
                NewItem.GetComponent<GetItem>().DontSetCounts = true;


                NewItem.GetComponent<StatsControll>().CurrentGrowState = count;
                NewItem.GetComponent<StatsControll>().ItemCount = count;


                Constr.DroppedItems.Add(NewItem);

                if (pl.menu.SL.ObjectsToDestroy.Contains(NewItem.name)) pl.menu.SL.ObjectsToDestroy.Remove(NewItem.name);
            }


        }

    }

    void SetSlots()
    {

        WidthSlot = 120;
        ScreenBorder = new Vector2(WidthSlot/1.5f , WidthSlot/1.5f);

        WidthSlot = CanvasTransform.rect.width / 16f;
        slotspace = CanvasTransform.rect.height / 192f;

        for (int i = 0; i < slots.Count; i++)
        {
/*#if UNITY_SWITCH
                   
        slotspace = 5;
            Vector3 c = new Vector3(i * WidthSlot/1.2f - SlotSlide * WidthSlot/1.2f, ScreenBorder.y, 0);
            SlotsRect[i] = new Rect( c.x , c.y , WidthSlot - slotspace, WidthSlot  - slotspace);
#endif*/
        
          
            Vector2 pos = new Vector2(i * (WidthSlot + slotspace) - SlotSlide * WidthSlot, -CanvasTransform.rect.height/2+ ScreenBorder.y);

            RectTransform rt = slots[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(ScreenBorder.x + pos.x, pos.y);

            rt.sizeDelta = new Vector2(WidthSlot, WidthSlot);
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
        Item T = GetItemInDatabase(id);

        if (GetItemInDatabase(id) == null)
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
        result.CantBeSold = T.CantBeSold;

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


    public string TooltipsString(Item i)
    {

        //#FF5D5D - red
        //#9DFF99 - green

        string red = "#280d14";
        string green = "#9DFF99";
        string yellow = "#FFF224";

        string s = "";
        string plus = "";

        string stargcolortag = "";
        string endgcolortag = "";

        stargcolortag = "<color=" + red + ">";
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




        if (GetItemInDatabase(i.itemID).Locked)
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
                    s += "<color=" + red + ">" + BodypartsNames[j] + "</color>";

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

      
        
        if (ChooseTopSegmentSlot)
        {
        stargcolortag = "<color=" + red + ">";
        endgcolortag = "</color>";

            s += "\n" + CostString + stargcolortag + " " + i.Cost + endgcolortag;

        }
        else if (!i.CantBeSold)


        {

            stargcolortag = "<color=" + red + ">";
            endgcolortag = "</color>";

            s += "\n" + CostString + stargcolortag + " " + (int)(i.Cost / 1.5f) + endgcolortag;

            }

        

        stargcolortag = "<color=" + red + ">";
        endgcolortag = "</color>";


      /*  s += "\n" + BuildingCostString + stargcolortag + " " + i.BuildingCost + endgcolortag;
        s += "\n";
      */

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


    void ShowControlls()
    {
        if (showinvent || showjournal || blueprintshow || _menu.HideUI) return;
        
        crafting = false;

        if (pl.Chatting)
        {

            ONOFF(Controlls, false);
            ONOFF(JournalOB.NewQuest, false);
            DrawInvNo = false;
        }
        else
        {
            if (!DrawInvNo)
            {
                ONOFF(Controlls, true);
                ONOFF(JournalOB.NewQuest, JournalOB.NewQuestBool);
                DrawInvNo = true;
            }

        }
        
    }

    public void ResetInventory()
    {
        inventory = new List<Item>();

      


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

    
   
    public bool MouseCollideWithSlots()
    {
        int r = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (pl.GetMouseCollList().Contains(slots[i]))
            {
                r++;
            }

        }
        if (r > 0)
            return true;
        else return false;
    }


}