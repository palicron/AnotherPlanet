using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float NoticeRange;
    [SerializeField]
    protected float FireRange;
    [SerializeField]
    protected Player_Ctr player;

    protected abstract void fireatPlayer();
    protected abstract void move();
    public abstract void takeDmg(int dmg);

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, NoticeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
}
