using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private Character playableCharacter;
    // [SerializeField] private Transform spawnPosition;
    public AudioEnum audioEnum;
    private GameObject pauseMenu;
    private bool isPaused;
    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";
    private readonly string WEAPON_AMMO_MAX_SAVELOAD = "WeaponAmmoMax_";
    public float delayTime = 0.5f;
    private AudioSource _audioSource;
    private AudioSetting bgmSetting;

    private void Start()
    {
        Invoke("PlayAudio", delayTime);

        isPaused = false;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + "Weapon_AK47", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + "Weapon_ShotGun", 30);
        PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + "Weapon_AK47", 180);
        PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + "Weapon_ShotGun", 180);
        bgmSetting = GameObject.Find("BGM").GetComponent<AudioSetting>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }

    private void PlayAudio()
    {
        _audioSource = AudioManager.Instance.Play(audioEnum);
    }
    
    

    public void ChangeBGMVolume(float sliderValue)
    {
        bgmSetting.SetVolume(sliderValue);
    }

    public float GetBGMVolume()
    {
        // TODO BUG
        return bgmSetting.GetVolume();
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