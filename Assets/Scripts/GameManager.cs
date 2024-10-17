using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    public int points = 0; // Points counter

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
            DontDestroyOnLoad(gameObject); // Keep it when changing scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    // Call this method to add points
    public void AddPoints(int amount)
    {
        points += amount;
        Debug.Log("Points: " + points);
    }
}
