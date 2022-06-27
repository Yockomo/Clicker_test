using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void Start()
    { 
        enemy.gameObject.SetActive(true);
        var enemyHight = enemy.transform.position.y;
        enemy.SetRoutePositions(new Vector3[] { new Vector3(1,enemyHight,1),
            new Vector3(2,enemyHight,2), new Vector3(10,enemyHight,10)});
    }
}
