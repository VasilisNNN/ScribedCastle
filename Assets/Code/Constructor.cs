using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using System.Text;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;
using TMPro;




public class TilesOnBoard
{
    public int xPOS;
    public int yPOS;
    public string Name;

    public TilesOnBoard(int x_pos, int y_pos, string name)
    {
        xPOS = x_pos;
        yPOS = y_pos;
        Name = name;
    }
}

public class TrashOnBoard
{
    public Vector2 Place;
    public string Name;
    

    public TrashOnBoard(Vector2 place, string name)
    {
        Place = place;
        Name = name;
    }
}




public class Constructor : MonoBehaviour
{
    public float Game_SPEED { get; set; }

    public bool ChooseMouseObject { get; set;}
    public bool ChooseRightSideIcons { get; set; }

    public bool DEMO { get; private set; }


    public Tilemap Tile;

    public Tilemap TopScafolds;
    private Tilemap UnsettingTile;
    public Tilemap StartBlock;
    public Tilemap TileBlock;

    private Vector3Int UndeadFloorTile;

    private int XPos, YPos;

    public InputMode IM { get; private set; }
    public int MenuNumber { get; set; }
    public int CurrenQuest { get; set; }
    private int QuestPageNum,UnlockPage;

    private string[] TableNames, Tech, TreesNames, FloorNames;
    private string[] BrushesNames;
    private string[] PersNames, DecorNames, PlantsNames, DecorWallsNames;

   
    private Vector2 MouthPosPrev;
    public int Cost { get; set; }
    public int Money { get; set; }
    public int Crowded { get; set; }
    private int[] SaveMoney;
    public int Comfort { get; set; }
    public int ComfortMax, ComfortMin;
    public int TimerStay { get; set; }
    private int TimerStayMax;



    public int AllPeople { get; set; }
    public int AllTables { get; set; }

    private int AllPeopleMax = 30;
    private int AllTablesMax = 50;

    public int AllMeatCost { get; set; }
    public int AllVegCost { get; set; }
    public int AllBeerCost { get; set; }
    
    public int Humans { get; set; }
    public int Language { get; set; }


    public float OnUIDelay { get; set; }
    [HideInInspector]
    public List<GameObject> ClientsList = new List<GameObject>();
    [HideInInspector]
    public List<ObjectOnBoard> OBOnBoard = new List<ObjectOnBoard>();
    [HideInInspector]
    public List<ObjectOnBoard> ConstructedStructures = new List<ObjectOnBoard>();
    [HideInInspector]
    public List<GameObject> DroppedItems = new List<GameObject>();
    [HideInInspector]
    public List<TilesOnBoard> TOnBoard = new List<TilesOnBoard>();
   // [HideInInspector]
    //public List<TilesOnBoard> PitsOnBoard = new List<TilesOnBoard>();
    [HideInInspector]

    public List<ObjectOnBoard> Enemies = new List<ObjectOnBoard>();

    //private List<GameObject> MoneyDifference = new List<GameObject>();
    private float  BuildDelay, SetObjectDelay;

    //public List<int> UIBrushINT = new List<int>();

    public List<Item> Dishes { get; set; }
    private DishesDatabase DD;

    private AudioClip Place, QuestBook;

    public int Floors { get;  set; }
    public int KitchenFloors { get;  set; }
    public int Walls { get;  set; }
    public int Grounds { get;  set; }

    public int Plants { get;  set; }
    public int PlantFarms { get;  set; }

    public int Grass { get;  set; }
    public int Veggies { get;  set; }
    public int Squids { get;  set; }
    public int SoldiersCount { get; set; }
    public int EnemiesCount { get; set; }

    public int SoldiersCountMAX = 30;
    public int EnemiesCountMAX = 30;

    [HideInInspector]
    public List<string> UnlockedText = new List<string>();

    private Vector2 MinField = new Vector2(-10, -9);
    private Vector2 MaxField = new Vector2(10, 7);
    private Transform _transform;
    private float XShake, YShake;
    private bool _DestroyObject;

    public int MinimumWage { get; private set; }
    private bool show_questbook;
  
    public MenuCustom _menu { get; private set; }
    private bool Achunlocked, EnterHolding;

    private List<string> LogListEN = new List<string>();
    private List<string> LogListUA = new List<string>();
    private List<string> LogListJP = new List<string>();
    private List<GameObject> ObjectsToConnect = new List<GameObject>();
    private Text LogText;


    public int Salaries{ get; set; }
    private ButtonsDatabase BData;
    public int OnButtonID { get; set; }

    private List<GameObject> Tables = new List<GameObject>();


    public bool CreativeMode { get; set; }

    private Camera MainCamera;
    private TextMeshProUGUI SalariesOB, AllMoneyObject, AllComfortObject, AllTimeObject, AllMetalObject, AllStoneObject, AllWoodObject, AllPeopleObject, AllTablesObject, IncomeOB;
    public GameObject GodEncounter { get; set; }
    private GameObject QPage0, QPage1, QPage2, PlayButton, PauseButton, Play2Button; 
    private Transform CanvasTransform;
    private QuestDatabase QD;
 
 
    private int OnBoardCount;

    private Texture2D CursorT, CursorRightT, CursorLeftT, CursorUpT, CursorDownT;

    
    
    private long SaveEnd;


    private Vector2 ConstructorObjectPosition;

    private GameObject ExitBuildingMode,ConstructorButtons,  MoneyDifferenceOB, BeerDifferenceOB, MeatDifferenceOB, VegDifferenceOB , QuestPartOB, MenuQuestOB, TipsPause;

    public int MinIncome { get; set; }
    public int MaxIncome { get; set; }

    public SaveLoad SL { get; set; }
    [HideInInspector]
    public List<int> TutorialPhaseBigTip = new List<int>();
    private Image ToolTip_IMG;
    [HideInInspector]
    public TextMeshProUGUI ToolTip_Text { get; set; }
    private GameObject ChooseUI;
    [HideInInspector]
    public List<int> CollNum = new List<int>();
    [HideInInspector]
    public Player pl;
    private Inventory inv;
    public int[] ItemNeeded { get; set; }
    public int[] ItemNeededCount { get; set; }


    public bool Building { get; private set; }
    public bool TutorialPause { get; set; }


    public bool ShowChargeUI;
    private AstarPath APath;
    public bool GameStarted { get; set; }

    private string StartLayer = "Pers";
    private string ForG = "ItemFG";

    public Tilemap GreyMap { get; set; }
    public Tilemap GrassMap { get; set; }
    public Tilemap WaterMap { get; set; }
    public Tilemap PitsTileBase { get; set; }

    private Material StunMaterial;


    private Image PLAY_ButtonImage ;
    private Image PAUSE_ButtonImage ;
    private Image SPEED2_ButtonImage ;


    private int FloorsCount = 3;

    public bool CanScrollCamera;
    private int ParentLayerOrder, LayerPlus;
    private StatsControll FO;
    private ObjectOnBoard[] OBOnBoardArray;
    private float distanceX, distanceY, orthographicSizeTimes2_5, minimalalpha;

    private PubObject PO;

    public MouseController MouseC { get; private set; }

    public int LastBuildingConstructed { get; private set; }

    public int MaxPoop { get; private set; }
    public int AllPoop { get; set; }
    public int MaxTrash { get; private set; }
    public int AllTrash { get; set; }
    private Material WhiteMaterial;

    private List<ObjectOnBoard> batch;
    private ObjectOnBoard batchpart;


    private float gridSize = 4; // Adjust this based on your object density and radius
    private Dictionary<Vector2Int, List<ObjectOnBoard>> grid = new Dictionary<Vector2Int, List<ObjectOnBoard>>();
    
    public List<ObjectOnBoard> objectsInRange = new List<ObjectOnBoard>();
    private Vector2Int playerGridPosition, gridPosition;
    private Vector2 distance;

    public float UpdateInRange = 10;
    public int TileMax = 300;

    public bool PlaceWaterOnTileDestroy;
    public TileBase WaterBase;

    private int TopObjectsMax = 3;
    private bool isRandomised;
    public int RandomisedNumber { get; set; }

    private GameObject BuildEffect, DemolishEffect;
    private TextDatabase textdatabase;

    void Start()
    {
    
        gameObject.AddComponent<TextDatabase>();
        textdatabase = GetComponent<TextDatabase>();
        
        AllPoop = 20;
        AllTrash = 20;

        SalariesOB = GameObject.Find("Salaries").GetComponent<TextMeshProUGUI>();
        ExitBuildingMode = GameObject.Find("ExitBuildingMode");

        PlayButton = GameObject.Find("PLAY_Button");
        PauseButton = GameObject.Find("PAUSE_Button");
        Play2Button = GameObject.Find("SPEED2_Button");

        PLAY_ButtonImage = GameObject.Find("PLAY_Button").GetComponent<Image>();
           PAUSE_ButtonImage = GameObject.Find("PAUSE_Button").GetComponent<Image>();
           SPEED2_ButtonImage = GameObject.Find("SPEED2_Button").GetComponent<Image>();

        StunMaterial = Resources.Load<Material>("Materials/DoodleHorizontal");
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");
        GreyMap = GameObject.Find("Grid").transform.Find("GreyGround").GetComponent<Tilemap>();
        GrassMap = GameObject.Find("Grid").transform.Find("Grass").GetComponent<Tilemap>();
        WaterMap = GameObject.Find("Grid").transform.Find("Water").GetComponent<Tilemap>();

        PitsTileBase = GameObject.Find("Grid").transform.Find("PitsTileBase").GetComponent<Tilemap>();


        ConstructorButtons = GameObject.Find("ConstructorButtons");

        APath = GameObject.Find("PathFinding").GetComponent<AstarPath>();

        ChooseUI = GameObject.Find("ChooseUI");
        pl = GameObject.Find("Player").GetComponent<Player>();
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        SL = GetComponent<SaveLoad>();

        TipsPause = GameObject.Find("TipsPause");
        MoneyDifferenceOB = Resources.Load<GameObject>("Prefabs/UI/MoneyDifference");
        BeerDifferenceOB = Resources.Load<GameObject>("Prefabs/UI/BearDifference");
        MeatDifferenceOB = Resources.Load<GameObject>("Prefabs/UI/MeatDifference");
        VegDifferenceOB = Resources.Load<GameObject>("Prefabs/UI/VegDifference");
        QuestPartOB = Resources.Load<GameObject>("Prefabs/UI/QuestPart");
        MenuQuestOB = Resources.Load<GameObject>("Prefabs/UI/MenuQuest");


        _menu = GetComponent<MenuCustom>();



        CursorT = Resources.Load<Texture2D>("Sprites/UI/Cursor");
        CursorRightT = Resources.Load<Texture2D>("Sprites/UI/CursorRight");
        CursorLeftT = Resources.Load<Texture2D>("Sprites/UI/CursorLeft");

        CursorUpT = Resources.Load<Texture2D>("Sprites/UI/CursorUp");
        CursorDownT = Resources.Load<Texture2D>("Sprites/UI/CursorDown");
        
        //MAYBE CAUSES A BUG ON SECOND LOAD

        Debug.Log("STARTED");
        



        QD = GameObject.Find("QuestDatabase").GetComponent<QuestDatabase>();
      
        CanvasTransform = GameObject.Find("Canvas").transform;
        AllMoneyObject = GameObject.Find("AllMoney").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("AllComfort")!=null)
        AllComfortObject =  GameObject.Find("AllComfort").GetComponent<TextMeshProUGUI>();
       
        AllPeopleObject = GameObject.Find("AllPeople").GetComponent<TextMeshProUGUI>();
        AllTablesObject = GameObject.Find("AllTables").GetComponent<TextMeshProUGUI>();

        QPage0 = GameObject.Find("QPage0");
        QPage1 = GameObject.Find("QPage1");
        QPage2 = GameObject.Find("QPage2");

        if (GameObject.Find("GodEncounter") != null)
        {
            GodEncounter = GameObject.Find("GodEncounter");
        }



        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

         Game_SPEED = 1;

     
        BData = GameObject.Find("ButtonsDatabase").GetComponent<ButtonsDatabase>();
        ToolTip_IMG = GameObject.Find("ToolTip").GetComponent<Image>();
        ToolTip_Text = GameObject.Find("ToolTipText").GetComponent<TextMeshProUGUI>();


        AllMeatCost = 200;
        AllVegCost = 200;
        AllBeerCost = 200;

        MinimumWage = 0;
      
        DEMO = false;
        _transform = transform;
        Place = Resources.Load<AudioClip>("Sound/UI/Build");
        QuestBook = Resources.Load<AudioClip>("Sound/UI/Button2");

      
        Dishes = new List<Item>();

        int dishcount = 0;
        for (int i = 0; i < inv.database.items.Count; i++)
        {
            if (inv.database.items[i].Dish)
            {
                Dishes.Add(inv.database.items[i]); dishcount++;
            }

            if (dishcount > 2) break;
        }

        TimerStay = 20;
    

              ComfortMax = 50;
        ComfortMin = -20;
        TimerStayMax = 50;

     
        FloorNames = new string[2] { "Floor", "FloorKitchen" };
        TableNames = new string[5] { "Table_0", "Table_1", "Table_2", "Table_3", "Bar" };
        BrushesNames = new string[4] { "Brush_0", "Brush_1", "Brush_2", "Brush_3" };
        PersNames = new string[4] { "Pers_0", "Pers_1", "Pers_2", "Pers_3" };
        DecorNames = new string[3] { "Decor_0", "Decor_1", "Decor_2"};
        DecorWallsNames = new string[9] { "DecorWall_0", "DecorWall_1", "DecorWall_2", "DecorWall_3", "DecorWall_4", "DecorWall_5", "DecorWall_6", "DecorWall_7", "DecorWall_8" };

        Tech = new string[3] { "Oven", "Veg", "Meat"};
        
        PlantsNames = new string[2] { "Tree_0", "Tree_1" };
        IM = GetComponent<InputMode>();

        TipsPause.SetActive(false);
        if(GameObject.Find("Gameover")!=null)
            inv.ONOFF( GameObject.Find("Gameover"), false);
        

        for (int i = 0; i < 10; i++)
        {
            inv.ONOFF( GameObject.Find("QuestPart" + i), false);
        }

        inv.ONOFF(GameObject.Find("QuestBookBG2Text"), false);
        inv.ONOFF(GameObject.Find("QuestBookBG2"), false);
        inv.ONOFF(GameObject.Find("QuestBookBG"), false);
        inv.ONOFF( QPage0, false);
        inv.ONOFF(QPage1, false);
        inv.ONOFF( QPage2, false);
     
        UndeadFloorTile = new Vector3Int(0, 0, 0);
       

        for (int x = -30; x < 30; x++)
        {

            for (int y = -30; y < 30; y++)
            {
                /*if (PitsTileBase.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    PitsOnBoard.Add(new TilesOnBoard(x, y, Tile.GetTile(new Vector3Int(x, y, 0)).name));

                }*/

                if (Tile.GetTile(new Vector3Int(x,y,0)) != null)
                {
                    
                    TOnBoard.Add(new TilesOnBoard(x, y, Tile.GetTile(new Vector3Int(x, y, 0)).name));

                    if (_menu.FirstStart == 0) Floors++;

                }
                
            }
            
        }

        
        
       DeActivateBuilding();
   
        GameStarted = true;
        
        MouseC = GameObject.Find("MouseOB").GetComponent<MouseController>();

        
        PopulateGrid();
        UpdateObjectsInRange();

        inv.ONOFF(ExitBuildingMode, false);
    }



   void Update()
    {
 
        if (AllPeople < 0) AllPeople = 0;
        if (AllTables < 0) AllTables = 0;
        
       DirectControlls();
        
        if (!SL.Saving && !SL.Loading)
        {
            ExplosionDestroy();
           
            UIVARS();
            GAMESPEEDMENU();

            
            PopulateGrid();
            UpdateObjectsInRange();

         
            
            BuildingDestroingApplyBrush();

            
        }





    }


    private void PopulateGrid()
    {
        grid.Clear();

        for (int i = 0; i < OBOnBoard.Count; i++)
        {
            if (OBOnBoard[i].Object != null)
            {
                gridPosition = GetGridPosition(OBOnBoard[i].Object.transform.position);
                if (!grid.ContainsKey(gridPosition))
                {
                    grid[gridPosition] = new List<ObjectOnBoard>();
                }
                grid[gridPosition].Add(OBOnBoard[i]);
            }

        }
    }

    private Vector2Int GetGridPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x / gridSize), Mathf.FloorToInt(position.y / gridSize));
    }

    private void UpdateObjectsInRange()
    {
        objectsInRange.Clear();

         playerGridPosition = GetGridPosition(MainCamera.gameObject.transform.position);

        for (int xOffset = -2; xOffset <= 2; xOffset++)
        {
            for (int yOffset = -2; yOffset <= 2; yOffset++)
            {
               gridPosition = new Vector2Int(playerGridPosition.x + xOffset, playerGridPosition.y + yOffset);

                if (grid.TryGetValue(gridPosition, out List<ObjectOnBoard> objectsInGrid))
                {
                    for (int i = 0; i < objectsInGrid.Count; i++)
                    {
                        distance =  MainCamera.gameObject.transform.position - objectsInGrid[i].Object.transform.position;

                       
                        if (Mathf.Abs(distance.x) <= UpdateInRange || Mathf.Abs(distance.y) <= UpdateInRange)
                        {
                            if (!objectsInRange.Contains(objectsInGrid[i]))
                            {
                                objectsInRange.Add(objectsInGrid[i]);
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < objectsInRange.Count; i ++)
        {
            batchpart = objectsInRange[i];
            ObjectOnBoardControll(ref batchpart);
        }


      //  SequentialBatchProcessing(ref objectsInRange, 100);

        // Now you have a smaller list "objectsInRange" containing objects within the radius of 3 units from the player
        // You can do whatever you need with this list
    }

    void SequentialBatchProcessing(ref List<ObjectOnBoard> list, int batchSize)
    {
        for (int i = 0; i < list.Count; i += batchSize)
        {
            batch = list.Skip(i).Take(batchSize).ToList();
            ProcessBatch(batch);
        }
    }

    void ProcessBatch(List<ObjectOnBoard> batch)
    {
        for (int i = 0; i < batch.Count; i++)
        {
            batchpart = batch[i];
            ObjectOnBoardControll(ref batchpart);
        }
    }



    void ObjectOnBoardControll(ref ObjectOnBoard destructibleOB)
    {
      
        if (destructibleOB == null || destructibleOB.Object == null) return;

        FO = destructibleOB.Stats;

        if (FO == null) return;

        distanceX = Mathf.Abs(FO.transform.position.x - pl.MainCamera.transform.position.x);
        distanceY = Mathf.Abs(FO.transform.position.y - pl.MainCamera.transform.position.y);

       // distanceX = distanceY = 1;
        orthographicSizeTimes2_5 = UpdateInRange;

        
        PO = destructibleOB.PO;

       
        if (distanceX >= orthographicSizeTimes2_5 && distanceY >= orthographicSizeTimes2_5)
        {
            if (PO != null)
            {
                if (PO.Draw && FO.GetComponent<DrawIfActive>() == null)
                {
                   pl.inv.ONOFF( FO.gameObject, false);
                    PO.Draw = false;
                }
                

                return;
            }
        }
        else if (distanceX < orthographicSizeTimes2_5 || distanceY < orthographicSizeTimes2_5)
        {
          
            if (FO.MaxVision >= pl.Vision && FO.MinVision <= pl.Vision && FO.MaxSniff >= pl.Sniff && FO.MinSniff <= pl.Sniff)
            {
                // Perform updates on object
                OBONBoardUpdate(destructibleOB);
               FO.HungerConroll();
               FO.PaymentConroll();
                if (FO.GettingDamageFromWalls) FO.WallDamage();

                // FO.PoopConroll();



                // Handle object collisions and vision draw
                if (pl.coll_obj.Contains(FO.gameObject))
                {
                    if (!pl.CollidingCharacter.Contains(FO.gameObject) && FO.Stunned)
                        pl.CollidingCharacter.Add(FO.gameObject);

                    if (!FO.AudioPlayed)
                    {
                        FO.CollisionAudio();
                        FO.AudioPlayed = true;
                    }
                }
                else
                {
                    if (pl.CollidingCharacter.Contains(FO.gameObject))
                        pl.CollidingCharacter.Remove(FO.gameObject);

                    if (FO.AudioPlayed)
                        FO.AudioPlayed = false;
                }



               if (PO != null)
                {
                    if (!PO.Draw && FO.GetComponent<DrawIfActive>() == null)
                    {
                        pl.inv.ONOFF(FO.gameObject, true);
                        PO.Draw = true;
                    }
                }
            }
            
            VisionDraw(ref FO);

           FO.GrowControll();

            if (FO.Draw)
                ObjectColorAlpha(ref FO);

        
        }

        if (FO.HP <= 0)
            FO.ObjectsDeath();

        if (FO.GrowTimer < Time.fixedTime && FO.GrowingSprites.Length > 0)
        {
            if (FO.CurrentGrowState < FO.GrowingSprites.Length - 1)
            {
                FO.CurrentGrowState++;
                FO.GrowTimer = Time.fixedTime + FO.GrowDelay;
            }
        }

  
            
        
        
    }

    void DestroyTiles()
    {

        if (!IM.RightMouseButton && !IM.delete_b)
        {
           
            UnsettingTile = null;
            return;
        }

     
        if (UnsettingTile == null && IM.ActionDelay < Time.fixedTime)
        {

            if (TopScafolds.GetTile(new Vector3Int(XPos, YPos - 1, 0)) != null)
            {
                UnsettingTile = TopScafolds;

            }
            else
            {

                if (Tile.GetTile(new Vector3Int(XPos, YPos - 1, 0)) != null)
                {
                    UnsettingTile = Tile;

                }
            }
        }
       

        if (UnsettingTile != null && !_DestroyObject && !ChooseMouseObject && Building)
         UnSetTile(UnsettingTile);
        
    }

    void BuildingDestroingApplyBrush()
    {
        if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().pitch = 1;

        if (TutorialPause || _menu.MenuONOFF || pl.inv.blueprintshow || pl.inv.crafting) return;

       
        DestroyTiles();


     

        PubObject ChildPubObject = null;

        if (_transform.childCount>0)
        ChildPubObject = _transform.GetChild(0).GetComponent<PubObject>();

        if (OnUIDelay < Time.fixedTime && IM.ActionDelay < Time.fixedTime && Building)
        {

               
            if (_transform.childCount > 0 && ChildPubObject._TileBase != null && ChildPubObject.floors > 0)
            {
                
                if (pl.inv.CheckItem(OnButtonID))
                {
                    if (ChildPubObject.MAPS != null)
                        ConstructTile(0, ChildPubObject._TileBase, pl.inv.GetItemInDatabase(OnButtonID).TargetTileMap);
                    else
                        ConstructTile(0, ChildPubObject._TileBase, pl.inv.GetItemInDatabase(OnButtonID).TargetTileMap);
                        


                }
                else
                {

                    if (!pl.inv.CheckItem(OnButtonID) && _transform.childCount > 0)
                    {
                        DeActivateBuilding();

                    }
                }

            }


        }

        
        if (IM.delete_b || IM.RightMouseButton)
        {

            if (IM.ActionDelay < Time.fixedTime && !ChooseMouseObject && Building)
            {
             
                UnSetObjects();
                _DestroyObject = true;
                IM.ActionDelay = Time.fixedTime + 0.3f;

            }

        }
        else _DestroyObject = false;

        if (!Building)
        {
            return;
        }


        if (_transform.childCount <= 0)
        {
            return;
        }
        
        if (_transform.GetChild(0) == null)
        {
            return;
        }
   


        if (OnUIDelay < Time.fixedTime && pl.inv.CheckItem(OnButtonID))
        {

            if (ChildPubObject.floors <= 0 && _transform.GetChild(0) != null)
            {
  
                ConstructObject(pl.inv.GetItemInDatabase(_transform.GetChild(0).GetComponent<StatsControll>().DatabaseID).TargetTileMap,
                    pl.inv.GetItemInDatabase(_transform.GetChild(0).GetComponent<StatsControll>().DatabaseID).TargetBrush);
            }
        }
        else
        {
           
            //----------------------------- Destroy Child to build when there is no item in the inventory--------------------------------


            if (!pl.inv.CheckItem(OnButtonID) && _transform.childCount > 0)
            {
                DeActivateBuilding();

            }
            //---------------------------------------------------------------------------------------------------

            SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));
        }
        
        
    }

    void OBONBoardUpdate(ObjectOnBoard ONBoard)
    {

        if (ONBoard == null) return;
         if (ONBoard.Object == null) return;
        


        if (Mathf.Abs(ONBoard.Object.transform.position.x - pl.transform.position.x) < UpdateInRange && Mathf.Abs(ONBoard.Object.transform.position.y - pl.transform.position.y) < UpdateInRange)
        {


            if (ONBoard.PO != null)
            {
                //PROBLEM!!!!!
                ONBoard.PO.PubObjectUpdate();
                ONBoard.PO.PubObjectTimers();
            }


            if (ONBoard.Stats != null)
            {
            ONBoard.Place = ONBoard.Object.transform.position;
            }
        }
           
    }




    private void FixedUpdate()
    {
        ToolTipControl();
        
    }



    void UIVARS()
    {
        if (Comfort > ComfortMax) Comfort = ComfortMax;
        if (Comfort < ComfortMin) Comfort = ComfortMin;


        if (pl.inv.GetItem(9) != null)
            Money = pl.inv.GetItem(9).Count;
        else Money = 0;

        AllMoneyObject.text = Money.ToString();

        if (AllMoneyObject.transform.localScale.x != 1) AllMoneyObject.transform.localScale =
            new Vector3(Mathf.Lerp(AllMoneyObject.transform.localScale.x,1,Time.deltaTime*3),
                        Mathf.Lerp(AllMoneyObject.transform.localScale.y, 1, Time.deltaTime * 3), 1);

        if (AllComfortObject!=null)
        AllComfortObject.GetComponent<TextMeshProUGUI>().text = Comfort.ToString() + " / " + ComfortMax;
        // AllTimeObject.GetComponent<TextMeshProUGUI>().text = TimerStay.ToString() + " / " + TimerStayMax;

        AllPeopleObject.text = AllPeople + " / " + AllPeopleMax;
        AllTablesObject.text = AllTables + " / " + AllTablesMax;


        //  CrowdedControll();
        Language = _menu.Language;

        if (pl.menu.Language == 0)
        {
            if (SalariesOB != null)
                SalariesOB.text = "Salaries: " + Salaries;

            if (IncomeOB != null)
                IncomeOB.text = "Income: \n" + MinIncome + " up to " + MaxIncome;
        }

        if (pl.menu.Language == 1)
        {
            if (SalariesOB != null)
                SalariesOB.text = "Зарплати: " + Salaries;

            if (IncomeOB != null)
                IncomeOB.text = "Прибуток: \n" + MinIncome + " до " + MaxIncome;
        }

        if (pl.menu.Language == 2)
        {
            if (SalariesOB != null)
                SalariesOB.text = "給与: " + Salaries;

            if (IncomeOB != null)
                IncomeOB.text = "利益: \n" + MinIncome + " - " + MaxIncome;
        }

        if (TutorialPause)
        {
            if (!IM.joystick)
            {
                if ((MouseC.UIColl(GameObject.Find("CloseBigTips")) && IM.LeftMouseButtonDown) || IM.exit_b || IM.menu_b || IM.enter_b)
                {
                    UnsetBigTips();
                }

            }
            else
            {
                if (( IM.menu_b || IM.exit_b || IM.enter_b) && TipsPause.activeInHierarchy && IM.ActionDelay < Time.fixedTime)
                {

                    TipsPause.SetActive(false);
                    TutorialPause = false;

                    OnUIDelay = Time.fixedTime + 0.1f;
                    IM.ActionDelay = Time.fixedTime + 0.1f;
                    SetObjectDelay = Time.fixedTime + 1;
                }



            }
        }



        /*
        if ((IM.menu_b || IM.exit_b) && ChooseMouseObject)
        {
            print("ChooseMouseObject    asdfljkaflkasjfljas");
            _menu.ActionDelay = Time.fixedTime + 1;
            ChooseMouseObject = false;
        }*/


        
        if (AllMeatCost < 0) AllMeatCost = 0;
            if (AllVegCost < 0) AllVegCost = 0;
            if (AllBeerCost < 0) AllBeerCost = 0;
        
    }

    private void LateUpdate()
    {
        MouthPosPrev = Input.mousePosition;
    }

    void ObjFlip(GameObject Child, ref int LayerPlus, string LLayer, int ParentLayerOrder, int i, int pluslayer)
    {
        if (Child.GetComponent<SpriteRenderer>() == null)
        return;
        
        Child.GetComponent<SpriteRenderer>().sortingLayerName = LLayer;
        Child.GetComponent<SpriteRenderer>().sortingOrder = ParentLayerOrder + (1 * i + 2);

        LayerPlus = ParentLayerOrder + (1 * i + 2);
        
    }



    void LayerFlip(GameObject obj)
    {

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        if (obj.tag != "Flipping")
        {
            spriteRenderer.sortingLayerName = "Default";
            spriteRenderer.sortingOrder = -900;
            return;
        }


        BoxCollider2D playerBox = pl.GetComponent<BoxCollider2D>();
        BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();

        string lLayer = "";

        if (boxCollider.bounds.min.y < playerBox.bounds.min.y)
            lLayer = ForG;
        else
            lLayer = StartLayer;

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = lLayer;
            spriteRenderer.sortingOrder = (int)(obj.transform.position.y * -200) + 5;
        }



        SetChildLayer(obj, lLayer, spriteRenderer.sortingOrder, 2);

        
    }

    void SetChildLayer(GameObject obj, string layerName, int parentSortingOrder, int childCount)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;

            if (!child.name.Contains("Base"))
            {
                ObjFlip(child, ref parentSortingOrder, layerName, parentSortingOrder, i, childCount);
                SetChildLayer(child, layerName, parentSortingOrder, childCount + 2);
            }
        }
    }



    void ConstructTile(int n, TileBase Brush, Tilemap Map)
        {

        int FinalXPos = XPos ;
        int FinalYPos = YPos -1;
        bool canbuild = false;

        if (!IM.enter_b && !IM.enter_b_hold) EnterHolding = false;

        if (GetComponent<CollList>().GetCollList().Contains(GameObject.Find("CloseBigTips")) || GetComponent<CollList>().GetCollList().Contains(QPage0) ||
                GetComponent<CollList>().GetCollList().Contains(QPage1) || GetComponent<CollList>().GetCollList().Contains(QPage2) || ChooseRightSideIcons)
        {
            return;
        }

  
        if (MenuNumber != n || _menu.MenuONOFF || show_questbook || EnterHolding)
        {
            return;
        }

        print("ConstructTile 0");
        //------------------Conditions of the Building block ---------------------//

        if (GameObject.Find(_transform.position.x + "_" + _transform.position.y) == null)
        {

         
                int c = 0;
                for (int x = XPos - 1; x < XPos + 2; x++)
                for (int y = YPos - 2; y < YPos + 1; y++)
                {
                    if (Map.GetTile(new Vector3Int(x, y, 0)) != null)
                    {
                        c++;
                    }
                }

            print("ConstructTile 00 " + Map.name + "   " + c);


            if ((( TOnBoard.Count< TileMax && Map != PitsTileBase) /*|| (PitsOnBoard.Count < TileMax && Map == PitsTileBase)*/)&&

                    // ------------------NeighbourRules--------------------------------//
                    
            ((c >0 && Floors > 1 && Map == Tile) || (Map == Tile && Floors <= 1) || (c > 0 && Map != Tile)) && 

                StartBlock.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) == null && 
                TileBlock.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) == null && 

                ((Map.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) == null && Map != GreyMap && GreyMap.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) != null) || 
                (Map.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) == null && Map == GreyMap )))
                {
                    SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(0.1f, 1, 0.1f, 0.7f));
           
                print("ConstructTile 1");
                canbuild = true;
                }
                else SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));

            if (Map.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) == null && Map == PitsTileBase && GreyMap.GetTile(new Vector3Int(FinalXPos, FinalYPos, 0)) != null)
            {

                SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(0.1f, 1, 0.1f, 0.7f));
                canbuild = true;
            }


        }
        //----------------------------------------------------


        int s = 0;
        bool setbrush = false;

        print("ConstructTile 2");
        if (ItemNeeded != null)
        {
            if (ItemNeeded.Length > 0)
            {
                for (int i = 0; i < ItemNeeded.Length; i++)
                {
                    if (pl.inv.GetItem(ItemNeeded[i]) != null)
                    {
                        if (pl.inv.GetItem(ItemNeeded[i]).Count >= ItemNeededCount[i])
                        {
                            s++;

                        }
                    }


                }

                if (s != ItemNeeded.Length) setbrush = false;
                else setbrush = true;
            }
            else setbrush = true;
        }
        else setbrush = true;

        
        if ((!IM.enter_b && !IM.LeftMouseButton && !IM.enter_b_hold) || SetObjectDelay > Time.fixedTime || !setbrush)
        {
            return;
        }

        print("ConstructTile 3 " + SetObjectDelay + " / " + Time.fixedTime + setbrush);


        if (!canbuild)
        return;

        if ((TOnBoard.Count >= TileMax && Map != PitsTileBase) /*|| (PitsOnBoard.Count >= TileMax && Map == PitsTileBase)*/) return;



        print("ConstructTile 4");
        PubObject ChildPubObject = _transform.GetChild(0).GetComponent<PubObject>();
        StatsControll _StatsControll = _transform.GetChild(0).GetComponent<StatsControll>();





        if (ItemNeeded != null)
        {
            for (int i = 0; i < ItemNeeded.Length; i++)
            {

                // if (PO.ItemNeededCount[i] <= pl.inv.GetItemInDatabase(PO.ItemNeeded[i]).Count)

                print("reduce item");
                if (pl.inv == null) print("pl.inv null");

                pl.inv.ReduceItemCount(ItemNeeded[i], ItemNeededCount[i]);

            }
        }


        print("ConstructTile 5");
        PlaySound(Place, 1);

        Map.SetTile(new Vector3Int(FinalXPos, FinalYPos, 0), Brush);

        if (PlaceWaterOnTileDestroy || Map == GreyMap)
            WaterMap.SetTile(new Vector3Int(XPos, YPos - 1, 0), null);


        pl.RescanInBounds(new Bounds(new Vector3Int(FinalXPos, FinalYPos, 0), new Vector3Int(5, 5, 0)));
        

      //  if(Map!=PitsTileBase)
        TOnBoard.Add(new TilesOnBoard(new Vector3Int(FinalXPos, FinalYPos, 0).x, new Vector3Int(FinalXPos, FinalYPos, 0).y, Brush.name));
        /*else
            PitsOnBoard.Add(new TilesOnBoard(new Vector3Int(FinalXPos, FinalYPos, 0).x, new Vector3Int(FinalXPos, FinalYPos, 0).y, Brush.name));
        */
        Floors += ChildPubObject.floors;

        KitchenFloors += ChildPubObject.kitchenfloors;
        Grounds += ChildPubObject.ground;
        Walls += ChildPubObject.wall;
                                         

            pl.inv.ReduceItemCount(OnButtonID, 1);

            IM.ActionDelay = Time.fixedTime + 0.1f;


        SpendMoney(_transform.position, pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).BuildingCost);

        print("ConstructTile 6");

        LastBuildingConstructed = OnButtonID;
        SetObjectDelay = Time.fixedTime + 0.1f;
        


    }

    void UnSetTile(Tilemap Map)
    {

        
        GameObject OBOnTile = null;

        for (int i = 0; i < OBOnBoard.Count; i++)
        {
               
            if (OBOnBoard[i].Object != null)
            {

                if (new Vector2(OBOnBoard[i].Object.transform.position.x, OBOnBoard[i].Object.transform.position.y) == ConstructorObjectPosition)
                {
                    OBOnTile = OBOnBoard[i].Object;
                    break;
                }

            }
        }



        if (OBOnTile != null || Map.GetTile(new Vector3Int(XPos, YPos - 1, 0)) == null 
             || _menu.MenuONOFF || show_questbook || ChooseRightSideIcons )
            return;

        /*if (Floors <= 2)
        {
            AddLogPartOnes("Can not delete last floor tile", "Не можна видалити останню підлогу", "最後のフロアタイルを削除できない", gameObject);
        }*/


        Floors--;

     
        if (Map.GetTile(new Vector3Int(XPos, YPos - 1, 0)) == SL.KitchenBrush[0] && _transform.childCount > 0)
        KitchenFloors -= _transform.GetChild(0).GetComponent<PubObject>().kitchenfloors;
        StatsControll _StatsControll = _transform.GetChild(0).GetComponent<StatsControll>();


        PlaySound(Place, 0.8f);
        AddMoney(_transform.position, pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).BuildingCost);
        


        for (int i = 0; i < TOnBoard.Count; i++)
        {
            if(XPos == TOnBoard[i].xPOS && (YPos-1) == TOnBoard[i].yPOS)
                TOnBoard.RemoveAt(i);
        }

        PitsTileBase.SetTile(new Vector3Int(XPos, YPos - 1, 0), null);

        Map.SetTile(new Vector3Int(XPos, YPos - 1, 0), null);

        if(PlaceWaterOnTileDestroy)
        WaterMap.SetTile(new Vector3Int(XPos, YPos - 1, 0), WaterBase);


        GameObject DestroyedOB = FindConstructedObject(ConstructorObjectPosition.x + "_" + ConstructorObjectPosition.y);
        _DestroyObject = false;




        IM.ActionDelay = Time.fixedTime + 0.1f;
        

        
    }

    void UnSetObjects()
    {

        GameObject ObjectToDestory = null;

        for (int i = 0; i < OBOnBoard.Count; i++)
        {
         
            if (OBOnBoard[i].Object != null)
            {
                if (new Vector2(OBOnBoard[i].Object.transform.position.x, OBOnBoard[i].Object.transform.position.y) == ConstructorObjectPosition && 
                    (OBOnBoard[i].Object.transform.parent==null || (OBOnBoard[i].Object.transform.parent!=null && OBOnBoard[i].Object.transform.parent.GetComponent<StatsControll>()==null)))

                {

                    ObjectToDestory = OBOnBoard[i].Object;
          
                    break;
                }
            }


        }


        if (ObjectToDestory == null || _menu.MenuONOFF || show_questbook || ChooseRightSideIcons)
            return;


     
        if (ObjectToDestory.GetComponent<PubObject>() != null || (ObjectToDestory.GetComponent<PubObject>() != null && ObjectToDestory.GetComponent<MovementControll>() != null && !ObjectToDestory.GetComponent<MovementControll>().Enemy))
        {
            PubObject POPO = ObjectToDestory.GetComponent<PubObject>();
            
            Floors -= POPO.floors;
            KitchenFloors -= POPO.kitchenfloors;
            if(Comfort> ComfortMin)
            Comfort -= POPO.ComfortPlus;
            if(TimerStay>-TimerStayMax)
            TimerStay -= POPO.TimePlus;
            Walls -= POPO.wall;
            Grounds -= POPO.ground;
            
            AllTables -= POPO.tables;
            AllPeople -= POPO.people;

            if (POPO.ItemNeeded != null)
            {
                if (POPO.ItemNeeded.Length > 0)
                {
                    for (int i = 0; i < POPO.ItemNeeded.Length; i++)
                        pl.inv.AddItem(POPO.ItemNeeded[i], POPO.ItemNeededCount[i], pl.inv.GetItemInDatabase(POPO.ItemNeeded[i]).Durability, pl._transform.position);
                }
            }


            if (POPO.GetComponent<MovementControll>() != null)
            {
                if (POPO.tag == "Pers" && !POPO.GetComponent<MovementControll>().Soldier && !POPO.GetComponent<MovementControll>().Enemy) AllPeople--;
                if (POPO.GetComponent<MovementControll>().Soldier && !POPO.GetComponent<MovementControll>().Enemy) SoldiersCount--;
            }

            if (ObjectToDestory.GetComponent<Enemies>() != null)
            ObjectToDestory.GetComponent<Enemies>().DestoryEnemies();
            

            if (Tables.Contains(POPO.gameObject)) Tables.Remove(POPO.gameObject);



        

            Remove_PERS_FromOBOnBoard(ObjectToDestory);
            Remove_PERS_ConstructedObjects(ObjectToDestory);
            
           
        }



        AddMoney(new Vector3(0,0,0),(int)(pl.inv.GetItemInDatabase(ObjectToDestory.GetComponent<StatsControll>().DatabaseID).BuildingCost * 0.8f));

        if (ObjectToDestory == null) return;

        if (ObjectToDestory.GetComponent<PubObject>() == null) return;


        if (DemolishEffect == null)
        DemolishEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/DemolishEffect"));

        DemolishEffect.GetComponent<AnimationFrame>().ResetAnimation();
        DemolishEffect.GetComponent<AudioSource>().Play();

    
        if (ObjectToDestory.GetComponent<PubObject>().TopObjectsCount <= 0 || ObjectToDestory.GetComponent<PubObject>().TopObjectsCount >= 99)
        {
                   
            RemoveFromOBOnBoard(ObjectToDestory);
            RemoveFrom_ConstructedObjects(ObjectToDestory);
            pl.inv.AddItemNOAUDIO(ObjectToDestory.GetComponent<StatsControll>().DatabaseOriginalID, 1, pl.inv.GetItemInDatabase(ObjectToDestory.GetComponent<StatsControll>().DatabaseID).Durability, ObjectToDestory.transform.position);

            DemolishEffect.transform.position = ObjectToDestory.transform.position;
            PlaySound(Place, UnityEngine.Random.Range(0.4f, 0.5f));

            Destroy(ObjectToDestory);
      
            pl.RescanInBounds(new Bounds(new Vector3(ObjectToDestory.transform.position.x, ObjectToDestory.transform.position.y, 0), new Vector3(10, 10, 0)));
            
        }
        else
        {
            RemoveFromOBOnBoard(ObjectToDestory.transform.GetChild(ObjectToDestory.transform.childCount - 1).gameObject);
            RemoveFrom_ConstructedObjects(ObjectToDestory.transform.GetChild(ObjectToDestory.transform.childCount - 1).gameObject);

            pl.inv.AddItemNOAUDIO(ObjectToDestory.transform.GetChild(ObjectToDestory.transform.childCount - 1).gameObject.GetComponent<StatsControll>().DatabaseOriginalID, 1, pl.inv.GetItemInDatabase(ObjectToDestory.transform.GetChild(ObjectToDestory.transform.childCount - 1).gameObject.GetComponent<StatsControll>().DatabaseID).Durability, ObjectToDestory.transform.position);

            DemolishEffect.transform.position = ObjectToDestory.transform.GetChild(ObjectToDestory.transform.childCount - 1).gameObject.transform.position;

            Destroy(ObjectToDestory.transform.GetChild(ObjectToDestory.transform.childCount - 1).gameObject);

             PlaySound(Place, 1 - ObjectToDestory.transform.childCount * ((1 - 0.55f) / TopObjectsMax));

            

         

            pl.RescanInBounds(new Bounds(new Vector3(ObjectToDestory.transform.position.x, ObjectToDestory.transform.position.y, 0), new Vector3(10, 10, 0)));
            
            ObjectToDestory.GetComponent<PubObject>().TopObjectsCount--;
        }

   

        

    }


    void CheckObjectOnGround(ref GameObject ObjectOnGround, ref GameObject TopOnGround, ref GameObject DecorationOnGround)
    {
      //  if (ObjectOnGround == null) return;

        for (int i = 0; i < OBOnBoard.Count; i++)
        {
            ObjectOnBoard ob = OBOnBoard[i];

          
            GameObject obj = ob.Object;
            if (obj != null )
            if (ob.Place == ConstructorObjectPosition && obj.transform.parent != _transform && (obj.transform.parent == null || (obj.transform.parent != null && obj.transform.parent.GetComponent<StatsControll>()==null)))
            {
                ObjectOnGround = obj;
                PubObject pubObject = obj.GetComponent<PubObject>();

                if (pubObject != null)
                {
                    if (pubObject.TopObject)
                    {
                        TopOnGround = obj;
                    }
                    else if (pubObject.decoration)
                    {
                        DecorationOnGround = obj;
                    }
                }

                return;
            }
        }

        ObjectOnGround = null;

    }



    void ModifyObjectToBuild_CorrespondingToParentObject(StatsControll _statsControll, ref GameObject ObjectOnGround, ref GameObject objecttobuild, ref PubObject _PubObject)
    {
        objecttobuild.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        if (ObjectOnGround == null)
        {
            objecttobuild.transform.position = ConstructorObjectPosition;


            if (_PubObject != null)
            {
                if (_PubObject.wall > 0 && !_PubObject.TransperentWall)
                {
                    objecttobuild.layer = 8;

                    pl.RescanInBounds(objecttobuild.GetComponent<BoxCollider2D>().bounds);
      
                }
            }

            if (pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefsBottom.Length > 0 )
                CopyObject(objecttobuild, pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefsBottom[RandomisedNumber]);
         
            return;
        }

   
        if (ObjectOnGround.GetComponent<PubObject>() == null) return;
        PubObject ObjectOnGround_PubObject = ObjectOnGround.GetComponent<PubObject>();

        if (ObjectOnGround_PubObject.TopObjectsCount < TopObjectsMax && ObjectOnGround_PubObject.wall > 0 && !_PubObject.decoration)
            ObjectOnGround_PubObject.TopObjectsCount++;

       
        if (ObjectOnGround_PubObject.TopObjectsCount < 99 && ObjectOnGround_PubObject.TopObjectsCount > 0 && ObjectOnGround_PubObject.TopObjectsCount <= TopObjectsMax)
        {
            if (_statsControll != null)
            {
            
               if (pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefsMid.Length > 0 && ObjectOnGround_PubObject.TopObjectsCount < TopObjectsMax)
                    CopyObject(objecttobuild, pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefsMid[RandomisedNumber]);

                else if (pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefsTop.Length > 0 && ObjectOnGround_PubObject.TopObjectsCount == TopObjectsMax)
                    CopyObject(objecttobuild, pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefsTop[RandomisedNumber]);
                else
                    CopyObject(objecttobuild, pl.inv.GetItemInDatabase(_statsControll.DatabaseID).ObjectPrefs);
            }


            if (!_PubObject.decoration)
                objecttobuild.transform.position = new Vector2(ConstructorObjectPosition.x, ConstructorObjectPosition.y + 1 * ObjectOnGround_PubObject.TopObjectsCount);
            else objecttobuild.transform.position = new Vector2(ConstructorObjectPosition.x, ConstructorObjectPosition.y);

            if (objecttobuild.transform.Find("Base") != null)
                Destroy(objecttobuild.transform.Find("Base").gameObject);

            objecttobuild.transform.parent = ObjectOnGround.transform;
            
            objecttobuild.layer = 0;


            if (_PubObject != null) _PubObject.enabled = false;


            if (objecttobuild.GetComponent<PolygonCollider2D>() != null)
                objecttobuild.GetComponent<PolygonCollider2D>().enabled = false;

   
            if (objecttobuild.GetComponent<StatsControll>() != null)
                objecttobuild.GetComponent<StatsControll>().enabled = false;
            
            if (objecttobuild.GetComponent<GetItem>() != null)
                objecttobuild.GetComponent<GetItem>().enabled = false;

        }
        else
        {
            objecttobuild.transform.position = ConstructorObjectPosition;



            if (_PubObject != null)
            {
                if (_PubObject.wall > 0 && !_PubObject.TransperentWall)
                {
                    objecttobuild.layer = 8;

                  //  pl.RescanInBounds(objecttobuild.GetComponent<BoxCollider2D>().bounds);

                    //pl.PathRescan = 1;
                }
            }

         

        }







    }


    void SetComponents_in_ObjectToBuild(ref GameObject objecttobuild , GameObject ObjectOnGround)
    {

        if (objecttobuild.GetComponent<CharacterMove>() != null)
            objecttobuild.GetComponent<CharacterMove>().enabled = true;


        PubObject _PubObject = objecttobuild.GetComponent<PubObject>();

        if (_PubObject.decoration) objecttobuild.name += "decoration";
        if (_PubObject.TopObject) objecttobuild.name += "TopObject";


        if (_PubObject != null)
            _PubObject.enabled = true;

        if (objecttobuild.GetComponent<MovementControll>() != null)
        objecttobuild.GetComponent<MovementControll>().enabled = true;
        
        if (objecttobuild.GetComponent<Enemies>() != null)
         objecttobuild.GetComponent<Enemies>().enabled = true;
        
        if (objecttobuild.GetComponent<GenerateMoney>() != null)
         objecttobuild.GetComponent<GenerateMoney>().enabled = true;
        
        if (objecttobuild.GetComponent<Enemies>() != null)
        objecttobuild.GetComponent<Enemies>().enabled = true;

        if (objecttobuild.GetComponent<Animator>() != null)
            objecttobuild.GetComponent<Animator>().enabled = true;


        if (objecttobuild.GetComponent<GetItem>() != null)
            objecttobuild.GetComponent<GetItem>().enabled = true;

        if (objecttobuild.GetComponent<PolygonCollider2D>() != null && ObjectOnGround == null)
        objecttobuild.GetComponent<PolygonCollider2D>().enabled = true;

        

        if (objecttobuild.transform.childCount > 0 && ObjectOnGround == null)
        {
            if (objecttobuild.transform.GetChild(0).GetComponent<PolygonCollider2D>() != null)
                objecttobuild.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;

        }

        if (objecttobuild.GetComponent<CharacterMove>() != null)
        objecttobuild.GetComponent<CharacterMove>().enabled = true;

        


        StatsControll _StatsControll = objecttobuild.GetComponent<StatsControll>();

        if (_StatsControll != null)
        {

            _StatsControll.enabled = true;
            _StatsControll.BuildedStructure = true;

            LastBuildingConstructed = _StatsControll.DatabaseID;

        }

    }

 

    void SetPubObject_In_ObjectToBuild(ref PubObject _PubObject)
    {
       
        if (_PubObject == null) return;
        
        PubObject POPO = _transform.GetChild(0).GetComponent<PubObject>();

        _PubObject.TrueName = POPO.TrueName;

        if (Comfort <= ComfortMax - _PubObject.ComfortPlus && Comfort > ComfortMin)
            Comfort += _PubObject.ComfortPlus;

        if (TimerStay <= TimerStayMax - _PubObject.TimePlus && TimerStay > -TimerStayMax)
            TimerStay += _PubObject.TimePlus;

      

        if (ItemNeeded != null)
        {
            for (int i = 0; i < ItemNeeded.Length; i++)
            {
                
                pl.inv.ReduceItemCount(ItemNeeded[i], ItemNeededCount[i]);

            }
        }


        Floors += _PubObject.floors;
        KitchenFloors += _PubObject.kitchenfloors;

        Grounds += _PubObject.ground;
        Walls += _PubObject.wall;

        if (_PubObject.Table) Tables.Add(_PubObject.gameObject);


     //   AllTables += _PubObject.tables;
      //  AllPeople += _PubObject.people;

        _PubObject.ItemNeeded = ItemNeeded;
        _PubObject.ItemNeededCount = ItemNeededCount;

  
        IM.ActionDelay = Time.fixedTime + 0.2f;
        BuildDelay = Time.fixedTime + 0.2f;

        if (POPO.GetComponent<MovementControll>() != null)
        {
            if (_PubObject.tag == "Pers" && !_PubObject.GetComponent<MovementControll>().Soldier && !_PubObject.GetComponent<MovementControll>().Enemy) AllPeople++;
        }
        
    }






    void ConstructObject(Tilemap Map, TileBase[] Brush)
    {

        if (BuildDelay > Time.fixedTime)
        {
            _transform.GetChild(0).position = new Vector3(9999, _transform.GetChild(0).position.y, _transform.GetChild(0).position.z);

            return;
        }
        else _transform.GetChild(0).position = new Vector3(_transform.position.x + 0.5f, _transform.GetChild(0).position.y - 0.5f, _transform.GetChild(0).position.z);



        if (MouseC.UIColl(GameObject.Find("MenuBox")) ||
           MouseC.UIColl(GameObject.Find("CloseBigTips")) ||
           MouseC.UIColl(pl.inv.InventoryButton) ||
           MouseC.UIColl(pl.inv.JournalButton) ||
           MouseC.UIColl(QPage0) ||
           MouseC.UIColl(QPage1) ||
           MouseC.UIColl(QPage2) ||
           ChooseRightSideIcons)
        {

            _transform.GetChild(0).position = new Vector3(_transform.GetChild(0).position.x, _transform.position.y, _transform.GetChild(0).position.z);

            return;
        }


        LayerFlip(_transform.GetChild(0).gameObject);

        PubObject POPO = _transform.GetChild(0).GetComponent<PubObject>();
        Transform ChildOnMouse = _transform.GetChild(0);
        bool ValidBrush = false;

       
        for (int i = 0; i < Brush.Length; i++)
        {
            if (Map.GetTile(new Vector3Int(XPos, YPos - 1, 0)) == Brush[i])
            ValidBrush = true;

        }

        if(Map.GetTile(new Vector3Int(XPos, YPos - 1, 0))==null) ValidBrush = false;

        if (Map == null) Map = Tile;

        if (Map.GetTile(new Vector3Int(XPos, YPos - 1, 0)) == null ||
            (_menu.MenuONOFF || show_questbook))
        {
            StatsControll _StatsControllR0 = ChildOnMouse.GetComponent<StatsControll>();
            GameObject ObjectOnGroundR = null;

            int max1 = 0;
            max1 = RNDMAX(ChildOnMouse.gameObject, ref ObjectOnGroundR, ref _StatsControllR0);

            SetCunstrcutedStrNum(max1, ChildOnMouse.GetComponent<SpriteRenderer>(), _StatsControllR0.DatabaseID);


            ChildOnMouse.position = new Vector3(ChildOnMouse.position.x, _transform.position.y, ChildOnMouse.position.z);
            SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));
            return;
        }
   
        if (!ValidBrush || StartBlock.GetTile(new Vector3Int(XPos, YPos - 1, 0)) != null || TileBlock.GetTile(new Vector3Int(XPos, YPos - 1, 0)) != null)
        {
            ChildOnMouse.position = new Vector3(ChildOnMouse.position.x, _transform.position.y, ChildOnMouse.position.z);
            SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));
            return;
        }
   

        GameObject ObjectOnGround = null;
        GameObject TopOnGround = null;
        GameObject DecorationOnGround = null;

        //----------Object on Ground can bacome parent object for object objecttobuild
      
        CheckObjectOnGround(ref ObjectOnGround, ref TopOnGround, ref DecorationOnGround);


        StatsControll _StatsControllR = ChildOnMouse.GetComponent<StatsControll>();

        int max = 0;
        max = RNDMAX(ChildOnMouse.gameObject, ref ObjectOnGround, ref _StatsControllR);

        SetCunstrcutedStrNum(max, ChildOnMouse.GetComponent<SpriteRenderer>(), _StatsControllR.DatabaseID);



        //GameObject ObjectOnGroundDecoration = GameObject.Find(ConstructorObjectPosition.x + "_" + ConstructorObjectPosition.y + "decoration");


        if (ObjectOnGround == null || POPO.decoration)
        ChildOnMouse.position = new Vector3(ChildOnMouse.position.x, _transform.position.y, ChildOnMouse.position.z);


        if (ObjectOnGround != null)
        {

            if (ChildOnMouse.GetComponent<SpriteRenderer>() != null)
            ChildOnMouse.GetComponent<SpriteRenderer>().sortingLayerName = ObjectOnGround.GetComponent<SpriteRenderer>().sortingLayerName;


            if (ChildOnMouse.GetComponent<SpriteRenderer>() != null)
            {
                if(ObjectOnGround.GetComponent<PubObject>()!=null)
                ChildOnMouse.GetComponent<SpriteRenderer>().sortingOrder = ObjectOnGround.GetComponent<SpriteRenderer>().sortingOrder + ObjectOnGround.GetComponent<PubObject>().TopObjectsCount * 3 + 1;

            }
        }
      



        if (IM.CamMove)
        {
            SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));
            ChildOnMouse.position = new Vector3(ChildOnMouse.position.x, _transform.position.y, ChildOnMouse.position.z);

            return;
        }
       

    
        if (NeededItemCheck() && (RegularObjectCheck(POPO, ObjectOnGround) ||
            WallObjectCheck(POPO, ObjectOnGround) ||
            DecorationObjectCheck(POPO, DecorationOnGround) ||
            TopObjectCheck(POPO,TopOnGround)||
                TableObjectCheck(POPO,ObjectOnGround) ||
                WorkerObjectCheck(POPO, ObjectOnGround)))
        {

            StatsControll _StatsControll = ChildOnMouse.GetComponent<StatsControll>();

           

            if (!POPO.decoration)
            {

                if (ObjectOnGround != null)
                {
                    //-------------------setting Object to build Y POS ---------------------------//
                    if (ObjectOnGround.GetComponent<PubObject>().TopObjectsCount < 99)
                        ChildOnMouse.position = new Vector3(
                            ChildOnMouse.position.x,
                            _transform.position.y + 1 * (ObjectOnGround.GetComponent<PubObject>().TopObjectsCount + 1),
                            ChildOnMouse.position.z);
                }

                RandomiseConstructingObject(ChildOnMouse.gameObject, ref ObjectOnGround, ref _StatsControll);


            }


        

            SetColorAndAlpha(ChildOnMouse.gameObject, new Color(0.1f, 1, 0.1f, 0.7f));

            if (!CostObjectCheck(_StatsControll.DatabaseID))
            {
                SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));

            }

            if (SetObjectDelay >= Time.fixedTime || pl.IM.ActionDelay >= Time.fixedTime)
            {
                return;
            }

            // ----------------We build here-----------------//

            if (!IM.enter_b && !IM.LeftMouseButton && !IM.enter_b_hold) return;

            if (!CostObjectCheck(_StatsControll.DatabaseID))
            {
                AllMoneyObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                pl.menu.PlayAudio(pl.menu.ErrorClip);

                return;
            }

            if (ObjectOnGround != null)
            {
                if (ObjectOnGround.GetComponent<PubObject>().TopObjectsCount>0)
                PlaySound(Place, 0.55f + ObjectOnGround.GetComponent<PubObject>().TopObjectsCount*((1-0.55f)/ TopObjectsMax));
                else
                PlaySound(Place, UnityEngine.Random.Range(0.55f, 0.65f));

            }
            else
            PlaySound(Place, UnityEngine.Random.Range(0.9f,1f));






            GameObject objecttobuild = Instantiate<GameObject>(ChildOnMouse.gameObject);
            PubObject _PubObject = objecttobuild.GetComponent<PubObject>();
            
            if (BuildEffect == null)
            {
                BuildEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/BuildEffect"));

            }


            BuildEffect.GetComponent<AnimationFrame>().ResetAnimation();
            BuildEffect.transform.position = ChildOnMouse.transform.position;
            BuildEffect.GetComponent<AudioSource>().Play();

            ModifyObjectToBuild_CorrespondingToParentObject(_StatsControll,ref ObjectOnGround, ref objecttobuild,ref _PubObject);
            SetComponents_in_ObjectToBuild(ref objecttobuild, ObjectOnGround);
            SetPubObject_In_ObjectToBuild(ref _PubObject);

            

            OBOnBoard.Add(new ObjectOnBoard(objecttobuild.GetComponent<StatsControll>().DatabaseID, objecttobuild.transform.position, objecttobuild.name, objecttobuild, _StatsControll, objecttobuild.GetComponent<PubObject>()));

            ConstructedStructures.Add(new ObjectOnBoard(objecttobuild.GetComponent<StatsControll>().DatabaseID, objecttobuild.transform.position, objecttobuild.name, objecttobuild, _StatsControll, objecttobuild.GetComponent<PubObject>()));

            SetColorAndAlpha(objecttobuild, new Color(1f, 1, 1, 1f));

          

            pl.RescanInBounds(new Bounds(new Vector3(objecttobuild.transform.position.x, objecttobuild.transform.position.y, 0), new Vector3(10, 10, 0)));

            SpendMoney(_transform.position, pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).BuildingCost);
            pl.inv.ReduceItemCount(OnButtonID, 1);

            pl.inv.UpdateInvFolder();

            SetObjectDelay = Time.fixedTime + 0.3f;
            isRandomised = false;

        }
        else
        {
           
            SetColorAndAlpha(_transform.GetChild(0).gameObject, new Color(1, 0.1f, 0.1f, 0.7f));
            ChildOnMouse.position = new Vector3(ChildOnMouse.position.x, _transform.position.y, ChildOnMouse.position.z);

        }

                
        
    }






    bool RegularObjectCheck(PubObject POPO, GameObject ObjectOnGround)
    {
        bool result = false;
      //  if (ObjectOnGround == null) return false;

        if (ObjectOnGround == null && !POPO.decoration && POPO.tables == 0 && POPO.tag != "Pers"  && POPO.wall == 0)
            result = true;
        
        return result;
    }

    bool CostObjectCheck(int ID)
    {
        bool result = false;
        if (pl.inv.GetItem(9) == null) return false;

        if (pl.inv.GetItemInDatabase(ID).BuildingCost <= pl.inv.GetItem(9).Count)
            result = true;

        return result;
    }

    bool WallObjectCheck(PubObject POPO, GameObject ObjectOnGround)
    {
        bool result = false;

        if(ObjectOnGround != null)
        if(ObjectOnGround.GetComponent<PubObject>() != null)
        if (POPO.wall > 0 && ObjectOnGround.GetComponent<PubObject>().TopObjectsCount < TopObjectsMax && ObjectOnGround.GetComponent<PubObject>().wall > 0)
            result = true;

        if (ObjectOnGround == null && POPO.wall > 0 )
            result = true;
        
        return result;
    }

    bool TableObjectCheck(PubObject POPO, GameObject ObjectOnGround)
    {
        bool result = false;
        if(ObjectOnGround == null && POPO.tables > 0 && AllTables + POPO.tables <= AllTablesMax)
        result = true;

        print("TableObjectCheck " + result);

        return result;
    }

    bool WorkerObjectCheck(PubObject POPO, GameObject ObjectOnGround)
    {
        bool result = false;
        if (ObjectOnGround == null && POPO.tag == "Pers" && AllPeople + 1 <= AllPeopleMax)
            result = true;

        print("WorkerObjectCheck " + result);

        return result;
    }

    bool DecorationObjectCheck(PubObject POPO, GameObject DecorationOnGround)
    {
        bool result = false;
        if (POPO.decoration && DecorationOnGround == null) result = true;

        print("DecorationObjectCheck " + result);

        return result;
    }

    bool TopObjectCheck(PubObject POPO, GameObject TopOnGround)
    {
        bool result = false;
        if (POPO.TopObject && TopOnGround == null) result = true;


        print("TopObjectCheck " + result);


        return result;
    }

    bool NeededItemCheck()
    {
        bool setbrush = false;
        int s = 0;

        if (ItemNeeded == null) return true;

        if (ItemNeeded.Length <= 0) return true;
      
        for (int i = 0; i < ItemNeeded.Length; i++)
        {
            if (pl.inv.GetItem(ItemNeeded[i]) != null)
            {
                print("pl.inv.GetItem(ItemNeeded[i]).Count  " + pl.inv.GetItem(ItemNeeded[i]).Count);

                if (pl.inv.GetItem(ItemNeeded[i]).Count >= ItemNeededCount[i])
                {

                    s++;

                }
            }


        }

        if (s != ItemNeeded.Length) setbrush = false;
        else setbrush = true;
           
        return setbrush;

    }





    void ObjectsColorControll(ref PubObject POPO, GameObject ObjectOnGround, GameObject ObjectOnGroundDecoration)
    {
        Transform POPO_Transform = _transform.GetChild(0).transform;

        if (((ObjectOnGround == null &&
             !POPO.decoration ) ||

         //---------------------------------decoration condition-----------------------------------//

         (POPO.decoration && ObjectOnGround != null && ObjectOnGround.GetComponent<PubObject>() != null && ObjectOnGround.GetComponent<PubObject>().wall > 0 && ObjectOnGroundDecoration == null) ||

         //---------------------------------wall condition-----------------------------------//
         
         (ObjectOnGround != null && ObjectOnGround.GetComponent<PubObject>() != null && ObjectOnGround.GetComponent<PubObject>().TopObjectsCount < TopObjectsMax && ObjectOnGround.GetComponent<PubObject>().wall > 0 && POPO.wall > 0))

         && StartBlock.GetTile(new Vector3Int(XPos, YPos - 1, 0)) == null && TileBlock.GetTile(new Vector3Int(XPos, YPos - 1, 0)) == null)
        {

            if (ObjectOnGround != null) print("ObjectOnGround ");


           
        }
        else
        {
            POPO_Transform.position = new Vector3(POPO_Transform.position.x, _transform.position.y, POPO_Transform.position.z);
            SetColorAndAlpha(POPO_Transform.gameObject, new Color(1, 0.1f, 0.1f, 0.7f));
        }





    }


    void CrowdedControl()
    {
        int crowdedInt = 0;

        foreach (var table in Tables)
        {
            if (table == null) continue;

            var pubObject = table.GetComponent<PubObject>();

            if (pubObject.Crowded)
            {
                crowdedInt = 1;
            }

            foreach (var client in pubObject.Clients)
            {
                if (client == null) continue;

                var clientComponent = client.GetComponent<Client>();

                if (!clientComponent.Hungry)
                {
                    int cost = clientComponent.Dish.Cost + 5 + (Comfort - crowdedInt * 5);
                    MinIncome += cost;
                    MaxIncome += cost;
                }
            }
        }
    }


    public void SetColorAndAlpha(GameObject ob,Color _color)
    {
        
        SetColorAndAlpha_S(ob, _color);

        for (int i = 0; i < ob.transform.childCount; i++)
        {
            SetColorAndAlpha_S(ob.transform.GetChild(i).gameObject, _color);
            

            for (int ii = 0; ii < ob.transform.GetChild(i).childCount; ii++)
            {
                SetColorAndAlpha_S(ob.transform.GetChild(i).GetChild(ii).gameObject, _color);
                
            }
        }
        
        
    }

    void SetColorAndAlpha_S(GameObject ob, Color _color)
    {
        if (ob == null)
        {
            return;
        }

        if (ob.name == "Vision") return;

        SpriteRenderer SPRT = ob.GetComponent<SpriteRenderer>();
        if (SPRT != null)
            SPRT.color = _color;


    }



    public void SetUIAlpha(GameObject ob, float Alpha, float Size)
    {
        if (ob == null)
        {
            return;
        }

        ob.transform.localScale = new Vector3(ob.transform.localScale.x*Size, ob.transform.localScale.y, ob.transform.localScale.z);

        if (ob.GetComponent<Image>() != null)
            ob.GetComponent<Image>().color = new Color(ob.GetComponent<Image>().color.r, ob.GetComponent<Image>().color.g, ob.GetComponent<Image>().color.b,Alpha);

        for (int i = 0; i < ob.transform.childCount; i++)
        {
            if (ob.transform.GetChild(i).GetComponent<Image>() != null)
                ob.transform.GetChild(i).GetComponent<Image>().color = new Color(ob.transform.GetChild(i).GetComponent<Image>().color.r, ob.transform.GetChild(i).GetComponent<Image>().color.g, ob.transform.GetChild(i).GetComponent<Image>().color.b, Alpha);


            for (int ii = 0; ii < ob.transform.GetChild(i).childCount; ii++)
            {
                if (ob.transform.GetChild(i).GetChild(ii).GetComponent<Image>() != null)
                    ob.transform.GetChild(i).GetChild(ii).GetComponent<Image>().color = new Color(ob.transform.GetChild(i).GetChild(ii).GetComponent<Image>().color.r, ob.transform.GetChild(i).GetChild(ii).GetComponent<Image>().color.g, ob.transform.GetChild(i).GetChild(ii).GetComponent<Image>().color.b, Alpha);

            }
        }
            
        


    }


    public void SetMaterial(GameObject ob, Material material)
    {
        if (ob == null)
        {
            return;
        }

        if (ob.GetComponent<SpriteRenderer>() != null)
            ob.GetComponent<SpriteRenderer>().material = material;

        for (int i = 0; i < ob.transform.childCount; i++)
        {
            if (ob.transform.GetChild(i).GetComponent<SpriteRenderer>() != null && ob.transform.GetChild(i).name!="Vision")
                ob.transform.GetChild(i).GetComponent<SpriteRenderer>().material = material;

            for (int ii = 0; ii < ob.transform.GetChild(i).childCount; ii++)
            {
                if (ob.transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>() != null && ob.transform.GetChild(i).GetChild(ii).name != "Vision")
                    ob.transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>().material = material;
            }
        }
        
    }

    
    void DirectControlls()
    {

        if (IM.HorizontalFlip)
        {
            if (_transform.childCount>0)
            {
                _transform.GetChild(0).transform.localScale =
                new Vector3(_transform.GetChild(0).transform.localScale.x * -1, _transform.GetChild(0).transform.localScale.y, _transform.GetChild(0).transform.localScale.z);
            }
        }

    

        if (Building && IM.ActionDelay < Time.fixedTime && !inv.blueprintshow)
        {
          
            if ( IM.menu_b || (IM.exit_b && !IM.joystick) || IM.inventory_b )
            {

                DeActivateBuilding();
                IM.ActionDelay = Time.fixedTime + 0.2f;
            }

        }

        float cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);

        Bounds bounds = GameObject.Find("CameraBox").GetComponent<BoxCollider2D>().bounds;
        Vector3 _min = MainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 _max = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width/1.09f, Screen.height / 1.2f, 0));


        if (_menu.MenuONOFF || ChooseMouseObject || show_questbook || ChooseRightSideIcons || pl.inv.blueprintshow || pl.inv.crafting)
            return;


        //---------------------------Move Camera with mouse-------------------------------//

        if (CanScrollCamera) ScroolCamera();
            
        if (Building && !_menu.MenuONOFF && !show_questbook )
        {
          
            if (_transform.position.y > _min.y && _transform.position.x < _max.x && ((IM._horizontal > 0 && IM._horizontalPush && _menu.ScrollDelay < Time.fixedTime) || (IM.DPADX > 0 && IM._horizontal_DPAD_Push && _menu.ScrollDelay < Time.fixedTime)))
            {
                YPos--;
                if(IM.joystick)
                _menu.ScrollDelay = Time.fixedTime + 0.1f;
            }

            if (_transform.position.y < _max.y && _transform.position.x > _min.x && ((IM._horizontal < 0 && IM._horizontalPush && _menu.ScrollDelay < Time.fixedTime) || (IM.DPADX < 0 && IM._horizontal_DPAD_Push && _menu.ScrollDelay < Time.fixedTime)) )
            {
                YPos++;
                if (IM.joystick)
                    _menu.ScrollDelay = Time.fixedTime + 0.1f;
            }


            if (_transform.position.y > _min.y && _transform.position.x > _min.x && ((IM._vertical < 0 && IM._verticalPush && _menu.ScrollDelay < Time.fixedTime) || (IM.DPADY < 0 && IM._vertical_DPAD_Push && _menu.ScrollDelay < Time.fixedTime)))
            {
                XPos--;
                if (IM.joystick)
                    _menu.ScrollDelay = Time.fixedTime + 0.1f;
            }

            if (_transform.position.y < _max.y && _transform.position.x < _max.x && ((IM._vertical > 0 && IM._verticalPush && _menu.ScrollDelay < Time.fixedTime) || (IM.DPADY > 0 && IM._vertical_DPAD_Push && _menu.ScrollDelay < Time.fixedTime)) )
            {
                XPos++;
                if (IM.joystick)
                    _menu.ScrollDelay = Time.fixedTime + 0.1f;
            }

        }

        

        Vector3 v = Tile.CellToWorld(new Vector3Int(XPos, YPos, 1));
        //  v = new Vector3(Mathf.Clamp(v.x, _min.x, _max.x), Mathf.Clamp(v.y, _min.y, _max.y), v.z);
        float ObshiftX = 0f;
        float ObshiftY = 0.25f;
        float ObshiftOnBoardX = 0.5f;
        Vector2 mouse = pl.MainCamera.ScreenToWorldPoint(new Vector3(IM.MousePosition.x, IM.MousePosition.y, 0));

        if (pl.IM.MouseMode)
        {



            Vector3Int vi = Tile.WorldToCell(new Vector3(mouse.x - ObshiftX, mouse.y + ObshiftY, 0));
            if (Building)
                v = Tile.CellToWorld(new Vector3Int(vi.x, vi.y, 1));




            XPos = Tile.WorldToCell(v).x;
            YPos = Tile.WorldToCell(v).y;

        }

        MaxField = bounds.max;
        MinField = bounds.min;

        if (ConstructorObjectPosition != new Vector2(v.x + ObshiftOnBoardX, v.y + 0.25f))
        {

            ConstructorObjectPosition = new Vector2(v.x + ObshiftOnBoardX, v.y + 0.25f);
            isRandomised = false;
        }

    
        if (!Building) v = new Vector3(9999, 9999, 0);
        _transform.position = v;

        
    }

    public void ActivateBuilding()
    {
        EnterHolding = true;
        Building = true;
        pl.inv.ONOFF(ExitBuildingMode, true);
        //  pl.inv.showinvent = true;

        pl.inv.ONOFF(gameObject, true);
        pl.inv.ONOFF(ConstructorButtons, true);

        IM.ActionDelay = Time.fixedTime + 0.7f;
     
    }

    public void DeActivateBuilding()
    {
        Building = false;
        pl.inv.ONOFF(ExitBuildingMode, false);
        pl.inv.showinvent = false;
        RandomisedNumber = 0;
        if (_transform.childCount > 0)
            Destroy(_transform.GetChild(0).gameObject);

        _transform.position = new Vector3(9999, 9999, 0);
        pl.inv.ONOFF( gameObject,false);
        pl.inv.ONOFF( ConstructorButtons, false);

        pl.inv.ShootPause = true;
        IM.ActionDelay = Time.fixedTime + 0.1f;

    }


    public void DeActivateBuildingNOINV()
    {
        Building = false;
        pl.inv.ONOFF(ExitBuildingMode, false);
        RandomisedNumber = 0;
        if (_transform.childCount > 0)
            Destroy(_transform.GetChild(0).gameObject);

        _transform.position = new Vector3(9999, 9999, 0);
        pl.inv.ONOFF(gameObject, false);
        pl.inv.ONOFF( ConstructorButtons, false);

    }



    public void SetNewAchivement(string newach)
    {
        RectTransform AchTr = GameObject.Find("Achivement").GetComponent<RectTransform>();

        if (AchTr.anchoredPosition.y >= 650)
            AchTr.anchoredPosition = new Vector2(AchTr.anchoredPosition.x,400);

        UnlockedText.Add(newach);
        
    }

    public void AddMoney(Vector3 pos, int cost)
    {

        pl.inv.AddItemNOAUDIO_NOPickedNames(9, cost,99,new Vector3(0,0,0));

    }
    public void SpendMoney(Vector3 pos, int cost)
    {
       
        pl.inv.ReduceItemCount(9, cost);
     
    }


    public void PlaySound(AudioClip AC, float pitch)
    {
        GetComponent<AudioSource>().pitch = pitch;
        GetComponent<AudioSource>().clip = AC;
       GetComponent<AudioSource>().Play();
    }


    


  

    
    void GAMESPEEDMENU()
    {
      
         if (pl.menu.UIColl(Play2Button) && (IM.enter_b || Input.GetMouseButtonDown(0)))
         {
             Game_SPEED = 3;
         }

         if (pl.menu.UIColl(PlayButton) && (IM.enter_b || Input.GetMouseButtonDown(0)))
         {
             Game_SPEED = 1;
         }

         if (pl.menu.UIColl(PauseButton) && (IM.enter_b || Input.GetMouseButtonDown(0)))
         {
             Game_SPEED = 0;
         }
       
        if (Game_SPEED == 3)
        {
            PLAY_ButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            PAUSE_ButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            SPEED2_ButtonImage.color = new Color(1, 1, 1, 1);
        }

        if (Game_SPEED == 1)
        {
            PLAY_ButtonImage.color = new Color(1, 1, 1, 1);
            PAUSE_ButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            SPEED2_ButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        if (Game_SPEED == 0)
        {
            PLAY_ButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            PAUSE_ButtonImage.color = new Color(1, 1, 1, 1);
            SPEED2_ButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        
    }
  
  
    void ToolTipControl()
    {
       // ToolTip_Text.text = Tips[i] + stats + brush;

        float ToolTipSpeed = 0.08f;
        //print((Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 1.3f) + " / " + (Screen.width - ToolTip_IMG.GetComponent<RectTransform>().sizeDelta.x));

        float XPlus = 1.5f;
        float YPlus = 0.8f;

        if (!IM.joystick)
        {
            if (Input.mousePosition.x < 1200)
                XPlus = 1.5f;
            else XPlus = -1.5f;

            if (Input.mousePosition.y < 200)
                YPlus = 0.8f;
            else YPlus = -0.8f;
        }
        else
        {

            if (ChooseUI.GetComponent<RectTransform>().anchoredPosition.x < 500)
                XPlus = 1.5f;
            else XPlus = -1.5f;

            if (ChooseUI.GetComponent<RectTransform>().anchoredPosition.y < 200)
                YPlus = 0.8f;
            else YPlus = -0.8f;
        }


       /* if (!IM.joystick)
            ToolTip_IMG.transform.position = new Vector2(pl.MainCamera.ScreenToWorldPoint(IM.MousePosition).x + XPlus, pl.MainCamera.ScreenToWorldPoint(IM.MousePosition).y + YPlus);
        else
            ToolTip_IMG.transform.position = new Vector2(ChooseUI.transform.position.x + XPlus, ChooseUI.transform.position.y + YPlus);
            */


        ToolTip_IMG.transform.SetAsLastSibling();



    }

    public void ONOFFVisuals(GameObject Obj, float alpha)
    {
        if (Obj == null) return;
       
        
            SpriteRenderer ObjSPRT = Obj.GetComponent<SpriteRenderer>();
        Image ObjIMG = Obj.GetComponent<Image>();

        if (ObjSPRT != null)
            ObjSPRT.color = new Color(ObjSPRT.color.r, ObjSPRT.color.g, ObjSPRT.color.b, alpha);

        if (ObjIMG != null)
            ObjIMG.color = new Color(ObjIMG.color.r, ObjIMG.color.g, ObjIMG.color.b, alpha);


        for (int i = 0; i < Obj.transform.childCount; i++)
        {
            if (Obj.transform.GetChild(i).GetComponent<SpriteRenderer>() != null && Obj.transform.GetChild(i).GetComponent<Blinking>() == null)
            {
                if (Obj.transform.GetChild(i).GetComponent<SpriteRenderer>().color == new Color(ObjSPRT.color.r - 0.1f * (i + 1), ObjSPRT.color.g - 0.1f * (i + 1), ObjSPRT.color.b - 0.1f * (i + 1), alpha))
                {
                    break;
                }
                else
                    Obj.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(ObjSPRT.color.r - 0.1f * (i + 1), ObjSPRT.color.g - 0.1f * (i + 1), ObjSPRT.color.b - 0.1f * (i + 1), alpha);
            }


            if (Obj.transform.GetChild(i).GetComponent<Image>() != null)
            {
                if (Obj.transform.GetChild(i).GetComponent<Image>().color == new Color(Obj.transform.GetChild(i).GetComponent<Image>().color.r , Obj.transform.GetChild(i).GetComponent<Image>().color.g , Obj.transform.GetChild(i).GetComponent<Image>().color.b , alpha))
                {
                    break;
                }
                else
                    Obj.transform.GetChild(i).GetComponent<Image>().color = new Color(Obj.transform.GetChild(i).GetComponent<Image>().color.r , Obj.transform.GetChild(i).GetComponent<Image>().color.g , Obj.transform.GetChild(i).GetComponent<Image>().color.b , alpha);
            }
        }
            
        
        

    }

   void  ObjectsUI()
    {
        if (!ShowChargeUI) return;
        
        if (IM.UButton)
        {
            if (!FO.DrawUI)
            {
                if (FO.HPUI != null)
                    pl.inv.ONOFF(FO.HPUI, true);

                if (FO.ChargeUI != null)
                    pl.inv.ONOFF(FO.ChargeUI, true);

                if (FO.ComfortUI != null)
                    pl.inv.ONOFF(FO.ComfortUI, true);


                FO.DrawUI = true;
            }

        }
        else
        {
            if (FO.DrawUI)
            {
                if (FO.HPUI != null)
                    pl.inv.ONOFF(FO.HPUI, false);

                if (FO.ChargeUI != null)
                    pl.inv.ONOFF(FO.ChargeUI, false);

                if (FO.ComfortUI != null)
                    pl.inv.ONOFF(FO.ComfortUI, false);

                FO.DrawUI = false;
            }

        }


        FO.UIControll();

    }



    void ObjectColorAlpha(ref StatsControll FO)
    {
        minimalalpha = 0.3f;

        ObjectsUI();

        if (FO.InvisTimer - 0.8f > Time.fixedTime)
        {
           
            if (!FO.Stunned)
            {
                FO.SetColorAndMaterial(0.5f, WhiteMaterial);
            }
            else
                FO.SetColorAndMaterial(1, StunMaterial);

            return;
        }
       


        if (FO.InvisTimer > Time.fixedTime && FO.InvisTimer > Time.fixedTime + 0.05f)
        {

            FO.AplhaColor = 0.5f;
            FO.SetColorAndMaterial(0.5f, FO.StartMaterial);
            return;
        }

        if (FO.InvisTimer > Time.fixedTime && FO.InvisTimer < Time.fixedTime+0.05f)
        {

            FO.AplhaColor = 1;
            FO.SetColorAndMaterial(1, FO.StartMaterial);
            return;
        }

        if (FO.ReduceAlphaOnColl)
        {
           // print("ReduceAlphaOnColl " + FO.name);

            if (pl.coll_obj.Contains(FO.gameObject) || (Mathf.Abs(pl.transform.position.x - FO.transform.position.x)<1 && Mathf.Abs(pl.transform.position.y - FO.transform.position.y) < 1))
            {
                if (FO.AplhaColor > minimalalpha && FO.name !="Base")
                    FO.AplhaColor -= 1 * Time.deltaTime*3;

                FO.SetColorAndMaterial(FO.AplhaColor, FO.StartMaterial);
            }
            else
            {

                if (FO.AplhaColor < 1)
                {
                    if (FO.AplhaColor > 0.9f) FO.AplhaColor = 1;
                    FO.SetColorAndMaterial(FO.AplhaColor, FO.StartMaterial);
                    FO.AplhaColor += 1 * Time.deltaTime;
                }


            }
        }
        else
        {
            if (FO.AplhaColor < 1)

            {
                FO.AplhaColor += 1 * Time.deltaTime;
                if (FO.AplhaColor > 0.9f) FO.AplhaColor = 1;
                FO.SetColorAndMaterial(FO.AplhaColor, FO.StartMaterial);
            }
        }

        
        if (FO.Stunned)
        {
            FO.SetColorAndMaterial(FO.AplhaColor, StunMaterial);
            return;
        }

        
     /* if (FO.NewMaterialOnColl == null)
      {
          FO.SetColorAndMaterial(FO.AplhaColor, FO.StartMaterial);
          return;
      }*/


      if (!pl.coll_obj.Contains(FO.gameObject) && FO.NewMaterialOnColl!=null)
      {
          FO.StartColl = false;
          FO.SetColorAndMaterial(FO.AplhaColor, FO.StartMaterial);
          return;
      }



      if (!FO.StartColl)
      {

          FO.CollMaterialTimer = Time.fixedTime + 0.5f;
          FO.StartColl = true;

      }



        if (FO.CollMaterialTimer > Time.fixedTime)
        {
            if(FO.CollMaterialTimer > Time.fixedTime+0.05f)
            FO.SetColorAndMaterial(FO.AplhaColor, FO.NewMaterialOnColl);
            else FO.SetColorAndMaterial(FO.AplhaColor, FO.StartMaterial);
        }

         
    }



    GameObject FindConstructedObject(string name)
    {
        GameObject ConstrObject = null;

        for (int i = 0; i < OBOnBoard.Count; i++)
        {
            if (OBOnBoard[i].Object != null)
            {
                //print("NOT NULL: " + OBOnBoard[i].Object.name + " / " + name);

                if (OBOnBoard[i].Object.name == name)
                    ConstrObject = OBOnBoard[i].Object;
            }
        }

        return ConstrObject;
    }


    void Log()
    {
        if (_menu.Language == 0)
        {
            LogText.text = "";
            for (int i = 0; i < LogListEN.Count; i++)
                LogText.text += LogListEN[i] + "\n";
        }

        if (_menu.Language == 1)
        {
            LogText.text = "";
            for (int i = 0; i < LogListUA.Count; i++)
                LogText.text += LogListUA[i] + "\n";
        }

        if (_menu.Language == 2)
        {
            LogText.text = "";
            for (int i = 0; i < LogListJP.Count; i++)
                LogText.text += LogListJP[i] + "\n";
        }
    }


    public void AddLogPartOnes(string logpartEN, string logpartUA, string logpartJP, GameObject ObjectToConnect)
    {
        if (LogListEN.Count < 5)
        {
            if (!LogListEN.Contains(logpartEN + "\n"))
            {
                LogListEN.Add(logpartEN + "\n");

            }
        }
        else
        {
            if (!LogListEN.Contains(logpartEN + "\n"))
            {
                LogListEN.RemoveAt(0);
                LogListEN.Add(logpartEN + "\n");

            }
        }


        if (ObjectToConnect != null)
            ObjectsToConnect.Add(ObjectToConnect);

        if (LogListUA.Count < 5)
        {
            if (!LogListUA.Contains(logpartUA + "\n"))
            {
                LogListUA.Add(logpartUA + "\n");

            }
        }
        else
        {
            if (!LogListUA.Contains(logpartUA + "\n"))
            {
                LogListUA.RemoveAt(0);
                LogListUA.Add(logpartUA + "\n");

            }
        }


        if (LogListJP.Count < 5)
        {
            if (!LogListJP.Contains(logpartJP + "\n"))
            {
                LogListJP.Add(logpartJP + "\n");

            }
        }
        else
        {
            if (!LogListJP.Contains(logpartJP + "\n"))
            {
                LogListJP.RemoveAt(0);
                LogListJP.Add(logpartJP + "\n");

            }
        }
    }


    public void RemoveLogPart(string logpartEN)
    {

        for (int i = 0; i < LogListEN.Count; i++)
        {
            if (LogListEN[i].Contains(logpartEN))
            {
                LogListEN.RemoveAt(i);
                LogListUA.RemoveAt(i);
                LogListJP.RemoveAt(i);

            }

        }

    }


    public void AddLogPart(string logpartEN, string logpartUA, string logpartJP, GameObject ObjectToConnect)
    {

        if (LogListEN.Count < 5)
        {

            LogListEN.Add(logpartEN + "\n");

        }
        else
        {
            LogListEN.RemoveAt(0);
            LogListEN.Add(logpartEN + "\n");

        }
        if (ObjectToConnect != null)
            ObjectsToConnect.Add(ObjectToConnect);

        if (LogListUA.Count < 5)
        {
            LogListUA.Add(logpartUA + "\n");

        }
        else
        {
            LogListUA.RemoveAt(0);
            LogListUA.Add(logpartUA + "\n");

        }


        if (LogListJP.Count < 5)
        {
            LogListJP.Add(logpartJP + "\n");

        }
        else
        {
            LogListJP.RemoveAt(0);
            LogListJP.Add(logpartJP + "\n");

        }

    }


    public void UnsetBigTips()
    {
        if(pl!=null && pl.inv!=null && TipsPause!=null)
        TipsPause.SetActive(false);
        TutorialPause = false;

        OnUIDelay = Time.fixedTime + 0.1f;
        if(IM!=null)
        IM.ActionDelay = Time.fixedTime + 0.1f;
        SetObjectDelay = Time.fixedTime + 1;

    }



    public void SetBigTip(int texttipnum)
    {
        print("SetBigTip 0");

        if (TutorialPhaseBigTip.Contains(texttipnum))
        {
            TutorialPause = false;
            TipsPause.SetActive(false);
            TutorialPhaseBigTip.Add(texttipnum);

            return;
        }
        print("SetBigTip 1");

        if (texttipnum <= -1)
        {

            TutorialPhaseBigTip.Add(texttipnum);
            return;
        }
        print("SetBigTip 2");

        if (_menu.DrawTutorial == 0)
        {
            TutorialPause = false;
            TipsPause.SetActive(false);
            TutorialPhaseBigTip.Add(texttipnum);
            return;
        }
        print("SetBigTip 3");



        if (textdatabase.textEN[NumberInData(texttipnum)].line[0].line[0] != "" && !TutorialPause)
        {
            print("SetBigTip 4");
         
            TipsPause.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = textdatabase.GetFirstLine(texttipnum, _menu.Language);

            
            TutorialPause = true;
            TipsPause.SetActive(true);
            
        
        }
       
        TutorialPhaseBigTip.Add(texttipnum);
    }

    public void TipsReminder(int texttipnum)
    {
        if (_menu.MenuONOFF || pl.inv.showinvent) return;

        TipsPause.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = textdatabase.GetFirstLine(texttipnum, _menu.Language);
        TutorialPause = true;
        TipsPause.SetActive(true);
   
     
    }
    void ONOFF_QUESTBOOK(int StartQuest,int EndQuest, bool TF)
    {
        for (int i = StartQuest; i < EndQuest; i++)
        {
            pl.inv.ONOFF(GameObject.Find("QuestPart" + i), TF);
        }

        pl.inv.ONOFF(GameObject.Find("QuestBookBG2Text"), TF);
        pl.inv.ONOFF(GameObject.Find("QuestBookBG2"), TF);
        pl.inv.ONOFF(GameObject.Find("QuestBookBG"), TF);
        pl.inv.ONOFF( QPage0, TF);
        pl.inv.ONOFF(QPage1, TF);
        pl.inv.ONOFF(QPage2, TF);
    }

    void CreateEffectUI(string Name, GameObject Target)
    {
        if (GameObject.Find(Name) != null) return;
        
        GameObject RI = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/"+ Name), Target.transform);
        RI.transform.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        RI.name = Name;
        print("effect");
        
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
    public void ShakeCam()
    {
        XShake = UnityEngine.Random.Range(-0.01f, 0.01f);
        YShake = UnityEngine.Random.Range(-0.01f, 0.01f);
    }
    public void ShakeCamY()
    {
        YShake = UnityEngine.Random.Range(-0.03f, 0.03f);
        XShake = UnityEngine.Random.Range(-0.01f, 0.01f);
    }
    public void NOShakeCam()
    {
        XShake = 0;
        YShake = 0;
    }

    void ExplosionDestroy()
    {
        GameObject[] EXPL = GameObject.FindGameObjectsWithTag("Explosion");

        for (int i = 0; i < EXPL.Length; i++)
        {
            if (EXPL[i].GetComponent<SpriteRenderer>() != null)
            {
                if (!EXPL[i].GetComponent<SpriteRenderer>().enabled)
                    Destroy(EXPL[i]);
            }
            else Destroy(EXPL[i]);
        }

        pl.ResetFlippingObjects();
    }

  

    int CheckObjectsCountInOnBoard(string truename)
    {   int count = 0;
        foreach (ObjectOnBoard OB in OBOnBoard)
        {
            if (OB.Name == truename) count++;
        }
        return count;

    }

    public bool CheckObjectsPositionOnBoard(Vector3 Pos)
    {
        bool res = false;
        int digits = 1;
        foreach (ObjectOnBoard OB in OBOnBoard)
        {

            if (OB.Object != null)
            {
                if (new Vector3((float)Math.Round(OB.Object.transform.position.x, digits), (float)Math.Round(OB.Object.transform.position.y, digits), 0) ==
                    new Vector3((float)Math.Round(Pos.x, digits), (float)Math.Round(Pos.y, digits), 0))
                {
                    res = true;
                }
            }
        }
        return res;

    }

    public void SetToPlayerPos()
    {

        IM.MousePosition = new Vector2(0, 0);

        XPos = Tile.WorldToCell(new Vector3Int((int)pl._transform.position.x-1, (int)pl._transform.position.y, 1)).x;
        YPos = Tile.WorldToCell(new Vector3Int((int)pl._transform.position.x, (int)pl._transform.position.y, 1)).y;

        Vector3 v = Tile.CellToWorld(new Vector3Int(XPos, YPos, 1));

        ConstructorObjectPosition = new Vector2(v.x + 0.5f, v.y - 0.5f);
        
        _transform.position = v;
        
    }

    void VisionDraw(ref StatsControll FO)
    {
        if (FO.DIA != null) return;
        
        if (FO.MaxVision < pl.Vision || FO.MinVision > pl.Vision || FO.MaxSniff < pl.Sniff || FO.MinSniff > pl.Sniff)
        {
            if (FO.DrawVision)
            {
                pl.inv.ONOFF(FO.gameObject, false);
                FO.DrawVision = false;
                return;
            }
            else return;

        }
        else if (!FO.DrawVision)
        {
            pl.inv.ONOFF(FO.gameObject, true);
            FO.DrawVision = true;
            return;
        }
        
    }


    public void BlowObject(StatsControll FO)
    {
   
        if (FO.gameObject.GetComponent<Tail>() != null)
        {
            for (int i = 0; i < FO.gameObject.GetComponent<Tail>().TailObjs.Count; i++)
                Destroy(FO.gameObject.GetComponent<Tail>().TailObjs[i]);

        }

        if (FO.MutationUI != null)
            Destroy(FO.MutationUI);

        pl.BlowThis(FO.gameObject);


        for (int i = 0; i < ConstructedStructures.Count; i++)
        {
            if (ConstructedStructures[i].Object == FO.gameObject) ConstructedStructures.RemoveAt(i);
        }


        if (!FO.IgnoreDestroyList && !FO.BuildedStructure)
        {
            for (int i = 0; i < OBOnBoard.Count; i++)
            {
                if (OBOnBoard[i].Object == FO.gameObject) OBOnBoard.RemoveAt(i);
            }


            pl.menu.SL.ObjectsToDestroy.Add(FO.gameObject.name);
        }

        pl.ResetFlippingObjects();

    }

    void CopyObject(GameObject _object, GameObject target)
    {
        if (_object.GetComponent<SpriteRenderer>()!=null)
        _object.GetComponent<SpriteRenderer>().sprite = target.GetComponent<SpriteRenderer>().sprite;

        if (_object.GetComponent<BoxCollider2D>() != null)
            _object.GetComponent<BoxCollider2D>().size = target.GetComponent<BoxCollider2D>().size;

        if (_object.GetComponent<AnimationFrame>() == null && target.GetComponent<AnimationFrame>() != null)
        {
            _object.AddComponent<AnimationFrame>();
            _object.GetComponent<AnimationFrame>().SPRT = target.GetComponent<AnimationFrame>().SPRT;
            _object.GetComponent<AnimationFrame>().CycleDelay = target.GetComponent<AnimationFrame>().CycleDelay;
        }

        if (target.GetComponent<Animator>() != null)
        {
            if (_object.GetComponent<Animator>() == null) _object.AddComponent<Animator>();

            _object.GetComponent<Animator>().runtimeAnimatorController = target.GetComponent<Animator>().runtimeAnimatorController;

        }
        else if (_object.GetComponent<Animator>() != null)  DestroyImmediate(_object.GetComponent<Animator>());

        _object.GetComponent<StatsControll>().DatabaseID = target.GetComponent<StatsControll>().DatabaseID;

        for (int i = 0; i < target.transform.childCount; i++)
        {
            GameObject obj = Instantiate<GameObject>(target.transform.GetChild(i).gameObject);
            obj.transform.parent = _object.transform;
            obj.transform.localPosition = target.transform.GetChild(i).transform.localPosition;
        }
       
    }


    

    public void DestroyThis(GameObject Target)
    {
        if (pl.CollidingItems.Contains(Target))
            pl.CollidingItems.Remove(Target);

        if (DroppedItems.Contains(Target))
            DroppedItems.Remove(Target);

        pl.GetComponent<Gun>().HitDuration = 0.2f;


        StatsControll T_ST = Target.GetComponent<StatsControll>();

        if (T_ST == null)
            pl.menu.SL.ObjectsToDestroy.Add(Target.name);
        else
        {
            if (T_ST.HPUI != null)
                Destroy(T_ST.HPUI);

            if (T_ST.ChargeUI != null)
                Destroy(T_ST.ChargeUI);

            if (T_ST.ComfortUI != null)
                Destroy(T_ST.ComfortUI);

            if (!T_ST.BuildedStructure)
                pl.menu.SL.ObjectsToDestroy.Add(Target.name);
        }
        

        if (Target.GetComponent<GetItem>() != null)
            if (Target.GetComponent<GetItem>().QuestID > -1)
                pl.inv.DoneQuest(Target.GetComponent<GetItem>().QuestID);


        Destroy(Target);

        pl.ResetFlippingObjects();
    }

    int RNDMAX(GameObject ChildOnMouse, ref GameObject ObjectOnGround, ref StatsControll _StatsControll)
    {
        int max = 0;
        if (BottomCheck(ref _StatsControll))
        {
            max = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsBottom.Length;
            return max;
        }

        if (MidCheck(ref ObjectOnGround, ref _StatsControll))
        {
            max = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsMid.Length;
                return max;
            
        }

        if (TopCheck(ref ObjectOnGround, ref _StatsControll))
        {
            max = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsTop.Length;
 
        }

        return max;
    }

    bool BottomCheck(ref StatsControll _StatsControll)
    {

        //if (ObjectOnGround != null) return false;
        
        if (pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsBottom == null) return false;

        if (pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsBottom.Length <= 0) return false;

        return true;
        
        
    }

    bool MidCheck(ref GameObject ObjectOnGround, ref StatsControll _StatsControll)
    {
        if (ObjectOnGround == null) return false;

        if (pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsMid != null)
        if (pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsMid.Length > 0 && ObjectOnGround.GetComponent<PubObject>().TopObjectsCount < TopObjectsMax - 1)
        return true;
            
        return false;
        
    }

    bool TopCheck(ref GameObject ObjectOnGround, ref StatsControll _StatsControll)
    {
        if (ObjectOnGround == null) return false;

        if (pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsTop != null)
        if (pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsTop.Length > 0 && ObjectOnGround.GetComponent<PubObject>().TopObjectsCount > 0 && ObjectOnGround.GetComponent<PubObject>().TopObjectsCount == TopObjectsMax - 1)
        return true;

        return false;

    }
    void SetCunstrcutedStrNum(int max, SpriteRenderer SPRT, int ID)
    {
        if ((IM.MouseScroll > 0 || IM.RightTrigger) && RandomisedNumber < max - 1 && IM.ActionDelay < Time.fixedTime)
        {
            RandomisedNumber++;
            IM.ActionDelay = Time.fixedTime + 0.1f;
        }
        if ((IM.MouseScroll < 0 || IM.LeftTrigger) && RandomisedNumber > 0 && IM.ActionDelay < Time.fixedTime)
        {
            RandomisedNumber--;
            IM.ActionDelay = Time.fixedTime + 0.1f;
        }

        if(pl.inv.GetItemInDatabase(ID).ObjectPrefsBottom == null) return;

        if (pl.inv.GetItemInDatabase(ID).ObjectPrefsBottom.Length == 1)
        {
            return;
        }

        if (RandomisedNumber >= pl.inv.GetItemInDatabase(ID).ObjectPrefsBottom.Length )
        {
            return;
        }
        SPRT.sprite = pl.inv.GetItemInDatabase(ID).ObjectPrefsBottom[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;

    }
    void RandomiseConstructingObject(GameObject ChildOnMouse, ref GameObject ObjectOnGround, ref StatsControll _StatsControll)
    {
       


        if (BottomCheck( ref _StatsControll))
            ChildOnMouse.GetComponent<SpriteRenderer>().sprite = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsBottom[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;

        if (MidCheck(ref ObjectOnGround, ref _StatsControll))
        ChildOnMouse.GetComponent<SpriteRenderer>().sprite = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsMid[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;
        
        if (TopCheck(ref ObjectOnGround, ref _StatsControll))
         ChildOnMouse.GetComponent<SpriteRenderer>().sprite = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsTop[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;
        

        if (isRandomised || _StatsControll == null) return;


        /*
        if (BottomCheck(ref ObjectOnGround, ref _StatsControll))
        {
            RandomisedNumber = UnityEngine.Random.Range(0, pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsBottom.Length);
            ChildOnMouse.GetComponent<SpriteRenderer>().sprite = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsBottom[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;
  
            isRandomised = true;
            return;
        }

        if (MidCheck(ref ObjectOnGround, ref _StatsControll))
        {
            RandomisedNumber = UnityEngine.Random.Range(0, pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsMid.Length);

                ChildOnMouse.GetComponent<SpriteRenderer>().sprite = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsMid[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;
            
        }

        if (TopCheck(ref ObjectOnGround, ref _StatsControll))
        {
            RandomisedNumber = UnityEngine.Random.Range(0, pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsTop.Length);
                ChildOnMouse.GetComponent<SpriteRenderer>().sprite = pl.inv.GetItemInDatabase(_StatsControll.DatabaseID).ObjectPrefsTop[RandomisedNumber].GetComponent<SpriteRenderer>().sprite;
            
        }
        isRandomised = true;
        */
    }

    void ScroolCamera()
    {
        if (IM.MouseScroll > 0 && MainCamera.orthographicSize > 2f)
        {
         
            MainCamera.orthographicSize -= 0.1f;
        }

        if (IM.MouseScroll < 0 && MainCamera.orthographicSize < 5)
        {
            
            MainCamera.orthographicSize += 0.1f;
        }


    }

    void Remove_PERS_FromOBOnBoard(GameObject Target)
    {
        for (int i = 0; i < OBOnBoard.Count; i++)
        {
            if (OBOnBoard[i].Object != null)
            {
                if (OBOnBoard[i].Object.name == Target.name && OBOnBoard[i].Place.x == ConstructorObjectPosition.x && OBOnBoard[i].Place.y == ConstructorObjectPosition.y && OBOnBoard[i].Object.GetComponent<PubObject>().TopObjectsCount <= 0)
                {
                    OBOnBoard.RemoveAt(i);

                }
            }
        }
    }

    void Remove_PERS_ConstructedObjects(GameObject Target)
    {
        for (int i = 0; i < ConstructedStructures.Count; i++)
        {
            if (ConstructedStructures[i].Object != null)
            {
                if (ConstructedStructures[i].Object.name == Target.name && ConstructedStructures[i].Place.x == ConstructorObjectPosition.x && ConstructedStructures[i].Place.y == ConstructorObjectPosition.y && ConstructedStructures[i].Object.GetComponent<PubObject>().TopObjectsCount <= 0)
                {
                    ConstructedStructures.RemoveAt(i);

                }
            }
        }
    }

    void RemoveFromOBOnBoard(GameObject Target)
    {
        for (int i = 0; i < OBOnBoard.Count; i++)
        {
            if (OBOnBoard[i].Object != null)
                if (OBOnBoard[i].Object == Target)
                    OBOnBoard.RemoveAt(i);


        }

    }

    void RemoveFrom_ConstructedObjects(GameObject Target)
    {
        for (int i = 0; i < ConstructedStructures.Count; i++)
        {
            if (ConstructedStructures[i].Object != null)
                if (ConstructedStructures[i].Object == Target)
                    ConstructedStructures.RemoveAt(i);


        }

    }




    public bool CheckObjectName(string n)
    {
        bool result = false;

        for (int i = 0; i < OBOnBoard.Count; i++)
        {
            if (OBOnBoard[i].Object != null)
            {
                if (OBOnBoard[i].Object.name == n) result = true;
            }
        }

        return result;
    }

    
}

