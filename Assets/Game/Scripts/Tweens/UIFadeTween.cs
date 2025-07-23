using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class UIFadeTween : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 0.5f;
    [SerializeField] private Ease _fadeEase = Ease.Linear;
    [SerializeField] private bool _startVisible = false;

    private Image _targetImage;
    private Tween _currentTween;


    private void Awake()
    {
        _targetImage = GetComponent<Image>();

        SetInitialColor();
        _targetImage.enabled = _startVisible;
    }

    private void SetInitialColor()
    {
        _targetImage.color = new Color(
            _targetImage.color.r,
            _targetImage.color.g,
            _targetImage.color.b,
            _startVisible ? 1f : 0f
        );
    }

    public void FadeIn(bool enableObject = true)
    {
        Fade(1f, _fadeInDuration, enableObject);
    }

    public void FadeOut(bool disableObject = false)
    {
        Fade(0f, _fadeOutDuration, !disableObject, disableObject);
    }

    public void Fade(float targetAlpha, float duration, bool enableImage = true, bool disableOnComplete = false)
    {
        _currentTween?.Kill();

        if (enableImage && !_targetImage.enabled)
        {
            _targetImage.enabled = true;
        }

        _currentTween = _targetImage.DOFade(targetAlpha, duration)
            .SetEase(_fadeEase)
            .OnComplete(() =>
            {
                if (disableOnComplete && Mathf.Approximately(targetAlpha, 0f))
                {
                    _targetImage.enabled = false;
                }
                _currentTween = null;
            });
    }

    private void OnDestroy()
    {
        _currentTween?.Kill();
    }
}