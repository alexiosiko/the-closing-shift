using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float sensX = 2f;
    [SerializeField] private float sensY = 2f;
    [SerializeField] private Transform playerTransform;
    private float xRotation;
    private float yRotation;
    private void Update()
    {
        // Normal camera rotation logic
        if (StatusManager.Singleton.GetFreeze())
            return;

		if (MenuManager.freeze)
			return;

        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerTransform.Rotate(Vector3.up * mouseX);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   public void LookAtTarget(Transform target, float offsetY = 2f, float duration = 1.5f)
	{
		// Set offset
		Vector3 pos = target.position;
		pos.y += offsetY;
		
		// Calculate direction to the target
		Vector3 directionToTarget = pos - Camera.main.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

		// Decompose target rotation into horizontal (player) and vertical (camera) components
		Vector3 eulerRotation = targetRotation.eulerAngles;

		

		// Handle horizontal rotation (Y-axis) for the player
		playerTransform.DOLocalRotate(new Vector3(0, eulerRotation.y, 0), duration, RotateMode.Fast);

		// Handle vertical rotation (X-axis) for the camera holder
		transform.DOLocalRotate(new Vector3(eulerRotation.x, 0, 0), duration, RotateMode.Fast).OnComplete(() =>
    {
        // Update the xRotation and yRotation to match the new orientation
        xRotation = eulerRotation.x > 180 ? eulerRotation.x - 360 : eulerRotation.x; // Handle wrap-around
        yRotation = eulerRotation.y > 180 ? eulerRotation.y - 360 : eulerRotation.y;
    });
	}

	public void Awake() => Singleton = this;
	public static PlayerCamera Singleton;
}
