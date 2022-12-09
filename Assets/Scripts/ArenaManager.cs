using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
  //arena setup parameters
  public int walkEnemyAmt;
  public int jumpEnemyAmt;
  public int rangedEnemyAmt;
  public int spikeTrapAmt;
  public float boulderFreq;

  //GameManager reference
  private GameObject gameManager;
  private GameManager gManagerScript;

  // Start is called before the first frame update
  void Start()
  {
    //grab arena parameters from GameManager
    gameManager = GameObject.Find("GameManager");
    gManagerScript = gameManager.GetComponent<GameManager>();
    walkEnemyAmt = gManagerScript.walkEnemyAmt;
    jumpEnemyAmt = gManagerScript.jumpEnemyAmt;
    rangedEnemyAmt = gManagerScript.rangedEnemyAmt;
    spikeTrapAmt = gManagerScript.spikeTrapAmt;
    boulderFreq = gManagerScript.boulderFreq;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetupScene(int floor)
  {
    //pass setup parameters to trap/enemy managers
    //reference managers
    //EnemyManager enemyManager = GameObject.Find("Enemyground").GetComponent<EnemyManager>();
    //TrapManager trapManager = GameObject.Find("Enemyground").GetComponent<TrapManager>();
    //enemies
    //enemyManager.walkEnemyAmt = walkEnemyAmt;
    //enemyManager.jumpEnemyAmt = jumpEnemyAmt;
    //enemyManager.rangedEnemyAmt = rangedEnemyAmt;
    //traps
    //trapManager.spikeTrapAmt = spikeTrapAmt;
    //trapManager.boulderFreq = boulderFreq;
  }
}
