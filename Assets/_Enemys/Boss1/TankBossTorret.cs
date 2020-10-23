using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBossTorret : MonoBehaviour
{
    [SerializeField]
    private GameObject Torreta;
    [SerializeField]
    private GameObject firePos;
    [SerializeField]
    private float RotateSpeed = 1f;

    public LayerMask thisaplanet;
    public GameObject player;

    private Vector3 lookpos;
    private Quaternion lookRot;

    public bool canRotate = true;
    private FireSystem Fs;
    // Start is called before the first frame update
    void Start()
    {
        Fs = GetComponent<FireSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canRotate)
            traceplayer();
    }

    public void traceplayer()
    {

        lookpos = player.transform.position - Torreta.transform.position;

        lookpos.y = 0;
        lookRot.z = 90;
        lookRot = Quaternion.LookRotation(lookpos);

        Torreta.transform.localRotation = Quaternion.Slerp(Torreta.transform.localRotation, lookRot, Time.deltaTime * RotateSpeed);


    }

    public void fireTorre(GameObject planet)
    {
        Fs.FireP(firePos.transform.position, Torreta.transform.rotation, player.gameObject);
    }


}
