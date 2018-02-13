using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    static public FollowCam S; // a FollowCam Singleton

    public GameObject pointOfInterest;
    public float camZ;

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (pointOfInterest == null) return; //Return if there is no pointOfInterest.
        
        // Get the position of the pointOfInterest.
	    Vector3 destination = pointOfInterest.transform.position;

        // Retain a destination.z of camZ.
	    destination.z = camZ;

        // Set the camera to the destination.
	    transform.position = destination;
	}
}
