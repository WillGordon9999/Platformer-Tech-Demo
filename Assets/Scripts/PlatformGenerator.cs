using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    List<List<List<List<int>>>> bigChungus;

    //The X Ranges for a platform to spawn from the previous platform
    public float minXRange = -5.0f;
    public float maxXRange = 5.0f;

    //The Y Ranges for a platform to spawn from the previous platform
    public float minYRange = 0.0f;
    public float maxYRange = 0.0f;

    //The Z Ranges for a platform to spawn from the previous platform
    public float minZRange = 5.0f;
    public float maxZRange = 10.0f;

    public GameObject platformPrefab;
    public uint maxPlatformCount = 100;
    uint currentPlatformCount;

    GameObject currentPlatform;
    List<GameObject> platformList;


    // Start is called before the first frame update
    void Start()
    {
        bigChungus = new List<List<List<List<int>>>>();
        currentPlatformCount = 0;
        platformList = new List<GameObject>();

        // Get the starting platform to build a level from
        currentPlatform = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       while (currentPlatformCount < maxPlatformCount)
       {
            float x = Random.Range(minXRange, maxXRange);
            float y = Random.Range(minYRange, maxYRange);
            float z = Random.Range(minZRange, maxZRange);

            //Vector3 pos = currentPlatform.transform.TransformVector(new Vector3(x, y, z));
            Vector3 pos = currentPlatform.transform.TransformPoint(new Vector3(x, y, z));

            currentPlatform = GameObject.Instantiate(platformPrefab, pos, Quaternion.identity);
            
            currentPlatformCount++;
       }
    }
}
