using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUIController : MonoBehaviour {
	private bool gameOver = false;
	// Use this for initialization

	public GameObject winScreen;
	public GameObject loseScreen;
	void Start () {
		
	}

	public void showLoseScreen(){
		loseScreen.SetActive(false);
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
	}
}
