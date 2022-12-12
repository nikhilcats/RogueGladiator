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
  public double boulderFreq;
  public string gameState = "fight";
  public GameObject portal1;
  public GameObject portal2;

  //for portal icons
  public GameObject icon1;
  public GameObject icon2;
  public Sprite spikeSprite;
  public Sprite boulderSprite;
  public Sprite enemy1Sprite;
  public Sprite enemy2Sprite;
  public Sprite enemy3Sprite;

  //GameManager reference
    private GameObject gameManager;
  private GameManager gManagerScript;

  private EnemyManager enemyManager;
  private bool stateChanged = false;         //bool to make sure gamestate only changes once when mobs cleared

  // Start is called before the first frame update
  void Start()
  {
        //grab arena parameters from GameManager
        
    gameManager = GameObject.Find("GameManager");
    gManagerScript = gameManager.GetComponent<GameManager>();
    enemyManager = GameObject.Find("Enemyground").GetComponent<EnemyManager>();
    walkEnemyAmt = gManagerScript.walkEnemyAmt;
    jumpEnemyAmt = gManagerScript.jumpEnemyAmt;
    rangedEnemyAmt = gManagerScript.rangedEnemyAmt;
    spikeTrapAmt = gManagerScript.spikeTrapAmt;
    boulderFreq = gManagerScript.boulderFreq;

    }

  // Update is called once per frame
  void Update()
  {
    //check for if enemy list is empty in enemy manager
    //Debug.Log("HERE: " + enemyManager.enemies.Count);
    /*
    Debug.Log("AM");
    if (enemyManager.enemies.Count <= 0 && !stateChanged)
    {
      gameState = "portal";
      stateChanged = true;
    }
    */
    //check game state and change when needed
    if (gameState == "portal")
    {
      gManagerScript.assignPortals();
      //get portal type
      string portalType1 = portal1.GetComponent<PortalBehavior>().portalType;
      string portalType2 = portal2.GetComponent<PortalBehavior>().portalType;
      //set icon based on portal type
      SetIcons(portalType1, icon1);
      SetIcons(portalType2, icon2);
      //play wizard spell animation
      Animator wizardAnim = GameObject.Find("Wizard").GetComponent<Animator>();
      wizardAnim.Play("wizardSpell");
      //spawn portals
      SpawnPortals();
      //deactivate traps
      GameObject.Find("Enemyground").GetComponent<TrapManager>().Disarm();
      //stop boulder spawning
      GameObject.Find("Enemyground").GetComponent<TrapManager>().StopBoulders();
      //despawn enemies
      GameObject.Find("Enemyground").GetComponent<EnemyManager>().DespawnEnemies();
      gameState = "fight";
    }
  }

  //set portal icons
  private void SetIcons(string typeOfPortal, GameObject icon)
  {
    SpriteRenderer render = icon.GetComponent<SpriteRenderer>();
    if (typeOfPortal == "spikes")
    {
      render.sprite = spikeSprite;
    }
    else if (typeOfPortal == "boulders")
    {
      render.sprite = boulderSprite;
    }
    else if (typeOfPortal == "enemy1")
    {
      render.sprite = enemy1Sprite;
    }
    else if (typeOfPortal == "enemy2")
    {
      render.sprite = enemy2Sprite;
    }
    else if (typeOfPortal == "enemy3")
    {
      render.sprite = enemy3Sprite;
    }
  }

  public void SpawnPortals()
  {
    portal1.gameObject.SetActive(true);
    portal2.gameObject.SetActive(true);
    //portal icons
    icon1.gameObject.SetActive(true);
    icon2.gameObject.SetActive(true);
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
