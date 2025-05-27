using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsDatabase : MonoBehaviour
{

    public List<Dish> DishList = new List<Dish>();

    void Awake()
    {
        DishList.Add(new Dish(0, 0, 0, new string[1] { "Meat" }, new string[1] { "Meat" }, 20));
        DishList[DishList.Count - 1].Meat = 1;
        DishList.Add(new Dish(1, 0, 0, new string[1] { "Vegs" }, new string[1] { "Vegs" }, 15));
        DishList[DishList.Count - 1].Vegs = 1;
        DishList.Add(new Dish(2, 0, 0, new string[1] { "Alcohol" }, new string[1] { "Alcohol" }, 15));
        DishList[DishList.Count - 1].Alcohol = 1;
    }
}
