using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScanForCirclesInTiles : MonoBehaviour
{
    public Tilemap Base;
    private List<Vector2Int> ScanXList = new List<Vector2Int>();
    private List<Vector2Int> PositionsToScan = new List<Vector2Int>();

    private List<Vector2Int> FillList = new List<Vector2Int>();

    public TileBase Brush;
    public TileBase BrushDraw;

    private Player pl;
    private AstarPath PF;

    private float Delay;

    void Start()
    {
        PF = GameObject.Find("PathFinding").GetComponent<AstarPath>();

        PositionsToScan.Add(new Vector2Int(-1, 0));
        PositionsToScan.Add(new Vector2Int(-1, -1));
        PositionsToScan.Add(new Vector2Int(0, -1));
        PositionsToScan.Add(new Vector2Int(0, 1));
        PositionsToScan.Add(new Vector2Int(1, 1));
        PositionsToScan.Add(new Vector2Int(1, 0));

        pl = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pl.GetComponent<Gun>().CurrentGunID == 409)
        {
            if (Base.GetTile(new Vector3Int(Base.WorldToCell(transform.position).x, Base.WorldToCell(transform.position).y, 0)) == null)
            {
                Base.SetTile(new Vector3Int(Base.WorldToCell(transform.position).x, Base.WorldToCell(transform.position).y, 0), BrushDraw);

                if (!ScanXList.Contains(new Vector2Int(Base.WorldToCell(transform.position).x, Base.WorldToCell(transform.position).y)))
                {
                    ScanXList.Add(new Vector2Int(Base.WorldToCell(transform.position).x, Base.WorldToCell(transform.position).y));
                    Delay++;
                }
            }
        }

      /*  if (pl.IM.enter_b)
        {
           

            for (int i = 0; i < ScanXList.Count; i++)
            {
                FillCircle();
              
            }



           
            Delay = 15;
        }
       */
    }


    private void LateUpdate()
    {
        if (Delay >= 15)
        {
            PF.Scan();
            Delay = 0;
        }
    }


    void Scan(int Y, int X)
    {
     

        for (int i=0; i< PositionsToScan.Count; i++)
        {

            int XX = X + PositionsToScan[i].x;
            int YY = Y + PositionsToScan[i].y;

            if (Base.GetTile(new Vector3Int(XX, YY, 0)) != null )
                {
                
                //   if (ScanXList.Contains(new Vector2Int(XX, YY))) FillCircle();

                if (!ScanXList.Contains(new Vector2Int(XX, YY)))
                {
                    
                    if (!Base.GetTile(new Vector3Int(XX, YY, 0)) != Brush)
                    {
                       
                        ScanXList.Add(new Vector2Int(XX, YY));

                    }
                }
                else
                {
                    
                    FillCircle();
                }
                   



            }
                
                
        }

    }

    
    


    void FillCircle()
    {
        for (int i = 0; i < ScanXList.Count-1; i++)
        {
            for (int ii = 0; ii < ScanXList.Count; ii++)
            {
                if (ScanXList[i].y == ScanXList[ii].y)
                {
                    int startX = ScanXList[i].x;
                    int EndX = ScanXList[i].x;
                    if (ScanXList[i].x < ScanXList[ii].x)
                    {
                        startX = ScanXList[i].x;
                        EndX = ScanXList[ii].x;
                    }
                    else if (ScanXList[i].x > ScanXList[ii].x)
                    {
                        startX = ScanXList[ii].x;
                        EndX = ScanXList[i].x;
                    }
                    for (int x = startX; x < ScanXList[ii].x; x++)
                    {
                        if (Base.GetTile(new Vector3Int(x, ScanXList[i].y, 0)) == null)
                            Base.SetTile(new Vector3Int(x, ScanXList[i].y, 0), Brush);

                    }


                }
            }
        }

        
    }
}
