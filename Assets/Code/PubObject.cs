using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Linq;

public class Sit
{
    public int Num = 0;
    public string Name = "";
}

public class PubObject : MonoBehaviour
{
 
    public bool Table;

    public bool TransperentWall;

    public int ComfortPlus;
    public int TimePlus;

    public int DamageFromWall =1;

    public float TableTimer { get;  set; }
    public float FarmingTimer { get;  set; }

    private float Waiting, PauseBetweenClients;



    private Constructor Const;

    public bool Hungry { get; set; }

    public int floors;

    public int tables;

    public int ground;
    public int wall;


    public int people;

    public int Trash;
    public int Poop;

    public List<GameObject> CharactersOnThisStructure = new List<GameObject>();

    public TileBase _TileBase { get; set; }
    public Tilemap MAPS { get; set; }

    private int durability = 20;



    public List<string> TrueName = new List<string>();
    public bool Crowded { get; set; }

    private SpriteRenderer ObjSPRT;

    public int FloorsCount { get; set; }


    public bool Draw = true;

    public int[] ItemNeeded { get; set; }
    public int[] ItemNeededCount { get; set; }

    public List<Item> DishesOnTable = new List<Item>();
    private GameObject DishObject;

    [HideInInspector]
    public bool isInsideBGLeft, isInsideBGRight, isInsideFGLeft, isInsideFGRight;
    [HideInInspector]
    public float AddLeftBG, AddRightBG, AddLeftFG, AddRightFG;

    private GetItem _GetItem;
    private SpriteRenderer DishObject_SPRT;
    private Transform _transform;
    void Start()
    {
        _transform = transform;
        Const = InitializeObjects.Constr;

        _GetItem = GetComponent<GetItem>();
        if (DishObject == null && Table)
        {
            DishObject = new GameObject();
            DishObject.name = "DishObject";
            DishObject.AddComponent<SpriteRenderer>();
            DishObject_SPRT = DishObject.GetComponent<SpriteRenderer>();
         
            DishObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }

    

      
        if (wall <= 0) FloorsCount = 99;

       
        ObjSPRT = GetComponent<SpriteRenderer>();
        if (wall<=0)
        durability = 20;
        //durability = 1;

    

        Waiting = 20;
        PauseBetweenClients = 10;
        TableTimer =  Random.Range(2,6);
       
        Const.AllPeople += people;
        Const.AllTables += tables;
        Const.Comfort += ComfortPlus;
        
    }

    public void PubObjectTimers()
    {

        if (Const != null)
        {
            TableTimer -= 0.01f * Const.Game_SPEED;
            FarmingTimer -= 0.01f * Const.Game_SPEED;
        }
       // Waiting -= 0.01f * Const.Game_SPEED;
    }

    public void PubObjectUpdate()
    {
        if (Table)
        {
            if (DishObject == null)
            {
                DishObject = new GameObject();
                DishObject.name = "DishObject";
                DishObject.AddComponent<SpriteRenderer>();
                DishObject_SPRT = DishObject.GetComponent<SpriteRenderer>();
                DishObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);

            }



            if (DishObject != null)
            {
                DishObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);

                if (DishObject_SPRT != null)
                {
                    if (DishesOnTable.Count > 0)
                    {
                        DishObject_SPRT.sprite =
                        Resources.Load<Sprite>("Sprites/Items/" + DishesOnTable[0].itemNames[0]);


                    }
                    else DishObject_SPRT.sprite = null;
                }
            }
        }


        if (Const == null) return;
        if (Const.Game_SPEED <= 0) return;



        Waiting = Const.TimerStay;

     
        PauseBetweenClients = 10 - (int)Const.Comfort / 2;
        if (PauseBetweenClients < 1) PauseBetweenClients = 1;


        TableDurability();

        if (_transform.parent != null) return;
        if (!Table) return;

        if (TableTimer < 0)
        {
           
            CrowdedCheck();
                        
            TableTimer = (PauseBetweenClients + Waiting) / Const.Game_SPEED;

        }


    }

    void TableDurability()
    {
       

        if (durability > 0) return;

        if (tag == "Pers") return;
        

       
        if (ObjSPRT != null)
            ObjSPRT.color = new Color(0.5f, 0.5f, 0.5f, ObjSPRT.color.a);

        if (FloorsCount <= 0)
        {
            for (int i = 0; i < _transform.childCount; i++)
            {

                if (_transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    _transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, _transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);


            }
        }
        

        
            
        
    }

 
  

    private void CrowdedCheck()
    {
        if (_transform.parent != null) return;
        
          
        int tables = 0;
  
        for (int i = 0; i < Const.OBOnBoard.Count; i++)
        {
            if (Const.OBOnBoard[i] != null && Const.OBOnBoard[i].Place!=null)
            {
                if (Const.OBOnBoard[i].Place.x > _transform.position.x - 1 && Const.OBOnBoard[i].Place.x < _transform.position.x + 1
                    && Const.OBOnBoard[i].Place.y > _transform.position.y - 1 && Const.OBOnBoard[i].Place.y < _transform.position.y + 1 && Const.OBOnBoard[i].PO != null)
                {
                    if (Const.OBOnBoard[i].PO.tables > 0)
                        tables++;
                }
            }

        }


        if (tables >= 5)
        {

            if (!Crowded)
            {
                Const.Crowded++;
                Const.AddLogPart("Crowded +1", "Толкучка +1", "混雑 +1", gameObject);
                Crowded = true;
            }
        }
        else
        {

            if (Crowded)
            {
                Const.Crowded--;
                Const.AddLogPart("Crowded -1", "Толкучка -1", "混雑 -1", gameObject);
                Crowded = false;
            }
        }


    }

 
    
  

    /*public void SetDinerTimer()
    {
        TableTimer += (DinerTimer+ Const.TimerStay);
    }*/
}
