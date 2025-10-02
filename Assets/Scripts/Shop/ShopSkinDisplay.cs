using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopSkinDisplay : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private GameObject _priceParent;
        [SerializeField] private GameObject _statusParent;

        private int _itemId;
        private int _itemPrice;

        public event Action<int, int> OnClicked;

        public void Initialize(SkinItem data, int id, SkinStatus status)
        {
            _image.sprite = data.PreviewSprite;
            _priceText.text = data.Price.ToString();
            
            _itemId = id;
            _itemPrice = data.Price;
            
            _button.onClick.AddListener(() => OnClicked?.Invoke(_itemId, _itemPrice));

            SetStatus(status);
        }

        public void SetStatus(SkinStatus status)
        {
            if (status == SkinStatus.Purchased)
            {
                _statusParent.SetActive(true);
                _priceParent.SetActive(false);
                _statusText.text = "PURCHASED";
            }

            if (status == SkinStatus.Used)
            {
                _statusParent.SetActive(true);
                _priceParent.SetActive(false);
                _statusText.text = "USED";
            }

            if (status == SkinStatus.NotPurchased)
            {
                _statusParent.SetActive(false);
                _priceParent.SetActive(true);
            }
        }
    }
}