using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private bool _gamePaused;

    public event Action GamePaused;
    public event Action GameResumed;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void TogglePause()
    {
        _gamePaused = !_gamePaused;
        Time.timeScale = _gamePaused == true ? 0 : 1;

        if (_gamePaused == true)
            GamePaused?.Invoke();
        else
            GameResumed?.Invoke();
    }
}
