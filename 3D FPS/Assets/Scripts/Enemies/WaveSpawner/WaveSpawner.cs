using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [HideInInspector] public float countdown;

    public Transform[] spawnPoints;
    private Transform spawnPointLocation;

    public Wave[] waves;

    public int currentWaveIndex = 0;

    public GameObject gameManager;

    public UIManager uiManager;

    private bool readyToCountDown;

    private void Start()
    {
        uiManager.GetComponent<UIManager>().WaveNumberUpdate(true);
        uiManager.GetComponent<UIManager>().CountdownUpdate(true);
        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    private void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            uiManager.GetComponent<UIManager>().WaveNumberUpdate(false);
            uiManager.GetComponent<UIManager>().CountdownUpdate(false);
            Debug.Log("You survived every wave!");
            gameManager.GetComponent<GameManager>().LevelCleared();
            return;
        }

        if (readyToCountDown)
        {
            countdown -= Time.deltaTime;
            uiManager.GetComponent<UIManager>().WaveNumberUpdate(true);
            uiManager.GetComponent<UIManager>().CountdownUpdate(true);
        }

        if (countdown <= 0)
        {
            readyToCountDown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;

            uiManager.GetComponent<UIManager>().WaveNumberUpdate(false);
            uiManager.GetComponent<UIManager>().CountdownUpdate(false);

            StartCoroutine(SpawnWave());
        }
        
        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
            uiManager.GetComponent<UIManager>().WaveNumberUpdate(true);
            uiManager.GetComponent<UIManager>().CountdownUpdate(true);
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                PickSpawnPoints();

                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPointLocation.transform);

                enemy.GetComponent<Health>().isWaveSpawnerEnemy = true;

                enemy.transform.SetParent(spawnPointLocation.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    void PickSpawnPoints()
    {
        int indexNumber = Random.Range(0, spawnPoints.Length);
        spawnPointLocation = spawnPoints[indexNumber];
        Debug.Log("Spawn Point: " + spawnPointLocation.transform);
    }

    public void DecreaseEnemies()
    {
        waves[currentWaveIndex].enemiesLeft--;
        Debug.Log("Enemies Left: " + waves[currentWaveIndex].enemiesLeft);
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}