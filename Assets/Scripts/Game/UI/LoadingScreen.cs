using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : GameScreen
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _loadTimer;

    public async UniTask Load()
    {
        _slider.value = 0;
        _slider.DOValue(1, _loadTimer);
        
        await UniTask.WaitForSeconds(_loadTimer);
    }
}
