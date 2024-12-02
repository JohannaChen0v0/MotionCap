using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{
    public LoadDataController loadController;

    void Start()
    {
        loadController.LoadData();
    }

    public void loadPlayerData()
    {
        loadController.LoadData();
        Debug.Log(loadController.gameData.Stamina);
        Debug.Log(loadController.gameData.HealthPoint);
    }

}
