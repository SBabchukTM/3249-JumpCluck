using System;
using System.Collections.Generic;
using Habits;
using Incubation;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class ChickenFarmFactory : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _firstRow;
        [SerializeField] private HorizontalLayoutGroup _secondRow;
        [SerializeField] private HorizontalLayoutGroup _thirdRow;
        [SerializeField] private HorizontalLayoutGroup _fourthRow;

        [SerializeField] private EggsConfig _eggsConfig;
        [SerializeField] private GameObject _prefab;

        [Inject] private HabitsService _habitsService;

        [Inject] private GameObjectFactory _factory;

        public List<ChickenFarmDisplay> GetChickens()
        {
            var eggs = new List<ChickenFarmDisplay>();

            var habits = _habitsService.GetHabits();

            var timeNow = DateTime.Now;

            foreach (var habit in habits)
            {
                if (!habit.Hatched)
                    continue;

                var display = _factory.Create<ChickenFarmDisplay>(_prefab);
                var targetRow = GetTargetRow();

                display.transform.SetParent(targetRow.transform, false);
                var sprite = _eggsConfig.Items[habit.EggId].ChickenSprite;

                bool showCoins = false;
                
                if(habit.LastDayChecked == String.Empty)
                    showCoins = true;
                else
                {
                    var date = DateTime.ParseExact(habit.LastDayChecked, "o", null);
                    showCoins = timeNow.Date != date.Date;
                }
                
                display.Initialize(habit, sprite, showCoins);
                eggs.Add(display);
            }

            return eggs;
        }

        private HorizontalLayoutGroup GetTargetRow()
        {
            if (_firstRow.transform.childCount < 4)
                return _firstRow;
            if (_secondRow.transform.childCount < 4)
                return _secondRow;
            if (_thirdRow.transform.childCount < 4)
                return _thirdRow;
            return _fourthRow;
        }
    }
}