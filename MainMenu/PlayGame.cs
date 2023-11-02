using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
   

    public void NextScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GotoSettings()
    {
        SceneManager.LoadScene("Settings");
    }
   
}
