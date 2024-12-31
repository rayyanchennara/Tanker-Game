using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    int enmies =3;

    // GameObjects
    [SerializeField] GameObject parkingSpce;

    // Canvases
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas defualtCanvas;

    // Texts
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI winScoreText;


    // Scripts
    [SerializeField] EnemyMover enemyMoverOne;
    [SerializeField] EnemyMover enemyMoverTwo;
    [SerializeField] EnemyMover enemyMoverThree;
    [SerializeField] PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parkingSpce.SetActive(false);
        gameOverCanvas.enabled = false;
        winCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enmies <= 0)
        {
            ParkingProcess();
        }
    }

    private void ParkingProcess()
    {
        parkingSpce.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        winScoreText.text = "Score:-" + score.ToString();
        gameOverScoreText.text = "Score:-" + score.ToString();
    }

    public void GameOverWindow()
    {
        defualtCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0f;
        if(enemyMoverOne != null)
        {
            enemyMoverOne.enabled = false;
        }
        if(enemyMoverTwo != null)
        {
            enemyMoverTwo.enabled = false;
        }
        if(enemyMoverThree)
        {
            enemyMoverThree.enabled = false;
        }
    }

    public void ReLoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        // Time.timeScale = 1f;
        defualtCanvas.enabled = true;
        gameOverCanvas.enabled = false;
        // if(enemyMoverOne != null)
        // {
        //     enemyMoverOne.enabled = true;
        // }
        // if(enemyMoverTwo != null)
        // {
        //     enemyMoverTwo.enabled = true;
        // }
        // if(enemyMoverThree)
        // {
        //     enemyMoverThree.enabled = true;
        // }
    }

    public void DecreaseEnemy()
    {
        enmies--;
    }

    public void WinWindowProcess()
    {
        winCanvas.enabled = true;
        defualtCanvas.enabled = false;
        playerMovement.enabled = false;
        // Time.timeScale = 0f;
    }
}
