using UnityEngine;
using IJunior.TypedScenes;

[RequireComponent(typeof(Animator), typeof(CanvasGroup))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameConfig _gameConfig;

    private Animator _animator;
    private CanvasGroup _prefabCanvasGroup;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _prefabCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        // prefab used on title scene where there's no _game component
        if (_game != null)
        {
            _game.Started += OnGameStarted;
        }
    }

    private void OnDisable()
    {
        // prefab used on title scene where there's no _game component
        if (_game != null)
        {
            _game.Started -= OnGameStarted;
        }
    }

    private void Start()
    {
        _prefabCanvasGroup.alpha = 1;

        if (_game == null)
            OnGameStarted();
    }

    private void OnGameStarted()
    {
        _animator.SetTrigger(AnimatorSceneLoader.Params.Hide);
        Debug.Log("Work");
        Time.timeScale = 1;
    }

    public void LoadGameScene()
    {
        _animator.SetTrigger(AnimatorSceneLoader.Params.Reveal);
        Game_Scene.Load(_gameConfig);
    }

    private void LoadTitleScene()
    {
        Time.timeScale = 1;
        _animator.SetTrigger(AnimatorSceneLoader.Params.Reveal);
        Start_Scene.Load();
    }
}
