using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    // Start is called before the first frame update

    public LoadDataController loadDataController;

    void Start()
    {
        loadDataController.SaveHpData("10");
        loadDataController.SaveStaminaData("100");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
