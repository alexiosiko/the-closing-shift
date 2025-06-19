using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : Interactable
{
	AudioSource source;
	[SerializeField] AudioClip openClip;
	[SerializeField] AudioClip closeClip;
	bool isClosed = true;
	[HideInInspector] public bool canClose = false;
	public override void Action()
	{
		base.Action();
		if (isClosed)
			Open();
		else if (canClose)
			Close();
	}
	public void Open()
	{
		isClosed = false;
		animator.CrossFade("Open", 0.2f);
		source.Stop();
		source.clip = openClip;
		source.Play();

	}
	public void Close()
	{
		isClosed = true;
		animator.CrossFade("Close", 0.2f);
		source.Stop();
		source.clip = closeClip;
		source.Play();
	}
	void Awake()
	{
		animator = GetComponentInParent<Animator>();
		source = GetComponent<AudioSource>();
	}
	Animator animator;
}
