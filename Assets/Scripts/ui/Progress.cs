using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Progress : MonoBehaviour {

	public Image boatImage;

	private GameObject boatImageObject;
	// Use this for initialization
	void Start () {
		boatImageObject = boatImage.gameObject;
		var rotationProperty = new RotationTweenProperty( new Vector3( 0, 0, 10 ) );

		var config = new GoTweenConfig();
		config.addTweenProperty( rotationProperty );

		config.setIterations(-1,GoLoopType.PingPong);
		var tween = new GoTween( boatImageObject.transform, 0.6f, config );
		
		Go.addTween( tween );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
