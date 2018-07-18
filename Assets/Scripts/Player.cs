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
    GameObject dir;
    GameObject theCamera;
    public float jumpForce = 100.0f;
    public float speed = 10.0f;
    public float rotSpeed = 0.15f;
    bool onGround;
    Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        dir = GameObject.Find("Direction Control");
        theCamera = GameObject.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetVectors();
        Move();
        Jump();
        Quit();
    }

    public void SetVectors()
    {
        //if (dir.transform.forward.x != theCamera.transform.forward.x || dir.transform.forward.z != theCamera.transform.forward.z)
        //{
        //    print("Inside failsafe");
        //    Vector3 pos = new Vector3(theCamera.transform.position.x, 0, theCamera.transform.position.z);
        //    //Set Vectors according to the camera if it fails, or according to this position somehow      
        //}
        //else
        //{
        //     Vector3 pos = new Vector3(theCamera.transform.position.x, 0, theCamera.transform.position.z);
        //     dir.transform.position = pos;
        //     forward = dir.transform.TransformDirection(Vector3.forward);
        //     right = dir.transform.TransformDirection(Vector3.right);
        //     left = dir.transform.TransformDirection(Vector3.left);
        //     back = dir.transform.TransformDirection(Vector3.back);
        //     upright = dir.transform.TransformDirection(new Vector3(1, 0, 1));
        //     upleft = dir.transform.TransformDirection(new Vector3(-1, 0, 1));
        //}

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
        print("dir forward " + dir.transform.forward.ToString() +  "Camera forward " + theCamera.transform.forward.ToString());

        Vector3 moveDirection = (dir.transform.forward * Input.GetAxis("Vertical")) + (dir.transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * speed;
       
        //Move Up Right
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright), rotSpeed);
            transform.Translate(0, 0, speed * 0.707f * Time.deltaTime, Space.Self);
            return;
        }
        //Move Up Left
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft), rotSpeed);
            transform.Translate(0, 0, speed * 0.707f * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down Right
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upleft * -1), rotSpeed);
            transform.Translate(0, 0, speed * 0.707f * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down Left
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(upright * -1), rotSpeed);
            transform.Translate(0, 0, speed * 0.707f * Time.deltaTime, Space.Self);
            return;
        }

        //Move Up
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Down
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(back), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(right), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(left), rotSpeed);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            return;
        }
        

        //if (Input.GetAxis("Vertical") != 0.0f)
        //{
        //    transform.rotation = Quaternion.LookRotation(forward);
        //    transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime, Space.Self);
        //}
        //
        //if (Input.GetAxis("Horizontal") != 0.0f && Input.GetKey(KeyCode.D))
        //{
        //    transform.rotation = Quaternion.LookRotation(right);
        //    transform.Translate(0, 0, Input.GetAxis("Horizontal") * speed * Time.deltaTime, Space.Self);
        //}
        //
        //if (Input.GetAxis("Horizontal") != 0.0f && Input.GetKey(KeyCode.A))
        //{
        //    transform.rotation = Quaternion.LookRotation(left);
        //    transform.Translate(0, 0, -1 * Input.GetAxis("Horizontal") * speed * Time.deltaTime, Space.Self);
        //}
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
