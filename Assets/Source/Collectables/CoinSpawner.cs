using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : ObjectPool
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _amountToSpawn;

    private Vector3 _newCoinPosition;

    private void Start()
    {
        Initialize(_prefab);

        _newCoinPosition = new Vector3(0, 15, 0);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (TryGetObject(out GameObject coinObject))
        {
            coinObject.SetActive(true);
            coinObject.transform.position = _newCoinPosition;
            _newCoinPosition += new Vector3(0, 0, 5);

            if (coinObject.TryGetComponent<Coin>(out Coin coin))
                coin.Init(_player);
        }
    }
}
