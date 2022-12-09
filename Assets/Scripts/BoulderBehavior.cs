using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBehavior : MonoBehaviour
{
  public int damage = 5;
  GameObject player;
  Player component;
  bool landed = false;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.Find("GameManager/Playerground/Player");
    component = player.GetComponent<Player>();
  }

  // Update is called once per frame
  void Update()
  {

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
    Debug.Log("boulder landing");
    //turn isTrigger off
    this.GetComponent<Collider2D>().isTrigger = false;
    //check if player inside box
    Collider2D playerBox = player.GetComponent<Collider2D>();
    Collider2D boulderBox = this.GetComponent<Collider2D>();
    if (playerBox.bounds.Intersects(boulderBox.bounds))
    {
        component.TakeDamage(damage);
    }
  }
}
