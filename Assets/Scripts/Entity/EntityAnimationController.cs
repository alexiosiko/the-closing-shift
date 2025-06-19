using UnityEngine;
using UnityEngine.AI;

public class EntityAnimationController : MonoBehaviour
{
    Animator animator;
	NavMeshAgent nav;
	void Awake()
    {
        animator = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
		Vector3 velocity = nav.velocity;
		float speed = Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z);
		animator.SetFloat("Speed", speed);
    }
}
