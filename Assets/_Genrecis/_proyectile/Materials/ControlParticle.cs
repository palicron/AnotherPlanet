using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime = 3.8f;
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
