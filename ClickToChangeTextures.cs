using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickToChangeTextures : MonoBehaviour, IPointerClickHandler
{
    public RawImage rawImage;        // Ҫ������ Raw Image
    public Texture newTexture;      // �µ� Texture

    // ����¼�����
    public void OnPointerClick(PointerEventData eventData)
    {
        if (rawImage != null && newTexture != null)
        {
            rawImage.texture = newTexture;
        }
    }
}
