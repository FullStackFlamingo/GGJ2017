using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderController : MonoBehaviour
{

	public Collider triggerCollider;
	public bool collidingWithObject = false;
	public GameObject collectedObject;
	private bool collectedObjectBool = false;


	public float shootSpeed = 100f;
     
	private float speed = 40f;
	// Use this for initialization
	void Start ()
	{
		collectedObjectBool = false; 
	}
	
	// Update is called once per frame
	void Update ()
	{

		Debug.Log ("collidingWithObject =" + collidingWithObject);


		if (collidingWithObject == true && Input.GetKeyDown (KeyCode.Space)) {
			collectedObject.transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
			collectedObject.transform.localPosition = new Vector3 (0, 0.5f, 1);
			collectedObjectBool = true;  
			SphereCollider col = collectedObject.GetComponent<SphereCollider> (); 
			col.enabled = false;
			collectedObject.GetComponent<Rigidbody> ().isKinematic = true;
		}


		if (collectedObjectBool == true && Input.GetKeyDown (KeyCode.Q)) {
			collectedObject.transform.SetParent (null);

			Rigidbody rb = collectedObject.GetComponent<Rigidbody> (); 
			rb.isKinematic = false; 
			rb.useGravity = true;

			SphereCollider col = collectedObject.GetComponent<SphereCollider> (); 
			col.enabled = true;
		
			rb.velocity = new Vector3 (20, 20, transform.forward.z) * shootSpeed;

			collectedObjectBool = false; 
		}
	}

	void OnCollisionStay (Collision other)
	{
		if (other.collider.gameObject.tag != "boat" && !collectedObjectBool) {
			collidingWithObject = true; 
			collectedObject = other.gameObject; 
			Rigidbody rb = collectedObject.GetComponent<Rigidbody> (); 
			rb.mass = 1;

			 
 
		}
	}
}
