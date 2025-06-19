using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
	public void MainMenu() => SceneManager.LoadScene(0);
	public void Quit() => Application.Quit();
	void Start()
	{
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}
}
