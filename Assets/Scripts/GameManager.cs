using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;        //Allows us to use Lists.
using TMPro;

public class GameManager : MonoBehaviour
{

  public static GameManager instance = null;    //Static instance of GameManager which allows it to be accessed by any other script.
  private ArenaManager arenaManager;            //Store a reference to our ArenaManager which will set up the level.
  private GameObject arenaManagerObj;
  public GameObject arenaManagerPrefab;
  private int seed;
  private Vector3 arenaManagerTransform = new Vector3(4.5f, -0.2f, -1f); //coordinates taken from unity inspector
  private CameraMovementScript camScript;
  private TextMeshProUGUI floorUIText;
  private TextMeshProUGUI scoreUIText;
  private PortalBehavior portal1;
  private PortalBehavior portal2;

  //arena setup parameters
  public int spikeTrapAmt;
  public double boulderFreq;
  public int walkEnemyAmt;
  public int jumpEnemyAmt;
  public int rangedEnemyAmt;
  public string lastPortalChoice;   //which thing was tied to the most recent portal selection
  private string[] portalChoices = {"spikes", "boulders", "enemy1"};//, "enemy2", "enemy3"};

  // Player stats that persist through levels
  public int floorLevel = 1;                   //Current floor number, expressed in game as floor 1
  public int playerLevel = 1;
  public int playerHealth = 100;
  public int playerMaxHealth = 101;
  public int playerAtkDamage = 3;
  public float playerAtkSpeed = 0.1f;
  public float playerMoveSpeed = 4f;

  //scores
  public int totalMobsKilled = 0;
  private int floorsCleared = 0;
  private int pointScore = 0;
  private float timeTaken = 0f;
  private float timeCount = 0f;     //ongoing recording of time elapsed
  //path taken

//     public float expMultiplier = 1.8f;
//     public int playerExp = 0;
//     public int playerExpToNext = 10;
//     public string currentRoom;
//     //private bool doingSetup = true;

  //Awake is always called before any Start functions
  void Awake()
  {
    seed = 1111691111;
    UnityEngine.Random.seed = seed;
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
    NewArena();
    //Call the InitGame function to initialize the first level
    InitGame();
  }

  public void NewArena()
  {
    updateFloorText();
    //if old arenaManager exists destroy it
    if (arenaManagerObj)
    {
      UnityEngine.Object.Destroy(arenaManagerObj);
    }
    //update game manager arena generation parameters
    updateArenaGeneration();
    //spawn new arena
    arenaManagerObj = Instantiate(arenaManagerPrefab, arenaManagerTransform, Quaternion.identity);//GameObject.Find("ArenaManager").GetComponent<ArenaManager>();
    arenaManagerObj.transform.parent = this.transform;
    arenaManager = arenaManagerObj.GetComponent<ArenaManager>();

  }

  //handles modifying values for trap/enemy spawns
  private void updateArenaGeneration()
  {
    if  (lastPortalChoice == "spikes")
    {
      //modification can be tweaked here
      spikeTrapAmt += 2;
    }
    else if (lastPortalChoice == "boulders")
    {
      boulderFreq++;
    }
    else if (lastPortalChoice == "enemy1")
    {
      walkEnemyAmt++;
      //walkEnemyAmtTEMP++;
    }
    else if (lastPortalChoice == "enemy2")
    {
      jumpEnemyAmt++;
      //jumpEnemyAmtTEMP++;
    }
    else if (lastPortalChoice == "enemy3")
    {
      rangedEnemyAmt++;
      //rangedEnemyAmtTEMP++;
    }
  }

  public void assignPortals()
  {
    //set portal choices
    //find portal objects
    portal1 = GameObject.Find("GameManager/ArenaManager(Clone)/Portal1").GetComponent<PortalBehavior>();
    portal2 = GameObject.Find("GameManager/ArenaManager(Clone)/Portal2").GetComponent<PortalBehavior>();
    //set their types
    Debug.Log("last choice " + lastPortalChoice);
    if (floorLevel == 1)
    {
      string p1Result = randomPortal("whatever");
      portal1.portalType = p1Result;
      lastPortalChoice = p1Result;
    }
    else
    {
      portal1.portalType = lastPortalChoice;
    }
    portal2.portalType = randomPortal(lastPortalChoice);
    Debug.Log("portal1 " + portal1.portalType);
    Debug.Log("portal2 " + portal2.portalType);
  }

  private void updateFloorText()
  {
    string floorNum = floorLevel.ToString();
    floorUIText = GameObject.Find("GameManager/UICanvas/FloorText").GetComponent<TextMeshProUGUI>();
    floorUIText.text = "Floor: " + floorNum;
    //set score UI text as well
    scoreUIText = GameObject.Find("GameManager/UICanvas/ScoreText").GetComponent<TextMeshProUGUI>();
  }

  public void setArenaGameStatePortal()
  {
    arenaManager.gameState = "portal";
  }

  private string randomPortal(string existingChoice)
  {
    string choice = existingChoice;
    //while the new choice equals what is given in parameters, keep selecting new choices (so we dont get duplicates)
    while (choice == existingChoice)
    {
      float length = (float)portalChoices.Length;
      float randoNum = UnityEngine.Random.Range(0f, length - 1);
      int index = (int)Math.Round(randoNum);
      choice = portalChoices[index];
    }
    return choice;
  }

  //death
  public void Die()
  {
    //calculate ending stats
    floorsCleared = floorLevel - 1;
    timeTaken = timeCount;
    PlayerPrefs.SetInt("floorsCleared", floorsCleared);
    PlayerPrefs.SetInt("mobsKilled", totalMobsKilled);
    PlayerPrefs.SetInt("points", pointScore);
    PlayerPrefs.SetFloat("timeTaken", timeTaken);
    //play death animation

    //later on should tie the following functions to death animation
    //switch scene to post game
    SceneManager.LoadScene("PostGame");
    //kill current game
    UnityEngine.Object.Destroy(GameObject.Find("GameManager"));
  }

  public void AddPoints(int points)
  {
    pointScore += points;
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
    timeCount += Time.deltaTime;
    scoreUIText.text = "Score: " + pointScore;
  }
}
