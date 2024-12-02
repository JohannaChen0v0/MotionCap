using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class camera : MonoBehaviour
{
    //public Camera ca;
    private Ray ra;
    private RaycastHit hit;
    public LayerMask raycastLayerMask; // Set the raycast layer mask
    // we want the ray goes through the character and hits the umbrella

    private bool is_element = false;
    private float scale = 10.0f;    //defult scale
    private int flag = 0;
    private int islimited = 1;

    private float sizeX = 0.0f;
    private float sizeZ = 0.0f;
    private float squareSize = 0.0f;

    private int maxStamina = 100;
    public float stamnia = 100;

    public GameObject Element;
    public GameObject Shadow;
    public GameObject Ground;
    public GameObject warningTooLarge;
    public GameObject warningTooSmall;
    public string objectTag;

    public Image staminaBarImage;    // stamina UI image
    private float currentStamina;

    public TextMeshProUGUI stamina_number;

    public LoadDataController loadDataController;


    // Use this for initialization
    void Start()
    {
        warningTooLarge.SetActive(false);
        warningTooSmall.SetActive(false);
        currentStamina = maxStamina;
        UpdateStaminaBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ra = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ra, out hit, Mathf.Infinity, raycastLayerMask) && hit.collider.tag == objectTag)
            {
                Debug.Log(hit.collider.tag);
                is_element = true;
                Element = hit.collider.gameObject;
                if (flag == 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
        }

        if (flag == 1 && is_element)
        {
            islimited = 1;
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(Element.transform.position);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                targetScreenPos.z);
            Element.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

            //Shadow.transform.position = Element.transform.position;

            Vector3 ShadowTmp = Element.transform.position;
            ShadowTmp.y = Ground.transform.position.y + 4.0f;
            Shadow.transform.position = ShadowTmp;

            Renderer renderer = Element.GetComponent<Renderer>();

            Bounds bounds = renderer.bounds;
            sizeX = bounds.size.x; // length in X-Axis
            sizeZ = bounds.size.z; // length in Z-Axis
            squareSize = Mathf.Max(sizeX, sizeZ); // ensure the square can cover the whole object
            Shadow.transform.localScale = new Vector3(squareSize, 0.02f, squareSize);
        }

        if (flag == 1 && is_element && Input.GetAxis("Mouse ScrollWheel") != 0) // use scrollwheel to change the scale of the umbrella
        {
            //change scale
            Renderer renderer = Element.GetComponent<Renderer>();

            float minScale_x = 4f;
            float maxScale_x = 26f;
            float add_number = (maxScale_x - minScale_x) / maxStamina;


            scale += Input.GetAxis("Mouse ScrollWheel") * 10.0f * add_number; // the return of mouse scrollwheel is 0.1

            if (scale >= maxScale_x)
            {
                islimited = 0;
                scale = maxScale_x;
            }
            else if (scale <= minScale_x)
            {
                islimited = 0;
                scale = minScale_x;
            }
                
            stamnia -= islimited * (Input.GetAxis("Mouse ScrollWheel") * 10.0f);
            DrainStamina(islimited * Input.GetAxis("Mouse ScrollWheel") * 10.0f);

            if (stamnia >= 100)
                stamnia = 100;
            if (stamnia <= 0)
                stamnia = 0;
            stamina_number.text = stamnia.ToString();

            Bounds bounds = renderer.bounds;
            sizeX = bounds.size.x; // length in X-Axis
            sizeZ = bounds.size.z; // length in Z-Axis
            squareSize = Mathf.Max(sizeX, sizeZ); // ensure the square can cover the whole object

            /*
            //if (scale >= maxScale_x)
            //{
            //    scale = maxScale_x;
            //    stamnia = 0;
            //    stamina_number.text = stamnia.ToString();
            //}

            //if (scale <= minScale_x)
            //{
            //    scale = minScale_x;
            //    stamnia = 100;
            //    stamina_number.text = stamnia.ToString();
            //}
            */

            Element.transform.localScale = new Vector3(1 * scale, 1 * scale, 1 * scale);
            Shadow.transform.localScale = new Vector3(squareSize, 0.02f, squareSize);

            if (Element.transform.localScale.x <= minScale_x)
            {
                warningTooSmall.SetActive(true);
            }
            else if (Element.transform.localScale.x >= maxScale_x || stamnia == 0)
            {
                warningTooLarge.SetActive(true);
            }
            else
            {
                warningTooSmall.SetActive(false);
                warningTooLarge.SetActive(false);
            }
        }

    }

    private void UpdateStaminaBar()
    {
        // update the filled percent of stamina bar
        staminaBarImage.fillAmount = currentStamina / maxStamina;
    }

    public void DrainStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaBar();
    }


}