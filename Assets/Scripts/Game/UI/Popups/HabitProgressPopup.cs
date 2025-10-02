using System;
using System.Collections.Generic;
using Habits;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HabitProgressPopup : Popup
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Sprite _targetSprite;
    [SerializeField] private TextMeshProUGUI _habitText;
    
    [SerializeField] private GameObject _errorGo;
    [SerializeField] private Button _closeErrorButton;
    
    [SerializeField] private List<HabitProgressDisplay> _days;

    public event Action OnCheckedIn;
    
    private void Awake()
    {
        _closeButton.onClick.AddListener(DestroyPopup);
    }

    public void SetData(HabitData data)
    {
        _habitText.text = data.Name.ToString();
        
        for (int i = 0; i < _days.Count; i++) 
            _days[i].SetProgress(i + 1, i < data.DaysChecked);
        
        ProcessCheckIn(data);
        _days[data.Duration - 1].SetTargetImage(_targetSprite);
    }

    private void ProcessCheckIn(HabitData data)
    {
        if (data.LastDayChecked == String.Empty)
        {
            CheckIn(data);
            return;
        }
        
        var lastDate = DateTime.ParseExact(data.LastDayChecked, "o", null);
        var now = DateTime.Now;
        
        if(lastDate.Date == now.Date)
            return;
        
        if (now.Date != lastDate.Date.AddDays(1))
        {
            if(data.DaysChecked < 30)
                _days[data.DaysChecked].SetFailed();
            
            ResetProgress(data);
            return;
        }

        CheckIn(data);
    }

    private void ResetProgress(HabitData data)
    {
        data.LastDayChecked = String.Empty;
        data.DaysChecked = 0;
        
        _errorGo.SetActive(true);
        _closeErrorButton.onClick.AddListener(() =>
        {
            _errorGo.SetActive(false);
            SetData(data);
        });
    }

    private void CheckIn(HabitData data)
    {
        data.DaysChecked++;
        data.LastDayChecked = DateTime.Now.ToString("o");
        OnCheckedIn?.Invoke();
        
        if(data.DaysChecked <= 30)
            _days[data.DaysChecked - 1].PlaySuccessAnim();
    }
}
