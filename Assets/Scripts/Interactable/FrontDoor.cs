using UnityEngine;

public enum FrontDoorState {
	idle,
	finishedTasks,
	doorIsLocked,
}
public class FrontDoor : Interactable
{
	[SerializeField] FrontDoorState state = FrontDoorState.idle;
	public void SetState(FrontDoorState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case FrontDoorState.doorIsLocked:
				state = FrontDoorState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"The front door is locked...",
					"Who locked the front door?",
				});
				break;
			case FrontDoorState.finishedTasks:
				state= FrontDoorState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Wait...",
					"I should blow out the candles at my bosses desk before I leave.",
					"The last thing I want is to be reponsible for this place catching on fire."
				}, () => {
					FindAnyObjectByType<OfficeDesk>().SetState(OfficeDeskState.waitingToBlowCandles);
				});
				break;	
		} 
	}
}
