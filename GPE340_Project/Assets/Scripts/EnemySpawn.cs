using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private Enemy [] enemies;
    [SerializeField]
    private Enemy [] bosses;
    [SerializeField]
    private GameObject [] spawnPoints;
    [SerializeField]
    private int maxActiveEnemies;
    [SerializeField]
    private int currentActiveEnemies;
    [SerializeField]
    private int enemiesUntilBoss;
    [SerializeField]
    private float spawnDelay;
    public float spawnNumber;
    [SerializeField]
    private bool firstTimeFull;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine (SpawnOneEnemy());
        InvokeRepeating ("SpawnEnemy", 0f, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentActiveEnemies == maxActiveEnemies || enemiesUntilBoss == 0)
        {
            CancelInvoke ("SpawnEnemy");
            StopAllCoroutines();
            firstTimeFull = true;
        }

        if (enemiesUntilBoss > 0 && currentActiveEnemies < maxActiveEnemies)
        {
            if(firstTimeFull)
            {
                StartCoroutine (SpawnOneEnemy());
            }
            
        }

        if(enemiesUntilBoss == 0)
        {
            SpawnBoss();
        }
    }

    void BossDefeated()
    {
        GameManager.Instance.WinGame();
    }

    public void SpawnEnemy()
    {
        if (currentActiveEnemies < maxActiveEnemies)
        {
            IncreaseActiveEnemies();
            if(enemiesUntilBoss > 0)
            {
                MarkEnemiesUntilBoss();
            }
            
            Enemy enemy = Instantiate (enemies[Random.Range (0, enemies.Length)], spawnPoints[Random.Range (0, spawnPoints.Length)].transform.position, spawnPoints[Random.Range (0, spawnPoints.Length)].transform.rotation) as Enemy;
            
            enemy.healthEnemy.OnDeath.AddListener (HandleEnemyDeath);
        }
    }

    public void SpawnBoss()
    {
        Enemy boss = Instantiate (bosses[Random.Range (0, enemies.Length)], spawnPoints[Random.Range (0, spawnPoints.Length)].transform.position, spawnPoints[Random.Range (0, spawnPoints.Length)].transform.rotation) as Enemy;
        MarkEnemiesUntilBoss();
        boss.healthEnemy.OnDeath.AddListener (BossDefeated);
    }

    private void IncreaseActiveEnemies()
    {
        currentActiveEnemies++;
    }

    private void MarkEnemiesUntilBoss()
    {
        enemiesUntilBoss--;
    }

    private void HandleEnemyDeath()
    {
        currentActiveEnemies--;
    }

    IEnumerator SpawnOneEnemy ()
    {
        yield return new WaitForSeconds (spawnDelay);
        if (currentActiveEnemies < maxActiveEnemies)
        {
            IncreaseActiveEnemies();
            if(enemiesUntilBoss > 0)
            {
                MarkEnemiesUntilBoss();
            }
            
            Enemy enemy = Instantiate (enemies[Random.Range (0, enemies.Length)], spawnPoints[Random.Range (0, spawnPoints.Length)].transform.position, spawnPoints[Random.Range (0, spawnPoints.Length)].transform.rotation) as Enemy;
            
            enemy.healthEnemy.OnDeath.AddListener (HandleEnemyDeath);
        }
    }
}
