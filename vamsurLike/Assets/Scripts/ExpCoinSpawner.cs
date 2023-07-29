using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExpCoinSpawner : MonoBehaviour
{
    public Transform expCoinGroup;
    public GameObject expCoin;

    public float timeBetCoinSpawn = 0.1f;
    private float lastTimeCoinSpawn;

    private Transform playerTransform;
    public float spawnRange = 30f;

    private void Awake()
    {
        if(GameManager.instance != null)
        {
            playerTransform = GameManager.instance.player.transform;
        }

        lastTimeCoinSpawn = 0f;
    }

    private void Update()
    {
        if(timeBetCoinSpawn + lastTimeCoinSpawn <= Time.time)
        {
            lastTimeCoinSpawn = Time.time;
            if(GameManager.instance != null && !GameManager.instance.isGameover)
            {
                SpawnExpCoin();
            }
        }
    }
    private void SpawnExpCoin()
    {
        Vector3 spawnPosition;
        if (!RandomPoint(playerTransform.position, spawnRange, out spawnPosition)) return;

        spawnPosition.y += 0.5f;

        GameObject newExpCoin;
        if (transform.childCount == 0)
        {
            newExpCoin = Instantiate(expCoin);
            newExpCoin.GetComponent<ExpCoin>().onUse += () => newExpCoin.transform.SetParent(transform);
        }
        else
        {
            newExpCoin = transform.GetChild(0).gameObject;
            newExpCoin.SetActive(true);
        }


        newExpCoin.transform.SetParent(expCoinGroup);
        newExpCoin.transform.position = spawnPosition;
        newExpCoin.transform.rotation = Quaternion.identity;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
