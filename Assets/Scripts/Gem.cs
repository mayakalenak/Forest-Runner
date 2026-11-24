//reference: https://www.youtube.com/watch?v=EfUCEwKmcjc 
using System;
using NUnit.Framework;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private ParticleSystem gemEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {        
            GameManager.Instance.AddScore(value);
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.gemCollect);
            }

            // https://discussions.unity.com/t/how-to-access-a-particle-system-that-was-instantiated-after-the-function-call/138969
            // ParticleSystem effect = Instantiate(gemEffect, new Vector3(other.transform.position.x + 10f, other.transform.position.y, other.transform.position.z), Quaternion.identity);
            // Destroy(effect.gameObject);
            
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // increase the value of the gems as the player gets further in the game
        value += Mathf.FloorToInt(PlayerMovement.Instance.forwardSpeed * Time.deltaTime);
    }
}