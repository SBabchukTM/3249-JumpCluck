using System;
using System.Collections;
using System.Collections.Generic;
using Game.UI;
using Game.UI.Popups;
using Habits;
using Incubation;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IncubationScreen : GameScreen
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _onboardingButton;
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _farmButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _newHabitButton;
    
    [SerializeField] private IncubationFactory _incubationFactory;
    
    public event Action OnOnboardingPressed;
    public event Action OnFarmPressed;
    public event Action OnShopPressed;
    
    [Inject]
    private GameUIContainer _gameUIContainer;
    
    [Inject]
    private HabitsService _habitsService;

    private List<IncubatedEggDisplay> _eggs;
    
    private void Awake()
    {
        _settingsButton.onClick.AddListener(() => _gameUIContainer.CreatePopup<SettingsPopup>());
        _profileButton.onClick.AddListener(() => _gameUIContainer.CreatePopup<ProfilePopup>());
        _onboardingButton.onClick.AddListener(() => OnOnboardingPressed?.Invoke());
        _farmButton.onClick.AddListener(() => OnFarmPressed?.Invoke());
        _shopButton.onClick.AddListener(() => OnShopPressed?.Invoke());

        _eggs = _incubationFactory.GetEggs();

        var date = DateTime.Now;
        
        foreach (var egg in _eggs)
        {
            egg.OnHabitClicked += ProcessEggClicked;

            if (egg.HabitData.LastDayChecked == String.Empty)
            {
                egg.EnableExclamation(true);
                continue;
            }
            
            var eggDate = DateTime.ParseExact(egg.HabitData.LastDayChecked, "o", null);
            if(eggDate.Date != date.Date)
                egg.EnableExclamation(true);
        }
        
        _newHabitButton.onClick.AddListener(OpenNewHabit);
    }

    private void ProcessEggClicked(HabitData data, IncubatedEggDisplay egg)
    {
        if (data.DaysChecked == data.Duration)
        {
            data.Hatched = true;
            Destroy(egg.gameObject);
            
            var hatchPopup = _gameUIContainer.CreatePopup<ChickenHatchPopup>();
            hatchPopup.SetImage(_incubationFactory.GetChickenSprite(data.EggId));
            return;
        }
        
        var popup = _gameUIContainer.CreatePopup<HabitProgressPopup>();
        popup.OnCheckedIn += () =>
        {
            egg.EnableExclamation(false);
            if (data.Duration == data.DaysChecked) 
                egg.SetSprite(_incubationFactory.GetCrackedEggSprite(data.EggId));
        };
        popup.SetData(data);
    }

    private void OpenNewHabit()
    {
       var popup = _gameUIContainer.CreatePopup<NewHabitPopup>();
       
       popup.OnCreated += (name, days, eggId) =>
       {
           var egg = _incubationFactory.AddEgg(_habitsService.AddNewHabit(name, days, eggId));

           if (egg)
           {
               egg.OnHabitClicked += ProcessEggClicked;
               egg.EnableExclamation(true);
           }
           
           Destroy(popup.gameObject);
       };
    }
}
