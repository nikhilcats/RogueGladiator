using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTERSCRIPT : MonoBehaviour
{
    GameObject field;
    TrapManager trapMan;
    List<GameObject> traps;
    // Start is called before the first frame update
    void Start()
    {
      Debug.Log("TESTERSCRIPT");
      field = GameObject.Find("Enemyground");
      trapMan = field.GetComponent<TrapManager>();
      traps = trapMan.traps;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
