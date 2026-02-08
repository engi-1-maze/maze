using UnityEngine;

public class EnemyScoreByDistance : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public ScoreManager scoreManager;

    [Header("Detection (lose score once)")]
    public float detectDistance = 8f;
    public int detectPenalty = 20;

    [Header("Touch / Attack (lose score repeatedly)")]
    public float hitDistance = 1.2f;
    public int hitPenalty = 10;
    public float hitCooldown = 1.5f;

    [Header("Start protection")]
    public float startDelay = 1.5f; // seconds before detection starts

    private bool detectedOnce = false;
    private float hitTimer = 0f;
    private float timer = 0f;

    private void Update()
    {
        if (player == null || scoreManager == null) return;

        timer += Time.deltaTime;
        if (timer < startDelay) return; // 👈 ignore detection at start

        float distance = Vector3.Distance(transform.position, player.position);

        // DETECTION (only once)
        if (!detectedOnce && distance <= detectDistance)
        {
            scoreManager.AddScore(-detectPenalty);
            detectedOnce = true;
            Debug.Log("Enemy detected player: -" + detectPenalty);
        }

        // HIT / TOUCH (with cooldown)
        if (distance <= hitDistance)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0f)
            {
                scoreManager.AddScore(-hitPenalty);
                hitTimer = hitCooldown;
                Debug.Log("Enemy hit player: -" + hitPenalty);
            }
        }
    }
}
