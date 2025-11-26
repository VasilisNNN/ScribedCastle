using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlueprintDatabase {
	public List<Item> items = new List<Item>();

    //Pink - #FF7FA3
    //Green - #7EFF8F
    //Blue - #7EDAFF
    //Yellow - #FFFA00
    //Pink - #FF7FA3
    //Purple - #FF67E0

    public void SetData()
    {


        items.Add(new Item(
      0,0, Item.type.item,
      new string[7] { "the Start", "Початок", "", "", "", "", "" },
      new string[7] { "Start constructions from here.", "Почніть будівництво звідси.", "", "", "", "", "" },
      /*Cost*/ 0
      ));

        items.Add(new Item(
    1, 1, Item.type.item,
    new string[7] { "Tower", "Башта", "", "", "", "", "" },
    new string[7] { "A basic vertical defensive or lookout structure.", "Базова оборонна або спостережна споруда.", "", "", "", "", "" },
    /*Cost*/ 0
    ));
        items.Add(new Item(
   2, 2, Item.type.item,
   new string[7] { "Tower 1", "Башта 1", "", "", "", "", "" },
   new string[7] { "A variant or upgraded version of the standard tower.", "Модернізована версія стандартної вежі.", "", "", "", "", "" },
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
   new string[7] { "A fortified tower that's part of a castle complex.", "Укріплена вежа, що є частиною замкового комплексу.", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items.Add(new Item(
   6, 6, Item.type.item,
   new string[7] { "Tower with crops", "Вежа з городом", "", "", "", "", "" },
   new string[7] { "Small tower to watch over and protect the crops.", "Невелика вежа для спостереження та захисту врожаю.", "", "", "", "", "" },
   /*Cost*/ 0
   ));

        items.Add(new Item(
       7, 7, Item.type.item,
       new string[7] { "Fort with crops", "Форт з городом", "", "", "", "", "" },
       new string[7] { "Military fort with some greenery inside. ", "Військовий форт з невеликою зеленою зоною всередині. ", "", "", "", "", "" },
       /*Cost*/ 0
       ));


        items.Add(new Item(
       8, 8, Item.type.item,
       new string[7] { "Church", "Церква", "", "", "", "", "" },
       new string[7] { "Young priest has an order to build a new church.", "Молодий священик отримав наказ побудувати нову церкву.", "", "", "", "", "" },
       /*Cost*/ 0
       ));



        items.Add(new Item(
      9, 9, Item.type.item,
      new string[7] { "Wooden fort", "Дерев'яна фортеця", "", "", "", "", "" },
      new string[7] { "Military fort made out of wood.", "Військовий форт, зроблений з дерева.", "", "", "", "", "" },
      /*Cost*/ 0
      ));



        items.Add(new Item(
  10, 10, Item.type.item,
  new string[7] { "Glass tower", "Скляна вежа", "", "", "", "", "" },
  new string[7] { "A tower made of glass? Crazy order, but they promise to pay well. ", "Вежа зі скла? Божевільне замовлення, але вони обіцяють добре заплатити.", "", "", "", "", "" },
  /*Cost*/ 0
  ));



        items.Add(new Item(
  11, 11, Item.type.item,
  new string[7] { "Glass castle", "Скляний замок", "", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));

        items.Add(new Item(
12, 12, Item.type.item,
new string[7] { "Tower with glass", "Вежа зі склом", "", "", "", "", "" },
new string[7] { "Tower with glass walls. The person carved in wood can know something about this mansion.", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
   13, 13, Item.type.item,
   new string[7] { "Forest", "Ліс", "", "", "", "", "" },
   new string[7] { "The church needs to plant a forest.", "Церква повинна посадити ліс.", "", "", "", "", "" },
   /*Cost*/ 0
   ));

  

        items.Add(new Item(
14, 14, Item.type.item,
new string[7] { "Hidden mansion", "Прихований маєток", "", "", "", "", "" },
new string[7] { "Try to guess whats inside.", "Спробуй вгадати, що знаходиться всередині.", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
 15, 15, Item.type.item,
 new string[7] { "Secret society mansion", "Особняк таємного товариства", "", "", "", "", "" },
 new string[7] { "Secret society want this building, but their description is cryptic.", "Таємне товариство хоче цю будівлю, але їх опис є загадковим.", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        

        items.Add(new Item(
 16, 16, Item.type.item,
 new string[7] { "Secret society tower", "Маєток таємного товариства", "", "", "", "", "" },
 new string[7] { "Secret society gathers here for important meetings.", "Таємне товариство збирається тут для важливих зустрічей.", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new Item(
 17, 17, Item.type.item,
 new string[7] { "Secret society church", "Церква таємного товариства", "", "", "", "", "" },
 new string[7] { "The church to make some mysterious rituals. Glass statue can know soothing about it.", "Церква для проведення таємничих ритуалів. Скляна статуя може знати про це заспокійливе.", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new Item(
 18, 18, Item.type.item,
 new string[7] { "Thieves hideout", "Схованка злодіїв", "", "", "", "", "" },
 new string[7] { "The place where thieves can hide and wait. Shady, but they pay well. ", "Місце, де злодії можуть сховатися і чекати. Мутне замовлення, але платять добре. ", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new Item(
 19, 19, Item.type.item,
 new string[7] { "Thieves guild", "Гільдія злодіїв", "", "", "", "", "" },
 new string[7] { "Established place for thieves. Mysterious monument can know something about it.", "Місце для злодіїв. Таємничий пам'ятник може щось про це знати.", "", "", "", "", "" },
 /*Cost*/ 0
 ));

        items.Add(new Item(
20, 20, Item.type.item,
new string[7] { "Assassins hideout", "Схованка вбивць", "", "", "", "", "" },
new string[7] { "The place for death givers to rest.", "Місце спочинку тих, хто приносить смерть.", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
21, 21, Item.type.item,
new string[7] { "Assassins manor", "Маєток вбивць", "", "", "", "", "" },
new string[7] { "The manor of death.", "Маєток смерті.", "", "", "", "", "" },
/*Cost*/ 0
));
        
        items.Add(new Item(
22, 22, Item.type.item,
new string[7] { "Angels church", "Церква Ангелів", "", "", "", "", "" },
new string[7] { "Higher creatures need a place to come from the sky.", "Вищі істоти потребують місця, куди вони можуть спуститися з неба.", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
23, 23, Item.type.item,
new string[7] { "Angels miracle", "Чудо ангелів", "", "", "", "", "" },
new string[7] { "The miracle will shine upon us! ", "Чудо осяє нас! ", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
24, 24, Item.type.item,
new string[7] { "Devil house", "Будинок диявола", "", "", "", "", "" },
new string[7] { "The hole to the hell opened and the devil himself want you to create a building.", "Відкрилася діра в пекло, і сам диявол хоче, щоб ти побудував будівлю.", "", "", "", "", "" },
/*Cost*/ 0
));


            items.Add(new Item(
    25, 25, Item.type.item,
    new string[7] { "Devil church", "Диявольська церква", "", "", "", "", "" },
    new string[7] { "The unholy place.", "Нечестиве місце.", "", "", "", "", "" },
    /*Cost*/ 0
    ));
            items.Add(new Item(
    26, 26, Item.type.item,
    new string[7] { "Abomination", "Сад", "庭", "", "", "", "" },
    new string[7] { "Scariest place in the world. ", "Найстрашніше місце в світі.", "", "", "", "", "" },
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
 new string[7] { "Hidden mansion", "Прихований особняк", "", "", "", "", "" },
 new string[7] { "Try to guess whats inside", "Спробуйте вгадати, що знаходиться всередині", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new Item(
29, 29, Item.type.item,
new string[7] { "Inn hidden", "Прихований готель", "", "", "", "", "" },
new string[7] { "Travelers can rest here, only block descriptions are not clear.", "Мандрівники можуть тут відпочити, тільки опис замовлення нечіткий.", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
30, 30, Item.type.item,
new string[7] { "Heraldic castle", "Геральдичний замок", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
31, 31, Item.type.item,
new string[7] { "Heraldic mansion", "Геральдичний маєток", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
32, 32, Item.type.item,
new string[7] { "Magic castle", "Магічний замок", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
33, 33, Item.type.item,
new string[7] { "Magic districts", "Магічний квартал", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
34, 34, Item.type.item,
new string[7] { "Magic farm", "Мфгічна ферма", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
35, 35, Item.type.item,
new string[7] { "Magic mannor", "Магічний маєток", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
36, 36, Item.type.item,
new string[7] { "Magic rich mannor", "Магічний багатий маєток", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
37, 37, Item.type.item,
new string[7] { "Magic tiny farm", "Маленька магічна ферма", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));



        items.Add(new Item(
38, 38, Item.type.item,
new string[7] { "Magic village", "Магічне село", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));




        items.Add(new Item(
39, 39, Item.type.item,
new string[7] { "Magic village 1", "Магічне друге село", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));




        items.Add(new Item(
40, 40, Item.type.item,
new string[7] { "Mountain church", "Гірська церква", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
41, 41, Item.type.item,
new string[7] { "Mountain advanced church", "Гірська покращена церква", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
42, 42, Item.type.item,
new string[7] { "Mountain village", "Гірське село", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
43, 43, Item.type.item,
new string[7] { "st. Peters church", "Церква святого Петра", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
44, 44, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
45, 45, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
46, 46, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
47, 47, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
48, 48, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new Item(
49, 49, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
50, 50, Item.type.item,
new string[7] { "Hell house", "Пекельний будинок", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
51, 51, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
52, 52, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new Item(
53, 53, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
54, 54, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
55, 55, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
56, 56, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
57, 57, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
58, 58, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new Item(
59, 59, Item.type.item,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


    }

    public Item FindItem(int ID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(ID == items[i].itemID)
            return items[i];
        }

        return items[0];
    }
    


}
