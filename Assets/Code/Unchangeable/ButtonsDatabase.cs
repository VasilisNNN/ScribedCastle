using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsDatabase : MonoBehaviour
{
    public List<ButtonItem> Button = new List<ButtonItem>();
    public List<ButtonItem> ButtonUA = new List<ButtonItem>();
    public List<ButtonItem> ButtonJP = new List<ButtonItem>();
    void Awake()
    {
        string namecolor = "<color=#6dffc6>";
      

        string redcolor = "<color=#ff0000>";
        string colorend = "</color>";

        
        Button.Add(new ButtonItem(0, new string[2] { "Tips Controlls", "" }, new string[2] {
            "Welcome to the Frogvival! Let's cover some basics in several next quests. After that, I will disappear, and you will continue to build your pub by yourself!" +"\n"+"\n"+
            "A Button  - Pick and put" +"\n"+
            "B Button  - Destroy" +"\n"+
            "ZR  - Flip Object" +"\n" +
            "Right Stick Push  - Back to the field center" +"\n",

            "" }));

        Button.Add(new ButtonItem(1, new string[2] { "Build the wall", "" }, new string[2] {
            "You built the wall!" + "\n" + "\n" + "Walls protects you from the outside horrors!",
            ""}));

        Button.Add(new ButtonItem(2, new string[2] { "Build the table", "" }, new string[2] {
            "You built the table!" + "\n" + "\n" + "People can eat here if there is a dish on the table",
            ""}));

        Button.Add(new ButtonItem(3, new string[2] { "Hire a waiter", "" }, new string[2] {
            "You hired a waiter!" + "\n" + "\n" + "Waiters can bring food from oven to the table!",
            ""}));


        Button.Add(new ButtonItem(4, new string[2] { "Hire a cook", "" }, new string[2] {
            "You hired a cook!" + "\n" + "\n" + "Cooks can cook food if you have enough resources",
            ""}));


        Button.Add(new ButtonItem(5, new string[2] { "Build a Furnace", "" }, new string[2] {
            "You built a Furnace" + "\n" + "\n" + "You сcan process raw materials here",
            ""}));


        Button.Add(new ButtonItem(6, new string[2] { "Build an Oven", "" }, new string[2] {
            "You built an  Oven" + "\n" + "\n" + "You and Cooks can cook here",
            ""}));


        

    }


    /*private void Update()
    {
        Button[35].Descriptions = new string[2] {
            "Your meat. Refills when gets to 0. Cost: " + GameObject.Find("Constructor").GetComponent<Constructor>().AllMeatCost,
            "Ваше мясо. Новая партия закупается когда текущая иссякнет. Стоимость: " + GameObject.Find("Constructor").GetComponent<Constructor>().AllMeatCost};
        Button[36].Descriptions = new string[2] {
            "Your vegetables. Refills when gets to 0. Cost: " + GameObject.Find("Constructor").GetComponent<Constructor>().AllVegCost,
            "Ваши овощи. Новая партия закупается когда текущая иссякнет. Стоимость: " + GameObject.Find("Constructor").GetComponent<Constructor>().AllVegCost};



        if (GameObject.Find("Constructor").GetComponent<InputMode>().joystick) {
            Button[42].Descriptions = new string[2] {
            "Welcome to the Cthulhu pub! Let's cover some basics in several next quests. After that, I will disappear, and you will continue to build your pub by yourself!" +"\n"+"\n"+
            "A Button  - Pick and put" +"\n"+
            "B Button  - Destroy" +"\n"+
            "ZR  - Flip Object" +"\n" +
            "Right Stick Push  - Back to the field center" +"\n",

            "Добро пожаловать в Ктулху бар! Давайте пройдемся по азам в нескольких следующих квестах. После чего я уйду и вы продолжите развиваться самостоятельно!" +"\n"+"\n"+
            "LКМ  - Выбирать и строить" +"\n"+
            "RКМ  - Разрушать" +"\n"+
            "Left Shift  - Отражать объекты" };
        } else
        {
            Button[42].Descriptions = new string[2] {
            "Welcome to the Cthulhu pub! Let's cover some basics in several next quests. After that, I will disappear, and you will continue to build your pub by yourself!" +"\n"+"\n"+
            "LMB  - Pick and put" +"\n"+
            "RMB  - Destroy" +"\n"+
            "Left Shift  - Flip Object" +"\n" +
            "Left ALT  - Back to the field center" +"\n",

            "Добро пожаловать в Ктулху бар! Давайте пройдемся по азам в нескольких следующих квестах. После чего я уйду и вы продолжите развиваться самостоятельно!" +"\n"+"\n"+
            "LКМ  - Выбирать и строить" +"\n"+
            "RКМ  - Разрушать" +"\n"+
            "Left Shift  - Отражать объекты" };

        }


        Button[46].Descriptions = new string[2] {
            "Your drinks. Refills when gets to 0. Cost:" + GameObject.Find("Constructor").GetComponent<Constructor>().AllBeerCost,
            "Ваши напитки. Новая партия закупается когда текущая иссякнет. Стоимость:" + GameObject.Find("Constructor").GetComponent<Constructor>().AllBeerCost};
       
    }*/


  

}
