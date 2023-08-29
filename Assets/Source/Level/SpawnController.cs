using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _levelTile;
    private LevelSpawner _levelSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            _levelSpawner?.SpawnTile();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            _levelTile?.SetActive(false);
    }

    public void Init(LevelSpawner levelSpawner)
    {
        _levelSpawner = levelSpawner;
    }
}
