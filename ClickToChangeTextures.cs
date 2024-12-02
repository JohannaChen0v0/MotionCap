using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickToChangeTextures : MonoBehaviour, IPointerClickHandler
{
    public RawImage rawImage;        // 要操作的 Raw Image
    public Texture newTexture;      // 新的 Texture

    // 点击事件处理
    public void OnPointerClick(PointerEventData eventData)
    {
        if (rawImage != null && newTexture != null)
        {
            rawImage.texture = newTexture;
        }
    }
}
