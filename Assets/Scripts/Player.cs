using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager

    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class Player : MovingObject
    {
        public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
        // These have to update both ways ^_^
        // Reevaluate what variables Player actually needs access to.
        // private int maxHealth; // Doesn't need access. Game manager is going to keep track of this and it won't be used to calculate anything.
        private int health; // Needs access. When this value reaches zero, must signal game over.
        private float atkSpeed; // Most likely needs access. Can pass it to animator to set the speed of the animation, and set the delay between when the next attack is allowed to occur?
        private float moveSpeed; // But now I'm questioning the last comment... maybe this can all just be coordinated between the GameManager and animation assets? lol
        private int exp;
        private int expToNext;

        private Animator animator; //Used to store a reference to the Player's animator component.


        //Start overrides the Start function of MovingObject
        protected override void Start ()
        {
            //Get a component reference to the Player's animator component
            animator = GetComponent<Animator>();

            health = GameManager.instance.playerHealth;
            exp = GameManager.instance.playerExp;
            expToNext = GameManager.instance.playerExpToNext;

            //Call the Start function of the MovingObject base class.
            base.Start ();
        }


        //This function is called when the behaviour becomes disabled or inactive.
        private void OnDisable ()
        {
            //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
            GameManager.instance.playerHealth = health;
            GameManager.instance.playerExp = exp;
        }


        private void Update ()
        {
            //If it's not the player's turn, exit the function.
            if(!GameManager.instance.playersTurn) return;

            int horizontal = 0;      //Used to store the horizontal move direction.
            int vertical = 0;        //Used to store the vertical move direction.


            //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int) (Input.GetAxisRaw ("Horizontal"));

            //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
            vertical = (int) (Input.GetAxisRaw ("Vertical"));

            //Check if moving horizontally, if so set vertical to zero.
            if(horizontal != 0)
            {
                vertical = 0;
            }

            //Check if we have a non-zero value for horizontal or vertical
            if(horizontal != 0 || vertical != 0)
            {
                //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it) (?????????????)
                //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
                AttemptMove<Wall> (horizontal, vertical);
            }
        }

        //AttemptMove overrides the AttemptMove function in the base class MovingObject
        //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
        protected override void AttemptMove <T> (int xDir, int yDir)
        {
            //Every time player moves, subtract from food points total.
            //food--;

            //Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
            base.AttemptMove <T> (xDir, yDir);

            //Hit allows us to reference the result of the Linecast done in Move.
            RaycastHit2D hit;

            //If Move returns true, meaning Player was able to move into an empty space.
            if (Move (xDir, yDir, out hit)) 
            {
                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
            }

            //Since the player has moved and lost food points, check if the game has ended.
            CheckIfGameOver ();

            //Set the playersTurn boolean of GameManager to false now that players turn is over.
            GameManager.instance.playersTurn = false;
        }


        //OnCantMove overrides the abstract function OnCantMove in MovingObject.
        //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
        protected override void OnCantMove <T> (T component)
        {
            // //Set hitWall to equal the component passed in as a parameter.
            // Wall hitWall = component as Wall;

            // //Call the DamageWall function of the Wall we are hitting.
            // hitWall.DamageWall (wallDamage);

            // //Set the attack trigger of the player's animation controller in order to play the player's attack animation.
            // animator.SetTrigger ("playerChop");
        }


        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D (Collider2D other)
        {
            // //Check if the tag of the trigger collided with is Exit.
            // if(other.tag == "Exit")
            // {
            //     //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            //     Invoke ("Restart", restartLevelDelay);

            //     //Disable the player object since level is over.
            //     enabled = false;
            // }

            // //Check if the tag of the trigger collided with is Food.
            // else if(other.tag == "Food")
            // {
            //     //Add pointsPerFood to the players current food total.
            //     food += pointsPerFood;

            //     //Disable the food object the player collided with.
            //     other.gameObject.SetActive (false);
            // }

            // //Check if the tag of the trigger collided with is Soda.
            // else if(other.tag == "Soda")
            // {
            //     //Add pointsPerSoda to players food points total
            //     food += pointsPerSoda;


            //     //Disable the soda object the player collided with.
            //     other.gameObject.SetActive (false);
            // }
        }


        //Restart reloads the scene when called.
        private void Restart ()
        {
            //Load the last scene loaded, in this case Main, the only scene in the game. (TODO: CHANGE)
            SceneManager.LoadScene (0);
        }


        public void LoseHealth (int loss)
        {
            //Set the trigger for the player animator to transition to the playerHit animation.
            animator.SetTrigger ("playerHit");

            //Subtract lost food points from the players total.
            health -= loss;

            //Check to see if game has ended.
            CheckIfGameOver ();
        }

        public void GainExp (int gain)
        {
            // TODO: Play sound
            exp += gain;
            if (exp > expToNext) {
                GameManager.instance.playerLevel += 1;
                GameManager.instance.playerExp = exp - expToNext;
                GameManager.instance.playerExpToNext = (int)((float)expToNext * GameManager.instance.expMultiplier);
            }
        }


        //CheckIfGameOver checks if the player is out of food points and if so, ends the game.
        private void CheckIfGameOver ()
        {
            //Check if food point total is less than or equal to zero.
            if (health <= 0) 
            {

                //Call the GameOver function of GameManager.
                GameManager.instance.GameOver ();
            }
        }
    }