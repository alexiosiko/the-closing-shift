using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Collectable : Interactable
{	
	[SerializeField] public string[] texts;
	[SerializeField] string itemName;
	[SerializeField] bool destroyOnCollect;
	public override void Action()
	{
		base.Action();
		GetComponent<Collider>().enabled = false;
		DialogueManager.Singleton.StartDialogue(texts);
		PlayerInventory.Singleton.Add(itemName);
		if (destroyOnCollect)
			Destroy(gameObject);
	}
}
