using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject levelSelectMenu;
    public GameObject helpPanel;
    public GameObject Background;

    void Start()
    {
        ShowGameMenu();
        if (GameManager.ShouldShowLevelSelect)
    {
        // 1. 顯示關卡選擇面板
        // 2. 隱藏主選單面板
        // 假設你的變數叫 levelSelectMenu 和 gameMenu
        levelSelectMenu.SetActive(true);
        gameMenu.SetActive(false);
        Background.SetActive(false);

        // 重置標記，避免下次正常進遊戲時又跳出選單
        GameManager.ShouldShowLevelSelect = false;
    }
    }

    void HideAll()
    {
        gameMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        helpPanel.SetActive(false);
        Background.SetActive(false);

    }

    public void ShowGameMenu()
    {
        HideAll();
        gameMenu.SetActive(true);
        Background.SetActive(true);

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
