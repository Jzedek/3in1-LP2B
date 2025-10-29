using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    // AudioSource of the music playing in the backgoround
    protected AudioSource audioSource;
    // Reference to the Master script
    public MasterCode ref_Master;

    void Start()
    {
        // When starting, start playing the song
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player loose, stop the song
        if(ref_Master.ending == true)
        {
            audioSource.Stop();
        }
    }
}
