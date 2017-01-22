using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public GameObject frontWave, backWave;
	public GameObject sceneController;
	private MainSceneController mainSceneController;
	private WaveMovementController frontWaveMovementController, backWaveMovementController;
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

	void OnEnable()
	{
		frontWaveMovementController = frontWave.GetComponent<WaveMovementController>();
		backWaveMovementController = backWave.GetComponent<WaveMovementController>();
		frontWaveMovementController.waveActive = true;
		frontWave.SetActive(true);
		backWaveMovementController.waveActive = false;
		backWave.SetActive(false);
		mainSceneController = sceneController.GetComponent<MainSceneController> ();
	}

	void Update()
	{
		if (Mathf.Approximately(frontWaveMovementController.waveProgress,1f))
		{
			Debug.Log("frontWavebooop");
			backWaveMovementController.waveActive = true;
			backWave.SetActive(true);
			frontWaveMovementController.waveActive = false;
			frontWave.SetActive(false);
		}
		if (Mathf.Approximately(backWaveMovementController.waveProgress, 1f))
		{
			Debug.Log("backWavebooop");
			frontWaveMovementController.waveActive = true;
			frontWave.SetActive(true);
			backWaveMovementController.waveActive = false;
			backWave.SetActive(false);
		}
		if (Mathf.Approximately (frontWaveMovementController.waveProgress, 1f) || Mathf.Approximately (backWaveMovementController.waveProgress, 1f)) 
		{
			mainSceneController.InvokeItemSpawn ();
		}
	}
}
