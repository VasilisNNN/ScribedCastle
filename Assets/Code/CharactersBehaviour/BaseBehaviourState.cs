using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseBehaviourState : MonoBehaviour
{
    public bool SelfDestroy;
    public bool ItemPicker;
    public bool GoingBack = true;
    public bool IgnoreOccupation;

    public GameObject[] TargetGameObjects;

    public Item CarringDish { get; set; }
    public float AttackCooldown { get; set; }

    public GameObject MoveToObject { get; private set; }
    public StatsControll MTO_StatsControll;
    public PubObject MTO_PubObject;
    public GetItem MTO_GetItem;



    public Animator Anim;
    public string CurrentAnimName;
    public float AnimFade;
    public bool ActionOnPointComlete;
    public float OnPointAnimationTimer;


    public bool DropItemsOnTarget;
    public bool KeepItemForThemselves;
    public bool DestroyTarget;


    public float OnTheTableWaiting, PoisonTimer, SearchForTargetsTimer;

    public StatsControll Stats;

    public GameObject ObjectOfOccupation;
    public Transform _transform;
    public CharacterPath CM;

    private Vector2 maxpos = new Vector2(2, 2);

    public ItemDatabase itemDatabase;
    public Constructor Const;
    public Player pl;
    public bool BringsPickedItemToThePlayer;
    public bool NoAudioOnItems;

    public float mindistanceToTarget = 0.33f;
    private bool StartIgnoreOccupation;
    // Start is called before the first frame update
    void Init()
    {
        itemDatabase = InitializeObjects.Itemdatabase;

        pl = InitializeObjects.PL;
        Const = InitializeObjects.Constr;

        Anim = GetComponent<Animator>();

        CM = GetComponent<CharacterPath>();
        _transform = transform;

        Stats = GetComponent<StatsControll>();

    }

    public abstract void MainUpdate();

   


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

    public void SetOnPointToDefault()
    {
        OnPointAnimationTimer = -1;
        FadeToAnim("Start");
        ObjectOfOccupation = null;
        ActionOnPointComlete = false;

    }


    public void FadeToAnim(string n)
    {
        if (CurrentAnimName == n) return;
        AnimFade = Time.fixedTime + 0.2f;
        CurrentAnimName = n;

    }

    public void Animations()
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

    public int CharacterDamage()
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

    public void CharacterBringsItemToUs()
    {
        //Character brings item to us

        if ((Stats.CanBeHungry && Stats.Satiety <= 0) || MoveToObject == null || !BringsPickedItemToThePlayer)
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

    void SetOccupation(GameObject stats, bool occupation)
    {
        stats.GetComponent<StatsControll>().Occupied = occupation;


    }

   public void ItemProduction()
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
    public void AddDishesOnTable(Item additem)
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


    public void SearchForTargets(GameObject Target, int MinGrowState)
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

            CheckAndSetTheTarget(i, id, Target, ref MoveToOB);

        }

        if (MoveToObject == null && Target.GetComponent<MovementControll>() != null)
        {
            IgnoreOccupation = true;

            for (int i = 0; i < Const.OBOnBoardTargets[id].Count; i++)
            {

                CheckAndSetTheTarget(i, id, Target, ref MoveToOB);

            }

        }

        SearchForTargetsTimer = Random.Range(3, 5);
    }


    void CheckAndSetTheTarget(int i, int id, GameObject Target, ref GameObject MoveToOB)
    {

        if (!CheckTheTarget(i, id, Target)) return;


        for (int j = 0; j < Const.OBOnBoardTargets[id].Count; j++)
        {
            if (j >= Const.OBOnBoardTargets[id].Count)
            {
                j--;
                continue;
            }


            if (Const.OBOnBoardTargets[id].Count <= 0) return;

            if (CheckTheTarget(j, id, Target))
            {
                if (j >= Const.OBOnBoardTargets[id].Count) return;

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


    bool CheckTheTarget(int i, int id, GameObject Target)
    {
        if (i >= Const.OBOnBoardTargets[id].Count) return false;


        if (Const.OBOnBoardTargets[id][i].Object == null)
            Const.OBOnBoardTargets[id].RemoveAt(i);

        if (i > Const.OBOnBoardTargets[id].Count)
            i = Const.OBOnBoardTargets[id].Count - 1;

        // int TargetID = Target.GetComponent<StatsControll>().DatabaseID;


        if (Const.OBOnBoardTargets[id].Count <= 0)
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

        if (Const.OBOnBoardTargets[id][i].Object.GetComponent<StatsControll>().Occupied && !IgnoreOccupation)
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
            if (Vector2.Distance(Const.OBOnBoard[i].Object.transform.position, transform.position) <= 0.1f)
                return false;
        }

        return true;
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

    /* void CharacterEats()
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

     }*/



}
