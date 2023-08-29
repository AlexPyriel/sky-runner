using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _collided;

    public event Action Collected;
    public event Action ObstacleCollided;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Spot trigger {other.name}");
        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            Collected?.Invoke();
            other.gameObject.SetActive(false);
        }
        // else if (other.TryGetComponent<Powerup>(out Powerup powerup))
        // {
        //     ObstacleCollided?.Invoke();
        //     powerup.Destroy();
        // }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_collided == false)
        {
            _collided = true;
            Debug.Log($"Colleded {other.gameObject.name}");
            ObstacleCollided?.Invoke();
        }
    }

    // private void Init()
    // {
    //     _collided = false;
    // }
}
