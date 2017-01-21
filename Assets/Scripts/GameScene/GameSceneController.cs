using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game scene controller, spawns and keeps track of items on ship and invokes the wave methods.

public class GameSceneController : MonoBehaviour {

	public GameObject player; 
	public GameObject boat; 
	public GameObject[] itemPositions;
	public GameObject[] items;  

	void Start () {
 
		itemPositions = GameObject.FindGameObjectsWithTag("itemPosition");
		items = GameObject.FindGameObjectsWithTag("item");
		InvokeRepeating("InvokeWave", 1.0f, 5f);
	}
	
 
	void Update () {
 
	}

	public void InvokeWave() {

		//randomly istanciate an item in a 
		int numberOfObjectsToInstanciate = 10; 

		for (int i = 0; i < numberOfObjectsToInstanciate; i++) {

		GameObject newObj = Instantiate (items[Random.Range(0,items.Length)], itemPositions [Random.Range (0, itemPositions.Length)].transform.position,  Quaternion.identity);
			newObj.transform.SetParent(GameObject.FindGameObjectWithTag("boat").transform);
		}
		 
	 
	} 






}
