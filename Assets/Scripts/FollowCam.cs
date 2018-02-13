using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    static public FollowCam S; // a FollowCam Singleton

    public GameObject pointOfInterest;
    private float camZ;
    // V2 is 0,0 as default.
    private Vector2 minXY;

    private int MIN_SIZE_OF_CAMERA = 10;

    public float easing = 0.05f;

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// FixedUpdate is called every frame of the physics simulation (50fps).
	void FixedUpdate () {
        if (pointOfInterest == null) return; //Return if there is no pointOfInterest.
        
        // Get the position of the pointOfInterest.
	    Vector3 destination = pointOfInterest.transform.position;

        // Limit the X & Y to minimum values.
	    destination.x = Mathf.Max(minXY.x, destination.x);
	    destination.y = Mathf.Max(minXY.y, destination.y);

        // Interpolate from the current Camera position toward destination. 
        // - Lerp() interpolates between two points and returning a weighted average of the two. By setting easing = 0.05,
        //we are telling unity to move the camera about 5% of the way from its current location to the location of pointOfInterest every frame.
        destination = Vector3.Lerp(transform.position, destination, easing);

        // Retain a destination.z of camZ.
	    destination.z = camZ;

        // Set the camera to the destination.
	    transform.position = destination;

        // Set the orthographicSize of the Camera to keep Ground in view.
	    this.GetComponent<Camera>().orthographicSize = destination.y + MIN_SIZE_OF_CAMERA;
	}
}
