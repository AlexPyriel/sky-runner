using UnityEngine;
using IJunior.TypedScenes;

[RequireComponent(typeof(Animator), typeof(CanvasGroup))]
public class SceneLoader : MonoBehaviour
{
    // [SerializeField] private QuizSelector _quizSelector;
    [SerializeField] private GameState _gameState;
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
        // if (_quizSelector != null)
        //     _quizSelector.Selected += LoadGame;

        if (_gameState != null)
        {
            _gameState.GameStarted += OnGameStarted;
            _gameState.LoadGame += LoadGame;
            _gameState.LoadTitle += LoadTitle;
        }
    }

    private void OnDisable()
    {
        // if (_quizSelector != null)
        //     _quizSelector.Selected -= LoadGame;

        if (_gameState != null)
        {
            _gameState.GameStarted -= OnGameStarted;
            _gameState.LoadGame -= LoadGame;
            _gameState.LoadTitle -= LoadTitle;
        }
    }

    private void Start()
    {
        _prefabCanvasGroup.alpha = 1;

        if (_gameState == null)
        {
            OnGameStarted();
        }
    }

    // public void QuitGame()
    // {
    //     Application.Quit();
    // }

    private void OnGameStarted()
    {
        _animator.SetTrigger(AnimatorSceneLoader.Params.Hide);
        Debug.Log("Work");
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        _animator.SetTrigger(AnimatorSceneLoader.Params.Reveal);
        // _animator.SetBool("Reveal1", true);
        // Debug.Log($"Clip length {_animator.GetCurrentAnimatorClipInfo(0).Length}");
        // Invoke(nameof(LoadGameScene), _animator.GetCurrentAnimatorClipInfo(0).Length);
        // Invoke(nameof(LoadGameScene), 3.4f);
        Invoke(nameof(LoadGameScene), 1.7f);
    }

    private void LoadTitle()
    {
        Time.timeScale = 1;
        _animator.SetTrigger(AnimatorSceneLoader.Params.Reveal);
        Invoke(nameof(LoadTitleScene), _animator.GetCurrentAnimatorClipInfo(0).Length);
    }

    private void LoadGameScene()
    {
        Game_Scene.Load(_gameConfig);
    }

    private void LoadTitleScene()
    {
        Start_Scene.Load();
    }
}
