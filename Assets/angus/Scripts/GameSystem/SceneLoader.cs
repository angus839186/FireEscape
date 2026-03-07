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
        DontDestroyOnLoad(gameObject);
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
        var sceneToUnload = SceneManager.GetActiveScene();

        var loadMenuOp = SceneManager.LoadSceneAsync(menuScene, LoadSceneMode.Additive);
        while (!loadMenuOp.isDone) yield return null;

        var menu = SceneManager.GetSceneByName(menuScene);
        if (menu.IsValid() && menu.isLoaded)
            SceneManager.SetActiveScene(menu);


        AsyncOperation unloadLevelOp = null;
        if (sceneToUnload.IsValid() && sceneToUnload.isLoaded && sceneToUnload != menu)
            unloadLevelOp = SceneManager.UnloadSceneAsync(sceneToUnload);

        AsyncOperation unloadUIOp = null;
        var ui = SceneManager.GetSceneByName("UI");
        if (ui.IsValid() && ui.isLoaded)
            unloadUIOp = SceneManager.UnloadSceneAsync(ui);


        while ((unloadLevelOp != null && !unloadLevelOp.isDone) ||
               (unloadUIOp != null && !unloadUIOp.isDone))
        {
            yield return null;
        }
    }


}
