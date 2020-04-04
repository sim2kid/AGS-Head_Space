using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBounce : MonoBehaviour, IInteract
{
    [Tooltip("What shows up when you hover over the object")] [SerializeField] private string tooltip = "Huh... This hasn't been filled in... Oops!!";

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    public void interact() {
        Debug.Log("Oh, you clicked this bounching ball!");
    }

    public string getTooltip() {
        return tooltip;
    }
}
