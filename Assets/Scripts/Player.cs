using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 forward;
    Vector3 right;
    Vector3 left;
    Vector3 back;
    Vector3 upright;
    Vector3 upleft;
    GameObject theCamera;
    Animator animator;
    public float jumpForce = 100.0f;
    public float speed = 10.0f;
    public float rotSpeed = 0.15f;
    const float norm = 0.707f;
    bool onGround;
    bool falling;
    bool running;
    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        theCamera = GameObject.Find("Camera");
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
    void Update ()
    {
        SetVectors();
        Move();
        Jump();
        FallCheck();
        Quit();
    }

    public void SetVectors()
    {
        Vector3 pos;
        pos = theCamera.transform.TransformDirection(Vector3.forward);
        forward = new Vector3(pos.x, 0.0f, pos.z);
        pos = theCamera.transform.TransformDirection(Vector3.right);
        right = new Vector3(pos.x, 0.0f, pos.z);
        pos = theCamera.transform.TransformDirection(Vector3.back);
        back = new Vector3(pos.x, 0.0f, pos.z);
        pos = theCamera.transform.TransformDirection(Vector3.left);
        left = new Vector3(pos.x, 0.0f, pos.z);
        pos = theCamera.transform.TransformDirection(new Vector3(1, 0, 1));
        upright = new Vector3(pos.x, 0.0f, pos.z);
        pos = theCamera.transform.TransformDirection(new Vector3(-1, 0, 1));
        upleft = new Vector3(pos.x, 0.0f, pos.z);

    }

    public void Move()
    {
        //Move Up Right
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Up Left
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down Right
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft * -1), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down Left
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright * -1), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Up
        if (Input.GetKey(KeyCode.W))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down
        if (Input.GetKey(KeyCode.S))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(back), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(right), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            if (onGround && !running)
            {
                animator.SetBool("Run", true);
                animator.Play("Run", 0);
                running = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(left), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //If there is no movement - make sure to go back to idle
        if (onGround)
        {
            animator.SetBool("Run", false);
            animator.Play("Idle", 0);
            running = false;
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            animator.SetBool("Jump", true);
            animator.Play("Jump", 0);
            rb.AddForce(Vector3.up * jumpForce);
            onGround = false;
        }
    }

    public void FallCheck()
    {
        if (rb.velocity.y < -1)
        {
            onGround = false;
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);

            if (!falling)
            {
                animator.Play("Fall", 0);
                falling = true;
            }
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
            {
                onGround = true;
                falling = false;
                animator.SetBool("Fall", false);

                if (!running)
                    animator.Play("Idle", 0);
                else
                    animator.Play("Run", 0);
            }
        }

        if (collision.gameObject.tag == "KillPlane")
        {
            transform.position = Vector3.zero;
        }
    }
}
