using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public GameObject waveForegroundSpawnpoint, waveBackgroundSpawnpoint;
	public GameObject waveForegroundTarget, waveBackgroundTarget;
	public GameObject wavePrefab;
	public float waveSpawnRate;
	public List<GameObject> activeForegroundWaves, activeBackgroundWaves;
	private float waveSpawnCountdown;
	private List<GameObject> activeWaves;

	private static WaveManager waveManager;
	public static WaveManager Instance
	{
		get
		{
			if (!waveManager)
			{
				waveManager = FindObjectOfType(typeof(WaveManager)) as WaveManager;
				if (!waveManager)
					waveManager = Camera.main.gameObject.AddComponent<WaveManager>();
			}
			return waveManager;
		}
	}

	void OnEnable ()
	{
		EventManager.StartListening<GameStatusData>(Events.GameStartEvent, OnGameStart);
		EventManager.StartListening<GameStatusData>(Events.GameStartEvent, OnGameEnd);
	}

	void OnDisable()
	{
		EventManager.StopListening<GameStatusData>(Events.GameStartEvent, OnGameStart);
		EventManager.StopListening<GameStatusData>(Events.GameStartEvent, OnGameEnd);
	}

	void OnGameStart(GameStatusData waveData)
	{
		InvokeRepeating("SpawnWave", 0f, waveSpawnRate);
	}

	void OnGameEnd(GameStatusData waveData)
	{
		CancelInvoke("SpawnWave");
	}

	void InvokeWaveSpawn()
	{
		switch ((int)(Mathf.PingPong(Time.deltaTime, 2)))
		{
			case 0:
				SpawnWave(waveForegroundSpawnpoint.transform.position, activeForegroundWaves, Quaternion.identity);
				break;
			case 1:
				SpawnWave(waveForegroundSpawnpoint.transform.position, activeForegroundWaves, Quaternion.identity);
				SpawnWave(waveBackgroundSpawnpoint.transform.position, activeBackgroundWaves, Quaternion.identity);
				break;
			case 2:
				SpawnWave(waveBackgroundSpawnpoint.transform.position, activeBackgroundWaves, Quaternion.identity);
				break;
		}
	}

	void SpawnWave(Vector3 waveSpawnPoint, List<GameObject> activeWaveList, Quaternion waveOrientation)
	{
		GameObject newWave;
		newWave = Instantiate(wavePrefab as GameObject, waveSpawnPoint, waveOrientation);
		activeWaveList.Add(newWave);
		EventManager.InvokeEvent<WaveStatusData>(Events.WaveStartEvent, new WaveStatusData(newWave, waveSpawnPoint, 0f));
	}

	void Update()
	{
		float waveProgress;
		float totalForegroundDistance = (waveForegroundSpawnpoint.transform.position - waveForegroundTarget.transform.position).magnitude;
		float totalBackgroundDistance = (waveBackgroundSpawnpoint.transform.position - waveBackgroundTarget.transform.position).magnitude;
		foreach (GameObject wave in activeForegroundWaves)
		{
			waveProgress = (wave.transform.position - waveForegroundSpawnpoint.transform.position).magnitude;
			EventManager.InvokeEvent<WaveStatusData>(Events.WaveActiveEvent, new WaveStatusData(wave, wave.transform.position, waveProgress / totalForegroundDistance));
		}
		foreach (GameObject wave in activeBackgroundWaves)
		{
			waveProgress = (wave.transform.position - waveBackgroundSpawnpoint.transform.position).magnitude;
			EventManager.InvokeEvent<WaveStatusData>(Events.WaveActiveEvent, new WaveStatusData(wave, wave.transform.position, waveProgress / totalBackgroundDistance));
		}
	}
}
