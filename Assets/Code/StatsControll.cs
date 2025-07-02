using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using TMPro;
using Pathfinding;



public class StatsControll : MonoBehaviour
{
    public int HP = 3;
    public int Speed = 3;
    public int Damage = 0;
   
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



    private List<AudioClip> DamageClips = new List<AudioClip>();
    public List<AudioClip> CollisionClips = new List<AudioClip>();
    public bool AudioPlayed;

    private Constructor Const;
    private GameObject WallBrakeEffect;

    public bool GettingDamageFromWalls;
    private TileBase BrushesPit;
  
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




   
    public int DurabilityMax = -1;

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

    private CharacterPath CM;
    private Character Charac;
    private PubObject PO;

    [HideInInspector]
    public DrawIfActive DIA;

    public bool IgnoreDestroyList;
    private PubObject _PubObject;

    [HideInInspector]
    public bool BuildedStructure = false;


    [HideInInspector]
    public string SpawnPointName;
    private Transform _transform;



    public ColorAndMaterial ColorMaterial;

    private void Awake()
    {

        _PubObject = GetComponent<PubObject>();
        AU = GetComponent<AudioSource>();
        CM = GetComponent<CharacterPath>();
        Charac = GetComponent<Character>();
        PO = GetComponent<PubObject>();
        DIA = GetComponent<DrawIfActive>();
        MC = GetComponent<MovementControll>();
        Coll = GetComponent<CollList>();
        _transform = transform;

        pl = InitializeObjects.PL;
        Const = InitializeObjects.Constr;
        gameObject.AddComponent<ColorAndMaterial>();
        ColorMaterial = GetComponent<ColorAndMaterial>();

    }




    void Start()
    {

        DrawVision = true;

        ChangeTheName();

      
        bool InList = false;

 
        
        for (int i = 0; i < Const.OBOnBoard.Count; i++)
        {
            if (Const.OBOnBoard[i].Object == gameObject) InList = true;

        }

        DatabaseOriginalID = pl.inv.GetItemInDatabase(DatabaseID).OriginalitemID;

        if (transform.parent != null)
        {
            if (transform.parent.GetComponent<Blueprint>() != null)
            {
                InList = true;
            }
        }


        if (DatabaseID > -1 && transform.parent != Const.transform && !InList)
        {

            ObjectOnBoard OOB = new ObjectOnBoard(DatabaseID, transform.position, name, gameObject, this, GetComponent<PubObject>());

            Const.OBOnBoard.Add(OOB);
            Const.OBOnBoard[Const.OBOnBoard.Count - 1].Place = transform.position;


            ConstructorID = Const.OBOnBoard.Count - 1;
            if (GrowingSprites.Length > 0)
                CurrentGrowState = UnityEngine.Random.Range(1, GrowingSprites.Length);

            if (StartGrowWithZero) CurrentGrowState = 0;

        }


        if (DatabaseID > -1)
        {
            DurabilityMax = pl.inv.GetItemInDatabase(DatabaseID).Durability;
            Durability = DurabilityMax;
        }



    

     
        if (CM != null)
            CM.SpeedMultiplier = Speed;


 
        FloorTM = InitializeObjects.FloorTilemap;
       


        SatietyMax = 20;
        Satiety = SatietyMax /3;

        if (!CanBeHungry) Satiety = 999;

        Draw = true;
  
        if(MC!=null)
        MC.MinDamage = Damage;



        PaymentTimer = Time.fixedTime + 700;
        Payed = true;
     

        SR = GetComponent<SpriteRenderer>();


      





        gun = pl.GetComponent<Gun>();




        if (CanBeStunned)
        {
            MutationUI = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/MutationUI"), GameObject.Find("Canvas").transform);
            MutationUI.SetActive(false);
        }



        if (Const.ShowChargeUI)
        { 
            if (Const.ShowChargeUI && GrowingSprites.Length > 0)
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



    
        if (GettingDamageFromWalls)
        {
            WallBrakeEffect = Resources.Load<GameObject>("Prefabs/Effects/WallBrakeEffect");
            BrushesPit = Resources.Load<TileBase>("Brushes/Pit");
        }

        MAXHP = HP;

        
        GrowTimer = Time.fixedTime + GrowDelay + UnityEngine.Random.Range(0, 6);




        if (SR != null && GrowingSprites.Length > 0)
        {
            if (CurrentGrowState >= GrowingSprites.Length)
             CurrentGrowState = GrowingSprites.Length - 1;
            
            SR.sprite = GrowingSprites[CurrentGrowState];
        }

        if(DeathClip==null)
        SetDeathClip();

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
        pl.inv.ADDPickedName(damage.ToString(),  1, transform.position);



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
                Const.AddLogPart("You payed sallery (-1 Gold)!", "Ви заплатили зарплату (-1 Золото)", "あなたはギャラリー（-1金）を支払った！", gameObject);

                pl.inv.ReduceItemCount(9, PaymentAmount);
                Payed = true;

            }
        }


        if (PaymentTimer < Time.fixedTime)
        {
            if (pl.inv.GetItem(9) != null)
            {
                Const.AddLogPart("You payed sallery (" + PaymentAmount + " Gold)!", "Ви заплатили зарплату (" + PaymentAmount + " Золото)", "あなたはギャラリー（" + PaymentAmount + "金）を支払った！", gameObject);

                pl.inv.ReduceItemCount(9, PaymentAmount);
                Payed = true;

            }
            else
            {
                Const.AddLogPart("A worker needs payment (" + PaymentAmount + " Gold) and you dont have it!", "У вас недостатньо золота для зарплати. Треба " + PaymentAmount + " Золота", "労働者が支払い（" + PaymentAmount + " 金）を必要としているのですが、あなたはそれを持っていません！", gameObject);

                Payed = false;
            }

            PaymentTimer = Time.fixedTime + 700;
        }
        

    }


    public void GrowControll()
    {

        ColorMaterial.ObjectColorAlpha();

            DamageObject();

        if (MC != null && ConstructorID>-1 && ConstructorID < Const.OBOnBoard.Count)
            Const.OBOnBoard[ConstructorID].Place = transform.position;
        
        if (Durability <= 0 && DurabilityMax > -1 && CanTurnInEgg)
            {
            Const.AddLogPart("WORKER " + pl.inv.GetItemInDatabase(DatabaseID).itemNames[0] + " DIED! You can pick an egg",
                              "РОБИЧИЙ ПОМЕР! Можете підібрати яйце", "ロボットが死ぬ！卵を拾う", gameObject);



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
                Const.BlowObject(GetComponent<StatsControll>());

            }

            
        }





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

                for (int j = 0; j < Const.OBOnBoard.Count; j++)
                {
                    if (Const.OBOnBoard[j].Object == Coll.WallColl)
                    {
                        Const.OBOnBoard.RemoveAt(j);


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
        Const.GetComponent<AudioSource>().clip = AC;
        Const.GetComponent<AudioSource>().pitch = pitch;
        Const.GetComponent<AudioSource>().Play();
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
        

        if (!Const.ShowChargeUI) return;

        if (Mathf.Abs(pl._transform.position.x - transform.position.x) < 1.7f && Mathf.Abs(pl._transform.position.y - transform.position.y) < 1.7f)
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

        if (Charac != null)
            pl.journal.DoneQuest(Charac.QuestIDOnDeath);

        if (MC != null)
        {
        
     

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


        
        for (int i = 0; i < Const.OBOnBoard.Count; i++)
        {
            if (Const.OBOnBoard[i].Object == gameObject)
                Const.OBOnBoard.RemoveAt(i);
        }

        for (int i = 0; i < Const.Enemies.Count; i++)
        {
            if (Const.Enemies[i].Object == gameObject)
                Const.Enemies.RemoveAt(i);
        }

        if (_PubObject != null)
        {
            if (Const.AllTrash>= _PubObject.Trash)
            Const.AllTrash -= _PubObject.Trash;

            if (Const.AllPoop >= _PubObject.Poop)
                Const.AllPoop -= _PubObject.Poop;
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
        Const.BlowObject(GetComponent<StatsControll>());

        


        
        




    }


}
