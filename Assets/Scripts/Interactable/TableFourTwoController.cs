
using UnityEngine;

public enum TableFourTwoState {
	first,
	waitingForCheck,
	noaction,
}
public class TableFourTwoController : Interactable
{
	TableFourTwoState state = TableFourTwoState.first;
	public override void Action()
	{
		base.Action();
		switch (state) {
			case TableFourTwoState.first: 
				state = TableFourTwoState.waitingForCheck;
				GetComponent<Collider>().enabled = false;
				GameObject.Find("Bill").GetComponent<Collider>().enabled = true;
				controllers[0].PlayAudio("first", Reset);
				foreach (var c in controllers)
						c.LookAtTarget();
				break;
			case TableFourTwoState.waitingForCheck: 
				if (!InventoryManager.Singleton.Remove("check")) {
					GetComponent<Collider>().enabled = false;
					foreach (var c in controllers)
						c.LookAtTarget();
					controllers[1].PlayAudio("nobill", Reset);
				} else {
					state = TableFourTwoState.noaction;
					controllers[0].PlayAudio("leaving");
					controllers[0].SetDestination(GameObject.Find("TalbeForTwoLeave").transform);
					controllers[1].SetDestination(GameObject.Find("TalbeForTwoLeave").transform);
				}
				break;
		}
	}
	void Reset() {
		GetComponent<Collider>().enabled = true;
		foreach (var c in controllers)
			c.LookAway();
	}
	void Awake()
	{
		controllers = GetComponentsInChildren<EntityController>();
	}
	void Start()
	{
		controllers[0].GetComponent<Animator>().Play("Sit");
		controllers[1].GetComponent<Animator>().Play("Sit");
	}
	EntityController[] controllers;
}
