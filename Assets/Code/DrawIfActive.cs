using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Tilemaps;

public class DrawIfActive : MonoBehaviour
{
    public SpriteRenderer[] SPRTs;
    public SpriteRenderer[] NotActiveSPRTs;
    private int[] Numbs;
    private int[] NoActiveNumbs;

    public bool TargetBool;

    public int QuestID = -1;

    private Player pl;
    private Constructor constr;
    public bool _destroy;
    public bool OnlyAnimation;

    public int MinVision = -99;
    public int MaxVision = 99;

    public int MinSniff = -99;
    public int MaxSniff = 99;

    public int MinDay = 0;
    public int MaxDay = 9999999;

    private bool Draw = true;
    public bool DoOnes = false;

    private bool Done;
    public float ShakeCamera = 0;
    public bool PlayAudio = true;
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        Numbs = new int[SPRTs.Length];

        if(NotActiveSPRTs!=null)
        NoActiveNumbs = new int[NotActiveSPRTs.Length];

        if (!OnlyAnimation)
        {
            for (int i = 0; i < SPRTs.Length; i++)
            {
                SPRTs[i].enabled = false;
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        if (SPRTs != null)
        {
            for (int i = 0; i < SPRTs.Length; i++)
            {
                Gizmos.color = new Color(0.2f, 1, 0.2f);
                Gizmos.DrawLine(transform.position, SPRTs[i].transform.position);
              
            }
        }
    }
        // Update is called once per frame
    void Update()
    {
     
        if (Done) return;

        if (SPRTs != null)
        {
            for (int i = 0; i < SPRTs.Length; i++)
            {
                if (SPRTs[i].enabled) Numbs[i] = 1;
                else Numbs[i] = 0;

              
            }
        }


        if (NotActiveSPRTs != null)
        {
            for (int i = 0; i < NotActiveSPRTs.Length; i++)
            {
                if (NotActiveSPRTs[i] != null)
                {
                    NoActiveNumbs[i] = 0;


                    //print("Numbs " + Numbs);
                }
                else
                {
                    NoActiveNumbs[i] = 1;

                }
            }


           
        }
        if (Numbs.Length > 0)
        {
            if (Numbs.Sum() == Numbs.Length)
            {
                FinishQuest();
                if (ShakeCamera > 0) pl.MainCamera.GetComponent<CameraBor>().CamShakeTimer = ShakeCamera;

                if (DoOnes) Done = true;
               
                OnOffObject(gameObject, TargetBool);
            }
            else OnOffObject(gameObject, !TargetBool);
        }


        if (NoActiveNumbs.Length > 0)
        {
            if (NoActiveNumbs.Sum() == NoActiveNumbs.Length)
            {
                FinishQuest();
                if (ShakeCamera > 0) pl.MainCamera.GetComponent<CameraBor>().CamShakeTimer = ShakeCamera;

                if (_destroy) Destroy(gameObject);
                if (DoOnes) Done = true;
                OnOffObject(gameObject, TargetBool);
                
            }
            else OnOffObject(gameObject, !TargetBool);
        }


        VisionDraw();

    }


    void VisionDraw()
    {

        if (MaxVision < pl.Vision || MinVision > pl.Vision || MaxSniff < pl.Sniff || MinSniff > pl.Sniff || MaxDay < constr.SL.DayNumber || MinDay > constr.SL.DayNumber)
        {
            if (Draw)
            {
                OnOffObject(gameObject, false);
                Draw = false;
            }
            if (_destroy) Destroy(gameObject);
        }
        else
        {
            if (!Draw)
            {
               
                OnOffObject(gameObject, true);
                Draw = true;
            }
        }
        

    }

    private void FinishQuest()
    {
        if (QuestID > -1)
        {
            pl.inv.AddQuest(QuestID);


            pl.inv.DoneQuest(QuestID);


            QuestID = -1;
        }
    }





    private void OnOffObject(GameObject uiel, bool tf)
    {
        if (_destroy) return;
        
        if (uiel == null) return;

        TurnComponentsONOFF(uiel.gameObject, tf);

        ToggleThroughChild(uiel.transform, tf);


        
    }


    void ToggleThroughChild(Transform parent, bool TF)
    {

        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            TurnComponentsONOFF(child.gameObject, TF);
            ToggleThroughChild(child, TF);
        }

    }




    void TurnComponentsONOFF(GameObject uiel, bool tf)
    {


       
        if (uiel.GetComponent<StatsControll>() != null)
        {
          
            if (constr.ShowChargeUI)
            {
                if(uiel.GetComponent<StatsControll>().ChargeUI!=null)
                TurnComponentsONOFF(uiel.GetComponent<StatsControll>().ChargeUI, tf);
                if (uiel.GetComponent<StatsControll>().HPUI != null)
                    TurnComponentsONOFF(uiel.GetComponent<StatsControll>().HPUI, tf);
            }

            uiel.GetComponent<StatsControll>().enabled = tf;
        }

        if (uiel.GetComponent<Door>() != null)
        {
            uiel.GetComponent<Door>().enabled = tf;
        }

        if (uiel.GetComponent<Enemies>() != null)
        {
            uiel.GetComponent<Enemies>().enabled = tf;
        }

        if (uiel.GetComponent<AnimationFrame>() != null )
        {
            uiel.GetComponent<AnimationFrame>().enabled = tf;
        }

        if (uiel.GetComponent<AudioSource>() != null && tf && PlayAudio)
        {
            uiel.GetComponent<AudioSource>().Play();
        }


        if (uiel.GetComponent<TilemapRenderer>() != null)
        {
            uiel.GetComponent<TilemapRenderer>().enabled = tf;
        }


        if (uiel.GetComponent<CharacterMove>() != null)
        {
            uiel.GetComponent<CharacterMove>().enabled = tf;
        }

        if (uiel.GetComponent<MovementControll>() != null)
        {
            uiel.GetComponent<MovementControll>().enabled = tf;
        }


        if (uiel.GetComponent<Animator>() != null)
        {
            uiel.GetComponent<Animator>().SetBool("Start", tf);
        }

        if (OnlyAnimation) return;

        if (uiel.GetComponent<Image>() != null)
        {
            uiel.GetComponent<Image>().enabled = tf;


            for (int i = 0; i < uiel.transform.childCount; i++)
            {

                if (uiel.transform.GetChild(i).GetComponent<Image>() != null)
                {
                    uiel.transform.GetChild(i).GetComponent<Image>().enabled = tf;
                }
            }
        }

        if (uiel.GetComponent<CharacterMove>() != null)
        {
            uiel.GetComponent<CharacterMove>().enabled = tf;
        }

        if (uiel.GetComponent<SpriteRenderer>() != null)
        {
            uiel.GetComponent<SpriteRenderer>().enabled = tf;
        }
        if (uiel.GetComponent<BoxCollider2D>() != null)
        {
            uiel.GetComponent<BoxCollider2D>().enabled = tf;
        }
        if (uiel.GetComponent<PolygonCollider2D>() != null)
        {
            uiel.GetComponent<PolygonCollider2D>().enabled = tf;
        }

        if (uiel.GetComponent<Dialog>() != null)
        {
            uiel.GetComponent<Dialog>().enabled = tf;
        }

    }





}
