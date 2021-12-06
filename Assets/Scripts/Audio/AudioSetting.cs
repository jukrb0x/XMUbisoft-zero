using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    private AudioSource _audioSource;
    public int _status = 1;
    [SerializeField] private float volumes = 1;
    [SerializeField] private bool isBGM = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        SetVolume(volumes);
        Test();
        if (!_audioSource.isPlaying && isBGM && _status == 1)
        {
            _audioSource.Play();
        }
    }
    
    private void SetVolume(float volume)
    {
        _audioSource.volume = volume;
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
    
    
    //TODO: Delete This.
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            Resume();
        }
        
    }
}
