using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ObjectOnBoard
{
    public Vector2 Place;
    public string Name;
    public int ID;
    public GameObject Object;
    public StatsControll Stats;
    public CharacterMove CharMove;
    public PubObject PO;
    public Vector2 BaseObjectPosition = new Vector2(-999, -999);
    public bool hasParrent;
    public int orderinParrent;

    public TileBase _TileBase;
    /*public PubObject _PubObject;
    public StatsControll _StatsControll;
    public CharacterMove _CharacterMove;
    public MovementControll _MovementControll;
    */


    public ObjectOnBoard(int id, Vector2 place, string name, GameObject _object, StatsControll stats, PubObject po)
    {
        ID = id;
        Place = place;
        Name = name;
        Object = _object;
        Stats = stats;
        PO = po;

        if (_object != null)
        {
            if (_object.GetComponent<CharacterMove>() != null) CharMove = _object.GetComponent<CharacterMove>();
        }
    }

    public ObjectOnBoard(int id, Vector2 place, string name)
    {
        ID = id;
        Place = place;
        Name = name;
    }
}
