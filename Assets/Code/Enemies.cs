using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class Enemies : MonoBehaviour
{
    public GameObject[] EnemyObjects;
    private float timer;
    public float DelayBetweenEnemies = 10;
    public float DelayBetweenWaves = 10;
    public float DelayStart = -1;


    public Transform[] SpawnPositions;

  
    [HideInInspector]
    public List<GameObject> BuildedPers = new List<GameObject>();

    
    private Constructor _constr;
    private int EnemiesInWaveCount;
    public int EnemiesInWaveCountMAX = 10;
  
    
    private int WaveCount;
    public int WaveCountMAX = 3;

    
    private Player pl;

    public DayAndNight.DayCycle Day_Cycle = DayAndNight.DayCycle.Day;
    public bool NotInTheMorning = false;
    
    private Tilemap TileBase;
    private StatsControll stats;
    private SpriteRenderer SPRT;
    private AnimationFrame AnimFrame;

    public bool RandomPosition;
    private List<Vector3> RandomPositions = new List<Vector3>();
    void Start()
    {
        pl = InitializeObjects.PL;
        _constr = InitializeObjects.Constr;

        stats = GetComponent<StatsControll>();
        SPRT = GetComponent<SpriteRenderer>();
        AnimFrame = GetComponent<AnimationFrame>();

        TileBase = GameObject.Find("GreyGround").GetComponent<Tilemap>();

        EnemiesInWaveCount = 0;


        if (DelayStart < 0) DelayStart = DelayBetweenWaves;

        if (DelayStart > pl.DayNight.DayLength)
            timer = DelayStart - _constr.SL.DayTime;
        else timer = DelayStart;

    }

    

    void Update()
    {
        if (pl.StartLoading || pl.menu.MenuONOFF) return;

        timer -= Time.deltaTime * _constr.Game_SPEED;

 
        if (NotInTheMorning && pl.DayNight.Day_Cycle == DayAndNight.DayCycle.Morning) return;

        if (_constr.Game_SPEED > 0 && (pl.DayNight.Day_Cycle == Day_Cycle || Day_Cycle == DayAndNight.DayCycle.AllTime))
        {
          
            CreateEnemy_Main();

            CheckEnemyNull();
        }

    }


    void AnimationAndColor()
    {
        float col = 1;
        if( 1 + (timer) / DelayBetweenEnemies * -1 > 0.5f)
        col = 1 + (timer) / DelayBetweenEnemies * -1;
        else col = 0.5f;

        if (SPRT != null)
            SPRT.color = new Color(col, col, col, 1);


        if (NotInTheMorning && pl.DayNight.Day_Cycle == DayAndNight.DayCycle.Morning) return;

        if (pl.DayNight.Day_Cycle != Day_Cycle)
        if (AnimFrame != null)  AnimFrame.Play = false;

    }



    void CreateEnemy_Main()
    {
      

        if (AnimFrame != null)  AnimFrame.Play = true;
     
        if (timer >= 0) return;
      
        if (transform.parent == _constr.gameObject) return;
        
        if (BuildedPers.Count >= EnemiesInWaveCountMAX * WaveCountMAX) return;
       
        if (EnemiesInWaveCount < EnemiesInWaveCountMAX)
        {
           
            CreateEnemy();
            
        }
        else
        {
            
            WaveCount++;


            EnemiesInWaveCount = 0;
            timer = DelayBetweenWaves;


        }
        
    }



    void CheckEnemyNull()
    {
        for (int i = 0; i < BuildedPers.Count; i++)
        {
            if (BuildedPers[i] == null)
            {
                BuildedPers.RemoveAt(i);
                EnemiesInWaveCount--;

                // BuildedPersCount--;
            }
        }
    }

    public void DestoryEnemies()
    {
        for (int i = 0; i < BuildedPers.Count; i++)
            Destroy(BuildedPers[i]);
    }

    void CreateEnemy()
    {
        int num = Random.Range(0, EnemyObjects.Length);


        MovementControll EnemyObjects_MC = EnemyObjects[num].GetComponent<MovementControll>();


     
 


        if (stats != null)
        {
            if (stats.DurabilityMax > -1)
                stats.Durability--;
        }



        GameObject Enemy = Instantiate<GameObject>(EnemyObjects[num]);

  
   

        BuildedPers.Add(Enemy);
        Enemy.name = EnemyObjects[num].name + BuildedPers.Count;

      
        while (GameObject.Find(Enemy.name) != null && GameObject.Find(Enemy.name)!= Enemy)
        Enemy.name += "N";

        

        StatsControll Enemy_Stats = Enemy.GetComponent<StatsControll>();

        Enemy_Stats.enabled = true;
       // Enemy_Stats.BuildedStructure = true;
        Enemy_Stats.SpawnPointName = name;

        _constr.ConstructedStructures.Add(new ObjectOnBoard(Enemy_Stats.DatabaseID, Enemy.transform.position, Enemy.name, Enemy, Enemy.GetComponent<StatsControll>(), Enemy.GetComponent<PubObject>()));

        Vector3 EnemyPos = CalculateRandomPosition();


        if (SpawnPositions.Length == 0)
        {

            EnemyPos = CalculateRandomPosition();


        }
        else
        {
            int n = Random.Range(0, SpawnPositions.Length);
            int notallareempty = 0;

            for (int ii = 0; ii < SpawnPositions.Length; ii++)
            {
                if (TileBase.GetTile(TileBase.WorldToCell(SpawnPositions[ii].transform.position)) == null)
                {
                    notallareempty++;
                }
            }


            if (notallareempty == 0)
            {
                Vector3Int pp = new Vector3Int(-50, -50, 0);
                for (int ii = 0; ii < SpawnPositions.Length; ii++)
                {
                       
                           
                        SpawnPositions[ii].transform.position += new Vector3( Random.Range(-10,10), Random.Range(-10, 10),0);
                        
                }
            }


            for (int ii = 0; ii < SpawnPositions.Length; ii++)
            {
                if (TileBase.GetTile(TileBase.WorldToCell(SpawnPositions[ii].transform.position)) != null)
                {
                    n = ii;


                }
            }

            EnemyPos = new Vector3(SpawnPositions[n].position.x,
                SpawnPositions[n].position.y, 1);


              
        }


        Enemy.transform.position = EnemyPos;


    

        EnemiesInWaveCount++;

        timer = DelayBetweenEnemies;
        
    }


    Vector3 CalculateRandomPosition()
    {
        Vector3 pos = transform.position;
        int maxsearchattempts = 10;

        float StartPos = 10f;

        if (RandomPosition)
        {
            StartPos = 30f;
          
        }
        else
            StartPos = 1f;
    
        
   
        for (int i = 0; i < maxsearchattempts; i++)
        {
            if (_constr.CheckStructures(pos) ||
               !_constr.CheckTheGround(pos) ||
               _constr.CheckWallTiles(pos))
                pos =
                    transform.position +
                    new Vector3(Random.Range(-StartPos, StartPos), Random.Range(-StartPos, StartPos), 0);
            else return pos;

        }

        
        return transform.position;
    }


}
