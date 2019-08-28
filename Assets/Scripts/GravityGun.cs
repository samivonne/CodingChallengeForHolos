using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Rigidbody rb;
   

    public Vector3 offset;
    public float maxGrab;
    public float minDistance;
    public float grabDistance;
    public Vector3 hitpoint;
    public float power = 100f;

    public LineRenderer line;
    public float oldMass;

    private RigidbodyInterpolation initInterp;

    public float test;
    private Ray MyRay()
    {
        return new Ray(this.transform.position, this.transform.forward);
    }

    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
        line.startWidth = .01f;
        line.endWidth = .01f;
        line.startColor = Color.blue;
        line.endColor = Color.blue;
        maxGrab = 20f;
        minDistance = 2f;
        power = 30;
    }
    // Update is called once per frame
    void Update()
    {

       // ray = MyRay();

        //Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red, 0.01f);



        if (rb)
        {

            //when mouse is not pressed or when you score

            if (rb.gameObject.tag == "Scored" || !Input.GetMouseButton(0))
            {
                rb.interpolation = initInterp;
                rb = null;
            }

            if (Input.GetKey(KeyCode.F))
            {
                rb.interpolation = initInterp;
                rb.gameObject.tag = "Shot";
               // Debug.Log(rb.transform.forward * (power * rb.mass));

                rb.AddForce(this.transform.forward * (power * rb.mass), ForceMode.Impulse);
                rb = null;
            }


        }
        else
        {
            Ray ray = MyRay();
            RaycastHit hit;
            if (Input.GetMouseButton(0))
            {

               //Physics.Raycast(ray, out hit);

                if (Physics.Raycast(ray, out hit, maxGrab))
                {
                    if (hit.rigidbody != null && hit.rigidbody.gameObject.name == "Boulder" && hit.rigidbody.gameObject.tag == "Untagged")
                    {
                        grabDistance = Vector3.Distance(ray.origin, hit.point);
                        if (grabDistance > minDistance)
                        {
                            rb = hit.rigidbody;
                            hitpoint = hit.transform.position;

                            initInterp = rb.interpolation;


                            offset = hit.transform.InverseTransformVector(hit.point - hit.transform.position);


                            //set interpolation for collision detection
                            rb.interpolation = RigidbodyInterpolation.Interpolate;
                           
                        }

                    }
                }
                //Physics.Raycast(this.transform.position, this.transform.forward, out hit);
            }
            line.SetPosition(0, ray.origin);
            line.SetPosition(1, ray.direction * 10000f);
           

        }
    }

    private void FixedUpdate()
    {
        if (rb)
        {

            Ray ray = MyRay();

            //local space to world space
            //destination point 
            Vector3 destPoint = ray.GetPoint(grabDistance);
            Vector3 centerDestination = destPoint - rb.transform.TransformVector(offset);


            Vector3 toDestination = centerDestination - rb.transform.position;


            //added force
            Vector3 force = toDestination / Time.fixedDeltaTime;

            //remove torque
            rb.angularVelocity = Vector3.zero;

            //remove velocity so it doesn't go flying
            rb.velocity = Vector3.zero;
            rb.AddForce(force, ForceMode.VelocityChange);

            line.SetPosition(0, ray.origin);
            line.SetPosition(1, destPoint);

        }

    }

}
