using UnityEngine;
using UnityEngine.Playables;

public enum OfficeDeskState {
	idle,
	waitingToCheck,
	waitingToBlowCandles,
	waitingToCallPolice,
}
public class OfficeDesk : Interactable
{
	[SerializeField] Transform hangedTrasnform;
	OfficeDeskState state = OfficeDeskState.waitingToCheck;
	public void SetState(OfficeDeskState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case OfficeDeskState.waitingToBlowCandles:
				state = OfficeDeskState.idle;
				GetComponentInChildren<Light>().enabled = false;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay, there go the candles.",
					"Now time to get out of here."
				}, () => {
					GameObject.Find("Grandma").GetComponent<Collider>().enabled = true;
					hangedTrasnform.gameObject.SetActive(true);
					FindFirstObjectByType<FrontDoor>().SetState(FrontDoorState.idle);
				});
				break;
			case OfficeDeskState.waitingToCheck:
				if (TaskManager.Singleton.pauseTasks) {
					DialogueManager.Singleton.StartDialogue(new string[] {
						"I should reset the breaker first before I do anything else."
					});
					return;
				}
				state = OfficeDeskState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay, so I've logged todays sales done today.",
					"Hmm,",
					"The net sales have really been decreasing over the last few months.",
				}, () => {
					TaskManager.Singleton.CompleteTask(3);
				});
				break;
		}
	}
}
