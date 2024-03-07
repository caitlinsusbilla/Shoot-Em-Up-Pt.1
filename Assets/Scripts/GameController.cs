using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    
    private int score = 0;
    private int highScore = 0;

    private Invaders invader;
    

    void Start(){
        scoreText.text = score.ToString("0000");
        invader = FindObjectOfType<Invaders>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.R)){
            ResetScore();
            invader.ResetGame();
        }
    }

    
    // Method to update score
    public void UpdateScore(int points)
    {  
        score += points;
        scoreText.text = score.ToString("D4");
        UpdateHighScore();
    }
    
    
    // Method to update session high score
    void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString("D4");
        }
    }
    
    // Method to reset score and game
    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}