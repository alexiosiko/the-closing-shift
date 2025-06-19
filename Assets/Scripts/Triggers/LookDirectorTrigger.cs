using UnityEngine;
using UnityEngine.Playables;

public class LookDirectorTrigger : LookTrigger
{
	public override void Action()
	{
		base.Action();
		GetComponent<Collider>().enabled = false;
		GetComponent<PlayableDirector>().Play();
	}
}
