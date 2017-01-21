using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{

	public GameObject player;
	public Vector3 playerMovementVelocity;
	public void OnEnable()
	{
		EventManager.StartListening<PlayerMovementData>(Events.PlayerMovementActiveEvent, OnPlayerMoving);
	}

	public void OnDisable()
	{
		EventManager.StopListening<PlayerMovementData>(Events.PlayerMovementActiveEvent, OnPlayerMoving);
	}

	public void Update()
	{
		PlayerMovementData playerMovementData = new PlayerMovementData(playerMovementVelocity, player);
		EventManager.InvokeEvent(Events.PlayerMovementActiveEvent, playerMovementData);
	}

	public void OnPlayerMoving(PlayerMovementData movementData)
	{
		Debug.Log(movementData.movementVelocity.ToString());
	}
}
