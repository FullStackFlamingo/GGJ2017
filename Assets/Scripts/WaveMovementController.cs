using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovementController : MonoBehaviour {

	public Transform spawnPoint;
	public Transform target;
	public bool waveActive;
	public float waveProgress;
	public float progressUpdateMultiplier;

	// Use this for initialization

	void OnEnable()
	{
		waveProgress = 0f;
		this.transform.position = spawnPoint.position;
		this.transform.rotation = spawnPoint.rotation;
		this.transform.localScale = spawnPoint.localScale;
	}

	// Update is called once per frame
	void Update ()
	{
		if (waveActive)
		{
			waveProgress += Time.deltaTime * progressUpdateMultiplier;
			waveProgress = Mathf.Clamp01(waveProgress);
			this.transform.position = Vector3.Lerp(spawnPoint.position, target.position, waveProgress);
			this.transform.rotation = Quaternion.Slerp(spawnPoint.rotation, target.rotation, waveProgress);
			this.transform.localScale = Vector3.Lerp(spawnPoint.localScale, target.localScale, waveProgress);
		}
	}

	void ResetWave ()
	{
		waveActive = false;
		this.gameObject.SetActive(false);
		waveProgress = 0f;
		this.transform.position = spawnPoint.position;
		this.transform.rotation = spawnPoint.rotation;
		this.transform.localScale = spawnPoint.localScale;
	}
}
