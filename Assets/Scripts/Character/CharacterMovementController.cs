 using UnityEngine;
 using System.Collections;
 
 public class CharacterMovementController : MonoBehaviour 
 {
     public float speed = 2f;
     public float turnSmoothing = 15f;

     //public LayerMask layerMask;
     private Vector3 movement;
     private Rigidbody playerRigidBody;
     
     private Quaternion targetRotation;

     private Vector3 tmpV1;
     private Vector3 tmpV2;

     private float targetYPos = 0;

     //RaycastHit hit; 
     
     void Awake()
     {
         playerRigidBody = GetComponent<Rigidbody> ();
         tmpV1 = new Vector3();
         tmpV2 = new Vector3();
         //layerMask = ~layerMask;
     }
     
     void FixedUpdate()
     {
         /*tmpV1 = transform.position;
         tmpV1.y=10;

        if (Physics.Raycast(tmpV1, -Vector3.up, out hit, 50,layerMask)){
            targetYPos = hit.point.y;
        }*/ ///RAYCASTING DISABLED


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
        
         tmpV1 = transform.position + movement;
         //tmpV1.y = targetYPos; ///RAYCASTING DISABLED
         //Debug.Log(targetYPos);
         //Debug.Log(tmpV1.y);
         playerRigidBody.MovePosition(tmpV1);
         
         
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