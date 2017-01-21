 using UnityEngine;
 using System.Collections;
 
 public class CharacterMovementController : MonoBehaviour 
 {
     public float speed = 2f;
     public float turnSmoothing = 15f;
     private Vector3 movement;
     private Rigidbody playerRigidBody;
     
     private Quaternion targetRotation;

     void Awake()
     {
         playerRigidBody = GetComponent<Rigidbody> ();
     }
     
     void FixedUpdate()
     {
         float lh = Input.GetAxisRaw ("Horizontal");
         float lv = Input.GetAxisRaw ("Vertical");
    
         Move (lh, lv);
     }
     
     
     void Move (float lh, float lv)
     {
         movement.Set (lh, 0f, lv);
         //direction by camera
         movement = Camera.main.transform.TransformDirection(movement);
         
         
         movement = movement.normalized * speed * Time.deltaTime;
        

         
         playerRigidBody.MovePosition (transform.position + movement);
         
         
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
 
 }