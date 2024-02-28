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
    public TextMeshProUGUI coin;
    public TextMeshProUGUI player;
    public GameObject coinObject;
    public GameObject failedLevel;
    public float coinLift = 0.001f; 
    private int coins;
    private int points;

    void Update()
    {
        int intTime = 100 - (int)Time.realtimeSinceStartup;
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

        if(intTime == 0){
            intTime = 0;
            failedLevel.SetActive(true);
        }
    }
}
