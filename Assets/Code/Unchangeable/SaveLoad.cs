using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    

    [HideInInspector]
    public List<int> Unlocked_IDs = new List<int>();

    public int PreplacedObjects_Count, GenMap_GrowStates_Count, TOnBoard_Count, PitsOnBoard_Count, ConstructedStructures_Count, TRASHOnBoard_Count, ObjectsToDestroy_Count, DroppedItems_Count, Inventory_Count, UnlockedItems_Count;

    private bool Resetpol = false;

    private List<string> LocationsMenu = new List<string>();



#if UNITY_PS5|| UNITY_PS4
    
    [HideInInspector]
    public string mountName = "ScribedCSave";
    private string fileName = "ScribedCSaveData";


    private const int saveDataVersion = 1;
    [HideInInspector]
    public const int saveDataSize = 131072;
    private const int MenusaveDataSize = 512;


    public SonySaveDataMain PS_SaveMain;


#endif



    public int SaveTimer { get; private set; }
    public int LoadTimer { get; private set; }

    private float SecondsTimer;
    public List<string> ACHNames = new List<string>();
  

    public string LastLocation { get; set; }
    
   
    private Player pl;
    private Inventory inv;
    private GenerateMap GenMap;

    private int slotsnum = 10;
    private GameObject SavingText;
    public string[] LocationsNames;
    public int[] CreateLocationOnStart;

  

 
    public int _TutorialPhase { get; set; }

    public int CurrentCharacter { get; set; }
 
    private int saveslotsnumber = 7;

    private AstarPath AP;
    private Tutorial _Tutorial;



   public SaveLoadBasic SaveLoadCurrent;
    private SwitchSaveLoad SaveLoad_Switch = new SwitchSaveLoad();
    private PCSaveLoad SaveLoad_PC = new PCSaveLoad();
    private PS5SaveLoad SaveLoad_PS5 = new PS5SaveLoad();
    private ItemDatabase itemDatabase;

    void Start()
    {
#if UNITY_SWITCH

        SaveLoadCurrent = SaveLoad_Switch;
#endif

#if UNITY_STANDALONE
        SaveLoadCurrent = SaveLoad_PC;
#endif

#if UNITY_PS5 || UNITY_PS4
        PS_SaveMain = GameObject.Find("Constructor").GetComponent<SonySaveDataMain>();
        SaveLoad_PS5.PS_SaveMain = PS_SaveMain;

        SaveLoadCurrent = SaveLoad_PS5;
#endif



        SaveLoadCurrent.Init();
        SaveLoadCurrent.SetOnStart(StartStarts);

        LocationsNames = SaveLoadCurrent.LocationsNames;
        LocationsMenu = SaveLoadCurrent.LocationsMenu;
        CreateLocationOnStart = new int[LocationsNames.Length];


        if (GameObject.Find("PathFinding") != null)
            AP = GameObject.Find("PathFinding").GetComponent<AstarPath>();

        if (GameObject.Find("TUTORIAL") != null) _Tutorial = GameObject.Find("TUTORIAL").GetComponent<Tutorial>();

        

   
        
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

        itemDatabase = InitializeObjects.Itemdatabase;



        print("saveload start 6");
        _menu = GameObject.Find("Constructor").GetComponent<MenuCustom>();
        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        if(GameObject.Find("DayAndNight")!=null)
        _DayAndNight = GameObject.Find("DayAndNight").GetComponent<DayAndNight>();

    
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
            print("pl.DayNight.Day " + SaveLoadCurrent.DayTime);
            _DayAndNight.DayTime = SaveLoadCurrent.DayTime;
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

        if (SaveLoadCurrent.Saving)
        {
            Save(true);
        }

        if (SaveLoadCurrent.Loading)
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
                SaveLoadCurrent.ResetPolygonColliders();
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


        SaveLoadCurrent.MainSave(SaveAll);



    }







    





    public void Load()
    {

        SaveLoadCurrent.MainLoad();


#if UNITY_PS4 || UNITY_PS5


        PS_SaveMain.StartAutoSaveLoad();
  
#endif
       
    }














    /* public void PS5_MENU_LoadLists(byte[] data)
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
        _menu.TransparencyUIValue= reader.ReadInt32();

         _menu.DrawTutorial = reader.ReadInt32();
         _menu.FirstStart = reader.ReadInt32();


     }
#endif
     }

     public void PS5_Load(byte[] data)
     {
#if UNITY_PS5 || UNITY_PS4
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
                         SetObjectsToDestroy();
                     }

                    // SWITCH_LOAD_PIT_TILES(ref reader);
                 }



             }
         }




     }
 */




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
                _DayAndNight.DayTime = 0;

            print("RESET LOCATION");
            SaveLoadCurrent.DayTime = 0;

            inv.ResetInventory();
            
            for (int i = 0; i < StartStarts[CurrentCharacter].StartItems.Length; i++)
                inv.AddItemNOAUDIO_NOPickedNames(StartStarts[CurrentCharacter].StartItems[i], StartStarts[CurrentCharacter].StartItemsCounts[i], itemDatabase.FindItem(StartStarts[CurrentCharacter].StartItems[i]).Durability, new Vector2(99999, 99999));

        }

    }

  
   

}
