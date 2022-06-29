using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float destinationGap = 1;

    private NavMeshAgent agent;
    private List<Vector3> route;
    private Vector3 currentDestination;
    private bool isMoving;
    private bool doReachDestination;
    
    private void Start()
    {
        Constructor();
    }

    protected virtual void Constructor()
    {
        try
        {
            agent = GetComponent<NavMeshAgent>();
        }
        catch
        {
            throw new NullReferenceException("There is no NavMeshAgent component on Enemy");
        }
    }

    private void FixedUpdate()
    {
        OnFixUpdate();
    }

    protected virtual void OnFixUpdate()
    {
        if (isMoving)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentDestination);

            doReachDestination = distanceToTarget < destinationGap;
        }
        else
            StartCoroutine(Move());
    }

    private bool CheckDestinationReached()
    {
        return doReachDestination;
    }

    public virtual IEnumerator Move()
    {
        isMoving = true;
        foreach (var point in route)
        {
            agent.SetDestination(point);
            currentDestination = point;
            yield return new WaitUntil(CheckDestinationReached);
            doReachDestination = false;
        }
        isMoving = false;
    }

    public void SetRoutePositions(List<Vector3> routePositions)
    {
        route = new List<Vector3>();
        route = routePositions;
    }

    public bool SetEnemySpeed(float value)
    {
        if(agent != null)
        {
            agent.speed = value;
            return true;
        }
        return false;
    }
}
