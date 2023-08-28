using UnityEngine;
using IJunior.TypedScenes;
using System;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private GameDataContainer _gameDataContainer;
    [SerializeField] private SceneLoaderAnimation _sceneLoaderAnimation;
    [SerializeField] private GameData _gameData;

    private event Action LoadScene;

    public enum Scenes { GameScene, LobbyScene }

    private void OnEnable()
    {
        _gameDataContainer.TransferDataToScene += TryLoadScene;
    }

    private void OnDisable()
    {
        _gameDataContainer.TransferDataToScene -= TryLoadScene;
    }

    private void TryLoadScene(GameData gameData, Scenes scene)
    {
        _gameData = gameData;

        LoadScene = scene switch
        {
            Scenes.GameScene => LoadGameScene,
            Scenes.LobbyScene => LoadStartcene,
            _ => null,
        };

        LoadScene?.Invoke();
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
