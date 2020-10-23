using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorHealthSystem : ResorseSystem
{

    public ResorseSystem[] Torretas;
    public GameObject explotion;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public override void takeDmg(int dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
        if (currentHealth <= 0)
        {
            destroy();
        }
    }

    public override void destroy()
    {
        foreach (ResorseSystem x in Torretas)
        {
            x.canTakeDmg = true;
            Destroy(x.ForceFile);
        }
        Instantiate(explotion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.2f);
    }

}
