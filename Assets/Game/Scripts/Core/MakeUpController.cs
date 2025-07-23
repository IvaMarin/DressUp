using System.Collections.Generic;
using UnityEngine;

public class MakeUpController : MonoBehaviour
{
    [SerializeField] private List<UIFadeTween> _blushes = new();
    [SerializeField] private List<UIFadeTween> _lipsticks = new();

    private UIFadeTween _currentBlush;
    private UIFadeTween _currentLipstick;


    public void ApplyBlush(Brush.BrushColors brushColor)
    {
        ClearBlush();

        _currentBlush = _blushes[(int)brushColor];
        _currentBlush.FadeIn(true);
    }

    public void ClearBlush()
    {
        if (_currentBlush != null) _currentBlush.FadeOut(true);
    }

    public void ApplyLipstick(Lipstick.LipstickColors lipstickColor)
    {
        ClearLipstick();

        _currentLipstick = _lipsticks[(int)lipstickColor];
        _currentLipstick.FadeIn(true);
    }

    public void ClearLipstick()
    {
        if (_currentLipstick != null) _currentLipstick.FadeOut(true);
    }

    public void ClearAll()
    {
        ClearBlush();
        ClearLipstick();
    }
}
