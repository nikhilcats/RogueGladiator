using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public GameObject player;
    //camera constraints
    public float followSpeed;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 cameraPosition = transform.position;

        cameraPosition = Vector3.Lerp(cameraPosition, playerPosition, followSpeed);

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, xMin, xMax);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, yMin, yMax);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, -1, -1);

        transform.position = cameraPosition;
    }
}