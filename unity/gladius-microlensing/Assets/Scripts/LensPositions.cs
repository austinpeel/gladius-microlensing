using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LensPositions : MonoBehaviour
{
    public LensParameters lensParams;
    public LensCollection lensCollection;
    public GameObject lensMarkerPrefab;
    public Button animateButton;
    public Image textureDisplayImage;

    private ImagePlane imagePlane;
    private Transform lensMarkers;

    private Vector2 imagePlaneSize;
    private Vector2 lensedImageSize;
    private float scaleFactor = 1;
    // Lensed image UV range (zero-centered) restricted to image plane area
    private Vector2 xRange = new Vector2(-0.5f, 0.5f);
    private Vector2 yRange = new Vector2(-0.5f, 0.5f);

    private int numLenses = 25;
    private bool animating;
    private float speed = 0.2f;

    // Initial lens positions
    private readonly double[] x = { -5.86325, 7.73263, 1.91267, 6.94573, -0.33579,
                                    -2.97493, 9.16998, 8.79194, 2.94710, -3.75307,
                                    0.06554, -8.76489, -8.90370, -5.49278, 8.87232,
                                    4.92588, 9.58402, 8.93881, -3.92979, 4.65179,
                                    9.75598, -8.82821, 1.32815, -9.43098, -6.79323,
                                    3.09924, -10.28880, -7.60164, 4.22388, 4.17965,
                                    -0.76177, -1.11611, -9.23649, -5.93643, -1.39999,
                                    8.36296, 1.92418, -4.03952, 7.49439, -0.18252,
                                    -1.65411, -8.84003, 2.75366, 6.26092, 3.17912,
                                    10.20920, -6.22973, -4.90661, -2.96547, 1.07860 };
    private readonly double[] y = { -4.67191, -7.77686, -6.85410, -7.30008, -7.78459,
                                    -0.18478, -5.24902, -6.25314, 1.58933, -8.84466,
                                    1.50699, 3.11699, -0.38136, 3.95107, 5.81144,
                                    9.11473, -2.73554, 6.23594, -6.83720, 0.03397,
                                    4.83714, -6.50238, 4.59360, 0.23545, -5.04880,
                                    -1.15811, -0.73228, -2.28455, 0.77617, -5.08801,
                                    -4.99638, 9.72844, -1.03108, 8.19101, 6.02728,
                                    -2.75185, 2.21735, -1.76288, 5.18637, -6.43698,
                                    -3.89563, -1.58948, 2.65422, -0.26763, 5.53124,
                                    0.35701, -4.48037, 9.37297, -2.07391, -5.87786 };

    private void Awake()
    {
        imagePlaneSize = ((RectTransform)transform).sizeDelta;

        if (TryGetComponent(out imagePlane))
        {
            lensedImageSize = ((RectTransform)(imagePlane.lensedImage.transform)).sizeDelta;
            scaleFactor = lensedImageSize.x / imagePlaneSize.x;
            Vector2 delta = 0.5f * (lensedImageSize - imagePlaneSize) / lensedImageSize;
            xRange = new Vector2(delta.x - 0.5f, 1 - delta.x - 0.5f);
            yRange = new Vector2(delta.y - 0.5f, 1 - delta.y - 0.5f);
        }

        // Randomize();
        Initialize();
    }

    private void OnEnable()
    {
        DraggableUI.OnDrag += HandleDragLensMarker;
    }

    private void OnDisable()
    {
        DraggableUI.OnDrag -= HandleDragLensMarker;

        ClearLensMarkers();
        animating = false;
    }

    private void Update()
    {
        if (!animating) return;

        lensCollection.TakeAStep(Time.deltaTime, true);
        UpdateTexture();
        UpdateLensMarkers();
    }

    public void Initialize()
    {
        if (!TryGetComponent(out imagePlane)) return;

        Vector2[] positions = new Vector2[x.Length];
        Vector2[] velocities = new Vector2[x.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            float scaleFactorX = xRange.y - xRange.x;
            float scaleFactorY = yRange.y - yRange.x;
            float xi = scaleFactorX * (float)x[i] / 24f;
            float yi = scaleFactorY * (float)y[i] / 24f;
            positions[i] = new Vector2(xi, yi);
            velocities[i] = speed * Random.insideUnitCircle.normalized;
        }
        lensCollection.positions = positions;
        lensCollection.velocities = velocities;
        lensCollection.xRange = xRange;
        lensCollection.yRange = yRange;

        UpdateTexture();
        UpdateLensMarkers();
    }

    public void Randomize()
    {
        if (!TryGetComponent(out imagePlane)) return;

        Vector2[] positions = new Vector2[numLenses];
        Vector2[] velocities = new Vector2[numLenses];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y));
            velocities[i] = speed * Random.insideUnitCircle.normalized;
        }
        lensCollection.positions = positions;
        lensCollection.velocities = velocities;
        lensCollection.xRange = xRange;
        lensCollection.yRange = yRange;

        UpdateTexture();
        UpdateLensMarkers();
    }

    public void UpdateTexture()
    {
        if (lensCollection.positions.Length == 0) return;

        Texture2D texture = new Texture2D(lensCollection.positions.Length, 1);
        Color[] pixels = new Color[texture.width];
        for (int i = 0; i < pixels.Length; i++)
        {
            Vector2 xy = lensCollection.positions[i];
            float r = Mathf.Clamp01(xy.x + 0.5f);
            float g = Mathf.Clamp01(xy.y + 0.5f);
            pixels[i] = new Color(r, g, 0, 1);
        }
        texture.SetPixels(pixels);
        texture.Apply();
        texture.filterMode = FilterMode.Point;

        // Update the display image
        if (textureDisplayImage)
        {
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            textureDisplayImage.sprite = Sprite.Create(texture, rect, Vector2.zero, texture.width);
            textureDisplayImage.sprite.name = "lens-positions";
        }

        if (lensParams) lensParams.SetPositionTexture(texture);
    }

    public void UpdateLensMarkers()
    {
        if (!transform.Find("Lens Markers"))
        {
            lensMarkers = new GameObject("Lens Markers").transform;
            lensMarkers.SetParent(transform);
            lensMarkers.localPosition = Vector3.zero;
            lensMarkers.localScale = Vector3.one;
        }

        ClearLensMarkers();

        if (!lensMarkerPrefab || !imagePlane) return;

        // Distribute new markers
        foreach (var position in lensCollection.positions)
        {
            RectTransform rect = Instantiate(lensMarkerPrefab, lensMarkers).GetComponent<RectTransform>();
            rect.anchoredPosition = scaleFactor * imagePlane.UVToRect(position + 0.5f * Vector2.one);
        }

        if (lensParams) SetLensMarkerVisibility(lensParams.lensesAreEditable);
    }

    private void ClearLensMarkers()
    {
        if (!transform.Find("Lens Markers")) return;

        // Clear out existing markers
        for (int i = lensMarkers.childCount; i > 0; i--)
        {
            DestroyImmediate(lensMarkers.GetChild(0).gameObject);
        }
    }

    public void SetLensMarkerVisibility(bool visibility)
    {
        if (lensParams) lensParams.lensesAreEditable = visibility;

        // if (visibility) animating = false;
        // if (animateButton)
        // {
        //     animateButton.interactable = !visibility;
        //     var tmp = animateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //     if (tmp) tmp.text = animating ? "Pause" : "Animate";
        // }

        if (!lensMarkers) return;

        foreach (RectTransform marker in lensMarkers)
        {
            marker.gameObject.SetActive(visibility);
        }
    }

    public void HandleDragLensMarker(int markerIndex)
    {
        if (!imagePlane) return;

        // Get the RectTransform of the marker
        RectTransform rect = (RectTransform)lensMarkers.GetChild(markerIndex).transform;
        // Update the corresponding position in the lens collection
        Vector2 position = lensCollection.positions[markerIndex];
        position = imagePlane.RectToUV(rect.anchoredPosition / scaleFactor) - 0.5f * Vector2.one;
        // Clamp the position to the area covered only by the image plane
        position.x = Mathf.Clamp(position.x, xRange.x, xRange.y);
        position.y = Mathf.Clamp(position.y, yRange.x, yRange.y);
        lensCollection.positions[markerIndex] = position;
        // Clamp the marker object's position
        rect.anchoredPosition = scaleFactor * imagePlane.UVToRect(position + 0.5f * Vector2.one);
        // Redraw the texture of positions and send to the shader
        UpdateTexture();
    }

    public void ToggleAnimation()
    {
        animating = !animating;

        if (animateButton)
        {
            var tmp = animateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (tmp) tmp.text = animating ? "Pause" : "Animate";
        }
    }
}

[System.Serializable]
public class LensCollection
{
    public Vector2[] positions;
    public Vector2[] velocities;

    [HideInInspector] public Vector2 xRange = new Vector2(-0.5f, 0.5f);
    [HideInInspector] public Vector2 yRange = new Vector2(-0.5f, 0.5f);

    public void TakeAStep(float deltaTime, bool enforceBoundaryConditions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] += velocities[i] * deltaTime;

            if (enforceBoundaryConditions)
            {
                if (positions[i].x <= xRange.x || positions[i].x >= xRange.y)
                {
                    velocities[i].x = -velocities[i].x;
                }

                if (positions[i].y <= yRange.x || positions[i].y >= yRange.y)
                {
                    velocities[i].y = -velocities[i].y;
                }

                positions[i].x = Mathf.Clamp(positions[i].x, xRange.x, xRange.y);
                positions[i].y = Mathf.Clamp(positions[i].y, yRange.x, yRange.y);
            }
        }
    }
}
