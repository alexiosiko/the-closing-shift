using UnityEngine;

public class HangedTrigger : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name != "Player")
			return;
		
		GetComponent<Collider>().enabled = false;
		GetComponent<AudioSource>().Play();
		PlayerCamera.Singleton.LookAtTarget(GameObject.Find("Hanging Body").transform, 2, 0.2f);
		FindFirstObjectByType<Radio>().SetState(RadioState.callPolice);
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Oh my god.",
			"That's my boss.",
			"I need to go to the office and radio the police."
		});
	}
}
