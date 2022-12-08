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

  //handle when boulder hits ground
  public void Land()
  {
    //play landing animation
    //.Play("boulderLand");
  }
}
