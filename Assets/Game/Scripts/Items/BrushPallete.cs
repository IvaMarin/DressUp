using UnityEngine;
using UnityEngine.EventSystems;

public class BrushPallete : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Brush.BrushColors _brushColor;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (HandController.Instance.CurrentState == HandState.Idle)
        {
            HandController.Instance.PickBrush(_brushColor, this);
        }
    }
}