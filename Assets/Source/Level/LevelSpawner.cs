using Unity.Profiling.Editor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject[] _levelTiles;
    [SerializeField] private GameObject _finishTile;
    [SerializeField] private int _amountToSpawn;

    private Vector3 _newTilePosition;

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            GameObject randomTile = _levelTiles[Random.Range(0, _levelTiles.Length)];
            SpawnTile(randomTile);
        }

        // SpawnTile(_finishTile);
    }

    private void SpawnTile(GameObject tile)
    {
        GameObject spawned = Instantiate(tile, _newTilePosition, tile.transform.rotation, _container.transform);

        if (spawned.TryGetComponent<Collider>(out Collider component))
            _newTilePosition += new Vector3(0, 0, component.bounds.size.z);
        else
            throw new System.NullReferenceException("Tile prefab is missing Collider component");
    }
}
