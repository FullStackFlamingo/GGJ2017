using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour {


	public void OnEnable()
	{
		EventManager.StartListening(Events.WaveBeginEvent, OnWaveBegin);
	}

	public void OnDisable()
	{
		EventManager.StopListening(Events.WaveBeginEvent, OnWaveBegin);
	}

	public void Update()
	{
		EventManager.InvokeEvent(Events.WaveBeginEvent);
	}

	public void OnWaveBegin()
	{
		Debug.Log("WaveEvent");
	}
}
