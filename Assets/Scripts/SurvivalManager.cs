using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SurvivalManager : MonoBehaviour
{
    [Header("Timer")]
    public float survivalTime = 90f;
    private float timer;

    public TextMeshProUGUI timerText;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private bool spawnedSecondEnemy;
    private bool spawnedFinalEnemy;

    [Header("Door")]
    public GameObject exitDoor;

    void Start()
    {
        timer = survivalTime;

        // Door locked at start
        exitDoor.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        UpdateTimerUI();

        // Spawn second enemy at 60 seconds left
        if (timer <= 60f && !spawnedSecondEnemy)
        {
            spawnedSecondEnemy = true;

            SpawnEnemy(spawnPoint1);
        }

        // Spawn final enemy at 30 seconds left
        if (timer <= 30f && !spawnedFinalEnemy)
        {
            spawnedFinalEnemy = true;

            SpawnEnemy(spawnPoint2);

            // Unlock door
            exitDoor.SetActive(true);
        }

        if (timer <= 0)
        {
            timer = 0;
        }
    }

    void SpawnEnemy(Transform spawnPoint)
    {
        Instantiate(enemyPrefab,
                    spawnPoint.position,
                    spawnPoint.rotation);
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timer);

        timerText.text = "Survive: " + seconds;
    }
}