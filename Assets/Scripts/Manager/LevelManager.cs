using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private Character playableCharacter;
    // [SerializeField] private Transform spawnPosition;
    public AudioEnum audioEnum;
    public float delayTime = 0.5f;
    private AudioSource _audioSource;
    private GameObject pauseMenu;
    private bool isPaused;

    private void Start()
    {
        Invoke("Audios", delayTime);

        isPaused = false;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
        
    }

    private void Audios()
    {
        _audioSource = AudioManager.Instance.Play(audioEnum);
        
    }

    // TODO: remove revive
    // TODO： useless code
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