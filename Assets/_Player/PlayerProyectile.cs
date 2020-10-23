using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProyectile : Proyectil
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

    public override void Fire()
    {
        StartCoroutine(coroutine);

    }

    IEnumerator move()
    {
        while (CurrentFlietime < MaxflyTime)
        {
            // snapToplanet();
            transform.position += transform.forward.normalized * (Speed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(this.transform.position, pp, 5 * Time.deltaTime);
            //transform.forward * Time.deltaTime * Speed;
            CurrentFlietime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        DestroidThis();
    }
    public override void MakeDmg(GameObject traget)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("planeta"))
        {
            //          DestroidThis();
        }
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
