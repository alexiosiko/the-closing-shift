using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 2f;
    public float gravity = -4f;
    public float sprintSpeed = 5f;
    float speedBoost = 1f;
    float verticalVelocity = 0f;
    public Vector3 moveDelta = Vector3.zero;

    public int entityLayerMask;

    void Awake()
    {
        Singleton = this;
        controller = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();

        // Create a LayerMask for the "Entity" layer
        entityLayerMask = LayerMask.GetMask("Entity");
    }

    void Update()
    {
        Footsteps();
		
        if (StatusManager.Singleton.GetFreeze())
        {
            moveDelta = Vector3.zero;
            source.Stop();
            return;
        }
        if (StatusManager.Singleton.freezeMovement)
        {
            moveDelta = Vector3.zero;
            source.Stop();
            return;
        }

        MovePlayer();
    }

    void Footsteps()
    {
        if (moveDelta.x == 0 && moveDelta.z == 0)
        {
			source.Stop();
            return;
        }

        if (source.isPlaying)
            return;

        source.Play();
    }

    void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // if (Input.GetButton("Fire3")) // Sprint
        //     speedBoost = sprintSpeed;
        // else
        //     speedBoost = 1f;

        // Calculate movement vector
        moveDelta = transform.right * x + transform.forward * z;

        // Check if the player is grounded
        if (controller.isGrounded)
        {
            verticalVelocity = -2f; // Reset vertical velocity when grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity
        }

        moveDelta.y = verticalVelocity;

        // Move the CharacterController, but ensure it doesn't push entities
        Vector3 newPosition = transform.position + moveDelta * (baseSpeed + speedBoost) * Time.deltaTime;
        if (!Physics.CheckSphere(newPosition, controller.radius, entityLayerMask))
        {
            // Move only if the new position doesn't collide with an entity
            controller.Move(moveDelta.normalized * (baseSpeed + speedBoost) * Time.deltaTime);
        }
        else
        {
            // Stop horizontal movement if colliding with an entity
            moveDelta.x = 0;
            moveDelta.z = 0;
        }
    }

    public static PlayerMovement Singleton;
    private CharacterController controller;
    private AudioSource source;
}
