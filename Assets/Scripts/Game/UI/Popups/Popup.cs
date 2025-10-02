using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using UnityEngine;
using Zenject;

public class Popup : MonoBehaviour
{
    [Inject]
    protected AudioPlayer _audioPlayer;

    private void Start()
    {
        _audioPlayer.PlayPopup();
    }

    protected void DestroyPopup()
    {
        _audioPlayer.PlayButton();
        Destroy(gameObject);
    }
}
