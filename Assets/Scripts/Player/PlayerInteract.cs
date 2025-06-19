using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private float reachDistance = 3f;

    void LateUpdate()
    {
        Debug.DrawLine(cameraPosition.position, cameraPosition.position + cameraPosition.transform.forward * reachDistance, Color.green);

		// Highlight();

		LookTrigger();
        Debug.DrawLine(cameraPosition.position, cameraPosition.position + cameraPosition.transform.forward * reachDistance * 5, Color.magenta);

		if (StatusManager.Singleton.GetFreeze())
			return;

        // Get interact input
       	if (Input.GetKeyDown("e")
            || Input.GetMouseButtonDown(0))
            Interact();

    }
	void LookTrigger()
	{
        if (Physics.Raycast(cameraPosition.position, cameraPosition.transform.forward, out RaycastHit hit, reachDistance * 5f))
		{
			LookTrigger l = hit.collider.GetComponent<LookTrigger>();
			l?.Action();
		}
		
	}
    void Interact()
    {
        if (Physics.Raycast(cameraPosition.position, cameraPosition.transform.forward, out RaycastHit hit, reachDistance))
        {
            Interactable i = hit.collider.GetComponent<Interactable>();
            i?.Action();
        }
    }
	void Highlight() {
		if (Physics.Raycast(cameraPosition.position, cameraPosition.transform.forward, out RaycastHit hit, reachDistance))
        {
            Interactable i = hit.collider.GetComponent<Interactable>();
            if (i != null)
                CanvasManager.Singleton.Highlight(i.highlightText);
			else
				CanvasManager.Singleton.Highlight(""); // Clear
        }
		else
			CanvasManager.Singleton.Highlight(""); // Clear

	}

    public Transform cameraPosition;
}