using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
80, 80, Item.type.item,
new string[7] { "Waitress", "", "", "", "", "", "" },
new string[7] { "Waitress", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Waitress");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 43 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };

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
new string[7] { "Knight", "", "", "", "", "", "" },
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
new string[7] { "Cleric", "", "", "", "", "", "" },
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
new string[7] { "Thief", "", "", "", "", "", "" },
new string[7] { "Thief", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
     
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Thief");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
     

        items.Add(new Item(
89, 89, Item.type.item,
new string[7] { "Heretic", "", "", "", "", "", "" },
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
new string[7] { "Demon", "", "", "", "", "", "" },
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
        
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 15;

        items.Add(new Item(
112, 112, Item.type.item,
new string[7] { "Merchant", "", "", "", "", "", "" },
new string[7] { "Merchant", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/Merchant");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        items[items.Count - 1].NeedItemsIDs = new int[2] { 1, 3 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 3, 2 };

        items.Add(new Item(
113, 113, Item.type.item,
new string[7] { "Dark merchant", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 100
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/DarkMerchant");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
   

        items.Add(new Item(
114, 114, Item.type.item,
new string[7] { "Thieves merchant", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 200
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1].CantBeSold = true;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/CraftingStructures/ThievesMerchant");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };






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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
new string[7] { "You can build sand walls on this floor", "На цій підлозі можна будувати піщані стіни", "このフロアに砂壁を作ることができる", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Sand");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Sand") };
 


        items.Add(new Item(
303, 303, Item.type.item,
new string[7] { "Rock floor", "Кам'яна підлога", "岩床", "", "", "", "" },
new string[7] { "You can build rock walls on this floor", "На цій підлозі можна будувати кам'яні стіни", "このフロアに岩壁を作ることができる", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Rock");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
304, 304, Item.type.item,
new string[7] { "Pit", "Прірва", "穴", "", "", "", "" },
new string[7] { "Hole in the ground", "Діра у землі", "地面の穴", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
   
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Pit");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Pit") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };

        items.Add(new Item(
305, 305, Item.type.item,
new string[7] { "Water ditch", "Рів", "水路", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/WaterDitch");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/WaterDitch") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
306, 306, Item.type.item,
new string[7] { "Road", "Дорога", "道路", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Road");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Road") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/GrassRegular") };
     


        items.Add(new Item(
308, 308, Item.type.item,
new string[7] { "Stone floor", "", "", "", "", "", "" },
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
new string[7] { "Dark sand", "", "", "", "", "", "" },
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
        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};


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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};
                    
        items[items.Count - 1].ObjectPrefsBottom = new GameObject[7]
      {Resources.Load<GameObject>("Prefabs/Structures/House"),
            Resources.Load<GameObject>("Prefabs/Structures/House1"),
            Resources.Load<GameObject>("Prefabs/Structures/House2"),
           Resources.Load<GameObject>("Prefabs/Structures/House3"),
           Resources.Load<GameObject>("Prefabs/Structures/House4"),
           Resources.Load<GameObject>("Prefabs/Structures/House5"),
           Resources.Load<GameObject>("Prefabs/Structures/House6")};

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
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/River");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/River") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
355, 355, Item.type.item,
new string[7] { "Island soil", "Земля", "土地", "", "", "", "" },
new string[7] { "Expands available land on the island", "Розширює доступну землю острова", "島で利用可能な土地の拡大", "", "", "", "" },
/*Cost*/ 1
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = GreyGround;
        items[items.Count - 1]._StructureType = Item.StructureType.Tiles;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/GreyGround");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Ground") };
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };



        items.Add(new Item(
356, 354, Item.type.item,
new string[7] { "Bank 1", "", "", "", "", "", "" },
new string[7] { "Bank 1", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Bank 1");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};
        
        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;

        items.Add(new Item(
357, 354, Item.type.item,
new string[7] { "Bank 2", "", "", "", "", "", "" },
new string[7] { "Bank 2", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Bank 2");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;


        items.Add(new Item(
358, 354, Item.type.item,
new string[7] { "Bank 3", "", "", "", "", "", "" },
new string[7] { "Bank 3", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/Bank 3");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;



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

        items[items.Count - 1].TargetBrush = new TileBase[1] {
        Resources.Load<TileBase>("Brushes/Floor")};

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

        items[items.Count - 1].TargetBrush = new TileBase[1] {
        Resources.Load<TileBase>("Brushes/Floor")};

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House1");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};
        
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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House2");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};
        
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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House3");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};
        
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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House4");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House5");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/House6");

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};

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

        items[items.Count - 1].TargetBrush = new TileBase[2] {
        Resources.Load<TileBase>("Brushes/Grass"),
        Resources.Load<TileBase>("Brushes/Floor")};

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

        items[items.Count - 1].TargetBrush = new TileBase[1] {
        Resources.Load<TileBase>("Brushes/Floor")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;

        items.Add(new Item(
401, 401, Item.type.item,
new string[7] { "Fish fountain", "Фонтан з рибою", "魚の像のある噴水", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/FountainFish");

        items[items.Count - 1].TargetBrush = new TileBase[1] {
        Resources.Load<TileBase>("Brushes/Floor")};

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;


        items.Add(new Item(
402, 402, Item.type.item,
new string[7] { "Frog fountain", "Фонтан з жабою", "カエルの像がある噴水", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 180
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Structures/FountainFrog");

        items[items.Count - 1].TargetBrush = new TileBase[1] {
        Resources.Load<TileBase>("Brushes/Floor")};

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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

   


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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };




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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };




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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };




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
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallChurchBottom");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallChurchBottom")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallChurchMid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallChurchTop")};
        
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallChurchBottom1");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallChurchBottom1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallChurchMid1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/WallChurchTop1")};


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallChurchMid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallChurchMid1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallChurchTop");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };



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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallChurchTop1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };



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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };
        
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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
750, 750, Item.type.item,
new string[7] { "Ethnic Wall", "Народна стіна", "民族の壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Ethnic");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Ethnic")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Ethnic_Mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_Ethnic_Top")};

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
760, 750, Item.type.item,
new string[7] { "Ethnic Wall Mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Ethnic_Mid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
770, 750, Item.type.item,
new string[7] { "Ethnic Wall Top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_Ethnic_Top");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
775, 775, Item.type.item,
new string[7] { "Wall with plants", "Стіна з рослинами", "植物のある壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_With_Plants");


        items[items.Count - 1].ObjectPrefsBottom = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_With_Plants")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_With_Plants_Mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[1] {
            Resources.Load<GameObject>("Prefabs/Walls/Wall_With_Plants_Top")};

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
780, 775, Item.type.item,
new string[7] { "Wall with plants Mid", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_With_Plants_Mid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
785, 775, Item.type.item,
new string[7] { "Wall with plants Top", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Wall_With_Plants_Top");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };





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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
870, 870, Item.type.item,
new string[7] { "Rock wall", "Кам'яна стіна", "岩壁", "", "", "", "" },
new string[7] { "Can be built on the rock", "Може бути побудований на камені", "岩の上に建てることができる", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRock");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRock"),
            Resources.Load<GameObject>("Prefabs/Walls/WallRock1") };

        items[items.Count - 1].ObjectPrefsMid = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRockMid"),
         Resources.Load<GameObject>("Prefabs/Walls/WallRockMid1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRockTop"),
        Resources.Load<GameObject>("Prefabs/Walls/WallRockTop1")};

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
871, 870, Item.type.item,
new string[7] { "Rock wall 1", "Кам'яна стіна середина", "真ん中の岩壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRock1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
875, 870, Item.type.item,
new string[7] { "Rock wall Mid", "Кам'яна стіна середина", "真ん中の岩壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRockMid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
876, 870, Item.type.item,
new string[7] { "Rock wall Mid 1", "Кам'яна стіна середина 1", "真ん中の岩壁1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRockMid1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
880, 870, Item.type.item,
new string[7] { "Rock wall Top", "Кам'яна стіна верх", "頂上の岩壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRockTop");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
881, 870, Item.type.item,
new string[7] { "Rock wall Top 1", "Кам'яна стіна верх 1", "頂上の岩壁1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRockTop1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
890, 890, Item.type.item,
new string[7] { "Red wall", "Червона стіна", "赤い壁", "", "", "", "" },
new string[7] { "Can be build on the rock", "Може бути побудований на камені", "岩の上に建てることができる", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRed");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRed"),
          Resources.Load<GameObject>("Prefabs/Walls/WallRed1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRedMid"),
         Resources.Load<GameObject>("Prefabs/Walls/WallRedMid1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallRedTop"),
          Resources.Load<GameObject>("Prefabs/Walls/WallRedTop1")};

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
  891, 890, Item.type.item,
  new string[7] { "Red wall 1", "Червона стіна 1", "赤い壁1", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 4
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRed1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
895, 890, Item.type.item,
new string[7] { "Red wall Mid", "Червона стіна середина", "真ん中の赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRedMid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
896, 890, Item.type.item,
new string[7] { "Red wall Mid 1", "Червона стіна середина 1", "真ん中の赤い壁1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRedMid1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
900, 890, Item.type.item,
new string[7] { "Red rooftop", "Червоний дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRedTop");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
901, 890, Item.type.item,
new string[7] { "Red rooftop", "Червоний дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallRedTop1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Rock") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
905, 905, Item.type.item,
new string[7] { "Pink wall", "Червона стіна", "赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallPink");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallPink"),
          Resources.Load<GameObject>("Prefabs/Walls/WallPink1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallPinkMid"),
         Resources.Load<GameObject>("Prefabs/Walls/WallPinkMid1")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/Walls/WallPinkTop"),
          Resources.Load<GameObject>("Prefabs/Walls/WallPinkTop1")};

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
  906, 905, Item.type.item,
  new string[7] { "Pink wall 1", "Червона стіна 1", "赤い壁1", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 4
  ));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallPink1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
910, 905, Item.type.item,
new string[7] { "Pink wall Mid", "Червона стіна середина", "真ん中の赤い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallPinkMid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
911, 905, Item.type.item,
new string[7] { "Pink wall Mid 1", "Червона стіна середина 1", "真ん中の赤い壁1", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallPinkMid1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
915, 905, Item.type.item,
new string[7] { "Pink rooftop", "Червоний дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallPinkTop");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items.Add(new Item(
916, 905, Item.type.item,
new string[7] { "Pink rooftop", "Червоний дах", "赤い屋上", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/WallPinkTop1");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
960, 960, Item.type.item,
new string[7] { "Devil wall", "Темна стіна", "暗い壁", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };






        items.Add(new Item(
   963, 963, Item.type.item,
   new string[7] { "Devil green wall", "Темна стіна", "暗い壁", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };





        items.Add(new Item(
   966, 966, Item.type.item,
   new string[7] { "Devil blue wall", "", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };




        items.Add(new Item(
970, 970, Item.type.item,
new string[7] { "Assassins wall", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };





        items.Add(new Item(
980, 980, Item.type.item,
new string[7] { "Arch wall", "Стіна з аркою", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Arch wall");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Arch wall")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Arch wall mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Arch wall top")};


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
985, 980, Item.type.item,
new string[7] { "Arch wall mid", "Стіна з аркою середина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Arch wall mid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
990, 980, Item.type.item,
new string[7] { "Arch wall top", "Стіна з аркою вершина", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Arch wall top");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };


        items.Add(new Item(
991, 991, Item.type.item,
new string[7] { "Fat column", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Fat column");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


        items.Add(new Item(
992, 992, Item.type.item,
new string[7] { "Thin column", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Thin column");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


        items.Add(new Item(
993, 993, Item.type.item,
new string[7] { "Sphere column", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 5
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Sphere column");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };




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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Basic wall 1");

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Basic wall 1")};

        items[items.Count - 1].ObjectPrefsMid = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Basic wall 1 mid")};

        items[items.Count - 1].ObjectPrefsTop = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/Walls/Basic wall 1 top")};


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Basic wall 1 mid");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Walls/Basic wall 1 top");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };



        items.Add(new Item(
1015, 1015, Item.type.item,
new string[7] { "Flower wall", "Квіткова стіна", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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


        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };



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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Mud") };

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
        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Mud"), Resources.Load<TileBase>("Brushes/Grass") };

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
        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Ground"), Resources.Load<TileBase>("Brushes/Grass") };

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
        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Mud"), Resources.Load<TileBase>("Brushes/Grass") };

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
        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Mud"), Resources.Load<TileBase>("Brushes/Grass") };

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
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Ground") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1306, 1306, Item.type.item,
new string[7] { "Mushroom", "Гриб", "キノコ", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 1 gold here", "Селяни можуть збирати тут їжу і заробляти 1 золотий", "農民はここで食料を集め、1ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 2
));
        items[items.Count - 1].CanStack = true;
  
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Mushroom");
        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Ground") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };



        items.Add(new Item(
1307, 1307, Item.type.item,
new string[7] { "Pumpkin farm", "Гарбузова ферма", "", "", "", "", "" },
new string[7] { "Peasants can collect food and earn 2 gold here", "Селяни можуть збирати тут їжу і заробляти 2 золотий", "農民はここで食料を集め、2ゴールドを得ることができる。", "", "", "", "" },
/*Cost*/ 2
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Farms;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Pumpkin farm");
        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Mud"), Resources.Load<TileBase>("Brushes/Grass") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 44 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 1 };


        items.Add(new Item(
1320, 1320, Item.type.item,
new string[7] { "Boosh", "Кущ", "灌木", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 4
));
        items[items.Count - 1].CanStack = true;
        //items[items.Count - 1].Plague = 1;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Farms/Boosh");

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

       
        //--------------------ENEMIES--------------------//


        items.Add(new Item(
3000, 3000, Item.type.item,
new string[3] { "Enemy Bird", "", "" },
new string[1] { "Enemy Bird" },
/*Cost*/ 0
));
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Enemies/Enemy Bird");


        //--------------------------------------------------------------FRIENDS---------------------------------------------------------//

    
        items.Add(new Item(
3503, 3503, Item.type.item,
new string[7] { "Miner", "", "", "", "", "", "" },
new string[7] { "This frog picks 10 stones for you, then it turns back into the egg.", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].Durability = 10;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Friend miner");
        items[items.Count - 1].NeedItemsIDs = new int[2] { 43, 9 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 1, 2 };

        items.Add(new Item(
3504, 3504, Item.type.item,
new string[7] { "Lumber", "", "", "", "", "", "" },
new string[7] { "This frog chops 15 trees for you, then it turns back into the egg.", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].TargetTileMap = MainTileBase;

        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].Durability = 15;
        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/Characters/Friend lumber");
        items[items.Count - 1].NeedItemsIDs = new int[2] { 43, 9 };
        items[items.Count - 1].NeedItemsCounts = new int[2] { 1,2 };



        //------------------------------------NATURE---------------------------------//
        items.Add(new Item(
4000, 4000, Item.type.item,
new string[7] { "Lake", "Озеро", "湖", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 50
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/StructuresNature/Lake");

        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Ground"), Resources.Load<TileBase>("Brushes/Grass") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };

        items[items.Count - 1].ObjectPrefsBottom = new GameObject[2] {
            Resources.Load<GameObject>("Prefabs/StructuresNature/Lake"),
            Resources.Load<GameObject>("Prefabs/StructuresNature/Lake 1")};
        items[items.Count - 1].BuildingCost = 12;

        items.Add(new Item(
4001, 4000, Item.type.item,
new string[7] { "Lake 1", "", "", "", "", "", "" },
new string[7] { "Lake 1", "", "", "", "", "", "" },
/*Cost*/ 50
));
        items[items.Count - 1].CanStack = true;
        items[items.Count - 1].Structure = true;
        items[items.Count - 1].TargetTileMap = MainTileBase;
        items[items.Count - 1]._StructureType = Item.StructureType.Building;

        items[items.Count - 1].ObjectPrefs = Resources.Load<GameObject>("Prefabs/StructuresNature/Lake 1");

        items[items.Count - 1].TargetBrush = new TileBase[2] { Resources.Load<TileBase>("Brushes/Ground"), Resources.Load<TileBase>("Brushes/Grass") };

        items[items.Count - 1].NeedItemsIDs = new int[1] { 1 };
        items[items.Count - 1].NeedItemsCounts = new int[1] { 2 };
        items[items.Count - 1].BuildingCost = 12;


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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };


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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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

        items[items.Count - 1].TargetBrush = new TileBase[1] { Resources.Load<TileBase>("Brushes/Floor") };

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
