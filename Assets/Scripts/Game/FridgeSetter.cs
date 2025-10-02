using System.Collections;
using System.Collections.Generic;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FridgeSetter : MonoBehaviour
{
    [SerializeField] private Image _bg;
    [SerializeField] private FridgesConfig _fridgesConfig;
    
    [Inject]
    private SaveSystem _saveSystem;

    private void Awake()
    {
        _bg.sprite = _fridgesConfig.Items[_saveSystem.GetData().UserInventoryData.UsedFridgeSkin].ItemSprite;
    }
}
