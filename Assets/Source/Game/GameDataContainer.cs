using System;
using UnityEngine;

public class GameDataContainer : MonoBehaviour
{
    [SerializeField] protected GameData _gameData;

    public event Action<GameData, SceneLoader.Scenes> TransferDataToScene;

    protected void InvokeTransferDataToScene(SceneLoader.Scenes scene)
    {
        TransferDataToScene?.Invoke(_gameData, scene);
    }
}
