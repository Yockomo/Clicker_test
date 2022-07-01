using System.Collections;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private float difficultyTimer;
    [SerializeField] private float speedUp;
    [SerializeField] private float timeToSpawnUp;

    private bool isCooldown;

    void Update()
    {
        if (!isCooldown)
            StartCoroutine(DifficultyUp());
    }

    private IEnumerator DifficultyUp()
    {
        isCooldown = true;
        enemyPool.DifficultyUp(speedUp, timeToSpawnUp);
        yield return new WaitForSeconds(difficultyTimer);
        isCooldown = false;
    }
}
