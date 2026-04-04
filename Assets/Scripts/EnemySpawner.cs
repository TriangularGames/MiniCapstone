using UnityEditor.Animations;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private int maxEnemies = 3;
    public int activeEnemies = 0;
    private float timer = 0f;
    private int randomEnemy;

    void Start()
    {
        int seed = System.DateTime.Now.Millisecond + gameObject.GetInstanceID();
        Random.InitState(seed);
    }   
    void Update()
    {
        if (activeEnemies <= maxEnemies)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTimer)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        randomEnemy = Random.Range(0, enemies.Length);
        GameObject enemy = Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
        activeEnemies++;
    }
}
