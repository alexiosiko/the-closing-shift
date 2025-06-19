using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TaskManager : MonoBehaviour
{
	[SerializeField] bool active = false;
	[SerializeField] GameObject powerGoesDownObj;
	public bool pauseTasks = false;
	bool isOpen = false;
	public void SetActive()
	{
		active = true;
		tasksParent.gameObject.SetActive(true);
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Task");
		foreach (GameObject obj in objs)
			obj.GetComponent<Collider>().enabled = true;
	}
	public void Update()
	{
		if (!active || !tasksParent.gameObject.activeInHierarchy)
			return;
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (isOpen)
			{
				// Move away
				tasksParent.DOKill();
				tasksParent.DOAnchorPos(new Vector2(0, -1000), 0.25f);
				tasksParent.DOLocalRotate(new Vector3(0, 0, -180), 0.25f);
				isOpen = false;

				// Sound
				openCloseSource.clip = closeClip;
				openCloseSource.Play();
			}
			else
			{
				// Move to screen
				tasksParent.DOKill();
				tasksParent.DOAnchorPos(Vector2.zero, 1f);
				tasksParent.DOLocalRotate(new Vector3(0, 0, 0), 1f);
				isOpen = true;

				
				// Sound
				openCloseSource.clip = openClip;
				openCloseSource.Play();
			}
			// if (tasksParent.gameObject.activeInHierarchy)
			// 	tasksParent.gameObject.SetActive(false);
			// else
			// 	tasksParent.gameObject.SetActive(true);	
		}
	}
	[SerializeField] RectTransform tasksParent;
	[SerializeField] int tasksLeft; 
	public void CompleteTask(int index)
	{
		TMP_Text taskText = tasksParent.GetChild(index).GetComponent<TMP_Text>();
        taskText.text = $"<s>{taskText.text}</s>";
		tasksLeft--;
		writingSource.Play();
		if (tasksLeft == 3)
			floorCreekGameObject.SetActive(true);
		else if (tasksLeft == 2)
			PauseTasks();
		else if (IsTasksComplete())
			Invoke(nameof(Complete), 1.5f);
	}
	void PauseTasks()
	{
		pauseTasks = true;
		DialogueManager.Singleton.dialogueDelay = false;
		powerGoesDownObj.SetActive(true);
	}
	void Complete()
	{
		DialogueManager.Singleton.dialogueDelay = false;
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Okay that's all the tasks done.",
			"Now I can finally go home.",
			"I can leave through the front door."
		}, () => {
			active = false;
			tasksParent.gameObject.SetActive(false);
			FindFirstObjectByType<FrontDoor>().SetState(FrontDoorState.finishedTasks);
		});
	}
	void Awake()
	{
		Singleton = this;
	}

	[SerializeField] AudioClip openClip;
	[SerializeField] AudioClip closeClip;
	[SerializeField] AudioSource openCloseSource;
	[SerializeField] AudioSource writingSource;
	[SerializeField] GameObject floorCreekGameObject;
	public bool IsTasksComplete() => tasksLeft == 0;
	public static TaskManager Singleton;
}
