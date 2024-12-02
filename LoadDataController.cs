using UnityEngine;
using System.IO;
using TMPro;

public class LoadDataController : MonoBehaviour
{
    public GameData gameData;
    public TextMeshProUGUI Stamina_number;
    public TextMeshProUGUI Health_number;


    private string filePath = Application.streamingAssetsPath + "/GameData.json";

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        Debug.Log("Player Health: " + gameData.HealthPoint);
        Debug.Log("Player Stamina: " + gameData.Stamina);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData()
    {
        string path = Application.streamingAssetsPath + "/GameData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogError("GameData.json not found!");
        }

        Stamina_number.text = gameData.Stamina.ToString();
        Health_number.text = gameData.HealthPoint.ToString();
    }

    public void SaveStaminaData(string stamina)
    {
        string data = File.ReadAllText(filePath);
        GameData gameData = JsonUtility.FromJson<GameData>(data);

        // Update player's stamina
        Debug.Log(stamina);
        gameData.Stamina = int.Parse(stamina);

        // change back to JSON and write into the data file
        data = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, data);

    }
    public void SaveHpData(string hp)
    {
        string data = File.ReadAllText(filePath);
        GameData gameData = JsonUtility.FromJson<GameData>(data);

        // update player's hp
        Debug.Log(hp);
        gameData.HealthPoint = int.Parse(hp);

        // change back to JSON and write into the data file
        data = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, data);

    }
}
