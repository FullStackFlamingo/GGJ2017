using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public GameObject frontWave, backWave;
	public GameObject sceneController;
	private MainSceneController mainSceneController;
	private WaveMovementController frontWaveMovementController;
	private WaveMovementController backWaveMovementController;
	private static WaveManager waveManager;
	private bool itemSpawned;
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

	void OnEnable()
	{
		frontWaveMovementController = frontWave.GetComponent<WaveMovementController>();
		backWaveMovementController = backWave.GetComponent<WaveMovementController>();
		frontWaveMovementController.waveActive = true;
		
		frontWave.SetActive(true);
		backWaveMovementController.waveActive = false;
		backWave.SetActive(false);
		mainSceneController = sceneController.GetComponent<MainSceneController> ();
		itemSpawned = false;
	}

	void Update()
	{
		if (Mathf.Approximately(frontWaveMovementController.waveProgress,1f) && frontWaveMovementController.waveActive)
		{
			Debug.Log("frontWavebooop");
			backWaveMovementController.waveActive = true;
			backWave.SetActive(true);
			frontWaveMovementController.waveActive = false;
			frontWave.SetActive(false);
			itemSpawned = false;
//			mainSceneController.InvokeItemSpawn ();
		}
		if (Mathf.Approximately(backWaveMovementController.waveProgress, 1f) && backWaveMovementController.waveActive)
		{
			Debug.Log("backWavebooop");
			frontWaveMovementController.waveActive = true;
			frontWave.SetActive(true);
			backWaveMovementController.waveActive = false;
			backWave.SetActive(false);
			itemSpawned = false;
		}
		if (itemSpawned == false 
			&& ((backWaveMovementController.waveProgress > 0.7f && backWaveMovementController.waveActive)
			|| (frontWaveMovementController.waveProgress > 0.7f && frontWaveMovementController.waveActive))) 
		{
			mainSceneController.InvokeItemSpawn ();
			itemSpawned = true;
		}
	}
}
