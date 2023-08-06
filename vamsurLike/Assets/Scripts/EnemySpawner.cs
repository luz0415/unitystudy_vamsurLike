using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;

    public GameObject enemy;

    private float lastTimeSpawn;
    private float timeBetSpawn;
    public float timeMinSpawn = 3f;
    public float timeMaxSpawn = 5f;

    private float lastTimeWave;
    private float timeBetWave;
    public float timeMinWave = 12f;
    public float timeMaxWave = 20f;
    private int waveCount;

    private int smallWaveEnemyNum;
    public int smallWaveEnemyMin = 3;
    public int smallWaveEnemyMax = 6;

    private int bigWaveEnemyNum;
    public int bigWaveEnemyMin = 7;
    public int bigWaveEnemyMax = 10;

    public float spawnDistance = 15f;

    public float enemySpeedMin = 5f;
    public float enemySpeedMax = 10f;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            player = GameManager.instance.player;
        }

        lastTimeSpawn = -3f;
        timeBetSpawn = 3f;

        lastTimeWave = 0f;
        timeBetWave = Random.Range(timeMinWave, timeMaxWave);

        waveCount = 0;
    }

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover) return;

        if(lastTimeSpawn + timeBetSpawn <= Time.timeSinceLevelLoad)
        {
            timeBetSpawn = Random.Range(timeMinSpawn, timeMaxSpawn);
            lastTimeSpawn = Time.timeSinceLevelLoad;
            spawnEnemy(1);
        }

        if (lastTimeWave + timeBetWave <= Time.timeSinceLevelLoad)
        {
            lastTimeWave = Time.timeSinceLevelLoad;
            
            if(waveCount == 4)
            {
                waveCount = 0;

                bigWaveEnemyNum = Random.Range(bigWaveEnemyMin, bigWaveEnemyMax);
                spawnEnemy(bigWaveEnemyNum);
            }
            else
            {
                waveCount++;

                smallWaveEnemyNum = Random.Range(smallWaveEnemyMin, smallWaveEnemyMax);
                spawnEnemy(smallWaveEnemyNum);
            }
            // 食君鯵 持失
        }
    }

    private void spawnEnemy(int enemyNum)
    {
        Vector3 spawnPoint;
        RandomPoint(player.transform.position, spawnDistance, out spawnPoint);

        for (int i = 0; i < enemyNum; i++)
        {
            Enemy newEnemy;
            if (transform.childCount > 0) 
            {
                newEnemy = transform.GetChild(0).GetComponent<Enemy>();
                newEnemy.transform.parent = null;

                newEnemy.gameObject.transform.position = spawnPoint;
                newEnemy.gameObject.transform.rotation = Quaternion.identity;
                newEnemy.gameObject.SetActive(true);
            } 
            else 
            {
                newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity).GetComponent<Enemy>();

                newEnemy.onDeath += () => print("ENEMY DEAD!!");
                newEnemy.onDeath += () => newEnemy.transform.parent = transform;
                newEnemy.onDeath += () => newEnemy.gameObject.SetActive(false);
            }

            float enemyIntensity = Random.value;

            float newSpeed = Mathf.Lerp(enemySpeedMin, enemySpeedMax, enemyIntensity);
            Color newColor = Color.Lerp(Color.white, Color.red, enemyIntensity);

            newEnemy.Setup(newColor, newSpeed);
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = new Vector3(Random.value, 0f, Random.value).normalized;
            Vector3 randomPoint = center + randomDirection * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
