using UnityEngine;

public class UFOAvoidence : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("UFO")) // Ensure your UFOs are tagged appropriately
        {
            Vector3 directionAwayFromOther = transform.position - other.transform.position;
            directionAwayFromOther.y = 0; // Optional, adjust based on your game's needs
            transform.position += directionAwayFromOther.normalized * Time.deltaTime; // Adjust the multiplier as needed for smoother movement
        }
    }
}
