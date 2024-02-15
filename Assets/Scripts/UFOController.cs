using UnityEngine;

public class UFOController : MonoBehaviour
{
    public Transform target;
    public float chaseSpeed = 5f;
    public float circlingSpeed = 50f;
    public float circlingRadius = 5f;
    public float detectionRadius = 10f;
    public GameObject explosionEffectPrefab;
    public float lifetime = 30f; // Lifetime in seconds

    private bool isCircling = false;
    private Vector3 circlingDirection;
    private float currentLifetime;
    public float damageRadius = 3f;
    // Example place where you might call TakeDamage
    

    void Start()
    {
        circlingDirection = transform.right;
        if (target == null)
        {
            GameObject targetObj = GameObject.FindGameObjectWithTag("Gun");
            if (targetObj != null)
            {
                target = targetObj.transform;
            }
        }
        currentLifetime = lifetime;
    }

    void Update()
    {
        if (target == null) return;

        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            Explode();
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRadius && !isCircling)
        {
            isCircling = true;
        }

        if (!isCircling)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
        else
        {
            circlingDirection = Quaternion.Euler(0, circlingSpeed * Time.deltaTime, 0) * circlingDirection;
            Vector3 targetPosition = target.position + circlingDirection * circlingRadius;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
        }

        // New check for distance to the player to reduce health if too close
        if (distanceToTarget <= 3f)
        {
            target.GetComponent<CarLives>()?.TakeDamage(1); // Safely try to call TakeDamage on the player
            Explode(); // Directly call the explode function instead of DestroyUFO to ensure the explosion effect happens
        }
    }

    void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            DestroyUFO();
            GameManager.Instance.IncreaseScore(2); // Assuming each UFO destroyed adds 10 to the score

        }
    }

    void DestroyUFO()
    {
        // Existing explosion effect instantiation...

        // New: Check for player within damage radius and apply damage
        if (Vector3.Distance(target.position, transform.position) <= damageRadius)
        {
            // Apply damage to the player
            CarLives playerLives = target.GetComponent<CarLives>();
            if (playerLives != null)
            {
                playerLives.TakeDamage(1); // Assuming 1 is the damage value
            }
        }

        Destroy(gameObject);
    }

    void CheckForPlayerAndDamage()
    {
        if (Vector3.Distance(target.position, transform.position) <= damageRadius)
        {
            // Assuming GameManager has a method to communicate with the CarLives script,
            // or directly accessing the CarLives script on the player object to apply damage.
            // Here's a simplified example assuming you have a direct way to access the CarLives script:

            CarLives playerLives = target.GetComponent<CarLives>();
            if (playerLives != null)
            {
                playerLives.TakeDamage(1); // Passing 1 as the damage value
            }
        }
    }

}
