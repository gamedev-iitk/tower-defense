using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    
    public Animator animator;
   
    string colorText;
     public Text dialogueText;
    private Queue<string> sentences;
    GameObject upgradeui;
    GameObject gameobject;
    void Start()
    {
        gameobject = GameObject.Find("GameObject");
        sentences = new Queue<string>();
        upgradeui = GameObject.Find("UpgradeUI");
    }

   public void StartDialogue(Dialogue dialogue)
    {
        
        animator.SetBool("IsOpen", true);
        colorText = dialogue.color;
        
        Debug.Log(dialogue.color+"Button Triggered");
      //  sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        /*  StopAllCoroutines();
          StartCoroutine(TypeSentence(dialogue.sentences));*/
        DisplayNextSentence();
    } 
    public void DisplayNextSentence()
    {
        if(sentences.Count== 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
  
    public void EndDialogue()
    {
       
        animator.SetBool("IsOpen", false);
      
    }  
    //following code is for text animation
  /*  IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }*/

    public void upgrade() {
        
        
            upgradeui.GetComponent<UpgradeMenuUISystem>().OnClick(colorText);
        

      
    }
}