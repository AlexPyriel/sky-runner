using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLoaderAnimation _sceneLoaderAnimation;
    [SerializeField] private GameConfig _gameConfig;

    private const string StartScene = nameof(StartScene);
    private const string GameScene = nameof(GameScene);

    public void LoadGameScene()
    {
        _sceneLoaderAnimation.Reveal();
        Invoke(nameof(LoadGame), _sceneLoaderAnimation.AnimationDelay);
    }

    public void LoadStartcene()
    {
        _sceneLoaderAnimation.Reveal();
        Invoke(nameof(LoadStart), _sceneLoaderAnimation.AnimationDelay);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);

    }
    private void LoadStart()
    {
        SceneManager.LoadScene(StartScene, LoadSceneMode.Single);
    }
}
