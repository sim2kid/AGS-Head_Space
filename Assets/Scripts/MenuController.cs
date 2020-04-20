using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary> 
/// This class is used to control the main menu and all of it's buttons.
///</summary> 
public class MenuController : MonoBehaviour
{

    [Tooltip("The GameObject of the Title Menu")] [SerializeField] private GameObject titleMenu;
    [Tooltip("The GameObject of the Chapter Menu")] [SerializeField] private GameObject chapterMenu;
    [Tooltip("The GameObject of the Credit Menu")] [SerializeField] private GameObject creditMenu;


    void Start() 
    {
        EnterTitleMenu();
    }


    public void LoadScene(int sceneNumber) 
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void ExitGame() 
    {
        Application.Quit();
    }
    public void EnterTitleMenu() 
    {
        Cursor.lockState = CursorLockMode.None;
        titleMenu.SetActive(true);
        chapterMenu.SetActive(false);
        creditMenu.SetActive(false);
    }
    public void EnterCreditMenu() 
    {
        titleMenu.SetActive(false);
        chapterMenu.SetActive(false);
        creditMenu.SetActive(true);
    }
    public void EnterChapterMenu() 
    {
        titleMenu.SetActive(false);
        chapterMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
}
