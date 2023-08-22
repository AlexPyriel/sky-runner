using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action Collected;
    public event Action ObstacleCollided;

    private void OnTriggerEnter(Collider other)
    {
        // if (other.TryGetComponent<Grass>(out Grass gras))
        // {
        //     Collected?.Invoke();
        //     Destroy(other.gameObject);
        // }
        // else if (other.TryGetComponent<Obstacle>(out Obstacle obstacle))
        // {
        //     ObstacleCollided?.Invoke();
        //     obstacle.Destroy();
        // }
    }
}
