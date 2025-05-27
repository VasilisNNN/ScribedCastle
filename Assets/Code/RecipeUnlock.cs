using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RecipeUnlock : MonoBehaviour
{
    public Tilemap Floor;
    private Player pl;
    public int DishID;
    private Constructor Const;

    public AudioClip Clip;
    public GameObject UnlockingObject;

    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();

        Floor = GameObject.Find("Floor").GetComponent<Tilemap>();


        if (Floor.GetTile(Floor.WorldToCell(transform.position)) != null)
        {
            if (DishID > -1)
            {
                Const.Dishes.Add(pl.inv.GetItemInDatabase(DishID));
            }

            Const.PlaySound(Clip, 1);

            Destroy(gameObject);

        }
    }


        void Update()
    {
  
        if (Floor.GetTile(Floor.WorldToCell(transform.position)) != null)
        {
            if (GameObject.Find("MenuRecipes") == null)
            {
               // GameObject MenuActive;
              //  MenuActive = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/MenuQuest"), GameObject.Find("Food Menu").transform);
               // MenuActive.transform.position = GameObject.Find("Food Menu").transform.position;
              //  MenuActive.name = "MenuRecipes" ;
            }

            //UNLOCKS NEXT TUTORIAL STEP

           
            if (DishID > -1)
            {
                Const.AddLogPart("New recipe!", "Новий рецепт","",null);
                Const.Dishes.Add(pl.inv.GetItemInDatabase(DishID));
                Const.PlaySound(Clip, 1);
            }

            if (UnlockingObject != null)
            {
                Const.AddLogPart("New decoration!", "Новое украшение!", "", null);
                UnlockingObject.GetComponent<ProgressionDraw>().Active = true;
            }


            Destroy(gameObject);
        }
    }

}
