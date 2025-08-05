using DG.Tweening;
using UnityEngine;

public class FloatingButton : MonoBehaviour
{
    [SerializeField] private float floatDuration;
 
    private RectTransform _rt;
    private Vector2 _startPos;
    private Sequence _sequence;
    
    private bool addScaling = true;
   private void Start()
    {
        _rt = GetComponent<RectTransform>();
        _startPos = _rt.anchoredPosition;
        
        StartFloating();
    }

    private void StartFloating()
    {
        _sequence?.Kill();
        
        _rt.anchoredPosition = _startPos;
        
        _sequence = DOTween.Sequence();
        
        _sequence.Append(
            _rt.DOAnchorPosY(60f, floatDuration)
                .SetEase(Ease.OutSine)
        ).Append(
            _rt.DOAnchorPosY(30f, floatDuration)
                .SetEase(Ease.OutSine)
        ).Join(
                _rt.DOScale(1.05f, floatDuration)
                    .SetLoops(2, LoopType.Yoyo)).
            SetLoops(-1);
        
        _sequence.Play();
    }

    void OnDestroy()
    {
        _sequence?.Kill();
    }
}

