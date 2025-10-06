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
        // 1. 載入新場景
        var op = SceneManager.LoadSceneAsync(level.sceneName, LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("UI").isLoaded)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
        while (!op.isDone) yield return null;

        // 2. 設定新的 Active Scene
        var newScene = SceneManager.GetSceneByName(level.sceneName);
        SceneManager.SetActiveScene(newScene);

        // 3. 卸載舊場景
        var menu = SceneManager.GetSceneByName(menuScene);
        if (menu.isLoaded)
        {
            var unloadOp = SceneManager.UnloadSceneAsync(menu);
            while (!unloadOp.isDone) yield return null;
        }

        SpawnPlayerAtSpawnPoint(level);
    }

    private void SpawnPlayerAtSpawnPoint(LevelData level)
    {
        var spawner = FindFirstObjectByType<PlayerSpawner>();
        Vector3 pos = level.defaultSpawnPos;
        Quaternion rot = Quaternion.Euler(level.defaultSpawnEuler);

        if (spawner != null)
        {
            pos = spawner.SpawnPosition;
            rot = Quaternion.Euler(spawner.SpawnEuler);
        }

        var player = Instantiate(playerPrefab, pos, rot);
    }
}
