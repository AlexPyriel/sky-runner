using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CanvasGroup))]
public class SceneLoaderAnimation : MonoBehaviour
{
    private Animator _animator;
    private CanvasGroup _prefabCanvasGroup;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _prefabCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Reveal()
    {
        _animator.SetTrigger(AnimatorSceneLoader.Params.Reveal);
    }

    protected void Hide()
    {
        _prefabCanvasGroup.alpha = 1;
        _animator.SetTrigger(AnimatorSceneLoader.Params.Hide);
    }
}
