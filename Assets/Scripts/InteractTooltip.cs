using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTooltip : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";

    public void interact()
    {
        
    }

    public string getTooltip()
    {
        return tooltip;
    }
    public bool isHeld()
    {
        return false;
    }
}
