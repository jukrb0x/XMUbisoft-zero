using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public GameObject BGM;
    public Slider slider;
    private AudioSource audioSource;
    void Awake()
    {
         audioSource = BGM.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SliderVolControl()
    {

        audioSource.volume = slider.value;
        

    }
}
