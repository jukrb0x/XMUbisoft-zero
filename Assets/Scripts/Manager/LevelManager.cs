using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private Character playableCharacter;
    // [SerializeField] private Transform spawnPosition;
    public AudioEnum audioEnum;
    private AudioSource _audioSource;

    private void Start()
    {
        // Invoke("Audios", 2);
        Audios();
    }

    private void Audios()
    {
        _audioSource = AudioManager.Instance.Play(audioEnum);
        
    }

    // TODO: remove revive
    // Update is called once per frame
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         ReviveCharacter();
    //     }
    // }
    //
    // private void ReviveCharacter()
    // {
    //     if (playableCharacter.GetComponent<PlayerHealth>().HealthPoint <= 0)
    //     {
    //         playableCharacter.GetComponent<PlayerHealth>().Revive();
    //         playableCharacter.transform.position = spawnPosition.position;
    //     }
    // }

}
