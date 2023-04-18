using UnityEngine;
using UnityEngine.UI;

public class ImageUploader : MonoBehaviour
{
    [SerializeField] private Image previewImage;
    [SerializeField] private int maxImageSize = 128;

    // private Texture2D uploadedTexture;

    public void UploadImage()
    {
        // // Declare the file extensions and filter labels
        // string[] extensions = new[] { "png", "jpg", "jpeg", "gif", "bmp" };
        // string[] filters = new[] { "Image files", "*.png;*.jpg;*.jpeg;*.gif;*.bmp" };

        // // Open file explorer to select an image
        // string imagePath = UnityEditor.EditorUtility.OpenFilePanelWithFilters("Select an image", "", filters);

        // if (!string.IsNullOrEmpty(imagePath))
        // {
        //     // Load the image file into a texture
        //     byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
        //     Texture2D originalTexture = new Texture2D(2, 2);
        //     originalTexture.LoadImage(imageData);

        //     // Resize the texture to have a maximum dimension of 128 pixels
        //     Texture2D resizedTexture = ImageResizer.Resize(originalTexture, maxImageSize);

        //     // Display the preview image
        //     previewImage.sprite = Sprite.Create(resizedTexture, new Rect(0, 0, resizedTexture.width, resizedTexture.height), Vector2.zero);
        //     previewImage.gameObject.SetActive(true);

        //     previewImage.preserveAspect = true;
        //     previewImage.SetNativeSize();
        // }
    }
}
