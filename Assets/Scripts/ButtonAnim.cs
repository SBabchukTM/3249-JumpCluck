using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonAnim : MonoBehaviour
{
    [Inject]
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            _audioPlayer.PlayButton();
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(Vector3.one * 1.05f, 0.15f));
            seq.Append(transform.DOScale(Vector3.one, 0.2f));
            seq.SetLink(gameObject);
        });
    }
}
