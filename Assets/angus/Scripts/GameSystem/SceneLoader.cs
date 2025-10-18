using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string menuScene;
    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Loader 常駐
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

        // SpawnPlayerAtSpawnPoint(level);

        // LoadUI();
    }

    // private void SpawnPlayerAtSpawnPoint(LevelData level)
    // {
    //     var spawner = FindFirstObjectByType<PlayerSpawner>();
    //     Vector3 pos = level.defaultSpawnPos;
    //     Quaternion rot = Quaternion.Euler(level.defaultSpawnEuler);

    //     if (spawner != null)
    //     {
    //         pos = spawner.SpawnPosition;
    //         rot = Quaternion.Euler(spawner.SpawnEuler);
    //     }

    //     var player = Instantiate(playerPrefab, pos, rot);
    // }

    // private void LoadUI()
    // {
    //     if (!SceneManager.GetSceneByName("UI").isLoaded)
    //     {
    //         SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    //     }
    // }
}
