using UnityEngine;

public class FinishPoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.LevelComplete();
    }
}
