using UnityEngine;
using UnityEngine.AI;
using System;
using DG.Tweening;

[RequireComponent(typeof(IKAnimatorController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EntityAnimationController))]
[RequireComponent(typeof(EntityAudio))]
public class EntityController : Interactable
{
	[SerializeField] Transform destination;
	public void SetDestination(Transform destination) => this.destination = destination;
	
	public virtual void Update()
	{
		if (StatusManager.Singleton.GetFreeze())
			nav.velocity = Vector3.zero;
		else if (destination)
			nav.SetDestination(destination.position);
	}
	public void SetLookAtTarget(Transform target) {
		ikAnimator.weight = 0;
		ikAnimator.EnableIK();
		ikAnimator.objTarget = target;
	} 
	public void LookAtTarget() => ikAnimator.EnableIK();

	public void LookAway() => ikAnimator.DisableIK();
	public float DestinationDistance() => Vector3.Distance(transform.position, destination.position);
    protected virtual void Awake()
    {
		ikAnimator = GetComponent<IKAnimatorController>();
		nav = GetComponent<NavMeshAgent>();
		entityAudio = GetComponent<EntityAudio>();
		animator = GetComponent<Animator>();
    }
	protected IKAnimatorController ikAnimator;
	NavMeshAgent nav;
	public void PlayAudio(string audioName, Action onComplete = null)
	{
		StopAllCoroutines();
		StartCoroutine(entityAudio.PlayAudioEnum(audioName, onComplete));
	}
	protected EntityAudio entityAudio;
	[HideInInspector] public Animator animator;
}
