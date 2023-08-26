using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum Routes { Left, Middle, Right }

    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Player _player;
    [SerializeField] private SceneLoader _sceneLoader;

    public event Action Started;
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
        _player.ObstacleCollided += OnObstacleCollided;
    }

    private void OnDisable()
    {
        _player.Collected -= OnCollected;
        _player.ObstacleCollided -= OnObstacleCollided;
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

    private void OnObstacleCollided()
    {
        _sceneLoader.LoadStartcene();
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
