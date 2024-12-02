using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using System;
using TMPro;
using UnityEngine.UI;

public class moveInCheckOutMode : MonoBehaviour
{
    public Transform target;        // target position and rotation
    public Camera mainCamera;
    public float moveSpeed = 2f;    // move speed
    public float rotateSpeed = 2f;  // rotate speed
    private bool isMoving = false;
    public GameObject targetObject;
    public GameObject hideUmbrella;
    public float orthographicSize = 5f;
    // change to orthographic mode to see the projection relationship

    public GameObject mainCanvas;
    public RawImage starImg;
    public GameObject star;
    public Texture2D star2Img;
    public Texture2D star3Img;
    public Texture2D star1Img;
    public GameObject vp1;
    public GameObject vp2;
    public GameObject exitBtn;
    public TextMeshProUGUI Health_number;


    public void MoveAndRotateToTarget()
    {
        Debug.Log("MoveAndRotateToTarget triggered");
        isMoving = true; // start moving

        mainCanvas.SetActive(false);

        int currentHp;
        int.TryParse(Health_number.text, out currentHp);

        if (currentHp == 7)
            starImg.texture = star1Img;
        else if (currentHp == 8)
            starImg.texture = star2Img;
        else if (currentHp == 10)
            starImg.texture = star3Img;

        star.SetActive(true);

        vp1.SetActive(true);
        vp2.SetActive(true);
        exitBtn.SetActive(true);

        mainCamera.orthographic = true;
        mainCamera.orthographicSize = orthographicSize;
    }

    private void Start()
    {
        mainCanvas.SetActive(true);
    }

    void Update()
    {
        if (isMoving)
        {
            // move smoothly
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);

            // rotate smoothly
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotateSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f &&
                Quaternion.Angle(transform.rotation, target.rotation) < 0.1f)
            {
                Debug.Log("Movement finished");
                isMoving = false; // stop moving

                Invoke("DelayedFunction", 2f); // delay 2s and call DelayedFunction
            }

        }

    }

    void DelayedFunction ()
    {
        targetObject.SetActive(true);
        hideUmbrella.SetActive(false);
    }

}
