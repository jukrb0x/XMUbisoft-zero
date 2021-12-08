using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    private AudioSource _audioSource;
    public int _status = 1;
    [SerializeField] private float volumes = 1;
    [SerializeField] private bool isBGM = false;
    //private Slider slider;
    [SerializeField] private Slider _slider;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        //slider = GameObject.Find("BGMSlider").GetComponent<Slider>();
    }


    private void Update()
    {
        SetVolume(_slider.value);
        if (!_audioSource.isPlaying && isBGM && _status == 1)
        {
            _audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return _audioSource.volume;
    }

    private void PlayClip(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private void Resume()
    {
        if (_status != 0) return;
        _audioSource.Play();
        _status = 1;
        Time.timeScale = 1;
    }

    private void Pause()
    {
        _audioSource.Pause();
        _status = 0;
        Time.timeScale = 0;
    }
}