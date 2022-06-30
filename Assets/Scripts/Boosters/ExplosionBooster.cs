using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBooster : Booster, IClickable
{
    [SerializeField] private EnemyPool pool;

    public void Click()
    {
        StartCoroutine(Boost());
    }

    public override IEnumerator Boost()
    {
        storedPosition = transform.position;
        transform.position = new Vector3(storedPosition.x, -2, storedPosition.z);
        yield return null;
        DestroyEnemies(pool.GetActiveEnemies());
        StartCoroutine(CoolDown());
    }

    private void DestroyEnemies(List<GameObject> enemies)
    {
        var list = enemies;
        while(list.Count > 1)
        {
            list[0].GetComponent<EnemyHealth>().Die();
        }
    }
}
