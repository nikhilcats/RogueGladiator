using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBehavior : MonoBehaviour
{
  public int damage = 5;
  GameObject player;
  Player component;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
    component = player.GetComponent<Player>();
  }

  // Update is called once per frame
  void Update()
  {
    //set new player on arena update
    if (!player)
    {
      player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
      component = player.GetComponent<Player>();
    }
  }

  public void DropBoulder()
  {
    //play falling animation
    Animator boulderAnim = this.GetComponent<Animator>();
    boulderAnim.Play("fullBoulder");
    //despawn handled in TrapManager
  }

  //handle when boulder hits ground
  public void Land()
  {
    //turn isTrigger off
    //check if player inside box
    //change render layer to under player/enemies but not spike or portal
    Collider2D playerBox = player.GetComponent<Collider2D>();
    Collider2D boulderBox = this.GetComponent<Collider2D>();
    Debug.Log(playerBox.bounds);
    if (playerBox.bounds.Intersects(boulderBox.bounds))
    {
      component.TakeDamage(damage);
    }
    this.GetComponent<Collider2D>().isTrigger = false;
    this.GetComponent<SpriteRenderer>().sortingOrder = 3;
  }
}
