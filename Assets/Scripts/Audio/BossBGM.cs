using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBGM : MonoBehaviour
{

    [SerializeField] private AudioClip audio0;
    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;
    private AudioSource _audioSource;
    private AudioSetting _audioSetting;
    private int _status = 1;
    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSetting = GetComponent<AudioSetting>();
    }

    private void Update()
    {
        _status = _audioSetting._status;
        if (!_audioSource.isPlaying && _status == 1)
        {
            int randomNum = Random.Range(0, 3);
            if (randomNum == 0)
            {
                _audioSource.clip = audio0;
            }
            else if (randomNum == 1)
            {
                _audioSource.clip = audio1;
            }
            else if (randomNum == 2)
            {
                _audioSource.clip = audio2;
            }
        }
    }

}
