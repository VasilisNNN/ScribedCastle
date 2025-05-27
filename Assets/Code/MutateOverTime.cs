using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutateOverTime : MonoBehaviour
{
    public int ID = -1;
    private float Timer;
    private Inventory inv;
    private DayAndNight DayNight;

    private int NumberOfDays = 1;
    private void Start()
    {
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        DayNight = GameObject.Find("Player").GetComponent<Player>().DayNight;

        Timer = Time.fixedTime + DayNight.DayLength* NumberOfDays;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Timer < Time.fixedTime)
        {
            GameObject O = Instantiate(inv.GetItemInDatabase(ID).ObjectPrefs);
            O.transform.position = transform.position;
            Destroy(gameObject);

        }
    }
}
