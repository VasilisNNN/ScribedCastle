using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerateMoney : MonoBehaviour
{
    private Constructor Constr;
    private Player pl;
    private Inventory inv;

    private Animator Anim;
    private GameObject MouseOB;

    public DayAndNight.DayCycle[] DayToAct;

    private bool IsActive;
    private float Delay;
    public int Money = 1;
    public int MoneyGrowSide  = 1;
    
    private StatsControll Stats;
    public bool IgnoreOccupation;
    public float MinDelay = 10;
    void Start()
    {
        Stats = GetComponent<StatsControll>();
        pl = InitializeObjects.PL;
        inv = pl.inv;
        Constr = InitializeObjects.Constr;
      
       
        Anim = GetComponent<Animator>();
        Delay = MinDelay;
    }

    void Update()
    {
        if (pl.Pause()) return;

        Delay -= Time.deltaTime ;

        if (Anim != null)
        {
            Anim.SetInteger("Day", pl.DayNight.Day_Cycle.GetHashCode());
            

        }

        if (CheckDayCycle())
        {
          IsActive = true;
        }else
          IsActive = false;

        if (IsActive)
        {
            AddMoney();
        }
    }

    void AddMoney()
    {

        if (pl.Pause()) return;
        if ((!Stats.Occupied || !Stats.HasCharacter) && !IgnoreOccupation) return;

        if (Stats.ReverseMoney) MoneyGrowSide = -1;
        else MoneyGrowSide = 1;


        if (Delay > 0) return;
        inv.AddItemNOAUDIO(9, (Money + MoneyBoost()) * MoneyGrowSide, 99,transform.position);
        Delay = Mathf.Clamp(MinDelay - DelayBoost(),1,9999999999999);


    }
    
    int MoneyBoost()
    {
        if (MoneyGrowSide <= 0) return 0;

        int moneyboost = 0;
        if (!IgnoreOccupation)
            moneyboost = pl.Peasants_CollectMoney_Amount_Boost;
        else
            moneyboost = pl.Buildings_CollectMoney_Amount_Boost;

        return moneyboost;
    }

    float DelayBoost()
    {
        if (MoneyGrowSide <= 0) return 0;

        float mindelay = 0;
        if (!IgnoreOccupation)
            mindelay = pl.Peasants_CollectMoney_Timer_Boost;
        else
            mindelay = pl.Buildings_CollectMoney_Timer_Boost;

        return mindelay;
    }
    bool CheckDayCycle()
    {
        bool result = false;
        for (int i = 0; i < DayToAct.Length; i++)
        {
            if (pl.DayNight.Day_Cycle == DayToAct[i]) return true;

        }


        return result;
    }
}
