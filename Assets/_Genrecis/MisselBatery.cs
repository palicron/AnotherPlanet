using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisselBatery : FireSystem
{
    // Start is called before the first frame update
    public GameObject[] position;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void FireP(Vector3 LaunchPos, Quaternion forward, GameObject player = null)
    {
        if (Time.time - lastFire >= timeBetweenFire)
        {
            lastFire = Time.time;
            GameObject p = Instantiate(proyectil, LaunchPos, Quaternion.identity);
            p.transform.rotation = forward;
            Proyectil pp = p.GetComponent<Proyectil>();
            pp.owner = tipo;
            pp.init(player);
            pp.Fire();
        }
    }
    public void FireB(GameObject player = null)
    {
        foreach (GameObject x in position)
        {
            StartCoroutine(Launch(x, player));
        }
    }

    IEnumerator Launch(GameObject x, GameObject player)
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 0.9f));
        GameObject p = Instantiate(proyectil, x.transform.position, Quaternion.identity);
        p.transform.rotation = x.transform.rotation;
        Proyectil pp = p.GetComponent<Proyectil>();
        pp.owner = tipo;
        pp.init(player);
        pp.Fire();
    }
}
