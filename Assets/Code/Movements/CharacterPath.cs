using System.Collections;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;




public class CharacterPath : MonoBehaviour
{
    public Transform[] MovePoints;
    public bool OnPointDelay;
    public List<Vector2> MovePointsBuffer { get; set; } = new List<Vector2>();
    public Vector2 StartPoint { get; set; }

    public bool DirectionUpdate;

    public int CurrentPoint { get; set; }
  
    [HideInInspector]
    public float SpeedMultiplier = 4;


    private float InvisTimer;
    
    public float FollowTime = 7;
    public float AttackDelayTime = 1;

    public bool Attacking { get; set; }
    private Player pl;

    private float AttackDelay, AttackCoolDown, CollisionDealy ;

    private Animator anim;
    public Animator LegsAnim { get; set; }
    public bool Suicide = false;

    private float DodgeTimer, DodgeDelay;
    private Vector2 PrevPos;


    public bool Dodging = false;
    private float DodgeSpeed_Y, DodgeSpeed_X;

    
    /*  private List<GameObject> StopTriggerUP = new List<GameObject>();
      private List<GameObject> StopTriggerDown = new List<GameObject>();
      private List<GameObject> StopTriggerLeft = new List<GameObject>();
      private List<GameObject> StopTriggerRight = new List<GameObject>();*/
      
    private float DirectionUpdate_Timer;

 
    private Rigidbody2D rb;
    
    public Path path { get; set; }
   
    public int currentStepPoint { get; set; }

    public Vector2 directionFixed { get; set; }
    public Vector2 SpeedForce { get; private set; }

    
    private bool Walk;
   
    
    private ItemDatabase database;
    public GameObject CurrentGun { get; set; }

    private float ShootTimer;
    public float MovePauseTimer { get; set; }

    public bool GoBackAfterDamage;
    
  

    public float slope = 0;
    public bool Fliping = true;

    private Transform _transform;
    public bool FollowPlayer;

    private AnimationFrame BodyAnimationFrames;
    public float side { get; set; }
    
 
    private float UpdateTimer;
    private GameObject RunAttackTrigger;
    
    private Character character;

    private Constructor Const;
    private List<GameObject> RayColliders = new List<GameObject>();
    private MovementControll MControll;
    public bool OnThePoint;

    void Awake()
    {
        database = InitializeObjects.Itemdatabase;
        gameObject.AddComponent<PathUpdate>();

        MControll = GetComponent<MovementControll>();
        pl = InitializeObjects.PL;
        Const = InitializeObjects.Constr;
        _transform = transform;

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

    }


    void Start()
    {
      

        character = GetComponent<Character>();

        if (transform.Find("Body")!=null)
        BodyAnimationFrames = transform.Find("Body").GetComponent<AnimationFrame>();

        if(transform.Find("RunAttackTrigger")!=null)
        RunAttackTrigger = transform.Find("RunAttackTrigger").gameObject;

        if (FollowPlayer) MovePoints = new Transform[1] { GameObject.Find("Player").transform };

      
        StartPoint = transform.position;

        if (MovePoints.Length > 0) 
        {
            for (int i = 0; i < MovePoints.Length; i++)
            {
                MovePointsBuffer.Add(MovePoints[i].position);
            }
        }

        
        if (_transform.Find("Legs") != null) LegsAnim = _transform.Find("Legs").GetComponent<Animator>();

   
    }


 
    void UpdatePoints()
    {

            for (int i = 0; i < MovePointsBuffer.Count; i++)
            {
                if (MovePoints.Length > 0)
                {
                    if (MovePoints[i] != null)
                    {
                        MovePointsBuffer[i] = MovePoints[i].position;
                    }
                    else
                    {
                        MovePointsBuffer.RemoveAt(i);
                    }
                }
            }

        
        
    }



  

    public void CharacterPathUpdate()
    {
        

        if (GetComponent<Attacks>() != null)
        {
            GetComponent<Attacks>().Attacking = Attacking;

        }



        UpdatePoints();
        
        DodgeBullets();


        //Turned off for now

        /*  if (AttackCoolDown < Time.fixedTime && Attacking)
          {

              Attacking = false;
          }*/



        //  AttackTriggers();
        Animations();
        //   GunControll(pl.gameObject);

        if (!pl.StartLoading && !pl.inv.blueprintshow && !pl.Pause())
        {
            PositionMove();

            if (!Attacking)
            RemoveFromAttackList();
            else DamagePlayer();
            
      
        }

      
    }
   

    void DodgeBullets()
    {
        if (DodgeTimer < Time.time)
        {
            DodgeSpeed_Y = 0;
            DodgeSpeed_X = 0;
        }

        if (!Dodging || DodgeDelay >= Time.time) return;

        // Cache the bullets with the "Bullet" tag
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (var bullet in bullets)
        {
            if (RunAttackTrigger.GetComponent<CollList>().GetCollList().Contains(bullet))
            {
                // Calculate DodgeSpeed_Y based on the bullet's position
                DodgeSpeed_Y = bullet.transform.position.y >= transform.position.y ? -0.05f : 0.05f;

                // Calculate DodgeSpeed_X based on the bullet's position
                DodgeSpeed_X = bullet.transform.position.x >= transform.position.x ? -0.05f : 0.05f;

                AttackCoolDown = FollowTime;

                if (FollowPlayer)
                    Attacking = true;

                DodgeTimer = Time.time + 0.05f;
                DodgeDelay = Time.time + Random.Range(1f, 2f);
            }
        }
    }





    void DamagePlayer()
    {
        if (!pl.coll_obj.Contains(gameObject)) return;
    
        if (AttackDelay >= Time.fixedTime || MovePauseTimer >= Time.fixedTime) return;

        if (GetComponent<Animator>() != null)
        {
            if(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "HitPlayer")
            GetComponent<Animator>().Play("HitPlayer",0);

        }

        if (GoBackAfterDamage)
            {
                CollisionDealy = Time.fixedTime + 10;
                Attacking = false;
            }
              
                AttackDelay = Time.fixedTime + AttackDelayTime;
             

            if (Suicide)
            {

                    RemoveFromAttackList();
                    pl.BlowThis(gameObject);
            }

               

               

            MovePauseTimer = Time.fixedTime + AttackDelayTime;
             
        
    
    }



    void PositionMove()
    {
        if(pl.StartLoading) return;
       
        if (pl._gameover || pl.PathRescanBoundTimer > 0 || pl.PathRescan > 0 || OnThePoint)
        {
            SpeedForce = Vector3.zero;
            return;
        }


      

        if (path == null)
        {
            SpeedForce = Vector3.zero;
           
            return;
         
        }
        
        if (SetSpeedZero_NoPathComp())
        {

            if (MControll == null)
            {
                if (CurrentPoint < MovePointsBuffer.Count - 1)
                    CurrentPoint++;
                else CurrentPoint = 0;

            }
     
            directionFixed = Vector3.zero;
            SpeedForce = Vector3.zero;

            return;
        }

        if (MovePauseTimer < Time.fixedTime)
        {
            SpeedForce = (directionFixed * Time.deltaTime) * SpeedMultiplier ;
        }
        else
        {
            
            SpeedForce = new Vector2(0, 0);
        }
    
        if (character != null)
        {

            if (!character.Enemy && character.Chatting)
            {
                if (pl.coll_obj.Contains(gameObject))
                {
           
                    SpeedForce = new Vector2(0, 0);
                }
            }

        }

   
        if (Mathf.Abs(SpeedForce.x) > 0.001f || Mathf.Abs(SpeedForce.y) > 0.001f) 
            Walk = true;
        else Walk = false;

      
        _transform.position = new Vector3(_transform.position.x + SpeedForce.x + DodgeSpeed_X, _transform.position.y + SpeedForce.y + DodgeSpeed_Y, _transform.position.z);
      

    }

    bool SetSpeedZero_NoPathComp()
    {

        if ((currentStepPoint >= path.vectorPath.Count - 1 && PathComlitionCheck()) )
        {
            if (CurrentPoint >= MovePoints.Length)
                return false;

            if (MovePoints[CurrentPoint] == null)
                return false;

            
            if (MovePoints[CurrentPoint].GetComponent<PubObject>() != null)
            if (MovePoints[CurrentPoint].GetComponent<PubObject>().wall > 0)
                return true;

            if (MovePoints[CurrentPoint].GetComponent<PubObject>() == null)
                return true;
              
           
        }

        return false;
    }


     void GunControll(GameObject GunTarget)
    {

        if (_transform.Find("GunBody") == null) return;
        
            if (CurrentGun == null )
            {
                CurrentGun = new GameObject();
                CurrentGun = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Gun"));
              
                CurrentGun.name = name + "EnemyGUN";
                
            }

            
         

            CurrentGun.transform.position = transform.Find("GunBody").transform.position;

            if (Attacking)
            {
               

                if (ShootTimer < Time.fixedTime)
                {
                   // CurrentGun.GetComponent<ItemGenerator>().Fire = true;
                    ShootTimer = Time.fixedTime + 1;
                }
                Vector3 target = GunTarget.transform.position;
                Transform Hands = transform.Find("GunBody");
              //  CurrentGun.GetComponent<ItemGenerator>().difference = target - Hands.position;

                Vector3 difference = target - Hands.position;
                Vector3 differenceP = target - Hands.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if (Mathf.Sqrt(Mathf.Abs(differenceP.x) * Mathf.Abs(differenceP.x) + Mathf.Abs(differenceP.y) * Mathf.Abs(differenceP.y)) > 0.2f)
                {
                    Hands.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                    CurrentGun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                    if ((rotationZ > 90 && rotationZ < 270) || (rotationZ < -90 && rotationZ > -270))
                    {
                        CurrentGun.GetComponent<SpriteRenderer>().flipY = true;
                    }
                    else CurrentGun.GetComponent<SpriteRenderer>().flipY = false;

                }
            }



        
    }

    void Animations()
    {
        if (pl == null) return;

        if (pl.Pause())
        {
            if(LegsAnim!=null)
            LegsAnim.speed = 0;
            if (anim != null)
                anim.speed = 0;
            return;
        }

        Flip();



        if (BodyAnimationFrames != null)
        {
            BodyAnimationFrames.Play = Walk;
            BodyAnimationFrames.FramesDelay = BodyAnimationFrames.CycleDelay = 0.1f-(Mathf.Abs(SpeedForce.x) + Mathf.Abs(SpeedForce.y)) ;
        }

        if (SpeedForce == Vector2.zero) Walk = false;

        if (anim != null)
        {
            anim.speed = 1;
           

        }

        if (_transform.Find("Legs") != null && LegsAnim != null)
        {
          LegsAnim.speed = (Mathf.Abs(SpeedForce.x) + Mathf.Abs(SpeedForce.y))*120;
            
          LegsAnim.SetBool("Walk", Walk);
        }

    }

    public void GoBack()
    {
        Attacking = false;
        if (MovePoints.Length <= 0) return;
        
        CurrentPoint = 0;

        for (int i = 0; i < MovePoints.Length; i++)
        {
            if (MovePoints[i] != null)
            {
                if (MovePoints[i].GetComponent<MovementControll>() != null)
                {
                    MovePoints[i].GetComponent<MovementControll>().Attacked = false;
                }
            }
        }

        MovePoints = new Transform[0];
   
        MovePointsBuffer = new List<Vector2>();
        MovePointsBuffer.Add(StartPoint);
        
    }

    private void AttackTriggers()
    {
        if (_transform.Find("AttackTrigger") == null || RunAttackTrigger == null) return;
        
        if (pl.coll_obj.Contains(transform.Find("AttackTrigger").gameObject))
        {
            if (!Attacking && CollisionDealy < Time.fixedTime)
            {
                StartAttack();
            }


            AttackCoolDown = Time.fixedTime + FollowTime;
        }
        else
        {
            if (RunAttackTrigger != null)
            {
                if (pl.coll_obj.Contains(RunAttackTrigger))
                {

                    AttackCoolDown = Time.fixedTime + FollowTime;

                    if (!Attacking && CollisionDealy < Time.fixedTime)
                    {


                        StartAttack();
                    }

                }
            }
        }
        

    }



    public bool PathComlitionCheck()
    {
      
        bool PathCanBe = true;
        if (path == null) return PathCanBe;
        if (path.vectorPath == null) return PathCanBe;

        if (MovePoints.Length <= 0) return PathCanBe;
        
        if (MovePoints[MovePoints.Length - 1] == null) return PathCanBe;

        Vector3 LastPointLastPos = MovePoints[MovePoints.Length - 1].transform.position;

        if (path.vectorPath.Count <= 0) return PathCanBe;

        
        if (Mathf.Abs(path.vectorPath[path.vectorPath.Count - 1].x - LastPointLastPos.x) < 0.25f &&
            Mathf.Abs(path.vectorPath[path.vectorPath.Count - 1].y - LastPointLastPos.y) < 0.25f)
        {
            PathCanBe = true;
            
        }


        if (Mathf.Abs(path.vectorPath[path.vectorPath.Count - 1].x - LastPointLastPos.x) >= 0.25f ||
            Mathf.Abs(path.vectorPath[path.vectorPath.Count - 1].y - LastPointLastPos.y) >= 0.25f)

        {
            PathCanBe = false;
        }

        // Can be important but off for now
        //if (path.vectorPath.Count <= 6) PathCanBe = true;
            
        return PathCanBe;
    }


    public bool WallObsticleCheck(GameObject Target)
    {

      // RayColliders = new List<GameObject>();
        bool wallishere = false;

        // Set layers for the LayerMask using bitwise OR
        int layer1 = LayerMask.NameToLayer("Wall");

        // Combine layers using the bitwise OR operator
        int layerMaskValue = 1 << layer1;

        // Create a LayerMask from the combined value
        LayerMask myLayerMask = layerMaskValue;


        RaycastHit2D Ray = Physics2D.Raycast(transform.position,  Target.transform.position- transform.position, Vector2.Distance(transform.position, Target.transform.position), myLayerMask);
       
        if (Ray.collider != null)
        {
               
                wallishere = true;
        }
     
        
        return wallishere;
    }



    private void Flip()
    {
           side = _transform.localScale.x;
       
            if (SpeedForce.x < -0.001f)
                side = -1;
            else if (SpeedForce.x > 0.001f)
            side = 1;

        if (Fliping)
            _transform.localScale = new Vector3(side, _transform.localScale.y, _transform.localScale.z);

    }


    public void StartAttack()
    {

        if (MControll != null)
        {
        

            if (MControll.FollowBorder == 0)
            {
                return;
            }
            MControll.StartAttack();
        }

        if (!pl.AttackingEnemies.Contains(gameObject))
            pl.AttackingEnemies.Add(gameObject);
        Attacking = true;


    }

    public void StopAttack()
    {
        if (!name.Contains("Boss"))
        {

            RemoveFromAttackList();
        }


        Attacking = false;
    }


    public void RemoveFromAttackList()
    {
       
            if (pl.AttackingEnemies.Contains(gameObject))
                pl.AttackingEnemies.Remove(gameObject);
        
    }



    public Vector3 GetCellWorldPosition(Vector2 pos)
    {
        var cellPosition = transform.rotation * new Vector3(pos.x, pos.y, 0f) * 0.25f + transform.position;
        return cellPosition;
    }

}

