using System;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
	private float timer = 0.0f;
	float localOffsetY;
	[SerializeField] float bobbingSpeed = 0.18f;
	[SerializeField] float bobbingAmount = 0.2f;
	[SerializeField] PlayerMovement playerMovement;
	
	void Update ()
	{
		float waveslice = 0.0f;
		Vector3 cSharpConversion = transform.localPosition; 
		float horizontal = playerMovement.moveDelta.x;
		float vertical = playerMovement.moveDelta.z;
		
		if (horizontal == 0 && vertical == 0) {
			timer = 0.0f;
			}
			else {
			waveslice = Mathf.Sin(timer);
			timer = timer + bobbingSpeed;
			if (timer > Mathf.PI * 2) {
				timer = timer - (Mathf.PI * 2);
			}
		}
		if (waveslice != 0) {
			float translateChange = waveslice * bobbingAmount;
			float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
			totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
			translateChange = totalAxes * translateChange;
			cSharpConversion.y = translateChange;
			cSharpConversion.y += localOffsetY;
		}
		
		transform.localPosition = cSharpConversion;

	}
	void Awake()
	{
		localOffsetY = transform.localPosition.y;
	}
	
}
