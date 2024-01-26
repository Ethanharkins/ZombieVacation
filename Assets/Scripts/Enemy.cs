using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Transform target;
    private Animator animator;
    private bool isAtTarget = false;
    public Vector3 teleportPositionAboveTarget; // Set this in the Inspector

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WalkForward", true);

        GameObject targetObject = GameObject.FindGameObjectWithTag("Target");
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target object not found. Please tag your target object with 'Target'");
        }
    }

    void Update()
    {
        if (target != null && !isAtTarget)
        {
            // Move towards the target only if not at the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target") && !isAtTarget)
        {
            isAtTarget = true;
            TeleportAboveTarget();
            animator.SetTrigger("PunchTrigger");
        }
    }

    private void TeleportAboveTarget()
    {
        transform.position = target.position + teleportPositionAboveTarget;
        animator.SetBool("WalkForward", false);
        animator.SetBool("Idle", true);
    }
}
