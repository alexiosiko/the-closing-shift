using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;


public enum OfficerDickState {
	idle,
	walkingToScene,
	crying,
}
public class OfficerDick : EntityController
{
	[SerializeField] GameObject finalScareObject;
	OfficerDickState state = OfficerDickState.walkingToScene;
	public void SetState(OfficerDickState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case OfficerDickState.crying:
				state = OfficerDickState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"My best friend...",
					"I can't believe this is happening",
					"You go home.",
					"Get home safe.",
					"Run."
				}, () => {

					// Disable rain
					Transform rainParent = GameObject.Find("Rain").transform;
					foreach (Transform child in rainParent)
						child.GetComponent<AudioSource>().DOFade(0, 2f);
						
					finalScareObject.SetActive(true);
					FindFirstObjectByType<FrontDoor>().SetState(FrontDoorState.doorIsLocked);
				});
				break;
			case OfficerDickState.walkingToScene:
				
				if (bill.DestinationDistance() > 4 || DestinationDistance() > 4)
					return;

				state = OfficerDickState.idle;

				bill.SetLookAtTarget(GameObject.Find("Player").transform);
				SetLookAtTarget(GameObject.Find("Player").transform);

				DialogueManager.Singleton.StartDialogue(new string[] {
					"Oh m-",
					"We thought this was a prank call",
					"Detective Konrad, you search around and investigate.",
					"See if you can find any information about this.",
					"And you stay here with me while I ask you some questions."
				}, () => {
					StatusManager.Singleton.freezeMovement = true;
					bill.LookAway();
					LookAway();
					bill.SetDestination(GameObject.Find("Office Table").transform);
					Invoke(nameof(AskQuestionsDelay), 12f);
				});
				break;
		}
	}
	void AskQuestionsDelay()
	{
		LookAtTarget();
		PlayerCamera.Singleton.LookAtTarget(transform);
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Alright, now stay calm. We're here to help.",
			"We'll need to gather some information from you to understand what happened.",
			"Just answer honestly and don't worry, you're not in trouble.",
			"Once we're done here, go directly home and lock your doo--"
		}, () => {
			AudioSource source = GameObject.Find("Dick Dies Audio").GetComponent<AudioSource>();
			source.Play();
			bill.GetComponent<EntityController>().enabled = false;
			bill.GetComponent<NavMeshAgent>().enabled = false;
			bill.animator.Play("Dead");
			bill.transform.position = new Vector3(bill.transform.position.x, 0, bill.transform.position.z);
			LookAway();
			StatusManager.Singleton.freezeMovement = true;
			Invoke(nameof(Delay), source.clip.length);
		});
	}
	void Delay()
	{
		LookAtTarget();
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Officer Konrad?",
			"...",
			"You stay here, I'm going to check on my partner"
		}, () => {
			LookAway();
			SetDestination(GameObject.Find("Dick Dies Audio").transform);
			Invoke(nameof(Cry), 12f);
		});
	}
	void Cry()
	{
		state = OfficerDickState.crying;
		animator.Play("Cry");
		crySource.Play();
		Invoke(nameof(IsOfficerOkay), 5f);
	}
	void IsOfficerOkay()
	{
		StatusManager.Singleton.freezeMovement = false;
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Are the detectives okay?",
			"I'm going to go check on them.",
		}, () => {

		});
	}
	void Start()
	{
		SetLookAtTarget(GameObject.Find("Hanging Body").transform);
		LookAtTarget();
	}
	[SerializeField] OfficerBill bill;
	[SerializeField] AudioSource crySource;
}
