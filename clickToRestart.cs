using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class clickToRestart : MonoBehaviour, IPointerClickHandler
{
    public RawImage clickImg;        // Raw Image
    public LoadDataController loadDataController;

    // Click event
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickImg != null)
        {
            SceneManager.LoadScene(1);
        }

        loadDataController.SaveHpData("10");
        loadDataController.SaveStaminaData("100");
    }
}
