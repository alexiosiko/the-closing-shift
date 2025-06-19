using UnityEngine;

public class OfficerBill : EntityController
{
    public override void Action()
	{
		base.Action();
	}
	void Start()
	{
		SetLookAtTarget(GameObject.Find("Hanging Body").transform);
		LookAtTarget();
	}
}
