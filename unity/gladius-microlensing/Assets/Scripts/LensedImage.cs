using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LensedImage : MonoBehaviour
{
    private Material material;

    private void OnDisable()
    {
        UpdatePosition(Vector2.zero);
    }

    public void UpdateTexture(Texture2D texture)
    {
        // Debug.Log("LensedImage > UpdateTexture");

        if (!material) material = GetComponent<Image>().material;

        material.SetTexture("_MainTex", texture);
    }

    public void UpdatePosition(Vector2 position)
    {
        // Debug.Log("LensedImage > UpdatePosition");

        if (!material) material = GetComponent<Image>().material;

        Vector2 sizeDelta = ((RectTransform)transform).sizeDelta;
        Vector4 uv = new Vector4(position.x / sizeDelta.x, position.y / sizeDelta.y, 0, 0);
        material.SetVector("_ImagePosition", uv);
    }

    public void UpdateSize(Vector2 sizeDelta)
    {
        // Debug.Log("LensedImage > UpdateSize");

        if (!material) material = GetComponent<Image>().material;

        float width = ((RectTransform)transform).sizeDelta.x;
        material.SetFloat("_ImageSize", sizeDelta.x / width);
    }
}
