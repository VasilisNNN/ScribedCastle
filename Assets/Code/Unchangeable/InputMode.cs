using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;


#if UNITY_SWITCH
using System.Threading.Tasks;
using nn.hid;
#endif



public class InputMode : MonoBehaviour
    {


#if UNITY_SWITCH
    private System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();


    private NpadId npadId = NpadId.Invalid;
    private NpadStyle npadStyle = NpadStyle.Invalid;
    private NpadState npadState = new NpadState();

   
#endif



    //public bool LeftMenu { get; set; }
    public bool SideButton { get; set; }
        private bool HorizontalFlipBuffer, SideButtonBuffer, exit_bBuffer, enter_bBuffer, menu_bBuffer, LeftTriggerBuffer, RightTriggerBuffer, QuestBookBuffer, LeftMouseButtonBuffer, RightMouseButtonBuffer, RightstickpushBuffer, LeftstickpushBuffer, HealBuffer;
        private float AttackBufferF, InventoryBufferF, SideButtonBufferF, exit_bBufferF, enter_b_pushBufferF, enter_bBufferF, menu_bBufferF, LeftTriggerBufferF, RightTriggerBufferF, QuestBookBufferF, LeftMouseButtonBufferF, RightMouseButtonBufferF, RightstickpushBufferF, LeftstickpushBufferF;
    private bool Achbutton_bBuffer, journal_bBuffer, Dash_bBuffer, space_bBuffer, inventory_bBuffer, enterENTER_bBuffer;

    public float _horizontal { get; set; }
        public float _vertical { get; set; }

        public float _horizontal_R { get; set; }
        public float _vertical_R { get; set; }

        public float DPADY { get; set; }
        public float DPADX { get; set; }

        public bool CamMove { get; set; }
        public bool CamMoveHold { get; set; }
        public bool HorizontalFlip { get; set; }

        // public bool SpaceB { get; set; }
        public bool _vertical_button { get; set; }



        public bool _horizontalPush { get; set; }
        public bool _verticalPush { get; set; }
        public bool _horizontal_R_Push { get; set; }
        public bool _vertical_R_Push { get; set; }
        public bool _vertical_DPAD_Push { get; set; }
        public bool _horizontal_DPAD_Push { get; set; }


        public float VerticalArrows { get; set; }
        private int _horizontalScroll_Timer;

        public float Enterdeley { get; set; }


        public bool exit_b { get; set; }
        public bool delete_b { get; set; }

        public bool Rightstickpush { get; set; }
        public bool menu_b { get; set; }

        public bool enter_b { get; set; }
        public bool enter_b_hold { get; set; }
        public bool shift { get; set; }

        public bool joystick = false;

        public float GamepadVertTimer { get; set; }
        public float GamepadHorTimer { get; set; }
        private float GamepadRHorTimer;

        public bool QuestBook { get; set; }

        public bool LeftTrigger { get; set; }
        public bool RightTrigger { get; set; }
    

        public bool LeftMouseButton { get; set; }
        public bool LeftMouseButtonDown { get; set; }
        public bool RightMouseButton { get; set; }
    public bool RightMouseButtonDown { get; set; }

    public bool Heal { get; set; }
    public bool  inventory_b { get; set; }
    public bool space_b { get; set; }
    public bool Dash { get; set; }
  

    public bool  Achbutton { get; set; }
    public float MouseScroll { get; set; }

    public bool journal_b { get; set; }
    public bool UButton { get; set; }
    public Vector2 MousePosition;

        private Image JoyConImage, ProGamepadImage;

    
    private bool RightStickMoveX, RightStickMoveY;

    public bool MouseMode { get; set; }
    private Vector3 PrevMousePos;
    private float MousePosTimer;

    public bool BKey { get; set; }
    public bool OKey { get; set; }
    public bool ZLKey { get; set; }

    public float ActionDelay { get; set; }
    public int CraftedItems { get; set; }

#if UNITY_PS5 || UNITY_PS4
    private SonySaveDataMain PS_SaveMain;
#endif

    private void Awake()
        {
        if (GameObject.Find("GamepadChoise") == null)
        {
            GameObject GamepadChoise = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/GamepadChoise"), GameObject.Find("Canvas").transform);
            GamepadChoise.name = "GamepadChoise";

        }



        JoyConImage = GameObject.Find("GamepadChoise").transform.Find("Joycon").GetComponent<Image>();
        ProGamepadImage = GameObject.Find("GamepadChoise").transform.Find("ProGamepad").GetComponent<Image>();

        JoyConImage.color = new Color(1, 1, 1, 0);
        ProGamepadImage.color = new Color(1, 1, 1, 0);
        #if UNITY_PS5 || UNITY_PS4
        PS_SaveMain = GameObject.Find("Constructor").GetComponent<SonySaveDataMain>();
        #endif
#if UNITY_SWITCH
        Npad.Initialize();
        Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1 });
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);

        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
            NpadState handheldState = npadState;

            if (handheldStyle != NpadStyle.None)
            {
                Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);

                if (npadId != NpadId.Handheld)
                {
                    JoyConImage.color = new Color(1, 1, 1, 1);
                    ProGamepadImage.color = new Color(1, 1, 1, 0);
                }
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
            }

        }

#endif

    }

    void OnEnable()
    {
#if UNITY_STANDALONE
      //  PlayerInputActionMap.Enable();
#endif
    }



    private bool UpdatePadState()
    {
#if UNITY_SWITCH
        DrawGamepad();

        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = npadState;
        if (handheldStyle != NpadStyle.None)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                if (npadId != NpadId.Handheld)
                {
                    JoyConImage.color = new Color(1, 1, 1, 1);
                    ProGamepadImage.color = new Color(1, 1, 1, 0);
                }
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
                return true;
            }
        }

        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;
        if (no1Style != NpadStyle.None)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if (no1State.buttons != NpadButton.None)
            {
                if (npadId != NpadId.No1)
                {
                    ProGamepadImage.color = new Color(1, 1, 1, 1);
                    JoyConImage.color = new Color(1, 1, 1, 0);
                }

                npadId = NpadId.No1;
                npadStyle = no1Style;
                npadState = no1State;

                return true;
            }
        }

        if ((npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
        {

            npadId = NpadId.Handheld;
            npadStyle = handheldStyle;
            npadState = handheldState;
        }
        else if ((npadId == NpadId.No1) && (no1Style != NpadStyle.None))
        {

            npadId = NpadId.No1;
            npadStyle = no1Style;
            npadState = no1State;
        }
        else
        {
            npadId = NpadId.Invalid;
            npadStyle = NpadStyle.Invalid;
            npadState.Clear();
            return false;
        }
        return true;
#endif

#if UNITY_STANDALONE
            return false;
#endif


        return false;
    }

    void Update()
        {

#if UNITY_STANDALONE


        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {


            if (joystick)
            {

                MouseMode = false;
                joystick = false;
            }
        }
        else
        {

            /*if (Input.GetButtonDown("Delete_J") || Input.GetButtonDown("Cancel_J") || Input.GetButtonDown("Enter_J"))
            {
                print("JOYSTICK");

                joystick = true;
            }*/

        }

        if (MousePosTimer < Time.fixedTime)
        {
            //  print("PrevMousePos " + PrevMousePos + " /  Input.mousePosition" + Input.mousePosition);

            if (PrevMousePos != Input.mousePosition)
            {

                MouseMode = true;
                PrevMousePos = Input.mousePosition;
            }
            MousePosTimer = Time.fixedTime + 0.1f;

        }


        if (LeftMouseButtonDown)
        {
            MouseMode = true;
        }
        if (_horizontal_R_Push || _vertical_R_Push || _horizontalPush || _verticalPush || enter_b || space_b || exit_b)
        {

            MouseMode = false;
        }

#endif

#if UNITY_SWITCH
        MouseMode = false;
        joystick = true;
#endif

#if UNITY_PS4 || UNITY_PS5
        MouseMode = false;
        joystick = true;
#endif



        if (!joystick)
        {
            KeyboardMouseControlls();
        }
        else
        {
#if UNITY_STANDALONE
            PCGamepadControlls();
#endif
#if UNITY_SWITCH

            SwitchGamepadControlls();

#endif
#if UNITY_PS5 || UNITY_PS4

            PS5GamepadControlls();

#endif


        }



    }





    void KeyboardMouseControlls()
    {

        /*
            MousePosition = LiveWallpaperCore.HookedInput.MousePosition;

            LeftMouseButtonDown = LiveWallpaperCore.HookedInput.GetMouseButtonDown(0);
            RightMouseButtonDown = LiveWallpaperCore.HookedInput.GetMouseButtonDown(1);

            LeftMouseButton = LiveWallpaperCore.HookedInput.GetMouseButton(0);
            RightMouseButton = LiveWallpaperCore.HookedInput.GetMouseButton(1);

            inventory_b = LiveWallpaperCore.HookedInput.GetKeyDown(LiveWallpaperCore.HookKeyCode.KEY_I);

            if (LiveWallpaperCore.HookedInput.GetKeyDown(LiveWallpaperCore.HookKeyCode.KEY_A))
                _horizontal = -1;
            if (LiveWallpaperCore.HookedInput.GetKeyDown(LiveWallpaperCore.HookKeyCode.KEY_D))
                _horizontal = 1;

            if (LiveWallpaperCore.HookedInput.GetKeyDown(LiveWallpaperCore.HookKeyCode.KEY_W))
                _vertical = -1;
            if (LiveWallpaperCore.HookedInput.GetKeyDown(LiveWallpaperCore.HookKeyCode.KEY_S))
                _vertical = 1;

            menu_b = LiveWallpaperCore.HookedInput.GetKeyDown(LiveWallpaperCore.HookKeyCode.ESC);
            
            return;
        */


#if UNITY_STANDALONE



        LeftMouseButtonDown = Input.GetMouseButtonDown(0);
        RightMouseButtonDown = Input.GetMouseButtonDown(1);

        MouseScroll = Input.GetAxis("Mouse ScrollWheel");
        MousePosition = Input.mousePosition;


        Heal = Input.GetButtonDown("Heal");
      //  journal_b = Input.GetButtonDown("Journal");
        inventory_b = Input.GetButtonDown("Inventory");
      //  Achbutton = Input.GetButtonDown("Achivements");


        LeftMouseButton = Input.GetMouseButton(0);



        RightMouseButton = Input.GetMouseButton(1);

        SideButton = Input.GetButtonDown("SideButton");

        LeftTrigger = Input.GetButtonDown("LeftTrigger");
        RightTrigger = Input.GetButtonDown("RightTrigger");


        //LeftMenu = Input.GetKeyDown(KeyCode.Z);
        QuestBook = Input.GetKeyDown(KeyCode.Q);

        //  SpaceB = Input.GetButtonDown("Space");

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical") * -1;

        _horizontal_R = Input.GetAxis("Horizontal_R");
        _vertical_R = Input.GetAxis("Vertical_R");
        space_b = Input.GetButtonDown("Space");

        _verticalPush = Input.GetButtonDown("Vertical");

        _horizontalPush = Input.GetButtonDown("Horizontal");


        _vertical_R_Push = Input.GetButtonDown("Vertical_R");
        _horizontal_R_Push = Input.GetButtonDown("Horizontal_R");


        VerticalArrows = Input.GetAxis("VerticalArrows");


        enter_b = Input.GetButtonDown("Enter");
        shift = Input.GetButton("Shift");
        enter_b_hold = Input.GetButton("Enter");

        exit_b = Input.GetButtonDown("Exit");
        delete_b = Input.GetButtonDown("Delete");




        menu_b = Input.GetButtonDown("Menu");
        CamMove = Input.GetKeyDown(KeyCode.LeftControl);
        CamMoveHold = Input.GetKey(KeyCode.LeftControl);

        HorizontalFlip = Input.GetButtonDown("HorizontalFlip");
        Rightstickpush = Input.GetButtonDown("RightStickPush");


      //  UButton = Input.GetKey(KeyCode.U);
        BKey = Input.GetButtonDown("BKey");
        OKey = Input.GetButtonDown("OKey");
#endif
    }



    void PCGamepadControlls()
    {

#if UNITY_STANDALONE

        HorizontalFlip = Input.GetButtonDown("HorizontalFlip");
        RightTrigger = Input.GetButtonDown("RightTrigger_J");

        LeftTrigger = Input.GetButtonDown("LeftTrigger_J");
        SideButton = Input.GetButtonDown("SideButton");

        menu_b = Input.GetButtonDown("Menu_J");
        exit_b = Input.GetButtonDown("Cancel_J");
        enter_b = Input.GetButtonDown("Enter_J");
        shift = Input.GetButton("Shift_J");
        delete_b = Input.GetButtonDown("Delete_J");
        enter_b_hold = Input.GetButton("Enter_J");


        QuestBook = Input.GetButtonDown("QuestBook_J");
        space_b = Input.GetButtonDown("Space_J");

        Heal = Input.GetButtonDown("Heal_J");
       // journal_b = Input.GetButtonDown("Journal_J");
        inventory_b = Input.GetButtonDown("Inventory_J");
       // Achbutton = Input.GetButtonDown("Achivements_J");

        _horizontal = Input.GetAxis("Horizontal_J");
        _vertical = Input.GetAxis("Vertical_J");


        _horizontal_R = Input.GetAxis("Horizontal_R_J");
        _vertical_R = Input.GetAxis("Vertical_R_J");



        Rightstickpush = Input.GetButtonDown("RightStickPush");

        DPADY = Input.GetAxis("DPADY");
        DPADX = Input.GetAxis("DPADX");
        

#endif

    }

    void SwitchGamepadControlls()
    {
#if UNITY_SWITCH
        if (!UpdatePadState()) return;

        enter_b = npadState.GetButtonDown(NpadButton.A);
        enter_b_hold = npadState.GetButton(NpadButton.A);
        Heal = npadState.GetButtonDown(NpadButton.X);

        exit_b = npadState.GetButtonDown(NpadButton.B);
        delete_b = npadState.GetButton(NpadButton.B);
        menu_b = npadState.GetButtonDown(NpadButton.Plus);
        Rightstickpush = npadState.GetButtonDown(NpadButton.StickR);
        HorizontalFlip = npadState.GetButtonDown(NpadButton.ZR);

        RightTrigger = npadState.GetButtonDown(NpadButton.R);
        LeftTrigger = npadState.GetButtonDown(NpadButton.L);
        SideButton = npadState.GetButtonDown(NpadButton.X);



        if (npadState.GetButton(NpadButton.StickLUp)) _vertical = 1;
        else if (npadState.GetButton(NpadButton.StickLDown)) _vertical = -1;
        else _vertical = 0;


        if (npadState.GetButton(NpadButton.StickLLeft)) _horizontal = -1;
        else if (npadState.GetButton(NpadButton.StickLRight)) _horizontal = 1;
        else _horizontal = 0;


        if (npadState.GetButton(NpadButton.StickRUp)) _vertical_R = 1;
        else if (npadState.GetButton(NpadButton.StickRDown)) _vertical_R = -1;
        else _vertical_R = 0;

        if (_vertical_R == 0)
        {
            if (npadState.GetButton(NpadButton.StickRLeft)) _horizontal_R = -1;
            else if (npadState.GetButton(NpadButton.StickRRight)) _horizontal_R = 1;
            else _horizontal_R = 0;
        }



        if (npadState.GetButton(NpadButton.Up)) DPADY = 1;
        else if (npadState.GetButton(NpadButton.Down)) DPADY = -1;
        else DPADY = 0;

        if (npadState.GetButton(NpadButton.Left)) DPADX = -1;
        else if (npadState.GetButton(NpadButton.Right)) DPADX = 1;
        else DPADX = 0;



        if (_horizontal > 0.1f || _horizontal < -0.1f)
        {
            if (GamepadHorTimer < Time.fixedTime)
            {
                _horizontalPush = true;
                GamepadHorTimer = Time.fixedTime + 0.01f;
            }
            else _horizontalPush = false;
        }
        else _horizontalPush = false;


        if (_horizontal_R > 0.1f || _horizontal_R < -0.1f)
        {
            if (GamepadRHorTimer < Time.fixedTime)
            {
                _horizontal_R_Push = true;
                GamepadRHorTimer = Time.fixedTime + 0.01f;
            }
            else _horizontal_R_Push = false;
        }
        else _horizontal_R_Push = false;


        if (DPADX > 0.1f || DPADX < -0.1f)
        {
            if (GamepadHorTimer < Time.fixedTime)
            {
                _horizontal_DPAD_Push = true;
                GamepadHorTimer = Time.fixedTime + 0.01f;
            }
            else _horizontal_DPAD_Push = false;
        }
        else _horizontal_DPAD_Push = false;

        if (DPADY > 0.1f || DPADY < -0.1f)
        {
            if (GamepadVertTimer < Time.fixedTime)
            {
                _vertical_DPAD_Push = true;
                GamepadVertTimer = Time.fixedTime + 0.01f;
            }
            else _vertical_DPAD_Push = false;
        }
        else _vertical_DPAD_Push = false;




        if (_vertical > 0.1f || _vertical < -0.1f)
        {
            if (GamepadVertTimer < Time.fixedTime)
            {
                _verticalPush = true;
                GamepadVertTimer = Time.fixedTime + 0.01f;
            }
            else _verticalPush = false;
        }
        else _verticalPush = false;

      //  journal_b = npadState.GetButtonDown(NpadButton.L);
        QuestBook = npadState.GetButtonDown(NpadButton.L);
        space_b = npadState.GetButtonDown(NpadButton.R);
        //Dash = npadState.GetButtonDown(NpadButton.B);
        inventory_b = npadState.GetButtonDown(NpadButton.Y);

        BKey = npadState.GetButtonDown(NpadButton.X);
        OKey = npadState.GetButtonDown(NpadButton.ZR);
        ZLKey = npadState.GetButtonDown(NpadButton.ZL);


        /*
          if (Mathf.Abs(npadState.analogStickR.y) < 100)
        _horizontal_R = npadState.analogStickR.x;
   else
    _vertical_R = npadState.analogStickR.y;

    if (npadState.analogStickR.y==0) _vertical_R = 0;

        Debug.Log("_horizontal_R: " + _horizontal_R);
        Debug.Log("_vertical_R: " + _vertical_R);
        */


        /*
        if (npadState.GetButtonDown(NpadButton.A))
        {
            Debug.Log("NpadButton.A Down");
        }
        else if (npadState.GetButtonUp(NpadButton.A))
        {
            Debug.Log("NpadButton.A Up");
        }*/



#endif
    }

   

    void PS5GamepadControlls()
    {
#if UNITY_PS5 || UNITY_PS4

        if (User.users == null) return;
        if (User.users.Length <= 0) return;
        if (User.users[0] == null) return;
       
        if (GamePad.activeGamePad == null) return;



        GamePad CurrentGamepad = GamePad.activeGamePad;
       
        
        enter_b = CurrentGamepad.IsCrossPressed;
        enter_b_hold = CurrentGamepad.currentFrame.cross;
        //Heal = CurrentGamepad.squareButton.wasPressedThisFrame;

        exit_b = CurrentGamepad.IsCirclePressed;
        delete_b = CurrentGamepad.IsCirclePressed;
        menu_b = CurrentGamepad.IsOptionsPressed;
        Rightstickpush = CurrentGamepad.IsDpadRightPressed;
       // HorizontalFlip = CurrentGamepad.currentFrame.R1;

        RightTrigger = CurrentGamepad.IsR1Pressed;
        LeftTrigger = CurrentGamepad.IsL1Pressed;
        SideButton = CurrentGamepad.IsSquarePressed;

    
        _vertical = CurrentGamepad.GetThumbstickLeft.y * -1;
        _horizontal = CurrentGamepad.GetThumbstickLeft.x;


        /*
        if (CurrentGamepad.GetThumbstickLeft.y>0) _vertical = 1;
        else if (CurrentGamepad.GetThumbstickLeft.y < 0) _vertical = -1;
        else _vertical = 0;
       
        if (CurrentGamepad.GetThumbstickLeft.x > 0) _horizontal = 1;
        else if (CurrentGamepad.GetThumbstickLeft.x < 0) _horizontal = -1;
        else _horizontal = 0;
         */


        if (_horizontal > 0.1f || _horizontal < -0.1f)
        {
            if (GamepadHorTimer < Time.fixedTime)
            {
                _horizontalPush = true;
                GamepadHorTimer = Time.fixedTime + 0.01f;
            }
            else _horizontalPush = false;
        }
        else _horizontalPush = false;


        if (_horizontal_R > 0.1f || _horizontal_R < -0.1f)
        {
            if (GamepadRHorTimer < Time.fixedTime)
            {
                _horizontal_R_Push = true;
                GamepadRHorTimer = Time.fixedTime + 0.01f;
            }
            else _horizontal_R_Push = false;
        }
        else _horizontal_R_Push = false;


        if (DPADX > 0.1f || DPADX < -0.1f)
        {
            if (GamepadHorTimer < Time.fixedTime)
            {
                _horizontal_DPAD_Push = true;
                GamepadHorTimer = Time.fixedTime + 0.01f;
            }
            else _horizontal_DPAD_Push = false;
        }
        else _horizontal_DPAD_Push = false;

        if (DPADY > 0.1f || DPADY < -0.1f)
        {
            if (GamepadVertTimer < Time.fixedTime)
            {
                _vertical_DPAD_Push = true;
                GamepadVertTimer = Time.fixedTime + 0.01f;
            }
            else _vertical_DPAD_Push = false;
        }
        else _vertical_DPAD_Push = false;




        if (_vertical > 0.1f || _vertical < -0.1f)
        {
            if (GamepadVertTimer < Time.fixedTime)
            {
                _verticalPush = true;
                GamepadVertTimer = Time.fixedTime + 0.01f;
            }
            else _verticalPush = false;
        }
        else _verticalPush = false;


        if (CurrentGamepad.IsDpadUpPressed) DPADY = 1;
        else if (CurrentGamepad.IsDpadDownPressed) DPADY = -1;
        else DPADY = 0;

        if (CurrentGamepad.IsDpadLeftPressed) DPADX = -1;
        else if (CurrentGamepad.IsDpadRightPressed) DPADX = 1;
        else DPADX = 0;


        _vertical_R = CurrentGamepad.GetThumbstickRight.y * -1;
        _horizontal_R = CurrentGamepad.GetThumbstickRight.x;
        
        
      //  journal_b = npadState.GetButtonDown(NpadButton.L);
        QuestBook = CurrentGamepad.IsL1Pressed;
        space_b = CurrentGamepad.IsR1Pressed;
        //Dash = npadState.GetButtonDown(NpadButton.B);
        inventory_b = CurrentGamepad.IsTrianglePressed;

        BKey = CurrentGamepad.IsSquarePressed;

        OKey = CurrentGamepad.IsR2Pressed;
        ZLKey = CurrentGamepad.IsL2Pressed;


#endif
    }


    void DrawGamepad()
    {
        if (JoyConImage.color.a > 0) JoyConImage.color = new Color(1, 1, 1, JoyConImage.color.a - 0.03f);
        if (ProGamepadImage.color.a > 0) ProGamepadImage.color = new Color(1, 1, 1, ProGamepadImage.color.a - 0.03f);

    }

}

