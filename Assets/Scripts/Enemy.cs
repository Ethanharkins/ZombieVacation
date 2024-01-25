using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Transform target;
    private Animator animator;
    private bool isAtTarget = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WalkForward", true);

        GameObject targetObject = GameObject.FindGameObjectWithTag("Target");
        if (targetObject != null)
        {
            target = targetObject.transform;
            // Orient the enemy towards the target
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
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
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // Check if the enemy has reached the target
            if (Vector3.Distance(transform.position, target.position) < 0.5f) // Adjust this threshold as needed
            {
                isAtTarget = true;
                animator.SetBool("WalkForward", false);
                animator.SetBool("Idle", true);
                animator.SetTrigger("PunchTrigger");
            }
        }
    }
}
