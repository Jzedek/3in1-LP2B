using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    // Variable to count the number of rounds
    public int start_game = 0;
    // Number of lives left
    public int lives = 3;
    // The number of bricks currently in the game (if a brick takes 2 hit to break, it will count for 2 in this variable)
    public int number_of_bricks = 0;
    // Variable to count how many times a partern of bricks has shown (+1 every 2 rounds)
    private int countPatern = 0;
    // Bool to tell if the ball is moving or not
    public bool throwBB = false;
    // Bool to tell if the game is finished or not
    public bool End = false;

    // References to different scripts 
    public ball ref_ball;
    public Paddle ref_Paddle;
    public brique ref_brique;

    // GameObject used in the script
    [SerializeField]
    protected GameObject prefab_brick;
    [SerializeField]
    protected GameObject ball;
    [SerializeField]
    protected GameObject gameOverScreen;

    // Table of all the different TMPro used
    public TextMeshProUGUI[] textToPrintTo;
    // Table of all the different AudioSource used
    public AudioSource[] audioSource;
    

    void Start()
    {
        audioSource[0].Play();
        End = false;

        // Set a message to the starting text
        textToPrintTo[1].SetText("Press SPACE to start");
    }


    void Update()
    {
        // To generate a game when space is pressed and there is no bricks in the game
        if(Input.GetKeyDown(KeyCode.Space) && number_of_bricks == 0)
        {
            // Set the starting text to show nothing
            textToPrintTo[1].SetText("");
            // Spawn our pattern of bricks
            spawner();
            // Set throwBB true to lauch the ball
            throwBB = true;
            // Add 1 to the number of rounds
            start_game +=1;
            // Set the level text to the number of rounds
            textToPrintTo[2].SetText(""+ start_game);
        }

        // If the bool throwBB is true then launch the ball and set the bool to false
        if(throwBB == true)
        {
            StartCoroutine(ballThrowing());
            throwBB = false;
        }

        // quand on a fini un niveau = message + pos de départ
        // When there is no more bricks in the game and this isn't the first round or before
        if(number_of_bricks == 0 && start_game >= 1)
        {
            // If the player attains the last level (18), set the starting text as below 
            if(start_game == 18){textToPrintTo[1].SetText("You finished level "+start_game+"\nLast Level !\nPress SPACE TO CONTINUE");}
            // Else set the starting text as below
            else{textToPrintTo[1].SetText("You finished level "+start_game+"\nPress SPACE TO CONTINUE");}
            // Put the ball at it's starting position
            ref_ball.ballStartPosition();
            // Put the paddle at it's starting position
            ref_Paddle.paddleStartPosition();
        }

        // If the ball goes out of the screen from below, the player lose a life, the life text is updated
        // If the player has no life left, game over
        if (ball.transform.position.y < ref_ball.BAS_ECRAN)
        {
            lives--;
            textToPrintTo[0].SetText("Lives : " + lives);
            if (lives <= 0){EndGame();}
        }

        // If the player finish all levels, game over
        if (start_game > 18){EndGame();}

        // If the player catches a Life up item, 1 life up and update the life text
        if(ref_Paddle.scoreL == true)
        {
            lives+=1;
            textToPrintTo[0].SetText("Lives : " + lives);
            ref_Paddle.scoreL = false;
        }

        // Cheat code to gain a life
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            lives += 1;
            textToPrintTo[0].SetText("Lives : " + lives);
        }

        // Cheat code to skip a patern of level (2 by 2)
        if(Input.GetKeyDown(KeyCode.A) && number_of_bricks == 0)
        {
            countPatern+=1;
            start_game+=2;
        }
    }

    // Function to spawn a level
    public void spawner()
    {
        // Variables to set the parameters of the generation
        int rowCount = 5;
        float startX = -5.5f;
        float startY = 2.35f;
        float brickWidth = 1f;
        float brickHeight = 0.5f;

        // 1st patern when the number of rounds is even
        if (start_game % 2 == 0)
        {
            ref_brique.hitCounter = countPatern;
            for (int row = 0; row < rowCount; row++)
            {
                for (float x = startX; x <= 5.5; x += brickWidth)
                {
                    GameObject newBrick = Instantiate(prefab_brick);
                    newBrick.transform.position = new Vector2(x,startY - row * brickHeight);
                    number_of_bricks = number_of_bricks + 1 + countPatern;
                }
            }
        }

        // 2nd patern when the number of rounds is odd
        else if(start_game % 2 != 0)
        {
            ref_brique.hitCounter = countPatern;
            for (int row = 0; row < rowCount; row++)
            {
                bool isOffsetRow = row % 2 != 0;
                float rowStartX = isOffsetRow ? startX + brickWidth : startX;
                
                for (float x = rowStartX; x <= 5.5; x += brickWidth*2)
                {
                    GameObject newBrick = Instantiate(prefab_brick);
                    newBrick.transform.position = new Vector2(x,startY - row * (brickHeight));
                    number_of_bricks = number_of_bricks + 1 + countPatern;
                }
            }
            countPatern++;
        }
    }

    // Function to report when a brick is touch
    public void ReportBrickDeath()
    {
        number_of_bricks -= 1;
        if(number_of_bricks == 0){audioSource[2].Play();}
    }

    // Function of game over
    protected void EndGame()
    {
        // Play game over sound
        audioSource[1].Play();
        // Set the bool end to true
        End = true;
        // Put the balla and the paddle at their starting position
        ref_ball.ballStartPosition();
        ref_Paddle.paddleStartPosition();
        
        // Show the game over screen
        gameOverScreen.SetActive(true);

        // Update the final score text
        textToPrintTo[3].SetText("Score : "+ref_ball.score);
    }

    // Coroutine to wait befort throwning the ball at the start of a round
    IEnumerator ballThrowing()
    {
        yield return new WaitForSeconds(2f);
        ref_ball.setVelocity();
    }
}