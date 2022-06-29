using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float floorSize;
    [SerializeField] private int routePointsCount;

    private List<Vector3> navMeshRoutePoints;
    private ObjectPool<GameObject> enemyPool;

    private void Start()
    {
        enemyPool = new ObjectPool<GameObject>(createFunc: () => new GameObject("Enemy"), actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), defaultCapacity: 10, maxSize: 50);

        navMeshRoutePoints = GetRandomNavMeshPoints(routePointsCount);

        enemy.gameObject.SetActive(true);
        
        enemy.SetEnemyRoute(navMeshRoutePoints);
    }

    private List<Vector3> GetRandomNavMeshPoints(int pointsNum)
    {
        var points = new List<Vector3>();
        for(var i =0; i < pointsNum; i++)
        {
            points.Add(GetRandomNavMeshPoint());
        }
        return points;
    }

    private Vector3 GetRandomNavMeshPoint()
    {
        var maxDistance = floorSize / 2;
        var randomPos = Random.insideUnitSphere * maxDistance;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
            return hit.position;
        else 
            return Vector3.zero;
    }
}
