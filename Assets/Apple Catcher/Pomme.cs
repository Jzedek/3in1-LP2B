using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The script which dictates the behavior of a collectible.
public class Pomme : MonoBehaviour
{
    // A const which indicates where is the bottom of the screen.
    const float BAS_ECRAN = -7.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the collectible drops too low, it's destroy.
        if ( transform.position.y < BAS_ECRAN)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collectible hit the player, it's destroy.
        if ( collision.gameObject.tag == "Player" )
        {
            Destroy(gameObject);
        }
    }
}