using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlleeee : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 m_moce = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(m_moce, Space.World);
	}
}
