using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemToUnlock
{
    public int ID;
    public int DayNumber;
    public string LocationName;

    public ItemToUnlock(int id, int day, string location)
    {
        ID = id;
        DayNumber = day;
        LocationName = location;
    }

}



public class UnlockItems : MonoBehaviour
{


    private SaveLoad SL;
    private Inventory inv;
    private Player pl;

    private GameObject UnlockAnimation;
    public List<ItemToUnlock> _ItemToUnlock = new List<ItemToUnlock>();


    void Start()
    {

        _ItemToUnlock.Add(new ItemToUnlock(405, 1,"Winter"));
        _ItemToUnlock.Add(new ItemToUnlock(406, 2, "Winter"));
        _ItemToUnlock.Add(new ItemToUnlock(407, 3, "Winter"));

        _ItemToUnlock.Add(new ItemToUnlock(408, 1, "Blood"));
        _ItemToUnlock.Add(new ItemToUnlock(409, 2, "Blood"));
        _ItemToUnlock.Add(new ItemToUnlock(410, 3, "Blood"));

        _ItemToUnlock.Add(new ItemToUnlock(422, 0, "Boss rush"));

        _ItemToUnlock.Add(new ItemToUnlock(2050, 10, "Main location"));
        _ItemToUnlock.Add(new ItemToUnlock(2051, 12, "Main location"));
        _ItemToUnlock.Add(new ItemToUnlock(2052, 15, "Main location"));
        _ItemToUnlock.Add(new ItemToUnlock(2053, 20, "Main location"));


        SL = GameObject.Find("Constructor").GetComponent<SaveLoad>();
        pl = InitializeObjects.PL;
        inv = pl.inv;
     

        UnlockAnimation = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/UnlockAnimation"),GameObject.Find("Canvas").transform);
       
    }


    void Update()
    {
        if (pl.StartLoading) return;
        _ItemToUnlock.ForEach(unlock => { if(SL.DayNumber >= unlock.DayNumber && SceneManager.GetActiveScene().name == unlock.LocationName) Unlock(unlock.ID); });   
        
    }

    void Unlock(int ID)
    {
        if (pl.StartLoading || !inv.GetItemInDatabase(ID).Locked || SL.Unlocked_IDs.Contains(ID)) return;

        TriggerAnimation(ID);
        SL.Unlocked_IDs.Add(ID);
        inv.GetItemInDatabase(ID).Locked = false;
        
        pl.menu.CurrentSlotNumber = 6;
       
        pl.menu.CurrentSlotLocations[pl.menu.CurrentSlotNumber] = SceneManager.GetActiveScene().name;
        
        SL.Save(true);


    }


    void TriggerAnimation(int ID)
    {
        UnlockAnimation.transform.Find("UnlockAnimation_Item").GetComponent<Image>().sprite =
        Resources.Load<Sprite>("Sprites/Items/" + inv.GetItemInDatabase(ID).itemNames[0]);

        UnlockAnimation.GetComponent<Animator>().Play("MainAnimation", 0);
    }



}
