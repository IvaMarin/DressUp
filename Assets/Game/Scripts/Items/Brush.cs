using DG.Tweening;
using System;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public enum BrushColors { Color1, Color2, Color3, Color4, Color5, Color6, Color7, Color8, Color9 }

    [SerializeField] private Ease _moveEase = Ease.OutQuad;
    [SerializeField] private float _moveDuration = 0.5f;

    [SerializeField] private RectTransform _defaultPos;
    [SerializeField] private RectTransform _readyPosition;

    private BrushColors _brushColor = BrushColors.Color1;
    private Tween _currentTween;


    public BrushColors BrushColor => _brushColor;

    public void SetBrushColor(BrushColors color)
    {
        _brushColor = color;
    }

    public RectTransform GetReadyPosition()
    {
        return _readyPosition;
    }

    public RectTransform GetDefaultPosition()
    {
        return _defaultPos;
    }

    public void ReturnToShelf()
    {
        transform.SetParent(_defaultPos);
        MoveToPosition(_defaultPos.position);
    }

    public Tween MoveToPosition(Vector3 targetPosition, Action onComplete = null)
    {
        _currentTween?.Kill();
        _currentTween = transform.DOMove(targetPosition, _moveDuration)
            .SetEase(_moveEase)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                _currentTween = null;
            });
        return _currentTween;
    }
}