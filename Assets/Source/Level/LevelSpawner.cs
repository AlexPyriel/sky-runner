using UnityEngine;

public class LevelSpawner : ObjectPool
{
    [SerializeField] private GameObject[] _levelTiles;
    [SerializeField] private int _amountToSpawn;

    private Vector3 _newTilePosition;

    private void Start()
    {
        Initialize(_levelTiles);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        if (TryGetRandomObject(out GameObject levelTIle))
        {
            levelTIle.SetActive(true);
            levelTIle.transform.position = _newTilePosition;

            if (levelTIle.TryGetComponent<Collider>(out Collider collider))
                _newTilePosition += new Vector3(0, 0, collider.bounds.size.z);
            else
                throw new System.NullReferenceException("Tile prefab is missing Collider component");
        }
    }
}
