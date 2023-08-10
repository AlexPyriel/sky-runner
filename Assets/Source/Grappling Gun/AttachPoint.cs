using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AttachPoint : MonoBehaviour
{
    [SerializeField] private Game.Routes _route;
    [SerializeField] private Material _active;
    [SerializeField] private Material _inactive;

    private Renderer _renderer;

    public Game.Routes Route => _route;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Enable()
    {
        _renderer.material = _active;
    }

    public void Disable()
    {
        _renderer.material = _inactive;
    }
}
