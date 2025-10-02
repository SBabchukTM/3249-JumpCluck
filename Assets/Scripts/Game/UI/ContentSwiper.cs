using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ContentSwiper : MonoBehaviour
    {
        [SerializeField] private List<Button> _rightButtons;

        [Header("Page Scroll Settings"), Space]
        [SerializeField] private RectTransform _containerTransform;
        [SerializeField] private List<RectTransform> _pagesTransform;
        [SerializeField] private float _scrollDuration = 0.5f;

        private int _pageIndex;
        private float _pageWidth;
        private Vector2 _startTouchPosition;
        private bool _isSwiping;

        public int PagesCount => _pagesTransform.Count;
        public event Action<int> OnPageChanged;

        private void Awake() => InitButtons();

        private void Start() => StartCoroutine(InitPageLayout());
        
        private void InitButtons()
        {
            for (int i = 0; i < _rightButtons.Count; i++)
            {
                int index = i;

                _rightButtons[i].onClick.AddListener(() =>
                {
                    _rightButtons[index].interactable = false;
                    ChangePage(1);
                });
            }
        }

        private IEnumerator InitPageLayout()
        {
            yield return new WaitForEndOfFrame();
            Canvas.ForceUpdateCanvases();

            RectTransform viewport = (RectTransform)_containerTransform.parent;
            _pageWidth = viewport.rect.width;

            _containerTransform.anchorMin = new Vector2(0, 0);
            _containerTransform.anchorMax = new Vector2(0, 1);
            _containerTransform.pivot = new Vector2(0, 0.5f);

            for (int i = 0; i < _pagesTransform.Count; i++)
                PositionPage(_pagesTransform[i], i);

            _containerTransform.sizeDelta = new Vector2(_pageWidth * PagesCount, 0);
            _containerTransform.anchoredPosition = Vector2.zero;
        }

        private void PositionPage(RectTransform page, int index)
        {
            page.anchorMin = new Vector2(0, 0);
            page.anchorMax = new Vector2(0, 1);
            page.pivot = new Vector2(0, 0.5f);

            page.sizeDelta = new Vector2(_pageWidth, 0);
            page.anchoredPosition = new Vector2(index * _pageWidth, 0);
        }
        
        private void ChangePage(int direction)
        {
            _pageIndex = Mathf.Clamp(_pageIndex + direction, 0, PagesCount - 1);
            ScrollToPage(_pageIndex);
        }

        private void ScrollToPage(int index)
        {
            float targetX = -index * _pageWidth;
            _containerTransform.DOAnchorPosX(targetX, _scrollDuration)
                .SetEase(Ease.InOutCubic)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    OnPageChanged?.Invoke(_pageIndex);
                });
        }
    }
}
