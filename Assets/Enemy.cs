using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int clicksToKill;
    [SerializeField,Range(0,10)] private int routeLength;
    [SerializeField] private float destinationGap;

    private Vector3[] route;
    private Vector3 currentDestination;
    private NavMeshAgent agent;

    public int ClicksToKill { get { return clicksToKill; }}
    
    private void Start()
    {
        Constructor();
        StartCoroutine(Move());
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

    public void SetRoutePositions(Vector3[] routePositions)
    {
        route = new Vector3[routeLength];
        route = routePositions;
    }

    public virtual IEnumerator Move()
    {
        foreach (var point in route)
        {
            if (point != null)
            {
                agent.SetDestination(point);
                currentDestination = point;
                yield return new WaitWhile(CheckDestinationReached);
            }
        }
    }

    private bool CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, currentDestination);
        return distanceToTarget < destinationGap ;
    }
}
