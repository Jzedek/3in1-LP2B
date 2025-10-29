using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Panier : MonoBehaviour
{
    // The right limit of the screen.
    protected const float RIGHT_SCREEN = 8.0f;
    // The left limit of the screen.
    protected const float LEFT_SCREEN = -8.0f;
    // The speed of catchboy.
    protected const float SPEED = 10.0f;

    
    [SerializeField]
    // The tmp that will show the score to the player during the game.
    protected TextMeshPro scoreText;
    [SerializeField]
    // The tmp that will show the score to the player inside the game over screen.
    public TextMeshProUGUI finalScoreText;
    [SerializeField]
    // The audio clip when collecting a normal apple.
    protected AudioClip appleCollectSound;
    [SerializeField]
    // The audio clip when collecting a coin.
    protected AudioClip coinCollectSound;
    [SerializeField]
    // The audio clip when collecting a rotten apple.
    protected AudioClip rottenCollectSound;
    [SerializeField]
    // The audio clip when collecting a stopwatch.
    protected AudioClip stopwatchCollectSound;

    [SerializeField]
    // The gamemaster that contains the script Timer that we will need to acces.
    protected GameObject gameMaster;
    
    // The score.
    protected int score = 0;
    // The audio source which will play a different sound in function of the apple we collect.
    protected AudioSource SfxSpeaker;
    // The animator of catchboy.
    protected Animator animator;
    // The timer script that we will need for later.
    protected Timer scriptTimer;


    
    // Start is called before the first frame update
    void Start()
    {
        // Creating an audio source.
        SfxSpeaker = gameObject.AddComponent<AudioSource>();
        // Getting acces to the animator.
        animator = GetComponent<Animator>();
        // Getting acces to the variables and functions inside the scipt Timer.
        scriptTimer = gameMaster.GetComponent<Timer>();
    }

    // Update will manages the movement of catchboy and his animations.
    void Update()
    {
        float speedThisFrame = 0;
        if ( Input.GetKey(KeyCode.RightArrow) )
        {
            // Just so that catchboy can't go beyond the screen limits.
            if (transform.position.x < RIGHT_SCREEN)
            {
                speedThisFrame += SPEED;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Same here.
            if (transform.position.x > LEFT_SCREEN)
            {
                speedThisFrame -= SPEED;
            }
        }
        // We're using float to manage transition between animations.
        if (Time.timeScale != 0){animator.SetFloat("Speed",speedThisFrame);}

        // The movements are made here.
        transform.Translate(speedThisFrame * Time.deltaTime, 0, 0);
    }

    /* This function tracks what types of collectibles catchboy has just collected. It increases or decreases the score
       and start the associated consequences.
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // A golden apple is just like a normal apple but appears exclusively during frenzy time.
        if (collision.gameObject.tag == "Apple" || collision.gameObject.tag == "Golden_apple")
        {
            score++;
            scoreText.SetText("Score :" + score);
            finalScoreText.SetText("Score :" + score);
            SfxSpeaker.PlayOneShot(appleCollectSound);
        }
        // A coin gives 5 points.
        if (collision.gameObject.tag == "Coin")
        {
            score += 5;
            scoreText.SetText("Score :" + score);
            finalScoreText.SetText("Score :" + score);
            SfxSpeaker.PlayOneShot(coinCollectSound);
        }
        // A rotten apple removes 10 points.
        if (collision.gameObject.tag == "Rotten_apple")
        {
            score -= 10;
            scoreText.SetText("Score :" + score);
            finalScoreText.SetText("Score :" + score);
            SfxSpeaker.PlayOneShot(rottenCollectSound);
        }
        // The rainbow apple gives 5 points and starts frenzy time.
        if (collision.gameObject.tag == "Rainbow_apple")
        {
            score += 5;
            scoreText.SetText("Score :" + score);
            finalScoreText.SetText("Score :" + score);
            scriptTimer.StartFrenzy();
        }
        // The stopwatch increases the timer duration by 10 sec.
        if (collision.gameObject.tag == "Stopwatch")
        {
            scriptTimer.TimerIncrease(10f);
            SfxSpeaker.PlayOneShot(stopwatchCollectSound);
        }
    }

    
}