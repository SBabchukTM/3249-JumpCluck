using UnityEngine;

namespace Game.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        [SerializeField] private AudioClip _buttonClip;
        [SerializeField] private AudioClip _popupClip;
        [SerializeField] private AudioClip _successClip;
        [SerializeField] private AudioClip _errorClip;

        public void SetMusicVolume(bool volume) => _musicSource.volume = volume ? 1 : 0;
        public void SetSoundVolume(bool volume) => _soundSource.volume = volume ? 1 : 0;
        
        public void PlayButton() => _soundSource.PlayOneShot(_buttonClip);
        public void PlayPopup() => _soundSource.PlayOneShot(_popupClip);
        public void PlaySuccess() => _soundSource.PlayOneShot(_successClip);
        public void PlayError() => _soundSource.PlayOneShot(_errorClip);
    }
}