using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public CompositeCollider2D spawnBoundary;
    public List<GameObject> enemies;
    private ArenaManager arenaManager;
    private int walkEnemyAmt;
    private int jumpEnemyAmt;
    private int rangedEnemyAmt;
    private bool stateChanged;         //bool to make sure gamestate only changes once when mobs cleared

    // Enemy prefabs
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    void Start()
    {
        arenaManager = transform.parent.GetComponent<ArenaManager>();
        spawnBoundary = this.GetComponent<CompositeCollider2D>();
        enemies = new List<GameObject>();
        walkEnemyAmt = arenaManager.walkEnemyAmt;
        jumpEnemyAmt = arenaManager.jumpEnemyAmt;
        rangedEnemyAmt = arenaManager.rangedEnemyAmt;
        stateChanged = false;
        SpawnEnemies();
    }

    void Update()
    {
      if (enemies.Count <= 0 && !stateChanged)
      {
        Debug.Log("enemy Count");
        arenaManager.gameState = "portal";
        stateChanged = true;
      }
    }

    private void SpawnEnemies()
    {
      spawnEnemy(walkEnemyAmt, Enemy1);
      spawnEnemy(jumpEnemyAmt, Enemy2);
      spawnEnemy(rangedEnemyAmt, Enemy3);
    }

    private void spawnEnemy(int numberOfEnemies, GameObject enemyObject)
    {
        int i = 0;
        double delta = 0.5;
        while (i < numberOfEnemies)
        {
            Vector3 rndPoint3D = RandomPointInBounds(spawnBoundary.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector2 rndPointInside = spawnBoundary.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
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
                GameObject newEnemy = Instantiate(enemyObject, rndPoint2D, Quaternion.identity);
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

    public void DespawnEnemies()
    {
      foreach (GameObject enemy in enemies)
      {
        enemies.Remove(enemy);
        UnityEngine.Object.Destroy(enemy);
      }
    }
}
