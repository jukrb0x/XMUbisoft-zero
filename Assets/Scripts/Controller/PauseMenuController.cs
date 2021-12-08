using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    private LevelManager levelManager;
    private Slider BGMSlider;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        BGMSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        BGMSlider.value = levelManager.GetBGMVolume(); // this should works fine
    }

    public void ContinueGame()
    {
        levelManager.InvertPauseState();
    }

    // Back to MainMenu
    public void QuitGame()
    {
        // double check
        levelManager.ResetLevel();
        SceneManager.LoadScene(0);
        
    }

    public void BGMSliderChange()
    {
        // TODO BGM slider change
        levelManager.ChangeBGMVolume(BGMSlider.value);
    }
}