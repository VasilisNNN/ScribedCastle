using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[System.Serializable]

public class Item {
    public string[] itemNames;
    public string[] itemDesc;
    public int itemID =-1;
    public int OriginalitemID = -1;

    public int Cost;
    public int BuildingCost;

    public enum type {weapon , item};
    public type _type;

    public enum Soundtype { regular, sword, club, axe, pistol, shotgun, rifle, fakegun};
    public Soundtype _Soundtype;

    public int Count;

    public int DamageAmount;
    public int BulletDamageAmount;
    public int MaxHP = 0;
    public int HP = 0;
    public int Intellect = 0;
    public int Speed;
    public int DashDuration;
    public int Vision;
    public int Plague = 0;

    public int Stamina;
    public int StaminaUse;
    public int StaminaRecoverySpeed;

    public Slot.bodypart[] _bodypart;
    public bool Alcohol;
    public bool Food;
    public bool Dish;
    public bool Drink;
    public bool Toast;


    public string MagicObjectToCast;
    public string MagicEffectToCast;

    public int MagicDamage;
    public int FireDamage;
    public int IceDamage;
    public int MechanicDamage;

    public int MagicDefense;
    public int NatureDefense;
    public int FireDefense;

    public bool CanStack = false;
    public bool Poop = false;
    public bool CantBeSold = false;
    public bool Structure;
    public enum StructureType {Building, Tiles, Farms, Decoration, Protection };
    public StructureType _StructureType;

    public bool Character;
    public GameObject ObjectPrefs;

    public GameObject[] ObjectPrefsBottom = new GameObject[0];
    public GameObject[] ObjectPrefsMid = new GameObject[0];
    public GameObject[] ObjectPrefsTop = new GameObject[0];

    public TileBase[] TargetBrush = new TileBase[0];
    public Tilemap TargetTileMap;

    public int Satiety;
    public int[] NeedItemsIDs;
    public int[] NeedItemsCounts;

    public bool Mutation;
    public int Heat;
    public int Height;
    public int Sniff;

    public float GunLength = 0.457f;
    public int Durability = 21;

    public bool CanDig;

    public int AddSlots = 0;

    public bool CanBeDropped = true;
    public bool CanNOTBeRemovedFromTheBody = false;

    public int LootItem = 0;
    public int Payment = 0;
    public bool Gun;
    public bool Locked = false;

    public Item(int id, int originalid, type __type, string[] itemnames,  string[] itemdesc, int cost)
    {
        itemNames = itemnames;
        itemID = id;
        OriginalitemID = originalid;
        itemDesc = itemdesc;
        Cost = cost;
        _type = __type;
    }

 
   
    
    public Item()
	{
		itemID = -1;
        
    }
    
}
