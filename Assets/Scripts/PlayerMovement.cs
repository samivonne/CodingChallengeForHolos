using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    Rigidbody rb;
    public bool isGrounded;

    public float speed = 3.0f;
    public float jumpPower = 10.0f;
    public float rotationSpeed = 2f;
    //public float curY;
    //public Quaternion quaternion;
    public Vector3 euler;
    public Animator anim;
    private float maxRightrotation = 90;
    private float maxLeftrotation = 270;
    private float maxBack = 180;

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.D)) {
            float currentRotY = rb.transform.eulerAngles.y;
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x,maxRightrotation,rb.transform.eulerAngles.z);
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            float currentRotY = rb.transform.eulerAngles.y;
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, maxLeftrotation, rb.transform.eulerAngles.z);
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            float currentRotY = rb.transform.eulerAngles.y;
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, 0, rb.transform.eulerAngles.z);
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float currentRotY = rb.transform.eulerAngles.y;
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, maxBack, rb.transform.eulerAngles.z);
            
        }

    }

    void Update()
    {
        euler = rb.transform.eulerAngles;

        if (Input.GetKey(KeyCode.D)) {
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector3(1,0,0) * speed;
           
        }
        else if(Input.GetKeyUp(KeyCode.D)){
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.A)) {
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector3(-1, 0, 0) * speed;
        }  else if (Input.GetKeyUp(KeyCode.A)){
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.W)) {
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector3(0, 0, 1) * speed;
        } else if (Input.GetKeyUp(KeyCode.W)) {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.S)) {
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector3(0, 0, -1) * speed;
        } else if (Input.GetKeyUp(KeyCode.S)){
            anim.SetBool("isRunning", false);
        }
       

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
       // if (euler.y <= 90f || euler.y >= 270f)
        //{
            if (mouseX < 0)
            {

                rb.transform.Rotate(0, mouseX * rotationSpeed, 0, Space.World);
            }
            if (mouseX > 0)
            {
                //go right
                rb.transform.Rotate(0, mouseX * rotationSpeed, 0, Space.World);
            }
      //  }else if(euler.y<270f && euler.y>90f){
           // if(Mathf.Abs(euler.y-270f) < euler.y-90f){
            //    rb.transform.rotation = Quaternion.Euler(euler.x,271f,euler.z);
            //}
            //else {
            //    rb.transform.rotation = Quaternion.Euler(euler.x, 89f, euler.z);
            //}
       // }
        if(mouseY < 0) {
            //go down?
            rb.transform.Rotate(-mouseY*rotationSpeed, 0, 0,Space.Self);
        }
        if(mouseY > 0) {
            //go up?
            rb.transform.Rotate(-mouseY*rotationSpeed, 0, 0,Space.Self);
        }

    }
}

