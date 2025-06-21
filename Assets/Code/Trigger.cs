using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Pathfinding;
using TMPro;


public class Trigger : MonoBehaviour
{
    private Player pl;
    private Inventory inv;

    public bool OnEnter;
    public float PushDelay = 0.1f;
   
    public int NeedItem = -1;
    public float NeedItemDrawRange = 1.5f;
    public int[] NeedBuildingsToConstruct;
    private List<int> BuildingsMatch = new List<int>();
    public bool PlayTargetSound = false;

    public GameObject[] ONObjects;
    public bool DoOnes;
    public bool SwitchState = true;
    public bool UndoActionIfNoColl;
    public bool[] TF;
    public bool StartOFF = true;

    public bool LockCamera;
    public GameObject LockOnObject;
    public bool ZoomCamera;
    public float ShakeCamera = 0;
    public float LockCameraTimer = 0;

    public bool Finish { get; set; }

    public bool RestartFinish;

    public string AchivementName = "";

    public bool OnlyAnim;

    private float DestroyTimer;
    public float DestroyTimerDelay = 0;

    private GameObject ViewTrigger;
    public bool OnlyOnBody;

    public bool SelfDestroy;
    public bool BloodEffectOnSelfDestroy;

    private List<GameObject> NeedItemGameobject = new List<GameObject>();
    private Constructor constr;
    public bool StartCutscene;

    public int Damage = 0;
    public bool DestroyOnGameover = false;
    public bool IgnorePlayerCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        ViewTrigger = GameObject.Find("ViewTrigger");

        pl = GameObject.Find("Player").GetComponent<Player>();
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        if (StartOFF)
        {
            for (int i = 0; i < TF.Length; i++)
                OnTrigger(false, i);
        }


        inv = GameObject.Find("Player").GetComponent<Inventory>();

        if (NeedItem>-1)
        {
            


                GameObject g = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/NeedItemSine2"), GameObject.Find("Canvas").transform);
                g.transform.SetAsFirstSibling();

                int NeedItemID = -1;
                int NeedItemCount = -1;

              
                NeedItemID = NeedItem;
                NeedItemCount = 1;
                

                g.transform.Find("NeedItemSineImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inv.GetItemInDatabase(NeedItemID).itemNames[0]);
                g.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + NeedItemCount;
                NeedItemGameobject.Add(g);
            

        }

        BuildingsMatch = new List<int>();
        for (int j = 0; j < NeedBuildingsToConstruct.Length; j++)
        {
            BuildingsMatch.Add(0);
        }


        if (NeedBuildingsToConstruct.Length > 0)
        {

            for (int i = 0; i < constr.OBOnBoard.Count; i++)
            {
                for (int j = 0; j < NeedBuildingsToConstruct.Length; j++)
                {
                    if (constr.OBOnBoard[i].ID == NeedBuildingsToConstruct[j])
                    {
                        BuildingsMatch[j] = 1;
                    }
                }
            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        if (pl.CutSceenMode) return;

        if (pl._gameover && DestroyOnGameover)
        {
            for (int i = 0; i < ONObjects.Length; i++)
                Destroy(ONObjects[i]);
            Finish = true;
        }
        
        if (pl.inv.showinvent || pl.inv.showjournal  || pl.menu.MenuONOFF || pl._gameover) return;
        if (NeedItem > -1) DrawNeedItemControll();

        if (constr.TutorialPause) return;
        if (DestroyTimer > Time.fixedTime && DestroyTimer - 0.1f < Time.fixedTime)
        {
            for (int i = 0; i < ONObjects.Length; i++)
                Destroy(ONObjects[i]);
            Finish = true;

        }

        if (Damage > 0 && pl.Invinc > Time.fixedTime)
            return;




        


        NeededBuildings();


        if (!pl.coll_obj.Contains(gameObject) && !pl.MouseUI.GetComponent<MouseController>().ObjectColl(gameObject))
        {
            if (!DoOnes)
            {
                if (!OnEnter && UndoActionIfNoColl)
                {
                    //   Allactions();
                    for (int i = 0; i < TF.Length; i++)
                        OnTrigger(!TF[i], i);
                }

                //Finish = false;
            }
            return;

        }

        if(IgnorePlayerCollision && pl.coll_obj.Contains(gameObject) && !pl.MouseUI.GetComponent<MouseController>().ObjectColl(gameObject))
            return;

        if (!OnEnter)
        {
            if (DoOnes)
            {
                if (!Finish)
                {
                    Allactions();
                print("Allactions");
                    Finish = true;
                }
            }
            else Allactions();

        }
        else if ((pl.IM.enter_b || pl.IM.space_b || pl.IM.LeftMouseButtonDown) && pl.IM.ActionDelay<Time.fixedTime)
        {
            
            if (NeedItem == -1)
            {
                if (!DoOnes)
                {
                    if (SwitchState)
                    {
                        for (int i = 0; i < TF.Length; i++)
                            TF[i] = !TF[i];
                    }

                    Allactions();
                }
                else
                {
                    if (!Finish)
                    {
                        Allactions();
                        Finish = true;
                    }

                }
            }
            else
            {
                if (pl.inv.CheckItem(NeedItem))
                {
                      
                        if (!Finish)
                        {
                            Allactions();
                            Finish = true;
                        }

                        
                }


            }

        }

                
            
        
    }

    void Allactions()
    {
    

        if (StartCutscene) pl.CutSceenMode = true;
        if (DestroyTimerDelay > 0) DestroyTimer = Time.fixedTime + DestroyTimerDelay;

      

        for (int i = 0; i < TF.Length; i++)
            OnTrigger(TF[i], i);

        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().Play();




        if (ShakeCamera > 0) pl.MainCamera.GetComponent<CameraBor>().CamShakeTimer = ShakeCamera;



        if (Damage < 0)
            pl.Heal(Damage*-1, "HealEffect");

        if (NeedItem > -1) pl.inv.ReduceItemCount(NeedItem, 1);


        if (SelfDestroy)
        {
            if (NeedItemGameobject != null)
            {
                for (int i = 0; i < NeedItemGameobject.Count; i++)
                {
                    Destroy(NeedItemGameobject[i]);
                }
            }

            if (GetComponent<BoxCollider2D>() != null)
                pl.RescanInBounds(new Bounds(new Vector3(transform.position.x, transform.position.y, 0), GetComponent<BoxCollider2D>().bounds.size*3));

            
            if (BloodEffectOnSelfDestroy)
            {
                if (GetComponent<StatsControll>() != null) GetComponent<StatsControll>().HP = 0;
                else
                pl.BlowThis(gameObject);
            }
            else
                Destroy(gameObject);



        }

        pl.IM.ActionDelay = Time.fixedTime + PushDelay;
    }



    void NeededBuildings()
    {
        if (NeedBuildingsToConstruct.Length <= 0) return;
        
        if (!CheckForConstrcutedBuildigs()) return;

        if (!DoOnes)
        {
            for (int i = 0; i < TF.Length; i++)
                OnTrigger(TF[i], i);
            return;
        }


        if (!Finish)
        {
            for (int i = 0; i < TF.Length; i++)
                OnTrigger(TF[i], i);
            Finish = true;
        }
      
            
        
    }






    bool CheckForConstrcutedBuildigs()
    {
        bool result = false;

        for (int i = 0; i < NeedBuildingsToConstruct.Length; i++)
        {
            if (NeedBuildingsToConstruct[i] == constr.LastBuildingConstructed)
            {
                BuildingsMatch[i] = 1;

            }
        }


        if (BuildingsMatch.Sum() >= NeedBuildingsToConstruct.Length)
        {
            result = true;
        }

        return result;
    }




    void DrawNeedItemControll()
    {
        float dialogyplus = 0;

        if (GetComponent<Character>() != null)
        {
            dialogyplus = 0.5f;
        }


        if (NeedItemGameobject == null)
        {
            return;
        }


        for (int n = 0; n < NeedItemGameobject.Count; n++)
        {
            NeedItemGameobject[n].transform.position = pl.MainCamera.WorldToScreenPoint(new Vector3(transform.position.x - 0.5f + 0.5f * n, transform.position.y + 0.6f + dialogyplus, 1));




            if (Mathf.Abs(transform.position.x - pl.transform.position.x) < NeedItemDrawRange && Mathf.Abs(transform.position.y - pl.transform.position.y) < NeedItemDrawRange)
            {

                Colors(NeedItemGameobject, 3);

            }
            else  Colors(NeedItemGameobject, -3);
            


        }

    }

    public void Colors(List<GameObject> obj, float color)
    {
        for (int n = 0; n < obj.Count; n++)
        {
            float ClampedAlpha = Mathf.Clamp(obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color.a + color * Time.deltaTime, 0, 1);

            obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color = new Color(1, 1, 1, ClampedAlpha);

            obj[n].transform.Find("BG").GetComponent<Image>().color = obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color;

            if (obj[n].transform.Find("Text") != null)
            {
                obj[n].transform.Find("Text").GetComponent<TextMeshProUGUI>().color = obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color;
            }
        }
    }


    void OnTrigger(bool TF, int Obnum)
    {

        if (ONObjects[Obnum] == null) return;
        
        if (ONObjects[Obnum].GetComponent<Animator>() != null)
            ONObjects[Obnum].GetComponent<Animator>().SetBool("Start", TF);

        if (ONObjects[Obnum].GetComponent<AnimationFrame>() != null)
            ONObjects[Obnum].GetComponent<AnimationFrame>().enabled = TF;


        if (ONObjects[Obnum].GetComponent<TextMeshProUGUI>() != null)
            ONObjects[Obnum].GetComponent<TextMeshProUGUI>().enabled = TF;



        if (PlayTargetSound)
        {
            if (ONObjects[Obnum].GetComponent<AudioSource>() != null && TF)
            {
                if(!ONObjects[Obnum].GetComponent<AudioSource>().isPlaying)
                ONObjects[Obnum].GetComponent<AudioSource>().Play();
            }

            if (ONObjects[Obnum].GetComponent<AudioSource>() != null && !TF)
                ONObjects[Obnum].GetComponent<AudioSource>().Stop();
        }


        if (ONObjects[Obnum].GetComponent<Dialog>() != null && !TF)
        {
            ONObjects[Obnum].GetComponent<Dialog>().enabled = false;
                
        }



        if (OnlyAnim) return;


        if (ONObjects[Obnum].GetComponent<Seeker>() != null)
            ONObjects[Obnum].GetComponent<Seeker>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<SpriteRenderer>() != null )
            ONObjects[Obnum].GetComponent<SpriteRenderer>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<BoxCollider2D>() != null)
            ONObjects[Obnum].GetComponent<BoxCollider2D>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<PolygonCollider2D>() != null)
            ONObjects[Obnum].GetComponent<PolygonCollider2D>().enabled = TF;

      
            pl.RescanInBounds(new Bounds(new Vector3(ONObjects[Obnum].transform.position.x, ONObjects[Obnum].transform.position.y, 0), Vector3.one*10));

        print("RescanInBounds trtr");

        if (ONObjects[Obnum].GetComponent<Light>() != null)
            ONObjects[Obnum].GetComponent<Light>().enabled = TF;

        if (TF)
        {
            if (ONObjects[Obnum].GetComponent<ParticleSystem>() != null)
                ONObjects[Obnum].GetComponent<ParticleSystem>().Play(true);
        }
        else
        {
            if (ONObjects[Obnum].GetComponent<ParticleSystem>() != null)
                ONObjects[Obnum].GetComponent<ParticleSystem>().Stop(true);
        }

        if (ONObjects[Obnum].GetComponent<CharacterMove>() != null)
            ONObjects[Obnum].GetComponent<CharacterMove>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<Door>() != null)
            ONObjects[Obnum].GetComponent<Door>().enabled = TF;


        if (ONObjects[Obnum].GetComponent<MovementControll>() != null)
            ONObjects[Obnum].GetComponent<MovementControll>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<Trigger>() != null)
            ONObjects[Obnum].GetComponent<Trigger>().Finish = false;

        if (ONObjects[Obnum].GetComponent<BoxCollider>() != null)
            ONObjects[Obnum].GetComponent<BoxCollider>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<MeshCollider>() != null)
            ONObjects[Obnum].GetComponent<MeshCollider>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<Light>() != null)
            ONObjects[Obnum].GetComponent<Light>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<SpriteRenderer>() != null)
            ONObjects[Obnum].GetComponent<SpriteRenderer>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<MeshRenderer>() != null)
            ONObjects[Obnum].GetComponent<MeshRenderer>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<SkinnedMeshRenderer>() != null)
            ONObjects[Obnum].GetComponent<SkinnedMeshRenderer>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<Light>() != null)
            ONObjects[Obnum].GetComponent<Light>().enabled = TF;

        if (ONObjects[Obnum].GetComponent<StatsControll>() != null)
            ONObjects[Obnum].GetComponent<StatsControll>().enabled = TF;


        if (ONObjects[Obnum].GetComponent<PubObject>() != null)
            ONObjects[Obnum].GetComponent<PubObject>().enabled = TF;


        print("OnTrigger " + ONObjects[Obnum].name + " / " + TF);

        for (int i = 0; i < ONObjects[Obnum].transform.childCount; i++)
        {
            if (TF)
            {
                if (ONObjects[Obnum].transform.GetChild(i).GetComponent<ParticleSystem>() != null)
                    ONObjects[Obnum].transform.GetChild(i).GetComponent<ParticleSystem>().Play(true);
            }
            else
            {
                if (ONObjects[Obnum].transform.GetChild(i).GetComponent<ParticleSystem>() != null)
                    ONObjects[Obnum].transform.GetChild(i).GetComponent<ParticleSystem>().Stop(true);
            }

            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = TF;

            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = TF;

            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<Light>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<Light>().enabled = TF;

            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<SkinnedMeshRenderer>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = TF;

            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<MeshRenderer>().enabled = TF;

            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<MeshCollider>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<MeshCollider>().enabled = TF;


            if (ONObjects[Obnum].transform.GetChild(i).GetComponent<BoxCollider>() != null)
                ONObjects[Obnum].transform.GetChild(i).GetComponent<BoxCollider>().enabled = TF;

            for (int ii = 0; ii < ONObjects[Obnum].transform.GetChild(i).childCount; ii++)
            {
                if (ONObjects[Obnum].transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>() != null)
                    ONObjects[Obnum].transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>().enabled = TF;

                if (ONObjects[Obnum].transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>() != null)
                    ONObjects[Obnum].transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>().enabled = TF;
            }
        }
        
        
    }
}
