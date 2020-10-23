using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteRFsystem : FireSystem
{
    // Start is called before the first frame update
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
            if (MuzzelVFX)
            {
                var f = Instantiate(MuzzelVFX, LaunchPos, forward);
                Destroy(f, 0.4f);
            }
            pp.init(player);
            pp.Fire();
        }
    }
}
