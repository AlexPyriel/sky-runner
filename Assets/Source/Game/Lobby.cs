using System;
using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;

public class Lobby : GameDataContainer, ISceneLoadHandler<GameData>
{
    [SerializeField] private StartSceneUI _startSceneUI;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        if (_gameData == null)
            _gameData = ScriptableObject.CreateInstance<GameData>();
    }

    private void Update()
    {

    }

    public void OnSceneLoaded(GameData argument)
    {
        _gameData = argument;
        _startSceneUI.ShowLobbyPanel();
    }

    public void StartGame()
    {
        InvokeTransferDataToScene(SceneLoader.Scenes.GameScene);
    }
}
