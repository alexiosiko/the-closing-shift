using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class FinalScare : MonoBehaviour
{
	[SerializeField] GameObject activateObject;
	void OnTriggerEnter(Collider collider)
	{
		if (collider.name != "Player")
			return;

		StatusManager.Singleton.Freeze();
		PlayerCamera.Singleton.LookAtTarget(GameObject.Find("Final Animation Target").transform, 0, 0.2f);

		GetComponent<PlayableDirector>().Play();	

		Invoke(nameof(ChangeScene), 7f);
	}
	void ChangeScene()
	{
		SceneManager.LoadScene(3);
	}

}
