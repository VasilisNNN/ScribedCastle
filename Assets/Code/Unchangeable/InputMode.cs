using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputMode: MonoBehaviour
{

    private IInputs CurrentInputs;

    public bool SideButton { get; set; }

    public float _horizontal { get; set; }
        public float _vertical { get; set; }

        public float _horizontal_R { get; set; }
        public float _vertical_R { get; set; }

        public float DPADY { get; set; }
        public float DPADX { get; set; }

        public bool CamMove { get; set; }
        public bool CamMoveHold { get; set; }
        public bool HorizontalFlip { get; set; }

        public bool _vertical_button { get; set; }



        public bool _horizontalPush { get; set; }
        public bool _verticalPush { get; set; }
        public bool _horizontal_R_Push { get; set; }
        public bool _vertical_R_Push { get; set; }
        public bool _vertical_DPAD_Push { get; set; }
        public bool _horizontal_DPAD_Push { get; set; }


        public float VerticalArrows { get; set; }
        public int _horizontalScroll_Timer { get; }

        public float Enterdeley { get; set; }


        public bool exit_b { get; set; }
        public bool delete_b { get; set; }

        public bool Rightstickpush { get; set; }
        public bool menu_b { get; set; }

        public bool enter_b { get; set; }
        public bool enter_b_hold { get; set; }
        public bool shift { get; set; }

        public bool joystick { get; set; }


    public bool QuestBook { get; set; }
    public bool FadeMode { get; private set; }
        public bool LeftTrigger { get; set; }
        public bool RightTrigger { get; set; }

    public bool R2 { get; set; }
    public bool L2 { get; set; }
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
    public Vector2 MousePosition { get;set; }


    public bool RightStickMoveX { get; }
    public bool RightStickMoveY { get; }


    public bool MouseMode { get; set; }
    public Vector3 PrevMousePos { get; set; }
    public float MousePosTimer { get; set; }

    public bool BKey { get; set; }
    public bool OKey { get; set; }
    public bool ZLKey { get; set; }

    public float ActionDelay { get; set; }
    public int CraftedItems { get; set; }

    private float GamepadHorTimer, GamepadRHorTimer, GamepadVertTimer;
#if UNITY_PS5 || UNITY_PS4
    private SonySaveDataMain PS_SaveMain;
#endif

    private void Awake()
        {

#if UNITY_STANDALONE

        CurrentInputs = new PCKeyboardInputs();


#endif

#if UNITY_SWITCH

    joystick = true;
        CurrentInputs = new SwitchInputs();

#endif

#if UNITY_PS5 || UNITY_PS4
        PS_SaveMain = GameObject.Find("Constructor").GetComponent<SonySaveDataMain>();
#endif
        CurrentInputs.Init();

    }

    void OnEnable()
    {
#if UNITY_STANDALONE
      //  PlayerInputActionMap.Enable();
#endif
    }




    void Update()
        {
#if UNITY_STANDALONE

        CurrentInputs = new PCKeyboardInputs();
        joystick = false;

        MouseModeChange();
#endif

        CurrentInputs.Body();

        if (!CurrentInputs.PudState) return;

        enter_b = CurrentInputs.enter_b;
        enter_b_hold = CurrentInputs.enter_b_hold;
        inventory_b = CurrentInputs.inventory_b;

        _horizontal = CurrentInputs._horizontal;
        _vertical = CurrentInputs._vertical;
        _horizontal_R = CurrentInputs._horizontal_R;
        _vertical_R = CurrentInputs._vertical_R;

        exit_b = CurrentInputs.exit_b;
        delete_b = CurrentInputs.delete_b;

        menu_b = CurrentInputs.menu_b;
        LeftMouseButton = CurrentInputs.LeftMouseButton;
        LeftMouseButtonDown = CurrentInputs.LeftMouseButtonDown;
        RightMouseButtonDown = CurrentInputs.RightMouseButtonDown;
        RightMouseButton = CurrentInputs.RightMouseButton;
        space_b = CurrentInputs.space_b;

        BKey = CurrentInputs.BKey;
        OKey = CurrentInputs.OKey;

        MouseScroll = CurrentInputs.MouseScroll;

        DPADX = CurrentInputs.DPADX;
        DPADY = CurrentInputs.DPADY;

        _horizontal_DPAD_Push = CurrentInputs._horizontal_DPAD_Push;
        _vertical_DPAD_Push = CurrentInputs._vertical_DPAD_Push;
        RightTrigger = CurrentInputs.RightTrigger;
        LeftTrigger = CurrentInputs.LeftTrigger;

        ZLKey = CurrentInputs.ZLKey;
        R2 = CurrentInputs.R2;
        L2 = CurrentInputs.L2;

        FadeMode = CurrentInputs.FadeMode;
        _horizontalPush = CurrentInputs._horizontalPush;
        _verticalPush = CurrentInputs._verticalPush;
        shift = CurrentInputs.shift;

        MousePosition = Input.mousePosition;



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

  

    public void MouseModeChange()
    {
  
        if (LeftMouseButtonDown || RightMouseButtonDown)
        MouseMode = true;

        if (MousePosTimer < Time.fixedTime)
        {

            if (PrevMousePos != Input.mousePosition)
            {
                PrevMousePos = Input.mousePosition;
                MouseMode = true;

            }
            MousePosTimer = Time.fixedTime + 0.1f;

        }


        if (_vertical != 0 || _horizontal != 0 || _horizontal_R_Push || _vertical_R_Push || _horizontalPush || _verticalPush || enter_b || space_b || exit_b)
        {

        MouseMode = false;
        }

   

       

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

        R2 =  CurrentGamepad.IsR2Pressed;
        L2 = CurrentGamepad.IsL2Pressed;


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


   

}

