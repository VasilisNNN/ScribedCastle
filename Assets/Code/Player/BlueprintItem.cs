using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[System.Serializable]

public class BlueprintItem
{
    public string[] itemNames;
    public string[] itemDesc;
    public int itemID =-1;
    public int OriginalitemID = -1;

    public int Cost;
    public int BuildingCost;

    public float Peasants_CollectMoney_Timer_Boost;
    public int Peasants_CollectMoney_Amount_Boost;

    public float Buildings_CollectMoney_Timer_Boost;
    public int Buildings_CollectMoney_Amount_Boost;



    public int Peasant_HP_Boost;
    public int Knight_HP_Boost;
    public int Guard_HP_Boost;
    public int Cleric_HP_Boost;


    public int Knight_Damage_Boost;
    public int Guard_Damage_Boost;
    public int Cleric_Damage_Boost;

    public int Blueprints_Money_Boost;
    public int Progression = -1;
    public int Ending = -1;


    public BlueprintItem(int id, int originalid, string[] itemnames,  string[] itemdesc, int cost)
    {
        itemNames = itemnames;
        itemID = id;
        OriginalitemID = originalid;
        itemDesc = itemdesc;
        Cost = cost;

    }

 
   
    
    public BlueprintItem()
	{
		itemID = -1;
        
    }
    
}
