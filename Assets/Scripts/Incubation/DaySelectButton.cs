using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DaySelectButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private int _value;
    
    public event Action<int> OnClick;

    private void Awake()
    {
        _button.onClick.AddListener(() => OnClick?.Invoke(_value));
        _text.text = _value.ToString();
    }

    public void SetSprite(Sprite sprite) => _button.image.sprite = sprite;
}
