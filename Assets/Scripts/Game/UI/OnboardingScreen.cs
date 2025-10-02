using System;
using System.Collections;
using System.Collections.Generic;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OnboardingScreen : GameScreen
{
    [SerializeField] private Button _finishButton;
    
    public event Action OnTutorialFinished;
    
    private void Awake()
    {
        _finishButton.onClick.AddListener(() => OnTutorialFinished?.Invoke());
    }
}
