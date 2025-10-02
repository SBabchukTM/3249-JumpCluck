using System;
using Habits;
using UnityEngine;
using UnityEngine.UI;

public class ChickenFarmDisplay : MonoBehaviour
{
    [SerializeField] private Button _coinButton;
    [SerializeField] private Image _image;

    private HabitData _habitData;

    public event Action<HabitData> OnCollectPressed;
    
    public void Initialize(HabitData data, Sprite sprite, bool enableCoin)
    {
        _habitData = data;
        _image.sprite = sprite;

        if (enableCoin)
        {
            _coinButton.gameObject.SetActive(true);
            _coinButton.onClick.AddListener(() =>
            {
                OnCollectPressed?.Invoke(_habitData);
                _coinButton.gameObject.SetActive(false);
            });
        }
    }
}
