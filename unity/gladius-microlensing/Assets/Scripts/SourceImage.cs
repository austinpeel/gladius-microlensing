using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SourceImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action<Vector2> OnStartDraggingSource;
    public static event Action<Vector2> OnStopDraggingSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform rect = (RectTransform)transform;
        OnStartDraggingSource?.Invoke(Input.mousePosition - rect.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnStopDraggingSource?.Invoke(Input.mousePosition);
    }
}
