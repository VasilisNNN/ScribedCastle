using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFrame : MonoBehaviour
{
    public float FramesDelay = 0.1f;
    public float CycleDelay = 1;

    public Sprite[] SPRT;
    public Sprite[] SPRT_AtTheEnd;


    private SpriteRenderer Renderer;
    private Image RendererImage;
    public int num { get; set; }
    private int numend;
    private float Timer;

    public bool DestroyAtTheEnd;
    public bool Loop = true;
    private bool EndAnim;
    private Constructor constr;

    public bool Play { get; set; }
    public bool StartFromRND = true;
   
    public float RND_CycleDelay = 0;


    private Sprite StartSPRT;
    private Player pl;

    private void Awake()
    {
        pl = InitializeObjects.PL;
        Play = true;
        if(GameObject.Find("Constructor")!=null)
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        Renderer = GetComponent<SpriteRenderer>();
        
        RendererImage = GetComponent<Image>();
        if (constr != null)
        {
            if (constr.Game_SPEED > 0)
            {
                if (StartFromRND)
                {
                    Timer = FramesDelay  + Random.Range(0.1f, 0.6f);
                  
                }
                else Timer = FramesDelay ;
            }
            else
            {
                if (StartFromRND)
                 Timer = FramesDelay  + Random.Range(0.1f, 0.6f);
                else Timer = FramesDelay;
            }
        }
        else
        {

            if (StartFromRND)
                Timer = FramesDelay + Random.Range(0.1f, 0.6f);
            else Timer = FramesDelay ;
        }

        if (Renderer != null) StartSPRT = Renderer.sprite;
    }

     void Update()
    {

        if (pl != null)
            if (pl.Pause()) return;

        PlayAnimation();
    }


    void PlayAnimation()
    {
        
        if (!Play)
        {
            if (Renderer != null)
                Renderer.sprite = StartSPRT;
            return;
        }

        float sp = 1;
        if (constr != null)
        {
            sp = constr.Game_SPEED;
        }

        Timer -= 0.02f * Time.deltaTime * 100;


        if (num >= SPRT.Length) num = SPRT.Length - 1;

        if (sp == 0)
        {
            return;
        }




            if (!EndAnim)
            {
                if (Renderer != null)
                    Renderer.sprite = SPRT[num];

                if (RendererImage != null)
                    RendererImage.sprite = SPRT[num];

                if (Timer < 0)
                {

                    num++;
                    Timer = FramesDelay / sp;
                }
            }

            if (Timer < 0 && EndAnim)
            {
                if (numend < SPRT_AtTheEnd.Length - 1)
                    numend++;
                else numend = 0;

                if (Renderer != null)
                    Renderer.sprite = SPRT_AtTheEnd[numend];

                if (RendererImage != null)
                    RendererImage.sprite = SPRT_AtTheEnd[numend];



                Timer = FramesDelay / sp;
            }

            if (num >= SPRT.Length)
            {
                if (!Loop) num = SPRT.Length - 1;

                if (DestroyAtTheEnd)
                {
                    if (GetComponent<AudioSource>() == null)
                        Destroy(gameObject);
                    else
                    {
                        if (!GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
                    }
                }
                if (!DestroyAtTheEnd && Loop)
                {
                    num = 0;
                    Timer = CycleDelay / sp + Random.Range(0, RND_CycleDelay);
                }
                if (!DestroyAtTheEnd && !Loop)
                    EndAnim = true;


            }
        
    }

    public void ResetAnimation()
    {
        num = 0;
        EndAnim = false;
    }
}
