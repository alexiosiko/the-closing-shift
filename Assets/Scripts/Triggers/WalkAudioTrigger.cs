using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WalkAudioTrigger : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		if (collider.name != "Player")
			return;
		
		GetComponent<Collider>().enabled = false;

		GetComponentInChildren<AudioSource>().Play();
	}
}
