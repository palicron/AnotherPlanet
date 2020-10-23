using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicoleLaser : Proyectil
{

    public GameObject[] Laserpre;
    public GameObject[] target;
    public LineRenderer[] laser;

    public float timeb = 1.4f;
    public float ExplotionRange;

    public float fireTime = 10.0f;

    private float passtime = 0;

    private float dd = 34;

    public LayerMask Todmg;


    public bool firing = false;
    void Start()
    {

    }
    public override void init(GameObject player = null)
    {
        this.Player = player;


    }

    public override void Fire()
    {
        if (firing)
            return;
        firing = true;
        target[0].SetActive(true);
        target[1].SetActive(true);
        target[2].SetActive(true);
        target[3].SetActive(true);
        Vector3 p = Player.transform.position;
        p.y = 0.7f;
        float a = Random.Range(-2f, 3f);
        target[0].transform.position = p + Player.transform.forward.normalized * (12f + a);

        target[1].transform.position = p + -Player.transform.forward.normalized * (12f + a);

        target[2].transform.position = p + Player.transform.right.normalized * (12f + a);

        target[3].transform.position = p + -Player.transform.right.normalized * (12f + a);

        StartCoroutine(move());

    }
    IEnumerator move()
    {
        Vector3 fpos = Player.transform.position;

        Laserpre[0].SetActive(true);
        Laserpre[1].SetActive(true);
        Laserpre[2].SetActive(true);
        Laserpre[3].SetActive(true);
        bool b1 = false;



        while (!b1)
        {
            target[0].transform.position = Vector3.MoveTowards(target[0].transform.position, fpos, Speed * Time.deltaTime);
            target[1].transform.position = Vector3.MoveTowards(target[1].transform.position, fpos, Speed * Time.deltaTime);
            target[2].transform.position = Vector3.MoveTowards(target[2].transform.position, fpos, Speed * Time.deltaTime);
            target[3].transform.position = Vector3.MoveTowards(target[3].transform.position, fpos, Speed * Time.deltaTime);

            laser[0].SetPosition(1, target[0].transform.position - this.transform.position);
            laser[1].SetPosition(1, target[1].transform.position - this.transform.position);
            laser[2].SetPosition(1, target[2].transform.position - this.transform.position);
            laser[3].SetPosition(1, target[3].transform.position - this.transform.position);

            if (Vector3.Distance(target[0].transform.position, fpos) <= 0.2f)
            {
                b1 = true;
            }
            MakeDmg(null);

            yield return new WaitForEndOfFrame();

        }
        passtime = 0;
        Laserpre[0].SetActive(false);
        Laserpre[1].SetActive(false);
        Laserpre[2].SetActive(false);
        Laserpre[3].SetActive(false);
        target[0].SetActive(false);
        target[1].SetActive(false);
        target[2].SetActive(false);
        target[3].SetActive(false);
        firing = false;
        // Laserpre.SetActive(target);
    }

    public override void MakeDmg(GameObject traget)
    {

        if (Time.time - dd <= timeb)
        {
            return;
            Debug.Log("acasc0");

        }


        Collider[] hit1 = Physics.OverlapSphere(target[0].transform.position, ExplotionRange, Todmg);
        Collider[] hit2 = Physics.OverlapSphere(target[1].transform.position, ExplotionRange, Todmg);
        Collider[] hit3 = Physics.OverlapSphere(target[2].transform.position, ExplotionRange, Todmg);
        Collider[] hit4 = Physics.OverlapSphere(target[3].transform.position, ExplotionRange, Todmg);


        foreach (var hitCollider in hit1)
        {
            Debug.Log("ac11");
            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();
            if (rs)
            {

                if (rs.tipo != owner)
                {
                    dd = Time.time;
                    rs.takeDmg(Dmg);

                }

            }
        }
        foreach (var hitCollider in hit2)
        {
            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();
            if (rs)
            {
                Debug.Log("ac12");
                if (rs.tipo != owner)
                {
                    dd = Time.time;
                    rs.takeDmg(Dmg);

                }

            }
        }
        foreach (var hitCollider in hit3)
        {
            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();
            if (rs)
            {

                if (rs.tipo != owner)
                {
                    dd = Time.time;
                    rs.takeDmg(Dmg);

                }

            }
        }
        foreach (var hitCollider in hit4)
        {
            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();
            if (rs)
            {

                if (rs.tipo != owner)
                {
                    dd = Time.time;
                    rs.takeDmg(Dmg);

                }

            }
        }

    }

    private void OnEnable()
    {
        firing = false;
    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target[0].transform.position, ExplotionRange);
        Gizmos.DrawWireSphere(target[1].transform.position, ExplotionRange);
        Gizmos.DrawWireSphere(target[2].transform.position, ExplotionRange);
        Gizmos.DrawWireSphere(target[3].transform.position, ExplotionRange);
    }
}
