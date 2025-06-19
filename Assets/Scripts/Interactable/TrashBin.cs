
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum TrashBinState {
	idle,
	waitingForTrash
}
public class TrashBin : Interactable
{
	[SerializeField] GameObject sideCustomerObject;
	public TrashBinState state = TrashBinState.idle;
	public void SetState(TrashBinState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case TrashBinState.waitingForTrash:
			state = TrashBinState.idle;
			sideCustomerObject.SetActive(true);
			DialogueManager.Singleton.StartDialogue(new string[] {
				"Great,",
				"Not I got smelly juice on me.",
				"Well that done and over with."
			}, () => {
				GameObject.Find("Back Door").GetComponentInChildren<Door>().canClose = true;
				TaskManager.Singleton.CompleteTask(0);
			});
				break;
		}
	
	}
}
