using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Tilemaps;

public class Steps : MonoBehaviour {
	public Player pl;
    public CharacterPath cm;
    private float _normalHSpeed, _normalVSpeed;

    public float delay = 0.35f;
    private float timer;
    public float stepLength = 0.2f;
    private AudioSource AS;
    public AudioClip[] AC;

    public AudioClip[] ConcreteClips;
    public AudioClip[] SandClips;
    public AudioClip[] GrassClips;

    public AudioClip[] WoodClips;
    public AudioClip[] RockyRoadClips;
    public AudioClip[] MudClips;

    private AudioClip[] MainClipArray;

    private int[] concretesteps, sandsteps, grasssteps, roadteps, woodsteps, mudsteps;
    private GameObject[]  ConcreteFloor, SandFloor, GrassFloor, RoadFloor, WoodFloor, MudFloor;
    public TileBase[] ConcreteFloor_TileBase, SandFloor_TileBase, GrassFloor_TileBase, RoadFloor_TileBase, WoodFloor_TileBase, MudFloor_TileBase;

    public List<Tilemap> BaseTile = new List<Tilemap>();
    

    private int s;
    private List<GameObject> coll_obj = new List<GameObject>();
    public bool RND;
    private int prevstepnum;


    public List<Tilemap> SortedTileList = new List<Tilemap>();

    void Start () {

        GameObject Grid = GameObject.Find("Grid");
        foreach (GameObject basechild in Grid.transform)
        BaseTile.Add(basechild.GetComponent<Tilemap>());



        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            if(Grid.transform.GetChild(i).GetComponent<Tilemap>()!=null)
            BaseTile.Add(Grid.transform.GetChild(i).GetComponent<Tilemap>());
        }

        SortTilemapsByLayerOrder();

        ConcreteFloor = GameObject.FindGameObjectsWithTag("Concrete");
        SandFloor = GameObject.FindGameObjectsWithTag("Sand");
        GrassFloor = GameObject.FindGameObjectsWithTag("Grass");
        WoodFloor = GameObject.FindGameObjectsWithTag("Wood");
        RoadFloor = GameObject.FindGameObjectsWithTag("Road");
        MudFloor = GameObject.FindGameObjectsWithTag("Mud");

        concretesteps = new int[BaseTile.Count];
        sandsteps = new int[BaseTile.Count];
        grasssteps = new int[BaseTile.Count];
        woodsteps = new int[BaseTile.Count];
        roadteps = new int[BaseTile.Count];
        mudsteps = new int[BaseTile.Count];

       
        AS = GetComponent<AudioSource>();


        MainClipArray = AC;
        AS.clip = MainClipArray[s];



        /*
        SandClips = new AudioClip[4];
        ConcreteClips = new AudioClip[4];
        GrassClips = new AudioClip[4];

         for (int i = 0; i < WaterClips.Length; i++)
             WaterClips[i] = Resources.Load<AudioClip>("Sound/Steps/WaterSteps_" + i);*/
        /* for (int i = 0; i < 4; i++)
             SandClips[i] = Resources.Load<AudioClip>("Sound/Steps/SandSteps_" + i);
         for (int i = 0; i < 4; i++)
             ConcreteClips[i] = Resources.Load<AudioClip>("Sound/Steps/ConcreteSteps_" + i);
         for (int i = 0; i < 4; i++)
             GrassClips[i] = Resources.Load<AudioClip>("Sound/Steps/GrassSteps_" + i);

         for (int i = 0; i < 2; i++)
             WoodClips[i] = Resources.Load<AudioClip>("Sound/Steps/WoodSteps_" + i);
             */


    }


    void Update () {


        if (pl != null)
        {
            _normalHSpeed = pl._normalHSpeed;
            _normalVSpeed = pl._normalVSpeed;
        }


        if (cm != null)
        {
            _normalHSpeed = cm.SpeedForce.x;
            _normalVSpeed = cm.SpeedForce.y;
        }


        /*
         delay = 0.38f/GameObject.Find("Player").GetComponent<Animator>().speed;
         */

        TileSteps();
        // OldSteps();
        if (roadteps.Sum() > 0) ChangeToCorrentClipArray(RockyRoadClips);
        else if (woodsteps.Sum() > 0) ChangeToCorrentClipArray(WoodClips);
        else if(mudsteps.Sum() > 0) ChangeToCorrentClipArray(MudClips);
        else if ( grasssteps.Sum() > 0) ChangeToCorrentClipArray(GrassClips);
        else if (sandsteps.Sum() > 0) ChangeToCorrentClipArray(SandClips);
        else if (concretesteps.Sum() > 0) ChangeToCorrentClipArray(ConcreteClips);




        if (concretesteps.Sum() <= 0 && sandsteps.Sum() <= 0&& grasssteps.Sum() <= 0 && roadteps.Sum() <= 0 && woodsteps.Sum() <= 0 && mudsteps.Sum() <= 0)
        {
            if (MainClipArray != AC)
            ChangeToCorrentClipArray(AC);
                
            

        }

       
        
        
        if (Mathf.Abs(_normalHSpeed) > 0.0001f || Mathf.Abs(_normalVSpeed) > 0.0001f)
        {
            if (timer + delay + stepLength < Time.fixedTime && !AS.isPlaying) PlaySteps();
            if (timer + stepLength < Time.fixedTime && AS.isPlaying) AS.Stop();
        }
        else if (timer + stepLength < Time.fixedTime && AS.isPlaying) AS.Stop();
    }
    void ChangeToCorrentClipArray(AudioClip[] ac)
    {
        if (MainClipArray != ac)
        {
            MainClipArray = ac;

            AS.Stop();
            timer = Time.fixedTime - delay - stepLength - 1;
            if (s >= MainClipArray.Length) s = 0;
            AS.clip = MainClipArray[s];
        }
    }
    void PlaySteps()
    {

        float rndpitch;

        if (!RND) s++;
        else
        {
            s = Random.Range(0, MainClipArray.Length);
            rndpitch  = Random.Range(0.9f, 1.1f);
        }
      

        if (s == prevstepnum)
        {
            while (s == prevstepnum) s = Random.Range(0, MainClipArray.Length);
        }

        prevstepnum = s;

      if (s > AC.Length - 1) s = 0;

      AS.clip =  MainClipArray[s];
      AS.Play();
        
      timer = Time.fixedTime;

    }

    private void OnTriggerStay2D(Collider2D c)
    {
        if (!coll_obj.Contains(c.gameObject))
        {
            coll_obj.Add(c.gameObject);
        }


    }

    private void OnTriggerExit2D(Collider2D c)
    {

        if (coll_obj.Contains(c.gameObject))
            coll_obj.Remove(c.gameObject);

    }


    void OldSteps()
    {
        if (ConcreteFloor.Length > 0)
        {
            for (int i = 0; i < ConcreteFloor.Length; i++)
            {
                if (coll_obj.Contains(ConcreteFloor[i]))
                {
                    concretesteps[i] = 1;
                }
                else concretesteps[i] = 0;
            }

            
        }


        if (SandFloor.Length > 0)
        {

            for (int i = 0; i < SandFloor.Length; i++)
            {
                if (coll_obj.Contains(SandFloor[i])) sandsteps[i] = 1;
                else sandsteps[i] = 0;
            }

        }

        if (GrassFloor.Length > 0)
        {

            for (int i = 0; i < GrassFloor.Length; i++)
            {
                if (coll_obj.Contains(GrassFloor[i])) grasssteps[i] = 1;
                else grasssteps[i] = 0;
            }
        }


        if (RoadFloor.Length > 0)
        {

            for (int i = 0; i < RoadFloor.Length; i++)
            {
                if (coll_obj.Contains(RoadFloor[i])) roadteps[i] = 1;
                else roadteps[i] = 0;
            }
        }


        if (WoodFloor.Length > 0)
        {

            for (int i = 0; i < WoodFloor.Length; i++)
            {
                if (coll_obj.Contains(WoodFloor[i]))
                {
                    print("w");
                    woodsteps[i] = 1;
                }
                else woodsteps[i] = 0;
            }
        }

        if (MudFloor.Length > 0)
        {

            for (int i = 0; i < MudFloor.Length; i++)
            {
                if (coll_obj.Contains(MudFloor[i])) mudsteps[i] = 1;
                else mudsteps[i] = 0;
            }
        }
    }

    void TileSteps()
    {
        Vector3Int CellPos = BaseTile[0].WorldToCell(pl._transform.position);

        int vert = 0;

        if (pl.IM._vertical > 0) vert = 1;
        else if (pl.IM._vertical < 0) vert = -1;
        else vert = 0;

        int hor = 0;

        if (pl.IM._horizontal > 0) hor = 1;
        else if (pl.IM._horizontal < 0) hor = -1;
        else hor = 0;


        transform.position = BaseTile[0].CellToWorld(CellPos ) + new Vector3(0.5f  * hor, 0.25f * vert, 0);

        concretesteps = new int[BaseTile.Count];
        sandsteps = new int[BaseTile.Count];
        grasssteps = new int[BaseTile.Count];
        woodsteps = new int[BaseTile.Count];
        roadteps = new int[BaseTile.Count];
        mudsteps = new int[BaseTile.Count];




        for (int j = 0; j< SortedTileList.Count; j++)
        {

            TileBase CurrentTileBase = SortedTileList[j].GetTile(CellPos );


                if (ConcreteFloor_TileBase.Length > 0)
                {
                    for (int i = 0; i < ConcreteFloor_TileBase.Length; i++)
                    {
                        if (CurrentTileBase == ConcreteFloor_TileBase[i])
                    
                        {
                            concretesteps[j] = 1;
                        break;
                        }
                        else concretesteps[j] = 0;


                    }

                    
                }

                

            if (SandFloor_TileBase.Length > 0)
            {
                for (int i = 0; i < SandFloor_TileBase.Length; i++)
                {
                    if (CurrentTileBase == SandFloor_TileBase[i])

                    {
                        sandsteps[j] = 1;
                        break;
                    }
                    else sandsteps[j] = 0;
                }


            }

            if (GrassFloor_TileBase.Length > 0)
            {
                for (int i = 0; i < GrassFloor_TileBase.Length; i++)
                {
                    if (CurrentTileBase == GrassFloor_TileBase[i])

                    {
                     

                        grasssteps[j] = 1;
                        break;
                    }
                    else grasssteps[j] = 0;
                }


            }


            if (RoadFloor_TileBase.Length > 0)
            {
                for (int i = 0; i < RoadFloor_TileBase.Length; i++)
                {
                    if (CurrentTileBase == RoadFloor_TileBase[i])

                    {
                        roadteps[j] = 1;
                        break;
                    }
                    else roadteps[j] = 0;
                }


            }

            if (WoodFloor_TileBase.Length > 0)
            {
                for (int i = 0; i < WoodFloor_TileBase.Length; i++)
                {
                    if (CurrentTileBase == WoodFloor_TileBase[i])

                    {
                        woodsteps[j] = 1;
                        break;
                    }
                    else woodsteps[j] = 0;
                }


            }

            if (MudFloor_TileBase.Length > 0)
            {
                for (int i = 0; i < MudFloor_TileBase.Length; i++)
                {
                    if (CurrentTileBase == MudFloor_TileBase[i])

                    {
                        mudsteps[j] = 1;
                        break;
                    }
                    else mudsteps[j] = 0;
                }


            }

            if (concretesteps.Sum() > 0 || sandsteps.Sum() > 0 || grasssteps.Sum() > 0 || roadteps.Sum() > 0 || woodsteps.Sum() > 0 || mudsteps.Sum() > 0)
            {
                break;
            }


        }

        

      

       
    }



    void SortTilemapsByLayerOrder()
    {
        SortedTileList = new List<Tilemap>(BaseTile);

        // Sort the tilemaps based on the sorting order
        SortedTileList.Sort((a, b) =>
        {
            TilemapRenderer aRenderer = a.GetComponent<TilemapRenderer>();
            TilemapRenderer bRenderer = b.GetComponent<TilemapRenderer>();

            if (aRenderer != null && bRenderer != null)
            {
                return bRenderer.sortingOrder.CompareTo(aRenderer.sortingOrder);
            }
            else
            {
                // If a TilemapRenderer component is missing, consider it as lower priority
                return aRenderer == null ? 1 : -1;
            }
        });

        // Print the sorted tilemap names
        foreach (Tilemap tilemap in SortedTileList)
        {
         //   Debug.Log(tilemap.name);
        }
    }


}
