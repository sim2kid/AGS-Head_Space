using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> 
/// This class is used to hold interactable objects. It used IInteract as a template.
///</summary> 

public class InteractHold : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltipPickUp = "Huh... This hasn't been filled in... Oops!!";
    [Tooltip("What shows up when you hold the object")] [SerializeField] private string tooltipPutDown = "Huh... This hasn't been filled in... Oops!!";
    [Tooltip("The objects position when held")] [SerializeField] private Vector3 heldOffset = new Vector3(1,0,0);
    [Tooltip("The object's rotation when held")] [SerializeField] private Vector3 heldRotation = new Vector3(0, 0, 0);
    [Tooltip("[Optional] Audio OneShot to be played durring an interaction.")] [SerializeField] private AudioClip audioClip = null;


    private GameObject player;
    private AudioSource audioSource;
    private bool isObjHeld;
    private Rigidbody rb;


    void Start()
    {
        isObjHeld = false;
        player = GameObject.Find("MainCamera");
        try
        {
            rb = GetComponent<Rigidbody>();

        } catch { }

        try
        {
            audioSource = GetComponent<AudioSource>();
        } catch { }

    }
    void Update()
    {
        if (isObjHeld) {
            Vector3 preMove = transform.position;

            transform.position = player.transform.position;
            transform.position += player.transform.forward * heldOffset.x;
            transform.position += player.transform.right * heldOffset.z;
            transform.position += player.transform.up * heldOffset.y;

            transform.rotation = player.transform.rotation;
            transform.Rotate(heldRotation);
            if (rb != null) {
                Vector3 postMove = transform.position;
                int velocityStrength = 3;
                rb.velocity = (postMove - preMove) * velocityStrength;
            }
        }
    }


    public void Interact()
    {
        isObjHeld = !isObjHeld;
        bool canPlayAudio = audioClip != null && audioSource != null;
        if (canPlayAudio) {
            audioSource.PlayOneShot(audioClip);
        }
    }
    public string GetTooltip()
    {
        if (isObjHeld)
        {
            return tooltipPutDown;
        }
        else
        {
            return tooltipPickUp;
        }
    }
    public bool IsHeld()
    {
        return isObjHeld;
    }
}
