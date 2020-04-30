using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryButtonScript : MonoBehaviour
{
    private TextMeshProUGUI itemTitleBox, itemDescriptionBox;

    // Start is called before the first frame update
    void Start()
    {
        itemTitleBox = GameObject.Find("Description/Item Name Box").GetComponent<TextMeshProUGUI>();
        itemDescriptionBox = GameObject.Find("Description/Item Description Box").GetComponent<TextMeshProUGUI>();

        //Debug.Log((itemTitleBox == null) + "  " + (itemDescriptionBox == null));
    }
    public void SetTheInventoryText(InventoryItem itemInfo)
    {
        itemTitleBox.text = itemInfo.GetName();
        itemDescriptionBox.text = itemInfo.GetDescription();
    }
}
