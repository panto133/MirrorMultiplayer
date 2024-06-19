using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class NPCBehaviour : NetworkBehaviour
{
    [SerializeField] private float minDelayTime = 0f;
    [SerializeField] private float maxDelayTime = 2f;
    /// <summary>
    /// 0-100% min chance to repath while moving
    /// </summary>
    [SerializeField] private float minChanceToRepath = 10f;
    /// <summary>
    /// 0-100% max chance to repath while moving
    /// </summary>
    [SerializeField] private float maxChanceToRepath = 60f;
    /// <summary>
    /// 1-8s min time after it tries to repath
    /// </summary>
    [SerializeField] private float minTimeUntilRepath = 1f;
    /// <summary>
    /// 1-8s max time after it tries to repath
    /// </summary>
    [SerializeField] private float maxTimeUntilRepath = 8f;

    [SerializeField] private NavMeshAgent agent;

    private float delayTime;
    private float chanceToRepath;
    private float timeUntilRepath;

    private float tempTime = 0;

    private bool courutineActivated = false;

    private void Awake()
    {
        InitializeValues();
        StartMoving();
    }
    [Server]
    private void Update()
    {
        tempTime += Time.fixedDeltaTime;

        if(tempTime >= timeUntilRepath && !courutineActivated)
        {
            Repath();
            tempTime = 0;
        }

        if(!agent.hasPath)
        {
            StartMoving();
        }
    }
    [Server]
    private void InitializeValues()
    {
        delayTime = Random.Range(minDelayTime, maxDelayTime);
        chanceToRepath = Random.Range(minChanceToRepath, maxChanceToRepath);
        timeUntilRepath = Random.Range(minTimeUntilRepath, maxTimeUntilRepath);
    }
    [Server]
    private void StartMoving()
    {
        Vector3 destination = new Vector3(
            Random.Range(RandomSpawnPointCreationSystem.startPoint.x, RandomSpawnPointCreationSystem.endPoint.x), 1,
            Random.Range(RandomSpawnPointCreationSystem.startPoint.z, RandomSpawnPointCreationSystem.endPoint.z));
        StartCoroutine(Move(destination));
    }
    [Server]
    private void Repath()
    {
        Vector3 destination = new Vector3(
            Random.Range(RandomSpawnPointCreationSystem.startPoint.x, RandomSpawnPointCreationSystem.endPoint.x), 1,
            Random.Range(RandomSpawnPointCreationSystem.startPoint.z, RandomSpawnPointCreationSystem.endPoint.z));
        agent.SetDestination(destination);
    }
    
    private IEnumerator Move(Vector3 destination)
    {
        courutineActivated = true;
        agent.SetDestination(destination);
        agent.isStopped = true;

        yield return new WaitForSeconds(delayTime);

        agent.isStopped = false;
        courutineActivated = false;
    }
}
