using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObstacle_Script : MonoBehaviour
{
    // Position where the pipe isn't visible anymore
    const float despawn_posX = -12f;
    // Speed of the pipes
    public float pipeSpeed = 4f;
    // Table with all the sprite of the colored pipes
    public Sprite[] pipeSprites; 

    void Start()
    {
        GeneratePipes();
    }

    void Update()
    {
        // Set the moving speed of the pipe
        transform.Translate( -pipeSpeed * Time.deltaTime , 0, 0 );
        // If the pipe exits the screnn, destroy it
        if (transform.position.x < despawn_posX)
        {
            Destroy(gameObject);
        }
    }

    // Function to generate a pipe
    private void GeneratePipes()
    {
        // Get a random sprite from the sprite table for the top sprite
        Sprite topPipeSprite = pipeSprites[Random.Range(0, pipeSprites.Length)];
        // Get a random sprite from the sprite table for the bottom sprite
        Sprite bottomPipeSprite = pipeSprites[Random.Range(0, pipeSprites.Length)];

        // Search for the top and bottom pipe child in the pipe prefab
        Transform topPipe = transform.Find("Pipe Top"); 
        Transform bottomPipe = transform.Find("Pipe Bottom");

        // Change the sprite of the prefab for the top and bottom pipes
        topPipe.GetComponent<SpriteRenderer>().sprite = topPipeSprite;
        bottomPipe.GetComponent<SpriteRenderer>().sprite = bottomPipeSprite;
    }
}
