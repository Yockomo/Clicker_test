using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPool pool;
    [SerializeField] private LeaderboardReader leaderboardReader;
    [SerializeField] private TimeFreezer freezer;

    private int currentScore;

    public UnityEvent UpdateUi;
    public UnityEvent OnEnd;
    
    private void Awake()
    {
        Time.timeScale = 1;
    }

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
        pool.Stop();
        OnEnd.Invoke();
    }

    public void Restart()
    {
        pool.Continue();
        SceneManager.LoadScene(1);
    }

    public void AddNewPlayerToLeaderBoard(string name)
    {
        var player = new Player();
        player.Name = name;
        player.Score = currentScore;
        leaderboardReader.AddPlayerToLeaderboard(player);
    }
}
