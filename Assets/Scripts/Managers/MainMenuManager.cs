using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] Image image;
	[SerializeField] AudioSource source;
	bool busy = false;
	public void StartGame()
	{
		if (busy)
			return;
		busy = true;
		image.enabled = true;
		image.DOFade(1, 2f);
		source.DOFade(0, 2f);
		Invoke(nameof(ChangeScene), 2.5f);
	}
	void ChangeScene() => SceneManager.LoadScene(1);
	public void QuitGame()
	{
		Application.Quit();
	}
}
