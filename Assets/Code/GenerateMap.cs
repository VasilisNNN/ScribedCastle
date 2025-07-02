using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Pathfinding;
using NUnit;


[System.Serializable]
public class DarkTiles
{
    public Tilemap TargetMap;
    public Tilemap ThisMap;
    public TileBase ThisBrush;
}


[System.Serializable]
public class ObjectToGenerate
{
    public GameObject obj;
    public enum cardinaldirections { north,south, east,west, center };
    public cardinaldirections direction;
    public int approximatenumber = 10;

    public int CreatedAmount;

}

[System.Serializable]
public class ObjectWithStableGround
{
    public GameObject obj;
    public bool RNDPOS;
    public bool DrawRoad = true;
    public int GroundArea = 10;

}


[System.Serializable]
public class PlacedObject
{
    public Vector2 Place;
    public string Name;
    public int ID;
    public GameObject Object;

    public bool PositionOccupied;


    public PlacedObject(int id, Vector2 place, string name, GameObject _object)
    {
        ID = id;
        Place = place;
        Name = name;
        Object = _object;
    }
}


public class GenerateMap : MonoBehaviour
{
    public Tilemap Map;
    public Tilemap WaterMap;
    public Tilemap GrassMap;
    public Tilemap SandMap;
    public Tilemap GrassDarkMap;
    public Tilemap FloorToBuild;
    public Tilemap GroundRoad;

    public Tilemap LeavesLight;
    public Tilemap LeavesDark;
    public Tilemap DirtMap;
    public Tilemap StoneMap;

    public Tilemap[] ClearMaps;

    public TileBase tiles;
    public TileBase Watertile;
    public TileBase SandTile;
    public TileBase woodfloor;
    public TileBase dirtfloor;
    public TileBase roadfloor;
    public TileBase leavsfloor;
    public TileBase stonefloor;
    public TileBase grassfloor;

    public DarkTiles[] DTiles;

    public ObjectToGenerate[] ObjectsToSpawn;
    public ObjectWithStableGround[] ObjectsWithStableGround;
    public List<Vector3> ObjectsWithStableGround_positions = new List<Vector3>();
    public List<int> ObjectsWithStableGround_areas = new List<int>();
    private ObjectWithStableGround[] ObjectsWithStableGround_Buffer;

    public List<Vector3> LastPoints = new List<Vector3>();

    public List<GameObject> CreatedObjects = new List<GameObject>();

    public List<List<PlacedObject>> PlacedObjectsList = new List<List<PlacedObject>>();

    public int mapSize = 10;     // Size of the map in tiles
    public int tileSize = 64;    // Size of each tile in pixels
    public float noiseScale = 0.1f;  // Scale of the Perlin noise
    public float noiseThreshold = 0.5f;  // Threshold for determining the type of tile based on the noise value

    public float treenoiseScale = 0.1f;

    private Player pl;
    private Constructor Constr;


    private AstarPath AP;
    private List<Seeker> seekers = new List<Seeker>();
    private List<Path> paths = new List<Path>();

    private List<int> loadparts = new List<int>();

    private bool[] pathcreated;

    public List<int> FloorStates = new List<int>();
    public Vector2Int RNDSHIFT;
    private List<Vector3> OccupiedPlaces = new List<Vector3>();


    private GameObject FadeIn;
    private List<GameObject> BufferObjects = new List<GameObject>();

    private float LoadTimer;
 
    
    private int tileIndex;
    private bool thebuildingisdone;


    private List<Vector3> RNDPositions = new List<Vector3>();

    public int RNDSTART_Y, RNDSTART_X;

 
    private List<int> RoadDrawTilesMax = new List<int>();
    private float DrawRoadDuration;
    private int DrawingRoadNum;
    private float DrawRoadTimer;

    private int CreatingObjectsNum;
    private float CreatingObjectsTimer;

    public bool CreateObjects { get; set; }
    public int RND_Stable_Pos;

    public float border =5;
    private Vector2 CreatedBorders_Min, CreatedBorders_Max;


    private System.Random random;
    public int ObjectPlacement_seed { get; set; }
    public int ObjectPlacement_seed_Start { get; set; }


    private void Awake()
    {
        Map.transform.position = new Vector3(0, 0, 0.0011f);
        WaterMap.transform.position = new Vector3(0, 0, 0.0012f);
        GrassMap.transform.position = new Vector3(0, 0, 0.0013f);
        SandMap.transform.position = new Vector3(0, 0, 0.0014f);
        GrassDarkMap.transform.position = new Vector3(0, 0, 0.0015f);
        FloorToBuild.transform.position = new Vector3(0, 0, 0.0016f);
        GroundRoad.transform.position = new Vector3(0, 0, 0.0017f);
       

       ObjectsWithStableGround_Buffer = ObjectsWithStableGround;

        for (int i = 0; i < ObjectsWithStableGround.Length; i++)
        {
            ObjectsWithStableGround_positions.Add(ObjectsWithStableGround[i].obj.transform.position);
            ObjectsWithStableGround_areas.Add(ObjectsWithStableGround[i].GroundArea);
        }
    }

    void Start()
    {

        border = 5;


        CreateObjects = true;

         FadeIn = GameObject.Find("FadeIn");

        pathcreated = new bool[ObjectsWithStableGround_Buffer.Length];


        if (GetComponent<Seeker>() == null) gameObject.AddComponent<Seeker>();

        
        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length; i++)
        {
      
                BufferObjects.Add(new GameObject());
            BufferObjects[BufferObjects.Count - 1].name = "BufferObjects " + i;
            BufferObjects[BufferObjects.Count - 1].AddComponent<Seeker>();
            

            loadparts.Add(0);
           
        }

        for (int i = 0; i < ObjectsToSpawn.Length; i++)
        {
            loadparts.Add(0);
        }
        
        for (int i = 0; i < BufferObjects.Count; i++)
        {
            seekers.Add(BufferObjects[i].GetComponent<Seeker>());
            RoadDrawTilesMax.Add(15);
        }


        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length; i++)
            LastPoints.Add(ObjectsWithStableGround_positions[i]);


        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;
        
        AP = GameObject.Find("PathFinding").GetComponent<AstarPath>();
       
        pl.StartLoading = true;

        RND_Stable_Pos = UnityEngine.Random.Range(0, ObjectsWithStableGround_Buffer.Length);

        int rndp = 0;

        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length + 1; i++)
        {

            if (ObjectsWithStableGround_Buffer[RND_Stable_Pos + rndp].RNDPOS)
            {
                RNDPositions.Add(ObjectsWithStableGround_positions[RND_Stable_Pos + rndp]);

            }

            if (RND_Stable_Pos + rndp < ObjectsWithStableGround_Buffer.Length - 1) rndp++;
            else rndp = -RND_Stable_Pos;

        }

     

        CreatedBorders_Min = new Vector2(pl.transform.position.x - border, pl.transform.position.y - border);
        CreatedBorders_Max = new Vector2(pl.transform.position.x + border, pl.transform.position.y + border);

    }

    private void Update()
    {

        
        
            if ((pl.FadeInDelay - 0.1f < Time.fixedTime && pl.FadeInDelay > Time.fixedTime) || !pl.StartLoading)
            {
                pl.StartLoading = false;

           

            if (FadeIn != null)
                FadeIn.GetComponent<Animator>().SetBool("Start", true);
            }

        if (FadeIn != null)
            FadeIn.transform.Find("FG").Find("Scrollbar").GetComponent<Scrollbar>().size = (float)loadparts.Sum() / (float)loadparts.Count;

      

        MainMapGen();

        
        PathsAndRoads();


     //   PlacingObjects();
    }
    void MainMapGen()
    {
        if (pl.PathRescan > 0) return;

        if (pl.FadeInDelay > Time.fixedTime) return;

        if (pl.StartLoading)
            StopAPath();

        if (!pl.StartLoading)
        {
         
        
            return;

           
        }
        if (loadparts.Sum() < seekers.Count)
        {

            if (LoadTimer <= 0)
                SeekForAPath(pl.transform.position);

            LoadTimer += Time.deltaTime;

            return;
        }
        
        LoadTimer = 0;


        if (CreatingObjectsTimer < Time.fixedTime)
        {
            print("CreatingObjectsTimer < Time.fixedTime");

            if (ObjectsToSpawn.Length > 0)
                CreateGroupOfObjects(Constr.GreyMap, ObjectsToSpawn[CreatingObjectsNum], ObjectsToSpawn[CreatingObjectsNum].direction, out thebuildingisdone);


        }


        if (loadparts.Sum() < loadparts.Count)
            return;


        for (int x = -mapSize; x < mapSize; x++)

        for (int y = -mapSize; y < mapSize; y++)
        {
            Vector3Int tilePos = new Vector3Int(x, y, 0);  // Calculate the position of the tile

            CreateSubTiles(tilePos);



        }



        for (int x = -mapSize; x < mapSize; x++)
        for (int y = -mapSize; y < mapSize; y++)
        {
            Vector3Int tilePos = new Vector3Int(x, y, 0);

            CreateSubTiles2(tilePos);
        }


        pl.menu.SL.ResetPolygonColliders();

       
        pl.FadeInDelay = Time.fixedTime + 2f;

        int sc = seekers.Count;
        print("Destroy seekers");
        for (int i = 0; i < sc; i++)
            Destroy(seekers[0]);

        for (int i = 0; i < BufferObjects.Count; i++)
        {
            Destroy(BufferObjects[i]);
        }


        BufferObjects = new List<GameObject>();

        pl.LayerSort.ResetFlippingObjects();
  


    }


    void PathsAndRoads()
    {
        if (loadparts.Sum() >= seekers.Count)
            return;

     
        if (!pl.StartLoading || LoadTimer <= 0.5f)
        {
            return;

        }
       
        float FinalPathX = 0;
        float FinalPathY = 0;

      
        for (int o = 0; o < seekers.Count; o++)
        {
            SetNewMapTileAndRescan(o, ref FinalPathX, ref FinalPathY);

            if (!ObjectsWithStableGround[o].DrawRoad) loadparts[o] = 1;
            


            if (Mathf.Abs(FinalPathX - ObjectsWithStableGround_positions[o].x) <= 1 &&
            Mathf.Abs(FinalPathY - ObjectsWithStableGround_positions[o].y) <= 1)
            {
              
                DrawingRoadNum = o;
                DrawingRoadTiles();

            }



        }


    


    }





    void DrawingRoadTiles()
    {
      

        if (DrawRoadTimer > Time.fixedTime)
            return;
  
        
        if (seekers[DrawingRoadNum].GetCurrentPath().vectorPath.Count <= 0)
            return;

        
        int numberdrawingtiles = 60;

  
        if (DrawRoadDuration > 50 )
        {
            RoadDrawTilesMax[DrawingRoadNum] = numberdrawingtiles;
            loadparts[DrawingRoadNum] = 1;
          
            DrawRoadDuration = 0;
            return;
        }

        if (RoadDrawTilesMax[DrawingRoadNum] > seekers[DrawingRoadNum].GetCurrentPath().vectorPath.Count-1 )
            RoadDrawTilesMax[DrawingRoadNum] = seekers[DrawingRoadNum].GetCurrentPath().vectorPath.Count-1;

        if (DrawingRoadNum > seekers.Count) return;
  
        int strt = RoadDrawTilesMax[DrawingRoadNum] - numberdrawingtiles;
        if (strt < 0) strt = 0;

        if (ObjectsWithStableGround_Buffer[DrawingRoadNum].DrawRoad)
        {
            for (int i = strt; i < RoadDrawTilesMax[DrawingRoadNum]; i++)
            {

                Vector3Int Pos = GroundRoad.WorldToCell(seekers[DrawingRoadNum].GetCurrentPath().vectorPath[i]);

                GroundRoad.SetTile(Pos, roadfloor);
                
            }

        }

      
        if (RoadDrawTilesMax[DrawingRoadNum] >= seekers[DrawingRoadNum].GetCurrentPath().vectorPath.Count - 1)
        {

            RoadDrawTilesMax[DrawingRoadNum] = numberdrawingtiles;
            loadparts[DrawingRoadNum] = 1;
           
            DrawRoadDuration = 0;
        }
        else
        {
            RoadDrawTilesMax[DrawingRoadNum] += numberdrawingtiles;
            DrawRoadDuration += Time.deltaTime;
        }

        if(DrawingRoadNum == seekers.Count-1)
        DrawRoadTimer = Time.fixedTime + 0.001f;
        
    }


    void SetNewMapTileAndRescan( int o, ref float FinalPathX, ref float FinalPathY)
    {
        if (seekers[o] == null) return;

   
        if (seekers[o].GetCurrentPath() == null)
        return;


    
        int c = 0;
        c = seekers[o].GetCurrentPath().path.Count - 1;
        if (c < 0) c = 0;


        if (seekers[o].GetCurrentPath().path.Count <= 0)
            return;

   
        if (seekers[o].GetCurrentPath().path[c] == null)
            return;
  
        if (seekers[o].GetCurrentPath().path.Count > 0)
        {
            FinalPathX = ((Vector3)seekers[o].GetCurrentPath().path[c].position).x;
            FinalPathY = ((Vector3)seekers[o].GetCurrentPath().path[c].position).y;
        }


        if (Mathf.Abs(FinalPathX - ObjectsWithStableGround_positions[o].x) <= 0.5 &&
            Mathf.Abs(FinalPathY - ObjectsWithStableGround_positions[o].y) <= 0.5)
            return;

       

        if (seekers[o].GetCurrentPath().vectorPath.Count > 0)
        {
            for (int x = -2; x < 3; x++)
            {
                for (int y = -2; y < 3; y++)
                {

                    Map.SetTile(GroundRoad.WorldToCell(seekers[o].GetCurrentPath().vectorPath[seekers[o].GetCurrentPath().vectorPath.Count - 1]) + new Vector3Int(x, y, 0), tiles);
                    WaterMap.SetTile(GroundRoad.WorldToCell(seekers[o].GetCurrentPath().vectorPath[seekers[o].GetCurrentPath().vectorPath.Count - 1]) + new Vector3Int(x, y, 0), null);

                   

            

                }
            }
        }




        if (pl.PathRescan <= 0)
        {
            pl.PathRescan = 0.06f;
        }

        if (seekers[o].IsDone())
            seekers[o].StartPath(pl._transform.position, ObjectsWithStableGround_positions[o], OnPathComplete);



    }






   public void CleanMap()
    {
        for (int x = -mapSize; x < mapSize; x++)
        {
            for (int y = -mapSize; y < mapSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);  // Calculate the position of the tile
                Map.SetTile(tilePos, null);
                WaterMap.SetTile(tilePos, null);
               // WaterMap.GetComponent<TilemapCollider2D>().usedByComposite = false;

                SandMap.SetTile(tilePos, null);
                GrassMap.SetTile(tilePos, null);
                GrassDarkMap.SetTile(tilePos, null);
                FloorToBuild.SetTile(tilePos, null);
                GroundRoad.SetTile(tilePos, null);

               
                    LeavesLight.SetTile(tilePos, null);
                    LeavesDark.SetTile(tilePos, null);
                


                DirtMap.SetTile(tilePos, null);
                StoneMap.SetTile(tilePos, null);


                for (int i = 0; i < ClearMaps.Length; i++)
                {

                    ClearMaps[i].SetTile(tilePos, null);
                  
                }

                for (int i = 0; i < DTiles.Length; i++)
                DTiles[i].ThisMap.SetTile(tilePos, null);

            }
        }

       
    }




    public void CreateNewMap()
    {
    
        CleanMap();

        SetRandomPositions();


        float noiseValue = 0;
        RNDSTART_Y = UnityEngine.Random.Range(-mapSize, mapSize);
        int RNDY = RNDSTART_Y;

        RNDSTART_X = UnityEngine.Random.Range(-mapSize, mapSize);
        int RNDX = RNDSTART_X;

        RNDSHIFT = new Vector2Int(RNDSTART_X, RNDSTART_Y);

        ObjectPlacement_seed_Start = UnityEngine.Random.Range(0, mapSize);
        ObjectPlacement_seed = ObjectPlacement_seed_Start;

        for (int x =-mapSize; x < mapSize; x++)
        {
            

            for (int y = -mapSize; y < mapSize; y++)
            {

                if (RNDY >= mapSize*2) RNDY = 0;

                noiseValue = Mathf.PerlinNoise(RNDX * noiseScale ,RNDY * noiseScale );  // Generate Perlin noise value for the current position
               
                RNDY++;
               
                tileIndex = Mathf.FloorToInt(noiseValue / noiseThreshold * 1);  // Calculate the tile index based on the noise value and the threshold
                Vector3Int tilePos = new Vector3Int(x, y, 0);  // Calculate the position of the tile
                FloorStates.Add(tileIndex);


                GanerateTiles(tilePos, tileIndex);
                

            }


            RNDX++;
            if (RNDX >= mapSize*2) RNDX = 0;

        }


        for (int x = -mapSize; x < mapSize; x++)

            for (int y = -mapSize; y < mapSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);  // Calculate the position of the tile

                CreateSubTiles(tilePos);



             }
        


        for (int x = -mapSize; x < mapSize; x++)

            for (int y = -mapSize; y < mapSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                CreateSubTiles2(tilePos);
            }
        




    }


    private void CreateGroupOfObjects(Tilemap TargetMap, ObjectToGenerate obj, ObjectToGenerate.cardinaldirections direction, out bool result)
    {

       
        List<Vector3> Spots = new List<Vector3>();

        int spotslastcount =0;


        Vector2 BorderX = new Vector2(0, 0);
        Vector2 BorderY = new Vector2(0, 0);

        result = false;

        if (direction == ObjectToGenerate.cardinaldirections.north)
        {
            BorderX = new Vector2(-mapSize, mapSize);
            BorderY = new Vector2(mapSize/6, mapSize );
        }

        else if (direction == ObjectToGenerate.cardinaldirections.south)
        {
            BorderX = new Vector2(-mapSize, mapSize);
            BorderY = new Vector2(-mapSize , -mapSize/6);
        }

        else if (direction == ObjectToGenerate.cardinaldirections.west)
        {
            BorderX = new Vector2(-mapSize, -mapSize / 6);
            BorderY = new Vector2(-mapSize, mapSize);
        }
        else if (direction == ObjectToGenerate.cardinaldirections.east)
        {
            BorderX = new Vector2(mapSize / 6, mapSize);
            BorderY = new Vector2(-mapSize, mapSize);
        }
        else if (direction == ObjectToGenerate.cardinaldirections.center)
        {
            BorderX = new Vector2(-mapSize / 3, mapSize / 3);
            BorderY = new Vector2(-mapSize / 3, mapSize / 3);
        }
        else
        {
            BorderX = new Vector2(-mapSize / 6, mapSize / 6);
            BorderY = new Vector2(-mapSize / 6, mapSize / 6);
        }
        int spawnrate = obj.approximatenumber;

        if (Spots.Count < spawnrate)
        {
            for (int x = -mapSize; x < mapSize; x++)
            {
                if (Spots.Count >= spawnrate) break;

                for (int y = -mapSize; y < mapSize; y++)
                {
                    if (x > BorderX.x && x < BorderX.y && y > BorderY.x && y < BorderY.y)
                    {
                        AddSpotForObject(TargetMap, x, y, ref Spots, 0.6f);

                        if (Spots.Count >= spawnrate) break;
                    }
                }
            }
            
        }


        if (Spots.Count < spawnrate)
        {

            
            for (int x = (int)BorderX.x; x < BorderX.y; x++)
            {

                if (Spots.Count >= spawnrate) break;

                for (int y = (int)BorderY.x; y < BorderY.y; y++)
                {

                    AddSpotForObject(TargetMap, x, y, ref Spots, 1);


                    if (Spots.Count >= spawnrate) break;
                }
            }


        }


        if (Spots.Count < spawnrate)
        {

            
            for (int x = -mapSize; x < mapSize; x++)
            {

                if (Spots.Count >= spawnrate) break;

                for (int y = -mapSize; y < mapSize; y++)
                {

                    AddSpotForObject(TargetMap, x, y, ref Spots, 1);


                    if (Spots.Count >= spawnrate) break;
                }
            }


        }
        // List<PlacedObject> PO = new List<PlacedObject>();

        for (int i = 0; i < Spots.Count; i++)
        {


            if (CreateObjects)
            {
                GameObject Tr = Instantiate(obj.obj);
                Tr.transform.position = Spots[i];

                CreatedObjects.Add(Tr);
                
              //  constr.ConstructedStructures.Add(new ObjectOnBoard(Tr.GetComponent<StatsControll>().DatabaseID, Tr.transform.position, Tr.name, Tr, Tr.GetComponent<StatsControll>(), Tr.GetComponent<PubObject>()));



                OccupiedPlaces.Add(Spots[i]);
                result = true;
            }


            obj.CreatedAmount++;

            spotslastcount = i;
            

            if (obj.CreatedAmount >= spawnrate)
            {
                loadparts[seekers.Count-1 + (CreatingObjectsNum+1)] = 1;

                if(CreatingObjectsNum< ObjectsToSpawn.Length-1)
                CreatingObjectsNum++;

                CreatingObjectsTimer = Time.fixedTime + 0.001f;
                break;
            }


        }



      // PlacedObjectsList.Add(PO);



    }

    void AddSpotForObject(Tilemap TargetMap, int x, int y, ref List<Vector3> Spots, float noisethreshold)
    {

        bool canbuild = true;
        Vector3Int PlaceSpot = new Vector3Int(x, y, 0);



        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length; i++)
        {

            if (Mathf.Abs(TargetMap.CellToWorld(PlaceSpot).x - ObjectsWithStableGround_positions[i].x) < 1 || Mathf.Abs(TargetMap.CellToWorld(PlaceSpot).y - ObjectsWithStableGround_positions[i].y) < 1)
            {

                canbuild = false;
            }

        }



        if (GroundRoad.GetTile(new Vector3Int(x, y)) != null)
        {
            for (int xx = -1; xx < 2; xx++)
                for (int yy = -1; yy < 2; yy++)
                {
                    if (GroundRoad.GetTile(new Vector3Int(x, y) + new Vector3Int(xx, yy, 0)) != null)
                    {

                        canbuild = false;
                    }
                }

        }

        if (OccupiedPlaces.Contains(TargetMap.CellToWorld(PlaceSpot) + new Vector3(0, 0.25f, 0))) canbuild = false;


        int rndindex = 0;


        float objValue = Mathf.PerlinNoise(x * 0.1f, y * 0.1f);
        rndindex = Mathf.FloorToInt(objValue / noisethreshold);

        ObjectPlacement_seed += 1;
       
        random = new System.Random(ObjectPlacement_seed);

        int r = GetRandomInt(0 , mapSize*2);

      //  print("rnd r " + r);

        rndindex += r;

        

        if (rndindex != 0) canbuild = false;
        

        if (canbuild && TargetMap.GetTile(new Vector3Int(x, y, 0)) != null && ((FloorToBuild.GetTile(new Vector3Int(x, y, 0)) == null && FloorToBuild!= TargetMap) || FloorToBuild == TargetMap))
        Spots.Add(TargetMap.CellToWorld(PlaceSpot) + new Vector3(0, 0.25f, 0));
        
    }



    void SeekForAPath(Vector3 PosStart)
    {
        for (int i = 0; i < seekers.Count; i++)
        {
         
            seekers[i].StartPath(PosStart, ObjectsWithStableGround_positions[i], OnPathComplete);
            

        }
    }


    void StopAPath()
    {

        for (int i = 0; i < seekers.Count; i++)
        {
            if(seekers[i]!=null)
            seekers[i].StopAllCoroutines();
        }

    }
    void OnPathComplete(Path p)
    {
        
        if (!p.error)
        {
           

            // if(p.vectorPath[p.vectorPath.Count-1].x-)


            //  seeker.StopAllCoroutines();

        }
    }
    void CreateSubTiles(Vector3Int tilePos)
    {

        float dirtThreshold = 0.3f;
        float mudnoiseValue = Mathf.PerlinNoise((tilePos.x+RNDSHIFT.x) * noiseScale*1.5f, (tilePos.y+ RNDSHIFT.y) * noiseScale * 1.5f);  // Generate Perlin noise value for the current position
        int dirtidex = Mathf.FloorToInt( mudnoiseValue / dirtThreshold);

        float stoneThreshold = 0.6f;
        float stonenoiseValue = Mathf.PerlinNoise((tilePos.x + RNDSHIFT.x*1.5f) * (noiseScale / 2), (tilePos.y + RNDSHIFT.y * 1.5f) * (noiseScale / 2));  // Generate Perlin noise value for the current position
        int stoneIndex = Mathf.FloorToInt(stonenoiseValue / stoneThreshold);


       

        List<Vector3Int> LastDirpPlace = new List<Vector3Int>();
        List<Vector3Int> LastGrassPlace = new List<Vector3Int>();


        if (dirtidex == 0 && stoneIndex!=0 )
        {
            for (int xx = -1; xx < 2; xx++)
            {
                for (int yy = -1; yy < 2; yy++)
                {
                    if (SandMap.GetTile(tilePos) == null && SandMap.GetTile(tilePos + new Vector3Int(xx, yy, 0)) == null && Map.GetTile(tilePos + new Vector3Int(xx, yy, 0)) != null)
                    {
                        DirtMap.SetTile(tilePos + new Vector3Int(xx, yy, 0), dirtfloor);
                        LastDirpPlace.Add(tilePos + new Vector3Int(xx, yy, 0));
                    }
                }
            }
        }

    
       

        
        pl.PathRescan = 1;

    }

    void CreateSubTiles2(Vector3Int tilePos)
    {


      
        float stonenoiseValue = Mathf.PerlinNoise((tilePos.x + RNDSHIFT.x * 1.5f) * (noiseScale / 2), (tilePos.y + RNDSHIFT.y * 1.5f) * (noiseScale / 2));  // Generate Perlin noise value for the current position
        int stoneIndex = Mathf.FloorToInt(stonenoiseValue / noiseThreshold * 3);


        
        List<Vector3Int> LastGrassPlace = new List<Vector3Int>();



        float grassnoiseThreshold = 0.4f;

        float grassnoiseScalse = noiseScale ;


        float grassnoiseValue = Mathf.PerlinNoise((tilePos.x + RNDSHIFT.x * 2.5f) * grassnoiseScalse, (tilePos.y + RNDSHIFT.y * 2.5f) * grassnoiseScalse);  // Generate Perlin noise value for the current position
        int grasstileIndex = Mathf.FloorToInt(grassnoiseValue / grassnoiseThreshold);


        if (Map.GetTile(tilePos) != null)
        {

            if (grasstileIndex == 0)
            {
                DirtMap.SetTile(tilePos, null);

                GrassMap.SetTile(tilePos, grassfloor);
                LastGrassPlace.Add(tilePos);



                if (Map.GetTile(tilePos + new Vector3Int(1, 1, 0)) != null)
                    GrassDarkMap.SetTile(tilePos + new Vector3Int(1, 1, 0), grassfloor);

            }






            if (stoneIndex == 0)
            {
                StoneMap.SetTile(tilePos, stonefloor);
            }
        }

        for (int i = 0; i < DTiles.Length; i++)
        {
            if(DTiles[i].TargetMap.GetTile(tilePos)!=null)
            DTiles[i].ThisMap.SetTile(tilePos, DTiles[i].ThisBrush);
            else DTiles[i].ThisMap.SetTile(tilePos, null);
        }

        pl.PathRescan = 1;

    }

    public void LoadMap()
    {
        int rndp = 0;

        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length + 1; i++)
        {
            if ((RND_Stable_Pos + rndp) > ObjectsWithStableGround_Buffer.Length) print("RND_Stable_Pos MOVE " + RND_Stable_Pos);

            if (ObjectsWithStableGround_Buffer[RND_Stable_Pos+ rndp].RNDPOS)
            {
                RNDPositions.Add(ObjectsWithStableGround_positions[RND_Stable_Pos+ rndp]);
                
            }

            if (RND_Stable_Pos + rndp < ObjectsWithStableGround_Buffer.Length - 1) rndp++;
            else rndp = -RND_Stable_Pos;

        }

        SetRandomPositions();
        
        float noiseValue = 0;

        print("RNDSTART_Y " + RNDSTART_Y);
        print("RNDSTART_X " + RNDSTART_X);


        int RNDY = RNDSTART_Y;

        int RNDX = RNDSTART_X;

        RNDSHIFT = new Vector2Int(RNDSTART_X, RNDSTART_Y);


        for (int x = -mapSize; x < mapSize; x++)
        {


            for (int y = -mapSize; y < mapSize; y++)
            {

                if (RNDY >= mapSize * 2) RNDY = 0;

                noiseValue = Mathf.PerlinNoise(RNDX * noiseScale, RNDY * noiseScale);  // Generate Perlin noise value for the current position

                RNDY++;

                tileIndex = Mathf.FloorToInt(noiseValue / noiseThreshold * 1);  // Calculate the tile index based on the noise value and the threshold
                Vector3Int tilePos = new Vector3Int(x, y, 0);  // Calculate the position of the tile
                FloorStates.Add(tileIndex);

                GanerateTiles(tilePos, tileIndex);


            }


            RNDX++;
            if (RNDX >= mapSize * 2) RNDX = 0;

        }




    


        for (int x = -mapSize; x < mapSize; x++)
        {
            for (int y = -mapSize; y < mapSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                CreateSubTiles(tilePos);
            }
        }

       
    }




    


    public void GanerateTiles(Vector3Int tilePos, int DrawGround)
    {
        TileBase tile = null;
        TileBase watertile = null;


        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length; i++)
        {
           
            if (Mathf.Abs(tilePos.x - Map.WorldToCell(ObjectsWithStableGround_positions[i]).x) < ObjectsWithStableGround_areas[i] && Mathf.Abs(tilePos.y - Map.WorldToCell(ObjectsWithStableGround_positions[i]).y) < ObjectsWithStableGround_areas[i])
            {
                DrawGround = 0;
              
            }
        }


        if (Mathf.Abs(tilePos.x - Map.WorldToCell(pl.transform.position).x) < 5 && Mathf.Abs(tilePos.y - Map.WorldToCell(pl.transform.position).y) < 5)
        {
            FloorToBuild.SetTile(tilePos, woodfloor);
        }

        if (DrawGround == 0)
        {
            tile = tiles;

        }
        if (DrawGround == 1)
            watertile = Watertile;


        Map.SetTile(tilePos, tile);
        WaterMap.SetTile(tilePos, watertile);
        WaterMap.SetTile(tilePos + new Vector3Int(0, 1, 0), watertile);
        WaterMap.GetComponent<TilemapCollider2D>().CreateMesh(true, false);


        int watermapborder = 10;


        for (int xx = -2; xx < 3; xx++)
        {
            for (int yy = -2; yy < 3; yy++)
            {
                if (WaterMap.GetTile(tilePos) != null && Map.GetTile(tilePos + new Vector3Int(xx, yy, 0)) != null)
                    SandMap.SetTile(tilePos + new Vector3Int(xx, yy, 0), SandTile);
            }
        }


        for (int xx = -watermapborder; xx < watermapborder; xx++)
        {
            for (int yy = -watermapborder; yy < watermapborder; yy++)
            {
                if (WaterMap.GetTile(tilePos + new Vector3Int(xx, yy, 0)) == null && Map.GetTile(tilePos + new Vector3Int(xx, yy, 0)) == null)
                    WaterMap.SetTile(tilePos + new Vector3Int(xx, yy, 0), Watertile);

               
            }
        }

      
           


    }

    void SetRandomPositions()
    {

        int iii = 0;

        for (int i = 0; i < ObjectsWithStableGround_Buffer.Length; i++)
        {

            if (ObjectsWithStableGround_Buffer[i].RNDPOS)
            {
                if (iii < RNDPositions.Count)
                {

                    ObjectsWithStableGround_positions[i] = RNDPositions[iii];
                    ObjectsWithStableGround_areas[i] = ObjectsWithStableGround[i].GroundArea;

                    ObjectsWithStableGround_Buffer[i].obj.transform.position = RNDPositions[iii];


                    if (ObjectsWithStableGround_Buffer[i].obj.GetComponent<CharacterPath>() != null)
                        ObjectsWithStableGround_Buffer[i].obj.GetComponent<CharacterPath>().StartPoint = RNDPositions[iii];

                    iii++;
                }
            }
            
        }
    }


    public int GetRandomInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

}
