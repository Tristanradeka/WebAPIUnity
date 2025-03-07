using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject findPlayerMenu;
    public GameObject addPlayerMenu;
    public GameObject showAllMenu;
    public GameObject deletePlayerMenu;
    public GameObject updatePlayerMenu;
   
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
        mainMenu.SetActive(true);
        findPlayerMenu.SetActive(false);
        addPlayerMenu.SetActive(false);
        showAllMenu.SetActive(false);
        deletePlayerMenu.SetActive(false);
        updatePlayerMenu.SetActive(false);
    }
}
