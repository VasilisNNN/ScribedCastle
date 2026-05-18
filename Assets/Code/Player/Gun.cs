using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject GunOB { get; set; }
    private GameObject SwingEffect;
    public GameObject Hand { get; set; }
    private GameObject GunTip { get; set; }


    private Player pl;
    private Inventory inv;
    private Constructor constr;

    private float HitTimer, HitEffectTimer;
    public float HitDuration { get; set; }
    public bool RotateGun;
    private float HitDurationMax = 0.4f;
    public int CurrentGunID { get; set; }
    public int EXItemID { get; set; }

    public AudioClip LightSwing, ReguralSwing, ClubSwing, MagicSwing, FunnySwing, SwordSwing, AxeSwing, PistolSwing, RifleSwing, FakeSwing;


    private List<GameObject> CastedObjects = new List<GameObject>();

    private List<float> CastedObjectsTimers = new List<float>();

    public GameObject[] RND_Bullets;
    public Vector2 LastDirection { get; private set; }
    private Vector2 DigDirection;
    private GameObject AttackEffect;
    private int buffi = -1;

 

    public int Durability;

    private Animator PlayerAnim;

    private GameObject MouseOB;
    public GameObject DigObject { get; set; }

    public Vector2 GunHitPoint = new Vector2(0.6f, -0.0324f);
    public int AddOnClientDeath = -1;


    public List<GameObject> BulletList = new List<GameObject>();


    private List<GameObject> BulletsForThisGun = new List<GameObject>();
    public GameObject RightHand;
    private ItemDatabase itemDatabase;
    void Start()
    {
        LastDirection = new Vector2(1, 0);
        SwingEffect = transform.Find("SwingEffect").gameObject;
        MouseOB = GameObject.Find("MouseOB");
        itemDatabase = InitializeObjects.Itemdatabase;


        //GunOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Gun"),transform);
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();
        Hand = GameObject.Find("Player").transform.Find("Hand").gameObject;
   
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        //CurrentGunID = -1;
        buffi = -1;

        PlayerAnim = GetComponent<Animator>();

    }


    void Update()
    {
        if (RotateGun) GunRotation();
        if (HitDuration > 0) HitDuration -= Time.deltaTime;

        if (HitTimer > Time.fixedTime)
        {
            pl.Attacking = true;
        }
        else pl.Attacking = false;

        GunHitsTheObject();


        GunObjectCreateDestroy();

        RemoveCastObject();
        
        GunAttack();
        
        DamageFromBullets();

    }

    void GunAttack()
    {

        if (!pl.IM.enter_b_hold && !pl.IM.LeftMouseButton && !pl.IM.space_b) pl.inv.ShootPause = false;

        if (GunOB == null || pl._gameover || CurrentGunID <= -1 || pl.StartLoading || constr.Building || pl.IM.ActionDelay>Time.fixedTime || pl.inv.ShootPause)
        {
            PlayerAnim.SetBool("Attack", false);
            PlayerAnim.SetBool("AttackDown", false);
            PlayerAnim.SetBool("AttackUp", false);
            PlayerAnim.SetBool("Gun", false);
            return;
        }

        //  MouseHit();




        if (GunOB.GetComponent<SpriteRenderer>().sprite == null)
        {
            GunOB.GetComponent<PolygonCollider2D>().enabled = false;
            return;
        }


        MoveDigItemObject();



        if (HitTimer < Time.fixedTime)
        {
            GunOB.GetComponent<PolygonCollider2D>().enabled = false;

            if (!RotateGun)
                GunOB.transform.position = Hand.transform.position;
            else
            {
                if (itemDatabase.FindItem(CurrentGunID).Gun)
                    GunOB.transform.position = pl._transform.position;
                else GunOB.transform.position = Hand.transform.position;

            }
            // GunOB.transform.localScale = pl._transform.localScale;

            // GunOB.transform.position = new Vector3(transform.position.x + 0.4f * pl.side, Hand.transform.position.y, Hand.transform.position.z);
            //  GunOB.transform.rotation = Hand.transform.rotation;

        }





        if (!pl.IM.LeftMouseButton && !RotateGun)
        {
            if (Mathf.Abs(pl._normalHSpeed) > Mathf.Abs(pl._normalVSpeed))
                LastDirection = new Vector2(1 * pl.transform.localScale.x, 0);

            if (Mathf.Abs(pl._normalVSpeed) > Mathf.Abs(pl._normalHSpeed) && pl._normalVSpeed < 0)
                LastDirection = new Vector2(0, -1);

            if (Mathf.Abs(pl._normalVSpeed) > Mathf.Abs(pl._normalHSpeed) && pl._normalVSpeed > 0)
                LastDirection = new Vector2(0, 1);
        }

 
        if (!constr.Building && !pl.menu.MenuONOFF && !pl.inv.showinvent && !pl.inv.crafting && (pl.IM.space_b || (pl.IM.LeftMouseButton && pl.CollidingItems.Count <= 0)) && pl.Stamina >= itemDatabase.FindItem(CurrentGunID).StaminaUse && HitDuration <= 0 && pl.IM.ActionDelay < Time.fixedTime)
        {
            if (!itemDatabase.FindItem(CurrentGunID).Gun)
            GunOB.GetComponent<PolygonCollider2D>().enabled = true;



            if (pl.IM.MouseMode)
            {
                if (Mathf.Abs(pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).x - transform.position.x) > Mathf.Abs(pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).y - transform.position.y))
                {
                    if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).x > transform.position.x)
                        pl.side = 1;

                    if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).x < transform.position.x)
                        pl.side = -1;



                    LastDirection = new Vector2(1 * pl.transform.localScale.x, 0);

                }
                else
                {
                    if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).y > transform.position.y)
                        LastDirection = new Vector2(0, 1);

                    if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).y < transform.position.y)
                        LastDirection = new Vector2(0, -1);

                }
            }




            ActualHit();


            if(!itemDatabase.FindItem(CurrentGunID).Gun)
            GunOB.transform.position = Hand.transform.position;

            // GunOB.transform.localScale = pl._transform.localScale;

            // GunOB.transform.position = transform.position + new Vector3(GunHitPoint.x * LastDirection.x, GunHitPoint.y, Hand.transform.position.z);
            // GunOB.transform.rotation = Quaternion.Euler(0, 0, ((1 - 1 * LastDirection.x) / 2) * 360);
            

        }





        if (HitEffectTimer < Time.fixedTime)
        {

            if (HitEffectTimer + 0.1f > Time.fixedTime)
            {
                if (itemDatabase.FindItem(CurrentGunID).MagicEffectToCast != null)
                {
                    if (itemDatabase.FindItem(CurrentGunID).MagicEffectToCast.Length > 0 && AttackEffect == null)
                    {
                        AttackEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/" + itemDatabase.FindItem(CurrentGunID).MagicEffectToCast));

                        AttackEffect.transform.position = GunTip.transform.position;
                        AttackEffect.transform.rotation = GunOB.transform.rotation;

                    }
                }
            }
        }

        if (HitTimer < Time.fixedTime)
        {


            PlayerAnim.SetBool("Attack", false);
            PlayerAnim.SetBool("AttackDown", false);
            PlayerAnim.SetBool("AttackUp", false);

        }



        if (SwingEffect != null&& LastDirection.y==0 && !itemDatabase.FindItem(CurrentGunID).Gun)
        {
            if (HitTimer > Time.fixedTime && HitTimer - 0.19 < Time.fixedTime)
            {
                SwingEffect.GetComponent<SpriteRenderer>().enabled = true;
                SwingEffect.transform.position = GunTip.transform.position;
            }
            else SwingEffect.GetComponent<SpriteRenderer>().enabled = false;
        }

        // SwingEffect.transform.rotation = Quaternion.Euler(Hand.transform.rotation.x, Hand.transform.rotation.y, Hand.transform.rotation.z - 90f);





    }

    void GunObjectCreateDestroy()
    {
        if (CurrentGunID == -1 || CurrentGunID != EXItemID)
        {
         
            Destroy(GunOB);
        }

        if (GunOB == null && CurrentGunID > -1)
        {

            if (!RotateGun)
                GunOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Gun"), Hand.transform);
            else
            {
                if (itemDatabase.FindItem(CurrentGunID).Gun)
                {
                    GunOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Gun"));
                    pl.inv.ONOFF(Hand, false);
                    if(RightHand!=null) pl.inv.ONOFF(RightHand, false);
                }
                else
                {
                    GunOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/Gun"), Hand.transform);
                    pl.inv.ONOFF(Hand, true);
                    if (RightHand != null) pl.inv.ONOFF(RightHand, true);

                }
            }
  
            GunTip = GunOB.transform.Find("GunTip").gameObject;

            if (GunTip == null || GunOB == null || itemDatabase.FindItem(CurrentGunID) == null) return;

            GunTip.transform.position = GunOB.transform.position + new Vector3(0, itemDatabase.FindItem(CurrentGunID).GunLength, 0);



            GunOB.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Items/" + itemDatabase.FindItem(CurrentGunID).itemNames[0]);
            GunOB.AddComponent<PolygonCollider2D>();
            GunOB.GetComponent<PolygonCollider2D>().isTrigger = true;
            GunOB.GetComponent<PolygonCollider2D>().enabled = false;

            EXItemID = CurrentGunID;
        }

    }

    void RemoveCastObject()
    {
        if (CastedObjects.Count <= 0) return;
        
        for (int i = 0; i < CastedObjects.Count; i++)
        {
            if (CastedObjectsTimers[i] < Time.fixedTime && CastedObjects[i]!=null)
            {
                DeactiveteBullet(CastedObjects[i]);
                CastedObjects.RemoveAt(i);
                CastedObjectsTimers.RemoveAt(i);

            
                /*
                CastedObjects[i].transform.position = new Vector3(9999, 9999, 9999);
                CastedObjects.RemoveAt(i);
                CastedObjectsTimers.RemoveAt(i);*/

            }
        }
        
    }


    void ActualHit()
    {
        DigItem(constr.Tile.WorldToCell(DigObject.transform.position));

        if(SwingEffect!=null && SwingEffect.GetComponent<Animator>()!=null &&  !itemDatabase.FindItem(CurrentGunID).Gun)
        SwingEffect.GetComponent<Animator>().Play(SwingEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);

        Vector3 Bulletpos = Vector3.zero;

        float gunlength = 0.4f;

        if (LastDirection.x != 0)
        {
            Bulletpos = new Vector3(gunlength * pl.transform.localScale.x, 0f, 0f);

            PlayerAnim.SetBool("Attack", true);

            if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast != null)
            {
                if (itemDatabase.FindItem(CurrentGunID).Gun)
                    PlayerAnim.SetBool("Gun", true);
            }
            else PlayerAnim.SetBool("Gun", false);


            PlayerAnim.SetBool("AttackDown", false);
            PlayerAnim.SetBool("AttackUp", false);

        }


        if (LastDirection.y < 0)
        {
            Bulletpos = new Vector3(0, -gunlength, 0f);

            PlayerAnim.SetBool("AttackDown", true);
            PlayerAnim.SetBool("Attack", false);
            PlayerAnim.SetBool("AttackUp", false);

        }

        if (LastDirection.y > 0)
        {
            Bulletpos = new Vector3(0, gunlength, 0f);

            PlayerAnim.SetBool("AttackUp", true);
            PlayerAnim.SetBool("Attack", false);
            PlayerAnim.SetBool("AttackDown", false);

        }


        CastMagicObject(Bulletpos);

       

        if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.regular)
        {
            Hand.GetComponent<AudioSource>().clip = ReguralSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.club)
        {
            Hand.GetComponent<AudioSource>().clip = ClubSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.sword)
        {
            Hand.GetComponent<AudioSource>().clip = SwordSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.axe)
        {
            Hand.GetComponent<AudioSource>().clip = AxeSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.fakegun)
        {
            Hand.GetComponent<AudioSource>().clip = AxeSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.shotgun)
        {
            Hand.GetComponent<AudioSource>().clip = AxeSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.rifle)
        {
            Hand.GetComponent<AudioSource>().clip = AxeSwing;
        }
        else if (itemDatabase.FindItem(CurrentGunID)._Soundtype == Item.Soundtype.pistol)
        {
            Hand.GetComponent<AudioSource>().clip = AxeSwing;
        }
        if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast != null)
        {
            if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast.Length > 0)
                Hand.GetComponent<AudioSource>().clip = MagicSwing;
        }

        if (AttackEffect != null) Destroy(AttackEffect);

        Hand.GetComponent<AudioSource>().Play();

        HitDuration = HitDurationMax;
        HitTimer = Time.fixedTime + 0.2f;
        HitEffectTimer = Time.fixedTime + 0.1f;
        pl.ReduceStamina(-itemDatabase.FindItem(CurrentGunID).StaminaUse);

    }




    void CastMagicObject( Vector3 Bulletpos)
    {
        if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast == null) return;
        
        if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast.Length <= 0) return;
        
        if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast != "Random")
        {

        

            Durability--;



            for (int i = 0; i < BulletsForThisGun.Count; i++)
            if (CastThisObject(BulletsForThisGun[i])) break;


        }
        else
        {
            CastRandomObject(Bulletpos);
        }


            
        
    }

    void CastRandomObject(Vector3 Bulletpos)
    {
        GameObject c = Instantiate<GameObject>(RND_Bullets[Random.Range(0, RND_Bullets.Length)]);
        c.transform.position = GunTip.transform.position + Bulletpos;
        CastedObjects.Add(c);

        Durability--;

     

        if (c.GetComponent<Bullet>() != null)
        {
            if (c.GetComponent<Bullet>().Fly)
                c.GetComponent<Rigidbody2D>().velocity = new Vector2(Bulletpos.x, Bulletpos.y) * 15;

            CastedObjectsTimers.Add(Time.fixedTime + c.GetComponent<Bullet>().Timer);
        }
        else CastedObjectsTimers.Add(Time.fixedTime + 2);
    }




    bool CastThisObject(GameObject bullet )
    {

        if (bullet == null) return false;
        
        if (CastedObjects.Contains(bullet)) return false;
           
        

        BulletList.Add(bullet);

        CastedObjects.Add(bullet);


        if (RotateGun)
        {
          
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(GunOB.transform.right.x, GunOB.transform.right.y) * bullet.GetComponent<Bullet>().bulletForce;
            bullet.transform.position = GunTip.transform.position;

            if (bullet.GetComponent<Bullet>().RotateWithTheGun)
                bullet.transform.rotation = GunTip.transform.rotation;

        }
        else
        {
            if (bullet.GetComponent<Bullet>().Fly)
                bullet.GetComponent<Rigidbody2D>().velocity = LastDirection * bullet.GetComponent<Bullet>().bulletForce;



            bullet.transform.position = Hand.transform.position;

        }


        if (bullet.GetComponent<Trail>() != null)
            bullet.GetComponent<Trail>().enabled = true;

        bullet.GetComponent<Bullet>().Damage = pl.DamageAll;


        CastedObjectsTimers.Add(Time.fixedTime + bullet.GetComponent<Bullet>().Timer);
        return true;
            
        
        
    }

    void HitObject(GameObject DestructibleOBs, StatsControll FO)
    {
        if (DestructibleOBs == null)
        {
            return;
        }

        if (DestructibleOBs.GetComponent<StatsControll>() == null)
        {
            return;
        }

        if (DestructibleOBs.GetComponent<MovementControll>() != null)
        {
            if (DestructibleOBs.GetComponent<CharacterPath>() != null)
            {
                if (!DestructibleOBs.GetComponent<CharacterPath>().PathComlitionCheck() && Mathf.Abs(DestructibleOBs.GetComponent<CharacterPath>().SpeedForce.x)>0.2f && Mathf.Abs(DestructibleOBs.GetComponent<CharacterPath>().SpeedForce.y) > 0.2f)
                    return;
            }
        }


        if (FO.InvisTimer - 0.1f > Time.fixedTime) return;

            if ((Mathf.Abs(DestructibleOBs.transform.position.x - transform.position.x) >= 3 ||
                Mathf.Abs(DestructibleOBs.transform.position.y - transform.position.y) >= 3))
            {
                return;
            }
          
         

            if ( FO.CurrentGrowState <= 0 && FO.GrowingSprites.Length != 0)
            {
                return;
            }
           
            if (FO.Gun_ToKill_ID > -1 && FO.Gun_ToKill_ID != CurrentGunID)
            {
                return;
            }

            FO.GetDamage(pl.DamageAll);

            Durability--;

      

    }




    void GunHitsTheObject()
    {
        if (pl.StartLoading) return;

        if (GunOB == null)
            return;

        CollList collList = GunOB.GetComponent<CollList>();
        if (collList == null)
            return;

        List<GameObject> collObjects = collList.GetCollList();
        if (collObjects == null)
            return;

        
        // Make a copy of the list to iterate over
        List<GameObject> collObjectsCopy = new List<GameObject>(collObjects);

        foreach (GameObject objtocoll in collObjectsCopy)
        {
            if (objtocoll != null && objtocoll != gameObject)
            {
                StatsControll statsControll = objtocoll.GetComponent<StatsControll>();
                TailObject tailObject = objtocoll.GetComponent<TailObject>();

                if (statsControll != null)
                {
                    if(statsControll.enabled)
                    HitObject(objtocoll, statsControll);
                }
                else if (tailObject != null)
                {
                    HitObject(tailObject.ParentObject, tailObject.ParentObject.GetComponent<StatsControll>());
                }
            }
        }
    }





    void MoveDigItemObject()
    {
        Vector3 pos = new Vector3(DigDirection.x * 0.5f, DigDirection.y * 0.25f, 0);



        Vector3Int TilePos = constr.Tile.WorldToCell(pl.transform.position + pos);

        if (pl.IM.MouseMode)
        {
            TilePos = constr.Tile.WorldToCell(pl.MainCamera.ScreenToWorldPoint(pl.MouseUI.transform.position));
        }

        if (DigObject == null) return;

        DigObject.transform.position = constr.Tile.CellToWorld(TilePos);


        /*
         Vector2 MousePos = pl.MainCamera.ScreenToWorldPoint(MouseOB.transform.position);
         Vector3Int MousePosCell = constr.Tile.WorldToCell(new Vector3(MousePos.x, MousePos.y,0));
         Vector3Int PlayerPosCell = constr.Tile.WorldToCell(pl.transform.position);



         if (pl.IM._horizontal_R == 0 && pl.IM._vertical_R == 0)
         {
             if(Mathf.Abs(MousePosCell.x- PlayerPosCell.x) <3 && Mathf.Abs(MousePosCell.y - PlayerPosCell.y) < 3)
                 DigDirection =new Vector2 (MousePosCell.x - PlayerPosCell.x, MousePosCell.y - PlayerPosCell.y-0.5f); 
         }

         if (pl.IM._horizontal_R > 0)
         {
             if (DigDirection.x < 2) DigDirection.x++;
         }

         if (pl.IM._horizontal_R < 0)
         {
             if (DigDirection.x >-2) DigDirection.x--;
         }
         if (pl.IM._vertical_R > 0)
         {
             if (DigDirection.y < 3) DigDirection.y++;
         }

         if (pl.IM._vertical_R < 0)
         {
             if (DigDirection.y > -3) DigDirection.y--;
         }*/

    }


    void DigItem(Vector3Int TilePos)
    {
        if (!itemDatabase.FindItem(CurrentGunID).CanDig) return;
        

        if (constr.Tile.GetTile(TilePos) == null) return;
        
        if (constr.Tile.WorldToCell(GunTip.transform.position) == new Vector3Int(0, 0, 0)) return;
        



        Vector3 pos = new Vector3(constr.Tile.CellToWorld(TilePos).x, constr.Tile.CellToWorld(TilePos).y + 0.75f, 0);
        
        if (constr.Tile.GetTile(TilePos).name == "Floor")
        {
            pl.inv.DropItemInSameSpot(pos, 1, new int[1] { 301 },-1);
            constr.Tile.SetTile(TilePos, null);
        }
        else if (constr.Tile.GetTile(TilePos).name == "Ground")
        {
            pl.inv.DropItemInSameSpot(pos, 1, new int[1] { 1 },-1);
            constr.Tile.SetTile(TilePos, null);

        }
        

        
        
    }

    void MouseHit()
    {
        if (!pl.IM.LeftMouseButton) return;
        
        Vector2 Move = pl.MainCamera.ScreenToWorldPoint(MouseOB.transform.position) - transform.position;

        if (Mathf.Abs(pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).x - transform.position.x) > Mathf.Abs(pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).y - transform.position.y))
        {
            if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).x > transform.position.x)
                LastDirection = new Vector2(1, 0);

            if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).x < transform.position.x)
                LastDirection = new Vector2(-1, 0);
        }
        else
        {
            if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).y > transform.position.y)
                LastDirection = new Vector2(0, 1);

            if (pl.MainCamera.ScreenToWorldPoint(MouseOB.GetComponent<RectTransform>().position).y < transform.position.y)
                LastDirection = new Vector2(0, -1);

        }


        if (Mathf.Abs(Move.x) > Mathf.Abs(Move.y))
        {
            if (Move.x > 0)
                pl._transform.localScale = new Vector3(1, 1, 1);
            else
                pl._transform.localScale = new Vector3(-1, 1, 1);


            LastDirection = new Vector2(Move.normalized.x, 0);
        }
        else if (Mathf.Abs(Move.x) < Mathf.Abs(Move.y))
            LastDirection = new Vector2(0, Move.normalized.y);

        
    }



    bool CollidingWithGun(GameObject Obj)
    {
        bool result = false;

        if (Obj == null)
            return false;

        if (Obj.GetComponent<BoxCollider2D>() == null)
            return false;


        BoxCollider2D boxCollider = Obj.GetComponent<BoxCollider2D>();

     
        if (GunOB.GetComponent<CollList>().GetCollList().Contains(Obj))
        {
            result = true;
        }


        if (boxCollider.bounds.Intersects(GunOB.GetComponent<Collider2D>().bounds))
            {
                result = true;
            }


        if (boxCollider.bounds.Contains(GunOB.transform.position))
        {
            result = true;
        }


        if (GunTip == null)
            return result;
        

            if (boxCollider.bounds.Contains(GunTip.transform.position ))
            {
                result = true;
            }


        
         Vector2 XTip = new Vector2(GunTip.transform.position.x - LastDirection.x, GunTip.transform.position.x);

        if (pl.side < 0) XTip = new Vector2(GunTip.transform.position.x, GunTip.transform.position.x - pl.side);

        float YTip = GunTip.transform.position.y;



        if (XTip.y > boxCollider.bounds.min.x &&
            XTip.x < boxCollider.bounds.max.x &&
            YTip < boxCollider.bounds.max.y &&
            YTip > boxCollider.bounds.min.y) result = true;

                


        return result;
    }

    void DamageFromBullets()
    {

        for (int b = 0; b < BulletList.Count; b++)
        {
            if (BulletList[b] != null && BulletList[b].transform.position.x != 9999 && BulletList[b].transform.position.y != 9999)
            {



                Vector2 PositionDifference = new Vector2(Mathf.Abs(BulletList[b].transform.position.x - pl._transform.position.x), Mathf.Abs(BulletList[b].transform.position.y - pl._transform.position.y));

                if (PositionDifference.x > 15 || PositionDifference.y > 15)
                {
                    DeactiveteBullet(BulletList[b]);

                    break;
                }

                else if (b <= BulletList.Count - 1)
                {

                    bool c = false;
               
                    for (int i = 0; i < constr.OBOnBoard.Count; i++)
                    {
                        if (BulletList[b].GetComponent<CollList>().coll_obj.Contains(constr.OBOnBoard[i].Object))
                        {
                            BulletColl(i, BulletList[b]);
                            c = true;
                            break;
                        }


                    }

                    if (c) break;

                }
            }
        }


    }

    void BulletColl(int i, GameObject bullet)
    {
        print(constr.OBOnBoard[i].Name);

        StatsControll FO = constr.OBOnBoard[i].Stats;

        if (!bullet.GetComponent<Bullet>().DamageEnemy || FO.Friend) return;

        if (bullet == null) return;


        if ((FO.Gun_ToKill_ID > -1 && FO.Gun_ToKill_ID == CurrentGunID) || FO.Gun_ToKill_ID == -1)
        {

            FO.GetDamage(bullet.GetComponent<Bullet>().Damage);

            if (FO.GetComponent<CharacterPath>() != null)
            {
                FO.GetComponent<CharacterPath>().StartAttack();
            }


            if (bullet.GetComponent<Bullet>().DestroyOnColl)
                DeactiveteBullet(bullet);


        }
    }


    void DeactiveteBullet(GameObject Bullet)
    {
        if (!BulletList.Contains(Bullet)) return;

        print("DeactiveteBullet name: " + Bullet.name);

        Bullet.transform.position = new Vector3(9999, 9999);
        Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Bullet.GetComponent<CollList>().coll_obj = new List<GameObject>();


        /* GameObject blow;
         blow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Small"));
         blow.transform.position = Bullet.transform.position;
         */

        if (Bullet.GetComponent<Trail>() != null)
        {

            for (int j = 0; j < Bullet.GetComponent<Trail>().ObjList.Count; j++)
            {
                Bullet.GetComponent<Trail>().ObjList[j].transform.position = new Vector3(9999, 9999, 0);

                //  Destroy(Bullet.GetComponent<Trail>().ObjList[j]);
            }

            Bullet.GetComponent<Trail>().enabled = false;
        }

        BulletList.Remove(Bullet);

        //  Destroy(Bullet);
    }


    public void SetGunID(int ID, int durability)
    {
        if (ID > -1)
        {
            if(inv ==null) inv = GameObject.Find("Player").GetComponent<Inventory>();

            print("ID " + ID);

            CurrentGunID = itemDatabase.FindItem(ID).itemID;
            Durability = durability;
            EXItemID = itemDatabase.FindItem(ID).itemID;

            for (int i = 0; i < BulletsForThisGun.Count; i++) Destroy(BulletsForThisGun[i]);
            BulletsForThisGun = new List<GameObject>();

            if (itemDatabase.FindItem(CurrentGunID).MagicObjectToCast!=null && itemDatabase.FindItem(CurrentGunID).MagicObjectToCast.Length > 1)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (BulletsForThisGun.Count < 20)
                    {
                        GameObject b = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Objects/" + itemDatabase.FindItem(CurrentGunID).MagicObjectToCast));
                        b.transform.position = new Vector3(9999, 9999);
                        BulletsForThisGun.Add(b);
                    }
                }
            }


           

        }
        else
        {
            SwingEffect.GetComponent<SpriteRenderer>().enabled = false;

        

            CurrentGunID = -1;
            inv.ONOFF(DigObject, false);
            Durability = 0;
        }

        if (ID > -1)
        {
            if (itemDatabase.FindItem(ID).CanDig)
                inv.ONOFF(DigObject, true);
            else inv.ONOFF(DigObject, false);
        }


        if (inv.GetComponent<Gun>().GunOB != null) Destroy(inv.GetComponent<Gun>().GunOB);

    }



    private void GunRotation()
    {
        if (GunOB == null) return;
        if (CurrentGunID<=-1) return;
        if (!itemDatabase.FindItem(CurrentGunID).Gun) return;

        Vector3 targetDirection;

        if (pl.IM.joystick)
        {
            // Get the direction from the gamepad right stick
            float gamepadRightStickX = pl.IM._horizontal_R;
            float gamepadRightStickY = pl.IM._vertical_R;
            targetDirection = new Vector3(gamepadRightStickX, gamepadRightStickY, 0f);
        }
        else
        {
            // Get the position of the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            // Get the direction from the gun to the mouse position
            targetDirection = mousePos - GunOB.transform.position;
        }

        targetDirection.Normalize();

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // Rotate the gun towards the target direction
        GunOB.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

