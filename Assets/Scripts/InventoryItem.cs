using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [Tooltip("What shows up as the item's name in menu")] [SerializeField] private string itemName = "Unknown Item";
    [Tooltip("What shows up as the item's description in menu")] [SerializeField] private string itemDescription = "This unknown item comes from the land of 'forgetting to add in content' and is only found in buggy releases of this game!";
    [Tooltip("The image for the item")] [SerializeField] private Sprite itemImage;

    public string GetName() {
        return itemName;
    }
    public string GetDescription()
    {
        return itemDescription;
    }
    public Sprite GetImage()
    {
        return itemImage;
    }
    public void SetContent(InventoryItem otherItem) {
        itemName = otherItem.GetName();
        itemDescription = otherItem.GetDescription();
        itemImage = otherItem.GetImage();
    }
}
