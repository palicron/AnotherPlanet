using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{

    public GameObject proyectil;


    public AudioSource Aus;
    public float timeBetweenFire = 0.3f;

    protected float lastFire = 0;
    public AudioClip sound;
    public Tipe tipo;

    public GameObject MuzzelVFX;

    // Start is called before the first frame update
    void Start()
    {
        Aus = GetComponent<AudioSource>();

    }

    public virtual void FireP(Vector3 LaunchPos, Quaternion forward, GameObject player = null)
    {

        if (Time.time - lastFire >= timeBetweenFire)
        {

            if (Aus)
            {
                if (!Aus.isPlaying)
                {

                    Aus.clip = sound;
                    Aus.PlayOneShot(sound);

                }


            }
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
