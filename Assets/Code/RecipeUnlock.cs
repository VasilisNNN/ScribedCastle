
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RecipeUnlock : MonoBehaviour
{
    public Tilemap Floor;
    private Player pl;
    public int DishID;
    private Constructor Constr;
    private ItemDatabase itemDatabase;

    public AudioClip Clip;
    public GameObject UnlockingObject;

   
    void Start()
    {
        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;
        itemDatabase = InitializeObjects.Itemdatabase;
        Floor = InitializeObjects.FloorTilemap;


        if (Floor.GetTile(Floor.WorldToCell(transform.position)) != null)
        {
            if (DishID > -1)
            {
                Constr.Dishes.Add(itemDatabase.FindItem(DishID));
            }

            Constr.PlaySound(Clip, 1);

            Destroy(gameObject);

        }
    }


        void Update()
    {
  
        if (Floor.GetTile(Floor.WorldToCell(transform.position)) != null)
        {
            
           
            if (DishID > -1)
            {
                Constr.AddLogPart("New recipe!", "Новий рецепт","",null);
                Constr.Dishes.Add(itemDatabase.FindItem(DishID));
                Constr.PlaySound(Clip, 1);
            }

            if (UnlockingObject != null)
            {
                Constr.AddLogPart("New decoration!", "Новое украшение!", "", null);
                UnlockingObject.GetComponent<ProgressionDraw>().Active = true;
            }


            Destroy(gameObject);
        }
    }

}
