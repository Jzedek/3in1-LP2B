using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    const float BAS_ECRAN = -7.0f;

    void Start()
    {
        
    }

    void Update()
    {
        // If the life up item goes out of the screen, destroy it
        if (transform.position.y < BAS_ECRAN)
        {
            Destroy(gameObject);
        }
    }
}
