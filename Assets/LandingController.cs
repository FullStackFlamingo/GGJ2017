using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LandingController : MonoBehaviour {

	public Image logo;
	public Image howToPlay;

	public GameObject titleScreen;

	private bool keyPressReady = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(hideSplash());
	}


    
    IEnumerator hideSplash() {
        yield return new WaitForSeconds(2);
        titleScreen.SetActive(false);
		scaleInImage(logo);
    }
    


	void scaleInImage(Image _image){
		var scaleIng = new ScaleTweenProperty( new Vector3( 1, 1, 1 ) );

		var config = new GoTweenConfig();
		config.addTweenProperty( scaleIng );
		config.setEaseType(GoEaseType.ElasticOut);
		config.delay = 0.5f;
		config.onComplete((AbstractGoTween abs)=>{
			scaleInImage(howToPlay);
			keyPressReady = true;
		});
		var tween = new GoTween( _image.transform, 2f, config );

		Go.addTween( tween );
	}


	// Update is called once per frame
	void Update () {
		if(Input.anyKey && keyPressReady){
					SceneManager.LoadScene("NewGameScene");
		}
	}

	

	public void onClickToStart(){

	}
	
}
