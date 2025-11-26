
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class AnimState
{
    public bool State;
    public string Name;

   public AnimState(string _name, bool state)
    {
        Name = _name;
        State = state;
    }
}

public class Attacks : MonoBehaviour
{
    private Player pl;
    private Transform _transform;
    private float HSpeed, VSpeed;

    private float DelayBetweenAttacks, AttackDuration, DamageTime;
    private float AttackDurationMax;

    public float DelayBetweenAttacks_MAX = 5;

    private int side;
    private GameObject FaceZone, FaceDownZone, FaceUpZone, TeleportCollider;
    private Material StartMaterial, WhiteMaterial;

    private int AttackNum;
    private Animator Anim;
    public Vector2 StartSpeed = new Vector2(1, 1);

    public enum AttackType { Dashing, Screaming, ShootingStraight, ShootingThree, ShootingCircle, Lazers, SpawnEnemies, CirclingSpin, Volcano, GrowingCircle, ElectroSphere, TeleportAndHit, FastDash, DropRocks, FollowBullet};


    public List<AttackType> _Attacktype;
    private AudioClip[] DashingClips, ScreamingClips, ShootingStraightClips, ShootingCircleClips, LazersClips, SpawnEnemiesClips, CirclingSpinClips, VolcanoClips, GrowingCircleClips;
    private GameObject StunEffect, ScreamingEffect, ShootingCircleEffect, ShootingStraightEffect, LazersEffect, SpawnEnemiesEffect, CirclingSpinEffect, VolcanoEffect, GrowingCircleEffect;

    public List<GameObject> Bullets = new List<GameObject>();
    private List<GameObject> Enemies = new List<GameObject>();
    private List<Vector3> BulletsSpeeds = new List<Vector3>();
    public GameObject AttackSine { get; private set; }

    public GameObject[] EnemiesToSpawn;
    public bool Attacking;

    private float DamageBuffTimer;

    private bool Dashing;
    private float StunnedTimer, AttackBuildup;

    public GameObject Bullet;

    private CharacterPath CM;

    private List<AnimState> AnimatorStates = new List<AnimState>();

    private GameObject AttackStartPos;
    public bool CanBeStunned;

    // Start is called before the first frame update
    void Start()
    {

        if(transform.Find("AttackStartPos")!=null)
        AttackStartPos = transform.Find("AttackStartPos").gameObject;

        AnimatorStates.Add(new AnimState("Walk", false));
        AnimatorStates.Add(new AnimState("DropRocks", false));
        AnimatorStates.Add(new AnimState("FollowBullet", false));
        AnimatorStates.Add(new AnimState("ElectroSphere", false));
        AnimatorStates.Add(new AnimState("GrowingCircle", false));
        AnimatorStates.Add(new AnimState("Lazers", false));
        AnimatorStates.Add(new AnimState("Volcano", false));
        AnimatorStates.Add(new AnimState("SpawnEnemies", false));
        AnimatorStates.Add(new AnimState("Screaming", false));
        AnimatorStates.Add(new AnimState("Shooting", false));
        AnimatorStates.Add(new AnimState("Shooting3", false));
        AnimatorStates.Add(new AnimState("ShootingCircle", false));

        CM = GetComponent<CharacterPath>();

        if (Bullet == null) Bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
        side = 1;

       
        Anim = GetComponent<Animator>();
        FaceZone = transform.Find("FaceZone").gameObject;
        FaceDownZone = transform.Find("FaceDownZone").gameObject;
        FaceUpZone = transform.Find("FaceUpZone").gameObject;

        AttackDurationMax = 1;
        pl = InitializeObjects.PL;
       
        _transform = transform;
       
        DelayBetweenAttacks = Time.fixedTime + DelayBetweenAttacks_MAX;


        StartMaterial = GetComponent<SpriteRenderer>().material;
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");
        HSpeed = StartSpeed.x;
        VSpeed = StartSpeed.y;

        DashingClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Air/Warmup Short/Stereo/Air_Warmup_Short_3_S") };

        ScreamingClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/BOSS/Scream") };

        ShootingStraightClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/EvilDark/Hit/Stereo/EvilDark_Hit_1_S") };

        ShootingCircleClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Air/Warmup Short/Stereo/Air_Warmup_Short_3_S") };


        LazersClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Air/Warmup Short/Stereo/Air_Warmup_Short_3_S") };


        SpawnEnemiesClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Earth/Hit/Stereo/Earth_Hit_1_S") };

        CirclingSpinClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Air/Warmup Short/Stereo/Air_Warmup_Short_3_S") };


        VolcanoClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Fire/Hit/Stereo/Fire_Hit_1_S") };

        GrowingCircleClips = new AudioClip[1]
            {Resources.Load<AudioClip>("Sound/Sound Library - Magic/Air/Warmup Short/Stereo/Air_Warmup_Short_3_S") };





       ScreamingEffect = Resources.Load<GameObject>("Prefabs/Effects/ScreamAttackEffect") ;
        ShootingCircleEffect = Resources.Load<GameObject>("Prefabs/Effects/CircleAttackEffect");
        ShootingStraightEffect = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect");
        LazersEffect = Resources.Load<GameObject>("Prefabs/Effects/Attack_Lazer_Effect");
        SpawnEnemiesEffect = Resources.Load<GameObject>("Prefabs/Effects/Explosion");
        CirclingSpinEffect = Resources.Load<GameObject>("Prefabs/Effects/CircleSwingAttackEffect");
        VolcanoEffect = Resources.Load<GameObject>("Prefabs/Effects/Explosion");
        GrowingCircleEffect = Resources.Load<GameObject>("Prefabs/Effects/Explosion");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (pl.menu.MenuONOFF)
        {
            Anim.speed = 0;
            return;
        }

        Anim.speed = 1;

        if (Attacking)
        {
            if (Dashing) Move();
            else
                HSpeed = VSpeed = 0;
        }

        Stunned();


        if (AttackDuration < Time.fixedTime)
        {
            if(CM != null)
                CM.enabled = true;
        }


        if (DelayBetweenAttacks > Time.fixedTime && AttackDuration < Time.fixedTime)
        {


            if (Anim != null)
            {
              //  Anim.SetBool("GrowingCircle", false);
                Anim.SetBool("Dashing", false);
            }


            if (AttackSine == null && Attacking && transform.Find("AttackPos")!=null)
            {
                AttackNum = Random.Range(0, _Attacktype.Count);

              

                AttackSine = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/AttackSines/" + _Attacktype[AttackNum].ToString()), transform.Find("AttackPos").transform);
                AttackSine.transform.position = transform.Find("AttackPos").position;
                AttackSine.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);

            }

          

            if (_Attacktype[AttackNum] != AttackType.Dashing )
            {
                if (DelayBetweenAttacks - 1 < Time.fixedTime && AttackBuildup < Time.fixedTime)
                {
                    AttackBuildup = Time.fixedTime + 1;
                }
            }

            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i] != null)
                {
                    if (Bullets[i].name.Contains("Lazer"))
                    {
                        if (Bullets[i] != null)
                        {
                         //   pl.BlowThisSmall(Bullets[i]);
                            
                            Bullets.RemoveAt(i);
                            BulletsSpeeds.RemoveAt(i);
                        }
                    }
                }
            }

           

        }

        if (AttackSine != null && Attacking)
        {
            AttackSine.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,
            1 - ((DelayBetweenAttacks - Time.fixedTime) / DelayBetweenAttacks_MAX) + 0.2f);
        }


        Animations();


        if (DelayBetweenAttacks < Time.fixedTime )
        {
           
            Destroy(AttackSine);

            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i] != null)
                {
                  


                    pl.BlowThisSmall(Bullets[i]);
             
             
                }
            }

            Bullets = new List<GameObject>();
            BulletsSpeeds = new List<Vector3>();

            Dashing = false;

            AttackList();

            DelayBetweenAttacks = Time.fixedTime + DelayBetweenAttacks_MAX;
        }


   

        BulletControll();

        if (Attacking)
        {
            DamageConroll();
            AnimationControll();
            Flip();
        }
    }



    void Move()
    {
        //if (HSpeed !=0 && VSpeed!=0)
        // {

        if (FaceZone != null)
        {
            if (FaceZone.GetComponent<CollList>().GetCollList().Count > 0)
            {
                if (Mathf.Abs(HSpeed) > Mathf.Abs(VSpeed) && VSpeed > 0)
                {
                    Dashing = false;
                    if(CanBeStunned)
                    StunnedTimer = Time.fixedTime + 2f;
                }
            }

          else  if (FaceUpZone.GetComponent<CollList>().GetCollList().Count > 0)
            {
                if (Mathf.Abs(VSpeed) > Mathf.Abs(HSpeed))
                {
                    Dashing = false;
                    if (CanBeStunned)
                        StunnedTimer = Time.fixedTime + 2f;
                }
            }

            else if (FaceDownZone.GetComponent<CollList>().GetCollList().Count > 0)
            {
                if (Mathf.Abs(VSpeed) > Mathf.Abs(HSpeed) && VSpeed<0)
                {
                    Dashing = false;
                    if (CanBeStunned)
                        StunnedTimer = Time.fixedTime + 2f;
                }
            }

        }

            if (AttackDuration < Time.fixedTime)
            {
                if (pl.transform.position.x - _transform.position.x > 0.1f)
                {
                    HSpeed = StartSpeed.x;

                }

                if (_transform.position.x - pl.transform.position.x > 0.1f)
                {
                    HSpeed = -StartSpeed.x;

                }


                if (_transform.position.y < pl.transform.position.y)
                {
                    VSpeed = StartSpeed.y;

                }

                if (_transform.position.y >= pl.transform.position.y)
                {
                    VSpeed = -StartSpeed.y;

                }

             if (Mathf.Abs(_transform.position.y - pl.transform.position.y) < 0.2f) VSpeed = 0;
             if (Mathf.Abs(_transform.position.x - pl.transform.position.x) < 0.2f) HSpeed = 0;

            }


            _transform.position += new Vector3(HSpeed / 10, VSpeed / 10, 0) * Time.deltaTime;
        //}
    }
    void AttackList()
    {

        if (pl.StartLoading)
            return;

        if (!Attacking)
            return;


            if (_Attacktype[AttackNum] == AttackType.Dashing)
                DashAttack();

            if (_Attacktype[AttackNum] == AttackType.ShootingStraight)
                ShootAttack();

            if (_Attacktype[AttackNum] == AttackType.ShootingThree)
                ShootAttackThree();

            if (_Attacktype[AttackNum] == AttackType.ShootingCircle)
                ShootCircleAttack();

            if (_Attacktype[AttackNum] == AttackType.Screaming)
                ScreamAttack();

            if (_Attacktype[AttackNum] == AttackType.Lazers)
                ShootLazers();

            if (_Attacktype[AttackNum] == AttackType.SpawnEnemies)
                Spawn();

            if (_Attacktype[AttackNum] == AttackType.CirclingSpin)
                CirclingAttack();

            if (_Attacktype[AttackNum] == AttackType.Volcano)
                VolcanoAttack();

            if (_Attacktype[AttackNum] == AttackType.GrowingCircle)
                GrowingCircleAttack();

            if (_Attacktype[AttackNum] == AttackType.ElectroSphere)
                ShootElectroSphere();


            if (_Attacktype[AttackNum] == AttackType.TeleportAndHit)
                TeleportAndHit();

            if (_Attacktype[AttackNum] == AttackType.FastDash)
                FastDash();

        if (_Attacktype[AttackNum] == AttackType.DropRocks)
            ShootDropRocks();

        if (_Attacktype[AttackNum] == AttackType.FollowBullet)
            ShootFollowBullet();

    }
    void AnimationControll()
    {
        if ( AttackDuration < Time.fixedTime )
        {
            Dashing = false;

            if (Anim != null && AttackBuildup < Time.fixedTime)
            {
              
                    Anim.SetBool("Walk", true);
                SetFalseAllAnimState();
            }
        }

        if (Anim != null && AttackBuildup < Time.fixedTime)
        {
          

            Anim.SetBool("Walk", true);
            SetFalseAllAnimState();

        }
    }


    void Animations()
    {
        if (AttackBuildup <= Time.fixedTime || !Attacking)
        {
            return;
        }
        if (Anim == null)
        {
            return;
        }


                if (_Attacktype[AttackNum] == AttackType.ShootingCircle)
                {
                   
                 SetAnimState("Shooting");
                }

        if (_Attacktype[AttackNum] == AttackType.ShootingStraight)
        {

            SetAnimState("Shooting");
        }

        if (_Attacktype[AttackNum] == AttackType.ShootingThree)
        {
            SetAnimState("Shooting3");

        }

        if (_Attacktype[AttackNum] == AttackType.GrowingCircle)
                {
                    
                SetAnimState("GrowingCircle");

                }


                if (_Attacktype[AttackNum] == AttackType.ElectroSphere)
                {
            SetAnimState("ElectroSphere");
            
                }


                if (_Attacktype[AttackNum] == AttackType.Lazers)
                {
            SetAnimState("Lazers");
                   
                }

                if (_Attacktype[AttackNum] == AttackType.Volcano)
                {
            SetAnimState("Volcano");
            
                }

                if (_Attacktype[AttackNum] == AttackType.SpawnEnemies)
                {
            SetAnimState("SpawnEnemies");
            

                }
                if (_Attacktype[AttackNum] == AttackType.Screaming)
                {
            SetAnimState("Screaming");
            
                }

        if (_Attacktype[AttackNum] == AttackType.DropRocks)
        {
            SetAnimState("DropRocks");

        }

        if (_Attacktype[AttackNum] == AttackType.FollowBullet)
        {
            SetAnimState("FollowBullet");

        }

    }

    void DamageConroll()
    {

    
        /*
        if (pl.coll_obj.Contains(gameObject))
        {
            pl.MakeDamage(1);
        }


        if (HP <= 0)
        {

            GameObject DItem = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Item"));
            DItem.transform.position = transform.position;
            DItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetItemInDatabase(DropItem).itemNames[0]);
            DItem.GetComponent<GetItem>().item = DropItem;

            pl.menu.SL.ObjectsToDestroy.Add(gameObject.name);
            pl.BlowThis(gameObject);
        }


        if (pl.GetComponent<Gun>().GunOB != null)
        {
            if (pl.GetComponent<Gun>().GunOB.GetComponent<CollList>().GetCollList().Contains(gameObject) && DamageTime < Time.fixedTime)
            {
                HP--;
                DamageTime = Time.fixedTime + 0.5f;
            }
        }
        

        if (DamageTime > Time.fixedTime) GetComponent<SpriteRenderer>().material = WhiteMaterial;
        else GetComponent<SpriteRenderer>().material = StartMaterial;*/
    }


    private void Flip()
    {
        if (FaceZone.GetComponent<CollList>().GetCollList().Count > 0 && ((_transform.localScale.x < 0 && HSpeed < 0) || (_transform.localScale.x > 0 && HSpeed > 0)))
        {
            HSpeed = 0;
        }

        if (FaceDownZone.GetComponent<CollList>().GetCollList().Count > 0 && VSpeed < 0)
        {
            VSpeed = 0;
        }
        if (FaceUpZone.GetComponent<CollList>().GetCollList().Count > 0 && VSpeed < 0)
        {
            VSpeed = 0;
        }

        if (FaceUpZone.GetComponent<CollList>().GetCollList().Count > 0 && VSpeed > 0)
        {
            VSpeed = 0;
        }


        if (AttackDuration < Time.fixedTime)
        {
            if (pl.transform.position.x < _transform.position.x)
                side = -1;
            else
                side = 1;
        }


        if (StartSpeed.x == 0) side = 1;

           // _transform.localScale = new Vector3(side, _transform.localScale.y, _transform.localScale.z);
    }

    void BulletControll()
    {
        
        for (int i = 0; i < Bullets.Count; i++)
        {
            if (Bullets[i] != null)
            {

              
                if (Bullets[i].GetComponent<Bullet>() != null)
                {
                    float amplitude = 1;
                    float frequency = 10;
                    float phase = 0;


                    Bullets[i].GetComponent<Bullet>().WaveTime += Time.deltaTime;

                    float waveOffset = Mathf.Sin(Bullets[i].GetComponent<Bullet>().WaveTime * frequency + phase);

                    float waveXSpeed = amplitude * Mathf.Cos(frequency * Bullets[i].GetComponent<Bullet>().WaveTime) * waveOffset;
                    float waveYSpeed = amplitude * Mathf.Sin(frequency * Bullets[i].GetComponent<Bullet>().WaveTime) * waveOffset;


                    if (Bullets[i].GetComponent<Bullet>().Wavy)
                        Bullets[i].GetComponent<Bullet>().WavySpeed = new Vector3(waveXSpeed, waveYSpeed, 0);


                    
                        Bullets[i].GetComponent<Bullet>().MoveSpeed = BulletsSpeeds[i];

                    


                    Bullets[i].transform.position += (BulletsSpeeds[i] + Bullets[i].GetComponent<Bullet>().WavySpeed) * Time.deltaTime * 2;
                }
            }
        }


        
    }

    public void DashAttack()
    {


        if (AttackDuration < Time.fixedTime)
        {
            Debug.Log("DAASH");
            if (Anim != null)
            {
                Anim.SetBool("Walk", false);
                Anim.SetBool("Dashing", true);
            }


            HSpeed = (pl.transform.position.x - _transform.position.x) * 15;
            VSpeed = (pl.transform.position.y - _transform.position.y) * 15;
       

            if (HSpeed > 15) HSpeed = 15;
            if (HSpeed < -15) HSpeed = -15;

            if (VSpeed > 15) VSpeed = 15;
            if (VSpeed < -15) VSpeed = -15;
            
            Dashing = true;

            PlayAudio(DashingClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }

    }

    void FastDash()
    {

        if (AttackDuration < Time.fixedTime)
        {
            Debug.Log("DAASH");
            if (Anim != null)
            {
                Anim.SetBool("Walk", false);
                Anim.SetBool("Dashing", true);
            }

            HSpeed = (pl.transform.position.x - _transform.position.x) * 25;
            VSpeed = (pl.transform.position.y - _transform.position.y) * 25;


            if (HSpeed > 25) HSpeed = 25;
            if (HSpeed < -25) HSpeed = -25;

            if (VSpeed > 25) VSpeed = 25;
            if (VSpeed < -25) VSpeed = -25;

            Dashing = true;
            PlayAudio(DashingClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }
    }
    public void ScreamAttack()
    {

        if (AttackDuration >= Time.fixedTime)
        {
            return;
        }



            if (Anim != null)
                Anim.SetBool("Walk", false);

            HSpeed = (pl.transform.position.x - _transform.position.x) * 15;
            VSpeed = (pl.transform.position.y - _transform.position.y) * 15;

        

            for (float x = -1; x < 2; x+= 0.5f)
            {
                for (float y = -1; y < 2; y+=0.5f)
                {
                    bool br = false;
                    if (x == 0 && y == 0) br = true;
                    if (!br)
                    {
                        GameObject bullet = Instantiate(Bullet);
                        bullet.transform.position = _transform.position + new Vector3(x / 4, y / 4, 0);
                        bullet.name = "b " + x + y;
                        BulletsSpeeds.Add(new Vector2(x, y));
                        Bullets.Add(bullet);


                    }
                }
            }

            CreateEffect(ScreamingEffect);
            PlayAudio(ScreamingClips);
        

    }


    void ShootAttackThree()
    {

        if (AttackDuration < Time.fixedTime)
        {
            if (Anim != null)
                Anim.SetBool("Walk", false);

            Vector2 SP = new Vector2(pl.transform.position.x - _transform.position.x, pl.transform.position.y - _transform.position.y);


            Vector3 difference = pl.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

          
            float max = 2.5f;
            if (SP.x < -max) SP = new Vector2(-max, SP.y);
            if (SP.x > max) SP = new Vector2(max, SP.y);
            if (SP.y < -max) SP = new Vector2(SP.x, -max);
            if (SP.y > max) SP = new Vector2(SP.x, max);



            for (int i = -1; i < 2; i++)
            {
                GameObject bullet = Instantiate(Bullet);
                bullet.transform.position = _transform.position;
                bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90);

                if (AttackStartPos == null)
                    bullet.transform.position = _transform.position;
                else bullet.transform.position = AttackStartPos.transform.position;

                bullet.GetComponent<SpriteRenderer>().sortingOrder = 99 - i;

                float a = SP.x * Mathf.Cos(0.174532925f* i) - SP.y * Mathf.Sin(0.174532925f* i);
                float b = SP.x * Mathf.Sin(0.174532925f* i) + SP.y * Mathf.Cos(0.174532925f* i);

                BulletsSpeeds.Add(new Vector2(a, b));


                Bullets.Add(bullet);

                CreateEffect(ShootingStraightEffect);
                PlayAudio(ShootingStraightClips);
            }

            AttackDuration = Time.fixedTime + AttackDurationMax;
        }
    }




    public void ShootAttack()
    {

        if (AttackDuration < Time.fixedTime)
        {
            if (Anim != null)
                Anim.SetBool("Walk", false);
            
            GameObject bullet = Instantiate(Bullet);

            if (AttackStartPos == null)
                bullet.transform.position = _transform.position;
            else bullet.transform.position = AttackStartPos.transform.position;


            Vector2 SP = new Vector2(pl.transform.position.x - _transform.position.x, pl.transform.position.y - _transform.position.y);

            float max = 2;
            if (SP.x < -max) SP = new Vector2(-max, SP.y);
            if (SP.x > max) SP = new Vector2(max, SP.y);
            if (SP.y < -max) SP = new Vector2(SP.x, -max);
            if (SP.y > max) SP = new Vector2(SP.x, max);

            BulletsSpeeds.Add(new Vector2(SP.x, SP.y));


            Bullets.Add(bullet);

            CreateEffect(ShootingStraightEffect);
            PlayAudio(ShootingStraightClips);

            AttackDuration = Time.fixedTime + AttackDurationMax;
        }
        

    }


    public void ShootCircleAttack()
    {
      
        if (AttackDuration < Time.fixedTime)
        {
           
            
                print("NO Buildup");

                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        bool br = false;
                        if (x == 0 && y == 0) br = true;
                        if (!br)
                        {
                            GameObject bullet = Instantiate(Bullet);
                        bullet.transform.position = _transform.position + new Vector3(x / 2, y / 2, 0);
                            bullet.name = "b " + x + y;
                            BulletsSpeeds.Add(new Vector2(x, y));
                            Bullets.Add(bullet);


                        }
                    }
                }


                CreateEffect(ShootingCircleEffect);
                PlayAudio(ShootingCircleClips);

                AttackDuration = Time.fixedTime + 2;
            
        }


    }

    void ShootLazers()
    {
        if (AttackDuration < Time.fixedTime)
        {
           
                if (Anim != null)
                    Anim.SetBool("Walk", false);

                GameObject bullet = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullets/Lazer4"));
                    //bullet.transform.localRotation = Quaternion.Euler(0, 0, 90 * x);
                    bullet.transform.position = _transform.position;
                    bullet.name = "Lazer";
                   // bullet.GetComponent<MoveWithOther>().target = _transform;

                HSpeed = 0;
                VSpeed = 0;


                BulletsSpeeds.Add(new Vector2(0, 0));
                    Bullets.Add(bullet);
                
            

            CreateEffect(LazersEffect);
            PlayAudio(LazersClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }

    }

    void ShootDropRocks()
    {
        if (AttackDuration < Time.fixedTime)
        {

            if (Anim != null)
                Anim.SetBool("Walk", false);

            List<Vector3> Poss = new List<Vector3>();
            Poss.Add(pl.transform.position);

            for (int x = 0; x < 12; x++)
            {
                int RNDX = Random.Range(-4, 5);
                int RNDY = Random.Range(-4, 5);
                Poss.Add(transform.position + new Vector3(RNDX, RNDY, 0));
            }

            for (int x = 0; x < Poss.Count; x++)
            {
                GameObject bullet = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullets/RockDrop"));

                bullet.transform.position = Poss[x];
                bullet.name = "DropRocks";
       

                BulletsSpeeds.Add(new Vector2(0, 0));
                Bullets.Add(bullet);
            }



            HSpeed = 0;
            VSpeed = 0;


           



            CreateEffect(ShootingStraightEffect);
            PlayAudio(LazersClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }

    }

    void ShootFollowBullet()
    {
        if (AttackDuration < Time.fixedTime)
        {

            if (Anim != null)
                Anim.SetBool("Walk", false);
            float radius = 1;

            for (int x = 0; x < 5; x++)
            {
                GameObject bullet = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullets/FllowBullet"));

                bullet.transform.position = transform.position + new Vector3(radius * Mathf.Cos(0+30*x), radius * Mathf.Sin(0 + 30 * x));
                bullet.name = "FllowBullet";

               
                BulletsSpeeds.Add(new Vector2(0, 0));
                Bullets.Add(bullet);
                bullet.GetComponent<CharacterPath>().SpeedMultiplier = 35;
            }
            
            HSpeed = 0;
            VSpeed = 0;
            
            CreateEffect(ShootingStraightEffect);
            PlayAudio(LazersClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }

    }

    void ShootElectroSphere()
    {

        if (AttackDuration < Time.fixedTime)
        {
            if (Anim != null)
                Anim.SetBool("Walk", false);

            GameObject bullet = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullets/ElectroSphere"));
            bullet.transform.position = _transform.position;

            Vector2 SP = new Vector2(pl.transform.position.x - _transform.position.x, pl.transform.position.y - _transform.position.y);

            float max = 2;
            if (SP.x < -max) SP = new Vector2(-max, SP.y);
            if (SP.x > max) SP = new Vector2(max, SP.y);
            if (SP.y < -max) SP = new Vector2(SP.x, -max);
            if (SP.y > max) SP = new Vector2(SP.x, max);

            BulletsSpeeds.Add(new Vector2(SP.x, SP.y));


            Bullets.Add(bullet);

            CreateEffect(ShootingStraightEffect);
            PlayAudio(ShootingStraightClips);

            AttackDuration = Time.fixedTime + AttackDurationMax;
        }




    }

    void CirclingAttack()
    {
        if (AttackDuration < Time.fixedTime)
        {
        
                if (Anim != null)
                    Anim.SetBool("Walk", false);

                GameObject bullet = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullets/Circling"));
                bullet.transform.position = _transform.position;
                bullet.name = "Circling";
               // bullet.GetComponent<MoveWithOther>().target = _transform;

                HSpeed = 0;
                VSpeed = 0;
                
            


                BulletsSpeeds.Add(new Vector2(0, 0));
                Bullets.Add(bullet);


            CreateEffect(CirclingSpinEffect);
            PlayAudio(CirclingSpinClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }
    }


    void VolcanoAttack()
    {
        if (AttackDuration < Time.fixedTime)
        {
            for (int x = 0; x < 10; x++)
            {
                if (Anim != null)
                    Anim.SetBool("Walk", false);

                GameObject bullet = Instantiate(Bullet);
                bullet.transform.position = _transform.position;
                bullet.name = "Bullet";
                bullet.GetComponent<SpriteRenderer>().sortingOrder = 99-x;

                // bullet.GetComponent<MoveWithOther>().target = _transform;

                HSpeed = 0;
                VSpeed = 0;

                float side = 1;

                if (CM != null) side = CM.side;


                float RNDX = Random.Range(1 * CM.side, 3* CM.side);
                float RNDY = Random.Range(2, -3);

                if (RNDX == 0 && RNDY == 0)
                {
                    RNDX = 2 * transform.localScale.x;
                }

                BulletsSpeeds.Add(new Vector2(RNDX, RNDY));
                Bullets.Add(bullet);

            }

            CreateEffect(VolcanoEffect);
            PlayAudio(VolcanoClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }
    }


    void GrowingCircleAttack()
    {
        if (AttackDuration < Time.fixedTime)
        {

            if (Anim != null)
            {
                Anim.SetBool("Walk", false);
               // Anim.SetBool("GrowingCircle", true);
            }



                GameObject bullet = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullets/GrowingCircle"));
                bullet.transform.position = _transform.position;
                bullet.name = "GrowingCircle";
                // bullet.GetComponent<MoveWithOther>().target = _transform;

                HSpeed = 0;
                VSpeed = 0;

            if (CM != null) CM.enabled = false;


                BulletsSpeeds.Add(new Vector2(0, 0));
                Bullets.Add(bullet);




            PlayAudio(GrowingCircleClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }
    }


    void Spawn()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (AttackDuration < Time.fixedTime)
        {
            
                if (Anim != null)
                    Anim.SetBool("Walk", false);

                if (Enemies.Count > 6) return;

                GameObject en = Instantiate<GameObject>(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Length)]);

                en.transform.position = new Vector3(_transform.position.x , _transform.position.y, _transform.position.z);
                en.name = "Enemy"+ Enemies.Count;

           
            Enemies.Add(en);

                HSpeed = 0;
                VSpeed = 0;
                

            

           

            PlayAudio(SpawnEnemiesClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }

    }

    void TeleportAndHit()
    {
        if (AttackDuration < Time.fixedTime)
        {
           if(TeleportCollider == null)
            TeleportCollider = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/TeleportCollider"));

            Debug.Log("Teleport");
            if (Anim != null)
                Anim.SetBool("Walk", false);

            float dist = 0.5f;

            TeleportCollider.transform.position = pl.transform.position + new Vector3(0, dist, 0);

            if (TeleportCollider.GetComponent<CollList>().GetCollList().Count > 0)
            {
                TeleportCollider.transform.position = pl.transform.position + new Vector3(dist, dist, 0);
            }

            if (TeleportCollider.GetComponent<CollList>().GetCollList().Count > 0)
            {
                TeleportCollider.transform.position = pl.transform.position + new Vector3(-dist, dist, 0);
            }

            if (TeleportCollider.GetComponent<CollList>().GetCollList().Count > 0)
            {
                TeleportCollider.transform.position = pl.transform.position + new Vector3(-dist, -dist, 0);
            }

            transform.position = TeleportCollider.transform.position;


            PlayAudio(DashingClips);
            AttackDuration = Time.fixedTime + AttackDurationMax;
        }

    }

    void Stunned()
    {


        if (StunnedTimer > Time.fixedTime)
        {
            if (CM != null)
            {

                if (StunEffect == null && CM.MovePauseTimer < Time.fixedTime)
                {
                    StunEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/StunEffect"));
                    StunEffect.transform.position = _transform.position;
                }

                CM.MovePauseTimer = StunnedTimer;

            }
        }
    }
    void SetFalseAllAnimState()
    {
        
        for (int i = 0; i < AnimatorStates.Count; i++)
        {
           AnimatorStates[i].State = false;
           Anim.SetBool(AnimatorStates[i].Name, false);
        }

    }


    void SetAnimState(string _name)
    {
       
        for (int i = 0; i < AnimatorStates.Count; i++)
        {
            if (AnimatorStates[i].Name == _name)
            {
                AnimatorStates[i].State = true;
                Anim.SetBool(AnimatorStates[i].Name, true);


              

            }
            else
            {
                Anim.SetBool(AnimatorStates[i].Name, false);
                AnimatorStates[i].State = false;

               
            }
        }

    }



    public static float CalculateWave(float x)
    {
        float amplitude = 2;
        float frequency = 2 * Mathf.PI / 4; // Adjust the frequency as desired

        // Calculate the value of the wave function
        float value = amplitude * Mathf.Sin(frequency * x);

        return value;
    }

    void CreateEffect(GameObject g)
    {
        if (g != null)
        {
            GameObject Effect = Instantiate<GameObject>(g);
            Effect.transform.position = _transform.position;
        }
    }


    void PlayAudio(AudioClip[] AC)
    {
 

        GetComponent<AudioSource>().clip = AC[Random.Range(0, AC.Length)];
        GetComponent<AudioSource>().Play();


    }

}
