using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriger : MonoBehaviour {

    public string triger_name;

    void reset_triger()
    {
        this.GetComponent<Animator>().ResetTrigger(triger_name);
    }
	
}
