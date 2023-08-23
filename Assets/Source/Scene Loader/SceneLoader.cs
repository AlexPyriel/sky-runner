using UnityEngine;
using IJunior.TypedScenes;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLoaderAnimation _sceneLoaderAnimation;
    [SerializeField] private GameConfig _gameConfig;

    public void LoadGameScene()
    {
        _sceneLoaderAnimation.Reveal();
        Game_Scene.Load(_gameConfig);
    }

    private void LoadTitleScene()
    {
        _sceneLoaderAnimation.Reveal();
        Start_Scene.Load();
    }
}
