using System;
using Game.Audio;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Popups
{
    public class PrivacyPolicyPopup : Popup
    {
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _closeButton.onClick.AddListener(DestroyPopup);
        }
    }
}