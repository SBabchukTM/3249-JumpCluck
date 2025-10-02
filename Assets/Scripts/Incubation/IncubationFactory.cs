using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.UserData;
using Habits;
using Incubation;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IncubationFactory : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup _firstRow;
    [SerializeField] private HorizontalLayoutGroup _secondRow;
    [SerializeField] private HorizontalLayoutGroup _thirdRow;
    [SerializeField] private EggsConfig _eggsConfig;
    [SerializeField] private GameObject _prefab;
    
    [SerializeField] private Transform _addButton;
    
    [Inject]
    private HabitsService _habitsService;

    [Inject] private GameObjectFactory _factory;

    public IncubatedEggDisplay AddEgg(HabitData data)
    {
        _addButton.transform.SetParent(null);
        _addButton.gameObject.SetActive(false);
        
        if(data.Hatched)
            return null;
        
        var display = _factory.Create<IncubatedEggDisplay>(_prefab);
        var targetRow = GetTargetRow();
    
        if (targetRow == null)
            return null;
    
        display.transform.SetParent(targetRow.transform, false);
    
        var sprite = _eggsConfig.Items[data.EggId].EggSprite;
        
        if(data.DaysChecked == data.Duration)
            sprite = _eggsConfig.Items[data.EggId].CrackedEggSprite;
        
        display.Initialize(data, sprite);

        TryPlaceAddButton();
        return display;
    }

    public Sprite GetCrackedEggSprite(int eggId) => _eggsConfig.Items[eggId].CrackedEggSprite;
    public Sprite GetChickenSprite(int eggId) => _eggsConfig.Items[eggId].ChickenSprite;
    
    public List<IncubatedEggDisplay> GetEggs()
    {
        List<IncubatedEggDisplay> eggs = new List<IncubatedEggDisplay>();
        
        _addButton.transform.SetParent(null);
        _addButton.gameObject.SetActive(false);
    
        var habits = _habitsService.GetHabits();

        foreach (var habit in habits)
        {
            if (habit.Hatched)
                continue;
            
            var display = _factory.Create<IncubatedEggDisplay>(_prefab);
            var targetRow = GetTargetRow();
            if (targetRow == null)
                break;
        
            display.transform.SetParent(targetRow.transform, false);
            var sprite = _eggsConfig.Items[habit.EggId].EggSprite;
            display.Initialize(habit, sprite);
            eggs.Add(display);
        }

        TryPlaceAddButton();
        return eggs;
    }
    
    private void TryPlaceAddButton()
    {
        var row = GetTargetRow();
        if (row == null)
            return;

        _addButton.SetParent(row.transform, false);
        _addButton.SetAsLastSibling();
        _addButton.gameObject.SetActive(true);
        _addButton.transform.localScale = new Vector3(1, 1, 1);
    }
    
    private HorizontalLayoutGroup GetTargetRow()
    {
        if (_firstRow.transform.childCount < 3)
            return _firstRow;
        if (_secondRow.transform.childCount < 3)
            return _secondRow;
        if (_thirdRow.transform.childCount < 3)
            return _thirdRow;

        return null;
    }
}
