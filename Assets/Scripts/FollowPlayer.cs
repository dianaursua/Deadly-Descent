using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Reference to the player's transform
    public Transform player;

    // Offset from the player (optional)
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Set the position of the object to the player's position + offset
            transform.position = player.position + offset;
        }
    }
}
