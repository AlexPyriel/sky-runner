using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;

    private float _gameOverDelay = 3f;

    private void OnEnable()
    {
        _game.Started += OnGameStarted;
        _gameState.GamePaused += ShowPausePanel;
        _gameState.GameResumed += HidePausePanel;
    }

    private void OnDisable()
    {
        _game.Started -= OnGameStarted;
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

    private void OnGameStarted()
    {
        _startPanel.SetActive(false);
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private void OnGameOver()
    {
        Invoke(nameof(ShowGameOverPanel), _gameOverDelay);
    }
}
