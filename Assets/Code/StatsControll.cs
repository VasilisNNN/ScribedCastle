using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using TMPro;
using Pathfinding;
using static UnityEngine.GraphicsBuffer;

public class StatsControll : MonoBehaviour
{
    public int HP = 3;
    public int Speed = 3;
    public int Damage = 0;
    #if UNITY_STANALONE
    [Separator]
     #endif
    public int Gun_ToKill_ID = -1;
    public int MinVision = -99;
    public int MaxVision = 99;

    public int MinSniff = -99;
    public int MaxSniff = 99;

    public int DatabaseID = -1;
    [HideInInspector]
    public int DatabaseOriginalID = -1;
    public bool AddItemAutomaticly;
    public int[] ItemIDs;
    public int ItemCount = 1;


    public bool RNDItemDrop;



    public enum SoundType { Wood, Metal, Flesh, Plant, Silent, NoSoundType };


    public SoundType _SoundType;

    public AudioClip DeathClip;


    public Sprite[] GrowingSprites;
    public float GrowDelay = 10;
    public float GrowTimer { get; set; }
    public int CurrentGrowState { get; set; }
    public bool StartGrowWithZero;

    public bool Destructible;
    public bool CanBeStunned;
    public bool CanTurnInEgg;


    public int MAXHP { get; set; }
    public float InvisTimer { get; set; }

    public Material StartMaterial { get; set; }

    private List<AudioClip> DamageClips = new List<AudioClip>();
    public List<AudioClip> CollisionClips = new List<AudioClip>();
    public bool AudioPlayed;

    private Constructor constr;
    private GameObject WallBrakeEffect;

    public bool GettingDamageFromWalls;
    private TileBase BrushesPit;
    public float AplhaColor { get; set; }

    public bool ReduceAlphaOnColl;
    public Material NewMaterialOnColl;

    public float CollMaterialTimer { get; set; }
    public bool StartColl { get; set; }
    

    public GameObject ChargeUI { get; set; }
    public GameObject HPUI { get; set; }
    public GameObject ComfortUI { get; set; }


    private float Alpha = 0;
    public bool Stunned { get; set; }

    public float StunnedDelay { get; set; }
    public GameObject MutationUI { get; set; }

    public bool Occupied { get; set; }
    public bool HasAChracter { get; set; }

    public int Durability { get; set; }
    
    private Player pl;
    private Inventory inv;
    private SpriteRenderer SR;
    private Gun gun;

    private int ConstructorID = -1;

    public bool CanBeHungry;
    public int HungerDamage = 1;
    public bool Friend;
    public float HungerTimer { get; set; }
    public bool CanSleep;

    public bool NeedsPayment;
    public int PaymentAmount = 1;
    private float PaymentTimer;
    public bool Payed { get; private set; }
    private Material StunMaterial;



   
    public int DurabilityMax = -1;
    private AudioClip WorkerRebirthClip;

    [HideInInspector]
    public int Satiety = 10;
    [HideInInspector]
    public int SatietyMax = 20;

    [HideInInspector]
    public bool Draw;
    [HideInInspector]
    public bool DrawUI;
    [HideInInspector]
    private bool DrawHP;
    [HideInInspector]
    public bool DrawVision;

    private MovementControll MC;
    private int buffi;
    private Tilemap FloorTM;
    private CollList Coll;


    private AudioSource AU;

    private CharacterMove CM;
    private Character Charac;
    private PubObject PO;

    [HideInInspector]
    public DrawIfActive DIA;

    public bool IgnoreDestroyList;
    private PubObject _PubObject;

    [HideInInspector]
    public bool BuildedStructure = false;
    private SpriteRenderer ChildSPRT;

    [HideInInspector]
    public string SpawnPointName;
    private Transform _transform;

    private Color StartColor;
    private void Awake()
    {


        _transform = transform;

        DrawVision = true;

        ChangeTheName();

        ObjectOnBoard OOB = new ObjectOnBoard(DatabaseID, transform.position, name, gameObject, GetComponent<StatsControll>(), GetComponent<PubObject>());

        bool InList = false;
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();
        inv = GameObject.Find("Player").GetComponent<Inventory>();

        for (int i = 0; i < constr.OBOnBoard.Count; i++)
        {
            if (constr.OBOnBoard[i].Object == gameObject) InList = true;

        }

        DatabaseOriginalID = inv.GetItemInDatabase(DatabaseID).OriginalitemID;

        if (transform.parent != null)
        {
            if (transform.parent.GetComponent<Blueprint>() != null)
            {
                InList = true;
            }
        }


        if (DatabaseID > -1 && transform.parent != constr.transform && !InList)
        {


            constr.OBOnBoard.Add(OOB);
            constr.OBOnBoard[constr.OBOnBoard.Count-1].Place = transform.position;
            

            ConstructorID = constr.OBOnBoard.Count - 1;
            if (GrowingSprites.Length > 0)
                CurrentGrowState = UnityEngine.Random.Range(1, GrowingSprites.Length);

            if (StartGrowWithZero) CurrentGrowState = 0;

        }


        if (DatabaseID > -1)
        {
            DurabilityMax = inv.GetItemInDatabase(DatabaseID).Durability;
            Durability = DurabilityMax;
        }


    }


    private void Start()
    {
        _PubObject = GetComponent<PubObject>();

        AU = GetComponent<AudioSource>();
        CM = GetComponent<CharacterMove>();
        if (CM != null)
            CM.SpeedMultiplier = Speed;

        Charac = GetComponent<Character>();
        PO = GetComponent<PubObject>();
        DIA = GetComponent<DrawIfActive>();

        FloorTM = GameObject.Find("Floor").GetComponent<Tilemap>();
        Coll = GetComponent<CollList>();


        SatietyMax = 20;
        Satiety = SatietyMax /3;

        if (!CanBeHungry) Satiety = 999;

        Draw = true;
        MC = GetComponent<MovementControll>();
        if(MC!=null)
        MC.MinDamage = Damage;



        PaymentTimer = Time.fixedTime + 700;
        Payed = true;
        StunMaterial = Resources.Load<Material>("Materials/DoodleHorizontal");
        WorkerRebirthClip = Resources.Load<AudioClip>("Sound/Objects/WorkerRebirth_1");

        SR = GetComponent<SpriteRenderer>();


        StartColor = SR.color;





        gun = GameObject.Find("Player").GetComponent<Gun>();




        if (CanBeStunned)
        {
            MutationUI = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/MutationUI"), GameObject.Find("Canvas").transform);
            MutationUI.SetActive(false);
        }



        if (constr.ShowChargeUI)
        { 
            if (constr.ShowChargeUI && GrowingSprites.Length > 0)
            {
               
                ChargeUI = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/ChargeUI"), GameObject.Find("Canvas").transform);

                ChargeUI.transform.SetAsFirstSibling();
                ChargeUI.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z));

                pl.inv.ONOFF(ChargeUI, false);
            }



            HPUI = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/HPUI"), GameObject.Find("Canvas").transform);
            HPUI.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f * 1.2f, transform.position.z));
            HPUI.transform.SetAsFirstSibling();

            pl.inv.ONOFF(HPUI, false);

            if (GetComponent<PubObject>() != null)
            {
                if (GetComponent<PubObject>().ComfortPlus != 0)
                {
                    ComfortUI = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/ComfortUI"), GameObject.Find("Canvas").transform);
                    ComfortUI.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f - 0.6f * 1.2f, transform.position.z));
                    ComfortUI.transform.SetAsFirstSibling();

                    pl.inv.ONOFF(ComfortUI, false);
                }
            }
        }



        AplhaColor = 1;
        if (GettingDamageFromWalls)
        {
            WallBrakeEffect = Resources.Load<GameObject>("Prefabs/Effects/WallBrakeEffect");
            BrushesPit = Resources.Load<TileBase>("Brushes/Pit");
        }

        MAXHP = HP;

        
        GrowTimer = Time.fixedTime + GrowDelay + UnityEngine.Random.Range(0, 6);

        if (SR != null)
        StartMaterial = SR.material;


        if (SR != null && GrowingSprites.Length > 0)
        {
            if (CurrentGrowState >= GrowingSprites.Length)
             CurrentGrowState = GrowingSprites.Length - 1;
            
            SR.sprite = GrowingSprites[CurrentGrowState];
        }

        if(DeathClip==null)
        SetDeathClip();

        //&& !constr.OBOnBoard.Contains(new ObjectOnBoard(DatabaseID, transform.position, inv.GetItemInDatabase(DatabaseID).itemNames[0], gameObject))

        // HungerTimer = pl.DayNight.DayLength / 1.5f;


        //if (GameObject.Find(name) != null && GameObject.Find(name) != gameObject) Destroy(gameObject);

    }

    void SetDeathClip()
    {
        
        if (_SoundType == SoundType.Wood)
        {
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Wood/Club/Club_On_Wood_Club_1_Short"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Wood/Club/Club_On_Wood_Club_2_Short"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Wood/Club/Club_On_Wood_Club_3_Short"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Wood/Club/Club_On_Wood_Club_4_Short"));

            DeathClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Wood/Wood/Sword_On_Wood_Wood_2");
        }

        if (_SoundType == SoundType.Metal)
        {
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Metal/Metal/Club_On_Metal_Metal_1_Short"));

            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Metal/Metal/Club_On_Metal_Metal_2_Short"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Metal/Metal/Club_On_Metal_Metal_3_Short"));

            DeathClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Metal/Sword_On_Metal_Metal_1");

        }

        if (_SoundType == SoundType.Flesh)
        {
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Flesh/Flesh/Club_On_Flesh_Flesh_1_Short"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Flesh/Flesh/Club_On_Flesh_Flesh_2_Short"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Flesh/Flesh/Club_On_Flesh_Flesh_3_Short"));

            DeathClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Flesh/Flesh/Sword_On_Flesh_Flesh_1_Short");

        }

        if (_SoundType == SoundType.Plant)
        {
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Flesh/Club/Club_On_Flesh_Club_1"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Flesh/Club/Club_On_Flesh_Club_2"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Sound Library - Battle/Club/Club On Flesh/Club/Club_On_Flesh_Club_0"));


            DeathClip = Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Impact/Sword_On_Metal_Impact_3");

        }

        if (_SoundType == SoundType.Silent)
        {
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Objects/Slurp"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Objects/Slurp"));
            DamageClips.Add(Resources.Load<AudioClip>("Sound/Objects/Slurp"));

            DeathClip = Resources.Load<AudioClip>("Sound/Objects/Slurp");

        }

        if (_SoundType == SoundType.NoSoundType)
        {
            DeathClip = null;

        }
    }
    public void ChangeTheName()
    {
        GameObject ob = GameObject.Find(name);
        if(ob == gameObject) ob = GameObject.Find(name);

        while (ob != null && ob != gameObject)
        {
            name += "N";
            ob = GameObject.Find(name);
        }
    }

    void SetColorToSPRT(SpriteRenderer SPRT, Material material)
    {
        if (SPRT == null) return;
        
            if (material != null)
                SR.material = material;


        Vector3 TargetPos = pl.MainCamera.transform.position;

    

        if (BuildedStructure && tag == "Flipping" && _transform.parent ==null)
        {

            if (_transform.position.y > TargetPos.y-2)
            {
                float Camdepth = ((_transform.position.y - 2) - (pl.MainCamera.transform.position.y - 2)) / 6;
                float Mousedepth = (_transform.position.y - pl.MainCamera.ScreenToWorldPoint(pl.IM.MousePosition).y) / 20;

                float colordepth = Camdepth + Mousedepth;


               // colordepth = Mathf.Clamp(colordepth, 0f, 0.25f);
                colordepth = 0;

                SR.color = new Color(StartColor.r - colordepth / 1.5f, StartColor.g - colordepth / 1.1f, StartColor.b - colordepth , Mathf.Lerp(SR.color.a, Alpha, Time.deltaTime * 10));
                for (int i = 0; i < transform.childCount; i++)
                    SetChildColor(material, i, new Color(StartColor.r - colordepth / 1.5f, StartColor.g - colordepth / 1.1f, StartColor.b - colordepth ));

            }
            else

            {
                SR.color = new Color(StartColor.r, StartColor.g, StartColor.b, Mathf.Lerp(SR.color.a, Alpha, Time.deltaTime * 10));
                for (int i = 0; i < transform.childCount; i++)
                    SetChildColor(material, i, StartColor);

            }
            
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
                SetChildColor(material, i, transform.GetChild(i).GetComponent<SpriteRenderer>().color);

            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.Lerp(SR.color.a, Alpha, Time.deltaTime * 10));
        }


        
    }


    void SetChildColor(Material material, int i, Color color)
    {
        if (transform.GetChild(i).GetComponent<SpriteRenderer>() == null) return;
            ChildSPRT = transform.GetChild(i).GetComponent<SpriteRenderer>();

        if (ChildSPRT == null || transform.GetChild(i).GetComponent<Blinking>() != null) return;

        if (material != null)
            ChildSPRT.material = material;

        if (transform.GetChild(i).name == "Base") return;
        
        ChildSPRT.color = new Color(color.r, color.g, color.b, Alpha);

        for (int j = 0; j < ChildSPRT.transform.childCount; j++)
        {
            if (ChildSPRT.transform.GetChild(j).GetComponent<SpriteRenderer>() != null)
                ChildSPRT.transform.GetChild(j).GetComponent<SpriteRenderer>().color = ChildSPRT.color;
        }

    }




    public void SetColorAndMaterial(float alpha, Material material)
    {
        Alpha = alpha;
        SetColorToSPRT(SR, material);

        if (!BuildedStructure) return;

        if (transform.localScale.x<1)
        transform.localScale = new Vector3(
            transform.localScale.x + Time.deltaTime,
            transform.localScale.y ,
            transform.localScale.z );
        
        if (transform.localScale.y < 1)
            transform.localScale = new Vector3(
                transform.localScale.x ,
                transform.localScale.y + Time.deltaTime,
                transform.localScale.z );

        if (transform.localScale.y > 1 || transform.localScale.x > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).name = transform.GetChild(i).name.Replace("(Clone)", null);
                transform.GetChild(i).localScale = transform.localScale;
                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().enabled = true;
                    GetComponent<Animator>().applyRootMotion = true;
        
                    GetComponent<Animator>().Rebind();
                    GetComponent<Animator>().Update(0f);
                }
            }
           


        }


    }


    public void GetDamage(int damage)
    {
        if (damage == 0) return;

        if (DamageClips.Count > 0)
        {
            int rnd = UnityEngine.Random.Range(0, DamageClips.Count);
            if (rnd >= DamageClips.Count) rnd = 0;
          
            PlaySoundsPitched(DamageClips[rnd], 1);
        }

        if(pl.Showdamage)
        inv.ADDPickedName(damage.ToString(),  1, transform.position);



        if (CanBeStunned && !Stunned && HP>0 && HP<=damage)
        {
            StunnedDelay = Time.fixedTime + 8;
            if (MutationUI != null)
                MutationUI.SetActive(true);
            Stunned = true;
            InvisTimer = Time.fixedTime + 2;
            return;
        }


        HP -= damage;

      
        InvisTimer = Time.fixedTime + 1;

    }

    public void CollisionAudio()
    {
        if (CollisionClips.Count > 0 && AU != null)
        {
            AU.clip = CollisionClips[UnityEngine.Random.Range(0, CollisionClips.Count)];
            AU.Play();
        }

    }

    public void HungerConroll()
    {
       

        if (!CanBeHungry)
        {
            return;
        }

        HungerTimer -= Time.deltaTime;
       
        if (HungerTimer <= 0)
        {
            if (Satiety > 0) Satiety--;
            
            HungerTimer = (pl.DayNight.DayLength / (2f* (float)SatietyMax)) ;

        }

        

    }


    public void PaymentConroll()
    {
        if (!NeedsPayment)
        {
            return;
        }

        if (!Payed)
        {
            if (pl.inv.CheckItem(9))
            {
                constr.AddLogPart("You payed sallery (-1 Gold)!", "Ви заплатили зарплату (-1 Золото)", "あなたはギャラリー（-1金）を支払った！", gameObject);

                pl.inv.ReduceItemCount(9, PaymentAmount);
                Payed = true;

            }
        }


        if (PaymentTimer < Time.fixedTime)
        {
            if (pl.inv.GetItem(9) != null)
            {
                constr.AddLogPart("You payed sallery (" + PaymentAmount + " Gold)!", "Ви заплатили зарплату (" + PaymentAmount + " Золото)", "あなたはギャラリー（" + PaymentAmount + "金）を支払った！", gameObject);

                pl.inv.ReduceItemCount(9, PaymentAmount);
                Payed = true;

            }
            else
            {
                constr.AddLogPart("A worker needs payment (" + PaymentAmount + " Gold) and you dont have it!", "У вас недостатньо золота для зарплати. Треба " + PaymentAmount + " Золота", "労働者が支払い（" + PaymentAmount + " 金）を必要としているのですが、あなたはそれを持っていません！", gameObject);

                Payed = false;
            }

            PaymentTimer = Time.fixedTime + 700;
        }
        

    }


    public void GrowControll()
    {
        
 

        DamageObject();

        if (MC != null && ConstructorID>-1 && ConstructorID < constr.OBOnBoard.Count)
        constr.OBOnBoard[ConstructorID].Place = transform.position;
        
        if (Durability <= 0 && DurabilityMax > -1 && CanTurnInEgg)
            {
            constr.AddLogPart("WORKER " + pl.inv.GetItemInDatabase(DatabaseID).itemNames[0] + " DIED! You can pick an egg",
                              "РОБИЧИЙ ПОМЕР! Можете підібрати яйце", "ロボットが死ぬ！卵を拾う", gameObject);



            pl.PlayHandsAudio(WorkerRebirthClip,1);
                HP = 0;
        }
        
        if(SR == null && GetComponent<SpriteRenderer>()!=null) SR = GetComponent<SpriteRenderer>();

        if (GrowingSprites.Length > 0 && CurrentGrowState < GrowingSprites.Length)
            SR.sprite =
                  GrowingSprites[CurrentGrowState];

    }

    void DamageObject()
    {


        if (Stunned)
        {
            MutationUI.transform.position = pl.MainCamera.WorldToScreenPoint(transform.position);

            if (CM != null) CM.enabled = false;
            if (Charac != null) Charac.enabled = false;
            if (PO != null) PO.enabled = false;
            if (MC != null) MC.enabled = false;

            if (StunnedDelay < Time.fixedTime)
            {
                MutationUI.SetActive(false);
                if (CM != null) CM.enabled = true;
                if (Charac != null) Charac.enabled = true;
                if (PO != null) PO.enabled = true;
                if (MC != null) MC.enabled = true;

                Stunned = false;

            }

        }


        if (Stunned && pl.coll_obj.Contains(gameObject) && pl.IM.enter_b)
        {
            pl.MutationTimer = Time.fixedTime + 2.2f;
            pl.MutateIntoThis = gameObject;
            
        }

        if (pl.MutationTimer > Time.fixedTime && Stunned)
        {

            if (pl.coll_obj.Contains(gameObject))
            {

                transform.position = transform.position + new Vector3(1 * pl.side, 0, 0);


            }

            if (pl.MutationTimer - 1.7f < Time.fixedTime && Stunned && pl.MutateIntoThis == gameObject)
            {

             

                Destroy(HPUI);
                Destroy(ChargeUI);
                Destroy(ComfortUI);
                constr.BlowObject(GetComponent<StatsControll>());

            }

            
        }





    }



    void DestroyObjectsToDestroy(GameObject obj)
    {
        if (pl == null)
        {
            return;
        }

        if (pl.menu == null)
        {
            return;
        }
        if (pl.menu.SL == null)
        {
            return;
        }


        if (pl.menu.SL.ObjectsToDestroy == null)
        {

            return;
        }

        if (!pl.menu.SL.ObjectsToDestroy.Contains(name))
        {
            return;
        }

        ObjectsDeath();
                   
                

    }


    public void WallDamage()
    {

        if (Coll != null)
        {
            if (Coll.WallColl != null)
            {

                // print(name + WallTM.WorldToCell(transform.position));

                GetDamage(Coll.WallColl.GetComponent<PubObject>().DamageFromWall);

                GameObject BE = Instantiate<GameObject>(WallBrakeEffect);
                BE.transform.position = transform.position;
                BE.name = "WallBrakeEffect";

                for (int j = 0; j < constr.OBOnBoard.Count; j++)
                {
                    if (constr.OBOnBoard[j].Object == Coll.WallColl)
                    {
                        constr.OBOnBoard.RemoveAt(j);


                    }
                }

                Destroy(Coll.WallColl);


            }
        }

        if (FloorTM != null)
        {
            if (FloorTM.GetTile(FloorTM.WorldToCell(transform.position)) ==
                BrushesPit)
            {
                GetDamage(1);

                GameObject BE = Instantiate<GameObject>(WallBrakeEffect);
                BE.transform.position = transform.position;
                BE.name = "WallBrakeEffect";

                FloorTM.SetTile(FloorTM.WorldToCell(transform.position), null);


            }
        }
    }


    public void PlaySoundsPitched(AudioClip AC, float pitch)
    {
        //print("P");
        constr.GetComponent<AudioSource>().clip = AC;
        constr.GetComponent<AudioSource>().pitch = pitch;
        constr.GetComponent<AudioSource>().Play();
    }

    public void UIControll()
    {
      
        if (HP < MAXHP)
        {
            if (!DrawHP)
            {
                pl.inv.ONOFF(HPUI, true);
                DrawHP = true;
            }
        }
        

        if (!constr.ShowChargeUI) return;

        if (Mathf.Abs(constr.pl._transform.position.x - transform.position.x) < 1.7f && Mathf.Abs(constr.pl._transform.position.y - transform.position.y) < 1.7f)
        {
            if (Alpha < 1)
                Alpha += Time.deltaTime;
        }
        else
        {
            if (Alpha > 0)
                Alpha -= Time.deltaTime;
        }

        Vector2 SliderSize = new Vector2(3.5f / pl.MainCamera.orthographicSize, 3.5f / pl.MainCamera.orthographicSize) / 1.7f;

        if (HPUI != null)
        {
         
            HPUI.transform.position = pl.MainCamera.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f * 1.2f, transform.position.z));
            HPUI.transform.localScale = SliderSize;

            float hpx = (float)HP / (float)MAXHP;
            HPUI.transform.Find("Slider").transform.localScale = new Vector3(hpx, 1, 1);
        }



        if (ChargeUI != null)
        {
            float x = 0;
            if (GrowingSprites.Length > 1) x = (float)CurrentGrowState / (float)(GrowingSprites.Length - 1);
            if (GrowingSprites.Length == 1) x = (float)CurrentGrowState;

            ChargeUI.transform.localScale = SliderSize;

            ChargeUI.transform.position = pl.MainCamera.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z));

            ChargeUI.transform.Find("Slider").transform.localScale = new Vector3(x, 1, 1);
        }

        if (ComfortUI != null)
        {
            ComfortUI.transform.localScale = SliderSize*2;


            if (ChargeUI != null)
                ComfortUI.transform.position = pl.MainCamera.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f * 0.8f, transform.position.z));
            else ComfortUI.transform.position = pl.MainCamera.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z));

            string plus = "";
            if (GetComponent<PubObject>().ComfortPlus > 0) plus = "+";

            ComfortUI.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = " "+ plus + GetComponent<PubObject>().ComfortPlus;
        }
        

    }




    public void ObjectsDeath()
    {

        if (GetComponent<Character>() != null)
            pl.inv.DoneQuest(GetComponent<Character>().QuestIDOnDeath);

        if (MC != null)
        {
            if (MC.Enemy) constr.EnemiesCount--;
            if (MC.Soldier) constr.SoldiersCount--;

            if (MC.MoveToObject != null)
            {
                MC.UnSetMoveToObject();
            }
        }

        if (CM != null)
           CM.RemoveFromAttackList();


        if (AddItemAutomaticly)
        {
            if (!RNDItemDrop)
            {
                Array.ForEach(ItemIDs, item =>
                {
                    if (CurrentGrowState > 0) pl.inv.AddItemNOAUDIO(item, CurrentGrowState, 100, transform.position);
                    else pl.inv.AddItemNOAUDIO(item, ItemCount, 100, transform.position);
                });
                
            }
            else
            {
                pl.inv.AddItemNOAUDIO(ItemIDs[UnityEngine.Random.Range(0, ItemIDs.Length)], pl.LootItem, 100, transform.position);
            }

      
        }


        if (ItemIDs.Length > 0  && !AddItemAutomaticly)
        {
            if (!RNDItemDrop)
            {
                if (CurrentGrowState > 0)
                    pl.inv.DropItemInSameSpot(transform.position, CurrentGrowState, ItemIDs, 100);
                else pl.inv.DropItemInSameSpot(transform.position, ItemCount,  ItemIDs, 100);
            }
            else
            {

                pl.inv.DropItemInSameSpot(transform.position, pl.LootItem, new int[1] { ItemIDs[UnityEngine.Random.Range(0, ItemIDs.Length)] }, 100);
            }
        }


        CurrentGrowState = 0;
        GrowTimer = Time.fixedTime + GrowDelay;

        if (!Destructible)
        {
          
            pl.inv.ONOFF(HPUI, false);
            DrawHP = false;
            

            HP = MAXHP;
            return;
        }


        
        for (int i = 0; i < constr.OBOnBoard.Count; i++)
        {
            if (constr.OBOnBoard[i].Object == gameObject)
                constr.OBOnBoard.RemoveAt(i);
        }

        for (int i = 0; i < constr.Enemies.Count; i++)
        {
            if (constr.Enemies[i].Object == gameObject)
                constr.Enemies.RemoveAt(i);
        }

        if (_PubObject != null)
        {
            if (constr.AllTrash>= _PubObject.Trash)
            constr.AllTrash -= _PubObject.Trash;

            if (constr.AllPoop >= _PubObject.Poop)
                constr.AllPoop -= _PubObject.Poop;
        }


        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<StatsControll>() != null)
            {
                if (transform.GetChild(i).GetComponent<StatsControll>().HPUI != null)
                {

                    Destroy(transform.GetChild(i).GetComponent<StatsControll>().HPUI);
                    Destroy(transform.GetChild(i).GetComponent<StatsControll>().ChargeUI);
                    Destroy(transform.GetChild(i).GetComponent<StatsControll>().ComfortUI);
                }
            }
        }



        Destroy(HPUI);
        Destroy(ChargeUI);
        Destroy(ComfortUI);
        constr.BlowObject(GetComponent<StatsControll>());

        


        
        




    }


}
