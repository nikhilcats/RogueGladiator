using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapBehaviorScript : MonoBehaviour
{
  public int damage = 5;
  private GameObject player;
  private Player component;
  bool armed = true;

  void Start()
  {
    player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
    component = player.GetComponent<Player>();
  }

  void Update()
  {
    //set new player on arena update
    if (!player)
    {
      player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
      component = player.GetComponent<Player>();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    //check if traps activated
    if (armed)
    {
      //check if player object is set
      if (player)
      {
        //check if player object is the object colliding
        if (other == player.GetComponent<Collider2D>())
        {
          component.TakeDamage(damage);
        }
      }
    }
  }

  public void ArmTrap()
  {
    armed = true;
  }

  public void DisarmTrap()
  {
    armed = false;
  }
}
