using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Habits
{
    public class HabitExampleButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _text;
        [SerializeField] private TextMeshProUGUI _textMesh;
        
        public event Action<string> OnClick;
        
        private void Awake()
        {
            _button.onClick.AddListener(() => OnClick?.Invoke(_text));
            _textMesh.text = _text;
        }
    }
}