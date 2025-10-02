using System;
using System.Collections.Generic;
using Game.Audio;
using Game.UI;
using Habits;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FarmScreen : GameScreen
{
    [SerializeField] private Button _backButton;
    [SerializeField] private ChickenFarmFactory _factory;

    private List<ChickenFarmDisplay> _chickens;
    
    public event Action OnBackButtonPressed;

    [Inject]
    private GameUIContainer _uiContainer;
    
    [Inject]
    private InventoryService _inventoryService;
    
    [Inject]
    private AudioPlayer _audioPlayer;
    
    private void Awake()
    {
        _backButton.onClick.AddListener(() => OnBackButtonPressed?.Invoke());
        _chickens = _factory.GetChickens();

        foreach (var chicken in _chickens)
        {
            chicken.OnCollectPressed += ProcessCollect;
        }
    }

    private void ProcessCollect(HabitData data)
    {
        var popup = _uiContainer.CreatePopup<HabitProgressPopup>();
        popup.SetData(data);
        _inventoryService.AddBalance(100);
        
        _audioPlayer.PlaySuccess();
    }
}
