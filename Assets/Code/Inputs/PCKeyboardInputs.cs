
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class PCKeyboardInputs :  IInputs
{

    public bool enter_b => Input.GetButtonDown("Enter");
    public bool enter_b_hold => Input.GetButton("Enter");
    public bool inventory_b => Input.GetButtonDown("Inventory");


    public float _horizontal => Input.GetAxis("Horizontal");
    public float _vertical => Input.GetAxis("Vertical") * -1;

    public bool exit_b => Input.GetButtonDown("Exit");
    public bool delete_b  =>  Input.GetButtonDown("Delete");
    public bool menu_b => Input.GetButtonDown("Menu");
    public bool space_b => Input.GetButtonDown("Space");


    public bool LeftMouseButtonDown  =>  Input.GetMouseButtonDown(0);
    public bool RightMouseButtonDown  =>  Input.GetMouseButtonDown(1);

      public float MouseScroll  =>  Input.GetAxis("Mouse ScrollWheel");
   

    public bool LeftMouseButton => Input.GetMouseButton(0);


    public bool RightMouseButton => Input.GetMouseButton(1);

    public bool SideButton => Input.GetButtonDown("SideButton");

    public bool LeftTrigger => Input.GetButtonDown("LeftTrigger");
    public bool RightTrigger => Input.GetButtonDown("RightTrigger");

    public bool R2 => Input.GetKeyDown(KeyCode.T);
    public bool L2 => Input.GetKeyDown(KeyCode.R);

    public bool ZLKey => Input.GetKeyDown(KeyCode.M);

    public bool FadeMode => Input.GetKeyDown(KeyCode.N);

    public bool QuestBook => Input.GetKeyDown(KeyCode.Q);



    public float _horizontal_R => Input.GetAxis("Horizontal_R");
    public float _vertical_R => Input.GetAxis("Vertical_R");
 
    public bool _verticalPush => Input.GetButtonDown("Vertical");

    public bool _horizontalPush => Input.GetButtonDown("Horizontal");


    public bool _vertical_R_Push => Input.GetButtonDown("Vertical_R");
    public bool _horizontal_R_Push => Input.GetButtonDown("Horizontal_R");


    public float VerticalArrows => Input.GetAxis("VerticalArrows");


    public bool shift { get; set; }

    public bool HorizontalFlip => Input.GetButtonDown("HorizontalFlip");
    public bool Rightstickpush => Input.GetButtonDown("RightStickPush");

    public bool BKey => Input.GetButtonDown("BKey");
    public bool OKey => Input.GetButtonDown("OKey");

    public float DPADY => 0;

    public float DPADX => 0;

    public bool _horizontal_DPAD_Push => false;

    public bool _vertical_DPAD_Push => false;
 
    public bool joystick {
        get
        {
            return false;
        }
        set { joystick = joystick; }
        }


    
    public bool PudState 
    {
        get
        {
            return true;
        }
    }

    public void Init()
    { 
    
    }
    public void Body()
    {
        shift = Input.GetKey(KeyCode.LeftShift);
    }


}