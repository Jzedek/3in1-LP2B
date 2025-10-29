using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountainScript2 : MonoBehaviour
{
    // Set the speed of the mountain
    protected float SPEED = 0.75f;
    // Position where the moutain isn't in the sceen anymore
    const float despawn_posX = -18f;

    void Start()
    {
        
    }

    void Update()
    {
        // Set the moving speed of the cloud
        transform.Translate( -SPEED * Time.deltaTime , 0, 0 );
        // If the moutain exits the screnn, respawn it at the other side of the screen
        if (transform.position.x < despawn_posX)
        {
            transform.position = new Vector2(18,-3);
        }
    }
}
