using UnityEngine;
using System.Collections;

public class HeartSpawner : MonoBehaviour
{
    public Transform heart;
    public Transform[] spawnPoints;

    public float spawnCoolDown = 10f;
    float currentCoolDown;

    private void FixedUpdate()
    {
        if (currentCoolDown >= spawnCoolDown)
        {
            SpawnHeart(heart);
            currentCoolDown = 0;
        }
    }

    private void Update()
    {
        if (currentCoolDown < spawnCoolDown)
        {
            currentCoolDown += Time.deltaTime;
        }
    }
    void SpawnHeart(Transform heart)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(heart, _sp.position, _sp.rotation);
    }
}
