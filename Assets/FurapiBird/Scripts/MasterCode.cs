using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MasterCode : MonoBehaviour
{   
    // Pipe prefab reference
    [SerializeField]
    protected GameObject pipePrefab;
    
    // All of our TMPro text
    [SerializeField]
    protected TextMeshProUGUI finalScore; 
    public TextMeshProUGUI startingText;
    public TextMeshProUGUI underStartingText;
    public TextMeshProUGUI score;

    // Time between two spawn of a pipe
    protected float spawnTimer = 1f;
    protected float intervalle = 1.5f;
    // Speed of a pipe
    protected float speed = 4f;
    // Counter of distance fly by the bird
    public float counter = 0;
    // Bool to tell if the game has start or not
    public bool starting;
    // Bool to tell if the game has ended or not
    public bool ending;


    void Start()
    {
        // When starting, set the starting text, start the coroutine to count the distance, and sets ending and starting bools to false to tell that the game hasn't started yet
        startingText.SetText("READY ?");
        underStartingText.SetText("PRESS SAPCE TO START");
        StartCoroutine(CounterUpdate());
        ending = false;
        starting = false;
    }

    void Update()
    {
        // Spawn a pipe in the correct interval of time if the game has started
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0 && starting == true){
            Spawner();
            spawnTimer = intervalle;
        }

        // Cheat code when A is pressed, gain a meter in the counter varaible and update the score
        if(Input.GetKey(KeyCode.A))
        {
            counter++;
            score.SetText(counter+"m");
        }
        
    }

    // Function to cerate a prefab of a pipe
    void Spawner()
    {
        GameObject newPipe = Instantiate(pipePrefab);
        newPipe.transform.position = new Vector2(10f, Random.Range(-3.0f, 3.0f));
    }

    // Coroutine to update and add 1 to the score of the player every seconds
    private IEnumerator CounterUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if(starting == true)
            {
                counter++;
                score.SetText(counter+"m");
                finalScore.SetText("Score: "+counter+"m");
            }
        }
    }
}
