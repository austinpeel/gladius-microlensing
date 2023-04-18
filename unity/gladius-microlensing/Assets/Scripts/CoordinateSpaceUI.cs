using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class CoordinateSpaceUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Extent")]
    [SerializeField] private Vector2 xRange = new Vector2(-1, 1);
    [SerializeField] private Vector2 yRange = new Vector2(-1, 1);

    [HideInInspector] public RectTransform rect;
    [HideInInspector] public Camera mainCamera;
    [HideInInspector] public bool mouseIsDown;

    public virtual void OnEnable()
    {
        rect = transform as RectTransform;
        mainCamera = Camera.main;
    }

    public virtual void OnDisable()
    {
        rect = null;
        mainCamera = null;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        mouseIsDown = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        mouseIsDown = false;
    }

    public Vector2 ScreenToObjectUV(Vector2 position)
    {
        if (!rect) return Vector2.zero;

        Vector2 screenUV = mainCamera.ScreenToViewportPoint(position);
        Vector2 screenSpaceLowerLeft = rect.TransformPoint(-rect.pivot * rect.rect.size);
        Vector2 screenSpaceUpperRight = rect.TransformPoint(rect.rect.size * (Vector2.one - rect.pivot));
        Vector2 lowerLeftUV = mainCamera.ScreenToViewportPoint(screenSpaceLowerLeft);
        Vector2 upperRightUV = mainCamera.ScreenToViewportPoint(screenSpaceUpperRight);

        return (screenUV - lowerLeftUV) / (upperRightUV - lowerLeftUV);
    }

    public Vector2 ObjectUVToScreen(Vector2 uv)
    {
        if (!rect) return Vector2.zero;

        Vector2 screenSpaceLowerLeft = rect.TransformPoint(-rect.pivot * rect.rect.size);
        Vector2 screenSpaceUpperRight = rect.TransformPoint(rect.rect.size * (Vector2.one - rect.pivot));
        Vector2 lowerLeftUV = mainCamera.ScreenToViewportPoint(screenSpaceLowerLeft);
        Vector2 upperRightUV = mainCamera.ScreenToViewportPoint(screenSpaceUpperRight);

        Vector2 screenUV = uv * (upperRightUV - lowerLeftUV) + lowerLeftUV;
        return mainCamera.ViewportToScreenPoint(screenUV);
    }

    public Vector2 UVToCoordinates(Vector2 uv)
    {
        float x = (xRange.y - xRange.x) * uv.x + xRange.x;
        float y = (yRange.y - yRange.x) * uv.y + yRange.x;
        return new Vector2(x, y);
    }

    public Vector2 CoordinatesToUV(Vector2 xy)
    {
        float u = (xy.x - xRange.x) / (xRange.y - xRange.x);
        float v = (xy.y - yRange.x) / (yRange.y - yRange.x);
        return new Vector2(u, v);
    }

    public Vector2 UVToRect(Vector2 uv)
    {
        if (!rect) rect = transform as RectTransform;
        return (uv - 0.5f * Vector2.one) * rect.rect.size;
    }

    public Vector2 RectToUV(Vector2 rectPosition)
    {
        return (rectPosition / rect.rect.size) + 0.5f * Vector2.one;
    }

    public Vector2 CoordinatesToRect(Vector2 xy)
    {
        return UVToRect(CoordinatesToUV(xy));
    }

    public Vector2 RectToCoordinates(Vector2 rectPosition)
    {
        return UVToCoordinates(RectToUV(rectPosition));
    }
}