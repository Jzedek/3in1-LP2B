using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The script which will manage the spawning of collectibles.
public class Spawner : MonoBehaviour
{
    // The tab which contains all the prefabs that we need to spawn.
    [SerializeField]
    protected GameObject[] prefabs;
    // The timer.
    protected float spawnTimer = 1f;
    // The intervalle which separate two apple spawn.
    protected float intervalle = 1f;
    // The bool that says if frenzy time is activated or not.
    protected bool isFrenzy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // A simple function that enables or disables frenzy time and changes the intervalle.
    public void setFrenzy(bool state)
    {
        if (state)
        {
            isFrenzy = state;
            intervalle = 0.5f;
        }
        else
        {
            isFrenzy = state;
            intervalle = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0){
            spawner();
            spawnTimer = intervalle;
        }
    }
    
    // The main function of this script. It spawns a collectible randomly on the scene.
    protected void spawner(){
        // The rand that contains our random float between 0 to 100.
        float rand = Random.Range(0.0f,100.0f);
        // If frenzy time is activated, only golden apple will spawn.
        if (isFrenzy)
        {
            GameObject newGoldenApple = Instantiate(prefabs[2]);
            newGoldenApple.transform.position = new Vector2(Random.Range(-8.0f, 8.0f),7.0f);
        }
        // 10% chance that a coin spawns.
        else if (rand <= 10)
        {
            GameObject newCoin = Instantiate(prefabs[1]);
            newCoin.transform.position = new Vector2(Random.Range(-8.0f, 8.0f),7.0f);
        }
        // 10% chance that a rotten apple spawns.
        else if (rand > 10 && rand <= 20)
        {
            GameObject newRottenApple = Instantiate(prefabs[3]);
            newRottenApple.transform.position = new Vector2(Random.Range(-8.0f, 8.0f),7.0f);
        }
        // 3% chance that a rainbow apple spawns.
        else if(rand > 20 && rand <= 23)
        {
            GameObject newRainbowApple = Instantiate(prefabs[4]);
            newRainbowApple.transform.position = new Vector2(Random.Range(-8.0f, 8.0f),7.0f);
        }
        // 1% chance that a stopwatch spawns.
        else if (rand > 23 && rand < 25)
        {
            GameObject newStopwatch = Instantiate(prefabs[5]);
            newStopwatch.transform.position = new Vector2(Random.Range(-8.0f, 8.0f),7.0f);
        }
        // 76% chance that a normal apple spawns.
        else
        {
            GameObject newApple = Instantiate(prefabs[0]);
            newApple.transform.position = new Vector2(Random.Range(-8.0f, 8.0f),7.0f);
        }
    }
}