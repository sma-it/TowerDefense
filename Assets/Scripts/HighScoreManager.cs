using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class HighScore
{
    public string Name { get; set; }
    public int Score { get; set; }
}


public class HighScoreManager : MonoBehaviour
{
    // singleton code
    private static HighScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static HighScoreManager Get { get => instance; }

    private void Start()
    {
        if (File.Exists("highscores.json"))
        {
            using(var reader = new StreamReader("highscores.json"))
            {
                var content = reader.ReadToEnd();
                HighScores = JsonConvert.DeserializeObject<List<HighScore>>(content);
            }
        }
    }

    // class code
    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }

    public List<HighScore> HighScores = new List<HighScore>();

    public void AddHighScore(int score)
    {
        int lowestHighscore = 0;

        foreach(var item in HighScores)
        {
            if (item.Score > score)
            {
                lowestHighscore = item.Score;
            }
        }

        if (score <= lowestHighscore)
        {
            return;
        }

        HighScores.Add(new HighScore()
        {
            Name = PlayerName,
            Score = score
        });

        HighScores.Sort((a, b) => b.Score.CompareTo(a.Score));

        if (HighScores.Count > 5)
        {
            HighScores.RemoveAt(5);
        }

        using(var writer = new StreamWriter("highscores.json"))
        {
            var json = JsonConvert.SerializeObject(HighScores, Formatting.Indented);
            writer.Write(json);
        }
    }
}
