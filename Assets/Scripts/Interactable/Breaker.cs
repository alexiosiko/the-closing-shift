using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class Breaker : Interactable
{
	bool actioned = false;
	[SerializeField] Transform ownerScareParent;
	[SerializeField] Transform entityTransform;
	public override void Action()
	{
		base.Action();
		if (actioned)
			return;
		actioned = true;
		GetComponent<PlayableDirector>().Play();
		StatusManager.Singleton.Freeze();
		Invoke(nameof(AnimationDelay), 2f);
		
	}
	void AnimationDelay()
	{
		GetComponent<AudioSource>().DOFade(0, 1);
		TaskManager.Singleton.pauseTasks = false;
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Okay that seems to have reset the power.",
			"The fridge should be up and running now.",
			"...",
			"Is there someone behind me?"
		}, () => {
			ownerScareParent.GetComponent<PlayableDirector>().Play();
			PlayerCamera.Singleton.LookAtTarget(
				ownerScareParent.transform, 0.7f, 0.2f
			);
			StatusManager.Singleton.freezeMovement = true;
			Invoke(nameof(Delay), 3f);
		});
	}
	void Delay()
	{ 
		entityTransform.GetComponent<EntityController>().LookAtTarget();
		PlayerCamera.Singleton.LookAtTarget(entityTransform);
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Whoa easy there partner.",
			"What's got you so jumpy?",
			"Are you busy not doing your job or something?",
			"...",
			"I forgot my wallet, and I thought I'd come check to see if you're slacking off again ...",
			"Anyways ..."
		}, () =>{
			entityTransform.GetComponent<EntityController>().LookAway();
			Invoke(nameof(UnFreezeAndDelete), 5f);
			entityTransform.GetComponent<EntityController>().SetDestination(GameObject.Find("Exit").transform);
		});	
		
	}
	void UnFreezeAndDelete()
	{
		ownerScareParent.gameObject.SetActive(false);
		StatusManager.Singleton.freezeMovement = false;
	}
}
