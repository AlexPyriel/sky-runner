using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator), typeof(CanvasGroup))]
public class SceneLoaderAnimation : MonoBehaviour
{
    private Animator _animator;
    private CanvasGroup _prefabCanvasGroup;
    private readonly float _animationDelay = 0.4f;

    public float AnimationDelay => _animationDelay;

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
        _animator.SetTrigger(AnimatorSceneLoader.Params.Hide);
    }

    protected void MakeVisible()
    {
        _prefabCanvasGroup.alpha = 1;
    }
}
