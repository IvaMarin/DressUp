using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button), typeof(Image))]
public class PageButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _page;

    [Header("Sprites")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _pressedSprite;

    [Header("Settings")]
    [SerializeField] private bool _isActive;
    [SerializeField] private float _pressDuration = 0.2f;
    [SerializeField] private float _scaleMultiplier = 0.9f;

    private Button _button;
    private Image _image;
    private Vector3 _originalScale;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _originalScale = transform.localScale;

        _button.onClick.AddListener(OnButtonPressed);
        UpdateVisualState();
        _page.SetActive(_isActive);
    }

    private void OnButtonPressed()
    {
        _isActive = !_isActive;
        _page.SetActive(_isActive);

        PressBounce();
        UpdateVisualState();
    }

    public void PressBounce()
    {
        Sequence pressSequence = DOTween.Sequence();
        pressSequence.Append(transform.DOScale(_originalScale * _scaleMultiplier, _pressDuration / 2));
        pressSequence.Append(transform.DOScale(_originalScale, _pressDuration / 2));
    }

    private void UpdateVisualState()
    {
        _image.sprite = _isActive ? _pressedSprite : _normalSprite;
    }

    public void SetActiveState(bool isActive)
    {
        _isActive = isActive;
        _page.SetActive(_isActive);
        UpdateVisualState();
    }
}