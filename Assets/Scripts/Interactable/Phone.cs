using UnityEngine;
using UnityEngine.Playables;

public enum PhoneState {
	idle,
	waitingToCallPolice,
}
public class Phone : Interactable
{
	PhoneState state = PhoneState.idle;
	public void SetState(PhoneState state) => this.state = state;
	public override void Action()
	{
		base.Action();
	}
}
