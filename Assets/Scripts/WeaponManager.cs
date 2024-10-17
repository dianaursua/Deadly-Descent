// WeaponManager.cs
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons; // Array of available weapons

    private void Awake()
    {
        // Create a singleton instance if needed (optional)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static WeaponManager Instance { get; private set; } // Singleton instance
}
