using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingPlatform : MonoBehaviour
{
    public float scaleRate = 0.5f;
    public float stopTime = 2.0f;
    public Vector3 newScale;
    float timeStopped;
    Vector3 baseScale;
    bool scaleToNew;
    bool stop;

    // Use this for initialization
    void Start ()
    {
        scaleToNew = true;
        stop = false;
        baseScale = transform.localScale;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (stop)
        {
            if (Time.time - timeStopped >= stopTime)
                stop = false;
        }

        else
        {
            if (scaleToNew)
            {
                if (transform.localScale == newScale)
                    TargetSwitch();

                transform.localScale = Vector3.MoveTowards(transform.localScale, newScale, scaleRate);
            }

            else
            {
                if (transform.localScale == baseScale)
                    TargetSwitch();

                transform.localScale = Vector3.MoveTowards(transform.localScale, baseScale, scaleRate);
            }
        }
    }

    void TargetSwitch()
    {
        if (scaleToNew)
        {
            timeStopped = Time.time;
            stop = true;
            scaleToNew = false;
        }

        else
        {
            timeStopped = Time.time;
            stop = true;
            scaleToNew = true;
        }
    }
}
