using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public CanvasGroup canvasGroup;
   
    public void TriggerDialogue()
    {
        
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
