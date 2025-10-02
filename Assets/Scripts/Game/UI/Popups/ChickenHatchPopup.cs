using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups
{
    public class ChickenHatchPopup : Popup
    {
        [SerializeField] private Button _collectButton;
        [SerializeField] private Image _image;
        
        private void Awake()
        {
            _collectButton.onClick.AddListener(DestroyPopup);
        }
        
        public void SetImage(Sprite sprite) => _image.sprite = sprite;
    }
}