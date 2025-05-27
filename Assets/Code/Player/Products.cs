
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Products : MonoBehaviour
{
    private ProductsDatabase PD;
    public Dish Producs;
    public int[] DishesNumbs;

    // Start is called before the first frame update
    void Start()
    {
        PD = GameObject.Find("ProductsDatabase").GetComponent<ProductsDatabase>();

        Producs = PD.DishList[DishesNumbs[0]];
      //  Dish = DD.DishList[DishesNumbs[Random.Range(0, DishesNumbs.Length)]];
    }

  
}
