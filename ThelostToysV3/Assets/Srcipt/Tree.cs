using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public float tree_hp;

    public void tacking_dmg(float dmg)
    {
        tree_hp -= dmg;

        if(tree_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
