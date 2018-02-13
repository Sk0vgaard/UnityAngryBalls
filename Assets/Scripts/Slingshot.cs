using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    private GameObject launchPoint;
    private Vector3 launchPosition;
    private GameObject projectile;
    private bool aimingMode;


    public GameObject prefabProjectile;
    public float velocityMult = 4f;



	// Use this for initialization
	void Start ()
	{
	    Rigidbody rb = GetComponent<Rigidbody>();
	    rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        // If Slingshot is not in amingMode, don't run this code.
        if (!aimingMode) return;

        // Get the current mouse position in 2D screen coordinates.
        Vector3 mousePosition2D = Input.mousePosition;

        // Convert the mouse position to 3D world Coordinates.
        mousePosition2D.z = -Camera.main.transform.position.z;
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);

        // Find the delta from the launchPosition to rhe mousePosition3D.
        Vector3 mouseDelta = mousePosition3D - launchPosition;

        // Limit mouseDelta to the radius of the Slingshot SphereCollider.
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move the projectile to this new position.
        Vector3 projPosition = launchPosition + mouseDelta;
        projectile.transform.position = projPosition;

        if (Input.GetMouseButtonUp(0))
        // The mouse has been released.
        {
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.pointOfInterest = projectile;
            projectile = null;
        }
    }

    void OnMouseEnter()
    {
        //print("Slingshot: OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Slingshot: OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        // The player has pressed the mouse button while over Slingshot.
        aimingMode = true;
        // Instantiate a Projectile.
        projectile = Instantiate(prefabProjectile) as GameObject;
        // Start it at the launchPoint.
        projectile.transform.position = launchPosition;
        // Set it to isKinematic for now.
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Awake()
    {
        Transform launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
        launchPosition = launchPointTransform.position;
    }



}
