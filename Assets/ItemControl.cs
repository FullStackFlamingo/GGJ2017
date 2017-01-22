using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

	public Transform target;

	public MainSceneController sceneController; 
	// Use this for initialization
	void Start () {
		sceneController = FindObjectOfType<MainSceneController> (); 
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		if (this.transform.position.y < -6f) {

			Destroy (this.gameObject); 
			sceneController.itemsOnDeck.Remove (this.gameObject); 
		}
	}




 


	 
}
