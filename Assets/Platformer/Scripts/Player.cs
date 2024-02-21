using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float acceleration = 100f;
    public float maxSpeed = 15f;
    // public float minSpeed = -15f;
    public float jumpImpulse = 30f;
    public float jumpBoost = 3f;
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        // Debug.Log(horizontalMovement);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += Vector3.right * horizontalMovement * Time.deltaTime * acceleration;

   

        Collider col = GetComponent<Collider>();

        float halfHeight = col.bounds.extents.y + 0.03f;
        
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        
        // CapsuleCollider col = GetComponent<CapsuleCollider>();

  
        
        isGrounded = (Physics.Raycast(startPoint, Vector3.down, halfHeight));
        Color lineColor = (isGrounded) ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, Color.blue, 0f, true);



        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if(!isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.up * jumpBoost, ForceMode.Force);

        }
        if(Math.Abs(rb.velocity.x) > maxSpeed)
        {
            Vector3 newVel = rb.velocity;
            newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
        }

        if(isGrounded && Math.Abs(horizontalMovement) < .50f)
        {
            Vector3 newVel = rb.velocity;
            newVel.x *= 1f * Time.deltaTime;
            rb.velocity = newVel;
        }
        rb.velocity *= Math.Abs(horizontalMovement);

        float yaw = (rb.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        // if(rb.velocity.x < -maxSpeed)
        // {
        //     Vector3 newVel = rb.velocity;
        //     newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
        //     rb.velocity = newVel;
        // }

        


        // Debug.Log(rb.velocity);
    }
}
