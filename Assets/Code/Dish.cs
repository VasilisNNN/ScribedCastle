using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Dish
{
    public int ID;
    public int Cost;
    public string[] Name;
    public string[] Descriptions;
    public int NeedOven;
    public bool Drink;
    public int Meat;
    public int Vegs;
    public int Alcohol;

    public int NeedFridges;
    public int NeedPlantFarms;
    public int NeedFermenting;

    public int Grass;
    public int Veggies;
    public int Squids;

    public bool bread;

    public Dish(int id,  int meat, int vegs, string[] name, string[] desc,int cost)
    {
        Cost = cost;
        ID = id;
        Name = name;

        Meat = meat;
        Vegs = vegs;

        Descriptions = desc;

    }
}
