using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [Header("Pool parameters")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float floorSize;
    [SerializeField] private int routePointsCount;
    [SerializeField] private float timeBetweenEnemies;

    private List<Vector3> navMeshRoutePoints;
    private ObjectPool<GameObject> enemyPool;
    private bool isCooldown;
    private bool isStoped;
    private List<GameObject> enemiesInPool = new List<GameObject>();

    public UnityEvent OnEnemyCreate;
    public UnityEvent OnEnemyDie;

    private void Start()
    {        
        navMeshRoutePoints = GetRandomNavMeshPoints(routePointsCount);
        
        enemyPool = new ObjectPool<GameObject>(createFunc: () => ActionsOnCreate(), actionOnGet: (obj) => ActionsOnGet(obj),
            actionOnRelease: (obj) => ActionsOnRelease(obj), actionOnDestroy: (obj) => ActionsOnDestroy(obj), defaultCapacity: 10, maxSize: 50) ;
    }

    private void Update()
    {
        if(!isCooldown && !isStoped)
        {
           StartCoroutine(AddEnemyOnField());
        }
    }

    private IEnumerator AddEnemyOnField()
    {
        isCooldown = true;
        enemyPool.Get();
        ShuffleRoutePosition<Vector3>(navMeshRoutePoints);
        yield return new WaitForSecondsRealtime(timeBetweenEnemies);
        isCooldown = false;
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

    private void ShuffleRoutePosition<T>(List<T> list)
    {
        System.Random random = new System.Random();
        for(int i = list.Count; i > 1; i--)
        {
            i--;
            var randomIndex = random.Next(i + 1);
            var value = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = value;
        }
    }

    private GameObject ActionsOnCreate()
    {
        var obj = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        var currentEnemy = obj.GetComponent<Enemy>();
        var countOfRoutePoints = Random.Range(2, navMeshRoutePoints.Count);
        var randomPoints = navMeshRoutePoints.GetRange(0, countOfRoutePoints);
        currentEnemy.SetEnemyRoute(randomPoints);
        var lol = obj.GetComponent<EnemyHealth>();
            lol.OnDieEvent += enemyPool.Release; 
        return obj;
    }

    private void ActionsOnGet(GameObject enemy)
    { 
        enemy.SetActive(true);
        enemiesInPool.Add(enemy);
        enemy.transform.position = navMeshRoutePoints[Random.Range(0,navMeshRoutePoints.Count)];
        OnEnemyCreate.Invoke();
    }

    private void ActionsOnRelease(GameObject enemy)
    {
        enemiesInPool.Remove(enemy);
        enemy.SetActive(false);
        enemy.GetComponent<EnemyHealth>().ResetHealth();
        enemy.GetComponent<EnemyMovement>().RestMove();
        OnEnemyDie.Invoke();
    }

    private void ActionsOnDestroy(GameObject enemy)
    {
        enemy.GetComponent<EnemyHealth>().OnDieEvent -= enemyPool.Release;
    }

    public List<GameObject> GetActiveEnemies()
    {
        return enemiesInPool;
    }

    public void DifficultyUp(float speedUp, float timeToSpawn)
    {
        timeBetweenEnemies -= timeToSpawn;
        foreach(var enemy in enemiesInPool)
        {
            enemy.GetComponent<Enemy>().UpEnemySpeed(speedUp);
        }
    }

    public void Stop()
    {
        isStoped = true;
    }

    public void Continue()
    {
        isStoped = false;
    }
}
