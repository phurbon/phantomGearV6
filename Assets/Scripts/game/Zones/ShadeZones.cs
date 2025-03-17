using UnityEngine;

public class ShadeZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth playerHealth = other.GetComponent<playerHealth>();
            if (playerHealth != null)
                playerHealth.isInvincible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth playerHealth = other.GetComponent<playerHealth>();
            if (playerHealth != null)
                playerHealth.isInvincible = false;
        }
    }
}
