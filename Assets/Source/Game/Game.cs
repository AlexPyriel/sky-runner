using System;
using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;

public class Game : GameDataContainer, ISceneLoadHandler<GameData>
{

    [SerializeField] private Player _player;

    public event Action Started;
    public event Action<int> ScoreChanged;

    public enum Routes { Left, Middle, Right }

    private void OnEnable()
    {
        _player.Collected += OnCollected;
        _player.ObstacleCollided += OnObstacleCollided;
    }

    private void OnDisable()
    {
        _player.Collected -= OnCollected;
        _player.ObstacleCollided -= OnObstacleCollided;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {

    }

    public void OnSceneLoaded(GameData argument)
    {
        _gameData = argument;
        _gameData.SetReviewAbility(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Started?.Invoke();
        // if (_currentRoutine != null)
        //     StopCoroutine(_currentRoutine);

        // if (_hintsEnabled)
        //     StartCoroutine(SpawnHint());
        // else
        //     StartSpawnRoutine();
    }

    private void ReturnToLobby()
    {
        InvokeTransferDataToScene(SceneLoader.Scenes.LobbyScene);
    }

    private void OnObstacleCollided()
    {
        ReturnToLobby();
    }


    private void OnCollected()
    {
        // if (_grassAmount < _maxCollected)
        // {
        //     _grassAmount++;
        //     ScoreChanged?.Invoke(_grassAmount);
        // }

        // if (_grassAmount == _maxCollected)
        // {
        //     var quizTrigger = _quizTriggerSpawner.Spawn();

        //     if (_lastGrassInstanceSpawned != null)
        //     {
        //         quizTrigger.transform.position = new Vector3(0, 1, _lastGrassInstanceSpawned.transform.position.z);
        //         Destroy(_lastGrassInstanceSpawned);
        //     }

        //     if (_lastObstacleInstanceSpawned != null)
        //         Destroy(_lastObstacleInstanceSpawned);
        // }
    }


}
