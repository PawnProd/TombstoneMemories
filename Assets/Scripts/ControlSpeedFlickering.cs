using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpeedFlickering : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().speed = Random.Range(0.2f, 0.6f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
