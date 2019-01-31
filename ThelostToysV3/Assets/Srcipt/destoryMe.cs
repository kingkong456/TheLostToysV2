using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryMe : MonoBehaviour {
	[SerializeField]
	private float TimeCountDown = 10;

	// Use this for initialization
	void Start ()
	{

        this.transform.position += Vector3.up * 1.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{

		//transform.Translate (new Vector3(0,10 * Time.deltaTime,0));
		TimeCountDown -= Time.deltaTime;

		if (TimeCountDown <= 0) {
			Destroy (this.gameObject);
		}
	}
}
