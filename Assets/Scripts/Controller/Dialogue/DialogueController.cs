using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : Singleton<DialogueController>
{
    private Character player;
    [SerializeField] private GameObject HUDDialogue;
    private Image dialogueAvatar;
    private TextMeshProUGUI dialogueSentence;
    private Queue<Sentence> sentences;

    protected override void Awake()
    {
        base.Awake();
		// if(FindObjectOfType<Character>().CharacterType == Character.CharacterTypes.Player);
        dialogueAvatar = HUDDialogue.transform.Find("Avatar").GetComponent<Image>();
        // sentences =
    }

    private void Update()
    {
        // continue to next sentence
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            print("OK");
        }
    }
}