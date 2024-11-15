using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public EnemyAI enemyAI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If the enemy is not avoiding, they can enter the room
            if (!enemyAI.isAvoidingPlayer)
            {
                enemyAI.SetAvoidPlayer(true);  // Set AI to avoid the player while talking to NPC
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAI.SetAvoidPlayer(false);  // Allow AI to follow again after leaving the room
        }
    }
}
