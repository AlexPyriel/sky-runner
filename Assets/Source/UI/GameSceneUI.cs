using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;

    private float _gameOverDelay = 3f;

    private void OnEnable()
    {
        _gameState.GameStarted += OnGameStarted;
        _gameState.GamePaused += ShowPausePanel;
        _gameState.GameResumed += HidePausePanel;
    }

    private void OnDisable()
    {
        _gameState.GameStarted -= OnGameStarted;
        _gameState.GamePaused -= ShowPausePanel;
        _gameState.GameResumed -= HidePausePanel;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void ShowPausePanel()
    {
        _pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        _pausePanel.SetActive(false);
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private void OnGameStarted()
    {
        _gamePanel.SetActive(false);
    }

    private void OnGameOver()
    {
        // _quizPanel.SetActive(false);
        Invoke(nameof(ShowGameOverPanel), _gameOverDelay);
    }
}
