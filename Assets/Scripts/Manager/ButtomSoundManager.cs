using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomSoundManager : MonoBehaviour
{
    public void ButtonSoundPlay()
    {
        AudioManager.Instance.Play(AudioEnum.Buttom);
    }
}
