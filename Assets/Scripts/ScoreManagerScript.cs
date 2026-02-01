using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using TMPro;
public class ScoreManagerScript : MonoBehaviour
{
    public int score = 10;
    private Stopwatch stopwatch;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public static ScoreManagerScript Instance;
    public int platformLevel = 0;
    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartTimer()
    {
        stopwatch = Stopwatch.StartNew();
    }

    public void Update() 
    {
        if (stopwatch != null) {
            timeText.text = "Time: " + stopwatch.Elapsed.ToString();
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void StopTimer()
    {
        stopwatch.Stop();
        scoreText.text = "Score: " + score.ToString();
        timeText.text = "Time: " + stopwatch.Elapsed.ToString();
    }

    public void addScore(int amount)
    {
        score += amount;
    }

    public void subtractScore(int amount)
    {
        score -= amount;
    }

    public void setPlatformLevel(int level)
    {
        platformLevel = level;
    }
}
