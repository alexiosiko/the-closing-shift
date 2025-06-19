using UnityEngine;
public class Interactable : MonoBehaviour
{
	public string highlightText = "Press e to interact";
	public virtual void Action() {
		Debug.Log("Action on " + transform.name);
	}
}