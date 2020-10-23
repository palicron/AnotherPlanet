using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctr : MonoBehaviour
{

    public float forceup;
    [SerializeField]
    private float StarSpeed;
    [SerializeField]
    private GameObject mesh;

    [SerializeField]
    private float MaxSpeed = 50.0f;
    [SerializeField, Range(0, 1)]
    private float BackMulti = 1.0f;

    public LayerMask thisaplanet;
    private float CurrentSpeed;
    public float MouseSensibility = 100f;
    Rigidbody rb;
    float DistanceToground;
    public GameObject Point;
    private FireSystem Fs;
    private PlayerResorSystem Hs;
    Vector3 GroundNormal;
    public float inclSpeed = 0.02f;
    public bool onGround = false;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Fs = GetComponent<FireSystem>();
        Hs = GetComponent<PlayerResorSystem>();
        CurrentSpeed = StarSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
        groundCheck();

    }

    private void FixedUpdate()
    {
        Inputs();
    }

    private void Inputs()
    {

        if (!canMove)
            return;
        float mousex = Input.GetAxis("Mouse X") * MouseSensibility * Time.deltaTime;
        this.transform.Rotate(Vector3.up * mousex);
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward.normalized * CurrentSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward.normalized * (CurrentSpeed * BackMulti));
        }
        if (Input.GetKey(KeyCode.A))
        {
            mesh.transform.localRotation = Quaternion.Slerp(mesh.transform.localRotation, Quaternion.Euler(new Vector3(-80, -90, 90)), inclSpeed * Time.time);
            rb.AddForce(-transform.right.normalized * CurrentSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            mesh.transform.localRotation = Quaternion.Slerp(mesh.transform.localRotation, Quaternion.Euler(new Vector3(-100, -90, 90)), inclSpeed * Time.time);


            rb.AddForce(transform.right.normalized * CurrentSpeed);
        }
        else
        {
            mesh.transform.localRotation = Quaternion.Slerp(mesh.transform.localRotation, Quaternion.Euler(new Vector3(-90, 0, 0)), inclSpeed * Time.time);

        }
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 f = transform.forward * 1.5f;
            f.y = 0;
            Fs.FireP(transform.position + f, transform.rotation, null);

        }

    }
    void groundCheck()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -transform.up, out hit, 10, thisaplanet))
        {

            DistanceToground = hit.distance;
            GroundNormal = hit.normal;

            if (DistanceToground <= 0.8f)
            {

                onGround = true;
            }
            else if (DistanceToground >= 1.5f)
            {

                onGround = false;
            }

        }



        this.transform.position = new Vector3(this.transform.position.x, hit.point.y + (1f), this.transform.position.z);



    }



}