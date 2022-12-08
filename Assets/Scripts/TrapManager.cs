using UnityEngine;
using System;
using System.Collections.Generic;         //Allows us to use Lists.
using Random = UnityEngine.Random;

public class TrapManager : MonoBehaviour
{
    public CompositeCollider2D compositeCollider;
    public int numberOfTraps = 5;
    public List<GameObject> traps;

    public GameObject TrapPrefab;

    void Start()
    {
        compositeCollider = gameObject.GetComponent<CompositeCollider2D>();
        traps = new List<GameObject>();
        Debug.Log("START TRAP MANAGER");
        spawnTraps();
    }

    private void spawnTraps()
    {
        //Vector2[] cornerPoints = polygonCollider.points;
        int i = 0;
        double delta = 0.5;
        while (i < numberOfTraps)
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
}
