using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        BGMSlider.value = levelManager.GetBGMVolume();
        print(levelManager.GetBGMVolume());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BGMSliderChange()
    {
        levelManager.ChangeBGMVolume(BGMSlider.value);
    }
}