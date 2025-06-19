using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Singleton;
	bool freeze = false;
	public void Freeze() => freeze = true;
	public void UnFreeze() => freeze = false;
	public bool GetFreeze() => freeze;
	public bool freezeMovement = false;
	void Awake()
	{
		Singleton = this;
	}
}
