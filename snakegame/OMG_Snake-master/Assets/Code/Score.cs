using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public GameOver gameover;

    public Text scoreText;
    public Text highscoreText;
    public Text timerText;
    private float Timer = 0.0f;

    int score = 0;
    int highscore = 0;

    //instace activate
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "Points: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();        
    }

    //Time to minutes and seconds
    private void Update()
    {
        Timer += Time.deltaTime;
        float minutes = Mathf.FloorToInt(Timer / 60);
        float seconds = Mathf.FloorToInt(Timer % 60);
        timerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //add point to score and highscore text if highscore less than score
    public void AddPoint()
    {
        score += 1;
        scoreText.text = "Points: " + score.ToString();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void GameOver()
    {
        gameover.Setup(score);
    }
}
