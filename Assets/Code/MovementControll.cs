using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class MovementControll : MonoBehaviour
{

    public bool SelfDestroy;
    public bool SelfDestroyOnlyDamagingPlayer;
    public bool GoingBack = true;


    public int Poison { get; set; }
    public bool Attacked { get; set; }

    public bool ItemPicker;
    public bool BringsPickedItemToThePlayer;

    public int[] PickSpecificItem;



    public bool DropItemsOnTarget;
    public bool KeepItemForThemselves;
    public bool DestroyTarget;

    public bool LeavesTrash;

    public GameObject MoveToObject { get; private set; }

    private float PoisonDelay;

    private float  OnTheTableWaiting, PoisonTimer, SearchForTargetsTimer, DeathTimer;


    private Constructor Const;
    private Player pl;
    private GameObject moneyui;

  


    public float ActionDelay = 10;
    private float AttackCooldown;
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


    public GameObject[] TargetGameObjects;
    public float mindistanceToTarget = 0.33f;

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
    private bool StartIgnoreOccupation;
    private ItemDatabase itemDatabase;

    void Start()
    {
        itemDatabase = InitializeObjects.Itemdatabase;
        StartIgnoreOccupation = IgnoreOccupation;
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

       

        if (itemDatabase.FindItem(315) != null)
            Beds.Add(itemDatabase.FindItem(315).ObjectPrefs);

       


    
        PoisonDelay = 10;

    }

    private void Update()
    {

        DeathManager();
        

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

        if (pl.Pause())
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
        if (DeathTimer > Time.fixedTime) return;

            CM.CharacterPathUpdate();
        if (OnPointAnimationTimer <= Time.fixedTime)
            CM.OnThePoint = false;
        else CM.OnThePoint = true;

        if (Stats != null && transform!=null)
        {
          
            Stats.ConstructorElement.Place = transform.position;
        }

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





        if (TargetGameObjects.Length > 0)
        {
            if (Stats.Payed)
                MoveBetweenStructures();
        }








        if (PoisonTimer < 0 && Poison > 0)
        {
            Stats.GetDamage(1);
            Poison--;
            PoisonTimer = PoisonDelay;
        }





        PoisonTimer -= Time.deltaTime ;

        
        SearchForTargetsTimer -= Time.deltaTime;



        if (moneyui != null) moneyui.transform.position = new Vector3(_transform.position.x, moneyui.transform.position.y, moneyui.transform.position.z);




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
        if (dropped == null || itemDatabase.FindItem(dropped.GetComponent<GetItem>().item[0]).Dish)
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

       // if(CM.LegsAnim==null)
        FadeToAnim("Walking");

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

        if (OnPointAnimationTimer > Time.fixedTime) return;
       

        if (MoveToObject == null)
        {
            if (!ItemPicker)
            {

                SetOnPointToDefault();

                if (TargetGameObjects.Length > 0)
                    for (int i = 0; i < TargetGameObjects.Length; i++)
                    {
                        if (i > TargetGameObjects.Length || TargetGameObjects.Length == 0) continue;

                        SearchForTargets(TargetGameObjects[i], 0);
                    }

            }

            ActionOnPointComlete = false;

            return;
        }



 

        float distance = Mathf.Abs( Vector2.Distance(_transform.position, MoveToObject.transform.position));


        if (distance > mindistanceToTarget)
        {
            ActionOnPointComlete = false;
            WalkToTheTarget();
        }
        else
        if (distance <= mindistanceToTarget)
        {

            OnThePoint();

        }
        
        if(MoveToObject==null)
        {
            FadeToAnim("Start");

        }

       
    }


    void OnThePoint()
    {



      

        if (!ActionOnPointComlete)
        {
       
            if(!DestroyTarget)
            OnPointAnimationTimer = Time.fixedTime + 3;
            else OnPointAnimationTimer = Time.fixedTime + 1;
            AttackCooldown = Time.fixedTime + 1;
            FadeToAnim("OnThePoint");
           
            ActionOnPointComlete = true;
            return;
        }


        if (DestroyTarget && MoveToObject != null &&
             AttackCooldown <= Time.fixedTime)
        {
            MTO_StatsControll.GetDamage(CharacterDamage());
            AttackCooldown = Time.fixedTime + 1;
            OnPointAnimationTimer = Time.fixedTime + 1;
        }

        ObjectOfOccupation = MoveToObject;
        if (OnPointAnimationTimer > Time.fixedTime)
        {
            FadeToAnim("OnThePoint");
            return;
        }


        if (MTO_StatsControll != null)
            CharacterEats();

       

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

            if (MTO_StatsControll != null)
            {
                MTO_StatsControll.HasCharacter = true;
            
                if(KeepItemForThemselves) 
                 MTO_StatsControll.ReverseMoney = true;
            }
            


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







    void SearchForTargets(GameObject Target, int MinGrowState)
    {


        if (SearchForTargetsTimer > 0)
            return;
        int id = Target.GetComponent<StatsControll>().DatabaseID;

        GameObject MoveToOB = null;

        if (!Const.OBOnBoardTargets.TryGetValue(id, out var value)) return;
        if (Const.OBOnBoardTargets[id].Count <= 0) return;
        
        maxpos = new Vector2(99999, 99999);

        int startelement = Random.Range(0, Const.OBOnBoardTargets[id].Count);

        IgnoreOccupation = StartIgnoreOccupation;

        for (int i = 0; i < Const.OBOnBoardTargets[id].Count; i++)
        {
            if (MoveToObject != null && MTO_StatsControll != null)
                if (MTO_StatsControll.DatabaseID == Target.GetComponent<StatsControll>().DatabaseID) break;

            CheckAndSetTheTarget(i,id, Target, ref MoveToOB);

        }

        if (MoveToObject == null && Target.GetComponent<MovementControll>()!=null)
        {
            IgnoreOccupation = true;

            for (int i = 0; i < Const.OBOnBoardTargets[id].Count; i++)
            {

                CheckAndSetTheTarget(i, id, Target, ref MoveToOB);

            }
            
        }

        SearchForTargetsTimer = Random.Range(3,5);
    }

    public void SetMoveToObject(GameObject target)
    {
     

        MoveToObject = target;

        MTO_StatsControll = target.GetComponent<StatsControll>();

        if (target.GetComponent<PubObject>() != null)
            MTO_PubObject = target.GetComponent<PubObject>();
        MTO_GetItem = target.GetComponent<GetItem>();

        if (MTO_StatsControll != null)
        {
            if (!IgnoreOccupation)
                MTO_StatsControll.Occupied = true;
            SetOccupation(target, true);

        }



    }


    bool CheckTheTarget(int i,int id, GameObject Target)
    {
        if (i >= Const.OBOnBoardTargets[id].Count) return false;


        if (Const.OBOnBoardTargets[id][i].Object == null)
            Const.OBOnBoardTargets[id].RemoveAt(i);

        if (i > Const.OBOnBoardTargets[id].Count) 
            i = Const.OBOnBoardTargets[id].Count - 1;

       // int TargetID = Target.GetComponent<StatsControll>().DatabaseID;


        if (Const.OBOnBoardTargets[id].Count<=0)
           return false;

      //  print(name + " DANGER -- DO NOT USE EVERY FRAME");
        int countontheboard = 0;
        for (int j = 0; j < Const.OBOnBoardTargets[id].Count; j++)
        {
           countontheboard++;

        }
        if (i >= Const.OBOnBoardTargets[id].Count) i = 0;
        if (Const.OBOnBoardTargets[id][i].Object == null) return false;

        if (Const.OBOnBoardTargets[id][i].Object.GetComponent<StatsControll>() == null) return false;

        if (Const.OBOnBoardTargets[id][i].Object.GetComponent<StatsControll>().Occupied && !IgnoreOccupation )
            return false;

     



        if (CarringDish != null && Const.OBOnBoardTargets[id][i].PO.DishesOnTable.Count > 0)
        {
            for (int j = 0; j < Const.OBOnBoardTargets[id][i].PO.DishesOnTable.Count; j++)
            {
                if (Const.OBOnBoardTargets[id][i].PO.DishesOnTable[j].itemID == CarringDish.itemID)
                    return false;
            }
        }


        if (countontheboard > 1)
        {
            if (Vector2.Distance(Const.OBOnBoard[i].Object.transform.position,  transform.position) <= 0.1f)
                return false;
        }
     
        return true;
    }

    void CheckAndSetTheTarget(int i, int id, GameObject Target, ref GameObject MoveToOB)
    {
     
        if (!CheckTheTarget(i,id, Target)) return;

        
        for (int j = 0; j < Const.OBOnBoardTargets[id].Count; j++)
        {
            if (j >= Const.OBOnBoardTargets[id].Count)
            {
                j--;
                continue;
            }


            if (Const.OBOnBoardTargets[id].Count<=0) return;

            if (CheckTheTarget(j, id, Target))
            {
                if(j >= Const.OBOnBoardTargets[id].Count) return;

                if (maxpos.magnitude > Const.OBOnBoardTargets[id][j].Place.magnitude)
                {
                    maxpos = Const.OBOnBoardTargets[id][j].Place;
                    i = j;
                }
            }


        }



        MoveToOB = Const.OBOnBoardTargets[id][i].Object;

        SetMoveToObject(MoveToOB);
   
        SearchForTargetsTimer = Random.Range(3, 5);

        ObjectOfOccupation = null;

       

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

                    if (!KeepItemForThemselves)
                    {
                        for (int i = 0; i < MTO_StatsControll.ItemIDs.Length; i++)
                            if (!NoAudioOnItems)
                                pl.inv.AddItem(MTO_StatsControll.ItemIDs[i], MTO_StatsControll.CurrentGrowState, itemDatabase.FindItem(MTO_StatsControll.ItemIDs[i]).Durability, pl._transform.position);
                            else
                                pl.inv.AddItemNOAUDIO(MTO_StatsControll.ItemIDs[i], MTO_StatsControll.CurrentGrowState, itemDatabase.FindItem(MTO_StatsControll.ItemIDs[i]).Durability, pl._transform.position);
                    }


                }
                else
                {

                    for (int i = 0; i < MTO_StatsControll.ItemIDs.Length; i++)
                    {
                        if (!NoAudioOnItems)
                            pl.inv.DropItemInSameSpot(_transform.position, MTO_StatsControll.CurrentGrowState, MTO_StatsControll.ItemIDs, itemDatabase.FindItem(MTO_StatsControll.ItemIDs[i]).Durability);
                        else
                            pl.inv.DropItemInSameSpotNOAUDIO(_transform.position, MTO_StatsControll.CurrentGrowState, MTO_StatsControll.ItemIDs, itemDatabase.FindItem(MTO_StatsControll.ItemIDs[i]).Durability);

                    }

                }


                if (Stats.DurabilityMax > -1) Stats.Durability--;
            }

            MTO_StatsControll.CurrentGrowState = 0;
            MTO_StatsControll.Occupied = false;
            SetOccupation(MoveToObject, false);

            MTO_StatsControll.GrowTimer = Time.fixedTime + MTO_StatsControll.GrowDelay;

        }

        GetItem GI = MoveToObject.GetComponent<GetItem>();

        if (GI != null && GI.item.Length > 0)
        {

            for (int i = 0; i < GI.item.Length; i++)
                pl.inv.AddItem(GI.item[i], GI.itemcount[i], itemDatabase.FindItem(GI.item[i]).Durability, pl._transform.position);

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
                DropObject(itemDatabase.FindItem(9998).ObjectPrefs);
                Const.AllTrash++;
            }
        }

        if (ItemsTheyDropAfterEating.Length > 0)
        {
            for (int i = 0; i < ItemsTheyDropAfterEating.Length; i++)
                if (!NoAudioOnItems)
                    pl.inv.DropItemInSameSpot(_transform.position, Stats.ItemCount, ItemsTheyDropAfterEating, itemDatabase.FindItem(ItemsTheyDropAfterEating[i]).Durability);
                else
                    pl.inv.DropItemInSameSpotNOAUDIO(_transform.position, Stats.ItemCount, ItemsTheyDropAfterEating, itemDatabase.FindItem(ItemsTheyDropAfterEating[i]).Durability);

        }



        if (pl.Payment > 0)
        {
            if (ItemsTheyPaysToPlayer.Length > 0)
            {
                for (int i = 0; i < ItemsTheyPaysToPlayer.Length; i++)
                    pl.inv.AddItemNOAUDIO(ItemsTheyPaysToPlayer[i], pl.Payment * MTO_PubObject.DishesOnTable[0].Cost, -1, transform.position);

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
           CarringDish = pl.inv.DeepCopyItem(MTO_GetItem.item[0], 1, itemDatabase.FindItem(MTO_GetItem.item[0]).Durability);

            
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
            MTO_StatsControll.HasCharacter = false;
            MTO_StatsControll.ReverseMoney = false;

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

    int CharacterDamage()
    {
        int basedamage = 1;


        if (itemDatabase.FindItem(Stats.DatabaseID).itemNames[0].Contains("Knight"))
            basedamage += pl.Knight_Damage_Boost;
        if (itemDatabase.FindItem(Stats.DatabaseID).itemNames[0].Contains("Guard"))
            basedamage += pl.Guard_Damage_Boost;
        if (itemDatabase.FindItem(Stats.DatabaseID).itemNames[0].Contains("Cleric"))
            basedamage += pl.Cleric_Damage_Boost;


        return basedamage;
    }
    void FadeToAnim(string n)
    {
        if (CurrentAnimName == n) return;
        AnimFade = Time.fixedTime+ 0.2f;
        CurrentAnimName = n;

    }

    private void OnDestroy()
    {
        if (MoveToObject != null)
            if (MTO_StatsControll != null) MTO_StatsControll.Occupied = false;
    }

    public void Death()
    {

        DeathTimer = Time.fixedTime + 2;

        FadeToAnim("Death");
    }

    void DeathManager()
    {
        if (DeathTimer > Time.fixedTime && DeathTimer < Time.fixedTime + 1.6f)
        {

            Const.BlowObject(Stats);
        }
    
    }

    void RemoveDishesOnTable(int i, int count)
    {
        if (MTO_PubObject.DishesOnTable.Count <= 0) return;

        if (MTO_PubObject.DishesOnTable[i].Count > 1)
            MTO_PubObject.DishesOnTable[i].Count-= count;
        else
            MTO_PubObject.DishesOnTable.RemoveAt(i);
        
    }

    void SetOccupation(GameObject stats, bool occupation)
    {
        stats.GetComponent<StatsControll>().Occupied = occupation;


    }
    void DropObject(GameObject obj)
    {
        GameObject poop = Instantiate<GameObject>(obj);
        poop.transform.position = _transform.position;
    }
    
}
