using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class ChangeSceneManager : MonoBehaviour
{
    public AudioSource audiosource;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void playAppleCatcher()
    {
        StartCoroutine(CoAppleCatcher());
    }

    public void playCasseBrique()
    {
        StartCoroutine(CoCasseBrick());
    }

    public void playFurapiBird()
    {
        StartCoroutine(CoFurapiBird());
        SceneManager.LoadScene("FurapiBird");
    }

    IEnumerator CoAppleCatcher()
    {
        audiosource.Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("AppleCatcher");
    }
    IEnumerator CoCasseBrick()
    {
        audiosource.Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("CasseBrick");
    }
    IEnumerator CoFurapiBird()
    {
        audiosource.Play();
        yield return new WaitForSeconds(1.5f);
    }

    public void OpenFile()
    {
        string filePath = "Assets/Resources/rules.txt";
        string fileContents = string.Empty;

        if (File.Exists(filePath))
        {
            fileContents = File.ReadAllText(filePath);
            Debug.Log("Contenu du fichier : " + fileContents);
        }
        else
        {
            Debug.LogError("Le fichier rules.txt n'existe pas.");
        }
    }
}