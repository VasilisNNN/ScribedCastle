using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System.Drawing;


#if UNITY_PS5|| UNITY_PS4
using UnityEngine.PS5;
#endif


[System.Serializable]
public class StartStart
{
    public int MaxHP;
    public int MaxHunger;
    public int MaxPlague;
    public int MaxStamina = 5;
    public int Payment = 1;
    public int LootItem = 1;

    public int[] StartItems;
    public int[] StartItemsCounts;

    public int[] UpgradeItems;

    public StartStart(int maxhp, int maxHunger, int maxPlague, int payment, int lootItem, int[] starti, int[] starticounts)
    {

        MaxHP = maxhp;
        MaxHunger = maxHunger;
        MaxPlague = maxPlague;
        Payment = payment;
        LootItem = lootItem;
        StartItems = starti;
        StartItemsCounts = starticounts;
    }

}


public class SaveLoad : MonoBehaviour
{
    [HideInInspector]
    public bool SaveExists;

    public StartStart[] StartStarts;
    private MenuCustom _menu;
    private Constructor _constr;
    private DayAndNight _DayAndNight;

    private List<int> VaultIDs = new List<int>();
    private List<int> VaultCount = new List<int>();
    private List<string> Tile_names = new List<string>();

    private List<int> Tile_xpos = new List<int>();
    private List<int> Tile_ypos = new List<int>();


    private List<string> PitsOnBoard_names = new List<string>();
    private List<int> PitsOnBoard_xpos = new List<int>();
    private List<int> PitsOnBoard_ypos = new List<int>();


    private List<int> OB_IDs = new List<int>();
    private List<string> OB_names = new List<string>();
    private List<float> OB_xpos = new List<float>();
    private List<float> OB_ypos = new List<float>();
    private List<int> OB_horscale = new List<int>();
    private List<string> OB_SpawnPoint = new List<string>();

    private List<int> GenMap_GrowStates = new List<int>();

    private List<int> PreplacedObjects_GrowStates = new List<int>();

    private List<int> Dropped_IDs = new List<int>();
    private List<int> Dropped_Counts = new List<int>();
    private List<string> Dropped_names = new List<string>();
    private List<float> Dropped_xpos = new List<float>();
    private List<float> Dropped_ypos = new List<float>();

    public List<int> FloorStates = new List<int>();
    public int RNDSTART_Y, RNDSTART_X, RNDStablePos, ObjectPlacement_seed_Start;

    public Vector2Int RNDSHIFT;


    private List<string> Trash_names = new List<string>();
    private List<float> Trash_xpos = new List<float>();
    private List<float> Trash_ypos = new List<float>();
    private List<int> GrowStateList = new List<int>();

    [HideInInspector]
    public List<int> Unlocked_IDs = new List<int>();

    public int PreplacedObjects_Count, GenMap_GrowStates_Count, TOnBoard_Count, PitsOnBoard_Count, ConstructedStructures_Count, TRASHOnBoard_Count, ObjectsToDestroy_Count, DroppedItems_Count, Inventory_Count, UnlockedItems_Count;

    private bool Resetpol = false;

    private List<string> LocationsMenu = new List<string>();



#if UNITY_SWITCH

    public nn.account.UserHandle userHandle;
    public nn.account.Uid userId;
    [HideInInspector]
    public string mountName = "ScribedCSave";
    private string fileName = "ScribedCSaveData";

    private string filePath;
    private nn.fs.FileHandle fileHandle = new nn.fs.FileHandle();

    private const int saveDataVersion = 1;
    private const int saveDataSize = 131072;
    private const int MenusaveDataSize = 512;

#endif

#if UNITY_PS5|| UNITY_PS4
    
    [HideInInspector]
    public string mountName = "ScribedCSave";
    private string fileName = "ScribedCSaveData";


    private const int saveDataVersion = 1;
    [HideInInspector]
    public const int saveDataSize = 131072;
    private const int MenusaveDataSize = 512;

#endif
    public int SavingState, LoadingState;

    private string[] TileNames;

    public bool Saving { get; set; }
    public bool Loading { get; set; }

    public int SaveTimer { get; private set; }
    public int LoadTimer { get; private set; }

    private float SecondsTimer;
    public List<string> ACHNames = new List<string>();
    public List<string> ObjectsToDestroy = new List<string>();


    public string LastLocation { get; set; }
    
   
    private Player pl;
    private Inventory inv;
    private GenerateMap GenMap;

    private int slotsnum = 10;
    private GameObject SavingText;
    public string[] LocationsNames;
    public int[] CreateLocationOnStart;

    public List<int> BPConstructed = new List<int>();

    public int DayNumber;
    public float DayTime;
    public int _TutorialPhase { get; set; }

    public int CurrentCharacter { get; set; }
    public TileBase[] FloorBrush, KitchenBrush, TopBrush, GroundBrush, BaseBrush,GrassBrush, PitBrush;

    private int saveslotsnumber = 7;

    private AstarPath AP;
    private Tutorial _Tutorial;

#if UNITY_PS5
    [HideInInspector]
    public SonySaveDataMain PS_SaveMain;
#endif


    void Start()
    {




#if UNITY_SWITCH

        mountName = "ScribedCSave";
        fileName = "ScribedCSaveData";

        filePath = string.Format("{0}:/{1}", mountName, fileName);
#endif

        print("saveload start 0");
        if (GameObject.Find("PathFinding") != null)
            AP = GameObject.Find("PathFinding").GetComponent<AstarPath>();
        print("saveload start 01");
        if (GameObject.Find("TUTORIAL") != null) _Tutorial = GameObject.Find("TUTORIAL").GetComponent<Tutorial>();
        print("saveload start 02");
        TopBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Top Scafolds") };
        print("saveload start 03");
        KitchenBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/KitchenFloor") };
        
        FloorBrush = new TileBase[] {
            Resources.Load<RuleTile>("Brushes/Floor"),
            Resources.Load<RuleTile>("Brushes/Stone floor"),
            Resources.Load<RuleTile>("Brushes/Road"),
            Resources.Load<RuleTile>("Brushes/GrassRegular"),
            Resources.Load<RuleTile>("Brushes/Mud"),
            Resources.Load<RuleTile>("Brushes/Sand"),
            Resources.Load<RuleTile>("Brushes/Rock"),
            Resources.Load<RuleTile>("Brushes/Dark sand"),
        };

        print("saveload start 1");
        BaseBrush = new TileBase[1] {
            Resources.Load<TileBase>("Brushes/Ground")
            };

        GrassBrush = new TileBase[1] {
            Resources.Load<TileBase>("Brushes/GrassRegular")
            };

        PitBrush = new TileBase[2] {
            Resources.Load<TileBase>("Brushes/Pit"),
            Resources.Load<TileBase>("Brushes/WaterDitch")
            };


        print("saveload start 2");
        for (int i = 0; i < 100; i++)
            BPConstructed.Add(0);


        LocationsMenu.Add("StartMenu");
        LocationsMenu.Add("ChoosePlayer_Main");
        LocationsMenu.Add("ChoosePlayer_Tutorial");
        LocationsMenu.Add("Intro_Tutorial");
        LocationsMenu.Add("Intro");
        print("saveload start 3");

        LocationsNames = new string[6] { "Tutorial", "Main location", "Winter", "Spring", "Summer", "Autumn" };
        
        CreateLocationOnStart = new int[LocationsNames.Length];


        print("saveload start 4");
        if (GameObject.Find("Grid") != null)
        {
            if (GameObject.Find("Grid").GetComponent<GenerateMap>() != null)
            {
                if (GameObject.Find("Grid").GetComponent<GenerateMap>().isActiveAndEnabled)
                    GenMap = GameObject.Find("Grid").GetComponent<GenerateMap>();
            }

        }

        print("saveload start 5");
        if (GenMap == null)
        {
            if (GameObject.Find("FadeIn") != null)
                GameObject.Find("FadeIn").GetComponent<Animator>().SetBool("Start", true);

        }



        if (GameObject.Find("Player") != null)
        {
            
            pl = InitializeObjects.PL;
            inv = pl.inv;
          
        }


        TileNames = new string[3721];

        print("saveload start 6");
        _menu = GameObject.Find("Constructor").GetComponent<MenuCustom>();
        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        if(GameObject.Find("DayAndNight")!=null)
        _DayAndNight = GameObject.Find("DayAndNight").GetComponent<DayAndNight>();

        LoadingState = 0;
        print("saveload start 7");
#if UNITY_PS5 || UNITY_PS4

        PS_SaveMain = GetComponent<SonySaveDataMain>();

        if (!PS_SaveMain.StartLoad)
        {
            PS_SaveMain.StartLoad = true;
        }

        if (GameObject.Find("Player") != null)
        {
            pl.HP = 100;
            pl.MaxHP = 100;
        }
#else
        Load();
       _menu.LoadMenu();

        if (_DayAndNight != null)
        {
            print("pl.DayNight.Day " + DayTime);
            _DayAndNight.Day = DayTime;
        }

#endif










    }


    private void Update()
    {
        

        SecondsTimer -= Time.deltaTime;

        if (SecondsTimer < 0)
        {
            if (LoadTimer > 0)
                LoadTimer--;
            if (SaveTimer > 0)
                SaveTimer--;

            SecondsTimer = 1;

        }

        if (Saving)
        {
            Save(true);
        }

        if (Loading)
        {
            _menu.FirstStart = 1;

            //SceneManager.LoadScene(_menu.CurrentSlotLocations[_menu.CurrentSlotNumber]);

            _menu.TransitionToTheScene(_menu.CurrentSlotLocations[_menu.CurrentSlotNumber], false);
            
           /* if (!PS_SaveMain.StartLoad)
            {
                PS_SaveMain.StartLoad = true;
                Loading = false;
            }*/


        }

        if (GenMap == null)
        {
            if (!Resetpol)
            {
                ResetPolygonColliders();
                Resetpol = true;
            }
        }


    }

    public void Save(bool SaveAll)
    {
        if (_constr != null && SaveAll && !LocationsMenu.Contains(SceneManager.GetActiveScene().name))
        {
            _menu.FirstStart = 1;
        }

#if UNITY_SWITCH
        SWITCH_Save(SaveAll);
#endif

#if UNITY_STANDALONE
        UNITY_Save(SaveAll, _menu.CurrentSlotNumber);
#endif

#if UNITY_PS4||UNITY_PS5
        PS5_Save(SaveAll);

#endif
    }




    public void UNITY_Save(bool saveall, int slot)
    {

        string SlotName = "Slot" + slot;






        for (int i = 0; i < slotsnum; i++)
        {


            PlayerPrefs.SetString("CurrentSlotDates" + i, _menu.CurrentSlotDates[i]);

            PlayerPrefs.SetString("CurrentSlotLocations" + i, _menu.CurrentSlotLocations[i]);

            PlayerPrefs.SetString("CurrentSlotTimes" + i, _menu.CurrentSlotTimes[i]);

        }

        PlayerPrefs.SetInt("CurrentSlotNumber", _menu.CurrentSlotNumber);
        PlayerPrefs.SetInt("Language", _menu.Language);
        PlayerPrefs.SetInt("FirstLanguage", _menu.FirstLanguage);
        PlayerPrefs.SetInt("LastSystemLanguage", _menu.LastSystemLanguage);

        PlayerPrefs.SetFloat("MasterSliderValue", _menu.MasterSliderValue);
        PlayerPrefs.SetFloat("BGSliderValue", _menu.BGSliderValue);
        PlayerPrefs.SetFloat("ObjectsSliderValue", _menu.ObjectsSliderValue);

        PlayerPrefs.SetInt("HideUIValue", _menu.HideUIValue);

        PlayerPrefs.SetInt("DrawTutorial", _menu.DrawTutorial);
        PlayerPrefs.SetInt("FirstStart", _menu.FirstStart);




        PlayerPrefs.SetInt("SaveTimer", SaveTimer);
        PlayerPrefs.SetInt("LoadTimer", LoadTimer);


        LastLocation = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetString("LastLocation", LastLocation);



        if (_constr != null && saveall)
        {
            UNITY_SAVE_Vault();
            UNITY_SAVE_UnlockedItems(SlotName);

            UNITY_SAVE_Playervariables(SlotName);
            UNITY_SAVE_InventoryVaruables(SlotName);

            SaveTimer = (int)((_constr.TOnBoard.Count * 3 + _constr.OBOnBoard.Count * 5 + 11) / 32);

            for (int i = 0; i < LocationsNames.Length; i++)
            {
                if (LocationsNames[i] == SceneManager.GetActiveScene().name)
                {
                    UNITY_SAVE_CREATELOCATIONS(SlotName);

                    UNITY_SAVE_Location(SlotName);
                    UNITY_SAVE_Blueprints(SlotName);
                    UNITY_SAVE_ObjectsToDestroy(SlotName);

                    UNITY_SAVE_Tiles(SlotName);

                    UNITY_SAVE_DroppedItems(SlotName);

                    UNITY_SAVE_ConstructedStructures(SlotName);

                    UNITY_SAVE_PreplacedObjects(SlotName);
                }
            }

            Saving = false;
        }
    }

 
    void UNITY_SAVE_Playervariables(string SlotName)
    {
        // PlayerPrefs.SetInt("MaxHP" + SlotName, _constr.pl.MaxHP);
        PlayerPrefs.SetInt("HP" + SlotName, _constr.pl.HP);

        PlayerPrefs.SetInt("MaxHunger" + SlotName, _constr.pl.MaxHunger);
        PlayerPrefs.SetInt("Hunger" + SlotName, _constr.pl.Hunger);
        PlayerPrefs.SetInt("Plague" + SlotName, _constr.pl.Plague);
        PlayerPrefs.SetInt("MaxPlague" + SlotName, _constr.pl.MaxPlague);
        PlayerPrefs.SetInt("DayNumber" + SlotName, DayNumber);
        PlayerPrefs.SetFloat("DayTime" + SlotName, DayTime);

        if (_Tutorial != null) _TutorialPhase = _Tutorial.GetPhase();
        PlayerPrefs.SetInt("TutorialPhase" + SlotName, _TutorialPhase);

    }

    void UNITY_SAVE_InventoryVaruables(string SlotName)
    {
        Inventory_Count = inv.inventory.Count;
        PlayerPrefs.SetInt("Inventory_Count" + SlotName, Inventory_Count);

        

        for (int i = 0; i < Inventory_Count; i++)
        {
            int ii = -1;
            int iicount = 1;
            if (inv.inventory[i] != null && inv.inventory[i].itemID > -1)
                ii = inv.inventory[i].itemID;
            iicount = inv.inventory[i].Count;

            PlayerPrefs.SetInt("Item" + i + SlotName, ii);
            PlayerPrefs.SetInt("ItemCount" + i + SlotName, iicount);
        }
    }






    void UNITY_SAVE_Blueprints(string SlotName)
    {
        if (BPConstructed.Count==0)
        {
            for (int i = 0; i < 15; i++)
                BPConstructed.Add(0);
        }



        for (int i = 0; i < BPConstructed.Count; i++)
        {
            PlayerPrefs.SetInt("BPConstructed" + i + SlotName, BPConstructed[i]);
        }

    }

    void UNITY_SAVE_CREATELOCATIONS(string SlotName)
    {
        ThisLocationIsCreated();

        for (int i = 0; i < CreateLocationOnStart.Length; i++)
        {

            PlayerPrefs.SetInt("CreateLocationOnStart" + SlotName + i, CreateLocationOnStart[i]);
        }

    }


    void UNITY_SAVE_Location(string SlotName)
    {
        if (GenMap != null)
        {
            FloorStates = GenMap.FloorStates;

            RNDSTART_X = GenMap.RNDSTART_X;
            RNDSTART_Y = GenMap.RNDSTART_Y;

            RNDStablePos = GenMap.RND_Stable_Pos;

            ObjectPlacement_seed_Start = GenMap.ObjectPlacement_seed_Start;

        }



        PlayerPrefs.SetInt("RNDSTART_X" + SlotName, RNDSTART_X);
        PlayerPrefs.SetInt("RNDSTART_Y" + SlotName, RNDSTART_Y);
        PlayerPrefs.SetInt("RNDStablePos" + SlotName, RNDStablePos);

        PlayerPrefs.SetInt("ObjectPlacement_seed_Start" + SlotName, ObjectPlacement_seed_Start);


        PlayerPrefs.SetInt("FloorStatesCount" + SlotName, FloorStates.Count);

        if (GenMap != null)
            RNDSHIFT = GenMap.RNDSHIFT;

        PlayerPrefs.SetInt("RNDSHIF_X" + SlotName, RNDSHIFT.x);
        PlayerPrefs.SetInt("RNDSHIF_Y" + SlotName, RNDSHIFT.y);

        PlayerPrefs.SetInt("CurrentCharacter" + SlotName, CurrentCharacter);



    }
    void UNITY_SAVE_Vault()
    {
        VaultIDs = new List<int>();
        VaultCount = new List<int>();

        if (inv.VaultUI != null)
        {
            for (int i = 0; i < inv.VaultUI.Slots.Length; i++)
            {
                for (int ii = 0; ii < inv.VaultUI.Slots[i].items.Count; ii++)
                {
                    VaultIDs.Add(inv.VaultUI.Slots[i].items[ii].itemID);
                    VaultCount.Add(inv.VaultUI.Slots[i].items[ii].Count);

                    print("SAVE ADD pl.inv.VaultUI.Slots[i].items[ii].Count " + inv.VaultUI.Slots[i].items[ii].Count);
                }
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {

                VaultIDs.Add(-1);
                VaultCount.Add(0);


            }

        }

        PlayerPrefs.SetInt("VaultIDsCount", VaultIDs.Count);

        for (int i = 0; i < VaultIDs.Count; i++)
        {

            PlayerPrefs.SetInt("VaultID" + i, VaultIDs[i]);
            PlayerPrefs.SetInt("VaultCounts" + i, VaultCount[i]);
            print("SAVE VaultCounts " + VaultCount[i]);
        }

    }

    void UNITY_SAVE_ObjectsToDestroy(string SlotName)
    {

        ObjectsToDestroy_Count = ObjectsToDestroy.Count;
        if (_menu.FirstStart == 0) ObjectsToDestroy_Count = 0;

        PlayerPrefs.SetInt("ObjectsToDestroy_Count" + SlotName, ObjectsToDestroy_Count);


        for (int i = 0; i < ObjectsToDestroy_Count; i++)
        {
            PlayerPrefs.SetString("ObjectsToDestroy NAME" + i + SlotName, ObjectsToDestroy[i]);

        }

    }
    void UNITY_SAVE_Tiles(string SlotName)
    {

        if (_constr.TOnBoard != null)
        {
            TOnBoard_Count = _constr.TOnBoard.Count;
        }
        else TOnBoard_Count = 0;


        PlayerPrefs.SetInt("TOnBoard_Count" + SlotName, TOnBoard_Count);

        for (int i = 0; i < _constr.TOnBoard.Count; i++)
        {
            if (_constr.TOnBoard[i] != null)
            {
                if (_constr.TOnBoard[i].Name != null)
                    PlayerPrefs.SetString("TOnBoardName" + i + SlotName, _constr.TOnBoard[i].Name);
                else PlayerPrefs.SetString("TOnBoardName" + i + SlotName, "");

                PlayerPrefs.SetInt("TOnBoardxPOS" + i + SlotName, _constr.TOnBoard[i].xPOS);
                PlayerPrefs.SetInt("TOnBoardyPOS" + i + SlotName, _constr.TOnBoard[i].yPOS);

            }
            else
            {

                PlayerPrefs.SetString("TOnBoardName" + i + SlotName, "");
                PlayerPrefs.SetInt("TOnBoardxPOS" + i + SlotName, 0);
                PlayerPrefs.SetInt("TOnBoardyPOS" + i + SlotName, 0);
            }
        }

        /*
        if (_constr.PitsOnBoard != null)
        {
            PitsOnBoard_Count = _constr.PitsOnBoard.Count;
        }
        else PitsOnBoard_Count = 0;


        PlayerPrefs.SetInt("PitsOnBoard_Count" + SlotName, PitsOnBoard_Count);

        for (int i = 0; i < _constr.PitsOnBoard.Count; i++)
        {
            if (_constr.PitsOnBoard[i] != null)
            {
                if (_constr.PitsOnBoard[i].Name != null)
                    PlayerPrefs.SetString("PitsOnBoardName" + i + SlotName, _constr.PitsOnBoard[i].Name);
                else PlayerPrefs.SetString("PitsOnBoardName" + i + SlotName, "");

                PlayerPrefs.SetInt("PitsOnBoardxPOS" + i + SlotName, _constr.PitsOnBoard[i].xPOS);
                PlayerPrefs.SetInt("PitsOnBoardyPOS" + i + SlotName, _constr.PitsOnBoard[i].yPOS);

            }
            else
            {

                PlayerPrefs.SetString("PitsOnBoardName" + i + SlotName, "");
                PlayerPrefs.SetInt("PitsOnBoardxPOS" + i + SlotName, 0);
                PlayerPrefs.SetInt("PitsOnBoardyPOS" + i + SlotName, 0);
            }
        }*/


    }
    void UNITY_SAVE_UnlockedItems(string SlotName)
    {
        PlayerPrefs.SetInt("UnlockedItems_Count" + SlotName, UnlockedItems_Count);

        for (int i = 0; i < UnlockedItems_Count; i++)
        {

            PlayerPrefs.SetInt("UnlockedItems" + i + SlotName, Unlocked_IDs[i]);
        }
    }



    void UNITY_SAVE_DroppedItems(string SlotName)
    {
        if (_constr.DroppedItems != null)
        {
            DroppedItems_Count = _constr.DroppedItems.Count;
        }
        else DroppedItems_Count = 0;

        PlayerPrefs.SetInt("DroppedItems_Count" + SlotName, DroppedItems_Count);



        for (int i = 0; i < DroppedItems_Count; i++)
        {
            if (_constr.DroppedItems[i] != null)
            {
                if (_constr.DroppedItems[i].GetComponent<GetItem>() != null)
                {
                    PlayerPrefs.SetInt("DroppedItems ID" + i + SlotName, _constr.DroppedItems[i].GetComponent<GetItem>().item[0]);
                    PlayerPrefs.SetInt("DroppedItems Count" + i + SlotName, _constr.DroppedItems[i].GetComponent<GetItem>().itemcount[0]);
                    PlayerPrefs.SetString("DroppedItems Name" + i + SlotName, _constr.DroppedItems[i].name);

                    PlayerPrefs.SetFloat("DroppedItems PosX" + i + SlotName, _constr.DroppedItems[i].transform.position.x);
                    PlayerPrefs.SetFloat("DroppedItems PosY" + i + SlotName, _constr.DroppedItems[i].transform.position.y);
                }
                else
                {
                    PlayerPrefs.SetInt("DroppedItems ID" + i + SlotName, 0);
                    PlayerPrefs.SetInt("DroppedItems Count" + i + SlotName, 0);
                    PlayerPrefs.SetString("DroppedItems Name" + i + SlotName, "");

                    PlayerPrefs.SetFloat("DroppedItems PosX" + i + SlotName, 0);
                    PlayerPrefs.SetFloat("DroppedItems PosY" + i + SlotName, 0);
                }


            }

        }
    }


    void UNITY_SAVE_PreplacedObjects(string SlotName)
    {
        PreplacedObjects_GrowStates = new List<int>();

        for (int i = 0; i < _constr.OBOnBoard.Count; i++)
        {
            if (GenMap != null)
            {

                if (!ConstructedStructures_Contains(_constr.OBOnBoard[i]))
                {
                    if (_constr.OBOnBoard[i].Object != null)
                    {

                        if (_constr.OBOnBoard[i].Stats != null && !GenMap.CreatedObjects.Contains(_constr.OBOnBoard[i].Object))
                            PreplacedObjects_GrowStates.Add(_constr.OBOnBoard[i].Stats.CurrentGrowState);
                        else PreplacedObjects_GrowStates.Add(0);

                    }
                    else PreplacedObjects_GrowStates.Add(0);
                }



            }
            else
            {
                if (_constr.OBOnBoard[i].Object != null)
                {
                    if (_constr.OBOnBoard[i].Stats != null)
                        PreplacedObjects_GrowStates.Add(_constr.OBOnBoard[i].Stats.CurrentGrowState);
                    else PreplacedObjects_GrowStates.Add(0);

                }
                else PreplacedObjects_GrowStates.Add(0);

            }


        }

        if (PreplacedObjects_GrowStates != null)
        {
            PreplacedObjects_Count = PreplacedObjects_GrowStates.Count;
        }
        else PreplacedObjects_Count = 0;


        PlayerPrefs.SetInt("PreplacedObjects_Count" + SlotName, PreplacedObjects_Count);

        for (int i = 0; i < PreplacedObjects_GrowStates.Count; i++)
        {
            PlayerPrefs.SetInt("PreplacedObjects_GrowStates" + i + SlotName, PreplacedObjects_GrowStates[i]);
        }


    }

    void UNITY_LOAD_PreplacedObjects(string SlotName)
    {
        PreplacedObjects_Count = PlayerPrefs.GetInt("PreplacedObjects_Count" + SlotName);



        if (PreplacedObjects_Count > 0)
        {
            for (int i = 0; i < PreplacedObjects_Count; i++)
            {
                int grow = PlayerPrefs.GetInt("PreplacedObjects_GrowStates" + i + SlotName);

                PreplacedObjects_GrowStates.Add(grow);


            }
        }

    }



    void UNITY_SAVE_ConstructedStructures(string SlotName)
    {
        if (_constr.ConstructedStructures != null)
        {
            ConstructedStructures_Count = _constr.ConstructedStructures.Count;
        }
        else ConstructedStructures_Count = 0;


        PlayerPrefs.SetInt("ConstructedStructures_Count" + SlotName, ConstructedStructures_Count);

        if (GenMap != null)
        {
            if (GenMap.CreatedObjects != null)
            {
                GenMap_GrowStates_Count = GenMap.CreatedObjects.Count;
            }
            else GenMap_GrowStates_Count = 0;
        }
        GenMap_GrowStates_Count = 0;



        PlayerPrefs.SetInt("GenMap_GrowStates_Count" + SlotName, GenMap_GrowStates_Count);


        if (GenMap != null)
        {
            for (int i = 0; i < GenMap.CreatedObjects.Count; i++)
            {
                int Grow = 0;

                if (GenMap.CreatedObjects[i] != null && GenMap.CreatedObjects[i].GetComponent<StatsControll>() != null)
                {
                    Grow = GenMap.CreatedObjects[i].GetComponent<StatsControll>().CurrentGrowState;
                }

                PlayerPrefs.SetInt("GenMap_Grow" + i + SlotName, Grow);
            }

        }

        if (_constr.ConstructedStructures == null) return;






        for (int i = 0; i < _constr.ConstructedStructures.Count; i++)
        {
            int ID = -1;
            float xwright = 0;
            float ywright = 0;
            int hor = 1;
            string ConstructedStructuresName = "";
            string Spawnpointname = "";
            int growrate = 0;
            int CurrentGrowState = 0;


            if (_constr.ConstructedStructures[i].Object != null)
            {
                if (_constr.ConstructedStructures[i].Stats != null)
                    CurrentGrowState = _constr.ConstructedStructures[i].Stats.CurrentGrowState;

            }

            if (_constr.ConstructedStructures[i] != null)
            {
                ID = _constr.ConstructedStructures[i].ID;

                if (_constr.ConstructedStructures[i].Object != null)
                {
                    if (_constr.ConstructedStructures[i].Object.GetComponent<StatsControll>() != null)
                    {
                        Spawnpointname = _constr.ConstructedStructures[i].Object.GetComponent<StatsControll>().SpawnPointName;
                    }
                }

                if (_constr.ConstructedStructures[i].Name != null)
                {
                    ConstructedStructuresName = _constr.ConstructedStructures[i].Name;

                }

                if (_constr.ConstructedStructures[i].Place != null)
                {
                    if (_constr.ConstructedStructures[i].Object != null)
                    {
                        if (_constr.ConstructedStructures[i].Object.tag != "Pers")
                        {
                            if (_constr.ConstructedStructures[i].Object.transform.parent == null)
                            {
                                _constr.ConstructedStructures[i].Place = _constr.ConstructedStructures[i].Object.transform.position;

                                xwright = _constr.ConstructedStructures[i].Place.x;
                                ywright = _constr.ConstructedStructures[i].Place.y;
                            }
                            else
                            {
                                xwright = _constr.ConstructedStructures[i].Object.transform.parent.position.x;
                                ywright = _constr.ConstructedStructures[i].Object.transform.parent.position.y;

                            }
                        }
                        else
                        {
                            xwright = _constr.ConstructedStructures[i].Object.transform.position.x;
                            ywright = _constr.ConstructedStructures[i].Object.transform.position.y;


                        }

                        hor = (int)_constr.ConstructedStructures[i].Object.transform.localScale.x;



                    }
                }


            }

            print("SAVE POS" + new Vector2(xwright, ywright));



            PlayerPrefs.SetInt("OB_IDs" + i + SlotName, ID);
            PlayerPrefs.SetString("ConstructedStructures_Name" + i + SlotName, ConstructedStructuresName);
            PlayerPrefs.SetFloat("xwright" + i + SlotName, xwright);
            PlayerPrefs.SetFloat("ywright" + i + SlotName, ywright);
            PlayerPrefs.SetInt("hor" + i + SlotName, hor);
            PlayerPrefs.SetString("OB_SpawnPoint" + i + SlotName, Spawnpointname);

            PlayerPrefs.SetInt("FieldObjects CurrentGrowState" + i + SlotName, CurrentGrowState);




        }

    }



    void PS5_Save(bool SaveAll)
    {

        // Write only one save segment  - alldata / 7
#if UNITY_PS4 || UNITY_PS5
        byte[] data;
        byte[] MenuSavedata;

        long thisDatasizeStart = saveDataSize * _menu.CurrentSlotNumber + MenusaveDataSize;
        // long thisDatasizeStart = MenusaveDataSize;
        int AllDatasize = saveDataSize * 7 + MenusaveDataSize;


        using (MemoryStream stream = new MemoryStream(MenusaveDataSize))
        {

            BinaryWriter writer = new BinaryWriter(stream);




            for (int i = 0; i < saveslotsnumber; i++)
            {

                writer.Write(_menu.CurrentSlotDates[i]);
                writer.Write(_menu.CurrentSlotLocations[i]);
                writer.Write(_menu.CurrentSlotTimes[i]);
            }



            writer.Write(_menu.CurrentSlotNumber);


            writer.Write(_menu.Language);
            writer.Write(_menu.FirstLanguage);

            if (Utility.systemLanguage.ToString().Contains("ENGLISH"))
                _menu.LastSystemLanguage = 0;
            if (Utility.systemLanguage.ToString().Contains("UKRAINIAN") || Utility.systemLanguage.ToString().Contains("RUSSIAN"))
                _menu.LastSystemLanguage = 1;
            if (Utility.systemLanguage.ToString().Contains("JAPANESE"))
                _menu.LastSystemLanguage = 2;


            writer.Write(_menu.LastSystemLanguage);
            
            writer.Write((double)_menu.MasterSliderValue);
            writer.Write((double)_menu.BGSliderValue);
            writer.Write((double)_menu.ObjectsSliderValue);
            writer.Write(_menu.HideUIValue);

            writer.Write(_menu.DrawTutorial);
            writer.Write(_menu.FirstStart);


            writer.Write(SaveTimer);
            writer.Write(LoadTimer);




            stream.Close();
            MenuSavedata = stream.GetBuffer();
            PS_SaveMain.MenuSavedata = MenuSavedata;
        }

            using (MemoryStream stream2 = new MemoryStream(saveDataSize))
            {
            //stream2.Position = _menu.CurrentSlotNumber * saveDataSize;

            BinaryWriter writer = new BinaryWriter(stream2);


                if (_constr != null && SaveAll)
                {
                    SWITCH_SAVE_Vault(ref writer);
                    SWITCH_SAVE_UnlockedItems(ref writer);

                    SWITCH_SAVE_PlayerVariables(ref writer);
                    SWITCH_Save_InventoryVariables(ref writer);

                    SaveTimer = (int)((_constr.TOnBoard.Count * 3 + _constr.OBOnBoard.Count * 5 + 11) / 32);

                    for (int i = 0; i < LocationsNames.Length; i++)
                    {
                        if (LocationsNames[i] == SceneManager.GetActiveScene().name)
                        {
                            SWITCH_SAVE_CREATELOCATIONS(ref writer);

                            SWITCH_SAVE_GenMap(ref writer);

                            SWITCH_SAVE_Blueprints(ref writer);
                            SWITCH_SAVE_ObjectsToDestroy(ref writer);

                            SWITCH_SAVE_Tiles(ref writer);

                            SWITCH_SAVE_DroppedItems(ref writer);

                            SWITCH_SAVE_ConstructedStructures(ref writer);

                            SWITCH_SAVE_PreplacedObjects(ref writer);

                           // SWITCH_SAVE_PIT_Tiles(ref writer);
                        }
                    }

                    Saving = false;
                }


                stream2.Close();
                data = stream2.GetBuffer();

                PS_SaveMain.Largedata = data;
               PS_SaveMain.SaveAll = SaveAll;
            }
        


        PS_SaveMain.StartAutoSave();

#endif

        Saving = false;

    }


   




    void SWITCH_Save(bool SaveAll)
    {
#if UNITY_SWITCH
        byte[] data;
        byte[] MenuSavedata;

         long thisDatasizeStart = saveDataSize * _menu.CurrentSlotNumber + MenusaveDataSize;
       // long thisDatasizeStart = MenusaveDataSize;
        int AllDatasize = saveDataSize*7 + MenusaveDataSize;


        using (MemoryStream stream = new MemoryStream(MenusaveDataSize))
        {
        
            BinaryWriter writer = new BinaryWriter(stream);
          
            
           

            for (int i = 0; i < saveslotsnumber; i++)
            {
              
                writer.Write(_menu.CurrentSlotDates[i]);
                writer.Write(_menu.CurrentSlotLocations[i]);
                writer.Write(_menu.CurrentSlotTimes[i]);
            }



            writer.Write(_menu.CurrentSlotNumber);


            writer.Write(_menu.Language);
            writer.Write(_menu.FirstLanguage);
        
            writer.Write((double)_menu.MasterSliderValue);
            writer.Write((double)_menu.BGSliderValue);
            writer.Write((double)_menu.ObjectsSliderValue);
            writer.Write(_menu.HideUIValue);

            writer.Write(_menu.DrawTutorial);
            writer.Write(_menu.FirstStart);

     
            writer.Write(SaveTimer);
            writer.Write(LoadTimer);
       

    

            stream.Close();
            MenuSavedata = stream.GetBuffer();
        }


        using (MemoryStream stream2 = new MemoryStream(saveDataSize))
        {
            BinaryWriter writer = new BinaryWriter(stream2);


            if (_constr != null && SaveAll)
            {
                SWITCH_SAVE_Vault(ref writer);
                SWITCH_SAVE_UnlockedItems(ref writer);

                SWITCH_SAVE_PlayerVariables(ref writer);
                SWITCH_Save_InventoryVariables(ref writer);
               
                SaveTimer = (int)((_constr.TOnBoard.Count * 3 + _constr.OBOnBoard.Count * 5 + 11) / 32);

                for (int i = 0; i < LocationsNames.Length; i++)
                {
                    if (LocationsNames[i] == SceneManager.GetActiveScene().name)
                    {
                        SWITCH_SAVE_CREATELOCATIONS(ref writer);

                        SWITCH_SAVE_GenMap(ref writer);
                    
                        SWITCH_SAVE_Blueprints(ref writer);
                        SWITCH_SAVE_ObjectsToDestroy(ref writer);

                        SWITCH_SAVE_Tiles(ref writer);
                        
                        SWITCH_SAVE_DroppedItems(ref writer);
                        
                        SWITCH_SAVE_ConstructedStructures(ref writer);

                        SWITCH_SAVE_PreplacedObjects(ref writer);

                        //SWITCH_SAVE_PIT_Tiles(ref writer);
                    }
                }

                Saving = false;
            }

            
            stream2.Close();
            data = stream2.GetBuffer();
            

        }



        // Nintendo Switch Guideline 0080
        UnityEngine.Switch.Notification.EnterExitRequestHandlingSection();

        /*
        nn.Result result = nn.fs.File.Delete(filePath);
        if (!nn.fs.FileSystem.ResultPathNotFound.Includes(result))
        {
            result.abortUnlessSuccess();
        }

        result = nn.fs.File.Create(filePath, thisDatasize);
        result.abortUnlessSuccess();*/

        nn.Result result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Write);
        //result.abortUnlessSuccess();

        if (!result.IsSuccess())
        {
            result = nn.fs.File.Create(filePath, AllDatasize);
            result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Write);
        }

        long CurrentSize = 0;
        nn.Result resultsize = nn.fs.File.GetSize(ref CurrentSize, fileHandle);

        if (CurrentSize < AllDatasize)
        {
            result = nn.fs.File.SetSize(fileHandle, AllDatasize);
        }
        if (CurrentSize > AllDatasize)
        {
            result = nn.fs.File.SetSize(fileHandle, CurrentSize);
        }

        result = nn.fs.File.Write(fileHandle, 0, MenuSavedata, MenuSavedata.LongLength, nn.fs.WriteOption.Flush);
        result.abortUnlessSuccess();

        if (_constr != null && SaveAll)
        {
            result = nn.fs.File.Write(fileHandle, thisDatasizeStart, data, data.LongLength, nn.fs.WriteOption.Flush);
            result.abortUnlessSuccess();
        }

        nn.fs.File.Close(fileHandle);
        result = nn.fs.FileSystem.Commit(mountName);
        result.abortUnlessSuccess();

        UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection();

#endif

        // Debug.Log("SaveListCounts: TILE DATA END: " + SaveEnd);

        Saving = false;
    }

    void SWITCH_Save_InventoryVariables(ref BinaryWriter writer)
    {
        if (inv.inventory != null)
            Inventory_Count = inv.inventory.Count;
        else Inventory_Count = 0;

        writer.Write(Inventory_Count);

        for (int i = 0; i < Inventory_Count; i++)
        {
            writer.Write(inv.inventory[i].itemID);
            writer.Write(inv.inventory[i].Count);

        }

    }



    void SWITCH_SAVE_PlayerVariables(ref BinaryWriter writer)
    {
   
        
        writer.Write(_constr.pl.HP);

        writer.Write(_constr.pl.MaxHunger);
        writer.Write(_constr.pl.Hunger);
        writer.Write(_constr.pl.Plague);
        writer.Write(_constr.pl.MaxPlague);
        writer.Write(DayNumber);
        writer.Write((double)DayTime);

        if (_Tutorial != null) _TutorialPhase = _Tutorial.GetPhase();
        writer.Write(_TutorialPhase);

       
    }




    void SWITCH_SAVE_Blueprints(ref BinaryWriter writer)
    {

        if (BPConstructed.Count == 0)
        {
            for (int i = 0; i < 15; i++)
                BPConstructed.Add(0);
        }

        for (int i = 0; i < BPConstructed.Count; i++)
        {

            writer.Write(BPConstructed[i]);
        }

    }

    void SWITCH_SAVE_CREATELOCATIONS(ref BinaryWriter writer)
    {
        ThisLocationIsCreated();
        
        for (int i = 0; i < CreateLocationOnStart.Length; i++)
        {
            writer.Write(CreateLocationOnStart[i]);
        }

    }


    void SWITCH_SAVE_GenMap(ref BinaryWriter writer)
    {
        if (GenMap != null)
        {
            FloorStates = GenMap.FloorStates;

            RNDSTART_X = GenMap.RNDSTART_X;
            RNDSTART_Y = GenMap.RNDSTART_Y;

            RNDStablePos = GenMap.RND_Stable_Pos;

            ObjectPlacement_seed_Start = GenMap.ObjectPlacement_seed_Start;
        }




        LastLocation = SceneManager.GetActiveScene().name;


        writer.Write(LastLocation);

        writer.Write(FloorStates.Count);

        writer.Write(RNDSTART_X);
        writer.Write(RNDSTART_Y);
        writer.Write(RNDStablePos);
        writer.Write(ObjectPlacement_seed_Start);

        if (GenMap != null)
            RNDSHIFT = GenMap.RNDSHIFT;

        writer.Write(RNDSHIFT.x);
        writer.Write(RNDSHIFT.y);

        writer.Write(CurrentCharacter);


    }




    void SWITCH_SAVE_Vault(ref BinaryWriter writer)
    {
        VaultIDs = new List<int>();
        VaultCount = new List<int>();

        if (inv.VaultUI != null)
        {
            for (int i = 0; i < inv.VaultUI.Slots.Length; i++)
            {
                for (int ii = 0; ii < inv.VaultUI.Slots[i].items.Count; ii++)
                {
                    VaultIDs.Add(inv.VaultUI.Slots[i].items[ii].itemID);
                    VaultCount.Add(inv.VaultUI.Slots[i].items[ii].Count);

                }
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {

                VaultIDs.Add(-1);
                VaultCount.Add(0);


            }

        }

        writer.Write(VaultIDs.Count);

        for (int i = 0; i < VaultIDs.Count; i++)
        {
            writer.Write(VaultIDs[i]);
            writer.Write(VaultCount[i]);
        }

    }

    void SWITCH_SAVE_ObjectsToDestroy(ref BinaryWriter writer)
    {

        ObjectsToDestroy_Count = ObjectsToDestroy.Count;
        if (_menu.FirstStart == 0) ObjectsToDestroy_Count = 0;

        writer.Write(ObjectsToDestroy_Count);

        for (int i = 0; i < ObjectsToDestroy_Count; i++)
        {
            writer.Write(ObjectsToDestroy[i]);
        }

    }


    void SWITCH_SAVE_Tiles(ref BinaryWriter writer)
    {

        if (_constr.TOnBoard != null)
        {
            TOnBoard_Count = _constr.TOnBoard.Count;
        }
        else TOnBoard_Count = 0;


        writer.Write(TOnBoard_Count);

        for (int i = 0; i < TOnBoard_Count; i++)
        {
            string bname = "";
            int xPOS = 0;
            int yPOS = 0;


            if (_constr.TOnBoard[i] != null)
            {

                if (_constr.TOnBoard[i].Name != null)
                    bname = _constr.TOnBoard[i].Name;

                xPOS = _constr.TOnBoard[i].xPOS;
                yPOS = _constr.TOnBoard[i].yPOS;

            }

            writer.Write(bname);
            writer.Write(xPOS);
            writer.Write(yPOS);

        }

        
        
    }

    void SWITCH_SAVE_PIT_Tiles(ref BinaryWriter writer)
    {

       /*

        if (_constr.PitsOnBoard != null)
        {
            PitsOnBoard_Count = _constr.PitsOnBoard.Count;
        }
        else PitsOnBoard_Count = 0;


        writer.Write(PitsOnBoard_Count);

        for (int i = 0; i < PitsOnBoard_Count; i++)
        {
            string bname = "";
            int xPOS = 0;
            int yPOS = 0;


            if (_constr.PitsOnBoard[i] != null)
            {

                if (_constr.PitsOnBoard[i].Name != null)
                    bname = _constr.PitsOnBoard[i].Name;

                xPOS = _constr.PitsOnBoard[i].xPOS;
                yPOS = _constr.PitsOnBoard[i].yPOS;

            }

            writer.Write(bname);
            writer.Write(xPOS);
            writer.Write(yPOS);

        }
       */
    }

    void SWITCH_SAVE_UnlockedItems(ref BinaryWriter writer)
    {
        UnlockedItems_Count = Unlocked_IDs.Count;

        writer.Write(UnlockedItems_Count);

        for (int i = 0; i < UnlockedItems_Count; i++)
        {
            int UnlockedItems_ID = -1;


            UnlockedItems_ID = Unlocked_IDs[i];


            writer.Write(UnlockedItems_ID);
        }
    }

    void SWITCH_SAVE_DroppedItems(ref BinaryWriter writer)
    {
        if (_constr.DroppedItems != null)
        {
            DroppedItems_Count = _constr.DroppedItems.Count;
        }
        else DroppedItems_Count = 0;

        writer.Write(DroppedItems_Count);


        for (int i = 0; i < DroppedItems_Count; i++)
        {
            int DroppedItems_ID = -1;
            int DroppedItems_Num = 0;
            string DroppedItems_Name = "";

            double DroppedItems_PosX = 0;
            double DroppedItems_PosY = 0;

            if (_constr.DroppedItems[i] != null)
            {
                if (_constr.DroppedItems[i].GetComponent<GetItem>() != null)
                {
                    DroppedItems_ID = _constr.DroppedItems[i].GetComponent<GetItem>().item[0];
                    DroppedItems_Num = _constr.DroppedItems[i].GetComponent<GetItem>().itemcount[0];
                    DroppedItems_Name = _constr.DroppedItems[i].name;

                    DroppedItems_PosX = (double)_constr.DroppedItems[i].transform.position.x;
                    DroppedItems_PosY = (double)_constr.DroppedItems[i].transform.position.y;
                }
                else
                {
                    DroppedItems_ID = 0;
                    DroppedItems_Num = 0;
                    DroppedItems_Name = "";

                    DroppedItems_PosX = 0;
                    DroppedItems_PosY = 0;

                }


            }

            writer.Write(DroppedItems_ID);
            writer.Write(DroppedItems_Num);
            writer.Write(DroppedItems_Name);
            writer.Write(DroppedItems_PosX);
            writer.Write(DroppedItems_PosY);


        }
    }

    bool ConstructedStructures_Contains(ObjectOnBoard ob)
    {
        bool tf = false;

        if (ob.Object != null)
        {
            for (int i = 0; i < _constr.ConstructedStructures.Count; i++)
            {
                if (_constr.ConstructedStructures[i].Object != null)
                {
                    if (ob.Object == _constr.ConstructedStructures[i].Object) tf = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < _constr.ConstructedStructures.Count; i++)
            {
                if (_constr.ConstructedStructures[i].Place == ob.Place && _constr.ConstructedStructures[i].ID == ob.ID && _constr.ConstructedStructures[i].Name == ob.Name)
                {
                    tf = true;
                }
            }

        }


        return tf;

    }
    void SWITCH_SAVE_PreplacedObjects(ref BinaryWriter writer)
    {


        PreplacedObjects_GrowStates = new List<int>();

        for (int i = 0; i < _constr.OBOnBoard.Count; i++)
        {
            if (GenMap != null)
            {



                if (!ConstructedStructures_Contains(_constr.OBOnBoard[i]))
                {
                    if (_constr.OBOnBoard[i].Object != null)
                    {
                        if (_constr.OBOnBoard[i].Object.GetComponent<StatsControll>() != null && !GenMap.CreatedObjects.Contains(_constr.OBOnBoard[i].Object))
                            PreplacedObjects_GrowStates.Add(_constr.OBOnBoard[i].Object.GetComponent<StatsControll>().CurrentGrowState);
                        else PreplacedObjects_GrowStates.Add(0);

                    }
                    else PreplacedObjects_GrowStates.Add(0);
                }



            }
            else
            {
                if (_constr.OBOnBoard[i].Object != null)
                {
                    if (_constr.OBOnBoard[i].Object.GetComponent<StatsControll>() != null)
                        PreplacedObjects_GrowStates.Add(_constr.OBOnBoard[i].Object.GetComponent<StatsControll>().CurrentGrowState);
                    else PreplacedObjects_GrowStates.Add(0);

                }
                else PreplacedObjects_GrowStates.Add(0);

            }


        }

        if (_constr.OBOnBoard != null)
        {
            PreplacedObjects_Count = PreplacedObjects_GrowStates.Count;
        }
        else PreplacedObjects_Count = 0;



        writer.Write(PreplacedObjects_Count);

        for (int i = 0; i < PreplacedObjects_Count; i++)
        {
            print("SAVE PreplacedObjects " + PreplacedObjects_GrowStates[i]);
            writer.Write(PreplacedObjects_GrowStates[i]);
        }


    }


    void SWITCH_SAVE_ConstructedStructures(ref BinaryWriter writer)
    {
        if (_constr.ConstructedStructures != null)
        {
            ConstructedStructures_Count = _constr.ConstructedStructures.Count;
        }
        else ConstructedStructures_Count = 0;

        writer.Write(ConstructedStructures_Count);

        if (GenMap != null)
        {
            if (GenMap.CreatedObjects != null)
            {
                GenMap_GrowStates_Count = GenMap.CreatedObjects.Count;
            }
            else GenMap_GrowStates_Count = 0;
        }

        writer.Write(GenMap_GrowStates_Count);

        if (GenMap != null)
        {
            for (int i = 0; i < GenMap.CreatedObjects.Count; i++)
            {
                if (GenMap.CreatedObjects[i] != null)
                {
                    if (GenMap.CreatedObjects[i].GetComponent<StatsControll>() != null)
                        GenMap_GrowStates.Add(GenMap.CreatedObjects[i].GetComponent<StatsControll>().CurrentGrowState);
                    else GenMap_GrowStates.Add(0);
                }
                else GenMap_GrowStates.Add(0);
            }
        }

        for (int i = 0; i < GenMap_GrowStates_Count; i++)
        {
            writer.Write(GenMap_GrowStates[i]);
        }



     
        for (int i = 0; i < ConstructedStructures_Count; i++)
        {
            int ID = -1;
            double xwright = 0;
            double ywright = 0;
            int hor = 1;
            string ConstructedStructuresName = "";
            string SpawnPointName = "";
            int CurrentGrowState = 0;





            if (_constr.ConstructedStructures[i] != null)
            {
                if (_constr.ConstructedStructures[i].Object != null)
                {
                    if (_constr.ConstructedStructures[i].Stats != null)
                        CurrentGrowState = _constr.ConstructedStructures[i].Stats.CurrentGrowState;

                    if (_constr.ConstructedStructures[i].Object.GetComponent<StatsControll>() != null)
                    {
                        SpawnPointName = _constr.ConstructedStructures[i].Object.GetComponent<StatsControll>().SpawnPointName;
                    }


                }

                ID = _constr.ConstructedStructures[i].ID;

                if (_constr.ConstructedStructures[i].Name != null)
                {
                    ConstructedStructuresName = _constr.ConstructedStructures[i].Name;
                }

                if (_constr.ConstructedStructures[i].Place != null)
                {
                    if (_constr.ConstructedStructures[i].Object != null)
                    {
                        if (_constr.ConstructedStructures[i].Object.tag != "Pers")
                        {
                            if (_constr.ConstructedStructures[i].Object.transform.parent == null)
                            {

                                _constr.ConstructedStructures[i].Place = _constr.ConstructedStructures[i].Object.transform.position;

                                xwright = _constr.ConstructedStructures[i].Place.x;
                                ywright = _constr.ConstructedStructures[i].Place.y;
                            }
                            else
                            {
                                xwright = _constr.ConstructedStructures[i].Object.transform.parent.position.x;
                                ywright = _constr.ConstructedStructures[i].Object.transform.parent.position.y;

                            }
                        }
                        else
                        {
                            xwright = _constr.ConstructedStructures[i].Object.transform.position.x;
                            ywright = _constr.ConstructedStructures[i].Object.transform.position.y;


                        }

                        hor = (int)_constr.ConstructedStructures[i].Object.transform.localScale.x;



                    }

                }


            }


            writer.Write(_constr.ConstructedStructures[i].ID);
            writer.Write(ConstructedStructuresName);
            writer.Write(xwright);
            writer.Write(ywright);
            writer.Write(hor);
            writer.Write(SpawnPointName);
            writer.Write(CurrentGrowState);


        }

    }





    public void Load()
    {
#if UNITY_SWITCH
        SWITCH_Load();
#endif

#if UNITY_STANDALONE
        UNITY_Load();
#endif

#if UNITY_PS4 || UNITY_PS5


        PS_SaveMain.StartAutoSaveLoad();
        Loading = false;
#endif

    }

    
    public void UNITY_Load()
    {


        UNITY_LOAD_MenuVariables();
        string SlotName = "Slot" + _menu.CurrentSlotNumber;
        UNITY_LoadVault();
        SetVault();
        UNITY_LOAD_UnlockedItems(SlotName);


        if (_constr == null)
        {

       

            Loading = false;

            return;

        }

        if (_menu.FirstStart != 0)
            UnLoadAll();


        UNITY_LOAD_PlayerVariables(SlotName);
        UNITY_LOAD_InventoryVariables(SlotName);

        for (int i = 0; i < LocationsNames.Length; i++)
        {
            bool newloc = true;


            if (LocationsNames[i] == SceneManager.GetActiveScene().name)
            {
                UNITY_LOAD_CREATELOCATIONS(SlotName);

                if (CreateLocationOnStart[i] > 0 && _menu.FirstStart != 0) newloc = false;


                UNITY_LOAD_Location(SlotName, newloc);




                if (!newloc)
                {
                    UNITY_LOAD_DestroyObjects(SlotName);

                    UNITY_LOAD_Blueprints(SlotName);
                    UNITY_LOAD_Tiles(SlotName);


                    UNITY_LOAD_DroppedItems(SlotName);
                    UNITY_LOAD_ConstructedStructures(SlotName);
                    UNITY_LOAD_PreplacedObjects(SlotName);




                    if (_menu.FirstStart > 0)
                    {

                        SetDroppedItems();
                        SetTiles();
                        SetObjectsOnBoard();
                        SetObjectsToDestroy();
                    }
                }
            }

        }
        
        LoadingState = 10;

        LoadTimer = (int)((_constr.TOnBoard.Count * 3 + _constr.OBOnBoard.Count * 5 + 11) / 32);



        Loading = false;
    }

    void UNITY_LOAD_CREATELOCATIONS(string SlotName)
    {
        CreateLocationOnStart = new int[LocationsNames.Length];

        for (int i = 0; i < LocationsNames.Length; i++)
        {

            CreateLocationOnStart[i] = PlayerPrefs.GetInt("CreateLocationOnStart" + SlotName + i);

        }

    }


    void UNITY_LOAD_MenuVariables()
    {
        _menu.CurrentSlotNumber = PlayerPrefs.GetInt("CurrentSlotNumber");

        for (int i = 0; i < slotsnum; i++)
        {


            _menu.CurrentSlotDates[i] = PlayerPrefs.GetString("CurrentSlotDates" + i);

            _menu.CurrentSlotLocations[i] = PlayerPrefs.GetString("CurrentSlotLocations" + i);

            _menu.CurrentSlotTimes[i] = PlayerPrefs.GetString("CurrentSlotTimes" + i);
        }



        _menu.Language = PlayerPrefs.GetInt("Language");
        _menu.FirstLanguage = PlayerPrefs.GetInt("FirstLanguage");
        _menu.LastSystemLanguage = PlayerPrefs.GetInt("LasSystemLanguage");


        _menu.MasterSliderValue = PlayerPrefs.GetFloat("MasterSliderValue");
        _menu.BGSliderValue = PlayerPrefs.GetFloat("BGSliderValue");
        _menu.ObjectsSliderValue = PlayerPrefs.GetFloat("ObjectsSliderValue");
        _menu.HideUIValue = PlayerPrefs.GetInt("HideUIValue");


        _menu.DrawTutorial = PlayerPrefs.GetInt("DrawTutorial");
        _menu.FirstStart = PlayerPrefs.GetInt("FirstStart");


        SaveTimer = PlayerPrefs.GetInt("SaveTimer");
        LoadTimer = PlayerPrefs.GetInt("LoadTimer");

    }


    void UNITY_LOAD_Location(string SlotName, bool CreateNewLocation)
    {
        LastLocation = PlayerPrefs.GetString("LastLocation");

     

        if (CreateNewLocation)
        {

            if (GenMap != null)
            {
                GenMap.CreateNewMap();
                print("GenMap.CreateNewMap 0");
            }

            return;
        }

        int FloorStatesCount = PlayerPrefs.GetInt("FloorStatesCount" + SlotName);



        if (GenMap != null)
        {

            if (FloorStatesCount <= 0)
            {

                GenMap.CreateNewMap();
            }
            else
            {
                GenMap.CleanMap();

                RNDSTART_X = PlayerPrefs.GetInt("RNDSTART_X" + SlotName);
                RNDSTART_Y = PlayerPrefs.GetInt("RNDSTART_Y" + SlotName);
                RNDStablePos = PlayerPrefs.GetInt("RNDStablePos" + SlotName);

                GenMap.ObjectPlacement_seed_Start = PlayerPrefs.GetInt("ObjectPlacement_seed_Start" + SlotName);
                GenMap.ObjectPlacement_seed = GenMap.ObjectPlacement_seed_Start;

                RNDSHIFT = new Vector2Int(
                PlayerPrefs.GetInt("RNDSHIF_X" + SlotName),
                PlayerPrefs.GetInt("RNDSHIF_Y" + SlotName));

                CurrentCharacter = PlayerPrefs.GetInt("CurrentCharacter" + SlotName);


                GenMap.RNDSHIFT = RNDSHIFT;
                GenMap.RND_Stable_Pos = RNDStablePos;

                GenMap.RNDSTART_X = RNDSTART_X;
                GenMap.RNDSTART_Y = RNDSTART_Y;


                GenMap.LoadMap();
                GenMap.CreateObjects = true;
            }




        }




    }
    void UNITY_LOAD_DestroyObjects(string SlotName)
    {
        ObjectsToDestroy_Count = PlayerPrefs.GetInt("ObjectsToDestroy_Count" + SlotName);


        if (ObjectsToDestroy_Count > 0)
        {
            for (int i = 0; i < ObjectsToDestroy_Count; i++)
            {
                string s = PlayerPrefs.GetString("ObjectsToDestroy NAME" + i + SlotName);
                ObjectsToDestroy.Add(s);



            }
        }

    }
    void SetVault()
    {

        int j = 0;
        if (pl == null) return;
        if (inv.VaultUI != null)
        {
            for (int i = 0; i < inv.VaultUI.Slots.Length; i++)
            {
                for (int ii = 0; ii < inv.VaultUI.Slots[i].items.Count; ii++)
                {
                    if (j < VaultIDs.Count)
                    {
                        if (VaultIDs[j] > -1)
                        {

                            inv.VaultUI.Slots[i].items[ii] = inv.DeepCopyItem(VaultIDs[j], VaultCount[j], inv.GetItemInDatabase(VaultIDs[j]).Durability);
                        }
                        j++;
                    }

                }
            }
        }

    }
    void UNITY_LoadVault()
    {

        int VaultIDsCount = PlayerPrefs.GetInt("VaultIDsCount");
        if (VaultIDsCount < 6)
        {
            for (int i = 0; i < VaultIDsCount; i++)
            {
                VaultIDs.Add(-1);
                VaultCount.Add(1);
            }
            VaultIDsCount = 6;
        }
        else
        {
            for (int i = 0; i < VaultIDsCount; i++)
            {
                VaultIDs.Add(PlayerPrefs.GetInt("VaultID" + i));
                VaultCount.Add(PlayerPrefs.GetInt("VaultCounts" + i));
            }
        }

    }


    void UNITY_LOAD_Blueprints(string SlotName)
    {

        for (int i = 0; i < BPConstructed.Count; i++)
        {
            BPConstructed[i] = PlayerPrefs.GetInt("BPConstructed" + i + SlotName);

        }
    }

    
    void UNITY_LOAD_InventoryVariables(string SlotName)
    {
        Inventory_Count = PlayerPrefs.GetInt("Inventory_Count" + SlotName);

        if (Inventory_Count > inv.slotX && inv.slotX > 0) Inventory_Count = inv.slotX;
        
   
        for (int i = 0; i < Inventory_Count; i++)
        {
            int ii = PlayerPrefs.GetInt("Item" + i + SlotName);
            int iicount = PlayerPrefs.GetInt("ItemCount" + i + SlotName);

            if (ii > -1 && inv.GetItemInDatabase(ii)!=null && _menu.FirstStart != 0)
            {
                if (iicount > 0)
                    inv.AddItemNOAUDIO_NOPickedNames(ii, iicount, inv.GetItemInDatabase(ii).Durability, new Vector2(99999, 1));
                else inv.AddItemNOAUDIO_NOPickedNames(ii, 1, inv.GetItemInDatabase(ii).Durability, new Vector2(99999, 1));

            }
            
        }
        
    }


    void UNITY_LOAD_PlayerVariables(string SlotName)
    {

        if (_menu.FirstStart == 1)
        {
            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;


            pl.HP = PlayerPrefs.GetInt("HP" + SlotName);

            if (pl.HP <= 0) pl.HP = pl.MaxHP;

            pl.MaxHunger = PlayerPrefs.GetInt("MaxHunger" + SlotName);
            pl.Hunger = PlayerPrefs.GetInt("Hunger" + SlotName);

            pl.Plague = PlayerPrefs.GetInt("Plague" + SlotName);
            pl.MaxPlague = PlayerPrefs.GetInt("MaxPlague" + SlotName);

            DayNumber = PlayerPrefs.GetInt("DayNumber" + SlotName);
            DayTime = PlayerPrefs.GetFloat("DayTime" + SlotName);


            _TutorialPhase = PlayerPrefs.GetInt("TutorialPhase" + SlotName);

            if (_Tutorial != null) _Tutorial.SetPhase(_TutorialPhase);

            pl.MaxStamina = StartStarts[CurrentCharacter].MaxStamina;
  

        }
        else
        {

            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;
          pl.HP = StartStarts[CurrentCharacter].MaxHP;

            pl.MaxHunger = StartStarts[CurrentCharacter].MaxHunger;
            pl.MaxStamina = StartStarts[CurrentCharacter].MaxStamina;

            pl.Hunger = 0;

            pl.MaxPlague = StartStarts[CurrentCharacter].MaxPlague;

            pl.Payment = StartStarts[CurrentCharacter].Payment;
            pl.LootItem = StartStarts[CurrentCharacter].LootItem;

            for (int i = 0; i < StartStarts[CurrentCharacter].StartItems.Length; i++)
               inv.AddItemNOAUDIO_NOPickedNames(StartStarts[CurrentCharacter].StartItems[i], StartStarts[CurrentCharacter].StartItemsCounts[i], inv.GetItemInDatabase(StartStarts[CurrentCharacter].StartItems[i]).Durability, new Vector2(99999, 99999));


          
            /* pl.TEST = true;
                if (pl.TEST)
                    inv.AddItemNOAUDIO_NOPickedNames(9, 99999, 99999, new Vector2(99999, 99999));
            */

            if (_Tutorial != null) _Tutorial.SetPhase(0);


        }

    }

    void SetObjectsToDestroy()
    {

        if (ObjectsToDestroy_Count <= 0) return;

        for (int i = 0; i < ObjectsToDestroy.Count; i++)
        {

            if (GameObject.Find(ObjectsToDestroy[i]) != null)
            {
                DestroyObject(GameObject.Find(ObjectsToDestroy[i]));


            }


        }

    }



    void SetDroppedItems()
    {
        print("DroppedItems_Count " + DroppedItems_Count);


        for (int i = 0; i < DroppedItems_Count; i++)
        {

            if (inv.GetItemInDatabase(Dropped_IDs[i]) != null)
            {


                GameObject n = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Item"));
                n.name = Dropped_names[i];

                print("DroppedItems CREATED " + Dropped_IDs[i]);

                n.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inv.GetItemInDatabase(Dropped_IDs[i]).itemNames[0]);
                n.GetComponent<SpriteRenderer>().sortingOrder = -900;

                n.GetComponent<GetItem>().item = new int[1] { Dropped_IDs[i] };
                n.GetComponent<GetItem>().itemcount = new int[1] { Dropped_Counts[i] };
                n.GetComponent<GetItem>().DontSetCounts = true;

                n.transform.position = new Vector3(Dropped_xpos[i], Dropped_ypos[i], 0);
                _constr.DroppedItems.Add(n);
            }


        }



    }



    void UNITY_LOAD_UnlockedItems(string SlotName)
    {
        UnlockedItems_Count = PlayerPrefs.GetInt("UnlockedItems_Count" + SlotName);

        if (UnlockedItems_Count > 0)
        {
            for (int i = 0; i < UnlockedItems_Count; i++)
            {
                Unlocked_IDs.Add(PlayerPrefs.GetInt("UnlockedItems" + i + SlotName));

                inv.GetItemInDatabase(PlayerPrefs.GetInt("UnlockedItems" + i + SlotName)).Locked = false;
            }
        }


    }

    void UNITY_LOAD_DroppedItems(string SlotName)
    {
        DroppedItems_Count = PlayerPrefs.GetInt("DroppedItems_Count" + SlotName);

        if (DroppedItems_Count > 0)
        {
            for (int i = 0; i < DroppedItems_Count; i++)
            {
                Dropped_IDs.Add(PlayerPrefs.GetInt("DroppedItems ID" + i + SlotName));

                Dropped_Counts.Add(PlayerPrefs.GetInt("DroppedItems Count" + i + SlotName));


                Dropped_names.Add(PlayerPrefs.GetString("DroppedItems Name" + i + SlotName));


                Dropped_xpos.Add(PlayerPrefs.GetFloat("DroppedItems PosX" + i + SlotName));

                Dropped_ypos.Add(PlayerPrefs.GetFloat("DroppedItems PosY" + i + SlotName));


            }
        }

    }
    void UNITY_LOAD_ConstructedStructures(string SlotName)
    {
        ConstructedStructures_Count = PlayerPrefs.GetInt("ConstructedStructures_Count" + SlotName);
        GenMap_GrowStates_Count = PlayerPrefs.GetInt("GenMap_GrowStates_Count" + SlotName);


        print("UNITY_LOAD_ConstructedStructures Count " + ConstructedStructures_Count);

        if (GenMap_GrowStates_Count > 0)
        {
            for (int i = 0; i < GenMap_GrowStates_Count; i++)
            {
                GenMap_GrowStates.Add(PlayerPrefs.GetInt("GenMap_Grow" + i + SlotName));


            }
        }

        if (ConstructedStructures_Count > 0)
        {
            for (int i = 0; i < ConstructedStructures_Count; i++)
            {
                OB_IDs.Add(PlayerPrefs.GetInt("OB_IDs" + i + SlotName));


                OB_names.Add(PlayerPrefs.GetString("ConstructedStructures_Name" + i + SlotName));


                OB_xpos.Add(PlayerPrefs.GetFloat("xwright" + i + SlotName));

                OB_ypos.Add(PlayerPrefs.GetFloat("ywright" + i + SlotName));

                OB_horscale.Add(PlayerPrefs.GetInt("hor" + i + SlotName));

                OB_SpawnPoint.Add(PlayerPrefs.GetString("OB_SpawnPoint" + i + SlotName));

                GrowStateList.Add(PlayerPrefs.GetInt("FieldObjects CurrentGrowState" + i + SlotName));
            }
        }



    }



    void UNITY_LOAD_Tiles(string SlotName)
    {

        TOnBoard_Count = PlayerPrefs.GetInt("TOnBoard_Count" + SlotName);
        if (TOnBoard_Count > 0)

        {

            print("TOnBoard_Count: " + TOnBoard_Count);


            for (int i = 0; i < TOnBoard_Count; i++)
            {
                Tile_names.Add(PlayerPrefs.GetString("TOnBoardName" + i + SlotName));
                //  Debug.Log("LOADTILE NAME: " + Tile_names[i]);
                Tile_xpos.Add(PlayerPrefs.GetInt("TOnBoardxPOS" + i + SlotName));
                //   Debug.Log("LOADTILE XPOS: " + Tile_xpos[i]);
                Tile_ypos.Add(PlayerPrefs.GetInt("TOnBoardyPOS" + i + SlotName));
                //  Debug.Log("LOADTILE YPOS: " + Tile_ypos[i]);
            }
        }


        PitsOnBoard_Count = PlayerPrefs.GetInt("PitsOnBoard_Count" + SlotName);
        if (PitsOnBoard_Count > 0)

        {

           

            for (int i = 0; i < PitsOnBoard_Count; i++)
            {
                PitsOnBoard_names.Add(PlayerPrefs.GetString("PitsOnBoardName" + i + SlotName));
                //  Debug.Log("LOADTILE NAME: " + Tile_names[i]);
                PitsOnBoard_xpos.Add(PlayerPrefs.GetInt("PitsOnBoardxPOS" + i + SlotName));
                //   Debug.Log("LOADTILE XPOS: " + Tile_xpos[i]);
                PitsOnBoard_ypos.Add(PlayerPrefs.GetInt("PitsOnBoardyPOS" + i + SlotName));
                //  Debug.Log("LOADTILE YPOS: " + Tile_ypos[i]);
            }
        }

        
        // One Stable Tile To build another tiles
        if (TOnBoard_Count == 0)
        {

            _constr.Tile.SetTile(new Vector3Int(0, 0, 0), FloorBrush[0]);
            _constr.Floors++;

        }
    }





    void SetObjectsOnBoard()
    {
        if (_menu.FirstStart == 0) return;
 


        for (int i = 0; i < ConstructedStructures_Count; i++)
        {
            GameObject n = null;

            if (OB_names[i] != "")
            {
                GameObject ParentOB = null;
                CheckParent(ref ParentOB, i);



                if (OB_IDs[i] > -1)
                {
                    CreateGroundObject(ParentOB, ref n, i);
                }

                if (n != null)
                {
                    SetupGroundObject(ref n, i);
                    _constr.OBOnBoard.Add(new ObjectOnBoard(OB_IDs[i], new Vector2(OB_xpos[i], OB_ypos[i]), OB_names[i], n, n.GetComponent<StatsControll>(), n.GetComponent<PubObject>()));
                    _constr.ConstructedStructures.Add(new ObjectOnBoard(OB_IDs[i], new Vector2(OB_xpos[i], OB_ypos[i]), OB_names[i], n, n.GetComponent<StatsControll>(), n.GetComponent<PubObject>()));

                   
                }


            }


        }

        for (int i = 0; i < ConstructedStructures_Count; i++)
        {
            if (ConstructedStructures_Count <= _constr.ConstructedStructures.Count)
            {
                if (_constr.ConstructedStructures[i].Object != null)
                {
                    if (_constr.ConstructedStructures[i].Stats != null)
                        _constr.ConstructedStructures[i].Stats.CurrentGrowState = GrowStateList[i];
                }
            }
            // else print("OOPSY...WE HAVE MORE TO LOAD THAT IT IS ON THE FIELD. PROBLEM!!!");
        }



    }




    void SetupGroundObject(ref GameObject n, int i)
    {

        if (n.GetComponent<StatsControll>() != null)
        {
            n.GetComponent<StatsControll>().enabled = true;
        }
        if (n.GetComponent<MovementControll>() != null)
            n.GetComponent<MovementControll>().enabled = true;

        if (n.GetComponent<Enemies>() != null)
        {
            n.GetComponent<Enemies>().enabled = true;
        }

        if (n.GetComponent<BoxCollider2D>() != null)
        {
            n.GetComponent<BoxCollider2D>().enabled = true;
        }


        if (n.GetComponent<PubObject>() != null)
        {


            PubObject PO = n.GetComponent<PubObject>();

            _constr.Floors += PO.kitchenfloors;
            _constr.Floors += PO.floors;
            _constr.Grounds += PO.ground;
            _constr.Walls += PO.wall;

        
            _constr.AllTables += PO.tables;
            if (PO.tag == "Pers"  ) _constr.AllPeople++;

           


        }


        for (int c = 0; c < n.transform.childCount; c++)
        {
            if (n.transform.GetChild(c).GetComponent<SpriteRenderer>() != null)
                n.transform.GetChild(c).GetComponent<SpriteRenderer>().sortingOrder = ((int)(n.transform.position.y * 100) * -1 + c * 2);

        }



    }



    void CreateGroundObject(GameObject ParentOB, ref GameObject n, int i)
    {

        if (inv.GetItemInDatabase(OB_IDs[i]).ObjectPrefs == null)
        {
            return;
        }
       
        if (ParentOB == null)
        {
          
            n = Instantiate<GameObject>(inv.GetItemInDatabase(OB_IDs[i]).ObjectPrefs);
            n.name = OB_names[i];
            PubObject _pubObject = n.GetComponent<PubObject>();
            PolygonCollider2D _polygonCollider2D = n.GetComponent<PolygonCollider2D>();




            n.transform.position = new Vector3(OB_xpos[i], OB_ypos[i], 0);
          //  if (i < OB_horscale.Count)
             //   n.transform.localScale = new Vector3(OB_horscale[i], 1, 1);

            if (n.GetComponent<SpriteRenderer>() != null)
                n.GetComponent<SpriteRenderer>().sortingOrder = ((int)(n.transform.position.y * 100) * -1);

      
            if (_pubObject != null)
            {
                _pubObject.enabled = true;

                _pubObject.TrueName.Add(OB_names[i]);
            }



            if (_polygonCollider2D != null)
            {

                _polygonCollider2D.enabled = false;

            }


            if (n.GetComponent<StatsControll>() != null)
            {
                n.GetComponent<StatsControll>().BuildedStructure = true;

            }

            if (n.GetComponent<BoxCollider2D>() != null)
            {
                n.GetComponent<BoxCollider2D>().enabled = false;

            }
            return;
        }

        if (ParentOB.GetComponent<MovementControll>() != null) return;
         n = Instantiate<GameObject>(inv.GetItemInDatabase(OB_IDs[i]).ObjectPrefs);

        n.name = OB_xpos[i] + "_" + OB_ypos[i];
        PubObject _PubObject = n.GetComponent<PubObject>();
        SpriteRenderer _SpriteRenderer = n.GetComponent<SpriteRenderer>();
        PolygonCollider2D _PolygonCollider2D = n.GetComponent<PolygonCollider2D>();

        if (n == null) return;


        if (n.tag != "Pers")
        {
            if (n.tag == "Trash") n.name = OB_names[i];

            n.transform.parent = ParentOB.transform;

            if (_PubObject != null)
            {
                ParentOB.GetComponent<PubObject>().TopObjectsCount++;
                _PubObject.TrueName.Add(OB_names[i]);

                _PubObject.enabled = false;

                if (ParentOB.GetComponent<SpriteRenderer>() != null && _SpriteRenderer != null)
                    _SpriteRenderer.sortingOrder = ParentOB.GetComponent<SpriteRenderer>().sortingOrder + 1 * ParentOB.transform.childCount + 1;

                if (!_PubObject.decoration)
                    n.transform.position = new Vector3(ParentOB.transform.position.x, ParentOB.transform.position.y + 1 * ParentOB.GetComponent<PubObject>().TopObjectsCount, 0);
                else n.transform.position = new Vector3(ParentOB.transform.position.x, ParentOB.transform.position.y, 0);
            }
            else
            {
                n.transform.position = new Vector3(ParentOB.transform.position.x, ParentOB.transform.position.y, 0);

            }


        }
        else
        {


            if (_PubObject != null)
            {
                _PubObject.TrueName.Add(OB_names[i]);
                _PubObject.enabled = true;
            }


            if (_SpriteRenderer != null)
                _SpriteRenderer.sortingOrder = ((int)(n.transform.position.y * 100) * -1);

            n.transform.position = new Vector3(OB_xpos[i], OB_ypos[i], 0);

        }

        if (n.transform.Find("Base") != null)
            Destroy(n.transform.Find("Base").gameObject);


        if (_PolygonCollider2D != null)
        {

            _PolygonCollider2D.enabled = false;

        }

        if (n.GetComponent<StatsControll>() != null)
        {
            n.GetComponent<StatsControll>().BuildedStructure = true;

        }

        if (n.GetComponent<BoxCollider2D>() != null)
        {
            n.GetComponent<BoxCollider2D>().enabled = false;

        }



    }


    void CheckParent(ref GameObject ParentOB, int i)
    {
        for (int ii = 0; ii < _constr.OBOnBoard.Count; ii++)
        {
            if (OB_xpos[i] == _constr.OBOnBoard[ii].Place.x && OB_ypos[i] == _constr.OBOnBoard[ii].Place.y && OB_names[i] != _constr.OBOnBoard[ii].Name)
            {
                if (GameObject.Find(_constr.OBOnBoard[ii].Name) != null)
                {
                    if (GameObject.Find(_constr.OBOnBoard[ii].Name).GetComponent<PubObject>() != null)
                    {
                        if (GameObject.Find(_constr.OBOnBoard[ii].Name).tag != "Pers")
                        {
                            ParentOB = _constr.OBOnBoard[ii].Object;

                            break;
                        }
                    }
                }
            }
        }
    }
    void MENU_LoadLists()
    {

#if UNITY_SWITCH

        nn.fs.EntryType entryType = 0;
        nn.Result result = nn.fs.FileSystem.GetEntryType(ref entryType, filePath);
        if (nn.fs.FileSystem.ResultPathNotFound.Includes(result)) { return; }
        result.abortUnlessSuccess();
        Debug.Log("Open LOAD");

        result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Read);
        result.abortUnlessSuccess();
        Debug.Log("Open LOAD");

        if (!result.IsSuccess())
        {
            result = nn.fs.File.Create(filePath, MenusaveDataSize);
            result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Read);
        }

        long fileSize = 0;
        result = nn.fs.File.GetSize(ref fileSize, fileHandle);
        result.abortUnlessSuccess();

        if (fileSize < MenusaveDataSize || !result.IsSuccess())
        {
            long s = MenusaveDataSize;
            result = nn.fs.File.SetSize(fileHandle, s);
            result.abortUnlessSuccess();
        }

        long truefsize = MenusaveDataSize;

        byte[] data = new byte[truefsize];
        Debug.Log("truefsize " + truefsize);

        result = nn.fs.File.Read(fileHandle, 0, data, data.LongLength);
        result.abortUnlessSuccess();

        Debug.Log("Read LOAD");

        nn.fs.File.Close(fileHandle);

     
        using (MemoryStream stream = new MemoryStream(data))
    {
        BinaryReader reader = new BinaryReader(stream);

        for (int i = 0; i < saveslotsnumber; i++)
        {
            _menu.CurrentSlotDates[i] = reader.ReadString();
            Debug.Log("LOAD CurrentSlotDates: " + i + " ___ " + _menu.CurrentSlotDates[i]);

            _menu.CurrentSlotLocations[i] = reader.ReadString();
            Debug.Log("LOAD CurrentSlotLocations: " + i + " ___ " + _menu.CurrentSlotLocations[i]);

            _menu.CurrentSlotTimes[i] = reader.ReadString();
            Debug.Log("LOAD CurrentSlotTimes: " + i + " ___ " + _menu.CurrentSlotTimes[i]);
        }

        _menu.CurrentSlotNumber = reader.ReadInt32();




        _menu.Language = reader.ReadInt32();
        _menu.FirstLanguage = reader.ReadInt32();

        _menu.MasterSliderValue = (float)reader.ReadDouble();
        _menu.BGSliderValue = (float)reader.ReadDouble();
        _menu.ObjectsSliderValue = (float)reader.ReadDouble();
        _menu.HideUIValue = reader.ReadInt32();
        
         _menu.DrawTutorial = reader.ReadInt32();
        _menu.FirstStart = reader.ReadInt32();
     

        SaveTimer = reader.ReadInt32();
        Debug.Log("LOAD SaveTimer: ___ " + SaveTimer); 

        LoadTimer = reader.ReadInt32();
        Debug.Log("LOAD LoadTimer: ___ " + LoadTimer);

    }
#endif
    }


    public void PS5_MENU_LoadLists(byte[] data)
    {

#if UNITY_PS4 || UNITY_PS5

        print("PS5_MENU_LoadLists");

        using (MemoryStream stream = new MemoryStream(data))
    {
        BinaryReader reader = new BinaryReader(stream);

        for (int i = 0; i < saveslotsnumber; i++)
        {
            _menu.CurrentSlotDates[i] = reader.ReadString();
            Debug.Log("LOAD CurrentSlotDates: " + i + " ___ " + _menu.CurrentSlotDates[i]);

            _menu.CurrentSlotLocations[i] = reader.ReadString();
            Debug.Log("LOAD CurrentSlotLocations: " + i + " ___ " + _menu.CurrentSlotLocations[i]);

            _menu.CurrentSlotTimes[i] = reader.ReadString();
            Debug.Log("LOAD CurrentSlotTimes: " + i + " ___ " + _menu.CurrentSlotTimes[i]);
        }

        _menu.CurrentSlotNumber = reader.ReadInt32();
            OnScreenLog.Add("  _menu.CurrentSlotNumber " + _menu.CurrentSlotNumber);


            _menu.Language = reader.ReadInt32();
        _menu.FirstLanguage = reader.ReadInt32();
            _menu.LastSystemLanguage = reader.ReadInt32();


            _menu.MasterSliderValue = (float)reader.ReadDouble();
        _menu.BGSliderValue = (float)reader.ReadDouble();
        _menu.ObjectsSliderValue = (float)reader.ReadDouble();
                _menu.HideUIValue = reader.ReadInt32();

        _menu.DrawTutorial = reader.ReadInt32();
        _menu.FirstStart = reader.ReadInt32();
     

        SaveTimer = reader.ReadInt32();
        Debug.Log("LOAD SaveTimer: ___ " + SaveTimer); 

        LoadTimer = reader.ReadInt32();
        Debug.Log("LOAD LoadTimer: ___ " + LoadTimer);

    }
#endif
    }

    public void PS5_Load(byte[] data)
    {
#if UNITY_PS5||UNITY_PS4
        if (_constr == null) return;
       

        using (MemoryStream stream = new MemoryStream(data))
        {
            stream.Seek(_menu.CurrentSlotNumber * saveDataSize, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);
            
           
            PS5_LoadAllVariables(ref reader);
            
        }

        
        Loading = false;

#endif
    }

    void PS5_LoadAllVariables(ref BinaryReader reader)
    {
       
        /*
        if (_constr == null)
        {

            if (GameObject.Find("FogOfWar") != null)
                GameObject.Find("FogOfWar").GetComponent<FogOfWar>().OnLoad();

            Loading = false;

            return;

        }

        */


        if (_menu.FirstStart != 0)
            UnLoadAll();

       
        SWITCH_LoadVault(ref reader);
        SetVault();
        SWITCH_LOAD_UnlockedItems(ref reader);

        SWITCH_LOAD_PlayerVariables(ref reader);


        SWITCH_Load_InventoryVariables(ref reader);


        bool newloc = true;
        for (int i = 0; i < LocationsNames.Length; i++)
        {
            if (LocationsNames[i] == SceneManager.GetActiveScene().name)
            {

                SWITCH_LOAD_CREATELOCATIONS(ref reader);

                if (CreateLocationOnStart[i] > 0 && _menu.FirstStart != 0) newloc = false;



                SWITCH_LOAD_Location(ref reader, newloc);

                SWITCH_LOAD_Blueprints(ref reader);

                if (!newloc)
                {
                    SWITCH_LOAD_ObjectsToDestroy(ref reader);

                    SWITCH_LOAD_TILES(ref reader);

                    SWITCH_LOAD_DroppedItems(ref reader);
                    SWITCH_LOAD_ConstructedStructures(ref reader);
                    SWITCH_LOAD_PreplacedObjects(ref reader);

                    if (_menu.FirstStart > 0)
                    {
                        SetDroppedItems();
                        SetTiles();
                        SetObjectsOnBoard();

                    }

                   // SWITCH_LOAD_PIT_TILES(ref reader);
                }



            }
        }




    }

    void SWITCH_LoadVault(ref BinaryReader reader)
    {

        int VaultIDsCount = reader.ReadInt32();

        for (int i = 0; i < VaultIDsCount; i++)
        {
            int id = -1;
            id = reader.ReadInt32();
            VaultIDs.Add(id);

            int count = 0;
            count = reader.ReadInt32();
            VaultCount.Add(count);
        }


    }

    void SWITCH_LOAD_PlayerVariables(ref BinaryReader reader)
    {

      

      

        int hp = reader.ReadInt32();
        int maxhunger = reader.ReadInt32();
        int hunger = reader.ReadInt32();
        int plague = reader.ReadInt32();
        int maxplague = reader.ReadInt32();
        int daynumber = reader.ReadInt32();
        double daytime = reader.ReadDouble();
        int tutoriaphase = reader.ReadInt32();


        print("SWITCH_LOAD_PlayerVariables");

        if (_menu.FirstStart == 1)
        {
            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;

            if (hp <= 0) hp = StartStarts[CurrentCharacter].MaxHP;

            pl.HP = hp;

            if (pl.HP <= 0) pl.HP = StartStarts[CurrentCharacter].MaxHP;


            if (maxhunger <= 0) maxhunger = StartStarts[CurrentCharacter].MaxHunger;

            pl.MaxHunger = maxhunger;
            pl.Hunger = hunger;

            if (maxplague <= 0) maxplague = StartStarts[CurrentCharacter].MaxPlague;

            pl.Plague = plague;
            pl.MaxPlague = maxplague;

            DayNumber = daynumber;
            pl.MaxStamina = StartStarts[CurrentCharacter].MaxStamina;

            DayTime = (float)daytime;
            if (_Tutorial != null) _Tutorial.SetPhase(tutoriaphase);

        }
        else
        {

            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;
            pl.HP = StartStarts[CurrentCharacter].MaxHP;

            pl.MaxHunger = StartStarts[CurrentCharacter].MaxHunger;
            pl.MaxStamina = StartStarts[CurrentCharacter].MaxStamina;

           pl.Hunger = 0;

            pl.MaxPlague = StartStarts[CurrentCharacter].MaxPlague;

            pl.Payment = StartStarts[CurrentCharacter].Payment;
            pl.LootItem = StartStarts[CurrentCharacter].LootItem;

            for (int i = 0; i < StartStarts[CurrentCharacter].StartItems.Length; i++)
                inv.AddItemNOAUDIO_NOPickedNames(StartStarts[CurrentCharacter].StartItems[i], StartStarts[CurrentCharacter].StartItemsCounts[i], inv.GetItemInDatabase(StartStarts[CurrentCharacter].StartItems[i]).Durability, new Vector2(99999, 99999));

            if (_Tutorial != null) _Tutorial.SetPhase(0);




        }
    }

    void SWITCH_Load_InventoryVariables(ref BinaryReader reader)
    {
      
        Inventory_Count = reader.ReadInt32();
       // if (Inventory_Count > inv.slotX && inv.slotX>0) Inventory_Count = inv.slotX;
        

        for (int i = 0; i < Inventory_Count; i++)
        {
            int ii = reader.ReadInt32();
            int iicount = reader.ReadInt32();


            if (ii > -1 && inv.GetItemInDatabase(ii) != null && _menu.FirstStart != 0)
            {
                if (iicount > 0)
                    inv.AddItemNOAUDIO_NOPickedNames(ii, iicount, inv.GetItemInDatabase(ii).Durability, new Vector2(99999, 1));
                else inv.AddItemNOAUDIO_NOPickedNames(ii, 1, inv.GetItemInDatabase(ii).Durability, new Vector2(99999, 1));

            }


        }
        
        
    }

  

    void SWITCH_LOAD_TILES(ref BinaryReader reader)
    {
        TOnBoard_Count = reader.ReadInt32();
        if (TOnBoard_Count > 0)
        {
            for (int i = 0; i < TOnBoard_Count; i++)
            {
                Tile_names.Add(reader.ReadString());
                //  Debug.Log("LOADTILE NAME: " + Tile_names[i]);
                Tile_xpos.Add(reader.ReadInt32());
                //   Debug.Log("LOADTILE XPOS: " + Tile_xpos[i]);
                Tile_ypos.Add(reader.ReadInt32());
                //  Debug.Log("LOADTILE YPOS: " + Tile_ypos[i]);
            }
        }


       
        
    }
    void SWITCH_LOAD_PIT_TILES(ref BinaryReader reader)
    {
       
        PitsOnBoard_Count = reader.ReadInt32();
        if (PitsOnBoard_Count > 0)
        {
            for (int i = 0; i < PitsOnBoard_Count; i++)
            {
                PitsOnBoard_names.Add(reader.ReadString());
                //  Debug.Log("LOADTILE NAME: " + Tile_names[i]);
                PitsOnBoard_xpos.Add(reader.ReadInt32());
                //   Debug.Log("LOADTILE XPOS: " + Tile_xpos[i]);
                PitsOnBoard_ypos.Add(reader.ReadInt32());
                //  Debug.Log("LOADTILE YPOS: " + Tile_ypos[i]);
            }
        }


    }
    void SWITCH_LOAD_ConstructedStructures(ref BinaryReader reader)
    {
        ConstructedStructures_Count = reader.ReadInt32();

        GenMap_GrowStates_Count = reader.ReadInt32();

        if (GenMap_GrowStates_Count > 0)
        {
            for (int i = 0; i < GenMap_GrowStates_Count; i++)
            {
                int grow = reader.ReadInt32();

                GenMap_GrowStates.Add(grow);



            }
        }

        if (ConstructedStructures_Count <= 0) return;


        for (int i = 0; i < ConstructedStructures_Count; i++)
        {
            int id = reader.ReadInt32();
            OB_IDs.Add(id);

            string _name = reader.ReadString();
            OB_names.Add(_name);
            // Debug.Log("LOAD OBJECT NAME: " + OB_names[i]);

            double xload = reader.ReadDouble();
            OB_xpos.Add((float)xload);

            double yload = reader.ReadDouble();
            OB_ypos.Add((float)yload);

            int horload = reader.ReadInt32();
            OB_horscale.Add(horload);

            string spawnspot = reader.ReadString();
            OB_SpawnPoint.Add(spawnspot);

            int growstatelist = reader.ReadInt32();
            GrowStateList.Add(growstatelist);


        }


    }

    void SWITCH_LOAD_PreplacedObjects(ref BinaryReader reader)
    {
        PreplacedObjects_Count = reader.ReadInt32();

        print("LOAD PreplacedObjects_Count " + PreplacedObjects_Count);
        if (PreplacedObjects_Count > 0)
        {
            for (int i = 0; i < PreplacedObjects_Count; i++)
            {
                int grow = reader.ReadInt32();
                print("LOAD PreplacedObjects grow " + grow);
                PreplacedObjects_GrowStates.Add(grow);


            }
        }

    }




    void SWITCH_LOAD_Location(ref BinaryReader reader, bool CreateNewLocation)
    {

        LastLocation = reader.ReadString();


        int FloorStatesCount = reader.ReadInt32();

        RNDSTART_X = reader.ReadInt32();
        RNDSTART_Y = reader.ReadInt32();
        RNDStablePos = reader.ReadInt32();
        ObjectPlacement_seed_Start = reader.ReadInt32();


        RNDSHIFT = new Vector2Int(
        reader.ReadInt32(),
        reader.ReadInt32());

        CurrentCharacter = reader.ReadInt32();


        if (CreateNewLocation)
        {

            if (GenMap != null)
            {
                GenMap.CreateNewMap();
            }

            return;
        }

        if (GenMap == null) return;


        if (FloorStatesCount <= 0)
        {

            GenMap.CreateNewMap();
        }
        else
        {
            /* if (_constr.OBOnBoard.Count > 0)
             {
                 for (int i = 0; i < _constr.OBOnBoard.Count; i++)
                 {
                     DestroyObject(_constr.OBOnBoard[i]);

                     _constr.OBOnBoard.RemoveAt(i);

                 }
             }*/

            if (_constr.ConstructedStructures.Count > 0)
            {
                for (int i = 0; i < _constr.ConstructedStructures.Count; i++)
                {
                    DestroyObject(_constr.ConstructedStructures[i]);

                    _constr.ConstructedStructures.RemoveAt(i);

                }
            }



            GenMap.CleanMap();

            GenMap.RNDSHIFT = RNDSHIFT;
            GenMap.RND_Stable_Pos = RNDStablePos;
            GenMap.RNDSTART_X = RNDSTART_X;
            GenMap.RNDSTART_Y = RNDSTART_Y;
            GenMap.ObjectPlacement_seed_Start = ObjectPlacement_seed_Start;
            GenMap.ObjectPlacement_seed = ObjectPlacement_seed_Start;

            GenMap.LoadMap();
            GenMap.CreateObjects = true;
        }



    }

    void SWITCH_LOAD_ObjectsToDestroy(ref BinaryReader reader)
    {
        ObjectsToDestroy_Count = reader.ReadInt32();
        
        if (ObjectsToDestroy_Count > 0)
        {
            for (int i = 0; i < ObjectsToDestroy_Count; i++)
            {
                string s = reader.ReadString();

                if (_menu.FirstStart > 0)
                    ObjectsToDestroy.Add(s);

            }
        }

    }

    void SWITCH_LOAD_Blueprints(ref BinaryReader reader)
    {

        if (BPConstructed.Count == 0)
        {
            for (int i = 0; i < 15; i++)
                BPConstructed.Add(0);
        }

        for (int i = 0; i < BPConstructed.Count; i++)
        {
            int bp = reader.ReadInt32();
            BPConstructed[i] = bp;

        }

    }

    void SWITCH_LOAD_UnlockedItems(ref BinaryReader reader)
    {

        UnlockedItems_Count = reader.ReadInt32();
        if (UnlockedItems_Count > 0)
        {
            for (int i = 0; i < UnlockedItems_Count; i++)
            {
                int id = reader.ReadInt32();
                Unlocked_IDs.Add(id);
                inv.GetItemInDatabase(id).Locked = false;
            }
        }
    }

    void SWITCH_LOAD_DroppedItems(ref BinaryReader reader)
    {
        DroppedItems_Count = reader.ReadInt32();

        if (DroppedItems_Count > 0)
        {
            for (int i = 0; i < DroppedItems_Count; i++)
            {


                Dropped_IDs.Add(reader.ReadInt32());

                Dropped_Counts.Add(reader.ReadInt32());


                Dropped_names.Add(reader.ReadString());


                Dropped_xpos.Add((float)reader.ReadDouble());

                Dropped_ypos.Add((float)reader.ReadDouble());


            }
        }
    }


    void SetTiles()
    {
        _constr.TOnBoard = new List<TilesOnBoard>();

        for (int i = 0; i < Tile_names.Count; i++)
        {
            _constr.TOnBoard.Add(new TilesOnBoard(Tile_xpos[i], Tile_ypos[i], Tile_names[i]));
        }

        /*for (int i = 0; i < PitsOnBoard_names.Count; i++)
        {
            _constr.PitsOnBoard.Add(new TilesOnBoard(PitsOnBoard_xpos[i], PitsOnBoard_ypos[i], PitsOnBoard_names[i]));
        }*/


        for (int i = 0; i < TOnBoard_Count; i++)
        {


            //_constr.SetBigTip(-1, 1);

            for (int f = 0; f < FloorBrush.Length; f++)
            {
                if (FloorBrush[f] != null)
                {
                    if (_constr.TOnBoard[i].Name == FloorBrush[f].name)
                    {
                        _constr.Tile.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), FloorBrush[f]);
                        _constr.Floors++;
                    }
                }
            }

            for (int f = 0; f < KitchenBrush.Length; f++)
            {
                if (KitchenBrush[f] != null)
                {
                    if (_constr.TOnBoard[i].Name == KitchenBrush[f].name)
                    {
                        _constr.Tile.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), KitchenBrush[f]);
                        _constr.KitchenFloors++;
                        //_constr.SetBigTip(49, 5);
                    }
                }
            }

            for (int f = 0; f < GroundBrush.Length; f++)
            {
                if (GroundBrush[f] != null)
                {
                    if (_constr.TOnBoard[i].Name == GroundBrush[f].name)
                    {
                        _constr.Tile.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), GroundBrush[f]);
                        _constr.Grounds++;
                    }
                }
            }


            for (int f = 0; f < BaseBrush.Length; f++)
            {
                if (BaseBrush[f] != null)
                {
                    if (_constr.TOnBoard[i].Name == BaseBrush[f].name)
                    {
                        _constr.GreyMap.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), BaseBrush[f]);
                        _constr.WaterMap.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), null);

                    }
                }
            }

            for (int f = 0; f < GrassBrush.Length; f++)
            {
                if (GrassBrush[f] != null)
                {
                    if (_constr.TOnBoard[i].Name == GrassBrush[f].name)
                    {
                        _constr.GrassMap.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), GrassBrush[f]);
                    
                    }
                }
            }



        }

       /* for (int i = 0; i < PitsOnBoard_Count; i++)
        {
            for (int f = 0; f < PitBrush.Length; f++)
            {
                if (PitBrush[f] != null)
                {
                    if (_constr.PitsOnBoard[i].Name == PitBrush[f].name)
                    {
                        _constr.PitsTileBase.SetTile(new Vector3Int(_constr.PitsOnBoard[i].xPOS, _constr.PitsOnBoard[i].yPOS, 0), PitBrush[f]);
                       
                    }
                }
            }

           
        }*/


            if (TOnBoard_Count == 0)
        {
            _constr.Tile.SetTile(new Vector3Int(0, 0, 0), FloorBrush[0]);
            _constr.Floors++;


        }
    }





    void SWITCH_LoadVariables()
    {
#if UNITY_SWITCH
        if (_constr == null) return;
         long thisDatasizeStart = saveDataSize * _menu.CurrentSlotNumber + MenusaveDataSize;
        // long thisDatasizeStart = MenusaveDataSize;
        int AllDatasize = saveDataSize * 7 + MenusaveDataSize;

        nn.fs.EntryType entryType = 0;
        nn.Result result = nn.fs.FileSystem.GetEntryType(ref entryType, filePath);

        if (nn.fs.FileSystem.ResultPathNotFound.Includes(result)) { return; }
        result.abortUnlessSuccess();

        result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Read);
        result.abortUnlessSuccess();

        if (!result.IsSuccess())
        {
            result = nn.fs.File.Create(filePath, saveDataSize);
            result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Read);
        }

        long fileSize = 0;
        result = nn.fs.File.GetSize(ref fileSize, fileHandle);
        result.abortUnlessSuccess();

        /* if (fileSize < saveDataSize * saveslotsnumber + MenusaveDataSize || !result.IsSuccess())
         {
             long s = saveDataSize * saveslotsnumber + MenusaveDataSize;
             result = nn.fs.File.SetSize(fileHandle, s);
             result.abortUnlessSuccess();
         }*/

       if (fileSize < AllDatasize || !result.IsSuccess())
       {
           long s = AllDatasize;
           result = nn.fs.File.SetSize(fileHandle, s);
           result.abortUnlessSuccess();
       }

        long truefsize = fileSize - thisDatasizeStart;

        byte[] data = new byte[truefsize];

        // Debug.Log("LOAD thisDatasizeStart: " + thisDatasizeStart);

        result = nn.fs.File.Read(fileHandle, thisDatasizeStart, data, data.LongLength);
        result.abortUnlessSuccess();

        nn.fs.File.Close(fileHandle);

        //  Debug.Log("_menu.CreateLocationOnStart: " + _menu.CreateLocationOnStart);


        using (MemoryStream stream = new MemoryStream(data))
        {
            BinaryReader reader = new BinaryReader(stream);

            SWITCH_LoadAllVariables(ref reader);
        }

        
        Loading = false;

#endif
    }


    void SWITCH_LOAD_CREATELOCATIONS(ref BinaryReader reader)
    {
        CreateLocationOnStart = new int[LocationsNames.Length];

        for (int i = 0; i < LocationsNames.Length; i++)
        {
            CreateLocationOnStart[i] = reader.ReadInt32();
        }


    }


    void SWITCH_LoadAllVariables(ref BinaryReader reader)
    {

        /*
        if (_constr == null)
        {

            if (GameObject.Find("FogOfWar") != null)
                GameObject.Find("FogOfWar").GetComponent<FogOfWar>().OnLoad();

            Loading = false;

            return;

        }

        */

        if (_menu.FirstStart != 0)
            UnLoadAll();

        SWITCH_LoadVault(ref reader);
        SetVault();
        SWITCH_LOAD_UnlockedItems(ref reader);

        SWITCH_LOAD_PlayerVariables(ref reader);


       SWITCH_Load_InventoryVariables(ref reader);

        
       bool newloc = true;
       for (int i = 0; i < LocationsNames.Length; i++)
       {
           if (LocationsNames[i] == SceneManager.GetActiveScene().name)
           {

               SWITCH_LOAD_CREATELOCATIONS(ref reader);

               if (CreateLocationOnStart[i] > 0 && _menu.FirstStart != 0) newloc = false;



               SWITCH_LOAD_Location(ref reader, newloc);

               SWITCH_LOAD_Blueprints(ref reader);

               if (!newloc)
               {
                   SWITCH_LOAD_ObjectsToDestroy(ref reader);

                   SWITCH_LOAD_TILES(ref reader);

                   SWITCH_LOAD_DroppedItems(ref reader);
                   SWITCH_LOAD_ConstructedStructures(ref reader);
                   SWITCH_LOAD_PreplacedObjects(ref reader);

                   if (_menu.FirstStart > 0)
                   {
                       SetDroppedItems();
                       SetTiles();
                       SetObjectsOnBoard();

                   }

                    //SWITCH_LOAD_PIT_TILES(ref reader);
                }



           }
       }




    }

    public void SWITCH_Load()
    {
#if UNITY_SWITCH
         


            MENU_LoadLists();

           // if (_menu.CreateLocationOnStart == 1)
           // {
               if(_menu.FirstStart !=0)
               UnLoadAll();
               SWITCH_LoadVariables();
           // }

#endif
    }

  


    public void CreateSaveText()
    {
        if (SavingText == null)
        {
            SavingText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SavingText"), GameObject.Find("Canvas").transform);
        }
    }

    public void ThisLocationIsCreated()
    {
        for (int i = 0; i < LocationsNames.Length; i++)
        {
            if (LocationsNames[i] == SceneManager.GetActiveScene().name)
            {
                CreateLocationOnStart[i] = 1;

                print("ThisLocationIsCreated " + LocationsNames[i] + " / " + i);

            }
        }
    }

    public void NextLocationIsNOTCreated(string location)
    {
        for (int i = 0; i < LocationsNames.Length; i++)
        {
            if (LocationsNames[i] == location)
            {
                CreateLocationOnStart[i] = 0;

            }
        }
    }

    void SetGrowStatueToMapGenObjects()
    {
        if (GenMap != null)
        {
            if (GenMap.CreatedObjects.Count > 0)
            {
                for (int i = 0; i < GenMap_GrowStates_Count; i++)
                {
                    if (i <= GenMap.CreatedObjects.Count - 1 && i < GenMap_GrowStates.Count - 1)
                    {
                        if (GenMap.CreatedObjects[i] != null && GenMap.CreatedObjects[i].GetComponent<StatsControll>() != null)
                            GenMap.CreatedObjects[i].GetComponent<StatsControll>().CurrentGrowState = GenMap_GrowStates[i];

                        print("SetGrowStatueToMapGenObjects " + GenMap_GrowStates_Count);

                    }
                }
            }
        }
    }

    void SetGrowStatuesPreplaced()
    {
        int prepnum = 0;

        for (int i = 0; i < _constr.OBOnBoard.Count; i++)
        {

            if (GenMap != null)
            {


                if (!_constr.ConstructedStructures.Contains(_constr.OBOnBoard[i]) && PreplacedObjects_GrowStates.Count > 0)
                {

                    if (_constr.OBOnBoard[i].Object != null)
                    {
                        if (_constr.OBOnBoard[i].Object.GetComponent<StatsControll>() != null && !GenMap.CreatedObjects.Contains(_constr.OBOnBoard[i].Object))
                        {
                            _constr.OBOnBoard[i].Object.GetComponent<StatsControll>().CurrentGrowState = PreplacedObjects_GrowStates[prepnum];
                            if (prepnum < PreplacedObjects_GrowStates.Count - 1) prepnum++;
                        }

                    }

                }



            }
            else
            {
                if (_constr.OBOnBoard[i].Object != null && PreplacedObjects_GrowStates.Count > 0)
                {
                    if (_constr.OBOnBoard[i].Object.GetComponent<StatsControll>() != null)
                    {
                        _constr.OBOnBoard[i].Object.GetComponent<StatsControll>().CurrentGrowState = PreplacedObjects_GrowStates[prepnum];
                        if (prepnum < PreplacedObjects_GrowStates.Count - 1) prepnum++;
                    }

                }


            }

        }



    }

    public void ResetPolygonColliders()
    {
        if (_constr == null) return;




        if (_menu.FirstStart > 0)
        {


            for (int i = 0; i < LocationsNames.Length; i++)
            {
                if (LocationsNames[i] == SceneManager.GetActiveScene().name)
                {
                    if (CreateLocationOnStart[i] > 0)
                    {
                        SetGrowStatueToMapGenObjects();
                        SetGrowStatuesPreplaced();

                        SetObjectsToDestroy();
                        
                    }
                }
            }


        }


        if (_constr.ConstructedStructures.Count > 0)
        {
            for (int i = 0; i < _constr.ConstructedStructures.Count; i++)
            {
                if (i <= OB_SpawnPoint.Count - 1)
                {
                    if (GameObject.Find(OB_SpawnPoint[i]) != null)
                    {
                        _constr.ConstructedStructures[i].Object.GetComponent<StatsControll>().SpawnPointName = OB_SpawnPoint[i];

                        GameObject.Find(OB_SpawnPoint[i]).GetComponent<Enemies>().BuildedPers.Add(_constr.ConstructedStructures[i].Object);
                    }
                }

            }
        }



        for (int i = 0; i < _constr.OBOnBoard.Count; i++)
        {
            if (_constr.OBOnBoard[i].Object != null)
            {
                BoxCollider2D _boxCollider2D = _constr.OBOnBoard[i].Object.GetComponent<BoxCollider2D>();

                PolygonCollider2D _polygonCollider2D = _constr.OBOnBoard[i].Object.GetComponent<PolygonCollider2D>();
                PubObject _pubObject = _constr.OBOnBoard[i].Object.GetComponent<PubObject>();



                TurnPoly(ref _boxCollider2D, ref _polygonCollider2D, ref _pubObject);
            }

        }


        pl.PathScan();
    }



    void TurnPoly(ref BoxCollider2D _boxCollider2D, ref PolygonCollider2D _polygonCollider2D, ref PubObject _pubObject)
    {
        if (_polygonCollider2D == null) return;
        if (_pubObject == null) return;
        if (_pubObject.wall <= 0) return;


        if (_pubObject.transform.parent == null)
        {
            _boxCollider2D.enabled = true;
            _polygonCollider2D.enabled = true;
            _polygonCollider2D.isTrigger = false;
            return;
        }


        if (_pubObject.transform.parent.gameObject.GetComponent<PubObject>() == null)
        {
            _boxCollider2D.enabled = true;
            _polygonCollider2D.enabled = true;
            _polygonCollider2D.isTrigger = false;

            return;
        }
        else if (_pubObject.transform.parent.gameObject.GetComponent<PubObject>() != null)
        {

            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;
            _polygonCollider2D.isTrigger = true;
        }
        else
        {
            _boxCollider2D.enabled = true;
            _polygonCollider2D.enabled = true;
            _polygonCollider2D.isTrigger = false;
        }


    }



    public void ResetLocations()
    {
        for (int i = 0; i < CreateLocationOnStart.Length; i++)
            CreateLocationOnStart[i] = 0;

        if (pl != null)
        {
            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;
            pl.HP = StartStarts[CurrentCharacter].MaxHP;

            pl.MaxHunger = StartStarts[CurrentCharacter].MaxHunger;
            pl.MaxStamina = StartStarts[CurrentCharacter].MaxStamina;

            pl.Hunger = 0;

            pl.MaxPlague = StartStarts[CurrentCharacter].MaxPlague;

            pl.Payment = StartStarts[CurrentCharacter].Payment;
            pl.LootItem = StartStarts[CurrentCharacter].LootItem;

            if (_DayAndNight != null)
                _DayAndNight.Day = 0;

            DayTime = 0;

            inv.ResetInventory();
            
            for (int i = 0; i < StartStarts[CurrentCharacter].StartItems.Length; i++)
                inv.AddItemNOAUDIO_NOPickedNames(StartStarts[CurrentCharacter].StartItems[i], StartStarts[CurrentCharacter].StartItemsCounts[i], inv.GetItemInDatabase(StartStarts[CurrentCharacter].StartItems[i]).Durability, new Vector2(99999, 99999));

        }

    }

    public void UnLoadAll()
    {
        /*  if (_constr != null)
           {
               int obcount = _constr.OBOnBoard.Count;

               for (int i = 0; i < obcount; i++)
               {
                   if (_constr.OBOnBoard[0] != null)
                   {
                       DestroyObject(_constr.OBOnBoard[0]);

                       _constr.OBOnBoard.RemoveAt(0);
                   }
               }


           }*/

        for (int i = 0; i < LocationsNames.Length; i++)
        {

            CreateLocationOnStart[i] = 0;

        }

        if (GenMap != null)
        {
            _constr.Tile.ClearAllTiles();

       
            _constr.TOnBoard = new List<TilesOnBoard>();

        }

        if (_constr != null)
        {
            _constr.DroppedItems = new List<GameObject>();
        }

        Tile_names = new List<string>();
        Tile_xpos = new List<int>();
        Tile_ypos = new List<int>();

        PitsOnBoard_names = new List<string>();
        PitsOnBoard_xpos = new List<int>();
        PitsOnBoard_ypos = new List<int>();

   
        GenMap_GrowStates = new List<int>();

        PreplacedObjects_GrowStates = new List<int>();

        Dropped_IDs = new List<int>();
        Dropped_Counts = new List<int>();
        Dropped_names = new List<string>();
        Dropped_xpos = new List<float>();
        Dropped_ypos = new List<float>();


        OB_IDs = new List<int>();
        OB_names = new List<string>();
        OB_xpos = new List<float>();
        OB_ypos = new List<float>();
        OB_horscale = new List<int>();
        OB_SpawnPoint = new List<string>();

        Trash_names = new List<string>();
        Trash_xpos = new List<float>();
        Trash_ypos = new List<float>();
   
        ObjectsToDestroy = new List<string>();

        Unlocked_IDs = new List<int>();
    }


    void DestroyObject(ObjectOnBoard obj)
    {

        if (obj.Stats != null)
        {
            if (obj.Stats.HPUI != null)
                Destroy(obj.Stats.HPUI);

            if (obj.Stats.ChargeUI != null)
                Destroy(obj.Stats.ChargeUI);
        }
        Destroy(obj.Object);
    }

    void DestroyObject(GameObject obj)
    {

        if (obj.GetComponent<StatsControll>() != null)
        {
            if (obj.GetComponent<StatsControll>().HPUI != null)
                Destroy(obj.GetComponent<StatsControll>().HPUI);

            if (obj.GetComponent<StatsControll>().ChargeUI != null)
                Destroy(obj.GetComponent<StatsControll>().ChargeUI);
        }
        Destroy(obj);
    }

}
