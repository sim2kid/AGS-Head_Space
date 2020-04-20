using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> 
/// This class is used to trigger a BounceBall animation on an Animator. It used IInteract as a template.
///</summary> 

public class InteractBounce : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";


    private Animator anime;


    private void Start()
    {
        anime = GetComponent<Animator>();
    }


    public void Interact() {
        anime.SetTrigger("BounceBall");
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
