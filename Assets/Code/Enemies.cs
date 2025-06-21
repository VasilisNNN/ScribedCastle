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

    void Start()
    {
        stats = GetComponent<StatsControll>();
        SPRT = GetComponent<SpriteRenderer>();
        AnimFrame = GetComponent<AnimationFrame>();

        TileBase = GameObject.Find("GreyGround").GetComponent<Tilemap>();

        EnemiesInWaveCount = 0;

        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();
        if (DelayStart < 0) DelayStart = DelayBetweenWaves;

        if (DelayStart > pl.DayNight.DayLength)
            timer = DelayStart - _constr.SL.DayTime;
        else timer = DelayStart;

        //EnemyTimer = GameObject.Find("EnemyTimer");

            /*  for (int i = 0; i < _constr.OBOnBoard.Count; i++)
              {
                  if (_constr.OBOnBoard[i].Name.Contains(EnemyObjects[0].name))
                  BuildedPers.Add(_constr.OBOnBoard[i].Object);

              }*/


    }

    

    void Update()
    {
        if (pl.StartLoading) return;

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


        if (EnemyObjects_MC.Soldier && _constr.SoldiersCount >= _constr.SoldiersCountMAX) return;
  
        if (EnemyObjects_MC.Enemy && _constr.EnemiesCount >= _constr.EnemiesCountMAX) return;
   
      
 


        if (stats != null)
        {
            if (stats.DurabilityMax > -1)
                stats.Durability--;
        }



        GameObject Enemy = Instantiate<GameObject>(EnemyObjects[num]);

        if (Enemy.GetComponent<MovementControll>().Soldier)
            _constr.SoldiersCount++;

        if (Enemy.GetComponent<MovementControll>().Enemy)
            _constr.EnemiesCount++;

        BuildedPers.Add(Enemy);
        Enemy.name = EnemyObjects[num].name + BuildedPers.Count;

      
        if (GameObject.Find(Enemy.name) != null)
        Enemy.name += "N" + UnityEngine.Random.Range(0.1f, 100.1f) + UnityEngine.Random.Range(0.1f, 100.1f) + UnityEngine.Random.Range(0.1f, 100.1f) + UnityEngine.Random.Range(0.1f, 100.1f);

        

        StatsControll Enemy_Stats = Enemy.GetComponent<StatsControll>();

        Enemy_Stats.enabled = true;
       // Enemy_Stats.BuildedStructure = true;
        Enemy_Stats.SpawnPointName = name;

        _constr.ConstructedStructures.Add(new ObjectOnBoard(Enemy_Stats.DatabaseID, Enemy.transform.position, Enemy.name, Enemy, Enemy.GetComponent<StatsControll>(), Enemy.GetComponent<PubObject>()));

        Vector3 EnemyPos = transform.position;


        if (SpawnPositions.Length == 0)
        {

             
            for (int x = Random.Range(-1,2); x <2; x++)
            {
                int y = 0;

                float XPOS = 0.5f * x - 0.5f*y;
                float YPOS = 0.25f * y + 0.25f * x;

                if (x == 0 && y == 0) x++;

                if (transform.position + new Vector3(XPOS, YPOS, 0) == transform.position) x++;

                XPOS = 0.5f * x - 0.5f * y;
                YPOS = 0.25f * y + 0.25f * x-1f;


                if (TileBase.GetTile(TileBase.WorldToCell(transform.position + new Vector3(XPOS, YPOS, 0))) != null)
                {

                    if (!_constr.CheckObjectsPositionOnBoard(transform.position + new Vector3(XPOS, YPOS, 0)))
                    {
                        EnemyPos = transform.position + new Vector3(XPOS, YPOS, 0) ;

                        break;
                    }
                }


                    
            }



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
}
