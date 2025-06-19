using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Playables;
using Unity.VisualScripting;

[RequireComponent(typeof(Collider))]
public class Portrait : MonoBehaviour
{
	[SerializeField] Transform walkTo;
    void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player") {
			GetComponent<Collider>().enabled = false;
			Action();
		}
	}
	void Action()
	{
		StatusManager.Singleton.Freeze();
		
		DialogueManager.Singleton.dialogueDelay = false;
		DialogueManager.Singleton.isAlreadyTalking = false;
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Oh what's this?"
		}, () => {
			StartCoroutine(Event());
		}, true);
	}
	IEnumerator Event()
	{
		PlayerCamera.Singleton.LookAtTarget(transform, 0, 2f);
		yield return new WaitForSeconds(1.75f);
		Vector3 walkToPosition = walkTo.position;
		walkToPosition.y = PlayerMovement.Singleton.transform.position.y;
		PlayerMovement.Singleton.transform.DOMove(walkToPosition, 2f);
		yield return new WaitForSeconds(2);
		PlayerCamera.Singleton.LookAtTarget(transform, 0, 1f);
		yield return new WaitForSeconds(2);
		DialogueManager.Singleton.StartDialogue(new string[] {
			"This looks like the owner's Grandma that used to own the restaurant,",
			"and it was pass down to him after she died.",
			"Something looks... odd about this portrait.",
			"Almost as if it's alive an looking at me..."
		}, () => {
			walkToPosition.x -= 1;
			PlayerMovement.Singleton.transform.DOMove(walkToPosition, 6f);
			Invoke(nameof(Play), 5.5f);
		}, true);
	}
	void Play()
	{
		Camera.main.DOShakePosition(0.2f);
		GetComponent<PlayableDirector>().Play();
		StatusManager.Singleton.UnFreeze(); 
	}
}
