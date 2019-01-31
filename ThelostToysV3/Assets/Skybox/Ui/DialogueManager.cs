using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    //public bool isInformationScene;
    public float wordRate;
	public Text dialogueText;

	public  Queue<string> sentences;

	// Use this for initialization
	void Start ()
    {
        // sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
       
        sentences = new Queue<string>();
        

        //Debug.Log(sentences);
        sentences.Clear();
        //Debug.Log(sentences);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
        if (sentences.Count == 0)
        {
            FindObjectOfType<fadeSrcipt>().fade_triger("SampleScene");
            return;
        }

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;


            yield return new WaitForSeconds(1 / wordRate);
        }
	}
}
