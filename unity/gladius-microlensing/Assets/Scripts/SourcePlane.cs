using UnityEngine;

public class SourcePlane : CoordinateSpaceUI
{
    [Header("Image")]
    [SerializeField] private SourceImage sourceImage;
    [SerializeField] private SourceData sourceData;

    [Header("Position Bounds")]
    [SerializeField] private bool enforceBounds;
    [SerializeField] private Vector2 xBounds;
    [SerializeField] private Vector2 yBounds;

    private bool draggingSource;
    private Vector2 mouseOffset;

    public override void OnEnable()
    {
        base.OnEnable();
        SourceImage.OnStartDraggingSource += HandleStartDraggingSource;
        SourceImage.OnStopDraggingSource += HandleStopDraggingSource;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SourceImage.OnStartDraggingSource -= HandleStartDraggingSource;
        SourceImage.OnStopDraggingSource -= HandleStopDraggingSource;
    }

    public void HandleStartDraggingSource(Vector2 offset)
    {
        draggingSource = true;
        mouseOffset = offset;
    }

    public void HandleStopDraggingSource(Vector2 mousePosition)
    {
        draggingSource = false;
        mouseOffset = Vector2.zero;
    }

    private void Update()
    {
        if (!sourceImage) return;

        if (draggingSource)
        {
            Vector2 uv = ScreenToObjectUV((Vector2)Input.mousePosition - mouseOffset);

            // Clamp UV coordinates
            uv.x = Mathf.Clamp01(uv.x);
            uv.y = Mathf.Clamp01(uv.y);

            if (enforceBounds)
            {
                Vector2 lowerLeft = CoordinatesToUV(new Vector2(xBounds.x, yBounds.x));
                Vector2 upperRight = CoordinatesToUV(new Vector2(xBounds.y, yBounds.y));
                uv.x = Mathf.Clamp(uv.x, lowerLeft.x, upperRight.x);
                uv.y = Mathf.Clamp(uv.y, lowerLeft.y, upperRight.y);
            }

            RectTransform sourceImageRect = (RectTransform)sourceImage.transform;
            sourceImageRect.position = ObjectUVToScreen(uv);

            if (sourceData) sourceData.SetPosition(UVToRect(uv));
        }
    }

    public void ResizeSource(float size)
    {
        if (sourceImage)
        {
            Vector2 sizeDelta = size * Vector2.one;
            ((RectTransform)sourceImage.transform).sizeDelta = sizeDelta;

            if (sourceData) sourceData.SetSizeDelta(sizeDelta);
        }
    }
}
