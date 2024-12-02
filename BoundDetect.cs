using UnityEngine;

public class BoundDetect : MonoBehaviour
{

    public GameObject Element;

    private float maxLeft = 2f;
    //private float maxRight = -8f;
    //private float minHight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftBound"))
        {
            Debug.Log("Left!!");
            Vector3 tempPos = Element.transform.position;

            if (Mathf.Abs(tempPos.x) >= maxLeft)
            {
                Debug.Log("stop");
                tempPos.x = maxLeft;
                Element.transform.position = tempPos;
            }
        }

    }
}
