using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Progress : MonoBehaviour {

	public Canvas canvas;
	public Image boatImage;

	public float percentageComplete=0;

	public Sprite[] boatImageList;

	public MainSceneController sceneController;

	private GameObject boatImageObject;

	private Vector3 tmpV1 = new Vector3();

	private float boatImageWidth = 0;
	private float progressBarWidth = 0;


	private int currentSinkLevel = 0;
	void Start () {

		boatImageObject = boatImage.gameObject;
		setupUIBoatRotation();

		boatImageWidth = (boatImageObject.GetComponent<RectTransform>().rect.width* canvas.scaleFactor)+30;
		progressBarWidth = (this.gameObject.GetComponent<RectTransform>().rect.width*canvas.scaleFactor);
	}
	
	void setupUIBoatRotation(){
		var rotationProperty = new RotationTweenProperty( new Vector3( 0, 0, 10 ) );

		var config = new GoTweenConfig();
		config.addTweenProperty( rotationProperty );

		config.setIterations(-1,GoLoopType.PingPong);
		var tween = new GoTween( boatImageObject.transform, 0.6f, config );
		
		Go.addTween( tween );
	}
	
	// Update is called once per frame
	void Update () {
		tmpV1 = boatImageObject.transform.position;
		percentageComplete = Mathf.Clamp(percentageComplete,0,100);
		tmpV1.x = percentageComplete/100 * (progressBarWidth - boatImageWidth);
		tmpV1.x += boatImageWidth/2;
		boatImageObject.transform.position = tmpV1;

		currentSinkLevel = Mathf.Clamp(sceneController.itemsOnDeck.Count,0,16);

		boatImage.sprite = boatImageList[currentSinkLevel];
	}
}
