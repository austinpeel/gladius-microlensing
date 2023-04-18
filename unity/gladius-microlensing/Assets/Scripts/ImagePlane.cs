using UnityEngine;

[ExecuteInEditMode]
public class ImagePlane : CoordinateSpaceUI
{
    public LensedImage lensedImage;
    public SourceData sourceData;

    public override void OnEnable()
    {
        base.OnEnable();
        SourceData.OnSpriteChanged += HandleSourceSpriteChanged;
        SourceData.OnPositionChanged += HandleSourceMoved;
        SourceData.OnSizeChanged += HandleSourceResized;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SourceData.OnSpriteChanged -= HandleSourceSpriteChanged;
        SourceData.OnPositionChanged -= HandleSourceMoved;
        SourceData.OnSizeChanged -= HandleSourceResized;
    }

    public void HandleSourceSpriteChanged()
    {
        if (!lensedImage || !sourceData) return;

        lensedImage.UpdateTexture(sourceData.texture);
    }

    public void HandleSourceMoved()
    {
        if (!lensedImage || !sourceData) return;

        lensedImage.UpdatePosition(sourceData.position);
    }

    public void HandleSourceResized()
    {
        if (!lensedImage || !sourceData) return;

        lensedImage.UpdateSize(sourceData.sizeDelta);
    }
}
