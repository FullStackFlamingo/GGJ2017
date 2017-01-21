using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
	private Dictionary<Events, UnityEventBase> eventDictionary;

	private static EventManager eventManager;
	public static EventManager Instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
				if (!eventManager)
					Debug.LogError("There needs to be an active EventManager script on a GameObject in your scene");
				else
					eventManager.Init();
			}
			return eventManager;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
			eventDictionary = new Dictionary<Events, UnityEventBase>();
	}

	public static void StartListening(Events eventName, UnityAction listener)
	{
		UnityEventBase thisEvent = null;
		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
			(thisEvent as UnityEvent).AddListener(listener);
		else
		{
			thisEvent = new UnityEvent();
			(thisEvent as UnityEvent).AddListener(listener);
			Instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StartListening<T0>(Events eventName, UnityAction<T0> listener)
	{
		UnityEventBase thisEvent = null;
		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
			(thisEvent as UnityEvent<T0>).AddListener(listener);
		else
		{
			thisEvent = new UnityEvent();
			(thisEvent as UnityEvent<T0>).AddListener(listener);
			Instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(Events eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEventBase thisEvent = null;
		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
			(thisEvent as UnityEvent).RemoveListener(listener);
	}

	public static void StopListening<T0>(Events eventName, UnityAction<T0> listener)
	{
		if (eventManager == null) return;
		UnityEventBase thisEvent = null;
		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
			(thisEvent as UnityEvent<T0>).RemoveListener(listener);
	}

	public static void InvokeEvent(Events eventName)
	{
		UnityEventBase thisEvent = null;

		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
			(thisEvent as UnityEvent).Invoke();
	}

	public static void InvokeEvent<T0>(Events eventName, T0 arg)
	{
		UnityEventBase thisEvent = null;

		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
			(thisEvent as UnityEvent<T0>).Invoke(arg);
	}
}