using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMoney : MonoBehaviour
{
    private Constructor constr;
    private Player pl;
    private Inventory inv;

    private Animator Anim;
    private GameObject MouseOB;

    public DayAndNight.DayCycle[] DayToAct;

    private bool IsActive;
    private float Delay;
    public int Money = 1;

    public bool OnOccupation;
    void Start()
    {
        MouseOB = GameObject.Find("MouseOB");

        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();

        inv = GameObject.Find("Player").GetComponent<Inventory>();

        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        
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
        if (OnOccupation)


       
        if (!GetComponent<StatsControll>().Occupied || !GetComponent<StatsControll>().HasAChracter) return;

        if (Delay > Time.fixedTime) return;
        inv.AddItemNOAUDIO(9, Money, 99,transform.position);
        Delay = Time.fixedTime + 10;


    }

    bool CheckDayCycle()
    {
        bool result = false;
        for (int i = 0; i < DayToAct.Length; i++)
        {
            if (pl.DayNight.Day_Cycle == DayToAct[i]) result = true;

        }
        return result;
    }
}
