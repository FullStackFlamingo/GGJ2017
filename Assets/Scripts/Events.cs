using UnityEngine.Events;
using UnityEngine;

public enum Events
{
	PlayerCarryStartEvent,  //	Invoked on the first frame, when the player begins carrying an item
	PlayerCarryStopEvent,   //	Invoked every frame, while the player is carrying an item
	PlayerCarryActiveEvent, //	Invoked on the last frame, when the stops carrying an item

	ItemCollisionEnterEvent,    //	Invoked on the first frame, when the player begins colliding with an item
	ItemCollisionStayEvent,     //	Invoked every frame, while the player is colliding with an item
	ItemCollisionExitEvent,     //	Invoked on the last frame, when the player stops colliding with an item

	ItemSpawnedEvent,   //	Invoked on the first frame, when an item is spawned
	ItemActiveEvent,    //	Invoked every frame, while there is one or more active items
	ItemDestroyedEvent, //	Invoked on the last frame, when an item is destroyed

	PlayerMovementStartEvent,   //	Invoked on the first frame, when the player begins moving
	PlayerMovementActiveEvent,  //	Invoked every frame, while the player is moving
	PlayerMovementStopEvent,    //	Invoked on the last frame, when the player stops moving

	GameStartEvent,         //	Invoked on the first frame, when the gameplay begins
	GameActiveEvent,        //	Invoked on every frame, while gameplay is active
	GameEndEvent,           //	Invoked on the last frame, when a wave is activated

	WaveStartEvent,     //	Invoked on the first frame, when a wave is activated
	WaveActiveEvent,    //	Invoked every frame, while a wave is active
	WaveEndEvent        //	Invoked on the last frame, when a wave is de-activated
};


[System.Serializable]
public class PlayerCarryEvent : UnityEvent<PlayerCarryData> { }
public struct PlayerCarryData
{
	//	public PlayerCarryEvent playerCarryEvent;
	public GameObject player;
	public GameObject item;

	public PlayerCarryData(/*PlayerCarryEvent playerCarryEvent,*/ GameObject player, GameObject item)
	{
		//		this.playerCarryEvent = playerCarryEvent;
		this.player = player;
		this.item = item;
	}
}

[System.Serializable]
public class ItemCollisionEvent : UnityEvent<ItemCollisionData> { }
public struct ItemCollisionData
{
	//	ItemCollisionEvent itemCollisionEvent;
	public Collision collisionData;
	public GameObject item;

	public ItemCollisionData(/*ItemCollisionEvent itemCollisionEvent,*/ Collision collisionData, GameObject item)
	{
		//		this.itemCollisionEvent = itemCollisionEvent;
		this.collisionData = collisionData;
		this.item = item;
	}
}

[System.Serializable]
public class PlayerMovementEvent : UnityEvent<PlayerMovementData> { }
public struct PlayerMovementData
{
	//	PlayerMovementEvent playerMovementEvent;
	public Vector3 movementVelocity;
	public GameObject player;

	public PlayerMovementData(/*PlayerMovementEvent playerMovementEvent,*/ Vector3 movementVelocity, GameObject player)
	{
		//		this.playerMovementEvent = playerMovementEvent;
		this.movementVelocity = movementVelocity;
		this.player = player;
	}
}

[System.Serializable]
public class GameStatusEvent : UnityEvent<GameStatusData> { }
public struct GameStatusData
{
	//	GameStatusEvent gameStatusEvent;
	public bool isPaused;
	public float timeSincePlayStart;
	public float progressPercentage;

	public GameStatusData(/*GameStatusEvent gameStatusEvent,*/ bool isPaused, float timeSincePlayStart, float progressPercentage)
	{
		//		this.gameStatusEvent = gameStatusEvent;
		this.isPaused = isPaused;
		this.timeSincePlayStart = timeSincePlayStart;
		this.progressPercentage = progressPercentage;
	}
}

[System.Serializable]
public class ItemStatusEvent : UnityEvent<ItemStatusData> { }
public struct ItemStatusData
{
	//	ItemStatusEvent itemStatusEvent;
	public GameObject item;
	public Vector3 itemPosition;
	public bool isCarried;

	public ItemStatusData(/*ItemStatusEvent itemStatusEvent,*/ GameObject item, Vector3 itemPosition, bool isCarried)
	{
		//		this.itemStatusEvent = itemStatusEvent;
		this.item = item;
		this.itemPosition = itemPosition;
		this.isCarried = isCarried;
	}

}

[System.Serializable]
public class WaveStatusEvent : UnityEvent<WaveStatusData> { }
public struct WaveStatusData
{
	//	WaveStatusEvent waveStatusEvent;
	public GameObject wave;
	public Vector3 wavePosition;
	public float waveProgressPercentage;

	public WaveStatusData(/*WaveStatusEvent waveStatusEvent,*/ GameObject wave, Vector3 wavePosition, float waveProgressPercentage)
	{
		//		this.waveStatusEvent = waveStatusEvent;
		this.wave = wave;
		this.wavePosition = wavePosition;
		this.waveProgressPercentage = waveProgressPercentage;
	}
}