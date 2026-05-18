
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using System.IO;
using UnityEditor;

#if UNITY_STANDALONE
public class PS5Inputs : MonoBehaviour
{ 

}


#endif

#if UNITY_PS5 || UNITY_PS4
public class PS5Inputs :MonoBehaviour, IInputs
{


    public float GamepadVertTimer { get; set; }
    public float GamepadHorTimer { get; set; }
    private float GamepadRHorTimer, GamepadRVertTimer;


    public bool enter_b { get; set; }
    public bool enter_b_hold { get; set; }

    public bool inventory_b { get; set; }


    public float _horizontal { get; set; }

    public float _vertical { get; set; }


    public float _horizontal_R { get; set; }
    

    public float _vertical_R { get; set; }



    public bool exit_b { get; set; }
    public bool delete_b { get; set; }
    public bool menu_b { get; set; }
    public bool space_b { get; set; }


    public bool LeftMouseButton { get; set; }
    public bool RightMouseButton { get; set; }

    public bool LeftMouseButtonDown { get; set; }
    public bool RightMouseButtonDown { get; set; }

    public bool BKey { get; set; }

    public bool OKey { get; set; }
    public float DPADY { get; set; }
    


    public float DPADX { get; set; }
    public bool shift { get; set; }


    public float MouseScroll
    {
        /*get
        {
          if(GamePad.activeGamePad.currentFrame.GetThumbstickRight)
         return 1;
             else if (npadState.GetButtonDown(NpadButton.StickL))
                 return -1;
             else return 0;
         }  
        */
        get
        {
            return 0;
        }
    }



    public bool _horizontal_DPAD_Push
    { get; set; }
    public bool _vertical_DPAD_Push
    { get; set; }

    public bool RightTrigger { get; set; }
    public bool LeftTrigger { get; set; }
    public bool ZLKey { get; set; }
    public bool R2 { get; set; }
    public bool L2 { get; set; }

    public bool FadeMode => false;


    private Image JoyConImage, ProGamepadImage;

    private bool MidPadState;
    public bool PudState => MidPadState;
 
    public bool _horizontalPush
    {
        get 
        {
            if (_horizontal > 0.1f || _horizontal < -0.1f)
            {
                if (GamepadHorTimer < Time.fixedTime)
                {
                    GamepadHorTimer = Time.fixedTime + 0.01f;
                    return true;
            
                }
                else return false;
            }
            else return false;


        }

    }

    public bool _verticalPush
    {
        get
        {
            if (_vertical > 0.1f || _vertical < -0.1f)
            {
                if (GamepadVertTimer < Time.fixedTime)
                {
                    GamepadVertTimer = Time.fixedTime + 0.01f;
                    return true;

                }
                else return false;
            }
            else return false;


        }

    }

    public bool MouseMode => false;
      public bool joystick => true;
    bool UpdatePadState()
    {

        return true;
    }


    public void Body()
    {

        DrawGamepad();
        MidPadState = UpdatePadState();
        InputManager();



    }

    void InputManager()
    {
        if (User.users == null) return;
        if (User.users.Length <= 0) return;
        if (User.users[0] == null) return;

        if (GamePad.activeGamePad == null) return;

        enter_b = GamePad.activeGamePad.IsCrossPressed;
       enter_b_hold = GamePad.activeGamePad.currentFrame.cross;

    inventory_b = GamePad.activeGamePad.currentFrame.square;


   exit_b = GamePad.activeGamePad.IsCirclePressed;
    delete_b = GamePad.activeGamePad.IsCirclePressed;
    menu_b = GamePad.activeGamePad.IsOptionsPressed;
   space_b = GamePad.activeGamePad.IsCrossPressed;


  /* LeftMouseButton = Input.GetMouseButton(0);
   RightMouseButton = Input.GetMouseButton(1);

    LeftMouseButtonDown =Input.GetMouseButtonDown(0);
    RightMouseButtonDown = Input.GetMouseButtonDown(1);*/

    BKey = GamePad.activeGamePad.IsTrianglePressed;

     OKey = GamePad.activeGamePad.IsR2Pressed;


        _horizontal = GamePad.activeGamePad.GetThumbstickLeft.x;
        _vertical = GamePad.activeGamePad.GetThumbstickLeft.y*-1;

        _horizontal_R = GamePad.activeGamePad.GetThumbstickRight.x;
        _vertical_R = GamePad.activeGamePad.GetThumbstickRight.y*-1;


        RightTrigger = GamePad.activeGamePad.IsR1Pressed;
    LeftTrigger = GamePad.activeGamePad.IsL1Pressed;
    ZLKey = GamePad.activeGamePad.IsL2Pressed;
    R2 = GamePad.activeGamePad.IsR2Pressed;
    L2 = GamePad.activeGamePad.IsL2Pressed;
}
    public void DrawGamepad()
    {
       // if (JoyConImage.color.a > 0) JoyConImage.color = new Color(1, 1, 1, JoyConImage.color.a - 0.03f);
     //   if (ProGamepadImage.color.a > 0) ProGamepadImage.color = new Color(1, 1, 1, ProGamepadImage.color.a - 0.03f);

    }


    
  

   public  void Init()
    {

        

    }


   




}
#endif