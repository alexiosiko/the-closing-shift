public enum CashierState {
	idle,
	waitingToPickUpBill,
}
public class Cashier : Interactable
{
	public void SetState(CashierState state) => this.state = state;
	CashierState state = CashierState.idle;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case CashierState.waitingToPickUpBill:
			 	FindFirstObjectByType<Customer>().SetState(CustomerState.waitingForCheck);
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay I've got his bill.",
					"Now to give him his bill to he can leave."
				});
				state = CashierState.idle;
				break;
		}
	}
}
