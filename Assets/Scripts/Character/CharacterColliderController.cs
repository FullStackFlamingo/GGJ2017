using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderController : MonoBehaviour {

    public Collider triggerCollider;
	public bool collidingWithObject = false; 
	public GameObject collectedObject; 
	private bool collectedObjectBool = false; 
     
	private float speed = 5f;
	// Use this for initialization
	void Start () {
		collectedObjectBool = false; 
	}
	
	// Update is called once per frame
	void Update () {
		if (collidingWithObject == true && Input.GetKeyDown(KeyCode.Space)) {
			collectedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
			collectedObjectBool = true; 
			//Rigidbody rb = collectedObject.GetComponent<Rigidbody> (); 
 
		}
		if (collectedObjectBool == true && Input.GetKeyDown (KeyCode.Q)) {
			collectedObject.transform.SetParent (null);

			Rigidbody rb = collectedObject.GetComponent<Rigidbody> (); 
			rb.isKinematic = false; 
		
			rb.velocity = new Vector3(0, 10, transform.forward.z) * 5;

			collectedObjectBool = false; 
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "boat") {
  
				collidingWithObject = true; 
				collectedObject = other.gameObject; 
			Rigidbody rb = collectedObject.GetComponent<Rigidbody> (); 
			rb.mass = 1;
			SphereCollider col = collectedObject.GetComponent<SphereCollider> (); 
		 col.enabled = false; 
 
		}
	}
}
