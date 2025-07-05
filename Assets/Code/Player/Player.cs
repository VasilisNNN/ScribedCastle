using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.EventSystems.StandaloneInputModule;





public class Player : MonoBehaviour {


    public bool CutSceenMode { get; set; }

    public int MaxHP { get; set; }


    public int HP { get; set; }
    public int Height { get; set; }

    public int MaxHunger { get; set; }
    public int Hunger { get; set; }

    public int MaxPlague { get; set; }
    public int Plague { get; set; }

    public int MaxStamina { get; set; }
    public int Stamina { get; set; }

    public int DamageAmount { get; set; }

    public int Payment { get; set; }
    public int LootItem { get; set; }

    public float Speed { get; set; }
    public float DashDuration { get; set; }
    public int Vision { get; set; }
    public int VisionBase { get; set; }
    public float VisionBlackFieldIncreasing = 0.5f;
    public float VisionRemoveBlackTopBorder = 2;
    public int VisionPlusOnDay = 0;


    public int Sniff { get; set; }
    public float StaminaRestore { get; set; }


    public int DamageAll { get; set; }

    public List<GameObject> coll_obj = new List<GameObject>();
    public List<GameObject> Characters = new List<GameObject>();

    public InputMode IM;


    private Animator PlayerAnim;
    private Animator Bodyanim;
    public GameObject MutateIntoThis { get; set; }

    public bool _gameover { get; set; }

    public Transform _transform { get; set; }

    public MenuCustom menu { get; set; }
    public DayAndNight DayNight { get; set; }

    
    private float StaminaTimer;
    public float MutationTimer { get; set; }
    public float PathRescan { get; set; }

    public float PathRescanBoundTimer { get; private set; }
    private Bounds RescanBound;

    private float DashTimer;

    public int YPos { get; set; }
  

    public float Invinc { get; private set; }

    public float MaxSeconds { get; set; }
    
    private List<AudioClip> DamageClips = new List<AudioClip>();

    private AudioClip  DashClip;


    private string StartLayer = "Pers";
    private string ForG = "ItemFG";
    public Inventory inv { get; set; }
    public Journal journal { get; set; }
    private UnityEngine.Audio.AudioMixer mg;
    private bool devmode;

    public float _normalHSpeed, _normalVSpeed;
    public int side { get; set; }

    public bool Chatting;
    public bool Attacking { get; set; }
    public GameObject ChattingObject;
    public GameObject CurrentGun;

    private CollList FaceZone, FaceDownZone, FaceUpZone, FaceZoneBack;

    private GameObject PlayerMask;
    private Material StartMaterial, WhiteMaterial;
    
    private AstarPath AP;
    private bool Dashback;

    public bool UnderAttack { get; set; }
    private AudioSource FightMusic;
    public List<GameObject> AttackingEnemies = new List<GameObject>();

    private GameObject ItemEffect;
    private Constructor _constr;
    private GameObject StatsObject;

    private Material mat;

    private GameObject Hunger_Scrollbar, HP_Scrollbar, Stamina_Scrollbar;

    public Sprite[] LegsSPRTS;

    public Camera MainCamera { get; set; }

    public GameObject MouseUI { get; set; }

    public List<GameObject> CollidingItems;
    public List<GameObject> CollidingCharacter;

    private float HungerAlpha, HungerAlphaSide;

    public bool StartLoading { get; set; }


    private List<GameObject> Hearts = new List<GameObject>();
    private List<GameObject> HeartsMax = new List<GameObject>();
    private List<GameObject> StaminaHearts = new List<GameObject>();
    private List<GameObject> StaminaHeartsMax = new List<GameObject>();
    private List<GameObject> HungerHearts = new List<GameObject>();
    private List<GameObject> HungerHeartsMax = new List<GameObject>();

    private RectTransform HPBG;
    private RectTransform HungerBG;
    private RectTransform StaminaBG;

    public bool SliderHPUI;
    public bool SeparateHeartsHPUI;
    public bool EnableAttackSoundIfEnemyInCamera;


    public int HungerDamage = 3;
    public float TimerOnTheScene { get; set; }

    public int CameraNormalSize = 2;

    public bool RestartSameLocationOnDeath;
    public bool RestartAllOnDeath;

    public bool Showdamage;
 

    public int SpeedMultiplier = 1;

 
    private float PixelPerStat;
  
    private SpriteRenderer Child_SPRT;

    public bool TEST { get; set; }

    private bool AddTestitems;
    public float FadeInDelay { get; set; }

    public bool CanFlip = true;
    private float HPSlotWidth = 20;
    private float HungerSlotWidth = 15;
    private float StaminaSlotWidth = 15;

    public new Vector2 FlippingRange = new Vector2(10, 7);
    [HideInInspector]
    public GameObject MouseOB;
    private Rigidbody2D rigidbody2D;

    public LayerSorting LayerSort;

    private void Awake()
    {
        LayerSort = GetComponent<LayerSorting>();

        TEST = true;
        rigidbody2D = GetComponent<Rigidbody2D>();
        DayNight = GameObject.Find("DayAndNight").GetComponent<DayAndNight>();
        inv = GetComponent<Inventory>();
        journal = GetComponent<Journal>();
       

        if (GameObject.Find("HPBG") != null)
        {
            HPBG = GameObject.Find("HPBG").GetComponent<RectTransform>();
            HungerBG = GameObject.Find("HungerBG").GetComponent<RectTransform>();
            StaminaBG = GameObject.Find("StaminaBG").GetComponent<RectTransform>();
        }


        HungerAlphaSide = 1;
        MouseUI = GameObject.Find("MouseUI");


        MouseOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/MouseOB"));
        MouseOB.name = "MouseOB";

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mat = Resources.Load<Material>("Shaders/FadeMaterial");
        if(transform.Find("FightMusic")!=null)
        FightMusic = transform.Find("FightMusic").GetComponent<AudioSource>();
        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();
      
        if (GameObject.Find("PathFinding")!=null)
        AP = GameObject.Find("PathFinding").GetComponent<AstarPath>();

        Speed = 1;
        DashDuration = 0.6f;
  

        StartMaterial = GetComponent<SpriteRenderer>().material;
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");

        FaceZone = transform.Find("FaceZone").gameObject.GetComponent<CollList>();
        FaceZoneBack = transform.Find("FaceZoneBack").gameObject.GetComponent<CollList>();
        FaceDownZone = transform.Find("FaceDownZone").gameObject.GetComponent<CollList>();
        FaceUpZone = transform.Find("FaceUpZone").gameObject.GetComponent<CollList>();
        side = 1;
        mg = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/NewAudioMixer");
      
        MaxStamina = 5;

        Stamina = MaxStamina;
        DashClip = Resources.Load<AudioClip>("Sound/UI/Dash");
        YPos = 0;
        DamageClips.Add(Resources.Load<AudioClip>("Sound/Hits/Player_Get_Damage_0"));

        if (SceneManager.GetActiveScene().name == "CutSceen") CutSceenMode = true;
        
        _transform = transform;
        PlayerAnim = GetComponent<Animator>();
        Bodyanim = transform.Find("Body").Find("BodySPRT").GetComponent<Animator>();


        menu = GameObject.Find("Constructor").GetComponent<MenuCustom>();
     
        if(_constr.GetComponent<InputMode>()!=null)
        IM = _constr.GetComponent<InputMode>();
        else IM = GetComponent<InputMode>();


        MaxSeconds = 100;
        GameObject ChattingUIObject = GameObject.Find("Chatting");


        PlayerMask = GameObject.Find("PlayerMask");
        StatsObject = GameObject.Find("Stats");

        Hunger_Scrollbar = StatsObject.transform.Find("Hunger_Scrollbar").gameObject;
        HP_Scrollbar = StatsObject.transform.Find("HP_Scrollbar").gameObject;
        Stamina_Scrollbar = StatsObject.transform.Find("Stamina_Scrollbar").gameObject;
     


    }

  



    void Update()
    {
        if (!AddTestitems && TEST)
        {

            inv.AddItem(9, 9999, 99, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));
            inv.AddItem(980, 999, 99, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));
            inv.AddItem(1000, 999, 99, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));
            inv.AddItem(1015, 999, 99, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));

            AddTestitems = true;

        }



        if (!StartLoading)
        TimerOnTheScene += Time.deltaTime;


        if (DayNight != null)
        {

           

            if (DayNight.Day_Cycle == DayAndNight.DayCycle.Day)
                Vision = VisionBase + VisionPlusOnDay;
            else Vision = VisionBase + VisionPlusOnDay / 2;
        }
        else Vision = VisionBase;


        if (SeparateHeartsHPUI)
        DrawHPParts();

        if (SliderHPUI)
            DrawHPSliders();

        /*objectsInRange.Clear();

        colliders = Physics2D.OverlapBoxAll(MainCamera.transform.position, FlippingRange , 0f, LMask);

        for (int i=0; i < colliders.Length; i++)
        {
           
            if (colliders[i].gameObject.tag == "Flipping" || colliders[i].gameObject.tag == "Pers" || colliders[i].gameObject.tag == "Toilet")
            {
               
                objectsInRange.Add(colliders[i].gameObject);
            }
        }



       
      SequentialBatchProcessing(objectsInRange, 40);

        */

        // if (Input.GetKeyDown(KeyCode.LeftControl)&&Input.GetKeyDown(KeyCode.D))
        // devmode = !devmode;




        if (TEST)
        {

            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
            {

                for (int i = 0; i < inv.database.items.Count; i++)
                {
                    if (inv.database.items[i].CanStack)
                        inv.AddItem(inv.database.items[i].itemID, 999, inv.database.items[i].Durability, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));
                    else
                        inv.AddItem(inv.database.items[i].itemID, 1, inv.database.items[i].Durability, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));
                }

            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Equals))
            {

                inv.AddItem(9, 999, 99999, new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));


            }

        }



  


        if (_gameover)
        {
            print("GAMEOVER");
            PlayerAnim.SetBool("Death", true);

        }

        

        if (!CutSceenMode)
        {
            Anim();
        }

        MousePlayerMove();
        if (!_gameover && !menu.MenuONOFF && !_constr.Building && !StartLoading)
        {
            HandleInput();
         
        }

        Movement();

        if (PathRescan < 0.05 && PathRescan > 0)
        {
            if(!StartLoading)
                PathScan();
            else PathScan();

        }

      
        if (PathRescan > 0) PathRescan -= Time.deltaTime;
           else PathStopScan();


        if (PathRescanBoundTimer < 0.05 && PathRescanBoundTimer > 0)
        {
            AstarPath.active.UpdateGraphs(RescanBound);
            print("RescanBound " + RescanBound);
        }


        if (PathRescanBoundTimer > 0) PathRescanBoundTimer -= Time.deltaTime;
 

        if (PlayerMask != null)
        {
            mat.SetFloat("_MaskTargetX", PlayerMask.transform.position.x);
            mat.SetFloat("_MaskTargetY", PlayerMask.transform.position.y);
            mat.SetFloat("_RenderDistance", 2);
        }
    }




    void Movement()
    {


        if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().pitch = 1;

        if (StartLoading ||CutSceenMode|| _gameover || _constr.Building || menu.MenuONOFF || inv.blueprintshow || inv.showinvent || Attacking || inv.showjournal || inv.showinvent || _constr.ChooseMouseObject || MutationTimer > Time.fixedTime)
        {
            _normalHSpeed = _normalVSpeed = 0;
            rigidbody2D.velocity = new Vector3(_normalHSpeed * 2.5f, _normalVSpeed * 2.5f, 0);
            return;
        }

        if ( _constr.Game_SPEED > 0)
        {
            if (!IM.RightMouseButton)
            {
                if (DashTimer < Time.fixedTime)
                {
                    _normalHSpeed = IM._horizontal * SpeedMultiplier;
                    _normalVSpeed = IM._vertical * SpeedMultiplier;


                    if (Mathf.Abs(_normalVSpeed) > 0 && Mathf.Abs(_normalHSpeed) > 0)
                    {
                        _normalHSpeed = IM._horizontal / 1.4f * SpeedMultiplier;
                        _normalVSpeed = IM._vertical / 1.4f * SpeedMultiplier;
                    }
                }
            }
        }
        else _normalHSpeed = _normalVSpeed = 0;


        UnderAttackAudio();

        rigidbody2D.velocity = new Vector3(_normalHSpeed * 2.5f, _normalVSpeed * 2.5f, 0);
        if(CanFlip) Flip();
        

    }

    private void HandleInput()
    {
        CollRemoval();

        if (IM.Dash)
            DashHandler();
        DashStop();

    }







    void CollRemoval()
    {
        for (int i = 0; i < coll_obj.Count; i++)
        {
            if (coll_obj[i] != null)
            {
                if (coll_obj[i].GetComponent<BoxCollider2D>() != null)
                {

                    if (!coll_obj[i].GetComponent<BoxCollider2D>().enabled)
                    {

                        coll_obj.RemoveAt(i);
                    }

                } 
                
            }
            else coll_obj.RemoveAt(i);
        }
    }




    private void Flip()
    {

        if (DashTimer < Time.fixedTime && !Attacking)
        {
            if (!IM.joystick)
            {
                if (_normalHSpeed < 0)
                    side = -1;
                else if (_normalHSpeed > 0)
                    side = 1;
            }
            else
            {
                if (_normalHSpeed < -0.3)
                    side = -1;
                else if (_normalHSpeed > 0.3)
                    side = 1;
            }

            _transform.localScale = new Vector3(side, _transform.localScale.y, _transform.localScale.z);
        }
    }

  
    void DashAnimationAndSound(float DashTimerDuration, string anim_name)
    {
        PlayerAnim.SetBool("Walk", false);
        PlayerAnim.SetBool(anim_name, true);
        DashTimer = Time.fixedTime + DashTimerDuration;

     
        if (Stamina >= 2)
        {
            PlaySoundsPitched(DashClip, 1);
            ReduceStamina(-2);
        }
        else
        {
            PlaySoundsPitched(DashClip, 0.7f);
            ReduceStamina(-Stamina);
        }
    }


    private void Anim()
    {
        if (menu.MenuONOFF) return;

       
        if (_transform.Find("Vision") != null)
        {
            _transform.Find("Vision").transform.localScale = new Vector3(0.6f + (Vision * VisionBlackFieldIncreasing), 0.6f + (Vision * VisionBlackFieldIncreasing), 1);
            if (Vision == VisionRemoveBlackTopBorder) _transform.Find("Vision").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            else _transform.Find("Vision").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }


        if (Invinc - 0.8f > Time.fixedTime)
            _constr.SetMaterial(gameObject, WhiteMaterial);
        else _constr.SetMaterial(gameObject, StartMaterial);


        if (Invinc > Time.fixedTime && Invinc - 0.8f < Time.fixedTime)
        {
            _constr.SetColorAndAlpha(gameObject, new Color(1, 1, 1, 0.6f));
            
        }

        if (Invinc > Time.fixedTime && Invinc < Time.fixedTime+0.05f)
            _constr.SetColorAndAlpha(gameObject, new Color(1, 1, 1, 1));

        if (Invinc < Time.fixedTime )
            _constr.SetColorAndAlpha(gameObject, new Color(1, 1, 1, 1));

        /*for (int i = 0; i < expl.Length; i++)
        {
            if (coll_obj.Contains(expl[i]))
            {
                anim.SetBool("Death", true);
            }
        }*/

        if (Bodyanim != null)
        {
            if (MutationTimer > Time.fixedTime)
            {
                Bodyanim.SetBool("Mutate", true);

            }
            else Bodyanim.SetBool("Mutate", false);
        }

        if (DashTimer < Time.fixedTime)
        {
           

            if (_normalHSpeed != 0 || _normalVSpeed != 0)
            {
                PlayerAnim.SetBool("Walk", true);
                
            }
            else
            {
                PlayerAnim.SetBool("Walk", false);
            }
        }

    
      
        
        if (StaminaTimer < Time.fixedTime && Stamina< MaxStamina)
        {
            ReduceStamina(1);

        }

        if (DashTimer < Time.fixedTime)
        {
            Dashback = false;
            PlayerAnim.SetBool("Dash", false);
            PlayerAnim.SetBool("DashUp", false);
            PlayerAnim.SetBool("DashDown", false);
            PlayerAnim.SetBool("DashBack", false);
        }
        





    }

    void DashStop()
    {
        if (!Dashback)
        {

            if (FaceZone.GetCollList().Count > 0 && (DashTimer > Time.fixedTime))
            {

                _normalHSpeed = 0;

            }
        }
        if (FaceDownZone.GetCollList().Count > 0 && _normalVSpeed < 0)
        {

            _normalVSpeed = 0;

        }
        if (FaceUpZone.GetCollList().Count > 0 && _normalVSpeed > 0)
        {
            _normalVSpeed = 0;

        }

    }

    void DashHandler()
    {

        int DashSpeed = 5;
        float DashTimerDuration = 0.15f * DashDuration;
        if (Stamina < 2)
        {
            DashSpeed = 1;
            DashTimerDuration = 0.05f * DashDuration;
        }

        if (_normalHSpeed == 0 && _normalVSpeed == 0 && DashTimer < Time.fixedTime)
        {

            Invinc = Time.fixedTime + 0.3f * DashDuration;
            Dashback = true;
            if (FaceZoneBack.GetCollList().Count == 0)
            {

                _normalHSpeed = DashSpeed * _transform.localScale.x * -1;
                _normalVSpeed = 0;
            }


            DashAnimationAndSound(DashTimerDuration, "DashBack");
        }

        if (Mathf.Abs(_normalHSpeed) >= Mathf.Abs(_normalVSpeed) && _normalHSpeed != 0 && DashTimer < Time.fixedTime)
        {


            if (FaceZone.GetCollList().Count == 0)
            {

                Invinc = Time.fixedTime + 0.3f * DashDuration;
                _normalHSpeed = DashSpeed * _transform.localScale.x;

            }

            DashAnimationAndSound(DashTimerDuration, "Dash");

        }

        if (Mathf.Abs(_normalHSpeed) < Mathf.Abs(_normalVSpeed) && _normalVSpeed < 0 && DashTimer < Time.fixedTime)
        {


            if (FaceDownZone.GetComponent<CollList>().GetCollList().Count == 0)
            {
                print("DOWN2");
                Invinc = Time.fixedTime + 0.3f * DashDuration;
                _normalVSpeed = -DashSpeed;
            }

            DashAnimationAndSound(DashTimerDuration, "DashDown");
        }

        if (Mathf.Abs(_normalHSpeed) < Mathf.Abs(_normalVSpeed) && _normalVSpeed > 0 && DashTimer < Time.fixedTime)
        {

            if (FaceUpZone.GetCollList().Count == 0)
            {

                Invinc = Time.fixedTime + 0.3f * DashDuration;
                _normalVSpeed = DashSpeed;
            }


            DashAnimationAndSound(DashTimerDuration, "DashUp");
        }
    }
   
    public void BlowThisSmall(GameObject g)
    {
   
        if (g == null) return;

       
        GameObject blow;
        blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Small"));
        blow.transform.position = g.transform.position;


        if (g.GetComponent<Trail>() != null)
        {
            
                for (int j = 0; j < g.GetComponent<Trail>().ObjList.Count; j++)
                {
                    Destroy(g.GetComponent<Trail>().ObjList[j]);
                }
                

        }

        Destroy(g);
        LayerSort.ResetFlippingObjects();
    }

    public void BlowThis(GameObject g)
    {
        
        GameObject blow = null;
           

        if (g.GetComponent<Character>() != null)
        {

            if (g.GetComponent<Character>()._SoundType == Character.SoundType.Wood)
                blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Wood_Effect"));
            else if (g.GetComponent<Character>()._SoundType == Character.SoundType.Flesh)
                blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Flesh_Effect"));
            else 
                blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Flesh_Effect"));

        }
        else if (g.GetComponent<StatsControll>() != null)
        {
            if (g.GetComponent<StatsControll>()._SoundType == StatsControll.SoundType.Wood)
                blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Wood_Effect"));
        else if (g.GetComponent<StatsControll>()._SoundType == StatsControll.SoundType.Flesh)
            blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Flesh_Effect"));

        else if(g.GetComponent<StatsControll>()._SoundType == StatsControll.SoundType.Silent)
                blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Pop"));

            else blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion"));
        }
        else

            blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion"));

        if (blow != null)
        {
            if (GameObject.Find("Explosion") != null)
            {

                blow.GetComponent<AudioSource>().enabled = false;
            }

            blow.transform.position = g.transform.position;

            blow.name = "Explosion";
        }

        if (g.layer == 8)
        {
            if (g.GetComponent<BoxCollider2D>() != null)
            RescanInBounds(g.GetComponent<BoxCollider2D>().bounds);
            else PathRescan = 1;
        }


        Destroy(g);
        LayerSort.ResetFlippingObjects();
    }


    public void PathScan()
    {
        print("PathScan");

        if (AP != null) AP.Scan();
    }

    void PathScanAsync()
    {
        print("PathScanAsync");

        if (AP != null) AP.ScanAsync();
    }

    void PathStopScan()
    {
        if (AP != null) AP.StopAllCoroutines();
    }



  


    public void PlayHandsAudio(AudioClip AC, int pitch)
    {
        GetComponent<AudioSource>().pitch = pitch;
        GetComponent<AudioSource>().clip = AC;
        GetComponent<AudioSource>().Play();
    }



 
    

    public void PlaySoundsPitched(AudioClip AC,float pitch)
    {
      

        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = AC;
            GetComponent<AudioSource>().pitch = pitch;
            GetComponent<AudioSource>().Play();
        }
    }



    void DrawHPParts()
    {
     
        if (inv.crafting || inv.blueprintshow) return;


        if (HP <= 0 && !inv.showinvent)
        {
            /*if (!_gameover)
            {
                PlayHandsAudio(DeathClip, 1);
                if (FightMusic != null)
                    FightMusic.Stop();
                _gameover = true;
            }*/
        }

        if (Stamina_Scrollbar != null)
        {
            Destroy(HP_Scrollbar);
            Destroy(Hunger_Scrollbar);
            Destroy(StatsObject.transform.Find("Plague_Scrollbar").gameObject);
            Destroy(Stamina_Scrollbar);
        }


        if (_gameover)
        {
            if (IM.enter_b)
            {
                if (!SceneManager.GetActiveScene().name.Contains("Boss"))
                {
                    if(RestartSameLocationOnDeath)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);


                    if (RestartAllOnDeath)
                    {
                        menu.DrawTutorial = 0;
                        menu.FirstStart = 0;
                    

                        menu.SL.ResetLocations();

                        menu.TransitionToTheScene(menu.StartLocation, false);
                        
                    }
                }
                else
                {
                    if (RestartAllOnDeath)
                    {
                        menu.DrawTutorial = 0;
                        menu.FirstStart = 0;
               

                        menu.SL.ResetLocations();
                     
                        menu.TransitionToTheScene(menu.StartLocation, false);

                    }

                    if (RestartSameLocationOnDeath)
                    {
                        if (menu.SL.LastLocation != null)
                        {
                            if (menu.SL.LastLocation.Length > 0)
                                SceneManager.LoadScene(menu.SL.LastLocation);
                            else SceneManager.LoadScene(menu.StartLocation);
                        }
                        else SceneManager.LoadScene(menu.StartLocation);
                    }
                }
            }


            if (GameObject.Find("GomeoverOB") == null)
            {

                GameObject GomeoverOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Gameover"), GameObject.Find("Canvas").transform);
                GomeoverOB.name = "GomeoverOB";
            }
        }


        Vector2 pos = new Vector2(Screen.width / 9, Screen.height - Screen.height / 10);
        float switchdiv = 1;
#if UNITY_SWITCH
        switchdiv = 1.2f;
#endif
       
        CreateHeartsMax(HeartsMax, MaxHP, Resources.Load<GameObject>("Prefabs/UI/HPMax"), switchdiv);
        CreateHeartsMax(StaminaHeartsMax, MaxStamina, Resources.Load<GameObject>("Prefabs/UI/Stamina"), switchdiv);

        if(HungerDamage>0)
        CreateHeartsMax(HungerHeartsMax, MaxHunger, Resources.Load<GameObject>("Prefabs/UI/Hunger"), switchdiv);

        for (int h = 0; h < Hunger; h++)
        {
            if (HungerHearts.Count < Hunger)
            {
                GameObject H = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Hunger"), StatsObject.transform);
                HungerSlotWidth = H.GetComponent<RectTransform>().sizeDelta.x/ switchdiv;

                HungerHearts.Add(H);


            }
        }

        for (int h = 0; h < Stamina; h++)
        {
            if (StaminaHearts.Count < Stamina)
            {
                GameObject H = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Stamina"), StatsObject.transform);
                StaminaSlotWidth = H.GetComponent<RectTransform>().sizeDelta.x/ switchdiv;
                StaminaHearts.Add(H);

            }
        }

        RectTransform HPIcon_RectT = StatsObject.transform.Find("HPIcon").GetComponent<RectTransform>();

        RectTransform HungerIcon_RectT = StatsObject.transform.Find("HungerIcon").GetComponent<RectTransform>();
        RectTransform StaminaIcon_RectT = StatsObject.transform.Find("StaminaIcon").GetComponent<RectTransform>();

#if UNITY_SWITCH
        
         float HPSlotDist = 0;

         float HungerSlotDist = 0;

         float StaminaSlotDist = 0;

         float div = 1.25f;


         float HPBGWidth = (HPSlotWidth * 1.5f + HPSlotDist) * (MaxHP);
         float HungerBGWidth = (HungerSlotWidth*1.5f + HungerSlotDist) * (MaxHunger);
         float StaminaBGWidth = (StaminaSlotWidth*1.5f + StaminaSlotDist) * (MaxStamina) ;

        
         HPBG.sizeDelta = new Vector2(HPBGWidth + (HPSlotWidth / div) * 3, HPBG.sizeDelta.y);
         HungerBG.sizeDelta = new Vector2(HungerBGWidth + (HungerSlotWidth / div) * 3 , HungerBG.sizeDelta.y);
         StaminaBG.sizeDelta = new Vector2(StaminaBGWidth + (StaminaSlotWidth / div) * 3 , StaminaBG.sizeDelta.y);

#else



        float HPSlotDist = HPSlotWidth/6;

      
        float HungerSlotDist = HungerSlotWidth/3;

      
        float StaminaSlotDist = StaminaSlotWidth/3;

        float div = 1.25f;

      
        HPBG.sizeDelta = new Vector2((HPSlotWidth + HPSlotDist) * MaxHP + (HPSlotWidth / div) * 2 + HPSlotDist * 2, HPBG.sizeDelta.y);
        HungerBG.sizeDelta = new Vector2((HungerSlotWidth + HungerSlotDist) * MaxHunger + (HungerSlotWidth / div) *2 + HungerSlotDist * 2, HungerBG.sizeDelta.y);
        StaminaBG.sizeDelta = new Vector2((StaminaSlotWidth + StaminaSlotDist) * MaxStamina + (StaminaSlotWidth / div) * 2 + StaminaSlotDist * 2, StaminaBG.sizeDelta.y);


#endif



        HPBG.position = new Vector3(HPIcon_RectT.position.x + HPIcon_RectT.sizeDelta.x / 1.5f - HPSlotWidth/ div- HPSlotDist, HPIcon_RectT.position.y, 1);
        HungerBG.position = new Vector3(HungerIcon_RectT.position.x + HungerIcon_RectT.sizeDelta.x/ 1.5f - HungerSlotWidth/ div - HungerSlotDist, HungerIcon_RectT.position.y, 1);
        StaminaBG.position = new Vector3(StaminaIcon_RectT.position.x + StaminaIcon_RectT.sizeDelta.x/ 1.5f - StaminaSlotWidth/ div - StaminaSlotDist, StaminaIcon_RectT.position.y, 1);


        
        for (int h = 0; h < HeartsMax.Count; h++)
        HeartsMax[h].GetComponent<RectTransform>().position = new Vector3(HPIcon_RectT.position.x + HPIcon_RectT.sizeDelta.x / 1.5f + h * (HPSlotWidth + HPSlotDist), pos.y, 1);

        for (int i = 0; i < HungerHeartsMax.Count; i++)
        HungerHeartsMax[i].GetComponent<RectTransform>().position = new Vector3(HungerIcon_RectT.position.x + HungerIcon_RectT.sizeDelta.x / 1.5f + (HungerSlotWidth + HungerSlotDist) * i, HungerIcon_RectT.position.y, 1);

        for (int i = 0; i < StaminaHeartsMax.Count; i++)
        StaminaHeartsMax[i].GetComponent<RectTransform>().position = new Vector3(StaminaIcon_RectT.position.x + StaminaIcon_RectT.sizeDelta.x / 1.5f + (StaminaSlotWidth + StaminaSlotDist) * i, StaminaIcon_RectT.position.y, 1);


        DestroyOvervalueMaxHearts(HeartsMax, MaxHP);
        DestroyOvervalueMaxHearts(StaminaHeartsMax, MaxStamina);
        DestroyOvervalueMaxHearts(HungerHeartsMax, MaxHunger);


        if (Hearts.Count < HP)
        {
            GameObject H = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/HP"), StatsObject.transform);
           
            H.GetComponent<RectTransform>().position = new Vector3(HPIcon_RectT.position.x + HPIcon_RectT.sizeDelta.x/1.5f + Hearts.Count * (HPSlotWidth + HPSlotDist), pos.y, 1);
            H.GetComponent<RectTransform>().sizeDelta = new Vector3(HPSlotWidth, HPSlotWidth, 1);

            Hearts.Add(H);
            
            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].GetComponent<RectTransform>().position = new Vector3(HPIcon_RectT.position.x + HPIcon_RectT.sizeDelta.x/1.5f + i * (HPSlotWidth + HPSlotDist), HPIcon_RectT.position.y, 1);

            }

        }

       


        if (Hearts.Count > HP)
        {
            int hcount = Hearts.Count - 1;

            for (int i = hcount; i >= HP; i--)
            {
                // if (i > Hearts.Count - 1) break;
                if (i < Hearts.Count && Hearts.Count > 0)
                {
                    if (Hearts[i] != null)
                        Destroy(Hearts[i]);
                    Hearts.RemoveAt(Hearts.Count - 1);

                }


            }

        }

        for (int i = 0; i < HungerHearts.Count; i++)
        {
            HungerHearts[i].GetComponent<RectTransform>().position = new Vector3(HungerIcon_RectT.position.x + HungerIcon_RectT.sizeDelta.x/1.5f + (HungerSlotWidth + HungerSlotDist)*i, HungerIcon_RectT.position.y, 1);
           
        }

        if (HungerHearts.Count > Hunger)
        {
            int stcount = HungerHearts.Count - 1;

            for (int i = stcount; i >= Hunger; i--)
            {
                if (i < HungerHearts.Count && HungerHearts.Count > 0)
                {
                    Destroy(HungerHearts[i]);
                    HungerHearts.RemoveAt(HungerHearts.Count - 1);
                }

            }

        }


        for (int i = 0; i < StaminaHearts.Count; i++)
        {
           StaminaHearts[i].GetComponent<RectTransform>().position = new Vector3(StaminaIcon_RectT.position.x + StaminaIcon_RectT.sizeDelta.x/1.5f + (StaminaSlotWidth + StaminaSlotDist)*i , StaminaIcon_RectT.position.y, 1);
        }

        if (StaminaHearts.Count > Stamina)
        {
            int stcount = StaminaHearts.Count - 1;

            for (int i = stcount; i >= Stamina; i--)
            {
                if (i < StaminaHearts.Count && StaminaHearts.Count > 0)
                {
                    Destroy(StaminaHearts[i]);
                    StaminaHearts.RemoveAt(StaminaHearts.Count - 1);
                }

            }

        }

        

    }

    void DestroyOvervalueMaxHearts(List<GameObject> list, int maxvalue)
    {
        if (list.Count <= maxvalue) return;
        
        int hcount = list.Count - 1;

        for (int i = hcount; i >= maxvalue; i--)
        {
            
            if (i < list.Count && list.Count > 0)
            {
                if (list[i] != null)
                    Destroy(list[i]);
                HeartsMax.RemoveAt(list.Count - 1);

            }


        }
        
    }

    void CreateHeartsMax(List<GameObject> list, int maxcount, GameObject Heart_res, float switchdiv)
    {
        if (list.Count >= maxcount) return;
        
        int exmax = list.Count;
        for (int h = 0; h < (maxcount - exmax); h++)
        {
            GameObject H = Instantiate<GameObject>(Heart_res, StatsObject.transform);
            HPSlotWidth = H.GetComponent<RectTransform>().sizeDelta.x / switchdiv;
            H.GetComponent<Image>().color = new Color(0,0,0,1);
            list.Add(H);
        }
        
    }

     
    void UnderAttackAudio()
    {
        if (FightMusic == null) return;

    

        if (AttackingEnemies.Count > 0) UnderAttack = true;
        else UnderAttack = false;


        if (UnderAttack)
        {
            if (!FightMusic.isPlaying)
                FightMusic.Play();

            if (FightMusic.volume < 1) FightMusic.volume += Time.deltaTime;

            menu.mg.SetFloat("BG", -80);
        }
        else if (FightMusic.volume > 0)
        {


            float bg = 3 * (menu.BGSlider.value * 10) - 30;
            if (bg > 10) bg = 10;
            menu.mg.SetFloat("BG", bg);


            FightMusic.volume -= Time.deltaTime / 2;
        }
    }

    void DrawHPSliders()
    {

        if (HP <= 0 && !inv.showinvent)
        {
           /* if (!_gameover)
            {
                PlayHandsAudio(DeathClip, 1);
                if (FightMusic != null)
                    FightMusic.Stop();
                _gameover = true;
            }*/
        }



        //OLD DRAWING HEARTS PART

        /*
        if (Hearts.Count < HP)
        {
            GameObject H = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/HP"), GameObject.Find("Stats").transform);
            H.GetComponent<RectTransform>().position = new Vector3(GameObject.Find("Stats").GetComponent<RectTransform>().position.x + Hearts.Count * 0.5f, GameObject.Find("Stats").GetComponent<RectTransform>().position.y, 1);
            Hearts.Add(H);
        }

         if (Hearts.Count > HP)
         {
             int hcount = Hearts.Count-1;

             for (int i = hcount; i >= HP; i--)
             {
                 // if (i > Hearts.Count - 1) break;
                 if (i < Hearts.Count && Hearts.Count>0)
                 {
                     if (Hearts[i] != null)
                         Destroy(Hearts[i]);
                     Hearts.RemoveAt(Hearts.Count - 1);

                 }


             }

         }*/


        // NEW HP SLIDER

        PixelPerStat = 20;
        HP_Scrollbar.GetComponent<Scrollbar>().size = (float)HP / (float)MaxHP;

        if (HP_Scrollbar.transform.Find("Text") != null)
            HP_Scrollbar.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = HP + " / " + MaxHP;

        HP_Scrollbar.transform.Find("FG").GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHP * PixelPerStat, 40);
        HP_Scrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHP * PixelPerStat, 40);
     

        if (Hunger >= MaxHunger)
        {
            _constr.SetUIAlpha(Hunger_Scrollbar, HungerAlpha, 1 + HungerAlpha / 8);

            HungerAlpha += HungerAlphaSide * Time.deltaTime * 2;
            if (HungerAlpha > 1.2f) HungerAlphaSide = -1;
            if (HungerAlpha < 0.4) HungerAlphaSide = 1;
        }
        else
        {
            _constr.SetUIAlpha(Hunger_Scrollbar, 1, 1);

        }

        Hunger_Scrollbar.GetComponent<Scrollbar>().size = ((float)MaxHunger - (float)Hunger)/ (float)MaxHunger;
        Hunger_Scrollbar.transform.Find("FG").GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHunger * PixelPerStat, 40);
        Hunger_Scrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHunger * PixelPerStat, 40);

        if (MaxPlague > 0)
        {
            StatsObject.transform.Find("Plague_Scrollbar").GetComponent<Scrollbar>().size =  (float)Plague / (float)MaxPlague;
            StatsObject.transform.Find("Plague_Scrollbar").Find("FG").GetComponent<RectTransform>().sizeDelta = new Vector2(MaxPlague * PixelPerStat, 40);
            StatsObject.transform.Find("Plague_Scrollbar").GetComponent<RectTransform>().sizeDelta = new Vector2(MaxPlague * PixelPerStat, 40);
        }


        Stamina_Scrollbar.GetComponent<Scrollbar>().size = (float)Stamina / (float)MaxStamina;
        if(Stamina_Scrollbar.transform.Find("Text")!=null)
        Stamina_Scrollbar.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Stamina + " / " + MaxStamina;

        Stamina_Scrollbar.transform.Find("FG").GetComponent<RectTransform>().sizeDelta = new Vector2(MaxStamina * PixelPerStat, 40);
        Stamina_Scrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxStamina * PixelPerStat, 40);


        /* if (StaminaHearts.Count < Stamina)
         {
             GameObject H = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Stamina"), StatsObject.transform);
             H.GetComponent<RectTransform>().position = new Vector3(StatsObject.GetComponent<RectTransform>().position.x + StaminaHearts.Count * 0.5f, StatsObject.GetComponent<RectTransform>().position.y-1.6f, 1);
             StaminaHearts.Add(H);
         }*/


        if (StaminaHearts.Count > Stamina)
        {
            int stcount = StaminaHearts.Count-1;

            for (int i = stcount; i >= Stamina; i--)
            {
              
                Destroy(StaminaHearts[i]);
                StaminaHearts.RemoveAt(StaminaHearts.Count - 1);
                
                
            }

        }


        if (!_gameover)
        {
            return;
        }



        if (IM.enter_b)
        {
            if (RestartAllOnDeath)
            {
                menu.FirstStart = 0;
            }

            if (!SceneManager.GetActiveScene().name.Contains("Boss"))
                menu.TransitionToTheScene(SceneManager.GetActiveScene().name, false);
            
            else
            {
                if (menu.SL.LastLocation != null)
                {
                    if (menu.SL.LastLocation.Length > 0)
                        menu.TransitionToTheScene(menu.SL.LastLocation, false);
         
                    else  menu.TransitionToTheScene(menu.SL.LastLocation, false);
                   
                }
                else menu.TransitionToTheScene(menu.SL.LastLocation, false);
           
            }

         

        }


        if (GameObject.Find("GomeoverOB") == null)
        {

            GameObject GomeoverOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Gameover"), GameObject.Find("Canvas").transform);
            GomeoverOB.name = "GomeoverOB";
        }
        

    }

    public void Heal(int heal, string MagicEffectToCast)
    {
        if (MaxHP >= HP + heal)
            HP += heal;
        else HP = MaxHP;


        if (MagicEffectToCast != null)
        {
            ItemEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/" + MagicEffectToCast));

            ItemEffect.transform.position = _transform.position;
        }

    }

    public void Eating(int setiety, string MagicEffectToCast)
    {
        if (Hunger > 0)
            Hunger -= setiety;

        if (Hunger < 0) Hunger = 0;

       
        ItemEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/EatingEffect"));
       
        _constr.SetColorAndAlpha(gameObject, new Color(1, 1, 1, 1));

        ItemEffect.transform.position = _transform.position;

    }

    void MousePlayerMove()
    {
        if (IM.RightMouseButton)
        {
            Vector2 Move =  MainCamera.ScreenToWorldPoint(MouseUI.transform.position)  - _transform.position  ;
            
            _normalHSpeed = Move.normalized.x * SpeedMultiplier;
            _normalVSpeed = Move.normalized.y * SpeedMultiplier;

        }

        MouseOB.transform.position = MainCamera.ScreenToWorldPoint(MouseUI.transform.position);



    }

    public List<GameObject> GetMouseCollList()
    {

        return MouseUI.GetComponent<CollList>().coll_obj;
    }

    public List<GameObject> GetMouseOBCollList()
    {

        return MouseOB.GetComponent<CollList>().coll_obj;
    }

    
    public void RescanInBounds(Bounds bound)
    {
        AstarPath.active.UpdateGraphs(bound);

        RescanBound = bound;
        PathRescanBoundTimer = 0.06f;

    }


    public void ReduceStamina(int stam)
    {
        Stamina+= stam;


        StaminaTimer = Time.fixedTime + 1 - StaminaRestore / 10;
    }


    private void OnTriggerStay2D(Collider2D c)
	{
		
		if(!coll_obj.Contains(c.gameObject))
		{
            coll_obj.Add(c.gameObject);
		}
		
	}
	
	private void OnTriggerExit2D(Collider2D c)
	{
		
		if(coll_obj.Contains(c.gameObject))
		{
            coll_obj.Remove(c.gameObject);
		}
		
	}

}
