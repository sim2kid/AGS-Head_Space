using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItemPickup : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";
    [Tooltip("[Optional] Audio OneShot to be played durring an interaction.")] [SerializeField] private AudioClip audioClip = null;
    
    private AudioSource audioSource;
    private InventoryItem inventoryItemInfo;


    private void Start()
    {
        inventoryItemInfo = GetComponent<InventoryItem>();
        try
        {
            audioSource = GetComponent<AudioSource>();
        }
        catch { }
    }


    private void putItemInInventory() {
        InventoryController theInvCon = GameObject.FindObjectOfType<InventoryController>();
        if (theInvCon.AddItemToInventory(inventoryItemInfo)) {
            Destroy(this.gameObject);
        }
    }


    public void Interact()
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        putItemInInventory();
    }
    public string GetTooltip()
    {
        return tooltip;
    }
    public bool IsHeld()
    {
        return false;
    }
}
