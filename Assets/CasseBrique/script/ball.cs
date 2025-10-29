using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ball : MonoBehaviour
{
    // References to different scripts
    public Master ref_master;
    public Paddle ref_paddle;

    // Variable to follow the score of the game
    public int score = 0;
    // Const to set the speed of the ball
    const float SPPED = 7f;
    // Vector to set the direction in which the ball will go when launched
    protected Vector2 direction = Vector2.up;
    // Varaible to set the limit at the bottom of the screen
    public float BAS_ECRAN = -5.2f;
    // Variable to tell if the ball is moving or not
    protected bool throwB = false;

    // Variables used when the ball is locked between the walls
    public float minCollisionAngle = 15f;
    public float additionalVelocityY = 15f;

    // TMPro with the score
    [SerializeField]
    public TextMeshProUGUI displayedText;
    // AudioSource with the Point Loss, Brick breaking, Bounce on wall and Intro sound
    public AudioSource[] audioSource;

    void Start()
    {
        ballStartPosition();
    }

    void Update()
    {
        // If the ball goes below the screen
        if ( transform.position.y < BAS_ECRAN)
        {
            // If the score is above 500, loose 500 points and update the score text
            if(score-500 > 0)
            {
                score-=500;
                displayedText.SetText("Score : " + score);
            // Else if the score is below 500, set the score to 0 and update the score text
            }else
            {
                score=0;
                displayedText.SetText("Score : " + score);
            }
            throwB = true;
        }

        // If the ball catches a coin gain 100 points and update the score text, the set the value of the bool to false
        if(ref_paddle.scoreC == true)
        {
            score+=100;
            displayedText.SetText("Score : " + score);
            ref_paddle.scoreC = false;
        }

        // If this bool is true, start the coroutine to throw the ball, then set the bool to false
        if(throwB == true)
        {
            StartCoroutine(ballThrowing());
            throwB = false;
        }
    }

    // Ficed Update to adjust constantly the velocity of the ball
    private void FixedUpdate() {
        adjustVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the rigidbody od the ball
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // If the ball colide with a brick
        if (collision.gameObject.tag == "brick")
        {
            // Play a sound
            audioSource[1].Play();
            // Add 50 to the score and update its text
            score+=50;
            displayedText.SetText("Score : " + score);
            // Call the function ReprotBrickDeath
            ref_master.ReportBrickDeath();
        }

        // If the ball collides with the paddle, adjust its direction depending on where on the paddle it hits
        if(collision.gameObject.name == "Paddle")
        {
            float diffX = transform.position.x - collision.gameObject.transform.position.x;
            rb.linearVelocity += new Vector2(diffX * 5, 0);
        }

        // If the ball collides with a wall
        if(collision.gameObject.tag == "wall")
        {
            // Play a sound
            audioSource[2].Play();

            // If the walls are the walls on the left or right (situation where the ball is stuck between these two)
            if(collision.gameObject.name == "Wall 1" || collision.gameObject.name == "Wall 2")
            {
                // If the angle between y of the ball and the ball is to small, adjust the trajectory of the ball
                Vector2 collisionNormal = collision.contacts[0].normal;
                float angle = Vector2.Angle(collisionNormal, rb.linearVelocity);
                if (angle <=  minCollisionAngle)
                {
                    rb.linearVelocity += new Vector2(0f, additionalVelocityY);
                }
            }
        }
    }

    // Function to throw the ball
    public void setVelocity(){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * SPPED;
    }

    // Function to set the ball at its starting position and stop it
    public void ballStartPosition()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.transform.position = new Vector2(0,-3.5f);
        rb.linearVelocity = Vector2.zero;
    }

    // Function to adjust the velocity od the ball
    public void adjustVelocity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 currentVelocity = rb.linearVelocity;
        Vector2 desiredVelocity = currentVelocity.normalized * SPPED; 

        rb.linearVelocity = desiredVelocity;
    }

    // Coroutine to wait before throwning the ball when you loose a life
    IEnumerator ballThrowing()
    {
        audioSource[0].Play();
        ballStartPosition();
        yield return new WaitForSeconds(2f);
        setVelocity();
    }
}
