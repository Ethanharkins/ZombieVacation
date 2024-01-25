using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Animator animator;
    public Transform target; // Assign the target object at the bottom of the screen

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WalkForward", true);
    }

    void Update()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, target.position.z), moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the target
        if (Vector3.Distance(transform.position, target.position) < 0.5f) // 0.5f is a threshold distance
        {
            animator.SetBool("WalkForward", false);
            animator.SetBool("Idle", true);
            animator.SetTrigger("PunchTrigger");
        }
    }

    // Implement OnCollisionEnter or OnTriggerEnter if needed for collisions with bullets
}
