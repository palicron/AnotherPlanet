using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResorSystem : ResorseSystem
{
    // Start is called before the first frame update

    private const string LERP = "Vector1_10F7BB69";
    private const string Emision = "Vector1_C7A08C45";
    public Material shipmat;
    public float InbuTime = 0.2f;

    private float Lerping = 1;

    private float emis = 10;

    private float time = 0;

    private AudioSource Aus;

    public AudioClip dmgsound;

    public GameCAnvas canvas;


    void Start()
    {
        init();
        shipmat.SetFloat(LERP, 0);
        shipmat.SetFloat(Emision, 5);
        Aus = GetComponent<AudioSource>();
    }


    public override void takeDmg(int dmg)
    {
        if (!canTakeDmg)
            return;
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);

        if (currentHealth <= 0)
        {
            shipmat.SetFloat(LERP, 1);
            shipmat.SetFloat(Emision, 15);
            canvas.setPlayerlife(0);
            destroy();
        }
        else if (Time.time - time >= InbuTime)
        {
            if (Aus)
            {
                if (!Aus.isPlaying)
                {

                    Aus.clip = dmgsound;
                    Aus.PlayOneShot(dmgsound);

                }
            }


            time = Time.time;
            shipmat.SetFloat(LERP, 1);
            shipmat.SetFloat(Emision, 10);
            emis = 10;
            float por = Mathf.Clamp01((float)currentHealth / (float)maxHealth);
            canvas.setPlayerlife(por);
            Lerping = 1;
            StartCoroutine(ChangeColor());
        }
    }

    public override void destroy()
    {
        canvas.RestarMenu();
    }


    IEnumerator ChangeColor()
    {

        while (Lerping >= 0 && emis >= 5)
        {

            Lerping = Lerping - (Time.deltaTime);
            emis = emis - Time.deltaTime;

            shipmat.SetFloat(LERP, Lerping);
            shipmat.SetFloat(Emision, emis);
            yield return new WaitForEndOfFrame();
        }

        shipmat.SetFloat(LERP, 0);
        shipmat.SetFloat(Emision, 5);
    }


}
