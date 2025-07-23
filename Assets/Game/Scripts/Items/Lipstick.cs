using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static Brush;

public class Lipstick : MonoBehaviour, IPointerDownHandler
{
    public enum LipstickColors { Color1, Color2, Color3, Color4, Color5, Color6 }

    [SerializeField] private LipstickColors _lipstickColor;
    [SerializeField] private Ease _moveEase = Ease.OutQuad;
    [SerializeField] private float _moveDuration = 0.5f;

    private Tween _currentTween;
    private Transform _container;

    public LipstickColors LipstickColor => _lipstickColor;

    private void Awake()
    {
        _container = transform.parent;
    }

    public Vector3 GetDefaultPosition()
    {
        return _container.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (HandController.Instance.CurrentState == HandState.Idle)
        {
            HandController.Instance.PickLipstick(this);
        }
    }

    public void ReturnToShelf()
    {
        transform.SetParent(_container);
        MoveToPosition(_container.position);
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
