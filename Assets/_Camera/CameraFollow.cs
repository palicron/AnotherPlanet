using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    public Transform planet;
    public float Height = 10.0f;
    public float distance = 30f;

    public float cameraSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (!player)
            return;

        Vector3 worldPosition = (player.position - planet.position).normalized * distance;
        this.transform.position = Vector3.MoveTowards(transform.position, worldPosition, cameraSpeed);
        this.transform.LookAt(player);


    }
}
