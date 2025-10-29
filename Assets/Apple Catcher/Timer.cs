using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    // The tmp that displays the remaining time.
    protected TextMeshPro timer;
    [SerializeField]
    // The tmp that appears when frenzy time is on. Its sole purpose is aesthetic. 
    protected GameObject frenzyText;
    [SerializeField]
    // The game over screen that we will set active when the game ends.
    protected GameObject gameOverScreen;
    [SerializeField]
    // The game object catchboy that enables us to get acces to its script.
    protected GameObject catchBoy;
    [SerializeField]
    // The tab of audio clips which contains all the audio that we'll need.
    protected AudioClip[] sounds;

    // The duration of a game.
    protected float timerDuration = 90f;
    // The spawner script.
    protected Spawner scriptSpawner;
    // The menu script.
    protected Menu scriptMenu;
    // The cathboy script.
    protected Panier scriptPanier;
    // The audio source playing the background music.
    protected AudioSource audioGame;
    // The audio source playing occasionally the frenzy time song.
    protected AudioSource audioFrenzy;
    

    // Start is called before the first frame update
    void Start()
    {
        // Getting the spawner script.
        scriptSpawner = GetComponent<Spawner>();
        // Getting the menu script.
        scriptMenu = GetComponent<Menu>();
        // Getting the catchboy script.
        scriptPanier = catchBoy.GetComponent<Panier>();

        // Creating the audio source for playing the game music.
        audioGame = gameObject.AddComponent<AudioSource>();
        audioGame.clip = sounds[0];
        audioGame.loop = true;
        audioGame.Play();
        // Creating the audio source for playing the frenzy song occasionally.
        audioFrenzy = gameObject.AddComponent<AudioSource>();
        audioFrenzy.clip = sounds[1];

        // Starting the timer coroutine.
        StartCoroutine(TimerCoroutine());
    }

    // Nothing here.
    void Update()
    {
        
    }

    // A simple function for increasing the timer duration.
    public void TimerIncrease(float time)
    {
        timerDuration += time;
    }

    // The coroutine that manage the timer.
    IEnumerator TimerCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < timerDuration)
        {
            elapsedTime += Time.deltaTime;

            // Just calculating how much time's left.
            float timeRemaining = timerDuration - elapsedTime;

            // If 10 seconds remaining, the timer will go red.
            if (timeRemaining < 10f){timer.color = Color.red;}

            if (timeRemaining >= 0f)
            {
                // Converting in minutes and seconds in order to display it correctly on the timer.
                int minutes = Mathf.FloorToInt(timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
                // Shows the time remaining to the player.
                timer.SetText(string.Format("{0}:{1:00} left", minutes, seconds));
            }
            else
            {
                timer.SetText("0:00 left");
            }

            yield return new WaitForEndOfFrame();
        }

        // When time's up, start the game over coroutine.
        timer.SetText("Times up");
        StartCoroutine(GameOver());
    }

    // The game over coroutine.
    IEnumerator GameOver(){
        // Deactivate frenzy time text.

        frenzyText.SetActive(false);
        // Deactivate the spawn of apples and deactive the abbility to pause the game.
        scriptSpawner.enabled = false;
        scriptMenu.enabled = false;

        // Completely shuts down the two audio source.
        audioGame.clip = null;
        audioFrenzy.clip = null;
        
        // Plays the game over whistle and waits it to finish.
        audioGame.PlayOneShot(sounds[2]);
        yield return new WaitForSecondsRealtime(5.0f);

        // Now deactivate the ability to move.
        scriptPanier.enabled = false;

        // Shows the game over screen to the player.
        gameOverScreen.SetActive(true);
        // Plays the game over sound.
        audioGame.PlayOneShot(sounds[3]);
    }

    // A simple function that allows us to start the frenzy time.
    public void StartFrenzy(){
        StartCoroutine(Frenzy());
    }

    // The frenzy time coroutine.
    IEnumerator Frenzy(){
        // Stops the game music.
        audioGame.Pause();
        // Plays the frenzy song.
        audioFrenzy.Play();
        // Activate the frenzy time.
        scriptSpawner.setFrenzy(true);
        // Activate the frenzy text.
        frenzyText.SetActive(true);
        // Wait until the frenzy time's over.
        yield return new WaitForSeconds(20.0f);
        // Deactivate frenzy time.
        scriptSpawner.setFrenzy(false);
        // Deactivate the frenzy text.
        frenzyText.SetActive(false);
        // Stops the frenzy song.
        audioFrenzy.Stop();
        // Plays the game music.
        audioGame.Play();
    }
}