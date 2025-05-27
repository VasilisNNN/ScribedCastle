using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    public bool Hungry { get; set; }
    private GameObject dish;
    public Item Dish;
    private Constructor Const;
    
    void Start()
    {
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();
        Hungry = true;
        //Dish = Const.Dishes[Random.Range(0, Const.Dishes.Count)];

        //dish = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Dishes/"+ Dish.Name[0]),transform);
       // dish.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        
    }

    public void RestartDish()
    {
        Hungry = true;

        if (Const==null)
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();

      
        if (dish != null)
        {
            Dish = Const.Dishes[Random.Range(0, Const.Dishes.Count)];

            dish.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("Sprites/Items/" + Dish.itemNames[0]).GetComponent<SpriteRenderer>().sprite;

            dish.SetActive(true);
        }
        else
        {
            Dish = Const.Dishes[Random.Range(0, Const.Dishes.Count)];


            dish = Instantiate<GameObject>(Resources.Load<GameObject>("Sprites/Items/" + Dish.itemNames[0]), transform);
            dish.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            dish.SetActive(true);

        }
      

    }

    public void ClientUpdate()
    {
       
            if (!Hungry && dish!=null)
            {
            print("Deactivate Dish");
            dish.SetActive(false);

            }

        if (Hungry)
        {
            print("Activate Dish");
            dish.SetActive(true);
        }

    }
}
