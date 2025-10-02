using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HabitProgressDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private GameObject _clearedGo;
    [SerializeField] private GameObject _failedGo;
    [SerializeField] private Image _circleImage;
    
    public void SetProgress(int day, bool cleared)
    {
        _failedGo.SetActive(false);
        _clearedGo.SetActive(false);
        
        _dayText.text = "DAY \n" + day;
        
        if(cleared)
            _clearedGo.SetActive(true);
    }

    public void PlaySuccessAnim()
    {
        _clearedGo.transform.localScale = Vector3.zero;
        _clearedGo.SetActive(true);
        
        _clearedGo.transform.DOScale(Vector3.one, 0.25f).SetLink(gameObject);
    }
    
    public void SetTargetImage(Sprite sprite)
    {
        _circleImage.sprite = sprite;
        _dayText.color = Color.white;
    }

    public void SetFailed() => _failedGo.SetActive(true);
}
