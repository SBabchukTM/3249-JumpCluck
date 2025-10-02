using Game.Audio;
using Game.UserData;
using UnityEngine;
using Zenject;

namespace States
{
    public class BootstrapController : IInitializable
    {
        private readonly StateMachine _stateMachine;
        private readonly AudioPlayer _audioPlayer;
        private readonly SaveSystem _saveSystem;
        
        public BootstrapController(StateMachine stateMachine,
            AudioPlayer audioPlayer,
            SaveSystem saveSystem)
        {
            _stateMachine = stateMachine;
            _audioPlayer = audioPlayer;
            _saveSystem = saveSystem;
        }

        public async void Initialize()
        {
            _saveSystem.Load();
            SetAudioVolume();
            await _stateMachine.EnterState<LoadingStateController>();        
        }

        private void SetAudioVolume()
        {
            Debug.Log(_saveSystem == null);
            Debug.Log(_saveSystem.GetData() == null);
            Debug.Log(_audioPlayer == null);
            _audioPlayer.SetMusicVolume(_saveSystem.GetData().SettingsData.MusicVolume);
            _audioPlayer.SetSoundVolume(_saveSystem.GetData().SettingsData.SoundVolume);
        }
    }
}