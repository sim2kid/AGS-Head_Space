using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Tooltip("The GameObject of the Inventory")] [SerializeField] private GameObject invMenu;
    [Tooltip("The Prefabs for a button")] [SerializeField] private GameObject buttonPrefab;
    [Tooltip("The default offset for the inventory buttons")] [SerializeField] private Vector2 defaultItemPosition = new Vector2(160,30);
    [Tooltip("How far apart the buttons will sit from eachother")] [SerializeField] private Vector2 itemSpacing = new Vector2(95, 80);
    [SerializeField] private GameObject itemTitleBox, itemDescriptionBox;


    private PlayerController pc;
    private bool internalInv;
    private List<GameObject> inventoryItems;


    void Start()
    {
        inventoryItems = new List<GameObject>();
        GameObject player = GameObject.Find("Player");
        invMenu.SetActive(false);
        pc = player.GetComponent<PlayerController>();
        internalInv = pc.InInventory();
    }
    private void Update()
    {
        if (pc.InInventory() != internalInv)
        {
            internalInv = pc.InInventory();
            if (pc.InInventory() == true)
            {
                OpenMenu();
            }
            else
            {
                invMenu.SetActive(false);
            }
        }
    }


    public void OpenMenu()
    {
        invMenu.SetActive(true);
        itemTitleBox.GetComponent<TextMeshProUGUI>().text = "";
        itemDescriptionBox.GetComponent<TextMeshProUGUI>().text = "Please Select an Item.";
    }
    public void BackToGame()
    {
        invMenu.SetActive(false);
        pc.SetInventory(false);
    }
    public bool AddItemToInventory(InventoryItem item) {
        int numberOfItems = inventoryItems.Count;
        if (numberOfItems >= 10) {
            return false;
        }
        GameObject newButton = Instantiate(buttonPrefab);
        newButton.GetComponent<InventoryItem>().SetContent(item);
        Transform itemPanel = invMenu.transform.Find("Items Panel");
        newButton.transform.SetParent(itemPanel);

        // Set the button to the default position //
        newButton.transform.localPosition = Vector2.zero - defaultItemPosition;

        float verticalSpacingMultiplier = Mathf.Floor(numberOfItems / 2f) * -1;
        float horizontalSpacingMultiplier = numberOfItems % 2;

        Vector2 spacingMultiplier = new Vector2(horizontalSpacingMultiplier, verticalSpacingMultiplier);
        newButton.transform.Translate(spacingMultiplier * itemSpacing);

        Image itemImage = newButton.transform.Find("Image").gameObject.GetComponent<Image>();
        itemImage.sprite = item.GetImage();

        inventoryItems.Add(newButton);
        return true;
    }
}