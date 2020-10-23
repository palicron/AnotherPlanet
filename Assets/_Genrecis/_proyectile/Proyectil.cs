using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Tipe
{
    player, enemy
};
public abstract class Proyectil : MonoBehaviour
{
    [SerializeField]
    protected float Speed;
    [SerializeField]
    protected int Dmg;

    [SerializeField]
    protected float MaxflyTime;

    protected float CurrentFlietime = 0;

    protected float DistanceTOground;
    [SerializeField]
    protected float MaxDitToground = 0.4f;

    protected float DistanceToGorund;

    protected Vector3 GroundNormal;

    protected Rigidbody rb;

    [SerializeField]
    protected GameObject planet;
    public Tipe owner;
    public float downspeed = -1;
    public GameObject Player = null;
    public abstract void init(GameObject player = null);
    public abstract void Fire();
    public abstract void MakeDmg(GameObject traget);
    public LayerMask thisaplanet;

    public GameObject ExplocionVFX;
    public void DestroidThis()
    {
        if (ExplocionVFX)
        {
            var f = Instantiate(ExplocionVFX, this.transform.position, Quaternion.identity);
            Destroy(f, 0.4f);
        }
        StopAllCoroutines();
        GameObject.Destroy(this.gameObject, 0.1f);
    }

    public void snapToplanet()
    {
        RaycastHit hit = new RaycastHit();
        //Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        if (Physics.Raycast(transform.position, -transform.up, out hit, DistanceToGorund, thisaplanet))
        {

            DistanceTOground = hit.distance;
            GroundNormal = hit.normal;



            //Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
            //this.transform.position = new Vector3(this.transform.position.x, hit.point.y + (0.65f), this.transform.position.z);


        }
        else
        {
            this.transform.position += new Vector3(0, downspeed, 0) * Time.deltaTime;
        }
        Debug.DrawLine(this.transform.position, -transform.up.normalized * 30);
    }



}
