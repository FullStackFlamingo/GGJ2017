using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUIController : MonoBehaviour {
	private bool gameOver = false;
	private bool gameEnd = false;
	// Use this for initialization

	public GameObject winScreen;
	public GameObject loseScreen;

	public int maxTimeSeconds = 42;
	private float time = 0;
	public float currentTime = 0;
	void Start () {
		
	}

	public void showLoseScreen(){
		loseScreen.SetActive(true);
	}
	public void showWinScreen(){
		winScreen.SetActive(true);
	}

    IEnumerator enableRestart() {
        yield return new WaitForSeconds(2);
        gameOver = true;
    }
    
	// Update is called once per frame
	void Update () {
		if(Input.anyKey && gameOver){
			SceneManager.LoadScene("NewGameScene");
		}
		time += Time.deltaTime;
		currentTime = time;
		if(time>maxTimeSeconds && !gameEnd){
			gameEnd = true;
			showWinScreen();
		}
	}
}
