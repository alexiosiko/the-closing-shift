using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Singleton;
    [SerializeField] TMP_Text text;
    Action onComplete;
    bool allowInput = true;
    string[] texts;
    int chatIndex = 0;
    public bool isAlreadyTalking = false;
    bool isAnimating = false;
	bool freeze = false;
    Coroutine textAnimationRoutine;
	public bool dialogueDelay = false;
	void UnFreeze() => freeze = false;
	public void SetFontSize(int size)
	{
		text.fontSize = size;
	}
    void Awake()
    {
        Singleton = this;
    }
	public void StartNarration(string[] texts)
	{
		StartCoroutine(Narration(texts));
	}
	IEnumerator Narration(string[] texts)
	{
		for (int i = 0; i < texts.Length; i++)
		{
			StartCoroutine(AnimateTextRoutine(texts[i]));
			yield return new WaitForSeconds(5f);
		}
		text.text = ""; // Erase
	}

    // Method for starting a dialogue with multiple texts
    public void StartDialogue(string[] texts, Action onComplete = null, bool keepFreezeWhenDone = false)
    {
        if (isAlreadyTalking || dialogueDelay)
            return;

		this.keepFreezeWhenDone = keepFreezeWhenDone;
		
		dialogueDelay = true;
		freeze = true;
		Invoke(nameof(UnFreeze), 1f); // MAke the ionput freeze so you accidentally skip through dialogue
		

        this.texts = texts;
        this.onComplete = onComplete;
        chatIndex = 0;
        isAlreadyTalking = true;

        StatusManager.Singleton.Freeze(); // Freeze the game or player
        DisplayNextText(); // Display the first text
    }

    // Display the next text or end dialogue if no more text
    private void DisplayNextText()
    {
        if (chatIndex >= texts.Length) // If no more texts are left
        {
            EndDialogue();
            return;
        }

        if (textAnimationRoutine != null)
            StopCoroutine(textAnimationRoutine);

        text.text = ""; // Clear the current text
        isAnimating = true;
        textAnimationRoutine = StartCoroutine(AnimateTextRoutine(texts[chatIndex]));
        chatIndex++;
    }
	bool keepFreezeWhenDone;
    // End the dialogue and reset states
    private void EndDialogue()
    {
        if (textAnimationRoutine != null)
            StopCoroutine(textAnimationRoutine);

        text.text = ""; // Clear the dialogue text
		if (!keepFreezeWhenDone)
        	StatusManager.Singleton.UnFreeze(); // Unfreeze the game or player
        isAlreadyTalking = false; // Set busy to false to prevent restarting
        onComplete?.Invoke(); // Call the callback if it exists
		Invoke(nameof(UnDialogueDelay), 1f);
	}
	void UnDialogueDelay() => dialogueDelay = false;

    // Coroutine to animate text letter by letter
    private IEnumerator AnimateTextRoutine(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            text.text = message.Substring(0, i + 1); // Add the next letter
            yield return new WaitForSeconds(0.05f); // Wait between each letter
        }

        isAnimating = false; // Animation complete
        allowInput = true; // Allow user to proceed
    }

    // Update method to listen for user input
    void Update()
    {
		if (freeze)
			return;
			
        if (isAlreadyTalking && Input.GetMouseButtonDown(0))
        {
            if (isAnimating)
            {
                // If animating, instantly complete the text
                StopCoroutine(textAnimationRoutine);
                text.text = texts[chatIndex - 1]; // Show the full text
                isAnimating = false;
                allowInput = true; // Allow user to proceed
            }
            else if (allowInput)
            {
                // If animation is complete, show the next text
                DisplayNextText();
            }
        }
    }
}
