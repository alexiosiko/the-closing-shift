using UnityEngine;
public enum MopFloorState {
	idle,
	waitingToMop,
}
public class MopFloor : Interactable
{
	MopFloorState state = MopFloorState.waitingToMop;
	public override void Action()
	{
		base.Action();
		
		switch (state)
		{
			case MopFloorState.waitingToMop:
				if (TaskManager.Singleton.pauseTasks) {
					DialogueManager.Singleton.StartDialogue(new string[] {
						"I should reset the breaker first before I do anything else."
					});
					return;
				}
				transform.GetChild(0).gameObject.SetActive(false);
				GetComponent<Collider>().enabled = false;
				state = MopFloorState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"How can people be so clumbsy in a restaurant?",
				}, () => {
					TaskManager.Singleton.CompleteTask(2);
				});
				break;
		}
	}
}
