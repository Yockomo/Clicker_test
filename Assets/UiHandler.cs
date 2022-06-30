using TMPro;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyKilled;

    private int killedEnemies;

    public void UpdateUi()
    {
        killedEnemies++;
        enemyKilled.text = killedEnemies.ToString();
    }
}
