using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameUIContainer : MonoBehaviour
    {
        [SerializeField] private float _fadeTime = 0.25f;
        
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private CanvasGroup _fadeCanvas;
        
        [SerializeField] private GameScreen[] _screens;
        [SerializeField] private Popup[] _popups;
        

        private Dictionary<Type, GameScreen> _gameScreens = new();
        private Dictionary<Type, Popup> _gamePopups = new();
        
        [Inject]
        private GameObjectFactory _gameObjectFactory;
        
        private List<GameScreen> _openedScreens = new();
        
        private void Awake()
        {
            foreach (GameScreen screen in _screens) 
                _gameScreens.Add(screen.GetType(), screen);
            
            foreach (Popup popup in _popups) 
                _gamePopups.Add(popup.GetType(), popup);
        }

        public async UniTask<T> CreateScreen<T>() where T : GameScreen
        {
            if (_gameScreens.TryGetValue(typeof(T), out GameScreen prefab))
            {
                var screen = _gameObjectFactory.Create<T>(prefab.gameObject);
                _openedScreens.Add(screen);
                screen.transform.SetParent(_canvas, false);

                await FadeOut();
                
                return screen;
            }
            
            throw new Exception($"Screen {typeof(T)} not found");
        }
        
        public T CreatePopup<T>() where T : Popup
        {
            if (_gamePopups.TryGetValue(typeof(T), out Popup prefab))
            {
                var popup = _gameObjectFactory.Create<T>(prefab.gameObject);
                popup.transform.SetParent(_canvas, false);
                return popup;
            }
            
            throw new Exception($"Popup {typeof(T)} not found");
        }

        public async UniTask HideScreen<T>() where T : GameScreen
        {
            for (int i = 0; i < _openedScreens.Count; i++)
            {
                if (_openedScreens[i].GetType() == typeof(T))
                {
                    var screen = _openedScreens[i];
                    _openedScreens.RemoveAt(i);

                    await FadeIn();
                    
                    Destroy(screen.gameObject);
                }
            }
        }

        private async UniTask FadeIn()
        {
            _fadeCanvas.alpha = 0;
            _fadeCanvas.interactable = true;
            _fadeCanvas.blocksRaycasts = true;
            
            _fadeCanvas.DOFade(1, _fadeTime);
            await UniTask.WaitForSeconds(_fadeTime);
            
            _fadeCanvas.interactable = false;
            _fadeCanvas.blocksRaycasts = false;
        }
        
        private async UniTask FadeOut()
        {
            _fadeCanvas.alpha = 1;
            _fadeCanvas.interactable = true;
            _fadeCanvas.blocksRaycasts = true;
            
            _fadeCanvas.DOFade(0, _fadeTime);
            await UniTask.WaitForSeconds(_fadeTime);
            
            _fadeCanvas.interactable = false;
            _fadeCanvas.blocksRaycasts = false;
        }
    }
}