using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuensiaBeam : Proyectil
{
    // Start is called before the first frame update
    public GameObject decal;

    public GameObject pos1;
    public GameObject pos2;

    Vector3 Starpos;
    Vector3 endpos;
    public int Numoflasers = 3;
    public float alturadecal = 0.9f;
    private int index = 0;
    public GameObject Laserpre;
    public GameObject targetlaser;
    public LineRenderer laser;
    public float timeb = 1.2f;
    private bool firing = false;
    private float dd = 0;

    public float ExplotionRange = 2f;

    public bool candamg = true;
    public LayerMask Todmg;
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

        index = 0;
        firing = true;
        setfirtpath();
        StartCoroutine(move());
    }

    public void setfirtpath()
    {
        decal.SetActive(true);
        Vector3 lazerdir = Player.transform.right;
        Vector3 pos = Player.transform.position;
        pos.y = alturadecal;
        decal.transform.position = pos;
        decal.transform.right = lazerdir;

        pos1.transform.position = pos + (lazerdir.normalized * 25f);
        pos2.transform.position = pos + (lazerdir.normalized * -25f);

    }
    void setotherpath()
    {
        pos1.transform.position = pos2.transform.position;
        Vector3 pos = Player.transform.position;
        pos.y = alturadecal;
        Vector3 lazerdir = pos1.transform.position - pos;
        decal.transform.position = pos;
        decal.transform.right = lazerdir;
        pos2.transform.position = pos + (lazerdir.normalized * -40f);
        decal.SetActive(true);
    }
    void firelaser()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator move()
    {
        targetlaser.SetActive(true);
        Laserpre.SetActive(true);

        laser.SetPosition(0, this.transform.position);
        targetlaser.transform.position = pos1.transform.position;

        laser.SetPosition(1, targetlaser.transform.position - this.transform.position);
        yield return new WaitForSeconds(Random.Range(1.2f, 2.2f));
        decal.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        while (Vector3.Distance(targetlaser.transform.position, pos2.transform.position) >= 0.2f)
        {
            targetlaser.transform.position = Vector3.MoveTowards(targetlaser.transform.position, pos2.transform.position, Speed * Time.deltaTime);
            laser.SetPosition(1, targetlaser.transform.position - this.transform.position);

            MakeDmg(null);
            yield return new WaitForEndOfFrame();

        }

        targetlaser.SetActive(false);
        Laserpre.SetActive(false);
        if (index < Numoflasers)
        {
            yield return new WaitForSeconds(0.6f);
            index++;
            morelasers();
        }
        firing = false;
    }
    public override void MakeDmg(GameObject traget)
    {
        if (!candamg)
        {
            return;

        }
        Collider[] hit = Physics.OverlapSphere(targetlaser.transform.position, ExplotionRange, Todmg);

        foreach (var hitCollider in hit)
        {
            ResorseSystem rs = hitCollider.gameObject.GetComponent<ResorseSystem>();
            if (rs)
            {

                if (rs.tipo != owner)
                {
                    candamg = false;
                    rs.takeDmg(Dmg);

                }

            }
        }
        dd = 0;
    }
    void morelasers()
    {
        candamg = true;
        setotherpath();
        StartCoroutine(move());
    }
    private void OnEnable()
    {
        candamg = true;
    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetlaser.transform.position, ExplotionRange);
    }
}
