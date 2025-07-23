using UnityEngine;
using UnityEngine.UI;

public class FaceController : MonoBehaviour
{
    [SerializeField] private RectTransform _interactionZone;
    [SerializeField] private Image _acne;

    [SerializeField] private MakeUpController makeUpController;

    private bool _hasAcne = true;


    private void Awake()
    {
        _acne.gameObject.SetActive(true);
    }

    public bool ContainsPosition(Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(_interactionZone, screenPosition);
    }

    public void ClearAcne()
    {
        if (!_hasAcne) return;

        var fader = _acne.GetComponent<UIFadeTween>();
        fader.FadeOut(true);

        _hasAcne = false;
    }
}