using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IneractDialogue : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";
    [Tooltip("What conversation tree would you like to open?")] [SerializeField] private string conversation = "test";
    [Tooltip("[Optional] Audio OneShot to be played durring an interaction.")] [SerializeField] private AudioClip audioClip = null;
    
    private AudioSource audioSource;


    private void Start()
    {
        try
        {
            audioSource = GetComponent<AudioSource>();
        }
        catch { }
    }


    public void Interact()
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        GameObject.Find("EventSystem").GetComponent<InGameMenuController>().OpenDialogue(conversation);
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
