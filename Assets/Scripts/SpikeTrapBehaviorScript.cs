using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapBehaviorScript : MonoBehaviour
{
  public int damage = 5;
  GameObject player;
  Player component;
  bool armed = false;

  void Start()
  {
    player = GameObject.Find("GameManager/ArenaManager/PlayerBounds/Player");
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
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (armed)
    {
      Debug.Log(string.Format("haha idiot you stepped in the KNIFE PILE and took {0} damage!", damage));
      component.TakeDamage(damage);
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
