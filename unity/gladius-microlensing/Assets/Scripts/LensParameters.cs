using UnityEngine;

[CreateAssetMenu(menuName = "Lens Parameters", fileName = "New Lens Parameters", order = 50)]
public class LensParameters : ScriptableObject
{
    private Texture2D positionTexture;
    public float strength;
    public Material microlensingMaterial;
    public bool lensesAreEditable;

    private void OnEnable()
    {
        // Send position data to the shader
        if (microlensingMaterial) microlensingMaterial.SetTexture("_LensPositionTex", positionTexture);
    }

    public void OnDisable()
    {
        if (microlensingMaterial) microlensingMaterial.SetTexture("_LensPositionTex", positionTexture);
        lensesAreEditable = false;
    }

    public void SetPositionTexture(Texture2D positionTexture)
    {
        this.positionTexture = positionTexture;

        if (microlensingMaterial && positionTexture)
        {
            microlensingMaterial.SetTexture("_LensPositionTex", positionTexture);
        }
    }

    public void SetStrength(float strength)
    {
        this.strength = strength;

        if (microlensingMaterial)
        {
            microlensingMaterial.SetFloat("_LensStrength", strength);
        }
    }
}
