using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavigator : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;


    private void Awake()
    {
       mainMenu.SetActive(true); 
       levelMenu.SetActive(false);
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void EnterLevel(int chosenLevel)
    {
        // Level 1 --> 1
        SceneManager.LoadScene(chosenLevel);
    }
}