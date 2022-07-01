using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class LeaderboardReader : MonoBehaviour
{
    public TextAsset leaderboardJson;
    public PlayerLeaderboard leaderBoard = new PlayerLeaderboard();

    private string pathToJson;

    private void Awake()
    {
        pathToJson = Application.dataPath + "/Leaderboard/Leaderboard.json";
    }

    public void AddPlayerToLeaderboard(Player player)
    {
        var final = new StringBuilder();
        if (player != null)
        {
            TryAddPlayer(player);
            var toJs = JsonUtility.ToJson(leaderBoard);
            File.WriteAllText(pathToJson, toJs);
        }
    }

    private void TryAddPlayer(Player player)
    {
        for (int i = 0; i < leaderBoard.player.Length; i++)
        {
            if (leaderBoard.player[i].Score < player.Score)
            {
                leaderBoard.player[i] = player;
                return;
            }
        }
    }

    public void WriteAllLeaders(TextMeshProUGUI textField)
    {
        var fromJs = File.ReadAllText(pathToJson);
        var leaderBrd = JsonUtility.FromJson<PlayerLeaderboard>(fromJs);
        foreach (var el in leaderBrd.player)
            textField.text += $"{el.Name} - {el.Score} \n";
    }
}

[System.Serializable]
public class PlayerLeaderboard
{
    public Player[] player = new Player[5];
}

[System.Serializable]
public class Player
{
    public string Name;
    public int Score;
}