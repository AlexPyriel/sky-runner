using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{
    [SerializeField] private bool _canReview;

    public bool CanReview => _canReview;

    public void SetReviewAbility(bool value)
    {
        _canReview = value;
    }
}
