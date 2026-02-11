using UnityEngine;

public class DoorMoveScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int scoreForDoor = 50;

    // How far the door must move to count as "opened"
    public float openDistance = 0.05f;

    private Vector3 startPos;
    private bool given = false;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (given) return;
        if (scoreManager == null) return;

        // If the door has moved enough from its start position, give score once
        float moved = Vector3.Distance(transform.position, startPos);
        if (moved >= openDistance)
        {
            scoreManager.AddScore(scoreForDoor);
            given = true;
            Debug.Log("DoorMoveScore: score given by " + gameObject.name);
        }
    }
}
