using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime;
    public Transform[] spawnPoints;
    public Transform[] enemy;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyType = Random.Range(0, enemy.Length);

        Instantiate(enemy[enemyType], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
