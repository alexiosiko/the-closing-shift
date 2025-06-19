using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] List<string> items;
	public void Add(string name)
	{
		items.Add(name);
	}
	public bool Remove(string name)
	{
		foreach (string str in items)
		{
			if (name == str)
			{
				items.Remove(name);
				return true;
			}
		}
		return false;
	}
	void Awake()
	{
		Singleton = this;
	}
	public static PlayerInventory Singleton;
}
