using UnityEngine;

public class sceneinsight : MonoBehaviour
{
    // get main camera
    public Transform car_model;
    public string objectTag;

    private Ray ra;
    private RaycastHit hit;
    private bool is_element = false;
    private int flag = 0;

    // rotate speed
    public static float rotateSpeed = 10f;
    public static float rotateLerp = 8;

    // move speed
    public static float moveSpeed = 0.5f;
    public static float moveLerp = 10f;
    public static float zoomSpeed = 10f;   // zoom speed
    public static float zoomLerp = 4f;     // use lerp to make the camera move smoothly

    private Vector3 position, targetPosition;
    private Quaternion rotation, targetRotation;
    private float distance, targetDistance;
    // defult distance
    private const float default_distance = 1.0f;


    void Start()
    {
        // initialize the rotation
        targetRotation = Quaternion.identity;
        // the start position of camera
        targetPosition = car_model.position;
        // the start distance of camera (for calculating the zooming scale)
        targetDistance = default_distance;
    }


    void Update()
    {
        //Debug.Log("camera button ");
        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");


        is_element = false;

        if (Input.GetMouseButton(0))
        {
            ra = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ra, out hit) && hit.collider.tag == objectTag)
            {
                // Debug.Log(hit.collider.tag);
                is_element = true;
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

        // detect the click of middle button of mouse
        if (Input.GetMouseButton(2))
        {
            dx *= moveSpeed;
            dy *= moveSpeed;
            targetPosition -= transform.up * dy + transform.right * dx;
        }

        // detect the click of right button of mouse
        if (Input.GetMouseButton(1))
        {
            dx *= rotateSpeed;
            dy *= rotateSpeed;
            if (Mathf.Abs(dx) > 0 || Mathf.Abs(dy) > 0)
            {
                // get the euler angles of the camera
                Vector3 angles = transform.rotation.eulerAngles;
                // Euler angles represent rotate in axis order, e.g. angles.x = 30 means rotate in 30 degrees through x-axis
                angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
                angles.y += dx;
                angles.x -= dy;
                // calculate the angles of rotation
                targetRotation.eulerAngles = new Vector3(angles.x, angles.y, 0);
            }
        }

        if (flag == 0 && is_element == false)
        {
            // zoom in / out with the scrollwheel
            targetDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
    }

    private void FixedUpdate()
    {
        rotation = Quaternion.Slerp(rotation, targetRotation, Time.deltaTime * rotateLerp);
        position = Vector3.Lerp(position, targetPosition, Time.deltaTime * moveLerp);
        distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * zoomLerp);
        // Set the rotation of the camera
        transform.rotation = rotation;
        // Set the position of the camera
        transform.position = position - rotation * new Vector3(0, 0, distance);
    }
}