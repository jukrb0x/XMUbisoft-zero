using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character playableCharacter;
    [SerializeField] private Transform spawnPosition;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReviveCharacter();
        }
    }

    private void ReviveCharacter()
    {
        if (playableCharacter.GetComponent<Health>().Hp <= 0)
        {
            playableCharacter.GetComponent<Health>().Revive();
            playableCharacter.transform.position = spawnPosition.position;
        }
    }

}
