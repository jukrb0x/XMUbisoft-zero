using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
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
    private Sentence currentSentence;
    private bool isTyping;
    private bool isDialogRunning;

    [SerializeField] private Dialogue startingDialogue;

    protected override void Awake()
    {
        base.Awake();
        // init the dialogs
        sentences = new Queue<Sentence>();
        dialogueSentence = HUDDialogue.GetComponentInChildren<TextMeshProUGUI>();
        dialogueAvatar = HUDDialogue.transform.Find("Avatar").GetComponent<Image>();
    }

    private void Start()
    {
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
        // TODO: free game time here
        // LevelManager.Instance.CanPlayerMove = true;
        HUDDialogue.SetActive(false);
    }


    public void StartDialogue(Dialogue dialogue)
    {
        isDialogRunning = true;
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