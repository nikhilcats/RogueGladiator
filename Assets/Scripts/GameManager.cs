using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;        //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;                        //Store a reference to our BoardManager which will set up the level.
    private int gameLevel = 0;                                    //Current level number, expressed in game as "Day 1".
    [HideInInspector] public bool playersTurn = true;

    // Player stats that persist through levels
    public int playerLevel = 1;
    public int playerHealth = 5;
    public int playerMaxHealth = 5;
    public int playerAtkDamage = 3;
    public float playerAtkSpeed = 0.1f;
    public float playerMoveSpeed = 3f;
    
    public float expMultiplier = 1.8f;
    public int playerExp = 0;
    public int playerExpToNext = 10;

    //private bool doingSetup = true;


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //this is called only once, and the paramter tell it to be called only after the scene was loaded
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

    //Initializes the game for each level.
    void InitGame()
    {
        // doingSetup = true;
        // //Call the SetupScene function of the BoardManager script, pass it current level number.
        // doingSetup = false;
        boardScript.SetupScene(gameLevel);

    }



    public void GameOver()
    {
        enabled = false;
    }


    //Update is called every frame.
    void Update()
    {

    }
}