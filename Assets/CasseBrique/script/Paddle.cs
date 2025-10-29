using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Reference to the script Master.cs
    public Master ref_Master;

    const float SPEED = 7.0f;

    // Bool to tell if the player has taken a coin
    public bool scoreC = false;
    // Bool to tell if the player has taken a life up
    public bool scoreL = false;

    public AudioSource bounceOnPad;
    public AudioSource pointWin;

    void Start()
    {
        paddleStartPosition();
    }

    void Update()
    {
        // If the player press the right arrow and there is bricks and the game isn't finished
        if (Input.GetKey(KeyCode.RightArrow) && ref_Master.number_of_bricks != 0 && ref_Master.End != true)
        {
            // If the player try to force the wall the paddle will stop
            if(transform.position.x >= 5.397f){
                transform.Translate(0, 0, 0);
            }
            // Else the paddle move to the right
            else{
                transform.Translate(SPEED * Time.deltaTime, 0, 0);
            }
        }

        // If the player press the left arrow and there is bricks and the game isn't finished
        if (Input.GetKey(KeyCode.LeftArrow) && ref_Master.number_of_bricks != 0 && ref_Master.End != true)
        {
            // If the player try to force the wall the paddle will stop
            if(transform.position.x <= -5.383f){
                transform.Translate(0, 0, 0);
            }
            // Else the paddle move to the left
            else{
                transform.Translate(-SPEED * Time.deltaTime, 0, 0);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If the pad collides with a pieces : 
        // -A win sound will play
        // -The piece will be destroyed
        // -The variable scoreC will be true (we'll use it elsewhere)
        if (other.CompareTag("pieces"))
        {
            pointWin.Play();
            Destroy(other.gameObject);
            scoreC = true;
        }

        // If the pad collides with a life up item : 
        // -A win sound will play
        // -The life up item will be destroyed
        // -The variable scoreL will be true (we'll use it elsewhere)
        if (other.CompareTag("Life"))
        {
            pointWin.Play();
            Destroy(other.gameObject);
            scoreL = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the paddle collides with the ball a sound will play
        if(collision.gameObject.tag == "Ball")
        {
            bounceOnPad.Play();
        }
    }

    // A function to put the paddle at his start position, at the start of a round/game
    public void paddleStartPosition()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.transform.position = new Vector2(0,-4.59f);
    }
}
