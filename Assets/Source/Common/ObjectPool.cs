using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected void Initialize(params GameObject[] prefabs)
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject spawned = Instantiate(prefabs[randomIndex], _container.transform);

            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault<GameObject>(obj => obj.activeSelf == false);

        return result != null;
    }

    protected bool TryGetRandomObject(out GameObject result)
    {
        List<GameObject> inActiveObjects = _pool.Where(obj => obj.activeSelf == false).ToList();
        result = inActiveObjects[Random.Range(0, inActiveObjects.Count)];
        return result != null;
    }
}
