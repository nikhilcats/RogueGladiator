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
    //disable traps if game state no longer allows
    bool inPlay = true; //placeholder for game state decision
    if (!inPlay)
    {
      DisarmTrap();
    }
    //set new player on arena update
    if (!player)
    {
      player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
      component = player.GetComponent<Player>();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (armed)
    {
      if (other == player.GetComponent<Collider2D>())
      {
        Debug.Log(string.Format("haha idiot you stepped in the KNIFE PILE and took {0} damage!", damage));
        component.TakeDamage(damage);
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
