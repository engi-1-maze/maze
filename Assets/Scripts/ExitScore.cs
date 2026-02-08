using UnityEngine;

public class ExitScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int scoreForFinish = 100;

    private bool given = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (given) return;
        if (scoreManager == null) return;

        scoreManager.AddScore(scoreForFinish);
        given = true;
    }
}
