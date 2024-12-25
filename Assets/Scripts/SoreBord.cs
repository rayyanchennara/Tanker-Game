using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreBord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score;

    [SerializeField] GameOver gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Score:- " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore()
    {
        score += 25;
        UpdateScore();
    }

    private void UpdateScore()
    {
        gameOver.UpdateScore(score);
        scoreText.text = "Score:- " + score.ToString();
    }
}
