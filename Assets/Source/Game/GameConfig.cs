using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "GameConfig/Create Game Config")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private bool _canReview;
    [SerializeField] private bool _fullGameUnlocked;

    public bool CanReview => _canReview;
    public bool FullGameUnlocked => _fullGameUnlocked;

    public void SetReviewAbility(bool value)
    {
        _canReview = value;
    }

    public void UnlockFullGame()
    {
        _fullGameUnlocked = true;
    }
}
