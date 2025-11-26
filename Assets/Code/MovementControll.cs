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



    public bool DropItemsOnTarget;
    public bool DestroyTarget;

    public bool LeavesTrash;

    public GameObject MoveToObject { get; private set; }

    private float PoisonDelay;

    private float  OnTheTableWaiting, PoisonTimer, SearchForTargetsTimer;


    private Constructor Const;
    private Player pl;
    private GameObject moneyui;

  
    private List<TileBase> Brush = new List<TileBase>();

    public float ActionDelay = 10;
    public Enemies EnemiesBase;

    [HideInInspector]
    public int MinDamage = 0;

    public float MoveDelayMax = 0.2f;

    public float FollowTimerDelay = 10;
    private float InvisTimer;

    private Material StartMaterial, WhiteMaterial;
    private List<AudioClip> DamageClips = new List<AudioClip>();
    public float FollowBorder = 2;


    private List<GameObject> Beds = new List<GameObject>();

    public GameObject[] A_Point;
    public GameObject[] TargetGameObjects;


    private Item CarringDish;

    private GameObject DishObject;

    public bool IgnoreOccupation;
    private StatsControll Stats;

    public GameObject ObjectOfOccupation;
    private Transform _transform;
    private CharacterPath CM;


    public bool TriggerOFF_OffScreen;
    public int[] ItemsTheyPaysToPlayer;
    public int[] ItemsTheyDropAfterEating;

    private StatsControll MTO_StatsControll;
    private PubObject MTO_PubObject;
    private GetItem MTO_GetItem;

    public bool GoBackOffScreen;

    private Vector2 maxpos = new Vector2(2, 2);
    public bool NoAudioOnItems;

    private Animator Anim;
    private string CurrentAnimName;
    private float AnimFade;
    private bool ActionOnPointComlete;
    private float OnPointAnimationTimer;

    private SpriteRenderer SPRT;
    void Start()
    {
        SPRT = GetComponent<SpriteRenderer>();
        pl = InitializeObjects.PL;
        Const = InitializeObjects.Constr;

        Anim = GetComponent<Animator>();

        CM = GetComponent<CharacterPath>();
        _transform = transform;
  
        Stats = GetComponent<StatsControll>();



        DishObject = new GameObject();
        DishObject.AddComponent<SpriteRenderer>();
        DishObject.transform.parent = transform;
        DishObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);



        DamageClips.Add(Resources.Load<AudioClip>("Sound/Hits/Player_Get_Damage_0"));

        StartMaterial = SPRT.material;
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");

       

        if (pl.inv.GetItemInDatabase(315) != null)
            Beds.Add(pl.inv.GetItemInDatabase(315).ObjectPrefs);

       

        Brush.Add(Resources.Load<TileBase>("Brushes/Wall_Red"));
        Brush.Add(null);

    
        PoisonDelay = 10;

    }

    private void Update()
    {
        UpdateMoveControll();
        Animations();
    }



    public void StartAttack()
    {

        SetMoveToObject(pl.gameObject);
    }

    void Animations()
    {
        if (Anim == null) return;

        if (pl.menu.MenuONOFF)
        {
            Anim.speed = 0;
            return;

        }

        Anim.speed = 1;
        if (AnimFade > Time.fixedTime)
            Anim.CrossFade(CurrentAnimName, 0.2f, 0);
        else
            Anim.Play(CurrentAnimName, 0);
        

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

    

        bool DamageFromEnemies = true;
        if (CM != null)
        {
            CM.WallObsticleCheck(pl.gameObject);

          
            DamageFromEnemies = true;
        }
        else DamageFromEnemies = true;


        if (DamageFromEnemies && InvisTimer < Time.fixedTime && MinDamage > 0 && ((Stats.CurrentGrowState > 0 && Stats.GrowingSprites.Length > 1) || (Stats.GrowingSprites.Length <= 1)))
        {

            if (CM != null)
            {
                if (pl.coll_obj.Contains(gameObject) && (!CM.WallObsticleCheck(pl.gameObject) || (CM.WallObsticleCheck(pl.gameObject) && Vector2.Distance(transform.position, pl._transform.position) < 0.1f)))
                {

                    if (SelfDestroyOnlyDamagingPlayer)
                        Stats.HP = 0;

               
                }
            }
            else if (pl.coll_obj.Contains(gameObject))
            {

                if (SelfDestroyOnlyDamagingPlayer)
                    Stats.HP = 0;

          
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
         
        }


        if (MoveBetweenA_B) MoveBetweenAB();




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





        PoisonTimer -= Time.deltaTime * Const.Game_SPEED;

        
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


    





    bool CollWithOtherObject()
    {
        bool result = false;

        if (GetComponent<CollList>() == null)
            return false;


        for (int i = 0; i < Const.OBOnBoard.Count; i++)
        {
            if (Const.OBOnBoard[i].Object != null)
            {
                if (GetComponent<CollList>().GetCollList().Contains(Const.OBOnBoard[i].Object))
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



  

    void ItemPickerMove()
    {

       

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

  

    void WalkToTheTarget()
    {

        if (Const.Game_SPEED == 0)
            return;

        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);


        if (CM == null)
            return;

        if (MoveToObject == null)
        {
            SetOnPointToDefault();

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


       

        if (MoveToObject == null)
        {
            if (!ItemPicker && !MoveBetweenA_B)
            {

                SetOnPointToDefault();

                if (TargetGameObjects.Length > 0)
                    for (int i = 0; i < TargetGameObjects.Length; i++)
                        SearchForTargets(TargetGameObjects[i], 0);


            }

            ActionOnPointComlete = false;

            return;
        }



        float mindistance= 0.33f;

        float distance = Mathf.Abs( Vector3.Distance(_transform.position, MoveToObject.transform.position));


        if (distance > mindistance)
        {
            WalkToTheTarget();
        }
        else
        if (distance <= mindistance)
        {

            OnThePoint();

        }
        else
        {
            FadeToAnim("Start");

        }

       
    }


    void OnThePoint()
    {
        if (!ActionOnPointComlete)
        {
            OnPointAnimationTimer = Time.fixedTime + 3;
            FadeToAnim("OnThePoint");
           
            ActionOnPointComlete = true;
            return;
        }

        ObjectOfOccupation = MoveToObject;
        if (OnPointAnimationTimer > Time.fixedTime) return;

        if (MTO_StatsControll != null)
            CharacterEats();



       

        if (MoveBetweenA_B)
        {
            if (CarringDish == null)
            {

                if (MTO_PubObject.DishesOnTable.Count > 0)
                {
                    CarringDish = MTO_PubObject.DishesOnTable[0];
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

            if (MTO_PubObject != null)
            {

                if (MTO_PubObject.Table)
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
        if (SPRT != null)
        {
            SPRT.material = material;
            SPRT.color = new Color(SPRT.color.r, SPRT.color.g, SPRT.color.b, Alpha);
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


        if (Const.OBOnBoard.Count <= 0) return;
        
        maxpos = new Vector2(99999, 99999);

        for (int i = 0; i < Const.OBOnBoard.Count; i++)
        {
            if (MoveToObject != null && MTO_StatsControll != null)
                if (MTO_StatsControll.DatabaseID == Target.GetComponent<StatsControll>().DatabaseID) break;


            CheckAndSetTheTarget(i, Target, ref MoveToOB);

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



  
    void CharacterBringsItemToUs()
    {
        //Character brings item to us

        if ((Stats.CanBeHungry && Stats.Satiety <= 0) || MoveToObject == null || !BringsPickedItemToThePlayer )
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
        if (!Stats.CanBeHungry || Stats.Satiety == Stats.SatietyMax || MTO_PubObject.DishesOnTable.Count == 0)
            return;

        Stats.HungerTimer = pl.DayNight.DayLength / Stats.SatietyMax;


        if (LeavesTrash)
        {
            if (Const.AllTrash < Const.MaxTrash)
            {
                DropObject(pl.inv.GetItemInDatabase(9998).ObjectPrefs);
                Const.AllTrash++;
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
            else pl.inv.AddItemNOAUDIO(9, pl.Payment * MTO_PubObject.DishesOnTable[0].Cost, -1, transform.position);
        }


        Stats.Satiety += MTO_PubObject.DishesOnTable[0].Satiety;
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

        if (!ItemPicker || MTO_GetItem.item.Length <= 0)
            return;


        ReguralItemProduction();


    }

    void ReguralItemProduction()
    {

        for (int i = 0; i < MTO_GetItem.item.Length; i++)
        {
           CarringDish = pl.inv.DeepCopyItem(MTO_GetItem.item[0], 1, pl.inv.GetItemInDatabase(MTO_GetItem.item[0]).Durability);

            
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

    



    public void UnSetMoveToObject()
    {
        if (MTO_StatsControll != null)
        {
            MTO_StatsControll.Occupied = false;
          MTO_StatsControll.HasAChracter = false;

        }

        MoveToObject = null;

        if (CM != null) CM.Attacking = false;
        SetOnPointToDefault();

        MTO_StatsControll = null;

        MTO_GetItem = null;
        maxpos = new Vector2(9999999999, 999999999);

    }

    void SetOnPointToDefault()
    {
        OnPointAnimationTimer = -1;
        FadeToAnim("Start");
        ObjectOfOccupation = null;
        ActionOnPointComlete = false;

    }
    void AddDishesOnTable(Item additem)
    {

        if (!MTO_PubObject.DishesOnTable.Contains(additem))
        {
            MTO_PubObject.DishesOnTable.Add(additem);
            MTO_PubObject.DishesOnTable[MTO_PubObject.DishesOnTable.Count - 1].Count = 1;
            return;
        }
        
        for (int i = 0; i < MTO_PubObject.DishesOnTable.Count; i++)
        {
            if (MTO_PubObject.DishesOnTable[i].itemID == additem.itemID)
            {
                MTO_PubObject.DishesOnTable[i].Count++;
                return;
            }
        }
        
        
    }

    void FadeToAnim(string n)
    {
        if (CurrentAnimName == n) return;
        AnimFade = Time.fixedTime+ 0.2f;
        CurrentAnimName = n;

        print("fade to anim");
    }

    void RemoveDishesOnTable(int i, int count)
    {
        if (MTO_PubObject.DishesOnTable.Count <= 0) return;

        if (MTO_PubObject.DishesOnTable[i].Count > 1)
            MTO_PubObject.DishesOnTable[i].Count-= count;
        else
            MTO_PubObject.DishesOnTable.RemoveAt(i);
        
    }


    void DropObject(GameObject obj)
    {
        GameObject poop = Instantiate<GameObject>(obj);
        poop.transform.position = _transform.position;
    }
    
}
