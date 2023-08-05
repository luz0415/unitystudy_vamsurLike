using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    // 맵 스포닝 풀에 추가와 삭제를 담당하는 녀석
    public GameObject levelPrefab;
    public List<GameObject> activeLevels;

    public Transform levelGroup;

    public float timeBetCleanLevels = 2.0f;

    public NavMeshBaker navMeshBaker;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(FirstBake());
    }

    IEnumerator FirstBake()
    {
        yield return new WaitForSeconds(1f);
        navMeshBaker.BakeNavMesh();
    }

    private void Update()
    {
        //if(lastClearTime + timeBetCleanLevels <= Time.time)
        //{
        //    lastClearTime = Time.time;
            if(levelGroup.childCount > 5)
            {
                CleanLevels();
            }
        //}
    }

    public void InstantiateInSpawningPool()
    {
        GameObject childLevel = Instantiate(levelPrefab, transform.position, transform.rotation);
        childLevel.transform.parent = transform;
        childLevel.SetActive(false);
    }

    public void CleanLevels()
    {
        if (levelGroup.childCount <= 5)
        {
            return;
        }

        InfiniteGenerator nearestLevel = null;
        float nearestDistance = 30f;
        for (int i = 0; i < activeLevels.Count; i++)
        {
            Vector3 playerPosition = GameManager.instance.player.transform.position;
            float distance = Vector3.Distance(playerPosition, activeLevels[i].transform.position);

            if (nearestLevel == null)
            {
                nearestLevel = activeLevels[i].GetComponent<InfiniteGenerator>();
                nearestDistance = distance;
            }
            else
            {
                if(distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestLevel = activeLevels[i].GetComponent<InfiniteGenerator>();
                }
            }
        }
        print(nearestLevel.gameObject.name);
        List<GameObject> remainLevels = new List<GameObject> { nearestLevel.gameObject };

        for (int i = 0; i < 4; i++)
        {
            if(nearestLevel.nearLevels[i] != null)
            {
                remainLevels.Add(nearestLevel.nearLevels[i].gameObject);
                nearestLevel.nearLevels[i].nearLevels[i] = null;
            }
        }

        List<GameObject> removeLevels = activeLevels.Except(remainLevels).ToList();

        for (int i = 0; i < removeLevels.Count; i++)
        {
            removeLevels[i].transform.position = Vector3.zero;
            removeLevels[i].transform.parent = transform;
            removeLevels[i].SetActive(false);
        }

        activeLevels.Clear();
        activeLevels = remainLevels.ToList();

        navMeshBaker.BakeNavMesh();
    }
}
