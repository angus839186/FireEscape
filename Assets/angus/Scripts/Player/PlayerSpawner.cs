using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 SpawnPosition => transform.position;
    public Vector3 SpawnEuler => transform.eulerAngles;

    public GameObject PlayerPrefab;

    public GameObject GameInputPrefab;

    void Awake()
    {
        PreviewScreen preview = FindFirstObjectByType<PreviewScreen>();
        preview.StopPreviewVideo += SpawnPlayerAndUI;
    }

    void OnDisable()
    {
        PreviewScreen preview = FindFirstObjectByType<PreviewScreen>();
        if (preview != null)
        {
            preview.StopPreviewVideo -= SpawnPlayerAndUI;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        var forward = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
        Gizmos.DrawLine(transform.position, transform.position + forward * 1.0f);
    }
    public void SpawnPlayerAndUI()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        if (!FindFirstObjectByType<GameInputManager>())
        {
            Instantiate(GameInputPrefab);
        }
        Quaternion rot = Quaternion.Euler(SpawnEuler);
        Instantiate(PlayerPrefab, SpawnPosition, rot);

        yield return new WaitForSeconds(0.2f);
        if (!SceneManager.GetSceneByName("UI").isLoaded)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
