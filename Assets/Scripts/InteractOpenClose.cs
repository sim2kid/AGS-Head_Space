using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractOpenClose : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";
    [Tooltip("[Optional] Audio OneShot to be played durring an interaction.")] [SerializeField] private AudioClip audioClip = null;

    private AudioSource audioSource;
    private Animator anime;

    private void Start()
    {
        anime = GetComponent<Animator>();

        try
        {
            audioSource = GetComponent<AudioSource>();
        }
        catch { }
    }
    private void Update()
    {

    }

    public void interact()
    {
        anime.SetTrigger("OpenClose");
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public string getTooltip()
    {
        return tooltip;
    }

    public bool isHeld() {
        return false;
    }
}
