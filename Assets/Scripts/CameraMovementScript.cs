using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 cameraPosition;
    //camera constraints
    private float followSpeed = 1;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    void Start()
    {
      player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
    }
    // Update is called once per frame
    void Update()
    {
      if (player)
      {
        playerPosition = player.transform.position;
      }
      else
      {
        playerPosition = new Vector3(0f,0f,0f);
        player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
      }
      cameraPosition = transform.position;
      cameraPosition = Vector3.Lerp(cameraPosition, playerPosition, followSpeed);
      cameraPosition.x = Mathf.Clamp(cameraPosition.x, xMin, xMax);
      cameraPosition.y = Mathf.Clamp(cameraPosition.y, yMin, yMax);
      cameraPosition.z = Mathf.Clamp(cameraPosition.z, -2, -2);

      transform.position = cameraPosition;
    }
}
