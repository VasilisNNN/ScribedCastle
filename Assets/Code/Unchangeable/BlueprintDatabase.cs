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
      new string[7] { "the Start", "Початок", "", "", "", "", "" },
      new string[7] { "Start constructions from here.", "", "", "", "", "", "" },
      /*Cost*/ 0
      ));

        items.Add(new Item(
    1, 1, Item.type.item,
    new string[7] { "Tower", "", "", "", "", "", "" },
    new string[7] { "A basic vertical defensive or lookout structure.", "", "", "", "", "", "" },
    /*Cost*/ 0
    ));
        items.Add(new Item(
   2, 2, Item.type.item,
   new string[7] { "Tower 1", "Башта 1", "", "", "", "", "" },
   new string[7] { "A variant or upgraded version of the standard tower.", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   3, 3, Item.type.item,
   new string[7] { "Mansion", "Маєток", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   4, 4, Item.type.item,
   new string[7] { "Mansion 1", "Маєток 1", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   5, 5, Item.type.item,
   new string[7] { "Castle tower", "Замокова вежа", "", "", "", "", "" },
   new string[7] { "A fortified tower that's part of a castle complex.", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   6, 6, Item.type.item,
   new string[7] { "Tower with crops", "", "", "", "", "", "" },
   new string[7] { "Small tower to watch over and protect the crops.", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));

        items.Add(new Item(
       7, 7, Item.type.item,
       new string[7] { "Fort with crops", "", "", "", "", "", "" },
       new string[7] { "Military fort with some greenery inside. ", "", "", "", "", "", "" },
       /*Cost*/ 0
       ));


        items.Add(new Item(
       8, 8, Item.type.item,
       new string[7] { "Church", "", "", "", "", "", "" },
       new string[7] { "Young priest has an order to build a new church.", "", "", "", "", "", "" },
       /*Cost*/ 0
       ));



        items.Add(new Item(
      9, 9, Item.type.item,
      new string[7] { "Wooden fort", "", "", "", "", "", "" },
      new string[7] { "Military fort made out of wood.", "", "", "", "", "", "" },
      /*Cost*/ 0
      ));



        items.Add(new Item(
  10, 10, Item.type.item,
  new string[7] { "Glass tower", "", "", "", "", "", "" },
  new string[7] { "A tower made of glass? Crazy order, but they promise to pay well. ", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));



        items.Add(new Item(
  11, 11, Item.type.item,
  new string[7] { "Glass castle", "", "", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));

        items.Add(new Item(
12, 12, Item.type.item,
new string[7] { "Tower with glass", "", "", "", "", "", "" },
new string[7] { "Tower with glass walls. The person carved in wood can know something about this mansion.", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
   13, 13, Item.type.item,
   new string[7] { "Forest", "", "", "", "", "", "" },
   new string[7] { "The church needs to plant a forest.", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));

  

        items.Add(new Item(
14, 14, Item.type.item,
new string[7] { "Hidden mansion", "", "", "", "", "", "" },
new string[7] { "Try to guess whats inside.", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
 15, 15, Item.type.item,
 new string[7] { "Secret society mansion", "", "", "", "", "", "" },
 new string[7] { "Secret society want this building, but their description is cryptic.", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        

        items.Add(new Item(
 16, 16, Item.type.item,
 new string[7] { "Secret society tower", "", "", "", "", "", "" },
 new string[7] { "Secret society gathers here for important meetings.", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new Item(
 17, 17, Item.type.item,
 new string[7] { "Secret society church", "", "", "", "", "", "" },
 new string[7] { "The church to make some mysterious rituals. Glass statue can know soothing about it.", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new Item(
 18, 18, Item.type.item,
 new string[7] { "Thieves hideout", "", "", "", "", "", "" },
 new string[7] { "The place where thieves can hide and wait. Shady, but they pay well. ", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new Item(
 19, 19, Item.type.item,
 new string[7] { "Thieves guild", "", "", "", "", "", "" },
 new string[7] { "Established place for thieves. Mysterious monument can know something about it.", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));

        items.Add(new Item(
20, 20, Item.type.item,
new string[7] { "Assassins hideout", "", "", "", "", "", "" },
new string[7] { "The place for death givers to rest.", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
21, 21, Item.type.item,
new string[7] { "Assassins manor", "", "", "", "", "", "" },
new string[7] { "The manor of death.", "", "", "", "", "", "" },
/*Cost*/ 0
));
        
        items.Add(new Item(
22, 22, Item.type.item,
new string[7] { "Angels church", "", "", "", "", "", "" },
new string[7] { "Higher creatures need a place to come from the sky.", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
23, 23, Item.type.item,
new string[7] { "Angels miracle", "", "", "", "", "", "" },
new string[7] { "The miracle will shine upon us! ", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
24, 24, Item.type.item,
new string[7] { "Devil house", "", "", "", "", "", "" },
new string[7] { "The hole to the hell opened and the devil himself want you to create a building.", "", "", "", "", "", "" },
/*Cost*/ 0
));


            items.Add(new Item(
    25, 25, Item.type.item,
    new string[7] { "Devil church", "", "", "", "", "", "" },
    new string[7] { "The unholy place.", "", "", "", "", "", "" },
    /*Cost*/ 0
    ));
            items.Add(new Item(
    26, 26, Item.type.item,
    new string[7] { "Abomination", "Сад", "庭", "", "", "", "" },
    new string[7] { "Scariest place in the world. ", "", "", "", "", "", "" },
    /*Cost*/ 0
    ));


    items.Add(new Item(
    27, 27, Item.type.item,
    new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 0
    ));

        items.Add(new Item(
 28, 28, Item.type.item,
 new string[7] { "Hidden mansion", "", "", "", "", "", "" },
 new string[7] { "Try to guess whats inside", "", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new Item(
29, 29, Item.type.item,
new string[7] { "Inn hidden", "", "", "", "", "", "" },
new string[7] { "Travelers can rest here, only block descriptions are not clear.", "", "", "", "", "", "" },
/*Cost*/ 0
));

    }




}
