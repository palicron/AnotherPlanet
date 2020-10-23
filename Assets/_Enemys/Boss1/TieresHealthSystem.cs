using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieresHealthSystem : ResorseSystem
{
    // Start is called before the first frame update
    [SerializeField]
    private GiantTankAi body;
    public Tiretipe tipe;
    public GameObject smoke;

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


        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);

        if (currentHealth <= 0)
        {
            destroy();
        }
        else
        {

        }
    }
    public override void destroy()
    {
        smoke.SetActive(true);
        body.DestroidTire(tipe);
        Destroy(this);
    }
}
