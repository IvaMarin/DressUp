using UnityEngine;
using DG.Tweening;

public class BounceTween : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _punchScale = 0.2f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutElastic;

    private Vector3 _originalScale;
    private Tween _currentTween;


    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    public void Play()
    {
        _currentTween?.Kill();

        transform.localScale = _originalScale;

        _currentTween = DOTween.Sequence()
            .Append(transform.DOScale(_originalScale * _punchScale, _duration * 0.5f).SetEase(_easeType))
            .Append(transform.DOScale(_originalScale, _duration * 0.5f)).SetEase(Ease.OutBounce);
    }

    private void OnDestroy()
    {
        _currentTween?.Kill();
    }
}