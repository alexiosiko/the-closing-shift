using System.Collections;
using UnityEngine;
public enum CustomerState {
	idle,
	askingForCheck,
	waitingForCheck,
}
public class Customer : EntityController
{
	[SerializeField] CustomerState state = CustomerState.idle;
	public void SetState(CustomerState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{

			case CustomerState.waitingForCheck:
				LookAtTarget();
				state = CustomerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Thank you so much for your time and service.",
					"I'm sure we'll meet again shortly.",
					"Very... shortly...",
				}, () => {
					StartCoroutine(nameof(Delay));
				});
				break;
			case CustomerState.askingForCheck:
				LookAtTarget();
				state = CustomerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Hi there,",
					"The food is incredible here.",
					"This really is a high quality establishment.",
					"I'll have the check please and thank you."
				}, () => {
					FindFirstObjectByType<Cashier>().SetState(CashierState.waitingToPickUpBill);
					LookAway();
				});
				break;
		}
	}
	IEnumerator Delay()
	{	
		LookAway();
		animator.CrossFade("Idle", 0.5f);
		Owner owner = FindAnyObjectByType<Owner>();
		owner.SetState(OwnerState.waitingToGiveYouTasks);
		yield return new WaitForSeconds(2);
		SetDestination(GameObject.Find("Exit").transform);
		yield return new WaitForSeconds(7);
		owner.SetDestination(GameObject.Find("Player").transform);
	}
	void Start()
	{
		animator.Play("Sit");
	}
}
