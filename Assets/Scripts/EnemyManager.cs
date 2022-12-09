using UnityEngine;
using System;
using System.Collections.Generic;         //Allows us to use Lists.
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    //arena setup parameters
    private int walkEnemyAmt;

    public CompositeCollider2D compositeCollider;
    public List<GameObject> enemies;
    public GameObject Enemy1;
    private ArenaManager arenaManager;

    void Start()
    {
        arenaManager = GameObject.Find("ArenaManager").GetComponent<ArenaManager>();
        compositeCollider = gameObject.GetComponent<CompositeCollider2D>();
        enemies = new List<GameObject>();
        spawnEnemies();
    }

    private void spawnEnemies()
    {
        walkEnemyAmt = arenaManager.walkEnemyAmt;
        //Vector2[] cornerPoints = polygonCollider.points;
        int i = 0;
        double delta = 0.5;
        while (i < walkEnemyAmt)
        {
            Vector3 rndPoint3D = RandomPointInBounds(compositeCollider.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector2 rndPointInside = compositeCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
            if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
            {
                Debug.Log("Entered if statement branch with i=" + i);
                foreach (GameObject enemy in enemies)
                {
                    Debug.Log("Entered foreach loop with i=" + i);
                    if (Math.Abs(enemy.transform.position.x - rndPoint2D.x) < delta && Math.Abs(enemy.transform.position.y - rndPoint2D.y) < delta)
                    {
                        Debug.Log("Too close! Existing enemy at " + enemy.transform.position.x + "," + enemy.transform.position.y + ". Random point is " + rndPoint2D.x + "," + rndPoint2D.y);
                        goto tryAgain;
                    }
                }
                GameObject newEnemy = Instantiate(Enemy1, rndPoint2D, Quaternion.identity);
                newEnemy.transform.parent = this.transform;
                enemies.Add(newEnemy);
                Debug.Log("new enemy position: " + rndPoint2D.x + ", " + rndPoint2D.y);
                i++;
                tryAgain:;
            }
        }
    }

    private Vector3 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            Random.Range(bounds.min.z * scale, bounds.max.z * scale)
        );
    }
}
