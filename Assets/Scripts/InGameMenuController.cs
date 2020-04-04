using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenuController : MonoBehaviour
{
    [Tooltip("The GameObject of the Esc Menu")] [SerializeField] private GameObject escMenu;
    [Tooltip("GameObject of TextMeshPro for Interactive Objects")] [SerializeField] private GameObject interactiveTextOBJ;
    private TextMeshProUGUI interactiveText;
    private PlayerController pc;
    private bool internalPause;

    // Start is called before the first frame update
    void Start()
    {
        interactiveText = interactiveTextOBJ.GetComponent<TextMeshProUGUI>();
        setInteractiveText("");
        GameObject player = GameObject.Find("Player");
        escMenu.SetActive(false);
        pc = player.GetComponent<PlayerController>();
        internalPause = pc.isGamePaused();
    }

    private void Update()
    {
        if (pc.isGamePaused() != internalPause) {
            internalPause = pc.isGamePaused();
            if (pc.isGamePaused() == true)
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
        pc.setPause(false);
    }

    public void backToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
