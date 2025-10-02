using System;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopEggDisplay : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _priceText;

    private int _itemId;
    private int _itemPrice;

    public event Action<int, int, GameObject> OnClicked;

    public void Initialize(EggItem data, int id)
    {
        _image.sprite = data.EggSprite;
        _priceText.text = data.Price.ToString();
            
        _itemId = id;
        _itemPrice = data.Price;
            
        _button.onClick.AddListener(() => OnClicked?.Invoke(_itemId, _itemPrice, gameObject));
    }
}
