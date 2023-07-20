using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGenerator : MonoBehaviour
{
    private const int up = 0; private const int down = 1;
    private const int left = 2; private const int right = 3;

    private GameObject levelSpawningPool;
    public InfiniteGenerator[] nearLevels; // 惑 窍 谅 快

    public float generateSpace = 27f;

    private void Start()
    {
        levelSpawningPool = MapManager.instance.gameObject;
    }

    private void OnEnable()
    {
        nearLevels = new InfiniteGenerator[] { null, null, null, null };
    }
    private void OnDisable()
    {
        if (nearLevels[up] != null)
        {
            nearLevels[up].nearLevels[down] = null;
            nearLevels[up] = null;
        }
        if (nearLevels[down] != null)
        {
            nearLevels[down].nearLevels[up] = null;
            nearLevels[down] = null;
        }
        if (nearLevels[left] != null)
        {
            nearLevels[left].nearLevels[right] = null;
            nearLevels[left] = null;
        }
        if (nearLevels[right] != null)
        {
            nearLevels[right].nearLevels[left] = null;
            nearLevels[right] = null;
        }
    }

    public void FillNearLevels()
    {
        Vector3 newPosition = Vector3.zero;

        if (nearLevels[up] == null)
        {
            newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + generateSpace);
            
            // 甘 积己
            InfiniteGenerator upLevel = GenerateLevel(newPosition, Quaternion.identity);
            nearLevels[up] = upLevel;
            upLevel.nearLevels[down] = this;
        }
        if (nearLevels[down] == null)
        {
            newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - generateSpace);

            // 甘 积己
            InfiniteGenerator downLevel = GenerateLevel(newPosition, Quaternion.identity);
            nearLevels[down] = downLevel;
            downLevel.nearLevels[up] = this;
        }
        if (nearLevels[left] == null)
        {
            newPosition = new Vector3(transform.position.x - generateSpace, transform.position.y, transform.position.z);

            // 甘 积己
            InfiniteGenerator leftLevel = GenerateLevel(newPosition, Quaternion.identity);
            nearLevels[left] = leftLevel;
            leftLevel.nearLevels[right] = this;
        }
        if (nearLevels[right] == null)
        {
            newPosition = new Vector3(transform.position.x + generateSpace, transform.position.y, transform.position.z);

            // 甘 积己
            InfiniteGenerator rightLevel = GenerateLevel(newPosition, Quaternion.identity);
            nearLevels[right] = rightLevel;
            rightLevel.nearLevels[left] = this;
        }
    }

    private InfiniteGenerator GenerateLevel(Vector3 generatePosition, Quaternion generateRotation)
    {
        InfiniteGenerator newLevel;
        if(levelSpawningPool.transform.childCount == 0)
        {
            MapManager.instance.InstantiateInSpawningPool();
        }

        newLevel = levelSpawningPool.transform.GetChild(0).GetComponent<InfiniteGenerator>();
        newLevel.transform.SetParent(transform.parent);

        newLevel.transform.position = generatePosition;
        newLevel.transform.rotation = generateRotation;
        newLevel.gameObject.SetActive(true);

        MapManager.instance.activeLevels.Add(newLevel.gameObject);

        return newLevel;
    }
}
