using System;
using Habits;
using UnityEngine;
using UnityEngine.UI;

namespace Incubation
{
    public class IncubatedEggDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _exclamation;
        
        private HabitData _habitData;

        public HabitData HabitData => _habitData;
        public event Action<HabitData, IncubatedEggDisplay> OnHabitClicked;


        public void Initialize(HabitData data, Sprite sprite)
        {
            _habitData = data;
            _image.sprite = sprite;
            _button.onClick.AddListener(() => OnHabitClicked?.Invoke(data, this));
        }
        
        public void SetSprite(Sprite sprite) => _image.sprite = sprite;
        
        public void EnableExclamation(bool enable) => _exclamation.SetActive(enable);
    }
}