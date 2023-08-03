using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private bool _paused;

    public bool Paused => _paused;

    public event Action GameStarted;
    public event Action GamePaused;
    public event Action GameResumed;
    public event Action QuitDialogOpened;
    public event Action QuitDialogClosed;
    public event Action LoadGame;
    public event Action LoadTitle;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void StartGame()
    {
        _paused = false;
        GameStarted?.Invoke();
    }

    public void TogglePause()
    {
        _paused = !_paused;
        Time.timeScale = _paused == true ? 0 : 1;

        if (_paused == true)
        {
            GamePaused?.Invoke();
        }
        else
        {
            GameResumed?.Invoke();
        }
    }

    public void ToggleQuitDialog()
    {
        _paused = !_paused;
        Time.timeScale = _paused == true ? 0 : 1;

        if (_paused == true)
        {
            QuitDialogOpened?.Invoke();
        }
        else
        {
            QuitDialogClosed?.Invoke();
        }
    }

    public void RestartGame()
    {
        LoadGame?.Invoke();
    }

    public void LoadTitleSceen()
    {
        LoadTitle?.Invoke();
    }

    private void SetPaused()
    {
        _paused = true;
    }

    private void SetUnpaused()
    {
        _paused = false;
    }
}
