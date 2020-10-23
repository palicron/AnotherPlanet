using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCtrl : MonoBehaviour
{


    public GameObject PP;
    public GravityOrbit Gravity;
    private Rigidbody rb;
    private Vector3 gravityUp;
    private Vector3 localUp;
    private Quaternion TargetRot;
    public float RotationSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame



    void Update()
    {
        transform.position = Vector3.Lerp(PP.transform.position, transform.position, 0.1f);
        gravityUp = (transform.position - Gravity.transform.position).normalized;
        TargetRot = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, 0.1f);
    }
    void FixedUpdate()
    {
        // if (Gravity)
        //{
        //  gravityUp = Vector3.zero;
        // gravityUp = (transform.position - Gravity.transform.position).normalized;
        // localUp = transform.up;
        //TargetRot = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        //transform.up = Vector3.Lerp(transform.up, gravityUp, RotationSpeed * Time.deltaTime);

        //rb.AddForce((-gravityUp * Gravity.gravity) * rb.mass);
        //}

    }

}
