using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{

    private Tilemap Tile;
    public Tilemap Floor;
    public Tilemap Wall;

    private TileBase TileBase;

    public Constructor _constr;

    private List<Vector3Int> EmptyPositions = new List<Vector3Int>();

    private float Timer;
    private void Awake()
    {
        Tile = GetComponent<Tilemap>();
        TileBase = Resources.Load<TileBase>("Brushes/Floor2");


    }
    void Start()
    {

   
        OnLoad();

       
    }

    // Update is called once per frame
    void Update()
    {

        if (Timer < Time.fixedTime)
        {
            for (int i = 0; i < _constr.OBOnBoard.Count; i++)
            {
                SetTileArea(_constr.OBOnBoard[i].Object);


            }

          

            Timer = Time.fixedTime + 2;
        }


    }


    bool CheckOverlap(Vector3Int pos)
    {
        bool result = false;
        for (int i = 0; i < _constr.OBOnBoard.Count; i++)
        {
            for (int x = -3; x < 3; x++)
            {
                for (int y = -3; y < 3; y++)
                {
                    if (Tile.WorldToCell(_constr.OBOnBoard[i].Object.transform.position) + new Vector3Int(x, y, 0) == pos)
                    result = true;

                    
                }
            }


        }
        return result;
    }




    void SetTileArea(GameObject obj)
    {
        
        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
            {

                if (x < -3 || x > 3 || y < -3 || y > 3)
                {
                    if (!CheckOverlap(Tile.WorldToCell(obj.transform.position) + new Vector3Int(x, y, 0)))
                    {
                        for (int i = 0; i < EmptyPositions.Count; i++)
                        {
                            if (EmptyPositions[i] == (Tile.WorldToCell(obj.transform.position) + new Vector3Int(x, y, 0)))
                            {
                                Tile.SetTile((Tile.WorldToCell(obj.transform.position) + new Vector3Int(x, y, 0)), TileBase);
                                EmptyPositions.RemoveAt(i);
                            }

                        }
                    }

                }
            }
        }


        for (int x = -3; x < 3; x++)
        {
            for (int y = -3; y < 3; y++)
            {
                if (!EmptyPositions.Contains(Tile.WorldToCell(obj.transform.position) + new Vector3Int(x, y, 0)))
                {
                    Tile.SetTile((Tile.WorldToCell(obj.transform.position) + new Vector3Int(x, y, 0)), null);

                    EmptyPositions.Add(Tile.WorldToCell(obj.transform.position) + new Vector3Int(x, y, 0));
                }
               
            }
        }

    }

    public void OnLoad()
    {
        for (int x = -50; x < 50; x++)
        {
            for (int y = -50; y < 50; y++)
            {
                if (Floor.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    for (int yy = y - 3; yy < y + 3; yy++)
                    {
                        for (int xx = x - 3; xx < x + 3; xx++)
                        {
                            Tile.SetTile(new Vector3Int(xx, yy, 0), null);
                        }
                    }
                }


                if (Wall.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    for (int yy = y - 3; yy < y + 3; yy++)
                    {
                        for (int xx = x - 3; xx < x + 3; xx++)
                        {
                            Tile.SetTile(new Vector3Int(xx, yy, 0), null);
                        }
                    }
                }

            }

        }
    }
}
