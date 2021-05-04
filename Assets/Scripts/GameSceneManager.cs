using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Text playerName, livesText, clockText, scoreText;
    public int lives, score;
    public float clock;
    
    // Start is called before the first frame update
    void Start()
    {
        
        lives = PlayerPrefs.GetInt("Lives", 1);
        clock = PlayerPrefs.GetInt("Time", 60);
        score = 0;
        PlayerPrefs.SetInt("Score", score);
        livesText.text = lives.ToString();
        playerName.text = "Currently playing: " + PlayerPrefs.GetString("Name", "Anonymous");
        clockText.text = "(" + ((int)clock).ToString() + " seconds left)";
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        clock -= 1f * Time.deltaTime;
        clockText.text = "(" + ((int)clock).ToString() + " seconds left)";
        if (clock ==0f)
        {
            toEndScene();
        }
    }
    public void livesUp()
    {
        lives += 1;
        livesText.text = lives.ToString();
    }
    public void livesDown()
    {
        if (lives > 0)
        {
            lives -= 1;
            livesText.text = lives.ToString();
        }
    }
    public void scoreUp()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
    public void scoreDown()
    {
        if (score > 0)
        {
            score -= 1;
            scoreText.text = score.ToString();
        }
    }

    public void toEndScene()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
