using UnityEditor.Animations;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] Bosses;
    [SerializeField] private GameObject[] BossEnemy;
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private float bossEnemySpawnTimer = 2f;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private int maxBosses = 1;
    [SerializeField] private int maxBossEnemies = 2;
    public int activeEnemies = 0;
    private float timer = 0f;
    private int randomEnemy;
    private int bossTimer = 0;
    public int activeBoss = 0;
    public int activeBossEnemies = 0;   

    void Start()
    {
        int seed = System.DateTime.Now.Millisecond + gameObject.GetInstanceID();
        Random.InitState(seed);
    }   
    void Update()
    {
        if (activeBoss == 0)
        {
            if (activeEnemies < maxEnemies)
            {
                timer += Time.deltaTime;
                if (timer >= spawnTimer)
                {
                    SpawnEnemy();
                    timer = 0f;
                    bossTimer++;
                        if (bossTimer >= 6)
                        {
                            activeBoss++;
                        }
                    }
                }
        }
        else if (bossTimer >= 6)
        {
            if (activeEnemies <= 0)
            {
                if (activeBoss == 1)
                {
                    if (activeBossEnemies < maxBossEnemies)
                    {
                        timer += Time.deltaTime;
                        if (timer >= bossEnemySpawnTimer)
                        {
                            SpawnBossEnemy();
                            timer = 0f;
                        }
                    }
                    if (activeBossEnemies >= 2)
                    {
                        SpawnBoss();
                        bossTimer = 0;
                    }
                    
                }
            }
        }
    }

    void SpawnEnemy()
    {
        randomEnemy = Random.Range(0, enemies.Length);
        GameObject enemy = Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
        activeEnemies++;

    }
    void SpawnBoss()
    {
        GameObject enemy = Instantiate(Bosses[0], transform.position, Quaternion.identity);
        activeBoss++;
    }
    void SpawnBossEnemy()
    {
        GameObject enemy = Instantiate(BossEnemy[0], transform.position, Quaternion.identity);
        activeBossEnemies++;

    }
}
