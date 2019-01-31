using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyInput : MonoBehaviour {
    private Controller controller;

    public bool isLocked = false;

    public bool isPress;

    public bool GetButtonDown_up = false;
    public bool GetButtonDown_down = false;
    public bool GetButtonDown_left = false;
    public bool GetButtonDown_right = false;


    private void Awake()
    {
        controller = GetComponent<Controller>();
    }

    private void FixedUpdate()
    {
        GetButtonDown_down = false;
        GetButtonDown_left = false;
        GetButtonDown_right = false;
        GetButtonDown_up = false;

        if(isLocked)
        {
            return;
        }

        if (!isPress && (is_get_down_input() || is_get_up_input() || is_get_left() || is_get_right()))
        {
            if (is_get_down_input())
            {
                GetButtonDown_down = true;
            }
            else if (is_get_up_input())
            {
                GetButtonDown_up = true;
            }
            else if (is_get_right())
            {
                GetButtonDown_right = true;
            }
            else if (is_get_left())
            {
                GetButtonDown_left = true;
            }

            isPress = true;
        }
        else if (isPress && !(is_get_down_input() || is_get_up_input() || is_get_right() || is_get_left()))
        {
            isPress = false;
        //  GetButtonDown_down = false;
        //  GetButtonDown_left = false;
        //  GetButtonDown_right = false;
        //  GetButtonDown_up = false;
        }
    }

    private bool is_get_down_input()
    {
        if (Input.GetAxis(controller.diraction_button_Vertical) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //joy stick is input up
    private bool is_get_up_input()
    {
        if (Input.GetAxis(controller.diraction_button_Vertical) < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool is_get_right()
    {
        if (Input.GetAxis(controller.diraction_button_Horizontal) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool is_get_left()
    {
        if (Input.GetAxis(controller.diraction_button_Horizontal) < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
