using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum NightPhase { Night1, Night2, Night3, Night4 }
    public NightPhase currentNight = NightPhase.Night1;

    public Transform[] spawnPoints;
    public float wanderSpeed = 2f;
    public float followSpeed = 3f;
    public float spawnDelay = 5f; // Time between spawns
    private float noiseDetectionRange = 10f;
    private bool isPlayerNearby = false;
    private Transform player;
    public bool isAvoidingPlayer = false;
    private float avoidTime = 2f; // Time to avoid the player after talking to NPCs

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnEnemyRoutine());
    }

    private void Update()
    {
        if (isAvoidingPlayer)
        {
            avoidTime -= Time.deltaTime;
            if (avoidTime <= 0)
            {
                isAvoidingPlayer = false;
            }
            return;  // Don't do anything else while avoiding the player
        }

        // Handle different night behaviors
        switch (currentNight)
        {
            case NightPhase.Night1:
                Wander();
                break;
            case NightPhase.Night2:
                TrackNoise();
                break;
            case NightPhase.Night3:
                TrackNoise();
                break;
            case NightPhase.Night4:
                TrackPlayer();
                break;
        }
    }

    private void Wander()
    {
        // Randomly wander in a room, looking for random positions
        if (!isPlayerNearby)
        {
            Vector3 randomPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            transform.position = Vector3.MoveTowards(transform.position, randomPosition, wanderSpeed * Time.deltaTime);
        }
    }

    private void TrackPlayer()
    {
        // Track the player constantly (Night 4 behavior)
        if (Vector3.Distance(transform.position, player.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }

    private void TrackNoise()
    {
        // Track player only when noise is made
        if (isPlayerNearby && Vector3.Distance(transform.position, player.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            SpawnEnemy();

            // Adjust spawn rate based on night
            switch (currentNight)
            {
                case NightPhase.Night1:
                    spawnDelay = Random.Range(5f, 10f); // Infrequent spawn
                    break;
                case NightPhase.Night2:
                    spawnDelay = Random.Range(3f, 5f); // More frequent spawn
                    break;
                case NightPhase.Night3:
                    spawnDelay = Random.Range(2f, 4f); // Even more frequent spawn
                    break;
                case NightPhase.Night4:
                    spawnDelay = Random.Range(1f, 2f); // Constant spawn rate
                    break;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        // Spawn an enemy at a random spawn point
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        Instantiate(gameObject, spawnPoint.position, Quaternion.identity);
    }

    public void MakeNoise(Vector3 noisePosition)
    {
        // Notify enemy to check noise position
        if (currentNight == NightPhase.Night2 || currentNight == NightPhase.Night3)
        {
            // If noise is in range, enemy reacts
            float distance = Vector3.Distance(noisePosition, transform.position);
            if (distance <= noiseDetectionRange)
            {
                isPlayerNearby = true;
            }
        }
    }

    public void StopTrackingNoise()
    {
        // Stop tracking after a period
        isPlayerNearby = false;
    }

    public void SetAvoidPlayer(bool avoid)
    {
        // Enable or disable avoidance during NPC interaction
        isAvoidingPlayer = avoid;
    }
}
