using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

///<summary> 
/// This class is used to control the escape menu and all of it's buttons.
///</summary> 

public class InGameMenuController : MonoBehaviour
{
    [Tooltip("The GameObject of the Esc Menu")] [SerializeField] private GameObject escMenu;
    [Tooltip("GameObject of TextMeshPro for Interactive Objects")] [SerializeField] private GameObject interactiveTextOBJ;


    private TextMeshProUGUI interactiveText;
    private PlayerController pc;
    private bool internalPause;


    void Start()
    {
        interactiveText = interactiveTextOBJ.GetComponent<TextMeshProUGUI>();
        setInteractiveText("");
        GameObject player = GameObject.Find("Player");
        escMenu.SetActive(false);
        pc = player.GetComponent<PlayerController>();
        internalPause = pc.IsGamePaused();
    }
    private void Update()
    {
        if (pc.IsGamePaused() != internalPause) {
            internalPause = pc.IsGamePaused();
            if (pc.IsGamePaused() == true)
            {
                openMenu();
            }
            else {
                escMenu.SetActive(false);
            }
        }
    }


    public void setInteractiveText(string textInput) {
        interactiveText.text = textInput;
    }
    public void openMenu() {
        escMenu.SetActive(true);
    }
    public void backToGame()
    {
        escMenu.SetActive(false);
        pc.SetPause(false);
    }
    public void backToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
