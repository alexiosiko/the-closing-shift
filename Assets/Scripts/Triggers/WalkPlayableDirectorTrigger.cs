using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class WalkPlayableDirectorTrigger : MonoBehaviour
{
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != "Player")
			return;

		StatusManager.Singleton.Freeze();
		PlayerCamera.Singleton.LookAtTarget(GameObject.Find("Final Animation Target").transform, 0);
		GetComponentInParent<PlayableDirector>().Play();
	}
}
