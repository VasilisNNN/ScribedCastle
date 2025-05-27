using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Sit
{
    public int Num = 0;
    public string Name = "";
}

public class PubObject : MonoBehaviour
{
    public bool Red;
    public bool Green;
    public bool Gold;

    public int Cost;
    public int CornCost;

    public bool Table;
    public bool Shield;
    public bool TransperentWall;

    public int ComfortPlus;
    public int TimePlus;
    public int GodDelayPlus;

    public int DamageFromWall =1;

    public float TableTimer { get;  set; }
    public float FarmingTimer { get;  set; }

    private float Waiting, DinerTimer, PauseBetweenClients,StartCliantTimer;

    private float TableEmptyTimer;

    public List<Sit> Sits = new List<Sit>();
    public List<GameObject> Clients { get; private set; }

    private Constructor Const;

    public bool Hungry { get; set; }

    public int floors;
    public int kitchenfloors;
    public int tables;
    public int beds;
    public int ground;
    public int wall;

    public int oven;
    
    public int toilet;
    public int people;

    public int Trash;
    public int Poop;

    public List<GameObject> CharactersOnThisStructure = new List<GameObject>();

    public bool Shielded { get; set; }

    public GameObject ShieldObject { get; set; }

    public TileBase _TileBase { get; set; }
    public Tilemap MAPS { get; set; }

    private int durability = 20;
    public bool decoration;
    public bool TopObject;

    public List<string> TrueName = new List<string>();
    public bool Crowded { get; set; }

    private SpriteRenderer ObjSPRT;
    private Vector3 CameraPos;
    private Camera MainCam;

    public int TopObjectsCount { get; set; }

    private GameObject[] PersFG, PersBG;
    private GameObject WalkingClient, BrakedownOB;

    public bool Draw = true;

    public bool CleanerIsGoing { get; set; }
    public bool RepairerIsGoing { get; set; }

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

        _GetItem = GetComponent<GetItem>();
        if (DishObject == null && Table)
        {
            DishObject = new GameObject();
            DishObject.name = "DishObject";
            DishObject.AddComponent<SpriteRenderer>();
            DishObject_SPRT = DishObject.GetComponent<SpriteRenderer>();
         
            DishObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }

        PersFG = new GameObject[2]
        { Resources.Load<GameObject>("Prefabs/TablePers/Pers_FG0"),
          Resources.Load<GameObject>("Prefabs/TablePers/Pers_FG1")
        };

        PersBG = new GameObject[2]
         { Resources.Load<GameObject>("Prefabs/TablePers/Pers_BG0"),
              Resources.Load<GameObject>("Prefabs/TablePers/Pers_BG1")
         };

        WalkingClient = Resources.Load<GameObject>("Prefabs/TablePers/Client");
     

      
        if (wall <= 0) TopObjectsCount = 99;

        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        ObjSPRT = GetComponent<SpriteRenderer>();
        if (wall<=0)
        durability = 20;
        //durability = 1;

        Clients = new List<GameObject>();

        Const = GameObject.Find("Constructor").GetComponent<Constructor>();
        Waiting = 20;
        PauseBetweenClients = 10;
        DinerTimer = 10;
        TableTimer =  Random.Range(2,6);
        StartCliantTimer = Time.fixedTime + Random.Range(0, 2);



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



        if (_GetItem == null && GetComponent<Trigger>() == null)
        {
            if (CharactersOnThisStructure.Count > 0)
            {
                if (GetComponent<Animator>() != null)
                    GetComponent<Animator>().SetBool("Start", true);
            }
            else
            {

                if (GetComponent<Animator>() != null)
                    GetComponent<Animator>().SetBool("Start", false);
            }
        }

   
        //Clients_Deprecated();
        if (Const == null) return;
        if (Const.Game_SPEED <= 0) return;



        Waiting = Const.TimerStay;

      //  ShieldNeighbours();

        PauseBetweenClients = 10 - (int)Const.Comfort / 2;
        if (PauseBetweenClients < 1) PauseBetweenClients = 1;


        TableDurability();

        if (_transform.parent != null) return;
        if (!Table) return;

        // CharactersAtTheTable_Deprecated();

        if (TableTimer < 0)
        {
            for (int i = 0; i < Sits.Count; i++)
            {
                Sits[i].Num = 1;
            }
            CrowdedCheck();
                        
            TableTimer = (PauseBetweenClients + Waiting) / Const.Game_SPEED;

        }


        //if(StartCliantTimer<Time.fixedTime)
        // GenerateClients_Deprecated();


    }

    void TableDurability()
    {
        if (Const.DEMO)
        {
    
            if (tag != "Pers")
            {
                if (ObjSPRT != null)
                    ObjSPRT.color = new Color(1, 1, 1, ObjSPRT.color.a);


            }
            return;
        }

        if (durability > 0) return;

        if (tag == "Pers") return;
        

       
        if (ObjSPRT != null)
            ObjSPRT.color = new Color(0.5f, 0.5f, 0.5f, ObjSPRT.color.a);

        if (TopObjectsCount <= 0)
        {
            for (int i = 0; i < _transform.childCount; i++)
            {

                if (_transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    _transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, _transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);


            }
        }
        

        
            
        
    }

   void Clients_Deprecated()
    {
        if (Clients != null)
        {
            if (Clients.Count > 0)
            {
                int h = 0;
                for (int i = 0; i < Clients.Count; i++)
                {

                    if (Clients[i].GetComponent<Client>().Hungry)
                    {
                        h++;

                    }


                }

                if (h > 0) Hungry = true;
            }
            else Hungry = false;
        }
        else Hungry = false;


    }
    void CharactersAtTheTable_Deprecated()
    {
        if (TableTimer - PauseBetweenClients / Const.Game_SPEED >= 0 || TableTimer <= 0)
            return;

        for (int i = 0; i < Sits.Count; i++)
        {

            if (Sits[i].Num == 1 && Clients[i].activeInHierarchy)
            {

                int muneyplus = 0;
                int crowdedint = 0;
                if (Crowded) crowdedint = 1;


                if (Clients[i] != null)
                {
                    if (!Clients[i].GetComponent<Client>().Hungry)
                    {
                        muneyplus += (Clients[i].GetComponent<Client>().Dish.Cost + 5) + (Const.Comfort - crowdedint * 5);

                    }

                    Clients[i].GetComponent<Client>().ClientUpdate();
                }


                if (muneyplus < 0) muneyplus = 0;
                Const.Money += muneyplus;


                Const.ClientsList = new List<GameObject>();
                //Clients = new List<GameObject>();


                if (muneyplus != 0)
                    Const.AddLogPart("Check from a table. Money: +" + muneyplus, "Чек со стола. Деньги: +" + muneyplus, "テーブルの小切手 お金：+." + muneyplus, gameObject);

                Const.AddMoney(_transform.position, muneyplus);
            }

            Sits[i].Num = 0;


        }
        
    }


        void GenerateClients_Deprecated()
        {
            

            for (int i = 0; i < Sits.Count; i++)
            {
           

                int crowdedint = 0;

                if (Crowded) crowdedint = 1;
                
                if (_transform.Find("Sit" + i) != null && i< Clients.Count)
                {
                       
                    if (Sits[i].Num == 1 && !Clients[i].activeInHierarchy)
                    {
                      // print("i: "+i);
                            Sits[i].Name = Clients[i].name;

                                if (!Clients[i].GetComponent<Client>().Hungry)
                            Const.MinIncome += Clients[i].GetComponent<Client>().Dish.Cost + 5 + (Const.Comfort - crowdedint * 5);

                        Const.MaxIncome += Clients[i].GetComponent<Client>().Dish.Cost + 5 + (Const.Comfort - crowdedint * 5);

                        // print("active num: " + i);

                        Clients[i].SetActive(true);
                        Clients[i].GetComponent<Client>().RestartDish();

                        Const.ClientsList.Add(Clients[i]);

                        if (i <= 1)
                        {
                    Clients[i].GetComponent<SpriteRenderer>().sortingOrder = _transform.Find("Base").GetComponent<SpriteRenderer>().sortingOrder - 1;
                    Clients[i].transform.Find("Head").GetComponent<SpriteRenderer>().sortingOrder = _transform.Find("Base").GetComponent<SpriteRenderer>().sortingOrder + 1;
                        }
                        else
                        {
                    Clients[i].GetComponent<SpriteRenderer>().sortingOrder = _transform.Find("Base").GetComponent<SpriteRenderer>().sortingOrder + 1;
                    Clients[i].transform.Find("Head").GetComponent<SpriteRenderer>().sortingOrder = _transform.Find("Base").GetComponent<SpriteRenderer>().sortingOrder + 2;
                        }

                        if (i == 1) Clients[i].transform.localScale = new Vector3(-1, 1, 1);
                        if (i == 3) Clients[i].transform.localScale = new Vector3(-1, 1, 1);

                    }
                        
                }


            if (i < Clients.Count)
            {


                if (Clients[i] != null)
                {


                    if (Sits[i].Num == 0 && Clients[i].activeInHierarchy)
                    {
                        GameObject Client = Clients[i];
                     //  print("i dis:" + i);

                        if (Const.ClientsList.Count > 0 && i < Const.ClientsList.Count - 1)
                            Const.ClientsList.RemoveAt(i);

                       // if (Clients.Count > 0 && i < Clients.Count - 1)
                           // Clients.RemoveAt(i);

                        Sits[i].Name = "";


                        if (Client.GetComponent<Client>().Hungry)
                            Const.MinIncome -= Client.GetComponent<Client>().Dish.Cost + 5 + (Const.Comfort - crowdedint * 5);

                        Const.MaxIncome -= Client.GetComponent<Client>().Dish.Cost + 5 + (Const.Comfort - crowdedint * 5);

                        Clients[i].SetActive(false);
                        break;
                    }
                }
            }
                 
                    
            }
           

        }


    public void UnShield()
    {
        if (Shield)
        {
            if (_transform.parent == null )
            {
                for (int x = -3; x < 3; x++)
                {
                    for (int y = -3; y < 3; y++)
                    {
                        for (int i = 0; i < Const.OBOnBoard.Count; i++)
                        {
                            if ((_transform.position.x + x * 0.5f) == Const.OBOnBoard[i].Place.x &&
                                (_transform.position.x + y * 0.25f) == Const.OBOnBoard[i].Place.y)
                            {
                                GameObject OB = GameObject.Find((_transform.position.x + x * 0.5f) + "_" + (_transform.position.y + y * 0.25f));

                                if (OB != null)
                                {

                                    if (OB.GetComponent<PubObject>().Shielded)
                                    {
                                        
                                        OB.GetComponent<PubObject>().Shielded = false;
                                        OB.GetComponent<PubObject>().ShieldObject = gameObject;
                                    }


                                }
                            }


                        }

                    }
                }
            }
        }

    }


    void ShieldNeighbours()
    {
        if (!Shield)
        {
            if (ShieldObject == null)
                Shielded = false;
        }

        if (_transform.parent != null) return;
        if (!Const.IM.enter_b && !Const.IM.exit_b && !Const.IM.LeftMouseButtonDown) return;
        if (Mathf.Abs(Const.transform.position.x - transform.position.x) >= 3 || Mathf.Abs(Const.transform.position.y - transform.position.y) >= 3) return;


     
        for (int i = 0; i < Const.OBOnBoard.Count; i++)
        {

            if (Const.OBOnBoard[i].Place.x > _transform.position.x-1 && Const.OBOnBoard[i].Place.x < _transform.position.x + 1 &&
                Const.OBOnBoard[i].Place.y > _transform.position.y - 1 && Const.OBOnBoard[i].Place.y < _transform.position.y + 1 )
            {
                GameObject OB = Const.OBOnBoard[i].Object;

                if (OB != null)
                {

                    if (!Const.OBOnBoard[i].PO.Shielded)
                    {

                        print("Shielded" + OB.name);
                        Const.OBOnBoard[i].PO.Shielded = true;
                        Const.OBOnBoard[i].PO.ShieldObject = gameObject;
                    }


                }
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

 
    

    public void AddClient(GameObject Client)
    {
        if (Clients.Count < Sits.Count)
        {
            Clients.Add(Client);
            Const.ClientsList.Add(Client);
        }

    }


 

  

    /*public void SetDinerTimer()
    {
        TableTimer += (DinerTimer+ Const.TimerStay);
    }*/
}
