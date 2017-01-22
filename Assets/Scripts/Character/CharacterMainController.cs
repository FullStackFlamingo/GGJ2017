 using UnityEngine;
using System.Collections.Generic;
 using System.Collections;
 //new fixed
 public class CharacterMainController : MonoBehaviour 
 {
    public float speed = 2f;
    public float turnSmoothing = 15f;
    private Vector3 movement;
	public Rigidbody playerRigidBody;

    private Quaternion targetRotation;
	public Animator anim;

	private GameObject collectedItem; 
	private bool itemIsAttachedToCharacter; 

	public float shootSpeed;
	public bool isCarrying; 
	private bool isWalking; 
	public bool isThrowing;  


     void Awake()
     {
		itemIsAttachedToCharacter = false; 
		playerRigidBody = this.gameObject.GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator>();
		shootSpeed = 25f; 
		 
     }
	public void Update() {
 
	}

 
 
 
     void FixedUpdate()
     {
         float lh = Input.GetAxisRaw ("Horizontal");
         float lv = Input.GetAxisRaw ("Vertical");
    
		Move (lh, lv); 
 
			if (Input.GetKeyDown (KeyCode.Q) && itemIsAttachedToCharacter == true) {
				
				isThrowing = true; 
				anim.SetBool ("isThrowing", true); 

				collectedItem.GetComponent<SphereCollider> ().enabled = true; 
				collectedItem.GetComponent<Rigidbody> ().isKinematic = false; 
				collectedItem.transform.SetParent (null);
			 
				collectedItem.GetComponent<Rigidbody> ().velocity = new Vector3 (transform.forward.x * shootSpeed, 10, transform.forward.z * shootSpeed * Time.deltaTime);
	
			itemIsAttachedToCharacter = false;
			isCarrying = false; 
			anim.SetBool ("isCarrying", false); 

			isThrowing = false;
			// anim.SetBool ("isWalking",false); 
 

 
     }

		if (itemIsAttachedToCharacter == false && Input.GetKeyUp (KeyCode.Q) ) {

			isThrowing = false; 
			anim.SetBool ("isThrowing", false);
			isWalking = true; 
			anim.SetBool ("isWalking", true);


		}
		 
	 

	 

	}


     
     
     void Move (float lh, float lv)
     {
         movement.Set (lh, 0f, lv);
         movement = Camera.main.transform.TransformDirection(movement);
         movement = movement.normalized * speed * Time.deltaTime;
		 

        if (lh == 0f && lv == 0f){
            playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.angularVelocity = Vector3.zero;
 
        }else{

            playerRigidBody.MovePosition (transform.position + movement);
	 
        }
         
         
         if (lh != 0f || lv != 0f) 
         {
             Rotating(lh, lv);
         }

	 
     }
     
	 
  
     void Rotating (float lh, float lv)
     {
         Vector3 targetDirection = new Vector3 (lh, 0f, lv);         
		targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
         Quaternion newRotation = Quaternion.Lerp (playerRigidBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
         playerRigidBody.MoveRotation(newRotation);
	
     }

	public void	OnCollisionStay(Collision col) {
		if (col.gameObject.tag == "item") {

			if (Input.GetKeyDown(KeyCode.Space)) {
				 
				isCarrying = true; 
				anim.SetBool ("isCarrying", true);
		
				//if current picking up animation is complete
			
				collectedItem = col.gameObject;
				StartCoroutine(AnimateObjectPickUp(collectedItem));


				Vector3 startLocation = collectedItem.transform.position; 
				Vector3 targetLocation = new Vector3 (-0.05f, 1.08f, 0.61f); 
				 	
				//collectedItem.transform.localPosition = new Vector3 (-0.05f, 1.08f, 0.61f);
				collectedItem.GetComponent<Rigidbody> ().isKinematic = true; 
				collectedItem.GetComponent<SphereCollider> ().enabled = false; 

				itemIsAttachedToCharacter = true;
 
			}
				
		}

	}

		public IEnumerator AnimateObjectPickUp(GameObject item){

		collectedItem.transform.SetParent (this.transform);
			Vector3 startLocation = item.transform.position; 
			Vector3 targetLocation = new Vector3 (-0.05f, 1.08f, 0.61f); 
		yield return new WaitForSeconds(0.3f); 
		collectedItem.transform.localPosition = new Vector3 (-0.05f, 1.08f, 0.61f);
			yield return null; 
		}

	 
}

	  
 