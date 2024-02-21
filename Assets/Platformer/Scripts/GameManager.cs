using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Camera scrollingCam;
    public float cameraSpeed = 5f;

    void Update()
    {
        int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timeStr = $"Time \n {intTime}";
        timerText.text = timeStr;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            scrollingCam.transform.Translate(cameraSpeed * Time.deltaTime, 0f, 0f);
        }else if(Input.GetKey(KeyCode.LeftArrow))
        {
            if(scrollingCam.transform.position.x > 0)
            {
                scrollingCam.transform.Translate(-cameraSpeed * Time.deltaTime, 0f, 0f);
            }
        }
        
    }

}
