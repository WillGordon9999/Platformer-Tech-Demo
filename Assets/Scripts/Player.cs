using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 100.0f;
    public float speed = 10.0f;
    bool onGround;
    Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        Jump();
        Quit();
    }

    public void Move()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, 0, z);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce);
            onGround = false;
        }
    }

    public void Quit()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (collision.gameObject.transform.position.y <= transform.position.y)
                   onGround = true;
        }

        if (collision.gameObject.tag == "KillPlane")
        {
            transform.position = Vector3.zero;
        }
    }
}
