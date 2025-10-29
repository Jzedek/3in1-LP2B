using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud1 : MonoBehaviour
{
    // Speed of this cloud
    protected float SPEED = 0.35f;
    // Position where the cloud isn't in the sceen anymore
    const float despawn_posX = -13f;

    void Start()
    {
        
    }

    void Update()
    {
        // Set the moving speed of the cloud
        transform.Translate( -SPEED * Time.deltaTime , 0, 0 );
        // If the cloud exits the screnn, respawn it at the other side of the screen
        if (transform.position.x < despawn_posX)
        {
            transform.position = new Vector2(13,3.548422f);
        }
    }
}
