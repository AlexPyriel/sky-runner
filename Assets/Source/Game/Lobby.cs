using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += Test;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= Test;
    }
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Test(Scene current, Scene next)
    {
        if (current.name == null)
            Debug.Log($"{next.name} scene loaded");
    }
}
