using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject[] _levelTiles;
    [SerializeField] private GameObject _finishTile;
    [SerializeField] private int _amountToSpawn;

    private Vector3 _currentTilePosistion = Vector3.zero;

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        GameObject randomTile = _levelTiles[Random.Range(0, _levelTiles.Length)];
        GameObject tile = Instantiate(randomTile, transform.position, randomTile.transform.rotation, _container.transform);

        if (tile.TryGetComponent<Collider>(out Collider component))
        {
            _currentTilePosistion += new Vector3(0, 0, component.bounds.size.z);
            tile.transform.position = _currentTilePosistion;
        }
        else
        {
            throw new System.NullReferenceException("Tile prefab is missing Collider component");
        }
    }
}
