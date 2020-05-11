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
    [Tooltip("GameObject of the Heads Up Display")] [SerializeField] private GameObject hud;
    [Tooltip("GameObject of the dialogue stuff")] [SerializeField] private GameObject dialogue;


    private TextMeshProUGUI interactiveText;
    private PlayerController pc;
    private bool internalPause;
    private bool internalEscape;
    private bool internalDialogue;


    void Start()
    {
        interactiveText = interactiveTextOBJ.GetComponent<TextMeshProUGUI>();
        setInteractiveText("");
        GameObject player = GameObject.Find("Player");
        escMenu.SetActive(false);
        dialogue.SetActive(false);
        hud.SetActive(true);
        pc = player.GetComponent<PlayerController>();
        internalEscape = pc.IsEscaped();
        internalPause = pc.IsGamePaused();
        internalDialogue = false;
    }
    private void Update()
    {
        if (pc.IsEscaped() != internalEscape) {
            internalEscape = pc.IsEscaped();
            if (pc.IsEscaped() == true)
            {
                openMenu();
            }
            else {
                escMenu.SetActive(false);
            }
        }
        if (pc.IsGamePaused() != internalPause)
        {
            internalPause = pc.IsGamePaused();
            if (pc.IsGamePaused() == true)
            {
                ToggleHUD(false);
            }
            else
            {
                ToggleHUD(true);
            }
        }
    }
    public void OpenDialogue(string dialogueToOpen) 
    {
        dialogue.SetActive(true);
        dialogue.GetComponent<ConversationLoader>().LoadConversation(dialogueToOpen);
        pc.setPause(true);
    }
    public void CloseDialogue() {
        dialogue.SetActive(false);
        pc.setPause(false);
    }
    public void ToggleHUD(bool onOff) {
        hud.SetActive(onOff);
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
        pc.SetEscaped(false);
    }
    public void backToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
