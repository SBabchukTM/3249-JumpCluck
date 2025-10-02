using System;
using System.Collections.Generic;
using System.Linq;
using Habits;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Popups
{
    public class NewHabitPopup : Popup
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _calendarButton;
        
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private TextMeshProUGUI _daysSelectedText;
        
        [SerializeField] private List<HabitExampleButton> _examples;
        [SerializeField] private PageScroller _pageScroller;

        [SerializeField] private GameObject _eggSelectPagePrefab;

        [SerializeField] private EggsConfig _eggsConfig;

        [SerializeField] private RectTransform _content;
        
        [Inject]
        private HabitsService _habitsService; 
        
        [Inject]
        private InventoryService _inventoryService;
        
        public event Action<string, int, int> OnCreated;
        
        private List<int> EggIds = new List<int>();

        [SerializeField] private List<DaySelectButton> _daySelectButtons;
        [SerializeField] private Sprite _selectSprite;
        [SerializeField] private Sprite _deselectSprite;
        [SerializeField] private GameObject _calender;
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(DestroyPopup);
            _createButton.onClick.AddListener(() =>
            {
                OnCreated?.Invoke(_nameField.text, Convert.ToInt32(_daysSelectedText.text), EggIds[_pageScroller.CurrentPage]);
            });
            
            foreach (var example in _examples) 
                example.OnClick += text => _nameField.text = text;

            CreatePages();

            _calendarButton.onClick.AddListener(() => _calender.SetActive(!_calender.activeSelf));
            
            _daysSelectedText.text = 1.ToString();
            for (int i = 0; i < _daySelectButtons.Count; i++) 
                _daySelectButtons[i].SetSprite(_deselectSprite);

            UpdateSelectedDayVisual(1);
            
            for (int i = 0; i < _daySelectButtons.Count; i++) 
                _daySelectButtons[i].OnClick += (value) =>
                {
                    UpdateSelectedDayVisual(value);
                    _daysSelectedText.text = value.ToString();
                    _calender.SetActive(false);
                };
        }

        private void UpdateSelectedDayVisual(int day)
        {
            int prevDay = Convert.ToInt32(_daysSelectedText.text);
            _daySelectButtons[prevDay - 1].SetSprite(_deselectSprite);
            
            _daysSelectedText.text = day.ToString();
            _daySelectButtons[day - 1].SetSprite(_selectSprite);
        }

        private void CreatePages()
        {
            EggIds = GetPossibleEggIds();
            
            List<RectTransform> pageTransforms = new List<RectTransform>();

            for (int i = 0; i < EggIds.Count; i++)
            {
                var id = EggIds[i];
                var page = Instantiate(_eggSelectPagePrefab, _content);
                page.GetComponentInChildren<Image>().sprite = _eggsConfig.Items[id].EggSprite;
                pageTransforms.Add(page.GetComponent<RectTransform>());
            }
            
            _pageScroller.BuildPages(pageTransforms);
        }
        
        private List<int> GetPossibleEggIds()
        {
            var usedIds = _habitsService.GetHabits();
            var purchasedIds = _inventoryService.GetPurchasedEggs();
            
            List<int> ids = new List<int>();

            for (int i = 0; i < purchasedIds.Count; i++)
            {
                var id = purchasedIds[i];
                if(usedIds.Count(x => x.EggId == id) != 0)
                    continue;
                    
                ids.Add(id);
            }
            
            return ids;
        }
    }
}