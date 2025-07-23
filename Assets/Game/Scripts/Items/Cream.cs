using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cream : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform _creamHomePosition;
    [SerializeField] private Ease _moveEase = Ease.OutQuad;
    [SerializeField] private float _moveDuration = 0.5f;

    private Tween _currentTween;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (HandController.Instance.CurrentState == HandState.Idle)
        {
            HandController.Instance.PickCream(this);
        }
    }

    public void ReturnToShelf()
    {
        transform.SetParent(_creamHomePosition);
        MoveToPosition(_creamHomePosition.position);
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