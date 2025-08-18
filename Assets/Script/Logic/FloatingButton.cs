using DG.Tweening;
using UnityEngine;

public class FloatingButton : MonoBehaviour
{
    [SerializeField] private float floatDuration;
    [SerializeField] private RectTransform _rt;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _scale;
    [SerializeField] private int _loop;
    
    private Vector2 _startPos;
    private Sequence _sequence;
    
    private bool addScaling = true;
   private void Start()
    {
        _startPos = _rt.anchoredPosition;
        
        StartFloating();
    }

    private void StartFloating()
    {
        _sequence?.Kill();
        
        _rt.anchoredPosition = _startPos;
        
        _sequence = DOTween.Sequence();
        
        _sequence.Append(
            _rt.DOAnchorPosY(_maxDistance, floatDuration)
                .SetEase(Ease.OutSine)
        ).Append(
            _rt.DOAnchorPosY(_minDistance, floatDuration)
                .SetEase(Ease.OutSine)
        ).Join(
                _rt.DOScale(_scale, floatDuration)
                    .SetLoops(_loop, LoopType.Yoyo)).
            SetLoops(-1);
        
        _sequence.Play();
    }

    void OnDestroy()
    {
        _sequence?.Kill();
    }
}

