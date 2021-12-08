using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private Transform spawnPosition;
    public AudioEnum audioEnum;
    private GameObject player;
    private GameObject pauseMenu;
    private GameObject godMenu;
    private bool isPaused;
    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";
    private readonly string WEAPON_AMMO_MAX_SAVELOAD = "WeaponAmmoMax_";
    public float delayTime = 0.5f;
    public bool isDialogueRunning;
    public bool canMove;
    public bool canShoot;
    public bool isGodModeMenuToggled;
    public bool isGodMode;

    private AudioSource _audioSource;
    // private AudioSetting bgmSetting;

    private void Awake()
    {
        player = GameObject.Find("Player");
        // init Pause Menu
        // PauseMenu has to be active before the game started.
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        godMenu = GameObject.Find("GodModeMenu");
        godMenu.SetActive(false);
        // Reset Level States at the end of Awake
        ResetLevel();
        isGodModeMenuToggled = false;
    }

    private void Start()
    {
        Invoke("PlayAudio", delayTime);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + "Weapon_AK47", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + "Weapon_ShotGun", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + "Weapon_AK47", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + "Weapon_ShotGun", 60);
        // bgmSetting = GameObject.Find("BGM").GetComponent<AudioSetting>();
    }


    private void Update()
    {
        // pause the game and invoke the pause menu
        if (!CanPause()) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            InvertPauseState();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleGodModeMenu();
        }
    }

    private bool CanPause()
    {
        if (player.GetComponent<BaseHealth>().IsDead) return false;
        return !isDialogueRunning;
    }

    public void ToggleGodModeMenu()
    {
        godMenu.SetActive(!isGodModeMenuToggled);
        if (!isGodModeMenuToggled)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        isGodModeMenuToggled = !isGodModeMenuToggled;
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

    public void PauseGame(int timeScale = 0)
    {
        Time.timeScale = timeScale;
        InvertPlayerStates();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        InvertPlayerStates();
    }

    public void ResetLevel()
    {
        isPaused = false;
        Time.timeScale = 1;
        ResetPlayerStates();
    }

    public void ResetPlayerStates()
    {
        canMove = canShoot = true;
    }

    public void InvertPlayerStates()
    {
        canMove = !canMove;
        canShoot = !canShoot;
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