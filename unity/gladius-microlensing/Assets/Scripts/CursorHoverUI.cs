using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CustomCursor customCursor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Display the cursor while hovering
        if (customCursor)
        {
            Cursor.SetCursor(customCursor.texture, customCursor.hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RestoreDefault();
    }

    private void RestoreDefault()
    {
        // Restore the default cursor
        if (customCursor)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnDisable()
    {
        RestoreDefault();
    }
}
