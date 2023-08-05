using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDetector : MonoBehaviour
{
    private GameObject target;
    private InfiniteGenerator infiniteGenerator;
    private void Start()
    {
        if (GameManager.instance != null)
        {
            target = GameManager.instance.player;
        }
        print(target.name);

        infiniteGenerator = transform.parent.GetComponent<InfiniteGenerator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target && !GameManager.instance.isGameover)
        {
            // 상하좌우 채워주기
            infiniteGenerator.FillNearLevels();
        }
    }
}