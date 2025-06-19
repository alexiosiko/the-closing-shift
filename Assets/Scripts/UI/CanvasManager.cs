using UnityEngine;
using TMPro;
using System;
public class CanvasManager : MonoBehaviour
{
	public void Highlight(string text)
	{
		alertText.text = text;
	}

	void Awake()
	{
		alertText = GameObject.Find("Highlight").GetComponentInChildren<TMP_Text>();
		Singleton = this;
	}
	TMP_Text alertText;
	public static CanvasManager Singleton;
}
