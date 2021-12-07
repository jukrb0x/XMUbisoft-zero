using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public GameObject BGM;
    public Slider sd;
    private AudioSource audioSource;
    void Awake()
    {
         audioSource = BGM.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Con_sound()
    {

        asound.volume = sd.value;
        

    }
}
