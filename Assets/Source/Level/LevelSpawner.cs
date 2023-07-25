using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject[] _levelTiles;
    [SerializeField] private GameObject _finishTile;
    [SerializeField] private int _amountToSpawn;

    private float _tileLength = 60f;
    private Vector3 _newTileOffset;
    private Vector3 _newTilePosition;

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        GameObject randomTile = _levelTiles[Random.Range(0, _levelTiles.Length)];

        _newTileOffset = new Vector3(0, 0, _tileLength);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            SpawnTile(randomTile);
        }

        SpawnTile(_finishTile);
    }

    private void SpawnTile(GameObject tile)
    {
        _newTilePosition += _newTileOffset;
        Instantiate(tile, _newTilePosition, tile.transform.rotation, _container.transform);
    }
}
