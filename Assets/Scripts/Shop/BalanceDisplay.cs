using System;
using System.Collections;
using System.Collections.Generic;
using Game.UserData;
using Shop;
using TMPro;
using UnityEngine;
using Zenject;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    
    [Inject]
    private InventoryService _inventoryService;

    private void Awake()
    {
        _balanceText.text = _inventoryService.GetBalance().ToString();
        _inventoryService.OnBalanceChanged += UpdateBalance;
    }

    private void OnDestroy()
    {
        _inventoryService.OnBalanceChanged -= UpdateBalance;
    }

    private void UpdateBalance(int obj)
    {
        _balanceText.text = _inventoryService.GetBalance().ToString();
    }
}
