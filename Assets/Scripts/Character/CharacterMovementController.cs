 using UnityEngine;
 using System.Collections;
 
 public class CharacterMovementController : MonoBehaviour 
 {
    public float speed = 2f;
    public float turnSmoothing = 15f;
    private Vector3 movement;
	public Rigidbody playerRigidBody;

    private Quaternion targetRotation;
	private Animator anim;

	private GameObject collectedItem; 
	private bool itemIsAttachedToCharacter; 

	public float shootSpeed;


     void Awake()
     {
		itemIsAttachedToCharacter = false; 
		playerRigidBody = this.gameObject.GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator>();
		shootSpeed = 25f; 
     }
     
     void FixedUpdate()
     {
         float lh = Input.GetAxisRaw ("Horizontal");
         float lv = Input.GetAxisRaw ("Vertical");
    
		Move (lh, lv); 
 
			if (Input.GetKeyDown (KeyCode.Q) && itemIsAttachedToCharacter == true) {
				collectedItem.GetComponent<SphereCollider> ().enabled = true; 
				collectedItem.GetComponent<Rigidbody> ().isKinematic = false; 
				collectedItem.transform.SetParent (null);
			 
				collectedItem.GetComponent<Rigidbody> ().velocity = new Vector3 (transform.forward.x * shootSpeed, 10, transform.forward.z * shootSpeed * Time.deltaTime);
				itemIsAttachedToCharacter = false;

	 
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
				collectedItem = col.gameObject;
				collectedItem.transform.SetParent (this.transform);
				collectedItem.transform.localPosition = new Vector3 (0.39f, 0.59f, 0);
				collectedItem.GetComponent<Rigidbody> ().isKinematic = true; 
				collectedItem.GetComponent<SphereCollider> ().enabled = false; 

				itemIsAttachedToCharacter = true;
			}



		}

	 
	}
 
 }