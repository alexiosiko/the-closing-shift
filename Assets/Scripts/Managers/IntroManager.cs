using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text text;
	[SerializeField] string[] texts;
    void Start()
    {
		StartCoroutine(SlideShow());
    }

	IEnumerator SlideShow()
	{
		for (int i = 0; i < texts.Length; i++) 
			yield return AnimateTextRoutine(texts[i]);
		SceneManager.LoadScene(2);
	}
	private IEnumerator AnimateTextRoutine(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            text.text = message.Substring(0, i + 1); // Add the next letter
            yield return new WaitForSeconds(0.05f); // Wait between each letter
        }
		yield return new WaitForSeconds(3f); // Wait between each letter
    }

}
