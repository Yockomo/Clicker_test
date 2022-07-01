using System.Collections;
using UnityEngine;

public class ExplosionBooster : Booster, IClickable
{
    public void Click()
    {
        StartCoroutine(Boost());
    }

    public override IEnumerator Boost()
    {
        storedPosition = transform.position;
        transform.position = new Vector3(storedPosition.x, -2, storedPosition.z);
        yield return null;
        DestroyEnemies();
        StartCoroutine(CoolDown());
    }

    private void DestroyEnemies()
    {
        var enemies = Physics.OverlapSphere(storedPosition, 10f);
        
        foreach(var enemy in enemies)
        {
            if(enemy != null && enemy.TryGetComponent<EnemyHealth>(out EnemyHealth health))
                health.Die();
        }
    }
}
