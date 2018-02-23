using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{


    public int numberOfClouds = 40;     // # of clouds.
    public GameObject[] cloudPrefabs;  // The prefabs for the clouds.
    public Vector3 cloudPositionMin;   // Min position of each cloud.
    public Vector3 cloudPositionMax;   // Max position of each cloud.
    public float cloudScaleMin = 1;     // Min scale of each cloud.
    public float cloudScaleMax = 5;     // Max scale of each cloud.
    public float cloudSpeedMult = 0.5f; // Adjust speed of clouds.

    private GameObject[] cloudInstances;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Iterate over each cloud that was created.
	    foreach (GameObject cloud in cloudInstances)
	    {
	        // Get the cloud scale and position
	        float scaleVal = cloud.transform.localScale.x;
	        Vector3 cPos = cloud.transform.position;

            // Move larger clouds faster
	        cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            // If a cloud has moved to far to the left...
	        if (cPos.x <= cloudPositionMin.x)
	        {
	            // Move it to the far right.
	            cPos.x = cloudPositionMax.x;
	        }
            // Apply the new position to could.
	        cloud.transform.position = cPos;
        }
		
	}

    void Awake()
    {
        // Make an array large enough to hold all the Cloud_Instances...
        cloudInstances = new GameObject[numberOfClouds];

        // Find the CloudAnchor parent GameObject.
        GameObject anchor = GameObject.Find("CloudAnchor");

        // Iterate through and make Cloud(s).
        GameObject cloud;

        for (int i = 0; i < numberOfClouds; i++)
        {
            // Pick an int between 0 and cloudPrefabs.Length-1.
            // Random.Range will not ever pick as high as the top number.
            int prefabNumber = Random.Range(0, cloudPrefabs.Length);

            // Make an instance
            cloud = Instantiate(cloudPrefabs[prefabNumber]) as GameObject;

            // Position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPositionMin.x, cloudPositionMax.x);
            cPos.y = Random.Range(cloudPositionMin.y, cloudPositionMax.y);

            // Scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            // Smaller clouds (with smaller scaleU) should be nearer the ground.
            cPos.y = Mathf.Lerp(cloudPositionMin.y, cPos.y, scaleU);

            // Smaller clouds should be further away.
            cPos.z = 100 - 90 * scaleU;

            // Apply theres transforms to the cloud.
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            // Make cloud a child of the anchor
            cloud.transform.parent = anchor.transform;

            // Add the cloud to cloudInstances
            cloudInstances[i] = cloud;
      }

    }


}
