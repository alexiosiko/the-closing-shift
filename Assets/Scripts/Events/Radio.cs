using System;
using System.Collections;
using System.Diagnostics.Tracing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
public enum RadioState {
	idle,
	radioBroadcast,
	callPolice
}

[RequireComponent(typeof(AudioSource))]
public class Radio : Interactable
{
	[SerializeField] string[] texts;
	[SerializeField] RadioState state = RadioState.radioBroadcast;
	public void SetState(RadioState state) => this.state = state;
    public override void Action()
    {
		base.Action();
		switch (state)
		{
			case RadioState.callPolice:
				state = RadioState.idle;
				source.Play();
				source.DOFade(1, 1f);
				DialogueManager.Singleton.StartDialogue(new string[] {
					"911 what's your emergency?",
					"The town's killer snuck into my resturant and murdered my boss!",
					"You need to get over here and investigate!",
					"Okay sir I'm sending a dispatch to you immediately."
				}, () => {
					source.DOFade(0, 1f);
					GameObject.Find("Police").GetComponent<PlayableDirector>().Play();
				});
				break;
			case RadioState.radioBroadcast:
				state = RadioState.idle;
				source.Play();
				source.DOFade(1, 1f);
				StartCoroutine(DonePlay());
				DialogueManager.Singleton.StartNarration(texts);
				break;
		}
    }
	public Action onComplete = null;
    IEnumerator DonePlay() {
		yield return new WaitForSeconds(3.5f * texts.Length);
		source.DOFade(0, 1f);
		yield return new WaitForSeconds(1f);
		source.Stop();
		onComplete.Invoke();
	} 
    void Awake() => source = GetComponent<AudioSource>();
    private AudioSource source;
}
