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
	void FixedUpdate ()
	{

	    Vector3 destination;

        // If there is no POI, return to P: 0, 0, 0.
        if (pointOfInterest == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Get the position of the pointOfInterest.
            destination = pointOfInterest.transform.position;

            // If POI is a projectile, check to see if its at rest.
            if (pointOfInterest.tag == "Projectile")
            {
                // If it is sleeping (not moving)
                if (pointOfInterest.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view.
                    pointOfInterest = null;

                    return;
                }
            }
        }
        
        

        // Limit the X & Y to minimum values.
	    destination.x = Mathf.Max(minXY.x, destination.x);
	    destination.y = Mathf.Max(minXY.y, destination.y);
	    this.GetComponent<Camera>().orthographicSize = destination.y + 10;

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
