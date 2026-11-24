using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {    
            GameManager.Instance.LoseGame();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.obstacleHit); 
            Destroy(gameObject);
        }
    }
}