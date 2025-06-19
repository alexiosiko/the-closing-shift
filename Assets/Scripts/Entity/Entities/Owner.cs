using UnityEngine;
public enum OwnerState {
	idle,
	walkingToPlayer,
	walkingToOffice,
	finishedListeningToRadio,
	waitingToGiveYouTasks,
	leaving,
}
public class Owner : EntityController
{
	[SerializeField] OwnerState state = OwnerState.walkingToPlayer;
	public void SetState(OwnerState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state) {
			case OwnerState.waitingToGiveYouTasks:
				LookAtTarget();
				PlayerCamera.Singleton.LookAtTarget(transform);
				state = OwnerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"He's finally gone.",
					"He's been here for two hours sitting along watching people",
					"Well...",
					"I'm going home,",
					"and I'm leaving you alone to close the restaurant.",
					"Make sure to clean up properly this time or you won't get your last check.",
					"You can hit the TAB key to see your task list.",
					"Now get out of my way."
				}, () => {
					TaskManager.Singleton.SetActive();
					SetDestination(GameObject.Find("Exit").transform);
					LookAway();
				});
				break;
			case OwnerState.finishedListeningToRadio:
				LookAtTarget();
				PlayerCamera.Singleton.LookAtTarget(transform);
				state = OwnerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"God this town is being cursed.",
					"People are getting crazy and turn to killing...",
					"Anyways,",
					"Get the bill for that last guest so we can close.",

				}, () => {
					FindFirstObjectByType<Customer>().SetState(CustomerState.askingForCheck);
					LookAway();
					SetDestination(GameObject.Find("Behind Store").transform);
				});
				break;
			case OwnerState.walkingToPlayer:
				LookAtTarget();
				PlayerCamera.Singleton.LookAtTarget(transform);
				state = OwnerState.walkingToOffice;
				DialogueManager.Singleton.StartDialogue(new string[] {
						"Again thanks for coming in today.",
						"I've been so stressed at this place lately.",
						"I have to talk to you about something.",
						"Meet me in my office now."}, () => {
							SetDestination(GameObject.Find("Office").transform);
							LookAway();
					});
				break;
			case OwnerState.walkingToOffice:
				if (DestinationDistance() > 2f)
					return;
				state = OwnerState.idle;
				LookAtTarget();
				PlayerCamera.Singleton.LookAtTarget(transform);
				DialogueManager.Singleton.StartDialogue(new string[] {
					"This restaurant cannot support itself, ",
					"And I'm going to have to let you off.", 
					"And there's nothing I can do, the money is just not enough.",
					"I've tried m-"
					}, () => {
						LookAway();
						Radio radio = FindFirstObjectByType<Radio>();
						radio.onComplete = () => state = OwnerState.finishedListeningToRadio;
						radio.SetState(RadioState.radioBroadcast);
						radio.Action();
					}
				);
				break;
		}
	}
	// public override void Update()
	// {
	// 	base.Update();
	// 	//  Check the distance to the player
    //   float distanceToPlayer = Vector3.Distance(transform.position, PlayerCamera.Singleton.transform.position);
	// 	 print("Dest To player: " + distanceToPlayer);
    //   if (distanceToPlayer <= 3f)
	// 	Action();
	// }
	void Start()
	{
		Invoke(nameof(WalkToPlayer), 5f);
		playerTransform = GameObject.Find("Player").transform;
	}

	void WalkToPlayer() => SetDestination(playerTransform);
	Transform playerTransform;
}
