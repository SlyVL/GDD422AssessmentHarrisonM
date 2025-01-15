using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 6f; // Speed at which the enemy moves

    void Update()
    {
        // Checking if there is a player allocated to the enemy
        if (player != null)
        {
            // Calculates the direction of the play
            Vector3 direction = player.position - transform.position;

            // Stops enemy from moving faster when going diagonal
            direction.Normalize();

            // Move the Enemy to the players position
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
