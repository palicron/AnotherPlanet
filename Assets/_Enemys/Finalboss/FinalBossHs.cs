using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHs : FortressHealthSystem
{

    public FinalBossAi Aiboss;

    public int firtfaselife = 65;


    public int Seconaselife = 30;


    private float ffz;


    private bool ff = false;
    private bool sf = false;
    public float zforcefiel = -2.22f;

    public GameObject camerass;
    public GameObject[] Explotion;
    // Start is called before the first frame update
    void Start()
    {
        init();
        canTakeDmg = false;
        ffz = ForceFile.transform.position.y;
    }

    public void resetWave(int nt)
    {
        Generetors = nt;
    }

    public override void takeDmg(int dmg)
    {
        if (canTakeDmg)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                camerass.SetActive(true);
                Aiboss.destroid();
                canvas.setBosslife(0);
                Aiboss.Destroid();

                StartCoroutine(destroid());


            }
            else
            {
                canvas.setBosslife((float)currentHealth / (float)maxHealth);
                if (currentHealth <= firtfaselife && !ff)
                {
                    ff = true;
                    Aiboss.nextphase();
                    Generetors = 5;
                    StartCoroutine(ClosePhase());
                }
                else if (currentHealth <= Seconaselife && !sf)
                {
                    sf = true;
                    Aiboss.nextphase();
                    Generetors = 5;
                    StartCoroutine(ClosePhase());
                }


            }
        }
    }

    IEnumerator destroid()
    {
        yield return new WaitForSeconds(2f);
        Explotion[0].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Explotion[1].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Explotion[2].SetActive(true);
        yield return new WaitForSeconds(1.3f);
        Explotion[0].SetActive(false);
        Explotion[1].SetActive(false);
        Explotion[2].SetActive(false);
        Explotion[0].SetActive(true);
        Explotion[1].SetActive(true);
        Explotion[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        endgame();
    }
    public override void lessTorren()
    {
        Generetors--;
        if (Generetors == 0)
        {
            Debug.Log("Se acabaron las torres");
            StartCoroutine(ActiveWave());
        }
    }
    protected void endgame()
    {

        canvas.endGamemenu();

    }
    public void stardmgphase()
    {

        StartCoroutine(Dmgphase());
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ActiveWave()
    {
        yield return new WaitForSeconds(1f);
        Aiboss.Starfase();

    }
    IEnumerator Dmgphase()
    {

        Vector3 d = new Vector3(0, zforcefiel, 0);
        while (Vector3.Distance(ForceFile.transform.position, d) >= 0.2)
        {
            ForceFile.transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        canTakeDmg = true;
        canvas.ActiveBossLife();
    }
    IEnumerator ClosePhase()
    {
        canTakeDmg = false;
        canvas.DesactiveBossLife();
        Vector3 d = new Vector3(0, ffz, 0);
        while (Vector3.Distance(ForceFile.transform.position, d) >= 0.3f)
        {
            ForceFile.transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }

    }
}
