using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action Collected;
    public event Action ObstacleCollided;

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log($"Colleded {other.name}");
    //     if (other.TryGetComponent<Coin>(out Coin coin))
    //     {
    //         Collected?.Invoke();
    //         Destroy(other.gameObject);
    //         other.gameObject.SetActive(false);
    //     }
    //     else if (other.TryGetComponent<Powerup>(out Powerup powerup))
    //     {
    //         ObstacleCollided?.Invoke();
    //         powerup.Destroy();
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Colleded {other.gameObject.name}");
        ObstacleCollided?.Invoke();
    }
}
