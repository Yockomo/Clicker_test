using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void Start()
    {
       var _pool = new ObjectPool<GameObject>(createFunc: () => new GameObject("PooledObject"), actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 10, maxSize: 10);


        enemy.gameObject.SetActive(true);
        
        var enemyHight = enemy.transform.position.y;

        enemy.SetRoutePositions(new Vector3[] { new Vector3(1,enemyHight,1),
            new Vector3(2,enemyHight,2), new Vector3(10,enemyHight,10)});
    }
}
