using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> 
/// This class is used to show tooltips on objects that the player is looking at. It used IInteract as a template.
///</summary> 
///
public class InteractTooltip : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";
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
