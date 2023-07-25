using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface level;

    private void Start()
    {
        level = GetComponent<NavMeshSurface>();
        level.BuildNavMesh();
    }
    
    public void BakeNavMesh()
    {
        level.BuildNavMesh();
    }
}
