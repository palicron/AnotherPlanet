using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressHealthSystem : ResorseSystem
{
    [SerializeField]
    protected int Generetors;
    [SerializeField]
    private FoDAi Ai;
    public GameCAnvas canvas;
    public GameObject[] explotions;

    public GameObject camerastates;
    // Start is called before the first frame update
    void Start()
    {
        init();
        canTakeDmg = false;
    }

    public override void takeDmg(int dmg)
    {
        if (canTakeDmg)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                canvas.setBosslife(0);

                destroy();
            }
            else
            {
                canvas.setBosslife((float)currentHealth / (float)maxHealth);
            }
        }

    }

    protected virtual void endlevel()
    {
        GameManager.Instance.UnlockNextLevel();

        canvas.NextlvlMenu();

    }

    public override void destroy()
    {
        Ai.death();
        Destroy(Ai);
        camerastates.SetActive(true);
        StartCoroutine(destro());
    }

    IEnumerator destro()
    {
        yield return new WaitForSeconds(3f);
        foreach (GameObject x in explotions)
        {
            x.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        Invoke("endlevel", 2f);

    }
    public virtual void lessTorren()
    {
        Generetors--;
        if (Generetors == 0)
        {
            Ai.openFortress();
            canTakeDmg = true;
            canvas.ActiveBossLife();
        }
    }
}
