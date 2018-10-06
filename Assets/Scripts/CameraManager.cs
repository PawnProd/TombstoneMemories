using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform target;
    public float smoothTime;

    private Vector3 m_velocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 destination = target.position;
        destination.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref m_velocity, smoothTime);
	}
}
