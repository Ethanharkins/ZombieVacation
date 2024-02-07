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
            DestroyUFO();
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
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            DestroyUFO();
        }
    }

    void DestroyUFO()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
