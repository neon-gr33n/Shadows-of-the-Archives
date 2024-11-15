using System.Collections;
using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    public EnemyAI[] enemies;
    public float noiseDelay = 1f; // Time between making noise

    private void Start()
    {
        enemies = FindObjectsOfType<EnemyAI>();
    }

    public void MakeNoise(Vector3 noisePosition)
    {
        // Notify all enemies to react to noise
        foreach (var enemy in enemies)
        {
            enemy.MakeNoise(noisePosition);
        }
    }

    // Simulating the noise made when interacting with an NPC
    public void TalkToNPC(Vector3 npcPosition)
    {
        MakeNoise(npcPosition);
        // Disable tracking by the enemy for a short time
        foreach (var enemy in enemies)
        {
            enemy.SetAvoidPlayer(true);
        }

        // Timer to reset enemy AI behavior
        StartCoroutine(ResetAvoidance());
    }

    private IEnumerator ResetAvoidance(float avoidTime = 2f)
    {
        yield return new WaitForSeconds(avoidTime); // Wait for avoidance period
        foreach (var enemy in enemies)
        {
            enemy.SetAvoidPlayer(false);
        }
    }
}
