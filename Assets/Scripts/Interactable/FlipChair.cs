using UnityEngine;
using UnityEngine.Playables;

public class FlipChair : Interactable
{
	public override void Action()
	{
		base.Action();
		if (TaskManager.Singleton.pauseTasks) {
			DialogueManager.Singleton.StartDialogue(new string[] {
				"I should reset the breaker first before I do anything else."
			});
			return;
		}
		GetComponent<Collider>().enabled = false;
		
		GetComponent<PlayableDirector>().Play();
		Invoke(nameof(Delay), 1);
	}
	void Delay()
	{
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Nothing drives me more crazy when parents let their kids run around at restaurant."
		}, () => {
			TaskManager.Singleton.CompleteTask(4);
		});
		
	}
}
