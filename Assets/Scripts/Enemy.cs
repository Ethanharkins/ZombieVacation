using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Transform target;
    private bool isAtTarget = false;
    private Transform respawnPoint; // The empty GameObject's Transform

    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point object not found. Please tag your respawn point object with 'RespawnPoint'");
        }

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
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the target or player
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Player"))
        {
            TeleportToRespawnPoint();
        }
    }

    private void TeleportToRespawnPoint()
    {
        if (respawnPoint != null)
        {
            // Teleport the enemy to the respawn point's position.
            transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogError("Respawn point not set. Make sure there's a GameObject tagged 'RespawnPoint' in the scene.");
        }
    }


    private void RespawnOnTarget()
    {
        // Teleport the enemy to the respawn point's position.
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
    }
}
