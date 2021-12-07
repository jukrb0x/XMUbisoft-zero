using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : Singleton<DialogueController>
{
    private CharacterComponents playerComponents;
    [SerializeField] private GameObject HUDDialogue;
    private Image dialogueAvatar;
    private TextMeshProUGUI dialogueSentence;
    private Queue<Sentence> sentences;
    private Sentence currentSentence;
    private bool isTyping;
    private bool isDialogRunning;
    private LevelManager levelManager;

    [SerializeField] private Dialogue startingDialogue;

    protected override void Awake()
    {
        base.Awake();
        // init the dialogs
        sentences = new Queue<Sentence>();
        dialogueSentence = HUDDialogue.GetComponentInChildren<TextMeshProUGUI>();
        dialogueAvatar = HUDDialogue.transform.Find("Avatar").GetComponent<Image>();
        // get states of player
        playerComponents = GameObject.Find("Player").GetComponent<CharacterComponents>();
        // get Level Manager
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        if (HUDDialogue != null)
        {
            GameObject.Find("Dialogue");
        }

        if (startingDialogue != null)
        {
            StartCoroutine(DelayedCallStartDialogue(startingDialogue, .3f));
        }
    }

    private void Update()
    {
        // continue to display next sentence
        if (!isDialogRunning) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            if (isDialogRunning) PrintNextSentence();
        }
    }

    private IEnumerator TypeSentence(string text)
    {
        dialogueSentence.text = "";
        isTyping = true;
        foreach (char letter in text.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return new WaitForSeconds(0.025F);
        }

        isTyping = false;
    }


    private void CloseDialogue()
    {
        isDialogRunning = false;
        HUDDialogue.SetActive(false);
        levelManager.ResumeGame();
        levelManager.isDialogueRunning = isDialogRunning;
    }


    public void StartDialogue(Dialogue dialogue)
    {
        isDialogRunning = true;
        levelManager.ResetLevel();
        levelManager.PauseGame();
        levelManager.isDialogueRunning = isDialogRunning;
        HUDDialogue.SetActive(true);
        sentences.Clear(); // clear default text
        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // print the first sentence on HUD dialog
        PrintNextSentence();
    }

    private IEnumerator DelayedCallStartDialogue(Dialogue dialogue, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartDialogue(dialogue);
    }

    private void PrintNextSentence()
    {
        if (isTyping)
        {
            dialogueSentence.text = currentSentence.sentence;
            isTyping = false;
            StopAllCoroutines();
            return;
        }

        // close dialog once sentences are all printed
        if (sentences.Count == 0)
        {
            CloseDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        dialogueAvatar.sprite = currentSentence.avatar;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence.sentence));
    }
}