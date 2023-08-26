using UnityEngine;

public class StartSceneLoaderAnimation : SceneLoaderAnimation
{
    protected void Start()
    {
        MakeVisible();
        Hide();
    }
}
