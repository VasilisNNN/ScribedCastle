using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

#if UNITY_PS5|| UNITY_PS4
using UnityEngine.PS5;
#endif


public class PS5SaveLoad : SaveLoadBasic, ISaveLoad
{

    public StartStart[] StartStarts;



    [HideInInspector]
    public string mountName = "ScribedCSave";
    private string fileName = "ScribedCSaveData";


    private const int saveDataVersion = 1;
    [HideInInspector]
    public const int saveDataSize = 131072;
    private const int MenusaveDataSize = 512;

#if UNITY_PS5 || UNiTY_PS4
    public SonySaveDataMain PS_SaveMain;
#endif


    public override void SetOnStart(StartStart[] stats)
    {

#if UNITY_PS5 || UNITY_PS4
        if (!PS_SaveMain.StartLoad)
        {
            PS_SaveMain.StartLoad = true;
        }
#endif
        
        pl = InitializeObjects.PL;

        if (GameObject.Find("Player") != null)
        {
            pl.HP = 100;
            pl.MaxHP = 100;
        }

        StartStarts = stats;
    }

    public override void MainSave(bool SaveAll)
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
            writer.Write(_menu.TransparencyUIValue);

            writer.Write(_menu.DrawTutorial);
            writer.Write(_menu.FirstStart);


        



            stream.Close();
            MenuSavedata = stream.GetBuffer();
            PS_SaveMain.MenuSavedata = MenuSavedata;
        }

        using (MemoryStream stream2 = new MemoryStream(saveDataSize))
        {
          //  stream2.Position = _menu.CurrentSlotNumber * saveDataSize;

            BinaryWriter writer = new BinaryWriter(stream2);
            Debug.Log("SAVE 0 " + stream2.Position);

            if (_constr != null && SaveAll)
            {
                Debug.Log("SAVE 1");
    
                SWITCH_SAVE_UnlockedItems(ref writer);

                SWITCH_SAVE_PlayerVariables(ref writer);
                SWITCH_Save_InventoryVariables(ref writer);

              
                for (int i = 0; i < LocationsNames.Length; i++)
                {
                    if (LocationsNames[i] == SceneManager.GetActiveScene().name)
                    {
                        Debug.Log("SAVE 2");
                        SWITCH_SAVE_CREATELOCATIONS(ref writer);


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


        writer.Write(pl.HP);

        writer.Write(DayNumber);
        writer.Write((double)DayTime);

        if (_Tutorial != null) _TutorialPhase = _Tutorial.GetPhase();
        writer.Write(_TutorialPhase);


    }




    void SWITCH_SAVE_Blueprints(ref BinaryWriter writer)
    {

        if (BPConstructed.Count == 0)
        {
            for (int i = 0; i < 100; i++)
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


    void SWITCH_SAVE_Location(ref BinaryWriter writer)
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


    void SWITCH_SAVE_ConstructedStructures(ref BinaryWriter writer)
    {
        if (_constr.ConstructedStructures != null)
        {
            ConstructedStructures_Count = _constr.ConstructedStructures.Count;
        }
        else ConstructedStructures_Count = 0;

        writer.Write(ConstructedStructures_Count);

        Debug.Log("ConstructedStructures_Count SAVE " + ConstructedStructures_Count);
        
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
            string ConstructedStructuresName = "";
            float xwright = 0;
            float ywright = 0;
            int hor = 1;
            string Spawnpointname = "";
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


            writer.Write(_constr.ConstructedStructures[i].ID);
            writer.Write(ConstructedStructuresName);
            writer.Write((double)xwright);
            writer.Write((double)ywright);
            writer.Write(hor);
            writer.Write(Spawnpointname);
            writer.Write(CurrentGrowState);


        }

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
            writer.Write(PreplacedObjects_GrowStates[i]);
        }


    }


    

  




    void SWITCH_LoadVault(ref BinaryReader reader)
    {

        int VaultIDsCount = reader.ReadInt32();
        
        Debug.Log("VaultIDsCount " + VaultIDsCount);

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
        int daynumber = reader.ReadInt32();
        double daytime = reader.ReadDouble();
        int tutoriaphase = reader.ReadInt32();
        _TutorialPhase = tutoriaphase;



        Debug.Log("daytime " + daytime);

        if (_menu.FirstStart == 1)
        {
            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;

            if (hp <= 0) hp = StartStarts[CurrentCharacter].MaxHP;

            pl.HP = hp;

            if (pl.HP <= 0) pl.HP = StartStarts[CurrentCharacter].MaxHP;



            DayNumber = daynumber;
            pl.MaxStamina = StartStarts[CurrentCharacter].MaxStamina;

            DayTime = (float)daytime;
            if (_Tutorial != null)
            {
                _Tutorial.Phase = tutoriaphase;
                // _Tutorial.Init();
                //  _Tutorial.SetPhase(tutoriaphase);
            }
        }
        else
        {

            pl.MaxHP = StartStarts[CurrentCharacter].MaxHP;
            pl.HP = StartStarts[CurrentCharacter].MaxHP;


            pl.Payment = StartStarts[CurrentCharacter].Payment;
            pl.LootItem = StartStarts[CurrentCharacter].LootItem;

            for (int i = 0; i < StartStarts[CurrentCharacter].StartItems.Length; i++)
                inv.AddItemNOAUDIO_NOPickedNames(StartStarts[CurrentCharacter].StartItems[i], StartStarts[CurrentCharacter].StartItemsCounts[i], inv.database.FindItem(StartStarts[CurrentCharacter].StartItems[i]).Durability, new Vector2(99999, 99999));

            if (_Tutorial != null)
            {
                _Tutorial.Phase = _TutorialPhase;
                // _Tutorial.Init();
                // _Tutorial.SetPhase(0);
            }



        }
    }

    void SWITCH_Load_InventoryVariables(ref BinaryReader reader)
    {

        Inventory_Count = reader.ReadInt32();
        // if (Inventory_Count > inv.slotX && inv.slotX>0) Inventory_Count = inv.slotX;

        Debug.Log("Inventory_Count " + Inventory_Count);
        for (int i = 0; i < Inventory_Count; i++)
        {
            int ii = reader.ReadInt32();
            int iicount = reader.ReadInt32();


            if (ii > -1 && inv.database.FindItem(ii) != null && _menu.FirstStart != 0)
            {
                if (iicount > 0)
                    inv.AddItemNOAUDIO_NOPickedNames(ii, iicount, inv.database.FindItem(ii).Durability, new Vector2(99999, 1));
                else inv.AddItemNOAUDIO_NOPickedNames(ii, 1, inv.database.FindItem(ii).Durability, new Vector2(99999, 1));

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
        
                Tile_xpos.Add(reader.ReadInt32());
        
                Tile_ypos.Add(reader.ReadInt32());
              
            }
        }




    }
   
    void SWITCH_LOAD_ConstructedStructures(ref BinaryReader reader)
    {
        ConstructedStructures_Count = reader.ReadInt32();
        GenMap_GrowStates_Count = reader.ReadInt32();

        Debug.Log("ConstructedStructures_Count LOAD " + ConstructedStructures_Count);

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

        if (PreplacedObjects_Count > 0)
        {
            for (int i = 0; i < PreplacedObjects_Count; i++)
            {
                int grow = reader.ReadInt32();
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
                Debug.Log("Create new map");

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
        Debug.Log("LOAD ObjectsToDestroy_Count " + ObjectsToDestroy_Count);
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
            for (int i = 0; i < 100; i++)
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

        Debug.Log("UnlockedItems_Count LOAD " + UnlockedItems_Count);

        if (UnlockedItems_Count > 0)
        {
            for (int i = 0; i < UnlockedItems_Count; i++)
            {
                int id = reader.ReadInt32();
                Unlocked_IDs.Add(id);
                inv.database.FindItem(id).Locked = false;
            }
        }
    }

    void SWITCH_LOAD_DroppedItems(ref BinaryReader reader)
    {
        DroppedItems_Count = reader.ReadInt32();

        if (DroppedItems_Count <= 0) return;
        
        for (int i = 0; i < DroppedItems_Count; i++)
        {


            Dropped_IDs.Add(reader.ReadInt32());

            Dropped_Counts.Add(reader.ReadInt32());


            Dropped_names.Add(reader.ReadString());


            Dropped_xpos.Add((float)reader.ReadDouble());

            Dropped_ypos.Add((float)reader.ReadDouble());


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
            Debug.Log("SWITCH_LOAD_CREATELOCATIONS " + i + " / " + CreateLocationOnStart[i]);

        }


    }


   
    public override void MainLoad()
    {

#if UNITY_PS5 || UNITY_PS4
           PS_SaveMain.StartAutoSaveLoad();
#endif

        Loading = false;

    }





    public override void MENU_LOAD_DATA(byte[] data)
    {


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
            _menu.TransparencyUIValue = reader.ReadInt32();

            _menu.DrawTutorial = reader.ReadInt32();
            _menu.FirstStart = reader.ReadInt32();


        }

    }

    public override void MAIN_LOAD_DATA(byte[] data)
    {
#if UNITY_PS5 || UNITY_PS4
        if (_constr == null) return;


        using (MemoryStream stream = new MemoryStream(data))
        {
            BinaryReader reader = new BinaryReader(stream);
            reader.BaseStream.Seek(_menu.CurrentSlotNumber * saveDataSize, SeekOrigin.Begin);

            Debug.Log("LOAD Position " + stream.Position);
            PS5_LoadAllVariables(ref reader);

        }


        Loading = false;

#endif
    }
  
    void PS5_LoadAllVariables(ref BinaryReader reader)
    {


        Debug.Log("PS5_LoadAllVariables 0 ");

        if (_menu.FirstStart != 0)
            UnLoadAll();


      
        SWITCH_LOAD_UnlockedItems(ref reader);

        SWITCH_LOAD_PlayerVariables(ref reader);


        SWITCH_Load_InventoryVariables(ref reader);


        bool newloc = true;
        for (int i = 0; i < LocationsNames.Length; i++)
        {
            if (LocationsNames[i] == SceneManager.GetActiveScene().name)
            {
            

                SWITCH_LOAD_CREATELOCATIONS(ref reader);

                Debug.Log("PS5_LoadAllVariables 1 / CreateLocationOnStart[i] " + i + ": " +
                      CreateLocationOnStart[i] +
                      " / _menu.FirstStart " + _menu.FirstStart);



                if (CreateLocationOnStart[i] > 0 && _menu.FirstStart != 0) newloc = false;



                SWITCH_LOAD_Blueprints(ref reader);

                if (!newloc)
                {

                    Debug.Log("PS5_LoadAllVariables 2");

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





}
