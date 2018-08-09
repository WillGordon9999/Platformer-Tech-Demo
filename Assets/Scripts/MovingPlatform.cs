using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public float stopTime = 2.0f; //In seconds
    float timeStopped = 0.0f;
    bool stop;
    bool moveToDestination;
    Vector3 dest;
    Vector3 origin;
    
    // Use this for initialization
    void Start ()
    {
        stop = false;
        origin = transform.position;
        dest = transform.Find("Target").position;
        moveToDestination = true;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (stop)
        {
            if (Time.time - timeStopped >= stopTime)
            {
                stop = false;
            }
        }

        else
        {
            if (moveToDestination)
            {
                if (transform.position == dest)
                    TargetSwitch();

                transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed);
            }

            else
            {
                if (transform.position == origin)
                    TargetSwitch();

                transform.position = Vector3.MoveTowards(transform.position, origin, moveSpeed);
            }
        }
    }

    void TargetSwitch()
    {
        if (moveToDestination)
        {
            stop = true;
            timeStopped = Time.time;
            moveToDestination = false;
        }
    
        else
        {
            stop = true;
            timeStopped = Time.time;
            moveToDestination = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
