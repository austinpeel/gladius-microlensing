using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DraggableUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool canDrag = true;

    private Camera eventCamera = null;
    private bool mouseIsDown;
    private Vector2 offset;

    [HideInInspector] public RectTransform rect;

    public static event Action<int> OnDrag;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        mouseIsDown = true;
        eventCamera = Camera.main;

        rect = transform as RectTransform;
        offset = Input.mousePosition - rect.position;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        mouseIsDown = false;
        eventCamera = null;
        rect = null;
    }

    private void Update()
    {
        if (!canDrag || !mouseIsDown || !rect) return;

        Vector2 uv = eventCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 position = Vector3.zero;
        position.x = uv.x * Screen.width - offset.x;
        // position.x = Mathf.Clamp(position.x, rect.rect.xMin, Screen.width);
        position.y = uv.y * Screen.height - offset.y;
        // position.y = Mathf.Clamp(position.y, rect.rect.yMin, Screen.height);
        rect.position = position;

        OnDrag?.Invoke(transform.GetSiblingIndex());
    }
}
