using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Tiretipe
{
    Traccion, Turn
};
public class GiantTankAi : Enemy
{

    [SerializeField]
    TankBossTorret[] torrestas;
    [SerializeField]
    TankBossTorret Mortero;
    [SerializeField]
    private float speed;

    [SerializeField]
    private int numberofbarrage = 3;
    [SerializeField]
    private float turnrate;

    [SerializeField]
    private GameObject planet;
    public GameCAnvas canvas;
    [SerializeField]
    private float SpeedDecTrac;

    [SerializeField]
    private float SpeedDecTurn;
    [SerializeField]
    private float turnDec;

    [SerializeField]
    private float StopRange = 4;
    private NavMeshAgent agent;
    public LayerMask thisaplanet;
    float DistanceToground;

    public Vector3 GroundNormal;

    bool onGround = false;

    public GameObject ForceField;
    public int NumberOftires;
    Rigidbody rb;

    Vector3 lookpos;

    Quaternion lookRot;

    bool canMove = true;

    TankHealthSystem ts;

    float distToPlayer = 0;
    float FrontOrback = 0;
    Vector3 sideOfplayer = Vector3.zero;

    private Rigidbody prb;

    private bool canFireBarrage = true;
    public AudioSource Aus;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ts = GetComponent<TankHealthSystem>();
        prb = player.gameObject.GetComponent<Rigidbody>();
        Aus = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
            move();

        Fire();
    }

    protected override void fireatPlayer()
    {

    }
    public void deathplayer()
    {
        player.canMove = false;
        player.gameObject.GetComponent<PlayerResorSystem>().canTakeDmg = false;
    }
    protected override void move()
    {

        Vector3 predic = prb.velocity.normalized * 1.5f; ;
        predic.y = 0;
        lookpos = player.transform.position + predic - this.transform.position;
        lookpos.y = 0;
        lookRot = Quaternion.LookRotation(lookpos);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRot, Time.deltaTime * turnrate);
        transform.position += transform.forward * Time.deltaTime * speed;
        // agent.Move(transform.forward.normalized * 2 * Time.deltaTime);
        //agent.SetDestination(player.transform.position);
    }
    public override void takeDmg(int dmg)
    {

    }

    void groundCheck()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 3, thisaplanet))
        {

            DistanceToground = hit.distance;
            GroundNormal = hit.normal;

            if (DistanceToground <= 0.5f)
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }

        }
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        if (onGround == false)
        {
            rb.AddForce(gravDirection * -10f);

        }
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, GroundNormal) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Destruc"))
        {
            Destroy(other.gameObject);
            if (Aus)
            {


                Aus.PlayOneShot(sound);



            }

        }
        Rigidbody rr = other.gameObject.GetComponent<Rigidbody>();
        if (rr)
        {
            Vector3 b = (other.gameObject.transform.position - this.transform.position);
            rr.AddForce(b * 500);
        }

    }

    public void DestroidTire(Tiretipe tipe)
    {
        NumberOftires--;
        if (NumberOftires <= 0)
        {
            canvas.ActiveBossLife();
            canMove = false;
            Destroy(ForceField);
            Aus.Stop();
            ts.cantakedmg = true;
        }
        else
        {
            if (tipe == Tiretipe.Traccion)
            {
                speed -= SpeedDecTrac;
            }
            else
            {
                speed -= SpeedDecTurn;
                turnrate -= turnDec;
            }
        }
    }

    void Fire()
    {
        Vector3 Pdir = -(this.transform.position - player.transform.position).normalized;
        distToPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        if (distToPlayer <= FireRange)
        {
            if (canFireBarrage)
            {
                canFireBarrage = false;
                StartCoroutine(Barrage());
            }


            FrontOrback = Vector3.Dot(Pdir, this.transform.forward.normalized);
            if (FrontOrback >= 0.8f)
            {

                torrestas[0].fireTorre(planet);
                torrestas[1].fireTorre(planet);
            }
            else if (FrontOrback <= -0.8f)
            {

                torrestas[2].fireTorre(planet);
                torrestas[3].fireTorre(planet);
            }
            else
            {
                sideOfplayer = Vector3.Cross(Pdir, this.transform.forward.normalized);
                if (sideOfplayer.y > 0)
                {

                    torrestas[0].fireTorre(planet);
                    torrestas[2].fireTorre(planet);
                }
                else
                {

                    torrestas[1].fireTorre(planet);
                    torrestas[3].fireTorre(planet);
                }
            }
        }

        sideOfplayer = Vector3.Cross(Pdir, this.transform.forward.normalized);

    }

    IEnumerator Barrage()
    {

        int ik = numberofbarrage + Random.Range(-1, 2);
        int i = 0;
        while (i < ik)
        {
            yield return new WaitForSeconds(0.8f);

            Mortero.fireTorre(planet);

            i++;
        }

        yield return new WaitForSeconds(3f);
        canFireBarrage = true;
    }
}

