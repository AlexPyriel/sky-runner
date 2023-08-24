using UnityEngine;

public class GameSceneLoaderAnimation : SceneLoaderAnimation
{
    [SerializeField] private Game _game;

    private void OnEnable()
    {
        _game.Started += Hide;
    }

    private void OnDisable()
    {
        _game.Started -= Hide;
    }

    private void Start()
    {
        MakeVisible();
    }
}
