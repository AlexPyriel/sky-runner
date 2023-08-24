using UnityEngine;
using IJunior.TypedScenes;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLoaderAnimation _sceneLoaderAnimation;
    [SerializeField] private GameConfig _gameConfig;

    private float _loadDelay = 1.7f;

    public void LoadGameScene()
    {
        _sceneLoaderAnimation.Reveal();
        Invoke(nameof(LoadGame), _loadDelay);
    }

    private void LoadStartcene()
    {
        _sceneLoaderAnimation.Reveal();
        Start_Scene.Load();
    }

    private void LoadGame()
    {
        Game_Scene.Load(_gameConfig);
    }
}
