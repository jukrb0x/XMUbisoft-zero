using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    // [SerializeField] private Slider BGMSlider;

    void Awake()
    {
        if (levelManager == null)
        {
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }
        // if (BGMSlider == null)
        // {
        //     BGMSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        // }

        // BGMSlider.value = levelManager.GetBGMVolume(); // this should works fine
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
        Cursor.visible = true;
    }

    public void ToggleGodMode()
    {
        levelManager.isGodMode = !levelManager.isGodMode;
    }

    // public void BGMSliderChange()
    // {
    //     // TODO BGM slider change
    //     levelManager.ChangeBGMVolume(BGMSlider.value);
    // }
}