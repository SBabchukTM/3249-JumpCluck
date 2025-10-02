using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PageScroller : MonoBehaviour
{
    [Header("Both optional")] [SerializeField]
    private Button _leftButton;

    [SerializeField] private Button _rightButton;

    [SerializeField] [Space] private RectTransform _containerTransform;
    [SerializeField] private List<RectTransform> _pagesTransform;
    [SerializeField] private float _scrollDuration = 0.5f;

    private int _pageIndex;
    private float _pageWidth;
    private Vector2 _startTouchPosition;
    private bool _isSwiping = false;

    public int PagesCount => _pagesTransform.Count;
    public event Action<int> OnPageChanged;
    
    public int CurrentPage => _pageIndex;

    private void Awake()
    {
        if (_leftButton)
            _leftButton.onClick.AddListener(() => ChangedPage(-1));

        if (_rightButton)
            _rightButton.onClick.AddListener(() => ChangedPage(1));
    }

    public void BuildPages(List<RectTransform> pages)
    {
        _pagesTransform = pages;
        StartCoroutine(DelayedLayoutInit());
    }

    private IEnumerator DelayedLayoutInit()
    {
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();

        var viewport = (RectTransform)_containerTransform.parent;
        _pageWidth = viewport.rect.width;

        _containerTransform.anchorMin = new Vector2(0, 0);
        _containerTransform.anchorMax = new Vector2(0, 1);
        _containerTransform.pivot = new Vector2(0, 0.5f);

        for (var i = 0; i < _pagesTransform.Count; i++)
        {
            var page = _pagesTransform[i];

            page.anchorMin = new Vector2(0, 0);
            page.anchorMax = new Vector2(0, 1);
            page.pivot = new Vector2(0, 0.5f);

            page.sizeDelta = new Vector2(_pageWidth, 0);
            page.anchoredPosition = new Vector2(i * _pageWidth, 0);
        }

        _containerTransform.sizeDelta = new Vector2(_pageWidth * _pagesTransform.Count, 0);
        _containerTransform.anchoredPosition = Vector2.zero;

        EnableButtons();
    }

    private void ChangedPage(int increment)
    {
        _pageIndex += increment;
        _pageIndex = Mathf.Clamp(_pageIndex, 0, _pagesTransform.Count - 1);
        ScrollToPage(_pageIndex);
        DisableButtons();
    }

    private void ScrollToPage(int index)
    {
        var targetX = -index * _pageWidth;
        _containerTransform.DOAnchorPosX(targetX, _scrollDuration)
            .SetEase(Ease.InOutCubic)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                OnPageChanged?.Invoke(_pageIndex);
                EnableButtons();
            });
    }

    private void DisableButtons()
    {
        DisableButton(_leftButton);
        DisableButton(_rightButton);
    }

    private void EnableButtons()
    {
        EnableButton(_leftButton, () => _pageIndex > 0);
        EnableButton(_rightButton, () => _pageIndex < _pagesTransform.Count - 1);
    }

    private void DisableButton(Button button)
    {
        if (button)
            button.interactable = false;
    }

    private void EnableButton(Button button, Func<bool> condition)
    {
        if (button)
            button.interactable = condition.Invoke();
    }
}