// using UnityEngine;
// using System;
// using System.Collections.Generic;         //Allows us to use Lists.
// using Random = UnityEngine.Random; 

// public class Testing : MonoBehaviour
// {
//     public CompositeCollider2D compositeCollider;
//     public int numberOfEnemies  = 5;
//     public GameObject[] enemies;

//     public GameObject Enemy1;

//     void Start()
//     {
//         compositeCollider = gameObject.GetComponent<CompositeCollider2D>();
//         spawnEnemies();
//         enemies = new GameObject[numberOfEnemies];
//     }

//     private void spawnEnemies()
//     {
//         //Vector2[] cornerPoints = polygonCollider.points;
//         int i = 0;
//         double delta = 1.0;
//         while (i < numberOfEnemies)
//         {
//             Vector3 rndPoint3D = RandomPointInBounds(compositeCollider.bounds, 1f);
//             Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
//             Vector2 rndPointInside = compositeCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
//             if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
//             {
//                 foreach (GameObject enemy in enemies)
//                 {
//                     if (Math.Abs(enemy.position.x - rndPoint2D.x) < delta && Math.Abs(enemy.position.y - rndPoint2D.y) < delta)
//                     {
//                         Debug.Log("Too close! Existing enemy at " + enemy.position.x + "," + enemy.position.y + ". Random point is " + rndPoint2D.x + "," + rndPoint2D.y);
//                         continue;
//                     }
//                 }
//                 GameObject newEnemy = Instantiate(Enemy1, rndPoint2D, Quaternion.identity);
//                 // GameObject rndCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                 // rndCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
//                 // rndCube.transform.position = rndPoint2D;
//                 enemies[i] = newEnemy;
//                 Debug.Log("new enemy position: " + rndPoint2D.x + ", " + rndPoint2D.y);
//                 i++;
//             }
//         }
//     }

//     private Vector3 RandomPointInBounds(Bounds bounds, float scale)
//     {
//         return new Vector3(
//             Random.Range(bounds.min.x * scale, bounds.max.x * scale),
//             Random.Range(bounds.min.y * scale, bounds.max.y * scale),
//             Random.Range(bounds.min.z * scale, bounds.max.z * scale)
//         );
//     }
// }