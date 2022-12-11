using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;        //Allows us to use Lists.

public class GameManager : MonoBehaviour
{

  public static GameManager instance = null;    //Static instance of GameManager which allows it to be accessed by any other script.
  private ArenaManager arenaManager;            //Store a reference to our ArenaManager which will set up the level.
  private GameObject arenaManagerObj;
  public GameObject arenaManagerPrefab;
  private int floorLevel = 1;                   //Current floor number, expressed in game as floor 1
  private string seed;
  private Vector3 arenaManagerTransform = new Vector3(4.5f, -0.2f, -1f);
  private CameraMovementScript camScript;

  //arena setup parameters
  public int walkEnemyAmt;
  public int jumpEnemyAmt;
  public int rangedEnemyAmt;
  public int spikeTrapAmt;
  public double boulderFreq;

  // Player stats that persist through levels
  public int playerLevel = 1;
  public int playerHealth = 100;
  public int playerMaxHealth = 101;
  public int playerAtkDamage = 3;
  public float playerAtkSpeed = 0.1f;
  public float playerMoveSpeed = 3f;

  //scores
  private int totalMobsKilled = 0;
  private float pointScore = 0;
  private float timeTaken = 0f;
  //path taken

//     public float expMultiplier = 1.8f;
//     public int playerExp = 0;
//     public int playerExpToNext = 10;
//     public string currentRoom;
//     //private bool doingSetup = true;

  //Awake is always called before any Start functions
  void Awake()
  {
    seed = "1111111111111111";
    //Check if instance already exists
    if (instance == null)
    {
      //if not, set instance to this
      instance = this;
    }
    //If instance already exists and it's not this:
    else if (instance != this)
    {
      //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
      Destroy(instance);
    }
    //Sets this to not be destroyed when reloading scene
    DontDestroyOnLoad(instance);
    //Get a component reference to the attached BoardManager script
    newArena();
    //Call the InitGame function to initialize the first level
    InitGame();
  }

  public void newArena()
  {
    //if old arenaManager exists destroy it
    if (arenaManagerObj)
    {
      UnityEngine.Object.Destroy(arenaManagerObj);
    }
    //spawn new arena
    arenaManagerObj = Instantiate(arenaManagerPrefab, arenaManagerTransform, Quaternion.identity);//GameObject.Find("ArenaManager").GetComponent<ArenaManager>();
    arenaManagerObj.transform.parent = this.transform;
    arenaManager = arenaManagerObj.GetComponent<ArenaManager>();
  }

/*
  //this is called only once, and the parameter tells it to be called only after the scene was loaded
  //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
  static public void CallbackInitialization()
  {
    //register the callback to be called everytime the scene is loaded
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
  {
    instance.gameLevel++;
    instance.InitGame();
  }
*/

  //Initializes the game for each level.
  void InitGame()
  {
    //Call the SetupScene function of the ArenaManager script, pass it current level number.
    //arenaManager.SetupScene(floorLevel);
  }

//     public void GameOver()
//     {
//         enabled = false;
//     }

  //Update is called every frame.
  void Update()
  {

  }
}
