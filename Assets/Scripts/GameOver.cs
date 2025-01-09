using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Variables")]
    int enemies = 3; // Number of enemies

    [Header("Game Objects")]
    [SerializeField] GameObject parkingSpace; // Reference to the parking space GameObject

    [Header("Canvas")]
    [SerializeField] Canvas gameOverCanvas; // Reference to the game over canvas
    [SerializeField] Canvas winCanvas;      // Reference to the win canvas
    [SerializeField] Canvas defaultCanvas;  // Reference to the default canvas

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI gameOverScoreText; // Text for the game over score display
    [SerializeField] TextMeshProUGUI winScoreText;      // Text for the win score display

    [Header("Scripts")]
    [SerializeField] EnemyMover enemyMoverOne;   // Reference to the first enemy's mover script
    [SerializeField] EnemyMover enemyMoverTwo;   // Reference to the second enemy's mover script
    [SerializeField] EnemyMover enemyMoverThree; // Reference to the third enemy's mover script
    [SerializeField] PlayerMovement playerMovement; // Reference to the player's movement script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parkingSpace.SetActive(false); // Deactivate the parking space at the start
        gameOverCanvas.enabled = false; // Disable the game over canvas at the start
        winCanvas.enabled = false;      // Disable the win canvas at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies <= 0) // Check if all enemies are defeated
        {
            ParkingProcess(); // Call the parking process function
        }
    }

    private void ParkingProcess()
    {
        parkingSpace.SetActive(true); // Activate the parking space
    }

    public void UpdateScore(int score) // Function to update the score display
    {
        winScoreText.text = "Score:-" + score.ToString();      // Update the win score text
        gameOverScoreText.text = "Score:-" + score.ToString(); // Update the game over score text
    }

    public void GameOverWindow() // Function to display the game over window
    {
        defaultCanvas.enabled = false; // Disable the default canvas
        gameOverCanvas.enabled = true; // Enable the game over canvas
        Time.timeScale = 0f;          // Pause the game time

        if (enemyMoverOne != null) // Check if the enemy mover script exists before disabling it
        {
            enemyMoverOne.enabled = false; // Disable the first enemy's movement
        }
        if (enemyMoverTwo != null) // Check if the enemy mover script exists before disabling it
        {
            enemyMoverTwo.enabled = false; // Disable the second enemy's movement
        }
        if (enemyMoverThree) // Check if the enemy mover script exists before disabling it
        {
            enemyMoverThree.enabled = false; // Disable the third enemy's movement
        }
    }

    public void ReLoadScene() // Function to reload the current scene
    {
        Time.timeScale = 1f;          // Resume game time
        SceneManager.LoadScene(0);    // Reload the scene (index 0)
        defaultCanvas.enabled = true; // Enable the default canvas
        gameOverCanvas.enabled = false; // Disable the game over canvas
    }

    public void DecreaseEnemy() // Function to decrement the enemy count
    {
        enemies--; // Decrement the enemy count
    }

    public void WinWindowProcess() // Function to display the win window
    {
        Time.timeScale = 0f;
        playerMovement.enabled = false; // Disable the player's movement
        winCanvas.enabled = true;      // Enable the win canvas
        defaultCanvas.enabled = false; // Disable the default canvas
    }
}
