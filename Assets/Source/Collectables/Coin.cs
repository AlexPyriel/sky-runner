using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private UnityEvent _collected;

    private Transform _player;
    private float _boundOffset = 10f;
    private float _delay = 1f;

    private void OnEnable()
    {
        _prefab.SetActive(true);
    }

    private void Update()
    {
        if (transform.position.z < (_player.position.z - _boundOffset))
        {
            Disable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _collected?.Invoke();
            _prefab.SetActive(false);
            Invoke(nameof(Disable), _delay);
        }
    }

    public void Init(Transform player)
    {
        _player = player;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
