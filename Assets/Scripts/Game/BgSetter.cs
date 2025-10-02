using System;
using System.Collections;
using System.Collections.Generic;
using Game.UserData;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BgSetter : MonoBehaviour
{
    [SerializeField] private Image _bg;
    [SerializeField] private BackgroundsConfig _backgroundsConfig;
    
    [Inject]
    private SaveSystem _saveSystem;

    private void Awake()
    {
        _bg.sprite = _backgroundsConfig.Items[_saveSystem.GetData().UserInventoryData.UsedBackgroundSkin].ItemSprite;
    }
}
