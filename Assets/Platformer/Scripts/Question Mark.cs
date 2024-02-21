using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    public float rate = 1f;
    public float newTexturePart = 1f;

    // private float frameCount = 0f;
    private float timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
          
    }



     
        
    // Update is called once per frame
    void Update()
    {
        // ++frameCount;
        // if(frameCount == 100)
        // {
        //     MeshRenderer mr = GetComponent<MeshRenderer>();
        //     if(rate >= 5f){
        //         rate = 1f;
        //     }
        //     mr.material.mainTextureOffset = new Vector2(-1, -0.2f * rate);
        //     rate++;
        timePassed += Time.deltaTime;
        if(timePassed >= 0.2f){
            MeshRenderer mr = GetComponent<MeshRenderer>();
            if(rate >= 5f){
                rate = 1f;
            }
            mr.material.mainTextureOffset = new Vector2(-1, -0.2f * rate);
            rate++;
            // StartCoroutine(newTexture());
            timePassed = 0;
        }
        //     frameCount = 0;
        // }

        // MeshRenderer mr = GetComponent<MeshRenderer>();
        // if(rate >= 5f){
        //     rate = 1f;
        // }
        // mr.material.mainTextureOffset = new Vector2(-1, -0.2f * rate);
        // rate++;
    }

    IEnumerator newTexture()
    {
        float timeElapsed = 0;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if(rate >= 5f){
            rate = 1f;
        }
        while(timeElapsed < newTexturePart)
        {
            mr.material.mainTextureOffset = new Vector2(-1, -0.2f * rate);
            timeElapsed+= Time.deltaTime;
            yield return null;
        }
        rate++;
        
    }

}
