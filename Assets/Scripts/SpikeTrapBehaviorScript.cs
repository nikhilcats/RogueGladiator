using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapBehaviorScript : MonoBehaviour
{
  public int damage = 5;
  private GameObject player;
  private Player component;
  bool armed = true;
  public AudioClip spikeNoise;

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
    this.GetComponent<AudioSource>().volume = 0.2f;
    this.GetComponent<AudioSource>().clip = spikeNoise;
    this.GetComponent<AudioSource>().Play();
    armed = true;
  }

  public void DisarmTrap()
  {
    this.GetComponent<AudioSource>().volume = 0.2f;
    this.GetComponent<AudioSource>().clip = spikeNoise;
    this.GetComponent<AudioSource>().Play();
    armed = false;
  }
}
