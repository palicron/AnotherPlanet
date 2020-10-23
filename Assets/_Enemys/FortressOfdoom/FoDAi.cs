using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoDAi : MonoBehaviour
{
    public float AttackRange;
    public float pushbackrange;
    public float timeBetweenFire;

    public GameObject player;
    public GameObject TorretaCentral;
    public GameObject PuertaD;
    public GameObject PuertaI;

    public GameObject MisD;
    public GameObject MisI;

    public Vector3 PTc;
    public Vector3 PpD;
    public Vector3 PpI;
    public Vector3 PmD;
    public Vector3 Pmi;

    private MisselBatery MbD;
    private MisselBatery MbI;
    private bool canfire = false;

    public AudioSource Aus;
    public AudioClip sound;


    private float lastFire = 0;
    // Start is called before the first frame update
    void Start()
    {
        TorretaCentral.GetComponent<TorreTa>().activa = false;
        MbD = MisD.GetComponent<MisselBatery>();
        MbI = MisI.GetComponent<MisselBatery>();
        Aus = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!canfire)
            return;

        Attack();
    }

    void Attack()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= AttackRange && Vector3.Distance(this.transform.position, player.transform.position) >= pushbackrange)
        {
            if (Time.time - lastFire >= timeBetweenFire + Random.Range(0, 2f))
            {
                lastFire = Time.time;
                MbD.FireB(player);
                MbI.FireB(player);
            }

        }
    }

    public void openFortress()
    {
        StartCoroutine(Open());
    }

    public void death()
    {
        player.GetComponent<Player_Ctr>().canMove = false;
        player.GetComponent<PlayerResorSystem>().canTakeDmg = false;
    }
    IEnumerator Open()
    {

        bool Onpos = false;
        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        bool b5 = false;

        if (Aus)
        {
            if (!Aus.isPlaying)
            {
                Aus.PlayOneShot(sound);
            }

        }
        while (!Onpos)
        {
            Debug.Log(MisD.transform.position.y - PmD.y);

            if (TorretaCentral.transform.position.z - PTc.z <= 0.3f)
            {
                b1 = true;

            }
            else
            {
                TorretaCentral.transform.position += new Vector3(0, 0, -1) * Time.deltaTime;
            }
            if ((Mathf.Abs(MisD.transform.position.y - PmD.y) <= 0.3f))
            {
                b2 = true;
            }
            else
            {
                MisD.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            }
            if (Mathf.Abs(MisI.transform.position.y - Pmi.y) <= 0.3f)
            {
                b3 = true;

            }
            else
            {
                MisI.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            }
            if (Mathf.Abs(PuertaI.transform.position.x - PpI.x) <= 0.3f)
            {
                b4 = true;

            }
            else
            {
                PuertaI.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
            }
            if (Mathf.Abs(PuertaD.transform.position.x - PpD.x) <= 0.3f)
            {
                b5 = true;

            }
            else
            {
                PuertaD.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
            }

            Onpos = b1 && b2 && b3 && b4 && b5;
            yield return new WaitForEndOfFrame();
        }

        TorretaCentral.GetComponent<TorreTa>().activa = true;
        canfire = true;
    }


    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pushbackrange);
    }
}
