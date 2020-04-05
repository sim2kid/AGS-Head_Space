using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";
    private Animator anime;

    private void Start()
    {
        anime = GetComponent<Animator>();
    }
    private void Update()
    {

    }

    public void interact()
    {
        anime.SetTrigger("OpenClose");
    }

    public string getTooltip()
    {
        return tooltip;
    }
}
