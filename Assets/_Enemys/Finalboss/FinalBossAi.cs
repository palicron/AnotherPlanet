using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAi : MonoBehaviour
{

    public Player_Ctr player;

    public FinalBossHs Hs;


    public CicoleLaser LaserCircular;
    public SecuensiaBeam secuancialaser;
    public WallOfDoom wallofD;
    public FollowBeam laserFollow;

    public int face = 1;
    public GameObject wave1GO;

    public GameObject wave2GO;

    public GameObject wave3GO;

    public float zrise;
    public GameObject[] wave1;
    public GameObject[] wave2;
    public GameObject[] wave3;

    public GameObject[] lasers;
    public bool infase = false;
    public GameObject cf;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movef());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void nextphase()
    {
        infase = false;
        face++;

        if (face == 2)
        {
            StartCoroutine(move2());

        }
        else if (face == 3)
        {
            StartCoroutine(move3());
        }


    }

    public void Starfase()
    {
        switch (face)
        {
            case 1:
                Debug.Log("Iniciando fase 1");
                laserFollow.gameObject.SetActive(true);
                lasers[0].SetActive(true);
                StartCoroutine(fase1());
                break;
            case 2:
                lasers[1].SetActive(true);
                LaserCircular.gameObject.SetActive(true);
                StartCoroutine(fase2());
                break;
            case 3:
                lasers[2].SetActive(true);
                secuancialaser.gameObject.SetActive(true);
                StartCoroutine(fase3());
                break;
            default:
                break;
        }
    }

    IEnumerator fase1()
    {

        laserFollow.Fire();

        yield return new WaitForSeconds(3f);
        Hs.stardmgphase();
        infase = true;

        while (infase)
        {

            yield return new WaitForEndOfFrame();

        }

        laserFollow.gameObject.SetActive(false);
    }
    IEnumerator fase2()
    {
        LaserCircular.Fire();
        yield return new WaitForSeconds(4f);
        Hs.stardmgphase();
        infase = true;
        yield return new WaitForSeconds(2);
        laserFollow.gameObject.SetActive(true);
        laserFollow.Fire();
        float times = Random.Range(0.4f, 1.5f);
        while (infase)
        {
            yield return new WaitForSeconds(times);
            LaserCircular.Fire();
        }

        laserFollow.gameObject.SetActive(false);
        LaserCircular.gameObject.SetActive(false);
    }
    IEnumerator fase3()
    {
        secuancialaser.Fire();
        yield return new WaitForSeconds(5);
        Hs.stardmgphase();
        infase = true;
        yield return new WaitForSeconds(1f);
        laserFollow.gameObject.SetActive(true);
        laserFollow.Fire();
        float times = Random.Range(0.4f, 1.5f);
        while (infase)
        {

            yield return new WaitForSeconds(times);
            LaserCircular.gameObject.SetActive(true);
            LaserCircular.Fire();


        }
        laserFollow.gameObject.SetActive(false);
        LaserCircular.gameObject.SetActive(false);
        secuancialaser.gameObject.SetActive(false);
    }

    IEnumerator movef()
    {
      
        while (wave1GO.transform.position.y - zrise < 0f)
        {
            wave1GO.transform.position += new Vector3(0, 4f * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }

        foreach (GameObject x in wave1)
        {
            x.GetComponent<TorreTa>().activa = true;
            x.GetComponent<TooretEnemyResSystem>().canTakeDmg = true;
        }
    }
    IEnumerator move2()
    {

        while (wave2GO.transform.position.y - zrise < 0f)
        {
            wave2GO.transform.position += new Vector3(0, 4f * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }

        foreach (GameObject x in wave2)
        {
            x.GetComponent<TorreTa>().activa = true;
            x.GetComponent<TooretEnemyResSystem>().canTakeDmg = true;
        }
    }
    IEnumerator move3()
    {

        while (wave3GO.transform.position.y - zrise < 0f)
        {
            wave3GO.transform.position += new Vector3(0, 4f * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }

        foreach (GameObject x in wave3)
        {
            x.GetComponent<TorreTa>().activa = true;
            x.GetComponent<TooretEnemyResSystem>().canTakeDmg = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (infase && other.gameObject.tag.Equals("Player"))
        {
            cf.SetActive(true);
            cf.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(attack(other));


        }
    }
    IEnumerator attack(Collider other)
    {
        float a = 0;
        yield return new WaitForSeconds(0.5f);
        while (cf.transform.localScale.x <= 5)
        {
            a += 20 * Time.deltaTime;
            Debug.Log(a);
            cf.transform.localScale = (new Vector3(a, a, a));
            yield return new WaitForEndOfFrame();
        }
        other.GetComponent<Rigidbody>().AddForce((other.gameObject.transform.position - this.transform.position).normalized * 1500f);
        yield return new WaitForSeconds(0.5f);
        cf.SetActive(false);
    }


    public void Destroid()
    {
        laserFollow.gameObject.SetActive(false);
        LaserCircular.gameObject.SetActive(false);
        secuancialaser.gameObject.SetActive(false);
        Destroy(this);
    }

    public void destroid()
    {

        player.GetComponent<Player_Ctr>().canMove = false;
        player.GetComponent<PlayerResorSystem>().canTakeDmg = false;

    }
}
