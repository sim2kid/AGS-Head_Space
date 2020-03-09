using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [Tooltip("The GameObject of the Title Menu")] [SerializeField] private GameObject titleMenu;
    [Tooltip("The GameObject of the Chapter Menu")] [SerializeField] private GameObject chapterMenu;
    [Tooltip("The GameObject of the Credit Menu")] [SerializeField] private GameObject creditMenu;
    [Tooltip("All the 5 Scene's Names in Chapter Order")] [SerializeField] private string[] gameScenes = new string[5];

    void Start() 
    {
        EnterTitleMenu();
    }

    public void LoadScene(int sceneNumber) 
    {
        sceneNumber -= 1; //To match the array
        SceneManager.LoadScene(gameScenes[sceneNumber]);

    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void EnterTitleMenu() 
    {
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
