using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public float spawnDelay = 30f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnDelay, spawnDelay);
    }

   void SpawnEnemy()
{
    // Pick random spawn point
    int randomIndex = Random.Range(0, spawnPoints.Length);

    Transform spawnPoint = spawnPoints[randomIndex];

    Vector3 spawnPos = spawnPoint.position;

    NavMeshHit hit;

    if (NavMesh.SamplePosition(spawnPos,
                               out hit,
                               5f,
                               NavMesh.AllAreas))
    {
        Instantiate(enemyPrefab,
                    hit.position,
                    Quaternion.identity);
    }
}
}