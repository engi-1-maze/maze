using UnityEngine;

public class DeathScoreReset : MonoBehaviour
{
    public ScoreManager scoreManager;

    // Llama esto cuando el jugador muere
    public void ResetScore()
    {
        if (scoreManager == null) return;
        scoreManager.ResetScore();
    }
}
