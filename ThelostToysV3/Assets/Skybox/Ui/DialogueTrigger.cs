using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
   // public DialogueManager managerDi;

	public void TriggerDialogue ()
	{
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //managerDi.StartDialogue(dialogue);
	}

    public void next_di()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
