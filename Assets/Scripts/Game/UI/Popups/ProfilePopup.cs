using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups
{
    public class ProfilePopup : Popup
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(DestroyPopup);
        }
    }
}