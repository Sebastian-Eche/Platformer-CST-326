using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float acceleration = 100f;
    public float maxSpeed = 15f;
    // public float minSpeed = -15f;
    public float jumpImpulse = 30f;
    public float jumpBoost = 3f;
    public float dashPower = 100f;
    public float fallGravity = 1.1f;
    public bool isGrounded = false;
    public GameObject self;
    public RaycastHit hitInfo;
    public BrickBreaker brickBreaker;
    public GameObject inactiveQuestionBlock;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float dashTime = 3f;
    private float dashCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        dashCounter = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += Vector3.right * horizontalMovement * Time.deltaTime * acceleration;

        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.03f;
        
        Vector3 startPoint = transform.position;
        Vector3 endPointGround = startPoint + Vector3.down * halfHeight;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = (isGrounded) ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPointGround, lineColor, 0f, true);

        if(Physics.Raycast(startPoint, Vector3.up, out hitInfo, 1.8f)){
            // Debug.Log($"{hitInfo.collider.gameObject.name} hit");
            if(hitInfo.collider.gameObject.CompareTag("Brick")){
                    brickBreaker.points += 100;
                    brickBreaker.changeUI();
                    Destroy(hitInfo.collider.gameObject);
                }else if(hitInfo.collider.gameObject.CompareTag("Question")){
                    hitInfo.collider.gameObject.tag = "QuestionInactive";
                    Vector3 newPos = hitInfo.collider.gameObject.transform.position;
                    GameObject inactiveQuestion = Instantiate(inactiveQuestionBlock, newPos, Quaternion.identity);
                    brickBreaker.GetComponent<BrickBreaker>();
                    brickBreaker.coins++;
                    brickBreaker.points += 200;
                    brickBreaker.changeUI();
                    StartCoroutine(brickBreaker.coinSpawn(hitInfo));
                    hitInfo.collider.gameObject.SetActive(false);
                    // Destroy(hitInfo.collider.gameObject);
                }
        }


        
        // bool runJump = false;

        if(isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            // runJump = true;
            maxSpeed = 15;
            Vector3 newVel = rb.velocity;
            newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
            // rb.AddForce(horizontalMovement * runBoost * Vector3.right, ForceMode.Force);
        }

        if(isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            // runJump = true;
            maxSpeed = 6;
            Vector3 newVel = rb.velocity;
            newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
            // rb.AddForce(horizontalMovement * runBoost * Vector3.right, ForceMode.Force);
        }


        if(dashCounter < 0f && Input.GetKey(KeyCode.LeftAlt))
        {
            rb.AddForce(horizontalMovement * dashPower * Vector3.right, ForceMode.Impulse);
            dashCounter = dashTime;
            Debug.Log("DASHED");
        }else{
            dashCounter -= Time.deltaTime;
            Debug.Log($"Cooldown: {dashCounter}" );
        }

        //https://www.youtube.com/watch?v=RFix_Kg2Di0 helped with coyoteTime
        if(isGrounded){
            coyoteTimeCounter = coyoteTime;
        }else{
            coyoteTimeCounter -= Time.deltaTime;
        }

        


        if(coyoteTimeCounter > 0f && Input.GetKeyDown(KeyCode.Space))
        {
            // holdingJump = true;

            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            
        }else if(!isGrounded && Input.GetKey(KeyCode.Space)){
            if(rb.velocity.y > 0f){
                rb.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
            }
            coyoteTimeCounter = 0f;
        }

        // if(lastGroundedTime > 0 && lastJumpTime > 0 && isGrounded)
        // {
        //     rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        // }

        if(!isGrounded && rb.velocity.y < 0)
        {
            // Debug.Log(rb.velocity.y);
            rb.AddForce(Vector3.down * fallGravity, ForceMode.Acceleration);
        }

        if(Math.Abs(rb.velocity.x) > maxSpeed)
        {
            maxSpeed = 10;
            Vector3 newVel = rb.velocity;
            newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
        }

        if(isGrounded && Math.Abs(horizontalMovement) < .50f)
        {
            Vector3 newVel = rb.velocity;
            newVel.x *= 0.5f - Time.deltaTime;
            rb.velocity = newVel;
        }
        // rb.velocity *= Math.Abs(horizontalMovement);

        float yaw = (rb.velocity.x > -.001) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        float animationSpeed = rb.velocity.x;
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", Math.Abs(animationSpeed));

        // Debug.Log($"animation speed: {animationSpeed}");

        anim.SetBool("In Air", !isGrounded);

        // Debug.Log($"Velocity: {rb.velocity}");


        // if(rb.velocity.x < -maxSpeed)
        // {
        //     Vector3 newVel = rb.velocity;
        //     newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
        //     rb.velocity = newVel;
        // }

        


        // Debug.Log(rb.velocity);
        // maxSpeed = 8;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Avoid")){
            // self.SetActive(false);
            transform.position = new Vector3(11, 1.65f, 0f);
        }else if(other.gameObject.CompareTag("End")){
            // Debug.Log("END");
            SceneManager.LoadScene("World 1-2");
        }
    }

}
