using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private Transform spawnPosition;
    public AudioEnum audioEnum;
    private GameObject player;
    private GameObject pauseMenu;
    private bool isPaused;
    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";
    private readonly string WEAPON_AMMO_MAX_SAVELOAD = "WeaponAmmoMax_";
    public float delayTime = 0.5f;
    public bool isDialogueRunning;

    private AudioSource _audioSource;
    // private AudioSetting bgmSetting;

    private void Start()
    {
        player = GameObject.Find("Player");
        Invoke("PlayAudio", delayTime);
        // init Pause Menu
        // PauseMenu has to be active before the game started.
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + "Weapon_AK47", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + "Weapon_ShotGun", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + "Weapon_AK47", 180);
        PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + "Weapon_ShotGun", 180);
        // bgmSetting = GameObject.Find("BGM").GetComponent<AudioSetting>();
        // Reset Level States
        ResetLevel();
    }


    private void Update()
    {
        // pause the game and invoke the pause menu
        if (!CanPause()) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            InvertPauseState();
        }
    }

    private bool CanPause()
    {
        if (player.GetComponent<BaseHealth>().IsDead) return false;
        return !isDialogueRunning;
    }

    public void InvertPauseState()
    {
        pauseMenu.SetActive(!isPaused);
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        isPaused = !isPaused;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        player.GetComponent<CharacterComponents>().InvertPlayerStates();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        player.GetComponent<CharacterComponents>().InvertPlayerStates();
    }

    public void ResetLevel()
    {
        isPaused = false;
        Time.timeScale = 1;
        player.GetComponent<CharacterComponents>().ResetPlayerStates();
    }


    private void PlayAudio()
    {
        _audioSource = AudioManager.Instance.Play(audioEnum);
    }


    public void ChangeBGMVolume(float sliderValue)
    {
        // bgmSetting.SetVolume(sliderValue);
    }

    public float GetBGMVolume()
    {
        // TODO BUG
        // return bgmSetting.GetVolume();
        return 1f;
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