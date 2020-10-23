using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : Proyectil
{
    // Start is called before the first frame update

    public float AcelerationRange = 0;
    public float TurnRate = 1;
    public float ExplotionRange = 1;

    [Range(1, 3)]
    public float speedMuult;

    private IEnumerator coroutine;
    public LayerMask Todmg;
    private Vector3 lookpos;
    private Quaternion lookRot;

    public GameObject explotion;

    private bool IsnotAcce = false;


    public override void init(GameObject player = null)
    {
        this.Player = player;
        coroutine = move();
        rb = GetComponent<Rigidbody>();
    }


    public override void Fire()
    {

        StartCoroutine(coroutine);
    }

    public override void MakeDmg(GameObject traget)
    {

    }
    IEnumerator move()
    {
        while (CurrentFlietime < MaxflyTime)
        {
            if (Vector3.Distance(Player.transform.position, this.transform.position) <= AcelerationRange && !IsnotAcce)
            {
                Speed *= speedMuult;
                IsnotAcce = true;
            }

            if (!IsnotAcce)
            {
                transform.LookAt(Player.transform.position, Vector3.up);
            }

            CurrentFlietime += Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * Speed;

            yield return new WaitForEndOfFrame();
        }

        DestroidThis();
    }

    private void OnTriggerEnter(Collider other)
    {


        if (!other.gameObject.tag.Equals("planeta") && !other.gameObject.tag.Equals("enemy"))
        {
            Explote();
            DestroidThis();
        }



    }
    private void Explote()
    {
        Instantiate(explotion, this.transform.position, Quaternion.identity);
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
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AcelerationRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplotionRange);
    }
}

