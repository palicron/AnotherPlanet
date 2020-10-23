using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProyectile : Proyectil
{
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {


    }

    public override void init(GameObject player = null)
    {
        this.Player = player;
        coroutine = move();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

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
        Vector3 pp = Player.transform.position;
        transform.LookAt(pp);
        while (Vector3.Distance(this.transform.position, pp) >= 0.3f)
        {
            //snapToplanet();
            transform.position = Vector3.MoveTowards(this.transform.position, pp, Speed * Time.deltaTime);

            //transform.forward * Time.deltaTime * Speed;
            CurrentFlietime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        DestroidThis();
    }

    private void OnTriggerEnter(Collider other)
    {
        ResorseSystem rs = other.gameObject.GetComponent<ResorseSystem>();

        if (rs)
        {

            if (rs.tipo != owner)
            {

                rs.takeDmg(Dmg);
                DestroidThis();
            }

        }

    }
}
