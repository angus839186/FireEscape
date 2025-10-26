using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string menuScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Loader 常駐
        menuScene = SceneManager.GetActiveScene().name;
    }

    public void LoadLevel(LevelData level)
    {
        StartCoroutine(LoadLevelRoutine(level));
    }

    private IEnumerator LoadLevelRoutine(LevelData level)
    {
        var op = SceneManager.LoadSceneAsync(level.sceneName, LoadSceneMode.Additive);
        while (!op.isDone) yield return null;


        var newScene = SceneManager.GetSceneByName(level.sceneName);
        SceneManager.SetActiveScene(newScene);


        var menu = SceneManager.GetSceneByName(menuScene);
        if (menu.isLoaded)
        {
            var unloadOp = SceneManager.UnloadSceneAsync(menu);
            while (!unloadOp.isDone) yield return null;
        }
    }
    public void RestartScene()
    {
        Time.timeScale = 1f;

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void BackToMenu()
    {
        StartCoroutine(BackToMenuRoutine());
    }

    IEnumerator BackToMenuRoutine()
    {
        // 1) 先記住當下活躍的關卡（這個就是要卸的）
        var sceneToUnload = SceneManager.GetActiveScene();

        // 2) 載入選單
        var loadMenuOp = SceneManager.LoadSceneAsync(menuScene, LoadSceneMode.Additive);
        while (!loadMenuOp.isDone) yield return null;

        // 3) 切 Active 到選單（不然不能卸載原本 active 場景）
        var menu = SceneManager.GetSceneByName(menuScene);
        if (menu.IsValid() && menu.isLoaded)
            SceneManager.SetActiveScene(menu);

        // 4) 卸載關卡 + UI（各自判斷是否存在）
        AsyncOperation unloadLevelOp = null;
        if (sceneToUnload.IsValid() && sceneToUnload.isLoaded && sceneToUnload != menu)
            unloadLevelOp = SceneManager.UnloadSceneAsync(sceneToUnload);

        AsyncOperation unloadUIOp = null;
        var ui = SceneManager.GetSceneByName("UI");
        if (ui.IsValid() && ui.isLoaded)
            unloadUIOp = SceneManager.UnloadSceneAsync(ui);

        // 5) 正確等待（任一個還沒完成就持續等，並處理 null）
        while ((unloadLevelOp != null && !unloadLevelOp.isDone) ||
               (unloadUIOp != null && !unloadUIOp.isDone))
        {
            yield return null;
        }
    }


}
