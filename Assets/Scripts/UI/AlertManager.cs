using System.Collections;
using UnityEngine;

public class AlertManager : MonoBehaviour
{

	[SerializeField] TMPro.TMP_Text text;

	public void Alert(string text, float delay = 2)
	{
		this.text.text = text;
		StopAllCoroutines();
		StartCoroutine(EraseAlert(delay));
	}
	IEnumerator EraseAlert(float delay)
	{
		yield return new WaitForSeconds(delay);
		text.text = "";
	}
	public static AlertManager Singleton;
	void Awake() => Singleton = this;
}
