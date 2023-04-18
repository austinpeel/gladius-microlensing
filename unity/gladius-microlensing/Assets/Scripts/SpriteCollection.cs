using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteCollection : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] sprites;
    public bool cycleOnClick;
    public bool grayscale;

    [HideInInspector] public int currentSpriteIndex = 0;

    public static event Action<Sprite> OnSpriteChanged;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sprites.Length == 0 || !cycleOnClick) return;

        ShowNextSprite();
    }

    private void Start()
    {
        ShowSprite(currentSpriteIndex);
    }

    private void ShowNextSprite()
    {
        ShowSprite((currentSpriteIndex + 1) % sprites.Length);
    }

    public void ShowSprite(int spriteIndex)
    {
        if (TryGetComponent(out Image image))
        {
            if (spriteIndex < 0 || spriteIndex >= sprites.Length)
            {
                image.sprite = null;
                currentSpriteIndex = -1;
            }
            else
            {
                image.sprite = sprites[spriteIndex];
                currentSpriteIndex = spriteIndex;

                if (grayscale) image.sprite = GrayscaleSprite(image.sprite);
            }

            // Alert other scripts that the sprite has been updated
            OnSpriteChanged?.Invoke(image.sprite);
        }
    }

    private Sprite GrayscaleSprite(Sprite sprite)
    {
        // Create a new Texture2D from the sprite's texture
        Texture2D texture = new Texture2D(sprite.texture.width, sprite.texture.height);

        // Get the grayscale pixels of the texture
        Color[] pixels = sprite.texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            float value = pixels[i].grayscale;
            pixels[i] = new Color(value, value, value, pixels[i].a);
        }
        texture.SetPixels(pixels);
        texture.filterMode = FilterMode.Point;
        texture.Apply();

        // Create a new sprite with the grayscale texture
        Sprite grayscaleSprite = Sprite.Create(texture, sprite.rect, Vector2.zero);
        grayscaleSprite.name = sprite.name + "-grayscale";

        return grayscaleSprite;
    }
}
