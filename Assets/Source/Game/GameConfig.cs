using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "GameConfig/Create Game Config")]
public class GameConfig : ScriptableObject
{
    // [SerializeField] private QuizObject _currentQuiz;
    [SerializeField] private bool _hintsEnabled;
    [SerializeField] private bool _inOrder;
    [SerializeField] private bool _canReview;
    [SerializeField] private bool _fullGameUnlocked;

    // public QuizObject CurrentQuiz => _currentQuiz;
    public bool HintsEnabled => _hintsEnabled;
    public bool InOrder => _inOrder;
    public bool CanReview => _canReview;
    public bool FullGameUnlocked => _fullGameUnlocked;

    // public void SetQuiz(QuizObject quizObject)
    // {
    //     _currentQuiz = quizObject;
    // }

    public void ToggleQuizHints(bool value)
    {
        _hintsEnabled = value;
    }

    public void SetOrder(bool value)
    {
        _inOrder = value;
    }

    public void SetReviewAbility(bool value)
    {
        _canReview = value;
    }

    public void UnlockFullGame()
    {
        _fullGameUnlocked = true;
    }
}
