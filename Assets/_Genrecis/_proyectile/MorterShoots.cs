using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorterShoots : Proyectil
{
    public float flytime;
    public GameObject marker;
    public float ExplotionRange;
    private GameObject markerGO;

    public LayerMask Todmg;

    private float adjtsFly;

    public GameObject explotion;
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void init(GameObject player = null)
    {
        this.Player = player;
        rb = GetComponent<Rigidbody>();
        adjtsFly = flytime;
    }
    public override void Fire()
    {
        Vector3 Vo = CalculateVel();

        this.transform.rotation = Quaternion.LookRotation(Vo);
        rb.velocity = Vo;
    }
    public override void MakeDmg(GameObject traget)
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    Vector3 CalculateVel()
    {
        adjtsFly = flytime + Mathf.Abs(Mathf.Clamp((Vector3.Distance(this.transform.position, Player.transform.position) / 40), 0, 1f));

        Vector3 Rand = Player.transform.position + (Player.GetComponent<Rigidbody>().velocity.normalized * Random.Range(2.5f, 7f));
        Vector3 distance = Rand - this.transform.position;
        Rand.y -= 0.2f;
        markerGO = Instantiate(marker, Rand, Quaternion.identity);
        Vector3 DistanceXZ = distance;
        DistanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = DistanceXZ.magnitude;

        float Vxz = Sxz / adjtsFly;
        float Vy = Sy / adjtsFly + 0.5f * Mathf.Abs(Physics.gravity.y) * adjtsFly;

        Vector3 Result = DistanceXZ.normalized;
        Result *= Vxz;
        Result.y = Vy;

        return Result;
    }
    private void OnTriggerEnter(Collider other)
    {


        if (!other.gameObject.tag.Equals("enemy"))
        {
            Instantiate(explotion, this.transform.position, Quaternion.identity);
            Destroy(markerGO);
            Explote();
            DestroidThis();
        }



    }
    private void Explote()
    {

        Collider[] hit = Physics.OverlapSphere(transform.position, ExplotionRange, Todmg);



        foreach (var hitCollider in hit)
        {


            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();


            if (rs)
            {

                if (rs.tipo != owner)
                {

                    rs.takeDmg(Dmg);

                }

            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplotionRange);
    }
}
