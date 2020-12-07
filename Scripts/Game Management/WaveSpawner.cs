using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform[] enemy;
		public float waveDuration;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;
	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public Transform[] spawnPoints;

	public float timeBetweenWaves;
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (waves[nextWave].waveDuration <= 0)
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
                StartCoroutine( SpawnWave ( waves[nextWave] ) );
			}

            waves[nextWave].waveDuration -= Time.deltaTime;
        }
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("ALL WAVES COMPLETE! Looping...");
		}
		else
		{
			nextWave++;
		}
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;

		while (_wave.waveDuration > 0)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform[] _enemy)
    {
		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
        Transform enemy = _enemy[Random.Range(0, _enemy.Length)];
		Instantiate(enemy, _sp.position, _sp.rotation);
	}

}
