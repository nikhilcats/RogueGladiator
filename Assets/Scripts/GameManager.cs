using UnityEngine;
using System.Collections;
using System.Collections.Generic;        //Allows us to use Lists. 
using Completed;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;                        //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                    //Current level number, expressed in game as "Day 1".
    [HideInInspector] public bool playersTurn = true;


    // Player stats that persist through levels
    public int playerLevel = 1;
    public int playerHealth = 10;
    public int playerMaxHealth = 10;
    public int playerAtkDamage = 3;
    public float playerAtkSpeed = 0.1f;
    public float playerMoveSpeed = 1f;
    
    public float expMultiplier = 1.8f;
    public int playerExp = 0;
    public int playerExpToNext = 10;


    //Awake is always called before any Start functions
    void Awake() // I wish i was awake! t
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

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene(level);

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