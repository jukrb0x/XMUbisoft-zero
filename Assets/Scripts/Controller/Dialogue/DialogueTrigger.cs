using System;
using Controller;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        DialogueController.Instance.StartDialogue(dialogue);
        // destroy instance after used
        Destroy(gameObject);
    }
}