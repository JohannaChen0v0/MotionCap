using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject; // ��Ҫ���Ƶ�����

    void Start()
    {
        targetObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayMenu()
    {
        targetObject.SetActive(true);
    }

    public void closeMenu()
    {
        targetObject.SetActive(false);
    }
}
