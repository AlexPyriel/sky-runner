using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _lobbyPanel;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void ShowLobbyPanel()
    {
        _lobbyPanel.SetActive(true);
        _titlePanel.SetActive(false);
    }
}
