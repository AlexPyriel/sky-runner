using System;
using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Game : MonoBehaviour, ISceneLoadHandler<GameConfig>
{
    public enum Routes { Right, Left }

    [SerializeField] private Player _player;
    [SerializeField] private GameState _gameState;
    private GameConfig _gameConfig;

    public event Action<int> ScoreChanged;


    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        _player.Collected += OnCollected;
        _gameState.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _player.Collected -= OnCollected;
        _gameState.GameStarted -= OnGameStarted;
    }

    public void OnSceneLoaded(GameConfig argument)
    {
        _gameConfig = argument;
    }

    public void OnGameStarted()
    {
        Time.timeScale = 1;

        // if (_currentRoutine != null)
        //     StopCoroutine(_currentRoutine);

        // if (_hintsEnabled)
        //     StartCoroutine(SpawnHint());
        // else
        //     StartSpawnRoutine();
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
