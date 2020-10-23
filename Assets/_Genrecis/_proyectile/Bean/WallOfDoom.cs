using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDoom : Proyectil
{
    public float Rotationtime;
    private float ctime = 0;

    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        Fire();
        // transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
    }
    public override void init(GameObject player = null)
    {
        this.Player = player;


    }
    public override void Fire()
    {
        StartCoroutine(move());
    }
    public override void MakeDmg(GameObject traget)
    {

    }
    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(new Vector3(0, 5 * Time.deltaTime, 0), Space.Self);

    }
    IEnumerator move()
    {

        wall.SetActive(true);
        while (ctime <= Rotationtime)
        {
            ctime += Time.deltaTime;
            transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0), Space.Self);
            yield return new WaitForEndOfFrame();
        }

        wall.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            return;
        }

        ResorseSystem rs = other.gameObject.GetComponent<ResorseSystem>();
        if (rs)
        {
            Debug.Log("sadasd");
            if (rs.tipo != owner)
            {

                rs.takeDmg(Dmg);

            }

        }
    }
}
