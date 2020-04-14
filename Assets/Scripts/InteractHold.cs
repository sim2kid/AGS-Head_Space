using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
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

    // Update is called once per frame
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
                rb.velocity = (postMove - preMove) * 3;
            }
        }
    }
    public void interact()
    {
        isObjHeld = !isObjHeld;
        if (audioClip != null && audioSource != null) {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public string getTooltip()
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
    public bool isHeld()
    {
        return isObjHeld;
    }
}
