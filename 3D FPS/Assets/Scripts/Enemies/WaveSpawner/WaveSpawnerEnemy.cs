using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerEnemy : MonoBehaviour
{
    private WaveSpawner waveSpawner;

    private float countdown = 5f;

    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
    }
    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        }
    }
}
