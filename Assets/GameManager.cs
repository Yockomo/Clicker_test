using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPool pool;

    private int currentScore;

    public UnityEvent UpdateUi;

    public void CheckState()
    {
        if(pool.GetActiveEnemies().Count >= 10)
        {
            End();
        }
    }

    public void ScoreUp()
    {
        currentScore++;
        UpdateUi.Invoke();
    }

    private void End()
    {
        Time.timeScale = 0;
        Debug.Log("U lost");
    }
}
