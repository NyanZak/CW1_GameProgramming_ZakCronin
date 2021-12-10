using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject[] cameraList;
    private int currentCamera;
    void Start()
    {
        currentCamera = 0;
        for (int i = 0; i < cameraList.Length; i++)
        {
        }

        if (cameraList.Length > 0)
        {
            cameraList[0].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            currentCamera++;
            if (currentCamera < cameraList.Length)
            {
                cameraList[currentCamera - 1].gameObject.SetActive(false);
                cameraList[currentCamera].gameObject.SetActive(true);
            }
            else
            {
                currentCamera = 0;
                cameraList[currentCamera].gameObject.SetActive(true);
            }
        }
    }
}
