using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomSoundManager : MonoBehaviour
{
    public void ButtomSoundPlay()
    {
        AudioManager.Instance.Play(AudioEnum.Buttom);
    }
}
