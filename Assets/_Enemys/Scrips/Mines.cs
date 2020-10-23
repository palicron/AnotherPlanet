using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : Enemy
{

    private Vector3 starpos;
    public int dmg = 3;
    public Light EmerLight;
    private int mul = -1;

    public float linginte = 1;

    public LayerMask thisaplanet;
    public LayerMask Todmg;
    float flytime = 0;
    public Tipe owner;
    private float dis = 0;
    private bool explote = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        starpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (EmerLight.intensity >= 1 || EmerLight.intensity <= 0 && !explote)
        {
            mul = mul * -1;
            EmerLight.intensity = Mathf.Clamp01(EmerLight.intensity);
        }
        EmerLight.intensity += linginte * Time.deltaTime * mul;

        dis = Vector3.Distance(player.transform.position, this.transform.position);

        if (dis <= NoticeRange)
        {
            linginte = 5.0f;
            if (dis <= FireRange)
            {
                explote = true;
                EmerLight.intensity = 6;
                Invoke("fireatPlayer", 1.5f);
            }
        }
        else if (!explote)
        {
            linginte = 1;
        }


    }



    protected override void fireatPlayer()
    {

        StartCoroutine(jump());
    }


    protected override void move()
    {

    }
    public override void takeDmg(int dmg)
    {

    }

    IEnumerator jump()
    {

        RaycastHit hit = new RaycastHit();
        while (hit.distance <= 1.5f)
        {

            Physics.Raycast(transform.position, -transform.up, out hit, 10, thisaplanet);
            // this.transform.position = new Vector3(this.transform.position.x, hit.point.y + (0.75f), this.transform.position.z);
            rb.AddForce(transform.up.normalized * 2, ForceMode.VelocityChange);
            // transform.position += transform.up.normalized * Time.deltaTime * 3;
            yield return new WaitForEndOfFrame();

        }
        rb.velocity = new Vector3(0, 1, 0);
        yield return new WaitForSeconds(0.2f);
        Explote();

    }

    private void Explote()
    {

        Collider[] hit = Physics.OverlapSphere(starpos, NoticeRange, Todmg);



        foreach (var hitCollider in hit)
        {


            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();


            if (rs)
            {

                if (rs.tipo != owner)
                {

                    rs.takeDmg(dmg);
                    Destroy(this.gameObject, 0.01f);

                }

            }
        }

    }

}
