using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Video;
using UnityEngine.UI;

public class RunningController : MonoBehaviour
{
    public GameData gameData;
    public GameObject character;
    public GameObject umbrella_shadow;
    public GameObject umbrella;
    public GameObject character_height;
    public GameObject main_camera;

    public GameObject previewPanel;
    public GameObject replayBtn;
    public GameObject LetsGoBtn;
    public GameObject vp;

    public TextMeshProUGUI Health_number;
    public TextMeshProUGUI Stamina_number;
    public LoadDataController loadDataController;

    private bool isCovered = false;

    private bool is_covered_x = false;
    private bool is_covered_z = false;

    private float sizeX_character = 0.0f;
    private float sizeX_umbrella_shadow = 0.0f;
    private float sizeZ_character = 0.0f;
    private float sizeZ_umbrella_shadow = 0.0f;
    private float coverRate = 0.0f;
    private float heightComperation = 0.0f;

    private bool isTimeToCheckout = false;
    public float rotationSpeed = 2f; // 旋转速度
    public float movementSpeed = 2f; // 平移动作速度
    private bool isRotating = false;

    public RawImage heart01;
    public RawImage heart02;
    public RawImage heart03;
    public Texture2D newtexture;

    // Start is called before the first frame update
    void Start()
    {
        previewPanel.SetActive(true);
        replayBtn.SetActive(true);
        LetsGoBtn.SetActive(true);
        vp.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void isUmbrellaCover()
    {
        previewPanel.SetActive(false);
        replayBtn.SetActive(false);
        LetsGoBtn.SetActive(false);
        vp.SetActive(false);
        Renderer renderer_character = character.GetComponent<Renderer>();
        Renderer renderer_shadow = umbrella_shadow.GetComponent<Renderer>();
        Renderer renderer_umbrella = umbrella.GetComponent<Renderer>();
        Renderer renderer_character_height = character_height.GetComponent<Renderer>();

        Bounds bounds_character = renderer_character.bounds;
        Bounds bounds_shadow = renderer_shadow.bounds;
        Bounds bounds_umbrella = renderer_umbrella.bounds;
        Bounds bounds_character_height = renderer_character_height.bounds;

        Vector3 character_pos = character.transform.localPosition;
        Vector3 umbrella_shadow_pos = umbrella_shadow.transform.localPosition;

        sizeX_character = bounds_character.size.x;
        sizeZ_character = bounds_character.size.z;

        sizeX_umbrella_shadow = bounds_shadow.size.x;
        sizeX_umbrella_shadow = bounds_shadow.size.z;


        if (Mathf.Abs(character_pos.x - umbrella_shadow_pos.x) < getDistance(sizeX_character, sizeX_umbrella_shadow))
        {
            is_covered_x = true;
        }
        else
        {
            is_covered_x = false;
        }

        if (Mathf.Abs(character_pos.z - umbrella_shadow_pos.z) < getDistance(sizeZ_character, sizeZ_umbrella_shadow))
        {
            is_covered_z = true;
        }
        else
        {
            is_covered_z = false;
        }


        // calculate Height Comperation
        heightComperation = calHeightCompareation(bounds_character_height, bounds_umbrella);

        int currentHp, currentStamina;
        int.TryParse(Health_number.text, out currentHp);
        int.TryParse(Stamina_number.text, out currentStamina);

        // calculate cover Rate and deduct hp
        if (is_covered_z && is_covered_x)
        {
            isCovered = true;

            coverRate = calCoverRate(bounds_character, bounds_shadow);

            /*isCovered = false;
            //int currentValue;
            //if (int.TryParse(Stamina_number.text, out currentValue))
            //{
            //    currentValue -= 1; // 数值减1
            //    Stamina_number.text = currentValue.ToString(); // 更新到文本框
            //    loadDataController.SaveData(currentValue.ToString());
            //    loadDataController.LoadData();
            //}
            */
        }
        else
        {
            isCovered = false;
            currentHp -= 3;
        }
        loadDataController.SaveHpData(currentHp.ToString());

        // deduct hp according to height comperation
        //if (heightComperation < 0)
        //{
        //    currentHp -= 1;
        //    loadDataController.SaveHpData(currentHp.ToString());
        //}

        loadDataController.SaveStaminaData(currentStamina.ToString());
        loadDataController.LoadData();

        isTimeToCheckout = true;

        Debug.Log(isCovered);
        Debug.Log(currentHp);
    }

    private float calCoverRate(Bounds bounds_character, Bounds bounds_shadow)
    {
        float coverRate;

        coverRate = (Mathf.Abs(bounds_shadow.max.x - bounds_character.min.x) * Mathf.Abs(bounds_shadow.min.z - bounds_character.max.z)) / (Mathf.Abs(bounds_character.size.x * bounds_character.size.z));

        return coverRate;
    }

    private float calHeightCompareation(Bounds bounds_character_height, Bounds bounds_umbrella)
    {
        float heightComperation;

        heightComperation = bounds_umbrella.max.y - bounds_character_height.max.y;
        Debug.Log(bounds_umbrella.max.y);
        Debug.Log(bounds_character_height.max.y);

        return heightComperation;
    }

    private float getDistance(float size1, float size2)
    {
        float distance = 0.0f;

        distance = 0.5f * Mathf.Abs(size1 + size2);
        return distance;
    }

    public void changeTexture()
    {
        int currentHp;
        currentHp = loadDataController.gameData.HealthPoint;

        if (currentHp == 7)
        {
            heart01.texture = newtexture;
            heart02.texture = newtexture;
            heart03.texture = newtexture;
        }

        if (currentHp == 8)
        {
            heart02.texture = newtexture;
            heart03.texture = newtexture;
        }
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
