using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LandingController : MonoBehaviour {

	public Image logo;
	// Use this for initialization
	void Start () {
		setupLogoCrash();
	}
	
		void setupLogoCrash(){
		var scaleIng = new ScaleTweenProperty( new Vector3( 1, 1, 1 ) );

		var config = new GoTweenConfig();
		config.addTweenProperty( scaleIng );
		config.setEaseType(GoEaseType.BounceIn);
		var tween = new GoTween( logo.transform, 0.6f, config );
		
		Go.addTween( tween );
	}

	// Update is called once per frame
	void Update () {
		
	}
}
