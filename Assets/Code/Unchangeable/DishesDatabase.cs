using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesDatabase : MonoBehaviour
{

    public List<Dish> DishList = new List<Dish>();

    void Awake()
    {
        DishList.Add(new Dish(0,0,1,new string[1] { "Pizza" }, new string[1] { "Pizza" }, 15));
        DishList[DishList.Count - 1].NeedOven = 1;

        
        DishList.Add(new Dish(1,1,1, new string[1] { "Squid Soup" }, new string[2] {
            "Squid Soup.",
            "Суп из кальмара. Вы можете найти его на карте. Просто постройте пол под блюдом и вы откроете его рецепт."}, 
            20));
        DishList[DishList.Count - 1].NeedFridges = 1;
        DishList[DishList.Count - 1].NeedOven = 1;

        DishList.Add(new Dish(2,0,0, new string[1] { "Beer" }, new string[2] {
            "Beer",
            "Пиво" }, 10));
        DishList[DishList.Count - 1].Drink = true;

        DishList.Add(new Dish(3, 1, 1, new string[1] { "Octo Soup" }, new string[2] {
            "Octo Soup.",
            "Суп из осьминога. Вы можете найти его на карте. Просто постройте пол под блюдом и вы откроете его рецепт."}, 35));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;

        DishList.Add(new Dish(4, 1, 1, new string[1] { "2 Fishes" }, new string[2] {
            "2 Fishes.",
            "2 Рыбы. Вы можете найти его на карте. Просто постройте пол под блюдом и вы откроете его рецепт."}, 45));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;

        DishList.Add(new Dish(5, 1, 1, new string[1] { "Human Soup" }, new string[2] {
            "Human soup.",
            "Суп из людей. Вы можете найти его на карте. Просто постройте пол под блюдом и вы откроете его рецепт."}, 45));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;

        DishList.Add(new Dish(6, 2, 1, new string[1] { "Fish soup" }, new string[2] {
            "Fish soup.",
            "Суп с печенью. Вы можете найти его на карте. Просто постройте пол под блюдом и вы откроете его рецепт."}, 45));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;
    
        DishList.Add(new Dish(7, 0, 0, new string[1] { "Bread" }, new string[2] {
            "Bread",
            "Хлеб" }, 5));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].bread = true;

        DishList.Add(new Dish(8, 2, 1, new string[1] { "Fish head soup" }, new string[2] {
            "Fish head soup.",
            "Суп из рыбьей головы. Вы можете найти его на карте. Просто постройте пол под блюдом и вы откроете его рецепт."}, 45));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;


        DishList.Add(new Dish(9, 0, 0, new string[1] { "Dark Bread" }, new string[2] {
            "Dark Bread",
            "" }, 7));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].bread = true;

        DishList.Add(new Dish(10, 4, 1, new string[1] { "Rotten Dish" }, new string[2] {
            "Rotten Dish",
            "" }, 50));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFermenting = 1;

        DishList.Add(new Dish(11, 2, 2, new string[1] { "Omlet" }, new string[2] {
            "Omlet",
            "" }, 30));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;


        DishList.Add(new Dish(12, 0, 0, new string[1] { "Bread and butter" }, new string[2] {
            "Bread and butter",
            "" }, 30));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;
        DishList[DishList.Count - 1].bread = true;


        DishList.Add(new Dish(13, 5, 0, new string[1] { "Human Liver Dish" }, new string[2] {
            "Human Liver Dish",
            "" }, 35));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;


        DishList.Add(new Dish(14, 0, 4, new string[1] { "Pumpkin Dish" }, new string[2] {
            "Pumpkin Dish",
            "" }, 35));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;


        DishList.Add(new Dish(15, 3, 3, new string[1] { "Blue Squid Dish" }, new string[2] {
            "Blue Squid Dish",
            "" }, 35));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;
      //  DishList[DishList.Count - 1].Alcohol = 2;


        DishList.Add(new Dish(16, 0, 5, new string[1] { "Green Potato Dish" }, new string[2] {
            "Green Potato Dish",
            "" }, 35));
        DishList[DishList.Count - 1].NeedOven = 1;
        DishList[DishList.Count - 1].NeedFridges = 1;
        DishList[DishList.Count - 1].NeedFermenting = 1;
      //  DishList[DishList.Count - 1].Alcohol = 1;


    }


}
