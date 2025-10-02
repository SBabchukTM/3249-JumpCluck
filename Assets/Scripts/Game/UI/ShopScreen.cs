using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using Game.Audio;
using Game.UI;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopScreen : GameScreen
{
    [SerializeField] private RectTransform _fridgesContent;
    [SerializeField] private RectTransform _backgroundsContent;
    [SerializeField] private RectTransform _eggsContent;
    [SerializeField] private Button _backButton;
    
    [SerializeField] private ShopScreenFactory _shopScreenFactory;

    [SerializeField] private TextMeshProUGUI _errorText;
    
    [Inject]
    private InventoryService _inventoryService;
    
    [Inject]
    private AudioPlayer _audioPlayer;
    
    private List<ShopSkinDisplay> _backgroundItems;
    private List<ShopSkinDisplay> _fridgeItems;
    private List<ShopEggDisplay> _eggItems;
    
    private Sequence _sequence;

    public event Action OnBackPressed;
    
    private void Awake()
    {
        _backgroundItems = _shopScreenFactory.GetBackgroundItems();
        _fridgeItems = _shopScreenFactory.GetFridgeItems();
        _eggItems = _shopScreenFactory.GetEggItems();

        foreach (var item in _backgroundItems)
        {
            item.transform.SetParent(_backgroundsContent);
            item.OnClicked += ProcessBgClick;
        }

        foreach (var item in _fridgeItems)
        {
            item.transform.SetParent(_fridgesContent);
            item.OnClicked += ProcessFridgeClick;
        }

        foreach (var item in _eggItems)
        {
            item.transform.SetParent(_eggsContent);
            item.OnClicked += ProcessEggClick;
        }
        
        _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
    }

    private void ProcessBgClick(int id, int price)
    {
        if(_inventoryService.GetUsedBackgroundId() == id)
            return;

        if (_inventoryService.GetPurchasedBackgrounds().Contains(id))
        {
            _inventoryService.SetUsedBackgroundSkin(id);
            _shopScreenFactory.UpdateBackgroundStatuses(_backgroundItems);
            return;
        }

        if (_inventoryService.GetBalance() < price)
        {
            ShowError();
            return;
        }
        
        _inventoryService.AddBalance(-price);
        _inventoryService.AddPurchasedBackground(id);
        _shopScreenFactory.UpdateBackgroundStatuses(_backgroundItems);
        _audioPlayer.PlaySuccess();
    }

    private void ProcessFridgeClick(int id, int price)
    {
        if(_inventoryService.GetUsedFridgeId() == id)
            return;

        if (_inventoryService.GetPurchasedFridges().Contains(id))
        {
            _inventoryService.SetUsedFridgeSkin(id);
            _shopScreenFactory.UpdateFridgeStatuses(_fridgeItems);
            return;
        }

        if (_inventoryService.GetBalance() < price)
        {
            ShowError();
            return;
        }
        
        _inventoryService.AddBalance(-price);
        _inventoryService.AddPurchasedFridge(id);
        _shopScreenFactory.UpdateFridgeStatuses(_fridgeItems);
        _audioPlayer.PlaySuccess();
    }

    private void ShowError()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _audioPlayer.PlayError();
        _sequence.Append(_errorText.DOFade(1, 0.25f));
        _sequence.AppendInterval(0.55f);
        _sequence.Append(_errorText.DOFade(0, 0.25f));
    }

    private void ProcessEggClick(int itemId, int price, GameObject go)
    {
        if (price > _inventoryService.GetBalance())
        {
            ShowError();
            return;
        }
        
        _inventoryService.AddBalance(-price);
        _inventoryService.AddPurchasedEgg(itemId);
        _audioPlayer.PlaySuccess();
        Destroy(go);
    }
}
