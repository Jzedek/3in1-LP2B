using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brique : MonoBehaviour
{
    // GameObject used in the scirpt
    [SerializeField]
    protected GameObject prefab_pieces;
    [SerializeField]
    protected GameObject prefab_Life;

    // Probability for the coin and the life up item to spawn (both 5%)
    protected float pieceSpawnProbability = 0.05f; 
    protected float LifeSpawnProbability = 0.95f;

    // Table containing all the colors that a brick can take depending on the number of hits it can take
    private Color[] colorOptions = { Color.red, Color.yellow, Color.green , Color.cyan, Color.blue, Color.yellow, Color.magenta, Color.black};
    // Bool to tell if a brick will break or not when you hit it
    protected bool isIndestructible = true;
    // Variable counting how many times a brick must be hit to break
    public int hitCounter;

    void Start()
    {
        // Change the brick color depending of how many hot it can take
        Color grayColor = Color.Lerp(Color.white, Color.gray, hitCounter);
        GetComponent<Renderer>().material.color = grayColor;
        // If the brick breack in only one hit, set the variable isIndestructible to false
        if(hitCounter == 0){isIndestructible = false;}
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If this brick is hit by the ball
        if ( collision.gameObject.tag == "Ball" )
        {
            // If the brick takes more than 1 hit to break
            if(isIndestructible == true)
            {
                // Reduce its hitCounter value by one
                hitCounter--;
                // Change its color
                GetComponent<Renderer>().material.color = colorOptions[hitCounter];
                // If the ball has only 1 more hit to take to be brocken, set the IsIndestructible bool to false
                if(hitCounter == 0){isIndestructible = false;}
            }

            else
            {
                // If the random number is below the pieceSpawnProbability,spawn a piece
                if (Random.Range(0f, 1f) <= pieceSpawnProbability)
                {
                    Instantiate(prefab_pieces, transform.position, Quaternion.identity);
                }

                // Else if the random number is above the LifeSpawnProbability, spawn a life up
                else if (Random.Range(0f, 1f) >= LifeSpawnProbability)
                {
                    Instantiate(prefab_Life, transform.position, Quaternion.identity);
                }
                
                // Then destroy the brick
                Destroy(gameObject);
            }
        }
        
    }
}
