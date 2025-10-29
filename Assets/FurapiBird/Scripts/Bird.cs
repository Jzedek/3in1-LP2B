using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Get the GameObject of the game over screen
    [SerializeField]
    protected GameObject gameOverScreen;
    // Get the rigidbody of the bird
    protected Rigidbody2D rb;
    // Get the animator of the bird
    protected Animator birdAnimator;

    // Set the limit of the bottom of the screen
    const float BAS_ECRAN = -5.2f;
    // Set the juming range of the bird
    const float JUMP = 5f;

    // All of our audioSources
    public AudioSource hit;
    public AudioSource endMGS;
    public AudioSource endCOD;
    public AudioSource endUND;
    public AudioSource crow;

    // Reference to the Master script
    public MasterCode ref_Master;
    

    void Start()
    {
        // When a game starts, start the animations and set our bird as kinematic
        birdAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {

        // If space is pressed and the game hasn't start yet
        if(Input.GetKeyDown(KeyCode.Space) && ref_Master.starting == false)
        {
            // Revoke kinematic for the bird
            rb.isKinematic = false;
            // Set starting at true to signify the game has started
            ref_Master.starting = true;
            // Jump one time with the bird
            rb.linearVelocity = new Vector2(0f,JUMP);
            // Set the starting texts to null
            ref_Master.startingText.SetText("");
            ref_Master.underStartingText.SetText("");
            // Play the sound of a crow
            crow.Play();
            // Start the animation of a jump
            birdAnimator.SetTrigger("jump");
        }

        // Else if the player press space and the game has already started
        else if(Input.GetKeyDown(KeyCode.Space) && ref_Master.starting == true)
        {
            // Jump the bird
            rb.linearVelocity = new Vector2(0f,JUMP);
            // Play the sound of a crow
            crow.Play();
            // Start the animation of a jump
            birdAnimator.SetTrigger("jump");
        }

        // If the bird goes below the screen and the game isn't finished
        if (transform.position.y <= BAS_ECRAN && !ref_Master.ending)
        {
            // Set the ending bool to true to trigger the coroutine of game over
            ref_Master.ending = true;
            StartCoroutine(GameOver());
        }

        // If nothing happens do the idle animation
        birdAnimator.SetTrigger("idle");
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // If the bird collides with a pipe
        if(collision.gameObject.tag == "Pipe")    
        {
            // Set the ending bool to true to trigger the coroutine of game over
            ref_Master.ending = true;
            StartCoroutine(GameOver());
        }
    }

    // Coroutine of game over
    private IEnumerator GameOver()
    {
        // Do the death animation for the bird
        birdAnimator.SetTrigger("death");
        // Active the game over screen
        gameOverScreen.SetActive(true);
        // Stop the time
        Time.timeScale = 0f;
        // Play the sound of hit
        hit.Play();

        yield return new WaitForSecondsRealtime(1f);

        // Play the different game over sound depending on the score
        if(ref_Master.counter < 50){endUND.Play();}
        if(ref_Master.counter > 50 && ref_Master.counter < 100){endCOD.Play();}
        if(ref_Master.counter >= 100){endMGS.Play();}
    }
}
