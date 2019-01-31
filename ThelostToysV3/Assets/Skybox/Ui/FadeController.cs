using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {
    public string button_INPUT;
    public string Scene_Next;

    private void Update()
    {
        if(Input.GetButtonDown(button_INPUT))
        {
            this.GetComponent<fadeSrcipt>().fade_triger(Scene_Next);
        }
    }
}
