using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/ for line 29

public class BrickBreaker : MonoBehaviour
{
    public Camera cameraHelp;
    public TextMeshProUGUI coin;
    public TextMeshProUGUI player;
    public GameObject coinObject;
    public float coinLift = 0.001f; 
    private Vector3 newMousePosition;
    private int coins;
    private int points;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mosPos = cameraHelp.ScreenToWorldPoint(Input.mousePosition);
        mosPos.z = 0f;
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("PRESSED");
            Debug.Log($"MOUSE POSITION: {Input.mousePosition}");
            Debug.Log($"NEW MOUSE: {mosPos}");
            // Debug.Log($"CAMERA size: {cameraHelp.sensorSize}");
            // newMousePosition = new Vector3(Input.mousePosition.x/24f, Input.mousePosition.y/24f, Input.mousePosition.z/24f);
            // Debug.Log($"OFFSET POSITION: {newMousePosition}");
            // Debug.DrawRay(mosPos, Vector3.down * 2, Color.black);

            RaycastHit hitInfo;
            if(Physics.Raycast(mosPos, Vector3.up, out hitInfo, 0.5f)){
                Debug.Log($"{hitInfo.collider.gameObject.name} hit");
                Debug.DrawRay(mosPos, Vector3.up, Color.black);
                Debug.Log($"{hitInfo.collider.gameObject}");
                if(hitInfo.collider.gameObject.CompareTag("Brick")){
                    coins++;
                    Destroy(hitInfo.collider.gameObject);
                }else if(hitInfo.collider.gameObject.CompareTag("Question")){
                    coins++;
                    points += 200;
                    StartCoroutine(coinSpawn(hitInfo));
                }
                coin.text = $"x{coins.ToString()}";
                player.text = $"MARIO\n {points}";


            }
        }
        
    }

    IEnumerator coinSpawn(RaycastHit hitInfo)
    {   
        float timeElapsed = 0;
        GameObject questionHit = hitInfo.collider.gameObject;
        GameObject newCoin = Instantiate(coinObject, new Vector3(questionHit.transform.position.x,questionHit.transform.position.y,0f), Quaternion.identity);
        Rigidbody rb = newCoin.GetComponent<Rigidbody>();
        // float sameX = questionHit.transform.position.x;
        // float sameY = questionHit.transform.position.y;
        while(timeElapsed < coinLift)
        {
            // float newY = timeElapsed + newCoin.transform.position.y;
            rb.AddForce(Vector3.up, ForceMode.Force);
            // newCoin.transform.SetPositionAndRotation(new Vector3(sameX, newY, 0f), new Quaternion(0f, newY,0f,0f));
            timeElapsed += Time.deltaTime;
            // Debug.Log(timeElapsed);
            yield return null;
        }
        // Debug.Break();
        Destroy(newCoin);
    }
}
