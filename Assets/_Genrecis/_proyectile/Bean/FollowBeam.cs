using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBeam : Proyectil
{
    // Start is called before the first frame update

    public GameObject Laserpre;
    public GameObject target;
    public LineRenderer laser;

    public float timeb = 1.2f;
    public float ExplotionRange;

    public float fireTime = 10.0f;

    private float passtime = 0;

    private float dd = 0;

    public LayerMask Todmg;

    private bool firing = false;
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
        StartCoroutine(move());
    }
    IEnumerator move()
    {
        Laserpre.SetActive(true);


        Vector3 pos = Player.transform.position + Player.transform.forward.normalized * Random.Range(12f, 17f);
        pos.y = 0.8f;
        target.transform.position = pos;

        while (passtime <= fireTime)
        {
            pos = Player.transform.position;
            pos.y = 0.3f;
            target.transform.position = Vector3.MoveTowards(target.transform.position, pos, Speed * Time.deltaTime);
            laser.SetPosition(1, target.transform.localPosition);
            passtime += Time.deltaTime;
            dd += Time.deltaTime;
            MakeDmg(null);
            yield return new WaitForEndOfFrame();
            firing = false;

        }
        passtime = 0;

        Laserpre.SetActive(false);
    }

    public override void MakeDmg(GameObject traget)
    {
        if (dd <= timeb)
        {
            return;
        }
        Collider[] hit = Physics.OverlapSphere(target.transform.position, ExplotionRange, Todmg);

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
        dd = 0;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.transform.position, ExplotionRange);
    }
    private void OnEnable()
    {
        firing = false;
    }
}

