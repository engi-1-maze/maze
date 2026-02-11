using UnityEngine;

public class TimeBonusScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Cronometro cronometro;
    public float maxTime = 300f;
    public int bonusScore = 50;

    private bool given = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (given) return;
        if (scoreManager == null || cronometro == null) return;

        if (cronometro.Tiempo() <= maxTime)
        {
            scoreManager.AddScore(bonusScore);
            given = true;
        }
    }
}
