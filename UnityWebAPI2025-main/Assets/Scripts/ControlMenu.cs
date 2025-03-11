using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject startMenu;
    public GameObject findPlayerMenu;
    public GameObject addPlayerMenu;
    public GameObject showAllMenu;
    public GameObject deletePlayerMenu;
    public GameObject updatePlayerMenu;
    public GameObject showTopTenMenu;
   
    public void OpenFindPlayer()
    {
        findPlayerMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenAddPlayer()
    {
        addPlayerMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenShowAll()
    {
        showAllMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenShowTopTen()
    {
        showTopTenMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenDeletePlayer()
    {
        deletePlayerMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenUpdatePlayer()
    {
        updatePlayerMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
        findPlayerMenu.SetActive(false);
        addPlayerMenu.SetActive(false);
        showAllMenu.SetActive(false);
        deletePlayerMenu.SetActive(false);
        updatePlayerMenu.SetActive(false);
        showTopTenMenu.SetActive(false);
    }

    public void BackToStart()
    {
        startMenu.SetActive(true);
        mainMenu.SetActive(false);
        findPlayerMenu.SetActive(false);
        addPlayerMenu.SetActive(false);
        showAllMenu.SetActive(false);
        deletePlayerMenu.SetActive(false);
        updatePlayerMenu.SetActive(false);
        showTopTenMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
