using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ItemDatabase {
	public List<Item> items = new List<Item>();
    string totalJP = "";
    //Pink - #FF7FA3
    //Green - #7EFF8F
    //Blue - #7EDAFF
    //Yellow - #FFFA00
    //Pink - #FF7FA3
    //Purple - #FF67E0
    [HideInInspector]
    public Tilemap MainTileBase;
    [HideInInspector]
    public Tilemap GreyGround;
    [HideInInspector]
    public Tilemap WaterTileBase ;
    [HideInInspector]
    public Tilemap PitsTileBase ;
    [HideInInspector]
    public Tilemap MudTileBase;

    private TileBase[] StructuresTileList, PlantsRegularTileList;
    public ItemDatabase()
    { 
    
    }
    public void SetData()
    {


        MainTileBase = GameObject.Find("Floor").GetComponent<Tilemap>();
        GreyGround = GameObject.Find("GreyGround").GetComponent<Tilemap>();
        WaterTileBase = GameObject.Find("Water").GetComponent<Tilemap>();
        PitsTileBase = GameObject.Find("PitsTileBase").GetComponent<Tilemap>();
        MudTileBase = GameObject.Find("Mud").GetComponent<Tilemap>();

        StructuresTileList =  new TileBase[] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor"),
        Resources.Load<TileBase>("Brushes/Dark sand"),
        Resources.Load<TileBase>("Brushes/Stone floor"),
         };

        PlantsRegularTileList = new TileBase[] {
        Resources.Load<TileBase>("Brushes/Mud")
         };

        //--------------------RESOURCES--------------------//




        items.Add(new Item(
      1,1, Item.type.item,
      new string[7] { "Wood", "Деревина", "ウッド", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 0
      ));
        items[items.Count - 1].CanStack = true;

        items.Add(new Item(
       2,2, Item.type.item,
       new string[7] { "Stone", "Камінь", "石", "", "", "", "" },
       new string[7] { "", "", "", "", "", "", "" },
       /*Cost*/ 0
       ));
        items[items.Count - 1].CanStack = true;

        items.Add(new Item(
        3,3, Item.type.item,
        new string[7] { "Metal", "", "", "", "", "", "" },
        new string[7] { "", "", "", "", "", "", "" },
        /*Cost*/ 3
        ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 23 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
  9,9, Item.type.item,
  new string[7] { "Gold", "Золото", "金", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 27, };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
12,12, Item.type.item,
new string[7] { "Paper", "Папір", "紙", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        //--------------------FOOD--------------------//

        items.Add(new Item(
39,39, Item.type.item,
new string[7] { "Beetroot", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].Food = true;
        items[items.Count - 1].Satiety = 6;
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1]._bodypart = new Slot.bodypart[1] { Slot.bodypart.Body };
        items[items.Count - 1].MagicEffectToCast = "HealEffect";


  
        items.Add(new Item(
58, 58, Item.type.item,
new string[7] { "Tomato", "Томат", "トマト", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1]._bodypart = new Slot.bodypart[1] { Slot.bodypart.Body };
        items[items.Count - 1].Satiety = 5;


        items.Add(new Item(
59, 59, Item.type.item,
new string[7] { "Corn", "Кукурудза", "トウモロコシ", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1]._bodypart = new Slot.bodypart[1] { Slot.bodypart.Body };
        items[items.Count - 1].Satiety = 5;

        //--------------------CHARACTERS--------------------//



        items.Add(new Item(
81, 81, Item.type.item,
new string[7] { "Client1", "", "", "", "", "", "" },
new string[7] { "Client1", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Peasant1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };



        items.Add(new Item(
82, 82, Item.type.item,
new string[7] { "Client", "", "", "", "", "", "" },
new string[7] { "Client", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Peasant");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
83, 83, Item.type.item,
new string[7] { "Wolf", "", "", "", "", "", "" },
new string[7] { "Wolf", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Wolf");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };



        items.Add(new Item(
85, 85, Item.type.item,
new string[7] { "Guard", "", "", "", "", "", "" },
new string[7] { "Guard", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
 
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Guard");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
86, 86, Item.type.item,
new string[7] { "Knight", "Лицар", "", "", "", "", "" },
new string[7] { "Knight", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
  
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Knight");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };

        items.Add(new Item(
87, 87, Item.type.item,
new string[7] { "Cleric", "Клерик", "", "", "", "", "" },
new string[7] { "Cleric", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
      
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Cleric");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
88, 88, Item.type.item,
new string[7] { "Thief", "Вор", "", "", "", "", "" },
new string[7] { "Thief", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
     
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Thief");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
     

        items.Add(new Item(
89, 89, Item.type.item,
new string[7] { "Heretic", "Єретик", "", "", "", "", "" },
new string[7] { "Heretic", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Heretic");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
90, 90, Item.type.item,
new string[7] { "Demon", "Демон", "", "", "", "", "" },
new string[7] { "Demon", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Demon");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        //--------------------CRAFTING TABLES--------------------//



        items.Add(new Item(
111, 111, Item.type.item,
new string[7] { "Furnace", "Пічка", "炉", "", "", "", "" },
new string[7] { "Earns 6 gold", "Заробляє 6 золота", "6ゴールド獲得", "", "", "", "" },
/*Cost*/ 170
));

        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Furnace");

        items[items.Count - 1].TargetBrush =
        StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 15;

        items.Add(new Item(
112, 112, Item.type.item,
new string[7] { "Merchant", "Купець", "", "", "", "", "" },
new string[7] { "Merchant", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/Merchant");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[2] { 1, 3 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 3, 2 };

        items.Add(new Item(
113, 113, Item.type.item,
new string[7] { "Dark merchant", "Темний купець", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 700
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/DarkMerchant");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
114, 114, Item.type.item,
new string[7] { "Thieves merchant", "Купець-крадій", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 900
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/ThievesMerchant");
        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
115, 115, Item.type.item,
new string[7] { "Merchant lake", "Озерний купець", "", "", "", "", "" },
new string[7] { "Merchant from hell", "", "", "", "", "", "" },
/*Cost*/ 100
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/MerchantLake");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[2] { 1, 3 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 3, 2 };

        items.Add(new Item(
116, 116, Item.type.item,
new string[7] { "Merchant mountain", "Гірський купець", "", "", "", "", "" },
new string[7] { "Merchant from the mountain", "", "", "", "", "", "" },
/*Cost*/ 100
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/MerchantMountain");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[2] { 1, 3 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 3, 2 };

        items.Add(new Item(
118, 118, Item.type.item,
new string[7] { "Merchant hell", "Пекельний Купець", "", "", "", "", "" },
new string[7] { "Merchant from hell", "", "", "", "", "", "" },
/*Cost*/ 100
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/MerchantHell");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[2] { 1, 3 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 3, 2 };


        //--------------------WALLS AND FURNITURE--------------------//

        items.Add(new Item(
      300, 300, Item.type.item,
      new string[7] { "Wall block", "Стіна", "壁", "", "", "", "" },
      new string[7] { "Build this to create your castle", "Побудуйте це, щоб створити свій замок", "これを作って城を作る", "", "", "", "" },
      /*Cost*/ 3
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallBottom")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallTop")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
301, 301, Item.type.item,
new string[7] { "Castle floor", "Підлога замку", "城の床", "", "", "", "" },
new string[7] { "You can build walls on this floor", "На цій підлозі можна будувати стіни", "この階に壁を作ることができる", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Floor");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
302, 302, Item.type.item,
new string[7] { "Sand floor", "Піщана підлога", "砂の床", "", "", "", "" },
new string[7] { "Scribe some sand", "На цій підлозі можна будувати піщані стіни", "このフロアに砂壁を作ることができる", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Sand");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Sand") };
 


        items.Add(new Item(
307, 307, Item.type.item,
new string[7] { "Grass floor", "Трава", "草", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Grass floor");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Grass") };
     


        items.Add(new Item(
308, 308, Item.type.item,
new string[7] { "Stone floor", "Кам'яна підлога", "石の床", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Stone floor");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Stone floor") };




        items.Add(new Item(
309, 309, Item.type.item,
new string[7] { "Dark sand", "Темний пісок", "黒い砂", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Dark sand");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Dark sand") };


        items.Add(new Item(
310, 310, Item.type.item,
new string[7] { "Dark grass", "Темна трава", "暗い草", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Dark grass");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Dark grass") };


    items.Add(new Item(
311, 311, Item.type.item,
new string[7] { "Dark stone", "Темний камінь", "暗い石", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Dark stone");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Dark stone") };


        items.Add(new Item(
320, 320, Item.type.item,
new string[7] { "Castle enter", "Вхід у замок", "城への入り口", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1]._StructureType = Item.StructureType.Protection;

        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/CastleEnters/Castle enter");
        items[items.Count - 1].TargetBrush = StructuresTileList;



        items.Add(new Item(
351, 351, Item.type.item,
new string[7] { "Mill", "Млин", "製粉所", "", "", "", "" },
new string[7] { "Earns 5 gold", "Заробляє 5 золота", "5ゴールド獲得", "", "", "", "" },
/*Cost*/ 900
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Mill");
        items[items.Count - 1].TargetBrush = new TileBase[] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor"),
        Resources.Load<TileBase>("Brushes/Sand"),
        Resources.Load<TileBase>("Brushes/Mud"),
        Resources.Load<TileBase>("Brushes/Dark sand")};


        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 0;



        items.Add(new Item(
352, 352, Item.type.item,
new string[7] { "House", "Будинок", "家", "", "", "", "" },
new string[7] { "Pesants live here", "Тут живуть кріпаки", "農民が住んでいる", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[]
           {
            Resources.Load<GameObject>("Prefabs/Structures/House"),
            Resources.Load<GameObject>("Prefabs/Structures/House1"),
            Resources.Load<GameObject>("Prefabs/Structures/House2")
           };

        items[items.Count - 1].ObjectPrefsMid = new GameObject[]
     {
            Resources.Load<GameObject>("Prefabs/Structures/House"),
            Resources.Load<GameObject>("Prefabs/Structures/House1"),
            Resources.Load<GameObject>("Prefabs/Structures/House2")
     };

        items[items.Count - 1].ObjectPrefsTop = new GameObject[]
     {
            Resources.Load<GameObject>("Prefabs/Structures/House"),
            Resources.Load<GameObject>("Prefabs/Structures/House1"),
            Resources.Load<GameObject>("Prefabs/Structures/House2")
     };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
353, 353, Item.type.item,
new string[7] { "Dirt floor", "Багно", "土間", "", "", "", "" },
new string[7] { "You can plant crops in the dirt", "Ви можете висаджувати рослини в ґрунт", "土に作物を植えることができる", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Mud");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Mud") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
354, 354, Item.type.item,
new string[7] { "River", "Ріка", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = PitsTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/River");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/River") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };

        if (SceneManager.GetActiveScene().name == "Island" ||
            SceneManager.GetActiveScene().name == "Lake" ||
             SceneManager.GetActiveScene().name == "Mountain")
        {
            items.Add(new Item(
    355, 355, Item.type.item,
    new string[7] { "Island soil", "Земля", "土地", "", "", "", "" },
    new string[7] { "Expands available land on the island", "Розширює доступну землю острова", "島で利用可能な土地の拡大", "", "", "", "" },
    /*Cost*/ 150
    ));
            items[items.Count - 1].CanStack = true;
            items[items.Count - 1].Structure = true;
            items[items.Count - 1].TargetTileMap = GreyGround;
            items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

            items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/GreyGround");
            items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Ground") };
            items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
            items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };
        }


        if (SceneManager.GetActiveScene().name == "Hell" )
        {
            items.Add(new Item(
    355, 355, Item.type.item,
    new string[7] { "Island hell soil", "Земля", "土地", "", "", "", "" },
    new string[7] { "Expands available land in the hell", "Розширює доступну землю пекла", "地獄のアクセス可能な土地を拡大する", "", "", "", "" },
    /*Cost*/ 150
    ));
            items[items.Count - 1].CanStack = true;
            items[items.Count - 1].Structure = true;
            items[items.Count - 1].TargetTileMap = GreyGround;
            items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

            items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/GreyGround");
            items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/GroundHell") };
            items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
            items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };
        }




        items.Add(new Item(
370, 370, Item.type.item,
new string[7] { "Guard station", "Пост охорони", "守衛所", "", "", "", "" },
new string[7] { "Spawns guards. Guards kill wolves and can be killed by thieves.", "Спавнить вартових. Вартові вбивають вовків і можуть бути вбиті злодіями.", "衛兵を作る。警備兵は狼を殺し、泥棒に殺されることもある。", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Protection;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/GuardStation");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;


        items.Add(new Item(
375, 375, Item.type.item,
new string[7] { "Knights building", "Будинок лицарів", "ナイツ・ビル", "", "", "", "" },
new string[7] { "Spawns One Knight. Knights kill thieves and can be killed by heretics.", "Спавнить одного лицаря. Лицарі вбивають злодіїв і можуть бути вбиті єретиками.", "騎士を1人作る。騎士は盗賊を殺し、異端者に殺されることもある。", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Protection;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/KnightBuilding");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;


   

        items.Add(new Item(
390, 352, Item.type.item,
new string[7] { "House1", "", "", "", "", "", "" },
new string[7] { "House1", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House1");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
 391, 352, Item.type.item,
 new string[7] { "House2", "", "", "", "", "", "" },
 new string[7] { "House2", "", "", "", "", "", "" },
 /*Cost*/ 10
 ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House2");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
    392, 352, Item.type.item,
    new string[7] { "House3", "", "", "", "", "", "" },
    new string[7] { "House3", "", "", "", "", "", "" },
    /*Cost*/ 10
    ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House3");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
       393, 352, Item.type.item,
       new string[7] { "House4", "", "", "", "", "", "" },
       new string[7] { "House4", "", "", "", "", "", "" },
       /*Cost*/ 10
       ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House4");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
394, 352, Item.type.item,
new string[7] { "House5", "", "", "", "", "", "" },
new string[7] { "House5", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House5");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
 395, 352, Item.type.item,
 new string[7] { "House6", "", "", "", "", "", "" },
 new string[7] { "House6", "", "", "", "", "", "" },
 /*Cost*/ 10
 ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House6");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
398, 398, Item.type.item,
new string[7] { "Clerics house", "Будинок клірика", "", "", "", "", "" },
new string[7] { "Spawns One Cleric. Clerics kill heretics and can be killed by wolves.", "Спавнить одного клірика. Клірики вбивають єретиків і можуть бути вбиті вовками.", "クレリックを1体生む。クレリックは異端者を殺し、狼に殺されることもある。", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Protection;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Clerics house");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;

        items.Add(new Item(
400, 400, Item.type.item,
new string[7] { "Fountain", "Фонтан", "泉", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Fountain");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;



        items.Add(new Item(
   600, 600, Item.type.item,
   new string[7] { "Base wall", "", "", "", "", "", "" },
   new string[7] { "Wall block", "", "", "", "", "", "" },
   /*Cost*/ 3
   ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Base wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Base wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Base wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Base wall top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
620, 600, Item.type.item,
new string[7] { "Base wall mid", "", "", "", "", "", "" },
new string[7] { "Wall block", "", "", "", "", "", "" },
/*Cost*/ 3
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Base wall mid");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
630, 600, Item.type.item,
new string[7] { "Base wall top", "", "", "", "", "", "" },
new string[7] { "Wall block", "", "", "", "", "", "" },
/*Cost*/ 3
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Base wall top");

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
670, 670, Item.type.item,
new string[7] { "Assassins wall", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/AssassinsWall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/AssassinsWall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallTop")};

        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
671, 670, Item.type.item,
new string[7] { "Assassins wall mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
672, 670, Item.type.item,
new string[7] { "Assassins wall top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;



        items.Add(new Item(
675, 675, Item.type.item,
new string[7] { "Thieves wall", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ThievesWall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ThievesWall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ThievesWallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ThievesWallTop")};
        items[items.Count - 1].TargetBrush = StructuresTileList;



        items.Add(new Item(
676, 675, Item.type.item,
new string[7] { "Thieves wall mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ThievesWallMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
677, 675, Item.type.item,
new string[7] { "Thieves wall top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ThievesWallTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
680, 680, Item.type.item,
new string[7] { "Church block", "Церковна стіна", "教会の壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
681, 681, Item.type.item,
new string[7] { "Church block 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom1");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop1")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
682, 682, Item.type.item,
new string[7] { "Church block 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom2");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop2")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
683, 683, Item.type.item,
new string[7] { "Church block 4", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom3");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchBottom3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop3")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
690, 680, Item.type.item,
new string[7] { "Church mid block mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items.Add(new Item(
691, 681, Item.type.item,
new string[7] { "Church mid block mid 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
       
        
        
        items.Add(new Item(
   692, 682, Item.type.item,
new string[7] { "Church mid block mid 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
 684, 683, Item.type.item,
new string[7] { "Church mid block mid 4", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchMid3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
697, 680, Item.type.item,
new string[7] { "Church top block top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
698, 681, Item.type.item,
new string[7] { "Church top block top 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop1");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
699, 682, Item.type.item,
new string[7] { "Church top block top 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop2");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
685, 683, Item.type.item,
new string[7] { "Church top block top 4", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ChurchWall/WallChurchTop3");
        items[items.Count - 1].TargetBrush = StructuresTileList;









        items.Add(new Item(
700, 700, Item.type.item,
new string[7] { "Wood wall", "Дерев'яна стіна", "木の壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WoodWallBottom");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WoodWallBottom")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WoodWallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WoodWallTop")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
710, 700, Item.type.item,
new string[7] { "Wood wall mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WoodWallMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
711, 700, Item.type.item,
new string[7] { "Wood wall mid 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WoodWallMid1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
720, 700, Item.type.item,
new string[7] { "Wood wall top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WoodWallTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

       


        items.Add(new Item(
790, 790, Item.type.item,
new string[7] { "Golden wall", "Золота стіна", "黄金の壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 30
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Gold");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Gold")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Gold_Mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Gold_Top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
795, 790, Item.type.item,
new string[7] { "Golden Wall Mid", "Золота стіна", "黄金の壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 30
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Gold_Mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
800, 790, Item.type.item,
new string[7] { "Golden Wall Top", "Золота стіна", "黄金の壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 30
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Gold_Top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
801, 801, Item.type.item,
new string[7] { "Coal wall", "Вугільна стіна", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 30
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/CoalWall");

        items[items.Count - 1].TargetBrush = StructuresTileList;




        items.Add(new Item(
    810, 810, Item.type.item,
    new string[7] { "Sand wall", "Пісочна стіна", "砂の壁", "", "", "", "" },
    new string[7] { "Can be built on the sand", "Може бути побудований на піску", "砂の上でも建設可能", "", "", "", "" },
    /*Cost*/ 4
    ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Sand");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Sand")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Sand_Mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Sand_Top")};

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Sand") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
820, 810, Item.type.item,
new string[7] { "Sand wall Mid", "Золота стіна середина", "砂の壁 中", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Sand_Mid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Sand") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
830, 810, Item.type.item,
new string[7] { "Sand wall Top", "Золота стіна топ", "砂壁 トップ", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Sand_Top");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Sand") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };






        items.Add(new Item(
        840, 840, Item.type.item,
        new string[7] { "Glass wall", "Скляна стіна", "砂の壁", "", "", "", "" },
        new string[7] { "", "", "", "", "", "", "" },
        /*Cost*/ 10
        ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Glass");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Glass")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Glass_Mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Glass_Top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
850, 840, Item.type.item,
new string[7] { "Glass wall Mid", "Золота стіна середина", "砂の壁 中", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Glass_Mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
860, 840, Item.type.item,
new string[7] { "Glass wall Top", "Золота стіна топ", "砂壁 トップ", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Glass_Top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
920, 920, Item.type.item,
new string[7] { "Secret society wall", "Червона стіна", "赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/SecretSociety");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/SecretSociety")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/SecretSocietyMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/SecretSocietyTop")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
  925, 920, Item.type.item,
  new string[7] { "Secret society wall mid", "Гнила стіна 1", "回転する壁1", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 4
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/SecretSocietyMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
930, 920, Item.type.item,
new string[7] { "Secret society wall top", "Гнила стіна 1", "回転する壁1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/SecretSocietyTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };












        items.Add(new Item(
940, 940, Item.type.item,
new string[7] { "Rotting wall", "Гнила стіна", "回転する壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRotting");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRotting")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRottingMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRottingTop")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
941, 940, Item.type.item,
new string[7] { "Rotting wall Mid", "Гнила стіна середина", "真ん中の赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRottingMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
942, 940, Item.type.item,
new string[7] { "Rotting rooftop", "Гнилий дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRottingTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
960, 960, Item.type.item,
new string[7] { "Devil wall", "Темна стіна", "暗い壁", "", "", "", "" },
new string[7] { "Abandon all hope, ye who enter here.", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Devil wall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Devil wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Devil wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Devil wall top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        
        items.Add(new Item(
961, 960, Item.type.item,
new string[7] { "Devil wall Mid", "Темна стіна середина", "真ん中の赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Devil wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
962, 960, Item.type.item,
new string[7] { "Devil rooftop", "Темний дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Devil wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };






        items.Add(new Item(
   963, 963, Item.type.item,
   new string[7] { "Devil green wall", "Темна стіна", "暗い壁", "", "", "", "" },
   new string[7] { "Through me the way into the suffering city…", "", "", "", "", "", "" },
   /*Cost*/ 4
   ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DevilGreenWall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/DevilGreenWall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/DevilGreenWallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/DevilGreenWallTop")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
964, 963, Item.type.item,
new string[7] { "Devil green wall Mid", "Темна стіна середина", "真ん中の赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DevilGreenWallMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
965, 963, Item.type.item,
new string[7] { "Devil green rooftop", "Темний дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DevilGreenWallTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };





        items.Add(new Item(
   966, 966, Item.type.item,
   new string[7] { "Devil blue wall", "", "", "", "", "", "" },
   new string[7] { "In His will is our peace.", "", "", "", "", "", "" },
   /*Cost*/ 4
   ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DevilBlueWall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/DevilBlueWall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/DevilBlueWallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/DevilBlueWallTop")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
967, 966, Item.type.item,
new string[7] { "Devil blue wall Mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DevilBlueWallMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
968, 966, Item.type.item,
new string[7] { "Devil blue rooftop", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DevilBlueWallTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
970, 970, Item.type.item,
new string[7] { "Assassins wall", "", "", "", "", "", "" },
new string[7] { "Those who bring death rest here", "", "", "", "", "", "" },
/*Cost*/ 8
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/AssassinsWall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/AssassinsWall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallTop")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
971, 970, Item.type.item,
new string[7] { "Assassins wall Mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallMid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
972, 970, Item.type.item,
new string[7] { "Assassins rooftop", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/AssassinsWallTop");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };





        items.Add(new Item(
980, 980, Item.type.item,
new string[7] { "Arch wall", "Стіна з аркою", "", "", "", "", "" },
new string[7] { "Beautiful structure for developed town", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
981, 980, Item.type.item,
new string[7] { "Arch wall mid", "Стіна з аркою середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
982, 980, Item.type.item,
new string[7] { "Arch wall top", "Стіна з аркою вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
983, 983, Item.type.item,
new string[7] { "Arch wall 1", "Стіна з аркою 1", "", "", "", "", "" },
new string[7] { "Beautiful structure for developed town", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall 1");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top 1")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
984, 983, Item.type.item,
new string[7] { "Arch wall mid 1", "Стіна з аркою середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
985, 983, Item.type.item,
new string[7] { "Arch wall top 1", "Стіна з аркою вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
986, 986, Item.type.item,
new string[7] { "Arch wall 2", "Стіна з аркою 2", "", "", "", "", "" },
new string[7] { "Beautiful structure for developed town", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall 2");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid 2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top 2")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
987, 986, Item.type.item,
new string[7] { "Arch wall mid 2", "Стіна з аркою середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
988, 986, Item.type.item,
new string[7] { "Arch wall top 2", "Стіна з аркою вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
989, 989, Item.type.item,
new string[7] { "Arch wall 3", "Стіна з аркою 3", "", "", "", "", "" },
new string[7] { "Beautiful structure for developed town", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall 3");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top 3")};


        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
990, 989, Item.type.item,
new string[7] { "Arch wall mid 3", "Стіна з аркою середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
991, 989, Item.type.item,
new string[7] { "Arch wall top 3", "Стіна з аркою вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/ArchWall/Arch wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



    






        items.Add(new Item(
995, 995, Item.type.item,
new string[7] { "Fat column", "Широка колона", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Fat column");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
996, 996, Item.type.item,
new string[7] { "Thin column", "Вузька колона", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Thin column");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
997, 997, Item.type.item,
new string[7] { "Sphere column", "Сферична колона", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Sphere column");
        items[items.Count - 1].TargetBrush = StructuresTileList;



        items.Add(new Item(
1000, 1000, Item.type.item,
new string[7] { "Basic wall 1", "Базова стіна 1", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall 1");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall 1 mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall 1 top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1001, 1001, Item.type.item,
new string[7] { "Basic wall two windows", "Базова стіна два вікна", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall two windows");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall two windows")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall two windows mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall two windows top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1002, 1002, Item.type.item,
new string[7] { "Basic wall gothic", "Базова готична стіна", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall gothic");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall gothic")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall gothic mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall gothic top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1003, 1003, Item.type.item,
new string[7] { "Basic wall faces", "Базова стіна з обличчами", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall faces");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall faces")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall faces mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall faces top")};





        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items.Add(new Item(
1005, 1000, Item.type.item,
new string[7] { "Wall basic 1 mid", "Базова стіна 1 середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall 1 mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1006, 1001, Item.type.item,
new string[7] { "Wall basic two windows mid", "Базова стіна два вікна середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall two windows mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1007, 1002, Item.type.item,
new string[7] { "Wall basic gothic mid", "Базова готична стіна середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall gothic mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1008, 1003, Item.type.item,
new string[7] { "Wall basic faces mid", "Базова стіна з обличчами середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall faces mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1010, 1000, Item.type.item,
new string[7] { "Basic wall 1 top", "Базова стіна 1 вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall 1 top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1011, 1001, Item.type.item,
new string[7] { "Basic wall two windows top", "Базова стіна два вікна вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall two windows top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1012, 1002, Item.type.item,
new string[7] { "Basic wall gothic top", "Базова готична стіна вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall gothic top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1013, 1003, Item.type.item,
new string[7] { "Basic wall faces top", "Базова стіна з обличчами вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/BasicWall/Basic wall faces top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1015, 1015, Item.type.item,
new string[7] { "Flower wall", "Квіткова стіна", "", "", "", "", "" },
new string[7] { "Structure with flower decoration", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Flower wall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Flower wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Flower wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Flower wall top")};


        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1016, 1016, Item.type.item,
new string[7] { "Flower wall 1", "Квіткова стіна", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Flower wall 1");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Flower wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Flower wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Flower wall top 1")};


        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
1020, 1015, Item.type.item,
new string[7] { "Flower wall mid", "Квіткова стіна середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Flower wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1021, 1016, Item.type.item,
new string[7] { "Flower wall mid", "Квіткова стіна середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Flower wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1025, 1015, Item.type.item,
new string[7] { "Flower wall top", "Квіткова стіна вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Flower wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1026, 1016, Item.type.item,
new string[7] { "Flower wall top 1", "Квіткова стіна вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Flower wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1030, 1030, Item.type.item,
new string[7] { "German wall", "Німецька стіна", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/GermanWall/German wall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/GermanWall/German wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/GermanWall/German wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/GermanWall/German wall top")};



        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1031, 1030, Item.type.item,
new string[7] { "German wall mid", "Німецька стіна середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/German wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1032, 1030, Item.type.item,
new string[7] { "German wall top", "Німецька стіна вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/German wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1033, 1033, Item.type.item,
new string[7] { "Gothic church", "Готичний собор", "ゴシック様式の聖堂", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1034, 1034, Item.type.item,
new string[7] { "Gothic church 1", "Готичний собор 1", "ゴシック様式の聖堂", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1035, 1035, Item.type.item,
new string[7] { "Gothic church 2", "Готичний собор 2", "ゴシック様式の聖堂 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall mid 2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1036, 1036, Item.type.item,
new string[7] { "Gothic church 3", "Готичний собор 3", "ゴシック様式の聖堂 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/GothicWall2/Gothic wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };










        items.Add(new Item(
      1037, 1033, Item.type.item,
      new string[7] { "Gothic church mid", "Готичний собор середня", "", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 6
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
      1038, 1034, Item.type.item,
      new string[7] { "Gothic church mid 1", "Готичний собор середня 1", "", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 6
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
     1039, 1035, Item.type.item,
     new string[7] { "Gothic church mid 2", "Готичний собор середня 2", "", "", "", "", "" },
     new string[7] { "", "", "", "", "", "", "" },
     /*Cost*/ 6
     ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
     1040, 1036, Item.type.item,
     new string[7] { "Gothic church mid 3", "Готичний собор середня 3", "", "", "", "", "" },
     new string[7] { "", "", "", "", "", "", "" },
     /*Cost*/ 6
     ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
       1041, 1033, Item.type.item,
       new string[7] { "Gothic church top", "Готичний собор вершина", "", "", "", "", "" },
       new string[7] { "", "", "", "", "", "", "" },
       /*Cost*/ 6
       ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
      1042, 1034, Item.type.item,
      new string[7] { "Gothic church top 1", "Готичний собор вершина 1", "", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 6
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
    1043, 1035, Item.type.item,
    new string[7] { "Gothic church top 2", "Готичний собор вершина", "", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 6
    ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
    1044, 1036, Item.type.item,
    new string[7] { "Gothic church top 3", "Готичний собор вершина 3", "", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 6
    ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Gothic wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        /////
        ///
        items.Add(new Item(
1045, 1045, Item.type.item,
new string[7] { "Magic church", "Магічний собор", "ゴシック様式の聖堂", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 20
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1046, 1046, Item.type.item,
new string[7] { "Magic church 1", "Магічний собор 1", "ゴシック様式の聖堂", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 20
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1047, 1047, Item.type.item,
new string[7] { "Magic church 2", "Магічний собор 2", "ゴシック様式の聖堂 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 20
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid 2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1048, 1048, Item.type.item,
new string[7] { "Magic church 3", "Магічний собор 3", "ゴシック様式の聖堂 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 20
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
      1049, 1045, Item.type.item,
      new string[7] { "Magic church mid", "Магічний собор середня", "", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 20
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
      1050, 1046,  Item.type.item,
      new string[7] { "Magic church mid 1", "Магічний собор середня 1", "", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 20
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
     1051, 1047,  Item.type.item,
     new string[7] { "Magic church mid 2", "Магічний собор середня 2", "", "", "", "", "" },
     new string[7] { "", "", "", "", "", "", "" },
     /*Cost*/ 20
     ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
    1052, 1048,  Item.type.item,
     new string[7] { "Magic church mid 3", "Магічний собор середня 3", "", "", "", "", "" },
     new string[7] { "", "", "", "", "", "", "" },
     /*Cost*/ 20
     ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
       1053, 1045, Item.type.item,
       new string[7] { "Magic church top", "Магічна собор вершина", "", "", "", "", "" },
       new string[7] { "", "", "", "", "", "", "" },
       /*Cost*/ 20
       ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
      1054, 1046, Item.type.item,
      new string[7] { "Magic church top 1", "Магічна собор вершина 1", "", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 20
      ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
    1055, 1047, Item.type.item,
    new string[7] { "Magic church top 2", "Магічна собор вершина", "", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 20
    ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
    1056, 1048, Item.type.item,
    new string[7] { "Magic church top 3", "Магічна собор вершина 3", "", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 20
    ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MagicWall/Magic wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        // Dragons

        items.Add(new Item(
1057, 1057, Item.type.item,
new string[7] { "Dragon block", "Блок з драконом", "建設ブロックとドラゴン", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1058, 1058, Item.type.item,
new string[7] { "Dragon block 1", "Блок з драконом 1", "建設ブロックとドラゴン 1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };






        items.Add(new Item(
1059, 1059, Item.type.item,
new string[7] { "Dragon block 2", "Блок з драконом 2", "建設ブロックとドラゴン 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 10
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid 2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


    items.Add(new Item(
    1060, 1060, Item.type.item,
        new string[7] { "Dragon block 3", "Блок з драконом 3", "建設ブロックとドラゴン 3", "", "", "", "" },
        new string[7] { "", "", "", "", "", "", "" },
        /*Cost*/ 10
        ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };






        items.Add(new Item(
   1061, 1057, Item.type.item,
   new string[7] { "Dragon church mid", "", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 10
   ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
  1062, 1058, Item.type.item,
  new string[7] { "Dragon church mid 1", "", "", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 10
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
  1063, 1059, Item.type.item,
  new string[7] { "Dragon church mid 2", "", "", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 10
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
  1064, 1060, Item.type.item,
  new string[7] { "Dragon church mid 3", "", "", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 10
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
 1065, 1057, Item.type.item,
 new string[7] { "Dragon church top", "", "", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 10
 ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


items.Add(new Item(
 1066, 1058, Item.type.item,
 new string[7] { "Dragon church top 1", "", "", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 10
 ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

  items.Add(new Item(
   1067, 1059, Item.type.item,
   new string[7] { "Dragon church top 2", "", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 10
   ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
   1068, 1060, Item.type.item,
   new string[7] { "Dragon church top 3", "", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 10
   ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/DragonWall/Dragon wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        // Faces


        items.Add(new Item(
1069, 1069, Item.type.item,
new string[7] { "Faces block", "Блок з обличчам", "顔のあるブロック", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1070, 1070, Item.type.item,
new string[7] { "Faces block 1", "Блок з обличчам 1", "顔のあるブロック 1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1071, 1071, Item.type.item,
new string[7] { "Faces block 2", "Блок з обличчам 2", "顔のあるブロック 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1072, 1072, Item.type.item,
new string[7] { "Faces block 3", "Блок з обличчам 3", "顔のあるブロック 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
          items.Add(new Item(
1072, 1072, Item.type.item,
new string[7] { "Faces block 3", "Блок з обличчам 3", "顔のあるブロック 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1073, 1069, Item.type.item,
new string[7] { "Faces block mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1074, 1070, Item.type.item,
new string[7] { "Faces block mid 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1075, 1071, Item.type.item,
new string[7] { "Faces block mid 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1076, 1072, Item.type.item,
new string[7] { "Faces block mid 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1077, 1069, Item.type.item,
new string[7] { "Faces church top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1078, 1070, Item.type.item,
new string[7] { "Faces church top 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1079, 1071, Item.type.item,
new string[7] { "Faces church top 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1080, 1072, Item.type.item,
new string[7] { "Faces church top 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/FacesWall/Faces wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        
        //Heraldics


        items.Add(new Item(
1081, 1081, Item.type.item,
new string[7] { "Heraldics wall", "Геральдика", "紋章学 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1082, 1082, Item.type.item,
new string[7] { "Heraldics wall 1", "Геральдика 1", "紋章学 1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid 1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1083, 1083, Item.type.item,
new string[7] { "Heraldics wall 2", "Геральдика 2", "紋章学 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid 2")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

 items.Add(new Item(
1084, 1084, Item.type.item,
new string[7] { "Heraldics wall 3", "Геральдика 3", "紋章学 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 12
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid 3")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1085, 1081, Item.type.item,
new string[7] { "Heraldics wall mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1086, 1082, Item.type.item,
new string[7] { "Heraldics wall mid 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1087, 1083, Item.type.item,
new string[7] { "Heraldics wall mid 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1088, 1084, Item.type.item,
new string[7] { "Heraldics wall mid 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
1089, 1081, Item.type.item,
new string[7] { "Heraldics wall top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1090, 1082, Item.type.item,
new string[7] { "Heraldics wall top 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1091, 1083, Item.type.item,
new string[7] { "Heraldics wall top 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1092, 1084, Item.type.item,
new string[7] { "Heraldics wall top 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/HeraldicsWall/Heraldics wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };





        // Marginalia


        items.Add(new Item(
1093, 1093, Item.type.item,
new string[7] { "Marginalia wall", "Маргіналія", "余白の書き添え", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1094, 1094, Item.type.item,
new string[7] { "Marginalia wall 1", "Маргіналія 1", "余白の書き添え 1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid 1" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
1095, 1095, Item.type.item,
new string[7] { "Marginalia wall 2", "Маргіналія 2", "余白の書き添え 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid 2" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1096, 1096, Item.type.item,
new string[7] { "Marginalia wall 3", "Маргіналія 3", "余白の書き添え 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid 3" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1097, 1093, Item.type.item,
new string[7] { "Marginalia wall mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1098, 1094, Item.type.item,
new string[7] { "Marginalia wall mid 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1099, 1095, Item.type.item,
new string[7] { "Marginalia wall mid 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1100, 1096, Item.type.item,
new string[7] { "Marginalia wall mid 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1101, 1093, Item.type.item,
new string[7] { "Marginalia wall top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1102, 1094, Item.type.item,
new string[7] { "Marginalia wall top 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1103, 1095, Item.type.item,
new string[7] { "Marginalia wall top 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1104, 1096, Item.type.item,
new string[7] { "Marginalia wall top 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MarginaliaWall/Marginalia wall top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
1105, 1105, Item.type.item,
new string[7] { "Mountain gothic church", "Гірський готичний храм", "山岳ゴシック様式の教会", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1106, 1106, Item.type.item,
new string[7] { "Mountain gothic church 1", "Гірський готичний храм 1", "山岳ゴシック様式の教会 1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church 1");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid 1" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top 1")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1107, 1107, Item.type.item,
new string[7] { "Mountain gothic church 2", "Гірський готичний храм 2", "山岳ゴシック様式の教会 2", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church 2");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church 2")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid 2" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top 2")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
1108, 1108, Item.type.item,
new string[7] { "Mountain gothic church 3", "Гірський готичний храм 3", "山岳ゴシック様式の教会 3", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church 3");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church 3")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid 3" )};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top 3")};

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1109, 1105, Item.type.item,
new string[7] { "Mountain gothic church mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid");
        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1110, 1106, Item.type.item,
new string[7] { "Mountain gothic church mid 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;
            items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
1111, 1107, Item.type.item,
new string[7] { "Mountain gothic church mid 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
1112, 1108, Item.type.item,
new string[7] { "Mountain gothic church mid 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church mid 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
1113, 1105, Item.type.item,
new string[7] { "Mountain gothic church top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top");
        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
1114, 1106, Item.type.item,
new string[7] { "Mountain gothic church top 1", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top 1");
        items[items.Count - 1].TargetBrush = StructuresTileList;


        items.Add(new Item(
1115, 1107, Item.type.item,
new string[7] { "Mountain gothic church top 2", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top 2");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        items.Add(new Item(
1116, 1108, Item.type.item,
new string[7] { "Mountain gothic church top 3", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 6
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/MountainGothicWall/Mountain gothic church top 3");
        items[items.Count - 1].TargetBrush = StructuresTileList;

        //-------------------------- FARMS ----------------------------//


        items.Add(new Item(
1300, 1300, Item.type.item,
new string[7] { "Tomato farm", "Томати", "トマト農園", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 1 gold here", "Селяни можуть збирати тут їжу і заробляти 1 золотий", "農民はここで食料を集め、1ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 15
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Tomato farm");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };

        items.Add(new Item(
1301, 1301, Item.type.item,
new string[7] { "Trees", "Дерева", "樹木", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 20
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Trees");
        items[items.Count - 1].TargetBrush = new TileBase[] { 
        Resources.Load<TileBase>("Brushes/Mud"), 
        Resources.Load<TileBase>("Brushes/Grass"),
         Resources.Load<TileBase>("Brushes/Sand"),
        Resources.Load<TileBase>("Brushes/Dark sand")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };

        items.Add(new Item(
1302, 1301, Item.type.item,
new string[7] { "Trees 1", "Дерева", "樹木", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Trees");
        items[items.Count - 1].TargetBrush = new TileBase[] {
        Resources.Load<TileBase>("Brushes/Mud"),
        Resources.Load<TileBase>("Brushes/Grass"),
         Resources.Load<TileBase>("Brushes/Sand"),
        Resources.Load<TileBase>("Brushes/Dark sand")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1303, 1303, Item.type.item,
new string[7] { "Corn farm", "Кукурудза", "トウモロコシ畑", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 2 gold here", "Селяни можуть збирати тут їжу і заробляти 2 золотий", "農民はここで食料を集め、2ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 2
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Corn farm");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1304, 1304, Item.type.item,
new string[7] { "Wheat farm", "Пшениця", "小麦畑", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 3 gold here", "Селяни можуть збирати тут їжу і заробляти 3 золотий", "農民はここで食料を集め、3ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Wheat farm");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1305, 1305, Item.type.item,
new string[7] { "Eggplant farm", "Баклажан", "茄子", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 1 gold here", "Селяни можуть збирати тут їжу і заробляти 1 золотий", "農民はここで食料を集め、1ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 2
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Eggplant farm");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1307, 1307, Item.type.item,
new string[7] { "Pumpkin farm", "Гарбузова ферма", "かぼちゃ農園", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 2 gold here", "Селяни можуть збирати тут їжу і заробляти 2 золотий", "農民はここで食料を集め、2ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 2
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Pumpkin farm");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1320, 1320, Item.type.item,
new string[7] { "Bush", "Кущ", "ブッシュ", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Bush");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;


        items.Add(new Item(
1321, 1321, Item.type.item,
new string[7] { "Bush square", "Квадратний кущ", "四角い茂み", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Bush square");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;


        items.Add(new Item(
1322, 1322, Item.type.item,
new string[7] { "Tiny bush", "Малий кущ", "小さな茂み", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Tiny Bush");
        items[items.Count - 1].TargetBrush = PlantsRegularTileList;




        //--------------------ENEMIES--------------------//


        //--------------------------------------------------------------FRIENDS---------------------------------------------------------//


        //------------------------------------NATURE---------------------------------//


        //------------------DECORATION---------------//
        items.Add(new Item(
5000, 5000, Item.type.item,
new string[7] { "Statue", "Статуя", "像", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1000
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Statue");

        items[items.Count - 1].TargetBrush = StructuresTileList;

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        
        items[items.Count - 1].BuildingCost = 12;


        items.Add(new Item(
5001, 5001, Item.type.item,
new string[7] { "Glass statue", "Скляна статуя", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1500
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Glass statue");

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;




        items.Add(new Item(
5002, 5002, Item.type.item,
new string[7] { "Street light", "Вуличний ліхтар", "街灯", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 50
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/StreetLight");

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 10;



        items.Add(new Item(
5003, 5003, Item.type.item,
new string[7] { "Mysterious statue", "Містична статуя", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 2000
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Mysterious statue");

        items[items.Count - 1].TargetBrush = StructuresTileList;
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;



        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemNames.Length >= 3)
            {
                if (items[i].itemNames[2] != null)
                {
                    if (items[i].itemNames[2].ToArray().Length >= 1)
                    {
                        for (int t = 0; t < items[i].itemNames[2].ToArray().Length; t++)
                        {
                            if (!totalJP.Contains(items[i].itemNames[2].ToArray()[t]))
                            {
                                totalJP += items[i].itemNames[2].ToArray()[t];
                            }
                        }
                    }
                }
            }

            if (items[i].itemDesc.Length >= 3)
            {
                if (items[i].itemDesc[2] != null)
                {
                    if (items[i].itemDesc[2].ToArray().Length >= 1)
                    {
                        for (int t = 0; t < items[i].itemDesc[2].ToArray().Length; t++)
                        {
                            if (!totalJP.Contains(items[i].itemDesc[2].ToArray()[t]))
                            {
                                totalJP += items[i].itemDesc[2].ToArray()[t];
                            }
                        }
                    }
                }
            }


        }

      //  Debug.Log("Total JP Characters " + totalJP);

    }


      

}
