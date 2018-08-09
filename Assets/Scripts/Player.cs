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
    public float fallMass = 2.0f;
    const float norm = 0.707f;
    const float deadZone = 0.2f;
    bool onGround;
    bool falling;
    bool running;
    bool keyboard;
    bool gamepad;
    Rigidbody rb;
    Renderer rend;
    Color theColor;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        theCamera = GameObject.Find("Camera");
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetVectors();
        CheckInputMethod();
        FallCheck();
        JumpCheck();
        Move();
        ControllerMove();
        Jump();
        Quit();
    }

    private void SetVectors()
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

    private void CheckInputMethod()
    {
        if (Input.anyKey && !Input.GetButton("Jump"))
        {
            keyboard = true;
            gamepad = false;
            print("using keyboard");
            return;
        }

        if (Input.GetAxis("LeftStickHorizontal") != 0.0f || Input.GetAxis("LeftStickVertical") != 0.0f || Input.GetButtonDown("Jump"))
        {
            gamepad = true;
            keyboard = false;
            print("using gamepad");
            return;
        }
    }

    private void Move()
    {
        if (gamepad)
            return;

        //Move Up Right
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Up Left
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down Right
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft * -1), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down Left
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright * -1), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Forward
        if (Input.GetKey(KeyCode.W))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Back
        if (Input.GetKey(KeyCode.S))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(back), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(right), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            PlayRun();
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

    private void PlayRun()
    {
        if (onGround && !running)
        {
            animator.SetBool("Run", true);
            animator.Play("Run", 0);
            running = true;
        }
    }

    public bool GetRun()
    {
        return running;
    }

    private void ControllerMove()
    {
        if (keyboard)
            return;

        float xDir = Input.GetAxis("LeftStickHorizontal");
        float yDir = Input.GetAxis("LeftStickVertical");

        //Move Forward Right
        if (yDir > 0.0f && xDir > 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Forward Left
        if (yDir > 0.0f && xDir < 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Back Right
        if (yDir < 0.0f && xDir > 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft * -1), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Back Left
        if (yDir < 0.0f && xDir < 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright * -1), rotSpeed);
            transform.Translate(0, 0, speed * norm * Time.deltaTime, Space.Self);
            return;
        }
        //Move Forward
        if (yDir > 0.0f && xDir == 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Back
        if (yDir < 0.0f && xDir == 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(back), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Right
        if (xDir > 0.0f && yDir == 0.0f)
        {
            PlayRun();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(right), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Left
        if (xDir < 0.0f && yDir == 0.0f)
        {
            PlayRun();
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

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && onGround)
        {
            animator.SetBool("Jump", true);
            animator.Play("Jump", 0);
            rb.AddForce(Vector3.up * jumpForce);
            onGround = false;
        }
    }

    private void JumpCheck() // Help make the game less floaty by applying more gravity or mass when spacebar or a button is lifted
    {
        if (rb.velocity.y > 0 && !onGround)
        {
            if (keyboard)
            {
                if (!Input.GetKey(KeyCode.Space))
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallMass, rb.velocity.z);
            }

            else
            {
                if (!Input.GetKey("joystick button 0"))
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallMass, rb.velocity.z);
            }
        }
    }


    private void FallCheck()
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

        if (rb.velocity.y == 0)
        {
            RaycastHit hit;
            float maxDist = 1.0f;
            Physics.Raycast(transform.position, Vector3.down, out hit, maxDist);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Floor")
                {
                    onGround = true;
                    falling = false;
                    animator.SetBool("Fall", false);

                    if (!running)
                        animator.Play("Idle", 0);
                    else
                        animator.Play("Run", 0);
                }

                if (hit.collider.tag == "Player")
                    print("Hit player move pos");
            }
        }
    }

    private void Quit()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private bool IsGamepadConnected()
    {
        string[] pads = Input.GetJoystickNames();
        
        if (pads.Length == 0)
            return false;
        else
            return true;
    }

    private void ColorChange(Transform trans)
    {
        if (trans.GetComponent<Renderer>() != null)
        {
            rend = trans.GetComponent<Renderer>();
        }

        rend.material.color = theColor;

        foreach (Transform child in trans)
        {
            ColorChange(child);
        }
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Floor")
        {
            
            if (collision.gameObject.transform.position.y < transform.position.y)
            {
                theColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                ColorChange(transform);
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
