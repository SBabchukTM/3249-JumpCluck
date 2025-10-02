using System;
using Game.Audio;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Popups
{
    public class SettingsPopup : Popup
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Button _ppButton;
        [SerializeField] private Button _touButton;
        [SerializeField] private Button _closeButton;
        
        [Inject]
        private SaveSystem _saveSystem;
        
        [Inject]
        private GameUIContainer _gameUIContainer;

        private void Awake()
        {
            SetData();
            _musicToggle.onValueChanged.AddListener((value) =>
            {
                _saveSystem.GetData().SettingsData.MusicVolume = value;
                _audioPlayer.SetMusicVolume(value);
            }); 
            
            _soundToggle.onValueChanged.AddListener((value) =>
            {
                _saveSystem.GetData().SettingsData.SoundVolume = value;
                _audioPlayer.SetSoundVolume(value);
            }); 
            
            _ppButton.onClick.AddListener(() => _gameUIContainer.CreatePopup<PrivacyPolicyPopup>());
            _touButton.onClick.AddListener(() => _gameUIContainer.CreatePopup<TermsOfUsePopup>());
            
            _closeButton.onClick.AddListener(DestroyPopup);
        }

        private void SetData()
        {
            _musicToggle.isOn = _saveSystem.GetData().SettingsData.MusicVolume;
            _soundToggle.isOn = _saveSystem.GetData().SettingsData.SoundVolume;
        }
    }
}