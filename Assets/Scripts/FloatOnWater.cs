using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatOnWater : MonoBehaviour {
    public MeshWave meshwave;
    private Vector3 gameObjectPosition;
    private Vector3 rayOrigin = new Vector3();
    private Vector3 rayDirection =  Vector3.down;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObjectPosition = transform.position;
        rayOrigin.Set(gameObjectPosition.x, 0  , -gameObjectPosition.z);

        Debug.DrawRay(rayOrigin, rayDirection, Color.green, 1);

        Ray newRay = new Ray(rayOrigin, rayDirection);

        gameObjectPosition.y = meshwave.getHeightAtPosition(newRay);
        transform.position = gameObjectPosition;

    }
}
