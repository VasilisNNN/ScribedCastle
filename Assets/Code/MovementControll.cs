using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementControll : MonoBehaviour
{

    public bool SelfDestroy;
    public bool SelfDestroyOnlyDamagingPlayer;
    public bool GoingBack = true;


    public int Poison { get; set; }
    public bool Attacked { get; set; }

    public bool ItemPicker;
    public bool BringsPickedItemToThePlayer;
    public bool MoveBetweenA_B;
    public int[] PickSpecificItem;


    public bool Client;
    public bool Enemy;
    public bool Waitress;
    public bool Cook;

    public bool Soldier;


    public bool DropItemsOnTarget;
    public bool DestroyTarget;

    public bool LeavesTrash;

    public Vector2 Pos { get; set; }
    private Vector2 MoveToPos, NextStep;
    public GameObject MoveToObject { get; private set; }

    private float PoisonDelay;

    private float MoveTimer, OnTheTableWaiting, ToiletTimer, ActionTimer, PoisonTimer, PayTimer, SearchForTargetsTimer;
    private bool InTheToilet;

    private Constructor Const;
    private Player pl;
    private GameObject moneyui;

    private Tilemap RT;
    private List<TileBase> Brush = new List<TileBase>();

    public float ActionDelay = 10;
    public Enemies EnemiesBase;

    [HideInInspector]
    public int MinDamage = 0;

    public float MoveDelayMax = 0.2f;


    private GameObject BloodEffect_0, BloodEffect_1, MoneyDifference;

    private List<GameObject> DishesObjects = new List<GameObject>();
    private float SoldierDelay;

    private float FollowTimer;
    public float FollowTimerDelay = 10;
    private float InvisTimer;

    private Material StartMaterial, WhiteMaterial;
    private List<AudioClip> DamageClips = new List<AudioClip>();
    public float FollowBorder = 2;


    private List<GameObject> Beds = new List<GameObject>();

    public GameObject[] A_Point;
    public GameObject[] TargetGameObjects;



    private Constructor constr;
    private Item CarringDish;

    private GameObject DishObject;

    public bool IgnoreOccupation;
    private StatsControll Stats;

    public GameObject ObjectOfOccupation;
    private Transform _transform;
    private CharacterMove CM;


    public bool TriggerOFF_OffScreen;
    public int[] ItemsTheyPaysToPlayer;
    public int[] ItemsTheyDropAfterEating;

    private StatsControll MTO_StatsControll;
    private PubObject MTO_PubObject;
    private GetItem MTO_GetItem;

    public bool GoBackOffScreen;

    private Vector2 maxpos = new Vector2(2, 2);
    public bool NoAudioOnItems;
    void Start()
    {

        CM = GetComponent<CharacterMove>();
        _transform = transform;
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        Stats = GetComponent<StatsControll>();



        DishObject = new GameObject();
        DishObject.AddComponent<SpriteRenderer>();
        DishObject.transform.parent = transform;
        DishObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);



        DamageClips.Add(Resources.Load<AudioClip>("Sound/Hits/Player_Get_Damage_0"));

        StartMaterial = GetComponent<SpriteRenderer>().material;
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");

        pl = GameObject.Find("Player").GetComponent<Player>();

      
        if (pl.inv.GetItemInDatabase(315) != null)
            Beds.Add(pl.inv.GetItemInDatabase(315).ObjectPrefs);


        Const = GameObject.Find("Constructor").GetComponent<Constructor>();


        PayTimer = 60 + Random.Range(-5, 10);
        ActionTimer = 0;

        RT = GameObject.Find("Grid").transform.Find("Floor").GetComponent<Tilemap>();
        Brush.Add(Resources.Load<TileBase>("Brushes/Wall_Red"));
        Brush.Add(null);

        int max = 100;
        ToiletTimer = 300 + Random.Range(0, 100);
        PoisonDelay = 10;

        BloodEffect_0 = Resources.Load<GameObject>("Prefabs/Effects/BloodEffect_0");
        BloodEffect_1 = Resources.Load<GameObject>("Prefabs/Effects/BloodEffect_1");

        MoneyDifference = Resources.Load<GameObject>("Prefabs/UI/MoneyDifference");


        if (Enemy) constr.Enemies.Add(new ObjectOnBoard(GetComponent<StatsControll>().DatabaseID, transform.position, name, gameObject, GetComponent<StatsControll>(), GetComponent<PubObject>()));


        //  Dish = DD.DishList[0];
    }

    private void Update()
    {
        UpdateMoveControll();
    }



    public void StartAttack()
    {

        SetMoveToObject(pl.gameObject);
        FollowTimer = Time.fixedTime + FollowTimerDelay;
    }


    void UpdateMoveControll()
    {
        if (GoBackOffScreen && (Mathf.Abs(transform.position.x - pl._transform.position.x) > 10 || Mathf.Abs(transform.position.y - pl._transform.position.y) > 10))
        {
            if (CM != null)
            {
                CM.RemoveFromAttackList();
                CM.GoBack();
            }
            return;
        }

        if (Const.Game_SPEED <= 0) return;

        /* if (MoveToObject != null && !CM.PathComlitionCheck())
         {
             print(name  + " UnSetMoveToObject");
             UnSetMoveToObject();
         }*/



        if (Enemy)
            EnemyMove();

        bool DamageFromEnemies = true;
        if (CM != null)
        {
            CM.WallObsticleCheck(pl.gameObject);

            if (Enemy)
                DamageFromEnemies = CM.PathComlitionCheck();
            else DamageFromEnemies = true;
        }
        else DamageFromEnemies = true;

        if (Soldier) DamageFromEnemies = false;

        if (DamageFromEnemies && InvisTimer < Time.fixedTime && MinDamage > 0 && ((Stats.CurrentGrowState > 0 && Stats.GrowingSprites.Length > 1) || (Stats.GrowingSprites.Length <= 1)))
        {

            if (CM != null)
            {
                if (pl.coll_obj.Contains(gameObject) && (!CM.WallObsticleCheck(pl.gameObject) || (CM.WallObsticleCheck(pl.gameObject) && Vector2.Distance(transform.position, pl._transform.position) < 0.1f)))
                {

                    if (SelfDestroyOnlyDamagingPlayer)
                        Stats.HP = 0;

                    pl.ReceiveDamage(MinDamage);
                }
            }
            else if (pl.coll_obj.Contains(gameObject))
            {

                if (SelfDestroyOnlyDamagingPlayer)
                    Stats.HP = 0;

                pl.ReceiveDamage(MinDamage);
            }



            if (GetComponent<Tail>() != null)
            {
                for (int i = 0; i < GetComponent<Tail>().TailObjs.Count; i++)
                {
                    if (pl.coll_obj.Contains(GetComponent<Tail>().TailObjs[i]))
                        pl.ReceiveDamage(MinDamage);

                }
            }



        }



        if (CarringDish != null) DishObject.GetComponent<SpriteRenderer>().sprite =
                    Resources.Load<Sprite>("Sprites/Items/" + CarringDish.itemNames[0]);
        else DishObject.GetComponent<SpriteRenderer>().sprite = null;



     

        if (pl.DayNight.Day_Cycle == DayAndNight.DayCycle.Night && Stats.CanSleep)
        {

            for (int i = 0; i < Beds.Count; i++)
            {
                SearchForTargets(Beds[i], 0);

            }
        }

        if (!Stats.CanBeHungry || (Stats.CanBeHungry && Stats.Satiety == Stats.SatietyMax))
        {
            if (ItemPicker)
            {
                ItemPickerMove();
                if (Stats.Payed)
                    MoveBetweenStructures();
            }
            if (Client)
                ClientMove();
        }


        if (MoveBetweenA_B) MoveBetweenAB();

        if (Client) ToTheToilet();





        if (TargetGameObjects.Length > 0)
        {
            if (Stats.Payed)
                MoveBetweenStructures();
        }





        if (InvisTimer - 0.8f > Time.fixedTime)
        {
            SetColotAndMaterial(0.5f, WhiteMaterial);
        }
        else
        {
            if (InvisTimer > Time.fixedTime)
                SetColotAndMaterial(0.5f, StartMaterial);
            else SetColotAndMaterial(1, StartMaterial);
        }





        if (PoisonTimer < 0 && Poison > 0)
        {
            Stats.HP--;
            Poison--;
            PoisonTimer = PoisonDelay;
        }



        ToiletTimer -= Time.deltaTime * Const.Game_SPEED;

        MoveTimer -= Time.deltaTime * Const.Game_SPEED;



        PoisonTimer -= Time.deltaTime * Const.Game_SPEED;

        ActionTimer -= Time.deltaTime * Const.Game_SPEED;

        PayTimer -= Time.deltaTime * Const.Game_SPEED;
        SearchForTargetsTimer -= Time.deltaTime * Const.Game_SPEED;



        if (moneyui != null) moneyui.transform.position = new Vector3(_transform.position.x, moneyui.transform.position.y, moneyui.transform.position.z);




    }

    void MoveBetweenAB()
    {
        if (CarringDish == null)
        {

            for (int i = 0; i < A_Point.Length; i++)
                SearchForTargets(A_Point[i], 0);
        }
        else
        {
            for (int i = 0; i < TargetGameObjects.Length; i++)
                SearchForTargets(TargetGameObjects[i], 0);
        }

    }



    void ToTheToilet()
    {

        if (ToiletTimer >= 0)
            return;

        GameObject[] Toilets = GameObject.FindGameObjectsWithTag("Toilet");

        if (Toilets.Length <= 0)
        {
            MakeAPoop();
            return;
        }


        if (MoveToObject == null)
        {




            for (int i = 0; i < Toilets.Length; i++)
            {
                if (!Toilets[i].GetComponent<StatsControll>().Occupied && Toilets[i].transform.parent == null)
                {
                    SetMoveToObject(Toilets[i]);

                    InTheToilet = true;
                    break;
                }
            }


        }



        if (!MTO_StatsControll.Occupied)
        {
            WalkToTheTarget();

        }
        else
        {
            if (CheckToiletOccupation(Toilets))
            {
                for (int i = 0; i < Toilets.Length; i++)
                {
                    if (!Toilets[i].GetComponent<StatsControll>().Occupied && Toilets[i].transform.parent == null)
                    {
                        SetMoveToObject(Toilets[i]);

                        InTheToilet = true;
                        break;
                    }
                }
            }
            else
            {

                MakeAPoop();
            }

        }


    }

    void MakeAPoop()
    {

        if (constr.AllPoop < constr.MaxPoop)
        {
            DropObject(pl.inv.GetItemInDatabase(9999).ObjectPrefs);
            constr.AllPoop++;
        }


        ToiletTimer = pl.DayNight.DayLength / 1.5f;


    }

    void FromTheToilet()
    {
        if (!InTheToilet) return;

        if (MoveToObject == null)
            return;


        if (MTO_StatsControll != null)
            MTO_StatsControll.Occupied = false;
        if (MoveToObject.GetComponent<AudioSource>() != null)
            MoveToObject.GetComponent<AudioSource>().Play();



        InTheToilet = false;

        print("FromTheToilet " + name);
        ToiletTimer = pl.DayNight.DayLength / 1.5f;


    }

    bool CheckToiletOccupation(GameObject[] Toilets)
    {


        for (int i = 0; i < Toilets.Length; i++)
        {
            if (!Toilets[i].GetComponent<StatsControll>().Occupied && Toilets[i].transform.parent == null)
            {
                return true;
            }
        }

        return false;
    }

    void TakeASit()
    {
        if (MoveToObject == null)
            return;

        if (!MoveToObject.GetComponent<PubObject>().Table)
            return;

        if (_transform.position != MoveToObject.transform.position)
            return;

        for (int i = 0; i < MoveToObject.GetComponent<PubObject>().Sits.Count; i++)
        {
            if (MoveToObject.GetComponent<PubObject>().Sits[i].Num == 0)
            {
                MoveToObject.GetComponent<PubObject>().Sits[i].Num = 1;
                break;
            }
        }

    }



    void ClientMove()
    {
        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);



        TakeASit();

    }



    bool CollWithOtherObject()
    {
        bool result = false;

        if (GetComponent<CollList>() == null)
            return false;


        for (int i = 0; i < constr.OBOnBoard.Count; i++)
        {
            if (constr.OBOnBoard[i].Object != null)
            {
                if (GetComponent<CollList>().GetCollList().Contains(constr.OBOnBoard[i].Object))
                {
                    result = true;
                    break;
                }
            }

        }



        return result;
    }



    bool CollWithMoveToObject()
    {
        bool result = false;

        if (GetComponent<CollList>() == null)
            return false;



        if (GetComponent<CollList>().GetCollList().Contains(MoveToObject))
            result = true;




        return result;
    }



    void EnemyMove()
    {
        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);

        // TriggerOffScreenF();


        Vector2 MinCam = new Vector2(pl.MainCamera.transform.position.x - pl.MainCamera.orthographicSize / 2, pl.MainCamera.transform.position.y - pl.MainCamera.orthographicSize / 4);


        if (Mathf.Abs(transform.position.x - pl.transform.position.x) < FollowBorder &&
            Mathf.Abs(transform.position.y - pl.transform.position.y) < FollowBorder)
        {

            //ATTACKING PLAYER

            float BORDER = pl.MainCamera.GetComponent<CameraBor>().cameraHalfWidth;

            if (!pl.EnableAttackSoundIfEnemyInCamera)
                BORDER = 100;

            if (Mathf.Abs(transform.position.x - pl.MainCamera.transform.position.x) < BORDER / 2.5f &&
                Mathf.Abs(transform.position.y - pl.MainCamera.transform.position.y) < BORDER / 5f)
            {

                if (CM != null) CM.StartAttack();


                SetMoveToObject(pl.gameObject);

                FollowTimer = Time.fixedTime + FollowTimerDelay;
            }


            if (Mathf.Abs(transform.position.x - pl.transform.position.x) > BORDER ||
                Mathf.Abs(transform.position.y - pl.transform.position.y) > BORDER)
            {
                print("NOT IN CAMERA: " + name);

                if (CM != null) CM.RemoveFromAttackList();
            }




        }
        else
        {


            // ATTACKING WORKERS, FRIENDS

            GameObject[] Perss = GameObject.FindGameObjectsWithTag("Pers");

            for (int i = 0; i < Perss.Length; i++)
            {
                AttackWorkers(i);
            }

            if (CM != null)
            {
                if (CM.Attacking)
                {
                    if (MoveToObject == pl.gameObject && FollowTimer < Time.fixedTime) UnSetMoveToObject();
                }
            }
        }



        WalkToTheTarget();



        if (CM != null)
        {
            if (MoveToObject != null)
            {

                CM.MovePoints = new Transform[2] { transform, MoveToObject.transform };
                CM.MovePointsBuffer = new List<Vector2>();

                if (CM.MovePointsBuffer.Count < CM.MovePoints.Length)
                {
                    for (int i = 0; i < CM.MovePoints.Length; i++)
                        CM.MovePointsBuffer.Add(CM.MovePoints[i].position);
                }
            }
            else
            {
                if (GetComponent<MovementControll>().GoingBack)
                    CM.GoBack();

            }
        }



        if (CM == null)
            return;

        if (MoveToObject == null || !CM.Attacking)
            return;


        if (MoveTimer >= 0 || Mathf.Abs(transform.position.x - MoveToObject.transform.position.x) >= 0.01f || Mathf.Abs(transform.position.y - MoveToObject.transform.position.y) >= 0.01f)
            return;




        if (MoveToObject.transform.parent != null)
        {
            if (MoveToObject.transform.parent.GetComponent<PubObject>() != null)
            {
                for (int i = 0; i < MoveToObject.transform.parent.GetComponent<PubObject>().Sits.Count; i++)
                {
                    if (MoveToObject.transform.parent.GetComponent<PubObject>().Sits[i].Name == MoveToObject.name)
                    {
                        MoveToObject.transform.parent.GetComponent<PubObject>().Sits[i].Num = 0;
                        //print("ZERO");
                    }
                }
            }
        }



        if (MoveToObject.GetComponent<MovementControll>() == null)
        {




            // Const.AddLogPart("Clients death: -" + 50, "Смерть клієнта: -" + 50,gameObject);

            for (int j = 0; j < Const.OBOnBoard.Count; j++)
            {
                if (MoveToObject == Const.OBOnBoard[j].Object)
                    Const.OBOnBoard.RemoveAt(j);
            }


            if (MoveToObject != pl.gameObject)
                Const.DestroyThis(MoveToObject);


        }
        else
        {
            GameObject BE = Instantiate<GameObject>(BloodEffect_0);
            BE.transform.position = transform.position;


            Const.SpendMoney(transform.position, 5);
  
            MTO_StatsControll.HP -= MinDamage;
        }
        UnSetMoveToObject();
        MoveTimer = 4f;




    }


    void ItemPickerMove()
    {

        if (Waitress)
        {
            WaitressMove();
            return;
        }

        if (Const.DroppedItems.Count <= 0) return;


        for (int i = 0; i < Const.DroppedItems.Count; i++)
        {
            GameObject dropped = Const.DroppedItems[i];
            ItemPickerMoveThroughDropped(ref dropped);
        }


    }

    void ItemPickerMoveThroughDropped(ref GameObject dropped)
    {
        if (dropped == null || pl.inv.GetItemInDatabase(dropped.GetComponent<GetItem>().item[0]).Dish)
            return;

        if (MoveToObject != null) return;

        if (MoveToObject == dropped) return;

        if (dropped.GetComponent<StatsControll>().Occupied) return;

        if (PickSpecificItem.Length <= 0)
        {
            SetMoveToObject(dropped);
        }
        else
        {
            for (int i = 0; i < PickSpecificItem.Length; i++)
                for (int t = 0; t < dropped.GetComponent<GetItem>().item.Length; t++)
                    if (dropped.GetComponent<GetItem>().item[t] == PickSpecificItem[i])
                        SetMoveToObject(dropped);


        }



    }

    void WaitressMove()
    {

        if (CarringDish != null)
        {
            for (int i = 0; i < TargetGameObjects.Length; i++)
            {

                SearchForTargets(TargetGameObjects[i], 0);
            }
            return;

        }


        if (Const.DroppedItems.Count <= 0)
            return;


        for (int i = 0; i < Const.DroppedItems.Count; i++)
        {
            //  print(name + "Waitress 2");
            if (Const.DroppedItems[i] != null)
            {

                if (PickSpecificItem.Length <= 0)
                {
                    if (MoveToObject != Const.DroppedItems[i] && pl.inv.GetItemInDatabase(Const.DroppedItems[i].GetComponent<GetItem>().item[0]).Dish && !Const.DroppedItems[i].GetComponent<GetItem>().Crafting)
                    {
                        // print(name + "Waitress 4");
                        SetMoveToObject(Const.DroppedItems[i]);


                    }
                }
                else
                {
                    for (int j = 0; j < PickSpecificItem.Length; j++)
                    {
                        if (MoveToObject != Const.DroppedItems[i] && !Const.DroppedItems[i].GetComponent<GetItem>().Crafting && PickSpecificItem[j] == Const.DroppedItems[i].GetComponent<GetItem>().item[0])
                            SetMoveToObject(Const.DroppedItems[i]);
                    }
                }
            }
        }












    }


    void WalkToTheTarget()
    {

        if (Const.Game_SPEED == 0)
            return;

        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);


        if (CM == null)
            return;

        if (MoveToObject == null)
        {
            if (GoingBack)
                CM.GoBack();
            return;
        }



        CM.MovePoints = new Transform[2] { _transform, MoveToObject.transform };
        CM.MovePointsBuffer = new List<Vector2>();

        if (CM.MovePointsBuffer.Count < CM.MovePoints.Length)
        {
            for (int i = 0; i < CM.MovePoints.Length; i++)
                CM.MovePointsBuffer.Add(CM.MovePoints[i].position);
        }



    }



    void MoveBetweenStructures()
    {
        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);



        if (MoveToObject != null)
        {

            float minborder = 0.33f;

            if (Mathf.Abs(_transform.position.x - MoveToObject.transform.position.x) > minborder || Mathf.Abs(_transform.position.y - MoveToObject.transform.position.y) > minborder)
            {
                WalkToTheTarget();
            }

            if (Mathf.Abs(_transform.position.x - MoveToObject.transform.position.x) <= minborder && Mathf.Abs(_transform.position.y - MoveToObject.transform.position.y) <= minborder)
            {

                OnThePoint();

            }

            if (MTO_PubObject != null)
            {
                if (MTO_PubObject.wall > 0 && CollWithMoveToObject())
                {

                    OnThePoint();

                }
            }

        }
        else
        {
            if (!ItemPicker && !MoveBetweenA_B)
            {


                if (TargetGameObjects.Length > 0)
                    for (int i = 0; i < TargetGameObjects.Length; i++)
                        SearchForTargets(TargetGameObjects[i], 0);



            }
        }
    }


    void OnThePoint()
    {

        ObjectOfOccupation = MoveToObject;

        if (Cook)
            CheckItemForCooking();

        if (Soldier)
        {
            if (MTO_StatsControll != null)
            {
                if (Stats.Damage != 0)
                {

                    if (CM != null)
                        CM.Attacking = true;

                    MTO_StatsControll.GetDamage(Stats.Damage);
                    Stats.GetDamage(Stats.Damage);
                    UnSetMoveToObject();
                }
            }
        }

        if (MTO_StatsControll != null)
            CharacterEats();



       

        if (MoveBetweenA_B)
        {
            if (CarringDish == null)
            {

                if (MoveToObject.GetComponent<PubObject>().DishesOnTable.Count > 0)
                {
                    CarringDish = MoveToObject.GetComponent<PubObject>().DishesOnTable[0];
                    RemoveDishesOnTable(0, 1);

                    UnSetMoveToObject();

                    return;
                }
            }
            else
            {

                AddDishesOnTable(CarringDish);

                CarringDish = null;
                UnSetMoveToObject();


                return;

            }


        }

        if (DestroyTarget && MoveToObject != null)
        {
            MTO_StatsControll.HP = 0;

        }

        if (CarringDish != null && MoveToObject != null)
        {

            if (MoveToObject.GetComponent<PubObject>() != null)
            {

                if (MoveToObject.GetComponent<PubObject>().Table)
                {

                    AddDishesOnTable(CarringDish);
                    CarringDish = null;
                }
            }
        }



        CharacterBringsItemToUs();
        ItemProduction();

        if (MoveToObject != null)
        {
            if (Client && MoveToObject.tag == "Toilet") FromTheToilet();
            bool table = false;
            if (MTO_PubObject != null) table = MTO_PubObject.Table;

            if (MTO_StatsControll != null) MTO_StatsControll.HasAChracter = true;

            


            if (OnTheTableWaiting >= 10)
            {
                UnSetMoveToObject();
                OnTheTableWaiting = 0;
            }
            else
            {
                OnTheTableWaiting+=Time.deltaTime;
             
            }



        }

        if (SelfDestroy) Stats.HP = 0;

    }






    void SetColotAndMaterial(float Alpha, Material material)
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().material = material;
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, Alpha);
        }


        for (int i = 0; i < transform.childCount; i++)
        {
            if (_transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
            {
                _transform.GetChild(i).GetComponent<SpriteRenderer>().material = material;
                _transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(_transform.GetChild(i).GetComponent<SpriteRenderer>().color.r, _transform.GetChild(i).GetComponent<SpriteRenderer>().color.g, _transform.GetChild(i).GetComponent<SpriteRenderer>().color.b, Alpha);

            }
        }
    }



    void SearchForTargets(GameObject Target, int MinGrowState)
    {


        if (SearchForTargetsTimer > 0)
            return;


        GameObject MoveToOB = null;


        if (Const.OBOnBoard.Count > 0)
        {
            maxpos = new Vector2(99999, 99999);

            for (int i = 0; i < Const.OBOnBoard.Count; i++)
            {
                if (MoveToObject != null && MTO_StatsControll != null)
                    if (MTO_StatsControll.DatabaseID == Target.GetComponent<StatsControll>().DatabaseID) break;


                CheckAndSetTheTarget(i, Target, ref MoveToOB);

            }


        }




    }

    void SetMoveToObject(GameObject target)
    {


        MoveToObject = target;

        MTO_StatsControll = target.GetComponent<StatsControll>();

        if (target.GetComponent<PubObject>() != null)
            MTO_PubObject = target.GetComponent<PubObject>();
        MTO_GetItem = target.GetComponent<GetItem>();

        if (MTO_StatsControll != null)
            MTO_StatsControll.Occupied = true;
    }


    bool CheckTheTarget(int i, GameObject Target)
    {
        if (Const.OBOnBoard[i].Object == null)
            return false;
        if (Const.OBOnBoard[i].ID != Target.GetComponent<StatsControll>().DatabaseID)
            return false;
        if (Const.OBOnBoard[i].Object.GetComponent<StatsControll>().Occupied && !IgnoreOccupation)
            return false;

        if (CarringDish != null && Const.OBOnBoard[i].Object.GetComponent<PubObject>().DishesOnTable.Count > 0)
        {
            for (int j = 0; j < Const.OBOnBoard[i].Object.GetComponent<PubObject>().DishesOnTable.Count; j++)
            {
                if (Const.OBOnBoard[i].Object.GetComponent<PubObject>().DishesOnTable[j].itemID == CarringDish.itemID)
                    return false;
            }
        }


        if (Mathf.Abs(Const.OBOnBoard[i].Object.transform.position.x - transform.position.x) <= 0.1f &&
            Mathf.Abs(Const.OBOnBoard[i].Object.transform.position.y - transform.position.y) <= 0.1f && !MoveBetweenA_B)
            return false;


        return true;
    }

    void CheckAndSetTheTarget(int i, GameObject Target, ref GameObject MoveToOB)
    {

        if (!CheckTheTarget(i, Target)) return;


        for (int j = 0; j < Const.OBOnBoard.Count; j++)
        {

            if (CheckTheTarget(j, Target))
            {
                if (maxpos.magnitude > Const.OBOnBoard[j].Place.magnitude)
                {
                    maxpos = Const.OBOnBoard[j].Place;
                    i = j;
                }
            }
        }




        MoveToOB = Const.OBOnBoard[i].Object;

        SetMoveToObject(MoveToOB);

        SearchForTargetsTimer = 3;

        ObjectOfOccupation = null;

        if (!IgnoreOccupation)
            Const.OBOnBoard[i].Object.GetComponent<StatsControll>().Occupied = true;


    }



    void AttackWorkers(int i)
    {
        if (GetComponent<Attacks>() != null) return;


        GameObject[] Perss = GameObject.FindGameObjectsWithTag("Pers");

        if (Perss[i] == null)
            return;

        GameObject p = Perss[i];

        if (p.transform.parent == constr.transform)
            return;

        if (Mathf.Abs(p.transform.position.x - transform.position.x) >= 2 ||
            Mathf.Abs(p.transform.position.y - transform.position.y) >= 2)
            return;


        if (p.GetComponent<MovementControll>() != null)
        {
            if (!p.GetComponent<MovementControll>().Enemy && !p.GetComponent<MovementControll>().Soldier && p.transform.parent != constr.transform)
            {
                SetMoveToObject(p);


            }
        }
        else if (p.transform.parent != constr.transform)
            SetMoveToObject(p);



    }

    void CharacterBringsItemToUs()
    {
        //Character brings item to us

        if (Cook || (Stats.CanBeHungry && Stats.Satiety <= 0) || MoveToObject == null || !BringsPickedItemToThePlayer || Waitress)
            return;

        if (MTO_StatsControll != null)
        {
            if (MTO_StatsControll.CurrentGrowState > 0)
            {
                if (!DropItemsOnTarget)
                {

                    for (int i = 0; i < MTO_StatsControll.ItemIDs.Length; i++)
                        if (!NoAudioOnItems)
                            pl.inv.AddItem(MTO_StatsControll.ItemIDs[i], MTO_StatsControll.CurrentGrowState, pl.inv.GetItemInDatabase(MTO_StatsControll.ItemIDs[i]).Durability, pl._transform.position);
                        else
                            pl.inv.AddItemNOAUDIO(MTO_StatsControll.ItemIDs[i], MTO_StatsControll.CurrentGrowState, pl.inv.GetItemInDatabase(MTO_StatsControll.ItemIDs[i]).Durability, pl._transform.position);



                }
                else
                {

                    for (int i = 0; i < MTO_StatsControll.ItemIDs.Length; i++)
                    {
                        if (!NoAudioOnItems)
                            pl.inv.DropItemInSameSpot(_transform.position, MTO_StatsControll.CurrentGrowState, MTO_StatsControll.ItemIDs, pl.inv.GetItemInDatabase(MTO_StatsControll.ItemIDs[i]).Durability);
                        else
                            pl.inv.DropItemInSameSpotNOAUDIO(_transform.position, MTO_StatsControll.CurrentGrowState, MTO_StatsControll.ItemIDs, pl.inv.GetItemInDatabase(MTO_StatsControll.ItemIDs[i]).Durability);

                    }

                }


                if (Stats.DurabilityMax > -1) Stats.Durability--;
            }

            MTO_StatsControll.CurrentGrowState = 0;
            MTO_StatsControll.Occupied = false;

            MTO_StatsControll.GrowTimer = Time.fixedTime + MTO_StatsControll.GrowDelay;

        }

        GetItem GI = MoveToObject.GetComponent<GetItem>();

        if (GI != null && GI.item.Length > 0)
        {

            for (int i = 0; i < GI.item.Length; i++)
                pl.inv.AddItem(GI.item[i], GI.itemcount[i], pl.inv.GetItemInDatabase(GI.item[i]).Durability, pl._transform.position);
            print("CharacterBringsItemToUs 1");
            if (Stats.DurabilityMax > -1) Stats.Durability--;


            if (GI._destroy) Const.DestroyThis(MoveToObject);
        }



        UnSetMoveToObject();


    }



    void CharacterEats()
    {

        //Character Eats
        if (!Stats.CanBeHungry || Stats.Satiety == Stats.SatietyMax || MoveToObject.GetComponent<PubObject>().DishesOnTable.Count == 0)
            return;

        Stats.HungerTimer = pl.DayNight.DayLength / Stats.SatietyMax;


        if (LeavesTrash)
        {
            if (constr.AllTrash < constr.MaxTrash)
            {
                DropObject(pl.inv.GetItemInDatabase(9998).ObjectPrefs);
                constr.AllTrash++;
            }
        }

        if (ItemsTheyDropAfterEating.Length > 0)
        {
            for (int i = 0; i < ItemsTheyDropAfterEating.Length; i++)
                if (!NoAudioOnItems)
                    pl.inv.DropItemInSameSpot(_transform.position, Stats.ItemCount, ItemsTheyDropAfterEating, pl.inv.GetItemInDatabase(ItemsTheyDropAfterEating[i]).Durability);
                else
                    pl.inv.DropItemInSameSpotNOAUDIO(_transform.position, Stats.ItemCount, ItemsTheyDropAfterEating, pl.inv.GetItemInDatabase(ItemsTheyDropAfterEating[i]).Durability);

        }



        if (pl.Payment > 0)
        {
            if (ItemsTheyPaysToPlayer.Length > 0)
            {
                for (int i = 0; i < ItemsTheyPaysToPlayer.Length; i++)
                    pl.inv.AddItemNOAUDIO(ItemsTheyPaysToPlayer[i], pl.Payment * MoveToObject.GetComponent<PubObject>().DishesOnTable[0].Cost, -1, transform.position);

            }
            else pl.inv.AddItemNOAUDIO(9, pl.Payment * MoveToObject.GetComponent<PubObject>().DishesOnTable[0].Cost, -1, transform.position);
        }


        Stats.Satiety += MoveToObject.GetComponent<PubObject>().DishesOnTable[0].Satiety;
        if (Stats.Satiety > Stats.SatietyMax) Stats.Satiety = Stats.SatietyMax;

        RemoveDishesOnTable(0, 1);

        UnSetMoveToObject();

        SearchForTargetsTimer = 0;

    }



    void ItemProduction()
    {
      
        if (MoveToObject == null)
            return;

        if (MTO_GetItem == null)
            return;


        if (Cook)
        {

            CookItemProduction();
            return;
        }

       
        if (!ItemPicker || MTO_GetItem.item.Length <= 0)
            return;


        ReguralItemProduction();


    }

    void ReguralItemProduction()
    {

        for (int i = 0; i < MTO_GetItem.item.Length; i++)
        {
            if (!Waitress)
            {
                pl.inv.AddItem(MTO_GetItem.item[i], MTO_GetItem.itemcount[i], pl.inv.GetItemInDatabase(MTO_GetItem.item[i]).Durability, transform.position);
            }
            else
            {


                CarringDish = pl.inv.DeepCopyItem(MTO_GetItem.item[0], 1, pl.inv.GetItemInDatabase(MTO_GetItem.item[0]).Durability);

              
            }
        }


        if (Stats.DurabilityMax > -1) Stats.Durability--;

        for (int i = 0; i < Const.DroppedItems.Count; i++)
        {
            if (Const.DroppedItems[i] == MoveToObject)
            {
                Const.DestroyThis(Const.DroppedItems[i]);
                break;
            }

        }


        UnSetMoveToObject();
    }

    void CookItemProduction()
    {

        if (!MTO_GetItem.Oven)
            return;


        for (int i = 0; i < MTO_GetItem.item.Length; i++)
        {
            int GetItemID = MTO_GetItem.item[i];
            int itemn = 0;
            int[] NeededItemsIDs = pl.inv.GetItemInDatabase(GetItemID).NeedItemsIDs;

            if (CheckIfItemCanBeCooked(NeededItemsIDs, pl.inv.GetItemInDatabase(GetItemID).NeedItemsCounts))
            {
                DropCookedItem(NeededItemsIDs, pl.inv.GetItemInDatabase(GetItemID).NeedItemsCounts, MTO_GetItem.item[i]);
                break;
            }

            ActionTimer = ActionDelay;
        }


    }

    void DropCookedItem(int[] NeededItemsIDs, int[] NeededItemsCounts, int dropppedItemID)
    {


        for (int n = 0; n < NeededItemsIDs.Length; n++)
        {
            if (pl.inv.GetItem(NeededItemsIDs[n]) != null)
            {
                if (pl.inv.GetItem(NeededItemsIDs[n]).Count >= NeededItemsCounts[n])
                {
                    pl.inv.ReduceItemCount(NeededItemsIDs[n], NeededItemsCounts[n]);
                }
                else
                {
                    for (int i = 0; i < MTO_PubObject.DishesOnTable.Count; i++)
                    {
                        if (MTO_PubObject.DishesOnTable[i].itemID == NeededItemsIDs[n])
                            if (MTO_PubObject.DishesOnTable[i].Count >= NeededItemsCounts[n])
                                MTO_PubObject.DishesOnTable[i].Count -= NeededItemsCounts[n];
                    }

                }
            }
            else
            {
                for (int i = 0; i < MTO_PubObject.DishesOnTable.Count; i++)
                {
                    RemoveDishesOnTable(i, NeededItemsCounts[n]);
                }
            }
        }

        pl.inv.DropItemInSameSpotNOAUDIO(_transform.position, 1, new int[1] { dropppedItemID }, pl.inv.GetItemInDatabase(dropppedItemID).Durability);
        ActionTimer = ActionDelay;

    }






    void CheckItemForCooking()
    {

        int itemneededall = 0;

        for (int i = 0; i < MTO_GetItem.item.Length; i++)
        {
            int GetItemID = MTO_GetItem.item[i];
            int itemn = 0;
            int[] NeededItemsIDs = pl.inv.GetItemInDatabase(GetItemID).NeedItemsIDs;


            if (CheckIfItemCanBeCooked(NeededItemsIDs, pl.inv.GetItemInDatabase(GetItemID).NeedItemsCounts))
                itemneededall++;
        }

        if (itemneededall <= 0)
        {

            constr.AddLogPartOnes("You dont have enough resources for cook to make a dish", "У вас недостатньо ресурсів, щоб приготувати страву", "料理を作るのに十分な資源がない。", gameObject);

        }
        else constr.RemoveLogPart("You dont have enough resources for cook to make a dish");


    }

    bool CheckIfItemCanBeCooked(int[] NeededItemsIDs, int[] NeedItemsCounts)
    {
        int itemn = 0;

        for (int n = 0; n < NeededItemsIDs.Length; n++)
        {
            if (pl.inv.GetItem(NeededItemsIDs[n]) != null)
            {
                if (pl.inv.GetItem(NeededItemsIDs[n]).Count >= NeedItemsCounts[n])
                {
                    itemn++;
                }
                else
                {
                    for (int i = 0; i < MTO_PubObject.DishesOnTable.Count; i++)
                    {
                        if (MTO_PubObject.DishesOnTable[i].itemID == NeededItemsIDs[n])
                            if (MTO_PubObject.DishesOnTable[i].Count >= NeedItemsCounts[n])
                                itemn++;
                    }

                }
            }
            else
            {
                for (int i = 0; i < MTO_PubObject.DishesOnTable.Count; i++)
                {
                    if (MTO_PubObject.DishesOnTable[i].itemID == NeededItemsIDs[n])
                        if (MTO_PubObject.DishesOnTable[i].Count >= NeedItemsCounts[n])
                            itemn++;
                }
            }
        }

        if (itemn >= NeededItemsIDs.Length) return true;
        else return false;
    }



    void TriggerOffScreenF()
    {
        BoxCollider2D Box = GetComponent<BoxCollider2D>();

        if (Box == null) return;

        if (!TriggerOFF_OffScreen)
        {

            if (Box != null) Box.isTrigger = true;
            return;
        }



        if (CollWithOtherObject())
        {
            if (Mathf.Abs(transform.position.x - pl.transform.position.x) < pl.MainCamera.orthographicSize * 2 &&
                        Mathf.Abs(transform.position.y - pl.transform.position.y) < pl.MainCamera.orthographicSize)
                Box.isTrigger = true;
            else
                Box.isTrigger = false;


        }
        else
        {

            if (CM != null)
            {
                if (CM.LegsAnim != null)
                {
                    Box.isTrigger = true;

                }
            }

        }


    }

    public void UnSetMoveToObject()
    {
        if (MTO_StatsControll != null)
        {
            MTO_StatsControll.Occupied = false;
          MTO_StatsControll.HasAChracter = false;

        }

        MoveToObject = null;

        if (CM != null) CM.Attacking = false;
        

        MTO_StatsControll = null;

        MTO_GetItem = null;
        maxpos = new Vector2(9999999999, 999999999);

    }


    bool CheckPrefabsName(string prefabname, string objectname)
    {
        bool tf = false;

        int count =0;
        for (int i = 0; i < prefabname.Length; i++)
        {
            if (objectname.Length > i)
            {
                if (objectname[i] == prefabname[i])
                    count++;
            }
            
        }
        if (count >= prefabname.Length * 0.6f) tf = true;

        
        return tf;
    }




    void AddDishesOnTable(Item additem)
    {

        if (!MoveToObject.GetComponent<PubObject>().DishesOnTable.Contains(additem))
        {
            MoveToObject.GetComponent<PubObject>().DishesOnTable.Add(additem);
            MoveToObject.GetComponent<PubObject>().DishesOnTable[MoveToObject.GetComponent<PubObject>().DishesOnTable.Count - 1].Count = 1;
        }
        else
        {
            for (int i = 0; i < MoveToObject.GetComponent<PubObject>().DishesOnTable.Count; i++)
            {
                if (MoveToObject.GetComponent<PubObject>().DishesOnTable[i].itemID == additem.itemID)
                {
                    MoveToObject.GetComponent<PubObject>().DishesOnTable[i].Count++;
                    return;
                }
            }
        }
        
    }



    void RemoveDishesOnTable(int i, int count)
    {
        if (MoveToObject.GetComponent<PubObject>().DishesOnTable.Count <= 0) return;

        if (MoveToObject.GetComponent<PubObject>().DishesOnTable[i].Count > 1)
            MoveToObject.GetComponent<PubObject>().DishesOnTable[i].Count-= count;
        else 
            MoveToObject.GetComponent<PubObject>().DishesOnTable.RemoveAt(i);
        
    }


    void DropObject(GameObject obj)
    {
        GameObject poop = Instantiate<GameObject>(obj);
        poop.transform.position = _transform.position;
    }
    
}
