using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooretEnemyResSystem : ResorseSystem
{
    // Start is called before the first frame update
    [SerializeField]
    private FortressHealthSystem fortress;

    public GameObject explotion;

    private AudioSource Aus;

    public AudioClip dmgsound;
    void Start()
    {
        init();
        canTakeDmg = false;
        Aus = GetComponent<AudioSource>();
    }

    public override void takeDmg(int dmg)
    {
        if (canTakeDmg)
        {

            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);

            if (currentHealth <= 0)
            {
                destroy();
            }
            else
            {
                if (Aus)
                {
                    if (!Aus.isPlaying)
                    {

                        Aus.clip = dmgsound;
                        Aus.PlayOneShot(dmgsound);

                    }


                }
            }
        }

    }

    public override void destroy()
    {
        if (fortress)
        {
            fortress.lessTorren();
        }
        Instantiate(explotion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
