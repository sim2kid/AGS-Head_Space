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
    [Tooltip("Audio OneShot to be played durring footsteps.")] [SerializeField] private AudioClip stepAudio = null;
    [Tooltip("How often a step can be played (In Seconds)")] [SerializeField] private float stepFrequency = 0.68f;


    private AudioSource audioSource;
    private Rigidbody rb;
    private float lastStep;
    private bool isPaused = false;
    private bool isEscaped, inInventory = false;
    private bool isGrounded;
    private float xRotation = 0f;
    private CharacterController controller;
    private InGameMenuController ui;
    private IInteract lastHeld;
    private bool isObjHeld;


    private Vector3 velocity;

    private void Start()
    {
        lastStep = 0;
        ui = GameObject.Find("EventSystem").GetComponent<InGameMenuController>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        isObjHeld = false;
        lastHeld = null;
        try
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }
        catch { }
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        bool canFootStepsExist = isGrounded && velocity.y < 0;
        if (canFootStepsExist)
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
            bool isMovingOnGround = (x != 0 || z != 0) && isGrounded;
            if (isMovingOnGround)
            {
                float timeSinceLastStep = Time.time - lastStep;
                if (timeSinceLastStep > stepFrequency)
                {
                    if (stepAudio != null && audioSource != null)
                    {
                        lastStep = Time.time;
                        audioSource.PlayOneShot(stepAudio);
                    }
                }
            }
            else /* If not on ground or not moving */
            {
                lastStep = 0;
            }

            int layerMask = 1 << 10; // Layer 10 is Interactable Layers

            bool isThereAHeldObject = isObjHeld && lastHeld != null;
            if (isThereAHeldObject) // Then use that object
            {
                if (Input.GetButtonDown("Interact"))
                {
                    lastHeld.Interact();
                    isObjHeld = lastHeld.IsHeld();
                }
                ui.setInteractiveText(lastHeld.GetTooltip());
            }
            else // Find nearby object
            {
                RaycastHit foundObject;
                bool isRaycastSuccessful = Physics.Raycast(camera.transform.position, camera.transform.forward, out foundObject, reachDistance, layerMask);
                if (isRaycastSuccessful)
                {
                    GameObject hitObj = foundObject.transform.gameObject;
                    IInteract io = (IInteract)hitObj.GetComponent("IInteract"); ;
                    if (Input.GetButtonDown("Interact"))
                    {
                        // Interact Before IsHeld Check ALWAYS!!! //
                        io.Interact();
                        isObjHeld = io.IsHeld();
                        if (isObjHeld == true)
                        {
                            lastHeld = io;
                        }
                    }
                    ui.setInteractiveText(io.GetTooltip());
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
        else
        {
            ui.setInteractiveText("");
        }

        if (Input.GetButtonDown("Inventory") && !isEscaped)
        {
            SetInventory(!inInventory);
        }
        else if(Input.GetButtonDown("Escape"))
        {
            if (!inInventory)
            {
                SetEscaped(!isEscaped);
            }
            else 
            {
                SetInventory(false);
                SetEscaped(false);
            }
        }

    }
    private void setPause(bool trueOrFalse)
    {
        if (trueOrFalse == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        isPaused = trueOrFalse;
    }


    public bool IsGamePaused()
    {
        return isPaused;
    }
    public bool IsEscaped()
    {
        return isEscaped;
    }
    public bool InInventory()
    {
        return inInventory;
    }
    public void SetEscaped(bool trueOrFalse)
    {
        isEscaped = trueOrFalse;
        setPause(isEscaped);
    }

    public void SetInventory(bool trueOrFalse)
    {
        inInventory = trueOrFalse;
        setPause(inInventory);
    }
}