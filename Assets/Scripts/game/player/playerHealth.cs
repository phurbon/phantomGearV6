using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the game

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public bool isInvincible = false; // ✅ ADD THIS LINE

    public GameObject deathEffect; // Optional: Assign a death animation effect

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // ✅ Player does not take damage in shade zones

        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        Debug.Log("Player has died!");
        gameObject.SetActive(false); // Disable player on death

        // Restart the level after delay
        Invoke("RestartLevel", 2f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Player Healed: " + currentHealth);
    }
}
