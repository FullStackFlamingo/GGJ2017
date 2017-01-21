using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public GameObject waveFrontSpawnpoint, waveBackSpawnpoint;
	public GameObject waveFrontTarget, waveBackTarget;
	public GameObject wavePrefab;
	public float waveSpawnRate;
	public float waveSpeed;
	public List<GameObject> activeFrontWaves, activeBackWaves;
	private Queue<GameObject> inactiveWaves;
	private float waveSpawnCountdown;
	private Vector3 waveFrontSpawnpointPosition; 
	private Vector3 waveBackSpawnpointPosition;
	private Vector3 waveFrontTargetPosition;
	private Vector3 waveBackTargetPosition;
	private float totalFrontDistance;
	private float totalBackDistance;


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

	void Awake()
	{
		activeFrontWaves = new List<GameObject>();
		activeBackWaves = new List<GameObject>();
		inactiveWaves = new Queue<GameObject>();
	}

	void OnEnable()
	{

		waveFrontSpawnpointPosition = waveFrontSpawnpoint.transform.position;
		waveBackSpawnpointPosition = waveBackSpawnpoint.transform.position;
		waveFrontTargetPosition = waveFrontTarget.transform.position;
		waveBackTargetPosition = waveBackTarget.transform.position;
		totalFrontDistance = (waveFrontTargetPosition - waveFrontSpawnpointPosition).magnitude;
		totalBackDistance = (waveBackTargetPosition - waveBackSpawnpointPosition).magnitude;
		InvokeRepeating("InvokeWaveSpawn", 0f, waveSpawnRate);
	}

	void OnDisable()
	{
		CancelInvoke("InvokeWaveSpawn");
	}

	void Update()
	{
		Vector3 wavePosition;
		for (int i = 0; i < activeFrontWaves.Count; ++i)
		{
			wavePosition = activeFrontWaves[i].transform.position;
			wavePosition = Vector3.MoveTowards(wavePosition, waveFrontTargetPosition, waveSpeed * Time.deltaTime);
			activeFrontWaves[i].transform.position = wavePosition;
			if (wavePosition == waveFrontTargetPosition)
			{
				activeFrontWaves[i].SetActive(false);
				inactiveWaves.Enqueue(activeFrontWaves[i]);
				activeFrontWaves.Remove(activeFrontWaves[i]);
			}
		}

		for (int i = 0; i < activeBackWaves.Count; ++i)
		{
			wavePosition = activeBackWaves[i].transform.position;
			wavePosition = Vector3.MoveTowards(wavePosition, waveBackTargetPosition, waveSpeed * Time.deltaTime);
			activeBackWaves[i].transform.position = wavePosition;
			if (wavePosition == waveBackTargetPosition)
			{
				activeBackWaves[i].SetActive(false);
				inactiveWaves.Enqueue(activeBackWaves[i]);
				activeBackWaves.Remove(activeBackWaves[i]);
			}
		}
	}

	void InvokeWaveSpawn()
	{
		switch ((int)(Time.time % 2))
		{
			case 0:
				activeFrontWaves.Add(SpawnWave(waveFrontSpawnpoint, waveFrontTarget, activeFrontWaves, Quaternion.identity));
				break;
			case 1:
				activeBackWaves.Add(SpawnWave(waveBackSpawnpoint, waveBackTarget, activeBackWaves,Quaternion.Euler(0f,180f,0f)));
				break;
			default:
				Debug.Log("oops");
				break;
		}
	}

	GameObject SpawnWave(GameObject waveSpawnPoint, GameObject waveTarget, List<GameObject> activeWaveList, Quaternion waveOrientation)
	{
		GameObject newWave;
		if (inactiveWaves.Count == 0)
		{
			newWave = Instantiate(wavePrefab as GameObject, waveSpawnPoint.transform.position, waveOrientation) as GameObject;
			newWave.SetActive(true);
		}
		else
		{
			newWave = inactiveWaves.Dequeue();
			newWave.transform.position = waveSpawnPoint.transform.position;
			newWave.transform.rotation = waveOrientation;
			newWave.SetActive(true);
		}
		newWave.GetComponent<Rigidbody>().velocity = Vector3.zero;
		activeWaveList.Add(newWave);
		return newWave;
	}


}
