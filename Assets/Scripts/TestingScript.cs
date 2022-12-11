using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
  public GameObject enemyGround;
  public List<GameObject> traps;

  // Start is called before the first frame update
  void Start()
  {
    TrapManager trapMan = enemyGround.GetComponent<TrapManager>();
    traps = trapMan.traps;
  }

  // Update is called once per frame
  void Update()
  {

  }

  //arm all spike traps
  public void Arm()
  {
    foreach (GameObject trap in traps)
    {
      SpikeTrapBehaviorScript trapBehavior = trap.GetComponent<SpikeTrapBehaviorScript>();
      Animator trapAnim = trap.GetComponent<Animator>();
      trapBehavior.ArmTrap();
      trapAnim.Play("spikeArm");
      Debug.Log("trap armed");
    }
  }

  //disarm all spike traps
  public void Disarm()
  {
    foreach (GameObject trap in traps)
    {
      SpikeTrapBehaviorScript trapBehavior = trap.GetComponent<SpikeTrapBehaviorScript>();
      Animator trapAnim = trap.GetComponent<Animator>();
      trapBehavior.DisarmTrap();
      trapAnim.Play("spikeDisarm");
      Debug.Log("trap disarmed");
    }
  }
}
