using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    private GameObject launchPoint;

	// Use this for initialization
	void Start ()
	{
	    Rigidbody rb = GetComponent<Rigidbody>();
	    rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        print("Slingshot: OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        print("Slingshot: OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void Awake()
    {
        Transform launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
    }



}
