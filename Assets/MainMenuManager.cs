using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controls;
    public GameObject credits;


    private void Start()
    {
        mainMenu.SetActive(true);
        controls.SetActive(false);
        credits.SetActive(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowControls()
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
        credits.SetActive(false);
    }

    public void ShowMenu()
    {
        mainMenu.SetActive(true);
        controls.SetActive(false);
        credits.SetActive(false);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
