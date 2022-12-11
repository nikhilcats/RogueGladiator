using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
  private GameManager gameManager;
  private GameObject player;
  private Player playerComponent;
  // Start is called before the first frame update
  void Start()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
    playerComponent = player.GetComponent<Player>();
  }

  // Update is called once per frame
  void Update()
  {
    //set new player on arena update
    if (!player)
    {
      player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
      playerComponent = player.GetComponent<Player>();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    //check if player object is colliding
    if (other == player.GetComponent<Collider2D>())
    {
      Debug.Log("new arena spawned");
      //pass portal type to game manager
      //iterate floor number
      gameManager.floorLevel++;
      //spawn new arena with new setup
      gameManager.newArena();
    }
  }
}
