using UnityEngine;
public class PlayerAudio : MonoBehaviour
{
    void Update()
    {
		Vector3 moveDelta = movement.moveDelta;
    	if (Mathf.Abs(moveDelta.x) > 0.1f || Mathf.Abs(moveDelta.z) > 0.1f)
		{
			if (!source.isPlaying)
				source.Play();
		}
		else
			source.Stop();
    }

	void Awake() {
		movement = GetComponent<PlayerMovement>();
		source = GetComponent<AudioSource>();
	}
	AudioSource source;
	PlayerMovement movement;
}
