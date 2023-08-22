using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    // [SerializeField] private QuizSelector _quizSelector;
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _lobbyPanel;

    private void OnEnable()
    {
        // _quizSelector.Selected += HideGameModeDialog;
    }

    private void OnDisable()
    {
        // _quizSelector.Selected -= HideGameModeDialog;
    }


    public void ShowLobbyPanel()
    {
        _lobbyPanel.SetActive(true);
        _titlePanel.SetActive(false);
    }

    // private void HideGameModeDialog()
    // {
    //     _gameModePanel.SetActive(false);
    // }
}
