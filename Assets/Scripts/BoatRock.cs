using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRock : MonoBehaviour {
	public float maxAngle = 6f;
	private Quaternion rotation;
	private float currentAngle = 0;
	// Use this for initialization
	void Start () {
		rotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		currentAngle +=0.03f;
		//Debug.Log(Mathf.Sin(currentAngle));
		//Debug.Log(Mathf.Sin(currentAngle)*45);
		rotation.SetEulerAngles(0,0,Mathf.Sin(currentAngle)*maxAngle*Mathf.Deg2Rad);
		this.transform.rotation = rotation;
	}
}
