using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    public float Delay = 2;
    private float Timer;
    private bool Active;
    private SpriteRenderer SPRT;
    private Light LightO;
    private Image IMG;
    private Text TXT;
    private BoxCollider2D Box;

    public float Speed = 0.01f;
    private float StartAlpha;


    public bool Scale;

    void Start()
    {
        SPRT = GetComponent<SpriteRenderer>();
        LightO = GetComponent<Light>();
        IMG = GetComponent<Image>();
        TXT = GetComponent<Text>();

        // Box = GetComponent<BoxCollider2D>();
        StartAlpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer < Time.fixedTime)
        {
            Active = !Active;
            Timer = Time.fixedTime + Delay;
        }


        if(LightO!=null)
        {
            if (!Active && LightO.color.r > 0.3f) LightO.color = new Color(LightO.color.r - Speed, LightO.color.r - Speed, LightO.color.r - Speed, LightO.color.a - Speed);
            if (Active && LightO.color.r < StartAlpha) LightO.color = new Color(LightO.color.r + Speed, LightO.color.r + Speed, LightO.color.r + Speed, LightO.color.a + Speed);

        }



        if (SPRT != null)
        {
            if (!Active && SPRT.color.a > 0.3f) SPRT.color = new Color(SPRT.color.r, SPRT.color.g, SPRT.color.b, SPRT.color.a - Speed);
            if (Active && SPRT.color.a < StartAlpha) SPRT.color = new Color(SPRT.color.r, SPRT.color.g, SPRT.color.b, SPRT.color.a + Speed);
        }

        if (IMG != null)
        {
            if (!Active && IMG.color.a > 0.3f) IMG.color = new Color(IMG.color.r, IMG.color.g, IMG.color.b, IMG.color.a - Speed);
            if (Active && IMG.color.a < StartAlpha) IMG.color = new Color(IMG.color.r, IMG.color.g, IMG.color.b, IMG.color.a + Speed);
        }


        if (TXT != null)
        {
            if (!Active && TXT.color.a > 0.3f) TXT.color = new Color(TXT.color.r, TXT.color.g, TXT.color.b, TXT.color.a - Speed);
            if (Active && TXT.color.a < StartAlpha) TXT.color = new Color(TXT.color.r, TXT.color.g, TXT.color.b, TXT.color.a + Speed);
        }

        if (Scale)
        {
            if(!Active)
            transform.localScale = new Vector3(transform.localScale.x+ Speed / 10, transform.localScale.y + Speed/10, 1);
            else transform.localScale = new Vector3(transform.localScale.x - Speed / 10, transform.localScale.y - Speed / 10, 1);
        }
        // if (SPRT.color.a <= 0) Box.enabled = false;
        // if (SPRT.color.a >= 1) Box.enabled = true;
    }
}
