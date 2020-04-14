using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary> 
/// This interface is used as a structure for interactable objects.
///</summary> 
interface IInteract
{
    void Interact();
    string GetTooltip();
    bool IsHeld();
}
