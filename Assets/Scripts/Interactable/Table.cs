using UnityEngine;

public enum TableState {
	idle,
	waitingToClean,
}
public class Table : Interactable
{
	TableState state = TableState.waitingToClean;
	public override void Action()
	{
		base.Action();
		
		switch (state)
		{
			case TableState.waitingToClean:
				if (TaskManager.Singleton.pauseTasks) {
					DialogueManager.Singleton.StartDialogue(new string[] {
						"I should reset the breaker first before I do anything else."
					});
					return;
				}
				state = TableState.idle;
				transform.GetChild(0).gameObject.SetActive(false);
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay the table is clean.",
					"What is this?",
					"The customer forgot something.",
					"It looks like a note",
					"It says...",
					"\"This is your last night.\"",
				}, () => {
					TaskManager.Singleton.CompleteTask(1);
				});
				break;
		}
	}

}
