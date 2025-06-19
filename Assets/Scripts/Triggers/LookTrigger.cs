using UnityEngine;

public class LookTrigger : MonoBehaviour
{
	public virtual void Action()
	{
		Debug.Log("LookTrigger on " + this);
	}
}
