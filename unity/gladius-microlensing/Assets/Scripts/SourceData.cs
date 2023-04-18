using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Source Data", fileName = "New Source Data", order = 52)]
public class SourceData : ScriptableObject
{
    public Texture2D texture;
    public Vector2 sizeDelta;
    public Vector2 position;

    public static event Action OnSpriteChanged;
    public static event Action OnSizeChanged;
    public static event Action OnPositionChanged;

    private void OnEnable()
    {
        SpriteCollection.OnSpriteChanged += HandleSourceSpriteChanged;
    }

    private void OnDisable()
    {
        SpriteCollection.OnSpriteChanged -= HandleSourceSpriteChanged;
    }

    public void HandleSourceSpriteChanged(Sprite sprite)
    {
        texture = sprite.texture;
        OnSpriteChanged?.Invoke();
    }

    public void SetTexture(Texture2D texture)
    {
        this.texture = texture;
        OnSpriteChanged?.Invoke();
    }

    public void SetSizeDelta(Vector2 sizeDelta)
    {
        this.sizeDelta = sizeDelta;
        OnSizeChanged?.Invoke();
    }

    public void SetPosition(Vector2 position)
    {
        this.position = position;
        OnPositionChanged?.Invoke();
    }
}
