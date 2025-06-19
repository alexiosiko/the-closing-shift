using UnityEngine;

public class IKAnimatorController : MonoBehaviour
{
    Animator animator;
	public bool ikActive = false;
	public Transform objTarget;
	public float weight = 0;
	public void EnableIK() => ikActive = true;
	public void DisableIK () => ikActive = false;
  	void OnAnimatorIK()
	{
		if (!animator || !objTarget)
			return;


		if (ikActive)
			weight = Mathf.Lerp(weight, 1, Time.deltaTime * 2.5f);
		else
			weight = Mathf.Lerp(weight, 0, Time.deltaTime * 2.5f);

		animator.SetLookAtWeight(weight);
		animator.SetLookAtPosition(objTarget.position);
	}

	void Awake()
	{
		objTarget = GameObject.Find("Main Camera").transform;
		animator = GetComponent<Animator>();
	}
}
