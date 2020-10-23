using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResorseSystem : MonoBehaviour
{
    public Tipe tipo;
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected int maxEnergy;
    protected int currentHealth;
    protected int currentEnergy;
    public GameObject ForceFile;

    public bool canTakeDmg = true;
    // Start is called before the first frame update
    protected void init()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
    }
    public abstract void takeDmg(int dmg);

    public abstract void destroy();


}
