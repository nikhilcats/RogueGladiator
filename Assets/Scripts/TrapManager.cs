using UnityEngine;
using System;
using System.Collections.Generic;         //Allows us to use Lists.
using Random = UnityEngine.Random;

public class TrapManager : MonoBehaviour
{
    //arena setup parameters
    private int spikeTrapAmt;
    private double boulderFreq; //how many boulders per second

    private ArenaManager arenaManager;
    private float time = 0f;
    private float frequency = 0;
    private int chance;
    private bool sometimesBoulderSticks = false;
    public CompositeCollider2D compositeCollider;
    public List<GameObject> traps;
    public List<GameObject> boulders;
    public GameObject TrapPrefab;
    public GameObject BoulderPrefab;

    void Start()
    {
      arenaManager = transform.parent.GetComponent<ArenaManager>();
      compositeCollider = gameObject.GetComponent<CompositeCollider2D>();
      traps = new List<GameObject>();
      boulderFreq = arenaManager.boulderFreq;
      if (boulderFreq > 0)
      {
        frequency = (float)1/(float)boulderFreq;
      }
      spawnSpikeTraps();
    }

    void Update()
    {
      if (frequency != 0)
      {
        time += Time.deltaTime;
        if (time >= frequency)
        {
          spawnBoulder();
          time = 0f;
        }
      }
    }

    private void spawnBoulder()
    {
      //determine location of boulder to spawn
      Vector3 rndPoint3D = RandomPointInBoundsForTraps(compositeCollider.bounds, 1f);
      Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
      //add boulder to boulder list
      GameObject newBoulder = Instantiate(BoulderPrefab, rndPoint2D, Quaternion.identity);
      newBoulder.transform.parent = this.transform;
      boulders.Add(newBoulder);
      //run boulder behavior method
      BoulderBehavior behavior = newBoulder.GetComponent<BoulderBehavior>();
      behavior.DropBoulder();
      //handle random chance of boulder sticking around
      if (sometimesBoulderSticks)
      {
        chance = Random.Range(1,50);
      }
      else
      {
        chance = 0;
      }
      if (chance != 1) {
        UnityEngine.Object.Destroy(newBoulder, 2.0f);
      }
      else
      {
        UnityEngine.Object.Destroy(newBoulder, 60.0f);
      }
    }

    private void spawnSpikeTraps()
    {
        spikeTrapAmt = arenaManager.spikeTrapAmt;
        //Vector2[] cornerPoints = polygonCollider.points;
        int i = 0;
        double delta = 0.5;
        while (i < spikeTrapAmt)
        {
            Vector3 rndPoint3D = RandomPointInBoundsForTraps(compositeCollider.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector2 rndPointInside = compositeCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
            if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
            {
                foreach (GameObject trap in traps)
                {
                    if (Math.Abs(trap.transform.position.x - rndPoint2D.x) < delta && Math.Abs(trap.transform.position.y - rndPoint2D.y) < delta)
                    {
                      goto tryAgain;
                    }
                }
                GameObject newTrap = Instantiate(TrapPrefab, rndPoint2D, Quaternion.identity);
                newTrap.transform.parent = this.transform;
                traps.Add(newTrap);
                i++;
                tryAgain:;
            }
        }
    }

    private Vector3 RandomPointInBoundsForTraps(Bounds bounds, float scale)
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            -0.1f
        );
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
