using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 SpawnPosition => transform.position;
    public Vector3 SpawnEuler => transform.eulerAngles;

    public GameObject PlayerPrefab;

    public GameObject GameInputPrefab;

    void Awake()
    {
        if (FindFirstObjectByType<GameManager>() != null) return;
        SpawnPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        var forward = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
        Gizmos.DrawLine(transform.position, transform.position + forward * 1.0f);
    }
    public void SpawnPlayer()
    {
        Instantiate(GameInputPrefab);
        Quaternion rot = Quaternion.Euler(SpawnEuler);
        Instantiate(PlayerPrefab, SpawnPosition, rot);
    }
}
