using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _lobbyPanel;

    public void ShowLobbyPanel()
    {
        _lobbyPanel.SetActive(true);
        _titlePanel.SetActive(false);
    }
}
