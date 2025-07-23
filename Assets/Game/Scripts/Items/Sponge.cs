using UnityEngine;
using UnityEngine.EventSystems;

public class Sponge : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MakeUpController _makeUpController;
    [SerializeField] private BounceTween _bouncyScale;


    public void OnPointerDown(PointerEventData eventData)
    {
        _makeUpController.ClearAll();
        _bouncyScale.Play();
    }
}
