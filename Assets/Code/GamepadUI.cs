using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepadUI : MonoBehaviour
{

    private InputMode IM;
    public bool DrawOnlyOnGamepad;

    public Sprite MouseSPRT;
    public Sprite KeyBoardSPRT;

    public Sprite GamepadSPRT;

    public Sprite SwitchGamepad;
    private Sprite StartSPRT;
    private Image IMG;
    private SpriteRenderer SPRT;

    private void Awake()
    {
        IMG = GetComponent<Image>();
        SPRT = GetComponent<SpriteRenderer>();

#if UNITY_SWITCH

        if (IMG != null)
            IMG.sprite = SwitchGamepad;

        if (SPRT != null)
            SPRT.sprite = SwitchGamepad;
#endif

#if UNITY_PS4 || UNITY_PS5

        if (IMG != null)
            IMG.sprite = GamepadSPRT;

        if (SPRT != null)
            SPRT.sprite = GamepadSPRT;
#endif

    }

    void Start()
    {
        IM = GameObject.Find("Constructor").GetComponent<InputMode>();

        if(IMG!=null)
        StartSPRT = IMG.sprite;
    }
    
    void Update()
    {
        /*
        if (IMG != null)
            IMG.enabled = false;
        if (SPRT != null)
            SPRT.enabled = false;
            */

#if UNITY_STANDALONE
        if (!IM.joystick)
        {
            if (IMG != null)
            {
               
                if (DrawOnlyOnGamepad) IMG.enabled = false;
                if (GamepadSPRT != null)
                {
                   

                    if (IM.MouseMode) IMG.sprite = MouseSPRT;
                    else IMG.sprite = KeyBoardSPRT;

                }
            }

            if (SPRT != null)
            {
                
                if (DrawOnlyOnGamepad) SPRT.enabled = false;

                if (GamepadSPRT != null)
                {
                    if (IM.MouseMode) SPRT.sprite = MouseSPRT;
                    else SPRT.sprite = KeyBoardSPRT;

                }
            }
            return;
        }
#endif

        if (IMG != null)
        {
            if (DrawOnlyOnGamepad) IMG.enabled = true;

#if UNITY_STANDALONE
            if (GamepadSPRT != null)
            IMG.sprite = GamepadSPRT;
            else IMG.enabled = false;
#endif

#if UNITY_SWITCH
            if (SwitchGamepad != null)
                IMG.sprite = SwitchGamepad;
            else IMG.enabled = false;
#endif

#if UNITY_PS4 || UNITY_PS5
            if (SwitchGamepad != null)
                IMG.sprite = GamepadSPRT;
            else IMG.enabled = false;
#endif
        }

        if (SPRT != null)
        {

            if (DrawOnlyOnGamepad) SPRT.enabled = true;

#if UNITY_STANDALONE
            if (GamepadSPRT != null) 
            SPRT.sprite = GamepadSPRT;
            else SPRT.enabled = false;
#endif

#if UNITY_SWITCH
            if (SwitchGamepad != null)
                SPRT.sprite = SwitchGamepad;
            else SPRT.enabled = false;
#endif

#if UNITY_PS4 || UNITY_PS5
            if (SwitchGamepad != null)
                SPRT.sprite = GamepadSPRT;
            else SPRT.enabled = false;
#endif

        }



    }
}
