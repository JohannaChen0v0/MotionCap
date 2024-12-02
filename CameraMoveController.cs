using TMPro;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(0f, 0f, 0f); // target position
    public float rotationSpeed = 2f; // rotation speed
    public float movementSpeed = 2f; // move speed

    private Quaternion targetRotation;
    private Vector3 startPosition;
    private bool isRotating = false;
    private bool isDragging = false;
    private Vector2 dragStartPosition; // start position of mouse's drag

    private int rotationTimes = 0;
    private float orignal_x = -1.2f;
    private float orignal_y = 3.0f;
    private float orignal_z = -16.0f;

    void Update()
    {
        // continuing listen the mouse's input
        HandleInput();

        if (isRotating)
        {
            SmoothRotateAndMove();
        }
    }

    void HandleInput()
    {
        if (isRotating)
            return;

        // listen the right click of mouse
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition; // record the start position of mouse
        }

        // listen the lose of right button
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;

            // get the direction of dragging
            Vector2 dragEndPosition = Input.mousePosition;
            float dragDistanceX = dragEndPosition.x - dragStartPosition.x;

            if (dragDistanceX < -50) // we rotate the camera only if the player drag for a distance
            {
                TriggerRotationAndMoveLeft();
            }
            else if (dragDistanceX > -50)
            {
                TriggerRotationAndMoveRight();
            }

        }
    }

    void TriggerRotationAndMoveLeft()
    {
        // set the target position
        startPosition = transform.position;
        Vector3 targetPos = new Vector3(8f, 3f, -7f);

        // for the limitation of player's view
        // if the camera is not on the orignal position, we change it back
        if (rotationTimes == -1)
        {
            targetPos = new Vector3(orignal_x, orignal_y, orignal_z);
        }

        Debug.Log(rotationTimes);

        if (rotationTimes <= 0)
        {
            targetPosition = targetPos;
            // set the target rotation
            targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -90, 0));
            isRotating = true;
            rotationTimes += 1;
        }

    }

    void TriggerRotationAndMoveRight()
    {
        // set the target position
        startPosition = transform.position;
        Vector3 targetPos = new Vector3(-10f, 3f, -7f);

        if (rotationTimes == 1)
        {
           targetPos = new Vector3(orignal_x, orignal_y, orignal_z);
        }

        Debug.Log(rotationTimes);

        if (rotationTimes >= 0)
        {
            targetPosition = targetPos;
            // set the target rotation
            targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 90, 0));
            isRotating = true;
            rotationTimes -= 1;
        }

    }

    void SmoothRotateAndMove()
    {
        // rotate smoothly
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // move smoothly
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);

        // check if the camera is on the target position
        // the value can not be too small or 0 because of the calculate little error, especially for the rotation
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f &&
            Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isRotating = false; // stop rotating
        }
    }
}
