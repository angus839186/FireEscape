using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject levelSelectMenu;
    public GameObject helpPanel;

    void Start()
    {
        ShowGameMenu();
    }

    void HideAll()
    {
        gameMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        helpPanel.SetActive(false);
    }

    public void ShowGameMenu()
    {
        HideAll();
        gameMenu.SetActive(true);
    }

    public void ShowLevelSelect()
    {
        HideAll();
        levelSelectMenu.SetActive(true);
    }

    public void ShowHelp()
    {
        HideAll();
        helpPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
