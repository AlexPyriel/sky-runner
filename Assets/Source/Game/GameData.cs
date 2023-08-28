using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] private bool _canReview;

    public bool CanReview => _canReview;

    public GameData(bool canReview)
    {
        _canReview = canReview;
    }

    public void SetReviewAbility(bool value)
    {
        _canReview = value;
    }
}
