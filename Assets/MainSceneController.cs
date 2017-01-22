using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour {

	public GameObject player; 
	public GameObject boat; 
	public GameObject[] items; 
	public GameObject[] spawnablePositions; 
	public GameObject water; 

	public 	List<GameObject> itemsOnDeck ; 
	private bool waterRisen; 
  
	void Start () {
		InvokeRepeating("InvokeWave", 1.0f, 10f);
		waterRisen = false; 
	}

	public void Update() {

		if (waterRisen == false && itemsOnDeck.Count >= 2) {
			Debug.Log ("OVER "); 
			StartCoroutine(startSinking ()); 

		} 

	}
 
 
 
	public void InvokeWave() {

		//randomly istanciate an item in a 
		int numberOfObjectsToInstanciate = 2; 

		for (int i = 0; i < numberOfObjectsToInstanciate; i++) {

			GameObject newObj = Instantiate (items[Random.Range(0,items.Length)], spawnablePositions [Random.Range (0, spawnablePositions.Length)].transform.position,  Quaternion.identity);
			newObj.transform.SetParent(GameObject.FindGameObjectWithTag("boat").transform);
			itemsOnDeck.Add (newObj); 
		}




	}

	public IEnumerator startSinking () {
		//Vector3 direction = new Vector3 (0,0,0); 
		Debug.Log ("StartRise "); 
		float speed = 0.1f;
	//	water.transform.position = direction;

		waterRisen = true;
		yield return null; 
	} 

 
}
