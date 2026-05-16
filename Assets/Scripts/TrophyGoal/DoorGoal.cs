using UnityEngine;

public class TrophyGoal : MonoBehaviour
{
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.PlayerReachedTrophy();
        }
    }
}
