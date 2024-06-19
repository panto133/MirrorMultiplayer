using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NPCManagerSystem : NetworkBehaviour
{
    [SerializeField] private GameObject NPCPrefab = null;

    [SerializeField] private int AmountOfNPC = 10;

    private void Start()
    {
        SpawnNPCs();
    }
    [Server]
    private void SpawnNPCs()
    {
        /*foreach(Transform point in PlayerSpawnSystem.spawnPoints)
        {
            StartCoroutine(SpawnNPC(point));
        }*/
        for (int i = 0; i < AmountOfNPC; i++) 
        {
            StartCoroutine(SpawnNPC(PlayerSpawnSystem.spawnPoints[PlayerSpawnSystem.nextIndex++]));
        }
    }

    private IEnumerator SpawnNPC(Transform point)
    {
        GameObject npc = Instantiate(NPCPrefab, point.position, point.rotation);
        NetworkServer.Spawn(npc);
        yield return new WaitForSeconds(0.5f);
    }
}
