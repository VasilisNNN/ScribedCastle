using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlueprintDatabase {
	public List<BlueprintItem> items = new List<BlueprintItem>();

    //Pink - #FF7FA3
    //Green - #7EFF8F
    //Blue - #7EDAFF
    //Yellow - #FFFA00
    //Pink - #FF7FA3
    //Purple - #FF67E0

    public void SetData()
    {


        items.Add(new BlueprintItem(
      0,0, 
      new string[7] { "the Start", "Початок", "", "", "", "", "" },
      new string[7] { "Start constructions from here.", "Почніть будівництво звідси.", "", "", "", "", "" },
      /*Cost*/ 0
      ));




        items.Add(new BlueprintItem(
    1, 1,
    new string[7] { "Tower", "Башта", "", "", "", "", "" },
    new string[7] { "A basic vertical defensive or lookout structure.", "Базова оборонна або спостережна споруда.", "", "", "", "", "" },
    /*Cost*/ 0
    ));
        items[items.Count - 1].Peasants_CollectMoney_Timer_Boost = 0.1f;


        items.Add(new BlueprintItem(
   2, 2,
   new string[7] { "Tower 1", "Башта 1", "", "", "", "", "" },
   new string[7] { "A variant or upgraded version of the standard tower.", "Модернізована версія стандартної вежі.", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items[items.Count - 1].Peasants_CollectMoney_Amount_Boost = 1;

        items.Add(new BlueprintItem(
   3, 3,
   new string[7] { "Mansion", "Маєток", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));


        items.Add(new BlueprintItem(
   4, 4,
   new string[7] { "Mansion 1", "Маєток 1", "", "", "", "", "" },
   new string[7] { "", "", "", "", "", "", "" },
   /*Cost*/ 0
   ));
        items[items.Count - 1].Peasants_CollectMoney_Amount_Boost = 1;

        items.Add(new BlueprintItem(
   5, 5,
   new string[7] { "Castle tower", "Замокова вежа", "", "", "", "", "" },
   new string[7] { "A fortified tower that's part of a castle complex.", "Укріплена вежа, що є частиною замкового комплексу.", "", "", "", "", "" },
   /*Cost*/ 0
   ));

        items[items.Count - 1].Guard_Damage_Boost = 1;

        items.Add(new BlueprintItem(
   6, 6, 
   new string[7] { "Tower with crops", "Вежа з городом", "", "", "", "", "" },
   new string[7] { "Small tower to watch over and protect the crops.", "Невелика вежа для спостереження та захисту врожаю.", "", "", "", "", "" },
   /*Cost*/ 0
   ));

        items.Add(new BlueprintItem(
       7, 7, 
       new string[7] { "Fort with crops", "Форт з городом", "", "", "", "", "" },
       new string[7] { "Military fort with some greenery inside. ", "Військовий форт з невеликою зеленою зоною всередині. ", "", "", "", "", "" },
       /*Cost*/ 0
       ));

        items[items.Count - 1].Knight_Damage_Boost = 1;

        items.Add(new BlueprintItem(
       8, 8, 
       new string[7] { "Church", "Церква", "", "", "", "", "" },
       new string[7] { "Young priest has an order to build a new church.", "Молодий священик отримав наказ побудувати нову церкву.", "", "", "", "", "" },
       /*Cost*/ 0
       ));



        items.Add(new BlueprintItem(
      9, 9,
      new string[7] { "Wooden fort", "Дерев'яна фортеця", "", "", "", "", "" },
      new string[7] { "Military fort made out of wood.", "Військовий форт, зроблений з дерева.", "", "", "", "", "" },
      /*Cost*/ 0
      ));



        items.Add(new BlueprintItem(
  10, 10,
  new string[7] { "Glass tower", "Скляна вежа", "", "", "", "", "" },
  new string[7] { "A tower made of glass? Crazy order, but they promise to pay well. ", "Вежа зі скла? Божевільне замовлення, але вони обіцяють добре заплатити.", "", "", "", "", "" },
  /*Cost*/ 0
  ));

        items[items.Count - 1].Cleric_Damage_Boost = 1;

        items.Add(new BlueprintItem(
  11, 11, 
  new string[7] { "Glass castle", "Скляний замок", "", "", "", "", "" },
  new string[7] { "", "", "", "", "", "", "" },
  /*Cost*/ 0
  ));

        items.Add(new BlueprintItem(
12, 12, 
new string[7] { "Tower with glass", "Вежа зі склом", "", "", "", "", "" },
new string[7] { "Tower with glass walls. The person carved in wood can know something about this mansion.", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].Peasant_HP_Boost = 1;

        items.Add(new BlueprintItem(
   13, 13,
   new string[7] { "Forest", "Ліс", "", "", "", "", "" },
   new string[7] { "The church needs to plant a forest.", "Церква повинна посадити ліс.", "", "", "", "", "" },
   /*Cost*/ 0
   ));

  

        items.Add(new BlueprintItem(
14, 14, 
new string[7] { "Hidden mansion", "Прихований маєток", "", "", "", "", "" },
new string[7] { "Try to guess whats inside. Opens the Lake location.", "Спробуй вгадати, що знаходиться всередині. Відкриває локацію Озеро.", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].Knight_HP_Boost = 1;
    

        items.Add(new BlueprintItem(
 15, 15, 
 new string[7] { "Secret society mansion", "Особняк таємного товариства", "", "", "", "", "" },
 new string[7] { "Secret society want this building, but their description is cryptic.", "Таємне товариство хоче цю будівлю, але їх опис є загадковим.", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        



        items.Add(new BlueprintItem(
 17, 17, 
 new string[7] { "Secret society church", "Церква таємного товариства", "", "", "", "", "" },
 new string[7] { "The church to make some mysterious rituals. Glass statue can know soothing about it.", "Церква для проведення таємничих ритуалів. Скляна статуя може знати про це заспокійливе.", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items[items.Count - 1].Cleric_HP_Boost = 1;
        items[items.Count - 1].Progression = 1;


        items.Add(new BlueprintItem(
 18, 18,
 new string[7] { "Thieves hideout", "Схованка злодіїв", "", "", "", "", "" },
 new string[7] { "The place where thieves can hide and wait. Shady, but they pay well. ", "Місце, де злодії можуть сховатися і чекати. Мутне замовлення, але платять добре. ", "", "", "", "", "" },
 /*Cost*/ 0
 ));
        items.Add(new BlueprintItem(
 19, 19, 
 new string[7] { "Thieves guild", "Гільдія злодіїв", "", "", "", "", "" },
 new string[7] { "Established place for thieves. Mysterious monument can know something about it.", "Місце для злодіїв. Таємничий пам'ятник може щось про це знати.", "", "", "", "", "" },
 /*Cost*/ 0
 ));

        items.Add(new BlueprintItem(
20, 20, 
new string[7] { "Assassins hideout", "Схованка вбивць", "", "", "", "", "" },
new string[7] { "The place for death givers to rest.", "Місце спочинку тих, хто приносить смерть.", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
21, 21,
new string[7] { "Assassins manor", "Маєток вбивць", "", "", "", "", "" },
new string[7] { "The manor of death.", "Маєток смерті.", "", "", "", "", "" },
/*Cost*/ 0
));
        
        items.Add(new BlueprintItem(
22, 22,
new string[7] { "Angels church", "Церква Ангелів", "", "", "", "", "" },
new string[7] { "Higher creatures need a place to come from the sky.", "Вищі істоти потребують місця, куди вони можуть спуститися з неба.", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
23, 23, 
new string[7] { "Angels miracle", "Чудо ангелів", "", "", "", "", "" },
new string[7] { "The miracle will shine upon us! ", "Чудо осяє нас! ", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
24, 24, 
new string[7] { "Devil house", "Будинок диявола", "", "", "", "", "" },
new string[7] { "The hole to the hell opened and the devil himself want you to create a building.", "Відкрилася діра в пекло, і сам диявол хоче, щоб ти побудував будівлю.", "", "", "", "", "" },
/*Cost*/ 0
));


            items.Add(new BlueprintItem(
    25, 25, 
    new string[7] { "Devil church", "Диявольська церква", "", "", "", "", "" },
    new string[7] { "The unholy place.", "Нечестиве місце.", "", "", "", "", "" },
    /*Cost*/ 0
    ));
            items.Add(new BlueprintItem(
    26, 26, 
    new string[7] { "Abomination", "Сад", "庭", "", "", "", "" },
    new string[7] { "Scariest place in the world. ", "Найстрашніше місце в світі.", "", "", "", "", "" },
    /*Cost*/ 0
    ));


    items.Add(new BlueprintItem(
    27, 27,
    new string[7] { "Garden", "Сад", "庭", "", "", "", "" },
    new string[7] { "", "", "", "", "", "", "" },
    /*Cost*/ 0
    ));

        items.Add(new BlueprintItem(
 28, 28, 
 new string[7] { "Hidden mansion", "Прихований особняк", "", "", "", "", "" },
 new string[7] { "Try to guess whats inside", "Спробуйте вгадати, що знаходиться всередині", "", "", "", "", "" },
 /*Cost*/ 0
 ));


        items.Add(new BlueprintItem(
29, 29, 
new string[7] { "Inn hidden", "Прихований готель", "", "", "", "", "" },
new string[7] { "Travelers can rest here, only block descriptions are not clear.", "Мандрівники можуть тут відпочити, тільки опис замовлення нечіткий.", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
30, 30,
new string[7] { "Heraldic castle", "Геральдичний замок", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
31, 31,
new string[7] { "Heraldic mansion", "Геральдичний маєток", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
32, 32,
new string[7] { "Magic castle", "Магічний замок", "", "", "", "", "" },
new string[7] { "Opens the Mountain location.", "Відкриває локацію Гора.", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
33, 33,
new string[7] { "Magic districts", "Магічний квартал", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
34, 34, 
new string[7] { "Magic farm", "Мфгічна ферма", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
35, 35, 
new string[7] { "Magic manor", "Магічний маєток", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
36, 36, 
new string[7] { "Magic rich manor", "Магічний багатий маєток", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
37, 37, 
new string[7] { "Magic tiny farm", "Маленька магічна ферма", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));



        items.Add(new BlueprintItem(
38, 38, 
new string[7] { "Magic village", "Магічне село", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));




        items.Add(new BlueprintItem(
39, 39, 
new string[7] { "Magic village 1", "Магічне друге село", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].Progression = 2;



        items.Add(new BlueprintItem(
40, 40,
new string[7] { "Mountain church", "Гірська церква", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
41, 41, 
new string[7] { "Mountain advanced church", "Гірська покращена церква", "", "", "", "", "" },
new string[7] { "Opens the Hell location.", "Відкриває локацію Пекло.", "", "", "", "", "" },
/*Cost*/ 0
));
       

        items.Add(new BlueprintItem(
42, 42, 
new string[7] { "Mountain village", "Гірське село", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items[items.Count - 1].Progression = 3;


        items.Add(new BlueprintItem(
43, 43, 
new string[7] { "st. Peters church", "Церква святого Петра", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
44, 44, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
45, 45, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
46, 46, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
47, 47, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
48, 48,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));
        items.Add(new BlueprintItem(
49, 49,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
50, 50, 
new string[7] { "Hell house", "Пекельний будинок", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
51, 51,
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
52, 52, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


        items.Add(new BlueprintItem(
53, 53, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
54, 54, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
55, 55, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
56, 56, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
57, 57, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
58, 58, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));

        items.Add(new BlueprintItem(
59, 59, 
new string[7] { "", "", "", "", "", "", "" },
new string[7] { "", "", "", "", "", "", "" },
/*Cost*/ 0
));


    }

    public BlueprintItem FindItem(int ID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(ID == items[i].itemID)
            return items[i];
        }

        return items[0];
    }
    


}
