using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score { get; private set; }

    public TMP_Text scoreText;     // TMP text component to display score
    public TMP_Text highScoreText; // TMP text component to display high score
    public TMP_Text timerText;     // TMP text component to display the timer

    private float elapsedTime;     // Time elapsed since the start of the game

    private void Awake()
    {
        // Singleton pattern to ensure only one ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Initialize the score and high score display
        UpdateScoreText();
        elapsedTime = 0f;  // Initialize timer
    }

    private void Update()
    {
        // Update the timer every frame
        UpdateTimer();
    }

    // Method to add score, e.g., when a balloon is popped
    public void AddScore(int points)
    {
        score += points;          // Add points to the current score
        UpdateScoreText();        // Update the score on the UI
        CheckAndUpdateHighScore(); // Check and update the high score if needed
    }

    // Resets the current score to 0
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();  // Reset score display
    }

    // Updates the score and high score display text
    private void UpdateScoreText()
    {
        // Update the score display text
        if (scoreText != null)
        {
            scoreText.text = "Score: " + (score / 10).ToString();  // Display the score divided by 10
        }

        // Get the current high score from PlayerPrefs and update the high score display
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + (highScore / 10).ToString();  // Display the high score divided by 10
        }
    }

    // Checks and updates the high score if the current score is higher
    public void CheckAndUpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // If the current score exceeds the saved high score, update it
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);  // Save the new high score in PlayerPrefs
            PlayerPrefs.Save();  // Ensure PlayerPrefs is saved immediately

            // Update the high score display
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + (score / 10).ToString();
            }
        }
    }

    // Updates the timer display
    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;  // Increment elapsed time by the time passed since last frame

        // Convert elapsed time to seconds and milliseconds
        int seconds = (int)(elapsedTime % 60);  // Get the seconds part
        int milliseconds = (int)((elapsedTime * 1000) % 1000);  // Get the milliseconds part

        // Display the timer in the format "seconds:milliseconds"
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:000}", seconds, milliseconds);
        }
    }
}
