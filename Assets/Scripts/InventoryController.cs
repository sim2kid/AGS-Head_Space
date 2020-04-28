using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [Tooltip("The GameObject of the Inventory")] [SerializeField] private GameObject invMenu;

    private PlayerController pc;
    private bool internalInv;


    void Start()
    {
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
    }
    public void BackToGame()
    {
        invMenu.SetActive(false);
        pc.SetInventory(false);
    }
}
