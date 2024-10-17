using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Include the namespace for TextMeshPro

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 60f; // Set the time limit in seconds
    private float _timeRemaining;

    [SerializeField] private TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        _timeRemaining = _timeLimit; // Initialize the time remaining
        UpdateTimerText(); // Update the timer text at the start
    }

    private void Update()
    {
        // Decrease the time remaining
        _timeRemaining -= Time.deltaTime;

        // Check if time has run out
        if (_timeRemaining <= 0)
        {
            ResetScene(); // Call the method to reset the scene
        }

        UpdateTimerText(); // Update the timer text each frame
    }

    private void UpdateTimerText()
    {
        // Update the timer text to display the remaining time
        timerText.text = Mathf.CeilToInt(_timeRemaining).ToString(); // Display the time as an integer
    }

    private void ResetScene()
    {
        // Reset the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Optional: Method to manually reset the timer
    public void ResetTimer()
    {
        _timeRemaining = _timeLimit; // Reset the timer to the original time limit
        UpdateTimerText(); // Update the display when the timer resets
    }
}
