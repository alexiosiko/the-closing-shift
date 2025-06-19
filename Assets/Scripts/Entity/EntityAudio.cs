using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class EntityAudio : MonoBehaviour
{
	[SerializeField] AudioClip[] clips;
	public IEnumerator PlayAudioEnum(string audioName, Action onComplete = null) {
		foreach (AudioClip clip in clips) {
			if (clip.name == audioName) {

				yield return new WaitForSeconds(clip.length);
				onComplete?.Invoke();
				yield break;
			}
		}
		Debug.LogError("Could not find audio name in clips: " + audioName);
	}

    void Update()
    {	
       	if (nav.velocity.sqrMagnitude > 0.1f)
		{
			if (!footstepsSource.isPlaying)
				footstepsSource.Play();
		}
		else
			footstepsSource.Stop();
    }
	NavMeshAgent nav;
	AudioSource footstepsSource;
	void Awake()
	{
		footstepsSource = GetComponent<AudioSource>();
		nav = GetComponent<NavMeshAgent>();
	}
}
