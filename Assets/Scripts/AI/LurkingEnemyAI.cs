using UnityEngine;

public class LurkingEnemyAI : MonoBehaviour {
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    [SerializeField] private float moveSpeed = 2f;
    private bool isDistracted = false;
    [SerializeField] private float distractionTime = 5f; // How long the enemy stays distracted
    [SerializeField] private float distractionTimer = 0f;
    private Transform distractionSource; // Sound or light source that distracted the enemy
    public LayerMask detectionLayer;

    private Vector2 moveDirection;

    private void Start() {
        if (patrolPoints.Length > 0)
        {
            PatrolToNextPoint();
        }
    }

    private void Update() {
        if (isDistracted)
        {
            // Track distraction time
            distractionTimer += Time.deltaTime;
            if (distractionTimer >= distractionTime)
            {
                isDistracted = false;
                distractionTimer = 0f;
                PatrolToNextPoint();  // Resume patrol
            }
        }
        else
        {
            PatrolBehavior();
            DetectDistraction();
        }
    }

    void PatrolBehavior()
    {
        // Move towards the current patrol point
        if (patrolPoints.Length > 0)
        {
            Vector2 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // If close enough to the patrol point, switch to the next point
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
            {
                PatrolToNextPoint();
            }
        }
    }

    void PatrolToNextPoint()
    {
        // Move to the next patrol point
        if (patrolPoints.Length == 0) return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void DetectDistraction()
    {
        // Detect if any distraction sources (sound or light) are nearby
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, 5f, detectionLayer);

        foreach (var detectedObject in detectedObjects)
        {
            if (detectedObject.CompareTag("SoundSource") || detectedObject.CompareTag("LightSource"))
            {
                isDistracted = true;
                distractionSource = detectedObject.transform;
                MoveTowardsDistraction();
                break;
            }
        }
    }

    void MoveTowardsDistraction()
    {
        // Move towards the detected distraction source
        if (distractionSource != null)
        {
            Vector2 direction = (distractionSource.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}