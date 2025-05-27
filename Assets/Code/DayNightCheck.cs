using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCheck : MonoBehaviour
{
    private Player pl;
    public List<DayAndNight.DayCycle> DayC = new List<DayAndNight.DayCycle>();
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
    }
    
    void Update()
    {
        if (!DayC.Contains(pl.DayNight.Day_Cycle))
        {
            if (pl.AttackingEnemies.Contains(gameObject))
                pl.AttackingEnemies.Remove(gameObject);


            if (GetComponent<Tail>() != null)
            {
                for (int j = 0; j < GetComponent<Tail>().TailObjs.Count; j++)
                    Destroy(GetComponent<Tail>().TailObjs[j]);

            }

            if (gameObject.GetComponent<StatsControll>() != null)
            {
                gameObject.GetComponent<StatsControll>().HP = 0;
            }
            else Destroy(gameObject);
        }
    }
}
