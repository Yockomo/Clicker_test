using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth),typeof(EnemyMovement))]

public class Enemy : MonoBehaviour, IClickable
{
    private EnemyHealth hp;
    private EnemyMovement movement;

    public bool isClicked { get; set; }
    
    private void Start()
    {
        try
        {
            hp = GetComponent<EnemyHealth>();
            movement = GetComponent<EnemyMovement>();
        }
        catch
        {
            Debug.Log("There are no EnemyHealth/EnemyMovement components on Enemy");
        }
    }

    private void Update()
    {
        if (isClicked)
            OnClick();
    }

    public void OnClick()
    {
        hp.TakeDamage();
        isClicked = false;
    }

    public void SetEnemyHp(int hpValue)
    {
        hp.ChangeMaxHp(hpValue);
    }

    public void ResetEnemyHealth()
    {
        hp.ResetHealth();
    }

    public void SetEnemyRoute(List<Vector3> route)
    {
        if (movement == null)
            movement = GetComponent<EnemyMovement>();
        movement.SetRoutePositions(route);
    }

    public void SetEnemySpeed(float speedValue)
    {
        movement.SetEnemySpeed(speedValue);
    }
}
