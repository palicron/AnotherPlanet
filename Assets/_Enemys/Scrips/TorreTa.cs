using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreTa : Enemy
{
    [SerializeField]
    private GameObject Torreta;
    [SerializeField]
    private GameObject firePos;

    private FireSystem Fs;
    private float distToPlayer = 0;
    private Vector3 lookpos;
    private Quaternion lookRot;
    public bool activa = true;


    // Start is called before the first frame update
    void Start()
    {
        Fs = GetComponent<FireSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activa)
            return;

 
        fireatPlayer();
    }

    protected override void fireatPlayer()
    {
        distToPlayer = Vector3.Distance(player.gameObject.transform.position, this.transform.position);

        Debug.DrawLine(Torreta.transform.position, player.gameObject.transform.position);

        if (distToPlayer <= NoticeRange)
        {
            lookpos = player.gameObject.transform.position - this.transform.position;
            lookpos.y = 0;
            lookRot = Quaternion.LookRotation(lookpos);
            Torreta.transform.localRotation = Quaternion.Slerp(Torreta.transform.localRotation, lookRot, Time.deltaTime * 1.5f);
            float point = Vector3.Dot(lookpos.normalized, Torreta.transform.forward.normalized);
            if (Mathf.Abs(point) >= 0.90f)
            {
                Fs.FireP(firePos.transform.position, Torreta.transform.localRotation, player.gameObject);
            }
        }

    }

    protected override void move()
    {

    }
    public override void takeDmg(int dmg)
    {

    }
}
