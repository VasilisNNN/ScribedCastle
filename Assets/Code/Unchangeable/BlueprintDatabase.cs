using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlueprintDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>();

    //Pink - #FF7FA3
    //Green - #7EFF8F
    //Blue - #7EDAFF
    //Yellow - #FFFA00
    //Pink - #FF7FA3
    //Purple - #FF67E0

    void Awake()
    {


        items.Add(new Item(
      0,0, Item.type.item,
      new string[7] { "Small tower", "Маленька башта", "スモールタワー", "", "", "", "" },
      new string[7] { "1", "1", "1", "", "", "", "" },
      /*Cost*/ 0
      ));

        items.Add(new Item(
    1, 1, Item.type.item,
    new string[7] { "Mansion", "Маєток", "邸", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 0
    ));
        items.Add(new Item(
   2, 2, Item.type.item,
   new string[7] { "Tower and fields", "Башта і поля", "タワーとフィールド", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   3, 3, Item.type.item,
   new string[7] { "Mills", "Вітряки", "ミルズ", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   4, 4, Item.type.item,
   new string[7] { "One mantion", "Один маєток", "ワンマンション", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   5, 5, Item.type.item,
   new string[7] { "Castle", "Замок", "城", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   6, 6, Item.type.item,
   new string[7] { "Glass castle", "Скляний замок", "ガラスの城", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));

        items.Add(new Item(
       7, 7, Item.type.item,
       new string[7] { "Sand castle", "Замок з писка", "砂の城", "", "", "", "" },
       new string[7] { "", "", "", "", "", "", "" },
       /*Cost*/ 0
       ));


        items.Add(new Item(
       8, 8, Item.type.item,
       new string[7] { "Large sand castle", "Великий замок з писка", "大きな砂の城", "", "", "", "" },
       new string[7] { "", "", "", "", "", "", "" },
       /*Cost*/ 0
       ));



        items.Add(new Item(
      9, 9, Item.type.item,
      new string[7] { "Wood castle", "Дерев'яний замок", "ウッドキャッスル", "", "", "", "" },
      new string[7] { "", "", "", "", "", "", "" },
      /*Cost*/ 0
      ));



        items.Add(new Item(
  10, 10, Item.type.item,
  new string[7] { "Wood castle 2", "Дерев'яний замок 2", "ウッドキャッスル2", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));



        items.Add(new Item(
  11, 11, Item.type.item,
  new string[7] { "Wood castle with the tower", "Дерев'яний замок з баштою", "天守閣のある木造の城", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));

        items.Add(new Item(
12, 12, Item.type.item,
new string[7] { "Pink castle", "Рожевий замок", "ピンクの城", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
   13, 13, Item.type.item,
   new string[7] { "Rock castle", "Кам'яний замок", "岩城", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));


        items.Add(new Item(
  14, 14, Item.type.item,
  new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));

        items.Add(new Item(
 15, 15, Item.type.item,
 new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new Item(
 16, 16, Item.type.item,
 new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new Item(
 17, 17, Item.type.item,
 new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new Item(
 18, 18, Item.type.item,
 new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new Item(
 19, 19, Item.type.item,
 new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
 new string[7] { "", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));

    }




}
