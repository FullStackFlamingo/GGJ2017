using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandingController : MonoBehaviour {

	public Image logo;
	public Image howToPlay;
	// Use this for initialization
	void Start () {
		scaleInImage(logo);
	}
	
		void scaleInImage(Image _image){
			var scaleIng = new ScaleTweenProperty( new Vector3( 1, 1, 1 ) );

			var config = new GoTweenConfig();
			config.addTweenProperty( scaleIng );
			config.setEaseType(GoEaseType.ElasticOut);
			config.delay = 0.5f;
						config.onComplete((AbstractGoTween abs)=>{
				Debug.Log("dfdsdf");
				scaleInImage(howToPlay);
			});
			var tween = new GoTween( _image.transform, 2f, config );

			Go.addTween( tween );
		}


	// Update is called once per frame
	void Update () {
		
	}

	public void onClickToStart(){
		SceneManager.LoadScene("NewGameScene");

	}
	
}
