using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(HandAnimator))]
public class HandController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static HandController Instance { get; private set; }

    [Header("Refernces")]
    [SerializeField] private MakeUpController _makeUpController;
    [SerializeField] private FaceController _face;
    [SerializeField] private Brush _brush;

    [Header("Settings")]
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private Ease _moveEase = Ease.OutQuad;

    [Header("Key Positions")]
    [SerializeField] private RectTransform _creamPosition;
    [SerializeField] private RectTransform _brushPosition;
    [SerializeField] private RectTransform _lipstickReadyPosition;

    private HandAnimator _animator;
    private Tween _currentTween;

    private HandState _currentState = HandState.Idle;
    private Item _currentItem = Item.None;

    private Vector3 _homePosition;
    private Vector3 _returnPosition;

    private Cream _cream;
    private Lipstick _lipstick;

    public HandState CurrentState => _currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        _animator = GetComponent<HandAnimator>();
        _homePosition = transform.position;
        _returnPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_currentState == HandState.Dragging)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentState != HandState.Dragging) return;

        if (_face.ContainsPosition(eventData.position))
        {
            UseItem();
        }
        else
        {
            MoveToReturnPosition();
        }
    }

    private void UseItem()
    {
        switch (_currentItem)
        {
            case Item.Cream:
                StartCoroutine(ApplyCream());
                break;
            case Item.Brush:
                StartCoroutine(ApplyBlush(_brush.BrushColor));
                break;
            case Item.Lipstick:
                StartCoroutine(ApplyLipstick(_lipstick.LipstickColor));
                break;
        }
    }

    private void MoveToReturnPosition()
    {
        MoveToPosition(_returnPosition, _moveDuration, () =>
        {
            _currentState = HandState.Dragging;
        });
    }

    private void MoveToHomePosition()
    {
        MoveToPosition(_homePosition, _moveDuration, () =>
        {
            _currentState = HandState.Idle;
        });
    }

    public void PickCream(Cream cream)
    {
        _cream = cream;
        MoveToPosition(_cream.transform.position, _moveDuration, () => TakeCream());
    }

    private void TakeCream()
    {
        _cream.transform.SetParent(transform);
        _cream.transform.localPosition = Vector3.zero;

        MoveToPosition(_creamPosition.transform.position, _moveDuration, () =>
        {
            _returnPosition = _cream.transform.position;
            _currentItem = Item.Cream;
            _currentState = HandState.Dragging;
        });
    }

    public void PickBrush(Brush.BrushColors color, BrushPallete brushPallete)
    {
        MoveToPosition(_brushPosition.position, _moveDuration, () => TakeBrush(color, brushPallete));
    }

    private void TakeBrush(Brush.BrushColors color, BrushPallete brushPallete)
    {
        _brush.transform.SetParent(transform);
        _brush.transform.localPosition = Vector3.zero;

        ApplyBrushPallete(color, brushPallete);
    }

    private void ApplyBrushPallete(Brush.BrushColors color, BrushPallete brushPallete)
    {
        Vector3 offset = new Vector3(0f, -250f, 0f);
        MoveToPosition(brushPallete.transform.position + offset, _moveDuration, () =>
        {
            _brush.SetBrushColor(color);
            MoveToPosition(_brush.GetReadyPosition().transform.position, _moveDuration, () =>
            {
                _returnPosition = _brush.GetReadyPosition().transform.position;

                _currentItem = Item.Brush;
                _currentState = HandState.Dragging;
            });
        });
    }

    public void PickLipstick(Lipstick lipstick)
    {
        _lipstick = lipstick;
        MoveToPosition(_lipstick.transform.position, _moveDuration, () => TakeLipstick());
    }

    private void TakeLipstick()
    {
        _lipstick.transform.SetParent(transform);
        _lipstick.transform.localPosition = Vector3.zero;

        MoveToPosition(_lipstickReadyPosition.transform.position, _moveDuration, () =>
        {
            _returnPosition = _lipstickReadyPosition.transform.position;

            _currentItem = Item.Lipstick;
            _currentState = HandState.Dragging;
        });
    }

    private IEnumerator ApplyCream()
    {
        _currentState = HandState.ApplyingCream;
        yield return new WaitForSeconds(_animator.PlayApplyCreamAnimation());

        _face.ClearAcne();
        ReturnCream();
    }

    private IEnumerator ApplyBlush(Brush.BrushColors color)
    {
        _currentState = HandState.ApplyingBrush;
        yield return new WaitForSeconds(_animator.PlayApplyBlushAnimation());

        _makeUpController.ApplyBlush(color);
        ReturnBrush();
    }

    private IEnumerator ApplyLipstick(Lipstick.LipstickColors color)
    {
        _currentState = HandState.ApplyingLipstick;
        yield return new WaitForSeconds(_animator.PlayApplyLipstickAnimation());

        _makeUpController.ApplyLipstick(color);
        ReturnLipstick();
    }

    private void ReturnLipstick()
    {
        MoveToPosition(_lipstick.GetDefaultPosition(), _moveDuration, () =>
        {
            _lipstick.ReturnToShelf();
            MoveToHomePosition();
        });
    }

    private void ReturnBrush()
    {
        MoveToPosition(_brush.GetDefaultPosition().transform.position, _moveDuration, () =>
        {
            _brush.ReturnToShelf();
            MoveToHomePosition();
        });
    }

    private void ReturnCream()
    {
        MoveToPosition(_returnPosition, _moveDuration, () =>
        {
            _cream.ReturnToShelf();
            MoveToHomePosition();
        });
    }

    private Tween MoveToPosition(Vector3 targetPosition, float duration, System.Action onComplete = null)
    {
        _currentTween?.Kill();
        _currentTween = transform.DOMove(targetPosition, duration)
            .SetEase(_moveEase)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                _currentTween = null;
            });
        return _currentTween;
    }
}

public enum HandState
{
    Idle,
    Dragging,
    ApplyingCream,
    ApplyingBrush,
    ApplyingLipstick
}

public enum Item
{
    None,
    Cream,
    Brush,
    Lipstick
}