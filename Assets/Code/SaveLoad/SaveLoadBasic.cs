using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public abstract class SaveLoadBasic : MonoBehaviour, ISaveLoad
{

    public List<int> VaultIDs = new List<int>();


    public List<int> VaultCount = new List<int>();
    public List<string> Tile_names = new List<string>();

    public List<int> Tile_xpos = new List<int>();
    public List<int> Tile_ypos = new List<int>();
    public int saveslotsnumber => 7;



    public List<int> OB_IDs = new List<int>();
    public List<string> OB_names = new List<string>();
    public List<float> OB_xpos = new List<float>();
    public List<float> OB_ypos = new List<float>();
    public List<int> OB_horscale = new List<int>();
    public List<string> OB_SpawnPoint = new List<string>();

    public List<int> GenMap_GrowStates = new List<int>();

    public List<int> PreplacedObjects_GrowStates = new List<int>();

    public List<int> Dropped_IDs = new List<int>();
    public List<int> Dropped_Counts = new List<int>();
    public List<string> Dropped_names = new List<string>();
    public List<float> Dropped_xpos = new List<float>();
    public List<float> Dropped_ypos = new List<float>();
    public List<int> GrowStateList = new List<int>();

    public List<int> Unlocked_IDs = new List<int>();

    public List<int> BPConstructed = new List<int>();


    public string[] TileNames;

    public bool Saving { get; set; }
    public bool Loading { get; set; }

    private float SecondsTimer;
    public List<string> ACHNames = new List<string>();
    public List<string> ObjectsToDestroy = new List<string>();


    public string LastLocation { get; set; }


    public string[] LocationsNames;
    public List<string> LocationsMenu = new List<string>();
    public int[] CreateLocationOnStart;


    public static Player pl;
    public static Inventory inv;
    public static GenerateMap GenMap;
    public static MenuCustom _menu;
    public static Constructor _constr;
    public static DayAndNight _DayAndNight;
    public Tutorial _Tutorial;

    public TileBase[] FloorBrush, BaseBrush, GrassBrush, PitBrush;

    public int PreplacedObjects_Count, GenMap_GrowStates_Count, TOnBoard_Count, ConstructedStructures_Count, TRASHOnBoard_Count, ObjectsToDestroy_Count, DroppedItems_Count, Inventory_Count, UnlockedItems_Count;
    public int DayNumber { get; set; }
    public float DayTime { get; set; }
    public int _TutorialPhase { get; set; }

    public int CurrentCharacter { get; set; }



    public List<int> FloorStates = new List<int>();
    public int RNDSTART_Y, RNDSTART_X, RNDStablePos, ObjectPlacement_seed_Start;

    public Vector2Int RNDSHIFT;


    public void Init()
    {
        TileNames = new string[3721];

        if (GameObject.Find("Player") != null)
        {

            pl = InitializeObjects.PL;
            inv = pl.inv;

        }

        if (GameObject.Find("Grid") != null)
        {
            if (GameObject.Find("Grid").GetComponent<GenerateMap>() != null)
            {
                if (GameObject.Find("Grid").GetComponent<GenerateMap>().isActiveAndEnabled)
                    GenMap = GameObject.Find("Grid").GetComponent<GenerateMap>();
            }

        }

        if (GameObject.Find("Constructor") != null)
        {
            _menu = GameObject.Find("Constructor").GetComponent<MenuCustom>();
            _constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        }


        if (GameObject.Find("DayAndNight") != null)
            _DayAndNight = GameObject.Find("DayAndNight").GetComponent<DayAndNight>();

        if (GameObject.Find("TUTORIAL") != null) _Tutorial = GameObject.Find("TUTORIAL").GetComponent<Tutorial>();

        LocationsNames = new string[6] { "Tutorial", "Island", "Lake", "Mountain", "Hell", "Autumn" };
        LocationsMenu.Add("StartMenu");
        LocationsMenu.Add("ChoosePlayer_Main");
        LocationsMenu.Add("ChoosePlayer_Tutorial");
        LocationsMenu.Add("Intro_Tutorial");
        LocationsMenu.Add("Intro");
        CreateLocationOnStart = new int[LocationsNames.Length];

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

    

        BaseBrush = new TileBase[1] {
            Resources.Load<TileBase>("Brushes/Ground")
            };

        GrassBrush = new TileBase[1] {
            Resources.Load<TileBase>("Brushes/GrassRegular")
            };

        PitBrush = new TileBase[] {
            Resources.Load<TileBase>("Brushes/Pit"),
            Resources.Load<TileBase>("Brushes/WaterDitch"),
            Resources.Load<TileBase>( "Brushes/River")
            };


    }

    public abstract void SetOnStart(StartStart[] stats);

    public abstract void MainSave(bool saveall);



    public void ThisLocationIsCreated()
    {
        for (int i = 0; i < LocationsNames.Length; i++)
        {
            if (LocationsNames[i] == SceneManager.GetActiveScene().name)
            {
                CreateLocationOnStart[i] = 1;

            }
        }
    }



    public abstract void MainLoad();
   

    public void DestroyObject(ObjectOnBoard obj)
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

    public void SetTiles()
    {
        _constr.TOnBoard = new List<TilesOnBoard>();
        print("SetTiles 0");
        for (int i = 0; i < Tile_names.Count; i++)
        {
            _constr.TOnBoard.Add(new TilesOnBoard(Tile_xpos[i], Tile_ypos[i], Tile_names[i]));
        }
        print("SetTiles 1");
        for (int i = 0; i < TOnBoard_Count; i++)
        {

            print("SetTiles 2");

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


            print("SetTiles 4");
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
            print("SetTiles 5");
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
            print("SetTiles 6");
            for (int f = 0; f < PitBrush.Length; f++)
            {
                if (PitBrush[f] != null)
                {
                    if (_constr.TOnBoard[i].Name == PitBrush[f].name)
                    {
                        _constr.PitsTileBase.SetTile(new Vector3Int(_constr.TOnBoard[i].xPOS, _constr.TOnBoard[i].yPOS, 0), PitBrush[f]);

                    }
                }
            }


        }

        pl.PathRescan = 0.2f;
        print("SetTiles 6");

        if (TOnBoard_Count == 0)
        {
            _constr.Tile.SetTile(new Vector3Int(0, 0, 0), FloorBrush[0]);
            _constr.Floors++;


        }


    }


   public void SetDroppedItems()
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


   public void SetObjectsOnBoard()
    {
        if (_menu.FirstStart == 0) return;


        print("ConstructedStructures_Count "+ ConstructedStructures_Count +" / " + OB_names.Count);
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

        pl.PathRescan = 1;

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
            if (PO.tag == "Pers") _constr.AllPeople++;




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
                n.GetComponent<BoxCollider2D>().enabled = true;

            }

            if (n.GetComponent<Character>() != null)
            {
                n.GetComponent<Character>().enabled = true;

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
            n.GetComponent<BoxCollider2D>().enabled = true;

           
        }

        if (n.GetComponent<Character>() != null)
        {
            n.GetComponent<Character>().enabled = true;

        }

    

    }
    void CheckParent(ref GameObject ParentOB, int i)
    {
        for (int ii = 0; ii < _constr.OBOnBoard.Count; ii++)
        {
            if (OB_xpos[i] == _constr.OBOnBoard[ii].Place.x && OB_ypos[i] == _constr.OBOnBoard[ii].Place.y )
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


   public void SetObjectsToDestroy()
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


    public void SetVault()
    {

        int j = 0;
        if (pl == null) return;
        if (inv.VaultUI == null) return;
        
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


    public void UnLoadAll()
    {

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


        ObjectsToDestroy = new List<string>();

        Unlocked_IDs = new List<int>();
        GrowStateList = new List<int>();
        BPConstructed = new List<int>();

        FloorStates = new List<int>();


        print("OB_names " + OB_names);
    }


    public bool ConstructedStructures_Contains(ObjectOnBoard ob)
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

    public void SetGrowStatueToMapGenObjects()
    {
        if (GenMap == null) return;

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

    public void SetGrowStatuesPreplaced()
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

}
