using UnityEngine;
using IJunior.TypedScenes;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private GameDataContainer _gameDataContainer;
    [SerializeField] private SceneLoaderAnimation _sceneLoaderAnimation;
    [SerializeField] private GameData _gameData;

    private delegate void LoadScene();
    private LoadScene _loadScene;

    public enum Scenes { GameScene, LobbyScene }

    private void OnEnable()
    {
        _gameDataContainer.TransferDataToScene += TryLoadScene;
    }

    private void OnDisable()
    {
        _gameDataContainer.TransferDataToScene -= TryLoadScene;
        _loadScene = null;
    }

    private void TryLoadScene(GameData gameData, Scenes scene)
    {
        _gameData = gameData;

        _loadScene = scene switch
        {
            Scenes.GameScene => LoadGameScene,
            Scenes.LobbyScene => LoadStartcene,
            _ => null,
        };

        _loadScene?.Invoke();
    }

    private void LoadGameScene()
    {
        _sceneLoaderAnimation.Reveal();
        Invoke(nameof(LoadGame), _sceneLoaderAnimation.AnimationDelay);
    }

    private void LoadStartcene()
    {
        _sceneLoaderAnimation.Reveal();
        Invoke(nameof(LoadStart), _sceneLoaderAnimation.AnimationDelay);
    }

    private void LoadGame()
    {
        Game_Scene.LoadAsync(_gameData);
    }

    private void LoadStart()
    {
        Start_Scene.LoadAsync(_gameData);
    }
}
