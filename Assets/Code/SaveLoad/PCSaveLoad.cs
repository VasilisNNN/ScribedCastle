using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PCSaveLoad : SaveLoadBasic
{
    public StartStart[] StartStarts;

    private int slotsnum = 10;


    public override void SetOnStart(StartStart[] stats)
    {
        StartStarts = stats;

    }

    public override void MainSave(bool saveall)
    {

        string SlotName = "Slot" + _menu.CurrentSlotNumber;






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
        PlayerPrefs.SetInt("TransparencyUIValue", _menu.TransparencyUIValue);


        PlayerPrefs.SetInt("DrawTutorial", _menu.DrawTutorial);
        PlayerPrefs.SetInt("FirstStart", _menu.FirstStart);
        PlayerPrefs.SetString("StartLocation", _menu.StartLocation);
        PlayerPrefs.SetInt("Progression", _menu.Progression);





        LastLocation = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetString("LastLocation", LastLocation);



        if (_constr != null && saveall)
        {
            UNITY_SAVE_Vault();
            UNITY_SAVE_UnlockedItems(SlotName);

            UNITY_SAVE_Playervariables(SlotName);
            UNITY_SAVE_InventoryVaruables(SlotName);

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



    public override void MainLoad()
    {

        print("main load");

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


        Loading = false;
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
        if (BPConstructed.Count == 0)
        {
            for (int i = 0; i < 100; i++)
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
        _menu.TransparencyUIValue = PlayerPrefs.GetInt("TransparencyUIValue");

        _menu.DrawTutorial = PlayerPrefs.GetInt("DrawTutorial");
        _menu.FirstStart = PlayerPrefs.GetInt("FirstStart");
        _menu.StartLocation = PlayerPrefs.GetString("StartLocation");
        _menu.Progression = PlayerPrefs.GetInt("Progression");

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

            if (ii > -1 && inv.GetItemInDatabase(ii) != null && _menu.FirstStart != 0)
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

        if (_menu.StartLocation.Length <= 1)
            _menu.StartLocation = "Island";
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

                print("OB_names.Add");
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


        // One Stable Tile To build another tiles
        if (TOnBoard_Count == 0)
        {

            _constr.Tile.SetTile(new Vector3Int(0, 0, 0), FloorBrush[0]);
            _constr.Floors++;

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




}
