using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game scene controller, spawns and keeps track of items on ship and invokes the wave methods.

public class GameSceneController : MonoBehaviour {

	public GameObject player; 
	public GameObject boat; 
	public GameObject[] itemPositions;
	public GameObject[] items;  
	 

	public 	List<GameObject> itemsOnDeck ; 
	void Start () {
 
		itemPositions = GameObject.FindGameObjectsWithTag("itemPosition");
		items = GameObject.FindGameObjectsWithTag("item");
		InvokeRepeating("InvokeWave", 1.0f, 10f);

 
	}
	
 
	void Update () {
		Debug.Log(itemsOnDeck.Count + "c ount "); 
	}

	public void InvokeWave() {

		//randomly istanciate an item in a 
		int numberOfObjectsToInstanciate = 2; 

		for (int i = 0; i < numberOfObjectsToInstanciate; i++) {

			GameObject newObj = Instantiate (items[Random.Range(0,items.Length)], itemPositions [Random.Range (0, itemPositions.Length)].transform.position,  Quaternion.identity);
			//need to check the new object is not in same position as last 
			itemsOnDeck.Add (newObj); 
			newObj.transform.SetParent(GameObject.FindGameObjectWithTag("boat").transform);
 
		}
	
						
	 
	} 






}
