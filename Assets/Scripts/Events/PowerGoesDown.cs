using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PowerGoesDown : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(nameof(Delay));
	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds(2);
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(4);
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Well...",
			"That didn't sound good.",
			"I think the fridge cut the power again.",
			"I need to go downstairs and reset the breaker before the food spoils."
		});	
		GameObject.Find("Breaker").GetComponent<Collider>().enabled = true;
	}
}
