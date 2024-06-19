using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RandomSpawnPointCreationSystem : NetworkBehaviour
{
    [SerializeField] public static Vector3 startPoint { get; private set; } = new Vector3(-19, 0, -19);
    [SerializeField] public static Vector3 endPoint { get; private set; } = new Vector3(19, 0, 19);
    [SerializeField] private int pointCount = 25;

    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private Transform parent;

    private void Awake()
    {
        GenerateRandomSpawnPoints();
    }
    [Server]
    private void GenerateRandomSpawnPoints()
    {
        for (int i = 0; i < pointCount; i++)
        {
            Vector3 point = new Vector3(
                Random.Range(startPoint.x, endPoint.x), 
                0,
                Random.Range(startPoint.z, endPoint.z));
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

            GameObject spawnPoint = Instantiate(spawnPointPrefab, point, rotation, parent) as GameObject;
            NetworkServer.Spawn(spawnPoint);
        }
    }
}
