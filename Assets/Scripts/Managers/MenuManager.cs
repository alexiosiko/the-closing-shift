using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public static bool freeze = false;
	[SerializeField] GameObject menuGameObject;
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Toggle();
	}
	void Toggle()
	{

		if (menuGameObject.activeInHierarchy)
		{
			// Disable menu
        	Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			menuGameObject.SetActive(false);
			freeze = false;
			

		}
		else
		{
			// Enable menu
        	Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			menuGameObject.SetActive(true);
			freeze = true;
		}
	}
	public void OnQuitButton()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
