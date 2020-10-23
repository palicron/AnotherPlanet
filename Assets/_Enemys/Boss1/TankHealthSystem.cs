using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealthSystem : ResorseSystem
{

    public bool cantakedmg = false;

    public GameCAnvas canvas;
    public GameObject explotions;
    public GameObject cameras;

    public GiantTankAi ai;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void takeDmg(int dmg)
    {
        if (cantakedmg)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);

            if (currentHealth <= 0)
            {

                destroy();
            }
            else
            {
                canvas.setBosslife((float)currentHealth / (float)maxHealth);
            }
        }
    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(0.5f);
        cameras.SetActive(true);
        yield return new WaitForSeconds(3.7f);
        explotions.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvas.NextlvlMenu();
    }
    public override void destroy()
    {
        GameManager.Instance.UnlockNextLevel();
        ai.deathplayer();
        StartCoroutine(death());
    }
}
