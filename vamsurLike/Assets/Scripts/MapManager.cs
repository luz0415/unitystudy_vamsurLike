using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    // �� ������ Ǯ�� �߰��� ������ ����ϴ� �༮
    public GameObject levelPrefab;
    public GameObject[] activeLevels;

    private void Awake()
    {
        instance = this;
    }

    public void InstantiateInSpawningPool()
    {
        GameObject childLevel = Instantiate(levelPrefab, transform.position, transform.rotation);
        childLevel.transform.parent = transform;
        childLevel.SetActive(false);
    }
}
