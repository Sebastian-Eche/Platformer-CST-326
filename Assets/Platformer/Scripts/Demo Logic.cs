using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLogic : MonoBehaviour
{
    public GameObject package;
    public GameObject parachute;
    public float deploymentHeight = 7.5f;
    public float parachuteDrag = 5f;
    public float landingHeight = 0.5f;

    public float chuteOpenDuration = 0.5f;
    // Start is called before the first frame update

    private float originalDrag;
    void Start()
    {
        originalDrag = package.GetComponent<Rigidbody>().drag;
        // parachute.gameObject.SetActive(false);
        parachute.gameObject.SetActive(true);
        StartCoroutine(ExpandParachute());
    }

    IEnumerator ExpandParachute()
    {
        parachute.transform.localScale = Vector3.zero;
        float timeElapsed = 0;
        while(timeElapsed < chuteOpenDuration)
        {
            float newScale = timeElapsed / chuteOpenDuration;
            parachute.transform.localScale = new Vector3(newScale, newScale, newScale);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        parachute.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Physics.Raycast(package.transform.position, Vector3.down, out RaycastHit hitInfo, deploymentHeight))
        // {
        //     // change the drag value on the package's rigidbody
        //     package.GetComponent<Rigidbody>().drag = parachuteDrag;

        //     parachute.gameObject.SetActive(true);

        //     Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.red);
        //     if (hitInfo.distance < landingHeight)
        //     {
        //         parachute.SetActive(false);
        //         Debug.Log("LANDED");
        //     }
        // }
        // else
        // {
        //     parachute.gameObject.SetActive(false);
        //     package.GetComponent<Rigidbody>().drag = originalDrag;
        //     Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.cyan);
        // }
    }
}
