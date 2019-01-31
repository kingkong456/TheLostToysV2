using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [Header("joy controller")]
    public int controller_number;
    public string leftStick_Horizontal;
    public string leftStick_Vertical;
    public string button1_input;
    public string button2_input;
    public string button3_input;
    public string button4_input;
    public string diraction_button_Horizontal;
    public string diraction_button_Vertical;
    public string triger_R1_L_button;
    public string triger_L1_L_button;
    public string triger_R2_L_button;
    public string triger_L2_L_button;

    private void Start()
    {
        setJoySrick();
    }

    private void setJoySrick()
    {
        leftStick_Horizontal = "j" + controller_number + "_leftStick_Horizontal";
        leftStick_Vertical = "j" + controller_number + "_leftStick_Vertical";
        button1_input = "j" + controller_number + "_button1_input";
        button2_input = "j" + controller_number + "_button2_input";
        button3_input = "j" + controller_number + "_button3_input";
        button4_input = "j" + controller_number + "_button4_input";
        diraction_button_Horizontal = "j" + controller_number + "_button_Horizontal_input";
        diraction_button_Vertical = "j" + controller_number + "_button_Vertical_input";
        triger_R1_L_button = "j" + controller_number + "_triger_R1_L_button";
        triger_L1_L_button = "j" + controller_number + "_triger_L1_L_button";
        triger_R2_L_button = "j" + controller_number + "_triger_R2_L_button";
        triger_L2_L_button = "j" + controller_number + "_triger_L2_L_button";
    }
}
