using UnityEngine;
using System;
using System.Collections.Generic;         //Allows us to use Lists.
using Random = UnityEngine.Random;

public class TrapManager : MonoBehaviour
{
    public CompositeCollider2D compositeCollider;
    public int numberOfSpikeTraps = 5;
    public List<GameObject> traps;
    public List<GameObject> boulders;
    public float boulderFreq = 1; //how many boulders per x
    public int waiting = 300; //every x frames

    public GameObject TrapPrefab;
    public GameObject BoulderPrefab;

    void Start()
    {
        compositeCollider = gameObject.GetComponent<CompositeCollider2D>();
        traps = new List<GameObject>();
        spawnSpikeTraps();
    }

    void Update()
    {
        //spawn boulder IF IT MAKES SENSE IN CURRENT GAME STATE (still fighting enemies)
        bool inPlay = true; //placeholder for game state decision
        if (inPlay)
        {
          //must determine how frequently to drop boulder
          //PLACEHOLDER
          waiting--;
          if (waiting < 1)
          {
            waiting = 300;
            spawnBoulder();
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
      Debug.Log("boulder dropped");
      UnityEngine.Object.Destroy(newBoulder, 2.0f);
    }

    private void spawnSpikeTraps()
    {
        //Vector2[] cornerPoints = polygonCollider.points;
        int i = 0;
        double delta = 0.5;
        while (i < numberOfSpikeTraps)
        {
            Vector3 rndPoint3D = RandomPointInBoundsForTraps(compositeCollider.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector2 rndPointInside = compositeCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
            if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
            {
                Debug.Log("Entered if statement branch with i=" + i);
                foreach (GameObject trap in traps)
                {
                    Debug.Log("Entered foreach loop with i=" + i);
                    if (Math.Abs(trap.transform.position.x - rndPoint2D.x) < delta && Math.Abs(trap.transform.position.y - rndPoint2D.y) < delta)
                    {
                        Debug.Log("Too close! Existing trap at " + trap.transform.position.x + "," + trap.transform.position.y + ". Random point is " + rndPoint2D.x + "," + rndPoint2D.y);
                        goto tryAgain;
                    }
                }
                GameObject newTrap = Instantiate(TrapPrefab, rndPoint2D, Quaternion.identity);
                newTrap.transform.parent = this.transform;
                traps.Add(newTrap);
                Debug.Log("new trap position: " + rndPoint2D.x + ", " + rndPoint2D.y);
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
            Random.Range(0, 0)
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
