using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("The GameObject of the Player's Camera")] [SerializeField] private GameObject camera;
    [Tooltip("How Fast the Player's Camera moves")] [SerializeField] private float mouseSensitivity = 100f;
    [Tooltip("How far the Player can reach")] [SerializeField] private float reachDistance = 2.5f;
    [Tooltip("How Fast the Player Moves")] [SerializeField] private float movementSensitivity = 7f;
    [Tooltip("Strength of Gravity")] [SerializeField] private float gravity = -9.81f;

    [Tooltip("The Ground Check Object")] [SerializeField] private Transform groundcheck;
    [Tooltip("The Radius to check for the ground")] [SerializeField] private float groundDistance = 0.4f;
    [Tooltip("What layer the ground is located in.")] [SerializeField] private LayerMask groundMask;

    [Tooltip("The height of a jump")] [SerializeField] private float jumpHeight = 1f;

    private bool isPaused = false;
    private bool isGrounded;
    private float xRotation = 0f;
    private CharacterController controller;
    private InGameMenuController ui;

    private IInteract lastHeld;
    private bool isObjHeld;


    private Vector3 velocity;

    private void Start()
    {
        ui = GameObject.Find("EventSystem").GetComponent<InGameMenuController>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        isObjHeld = false;
        lastHeld = null;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        if (!isPaused)
        {
            int layerMask = 1 << 10;
            RaycastHit hit;
            if (isObjHeld && lastHeld != null)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    lastHeld.interact();
                    isObjHeld = lastHeld.isHeld();
                }
                ui.setInteractiveText(lastHeld.getTooltip());
            }
            else 
            {
                bool raycast = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, reachDistance, layerMask);
                if (raycast)
                {
                    GameObject hitObj = hit.transform.gameObject;
                    IInteract io = (IInteract)hitObj.GetComponent("IInteract"); ;
                    if (Input.GetButtonDown("Interact"))
                    {
                        // Interact Before IsHeld Check ALWAYS!!! //
                        io.interact();
                        isObjHeld = io.isHeld();
                        if (isObjHeld == true) {
                            lastHeld = io;
                        }
                    }
                    ui.setInteractiveText(io.getTooltip());
                }
                else
                {
                    ui.setInteractiveText("");
                }
            }
            
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            controller.Move(move * movementSensitivity * Time.deltaTime);
            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
        else {
            ui.setInteractiveText("");
        }

        if (Input.GetButtonDown("Escape"))
        {
            setPause(!isPaused);
        }

    }

    public bool isGamePaused() {
        return isPaused;
    }

    public void setPause(bool torf) {
        if (torf == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        isPaused = torf;
    }
}
