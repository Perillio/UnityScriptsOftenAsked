using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Player movement controller for objects with and without rigidbody. If rigidbody is found, the player can jump, else the player can move up and down.

public class PlayerController : MonoBehaviour
{
    
    //-------------------------------- ( SETUP YOUR CODE HERE : ) ------------------------------------------//
    public void DoAction()
    {
        Debug.Log("ACTION!");
        //Action code here ...
    }

    //-------------------------------- ( DO NOT CHANGE AFTER HERE : ) -------------------------------------//

    [Header("Controls:")]
    public KeyCode p_forward = KeyCode.W;
    public KeyCode p_backward = KeyCode.S;
    public KeyCode p_strafeLeft = KeyCode.A;
    public KeyCode p_strafeRight = KeyCode.D;
    public KeyCode p_up = KeyCode.Space;
    public KeyCode p_down = KeyCode.X;
    public KeyCode p_action = KeyCode.F;
    public bool isJumpInsteadUp = false;
    public float jumpForce = 350f;
    [TextArea]
    public string important = "Rotation is handled by Mouse. Up and Down sight has to be done in camera ! (Also Zooming etc)";

    [Header("Player Settings:")]
    public float speed = 10f;
    public float mouseSensitivity = 25f;

    [Header("DEBUG:")]
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 movement;
    public Vector3 viewpoint;
    public float viewDegrees;
    public int currentFPS;
    public int highestFPS;
    public int lowestFPS = 99999;
    public int averageFPS;

    private float frameTimer = 1f;
    private int frameCounter = 0;
    private int totalCounts = 0;
    private int totalFrames = 0;
    private Rigidbody rb;

   
    private void Awake()
    {
        lowestFPS = 99999;
        try
        {
            rb = transform.GetComponent<Rigidbody>();
            if (rb != null) { isJumpInsteadUp = true; } else { isJumpInsteadUp = false; }
        }
        catch 
        {
            isJumpInsteadUp = false;
        }
    }
    public void DoJump()
    {
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Acceleration);
    }
    void Update()
    {
        float h = 0;
        if (Input.GetKey(p_strafeLeft) && !Input.GetKey(p_strafeRight)) { h = -1; }
        if (!Input.GetKey(p_strafeLeft) && Input.GetKey(p_strafeRight)) { h = 1; }
        float v = 0;
        if (Input.GetKey(p_backward) && !Input.GetKey(p_forward)) { v = -1; }
        if (!Input.GetKey(p_backward) && Input.GetKey(p_forward)) { v = 1; }
        float ud = 0f;
        if (Input.GetKey(p_up) && !Input.GetKey(p_down)) { ud = 1f; }
        if (!Input.GetKey(p_up) && Input.GetKey(p_down)) { ud = -1f; }
        float mx = Input.GetAxis("Mouse X");
        if (Input.GetKeyDown(p_action)) { DoAction(); }
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        if(rb != null) 
        {
            rb.position += right * (h * Time.deltaTime * speed);
            rb.position += forward * (v * Time.deltaTime * speed);
            transform.Rotate(0f, (Time.deltaTime * mx * mouseSensitivity), 0f);
        }
        else
        {
            transform.position += right * (h * Time.deltaTime * speed);
            transform.position += forward * (v * Time.deltaTime * speed);
            transform.Rotate(0f, (Time.deltaTime * mx * mouseSensitivity), 0f);
        }
        if (isJumpInsteadUp)
        {
            if (Input.GetKeyDown(p_up))
            {
                DoJump();
            }
        }
        else
        {
            Vector3 trup = transform.up;
            transform.position += trup * (ud * Time.deltaTime * speed);
        }
        movement = new Vector3(h, ud, v);
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
        viewpoint = transform.forward;
        float vd = transform.rotation.eulerAngles.y * 100;
        vd = (int)vd;
        viewDegrees = vd / 100;
        frameCounter++;
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            currentFPS = frameCounter;
            if (currentFPS > highestFPS) { highestFPS = currentFPS; }
            if (currentFPS < lowestFPS && currentFPS != 0 && totalCounts > 5) { lowestFPS = currentFPS; }
            frameCounter = 0;
            frameTimer = 1f;
            if (totalCounts < 25000)
            {
                totalCounts++;
                totalFrames += currentFPS;
                averageFPS = totalFrames / totalCounts;
            }
        }
    }
}
