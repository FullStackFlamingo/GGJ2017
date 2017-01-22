using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour {
	public GameUIController uiController;
	public GameObject player; 
	public GameObject boat; 
	public GameObject[] items; 
	public SpawnableItem[] spawnablePositions; 

	public GameObject water; 

	public 	List<GameObject> itemsOnDeck ; 
	private bool waterRisen; 
	private int maxNumberOfItemsOnDeck = 16; 
  
	void Start () {
		waterRisen = false; 
	}

	public void Update() {

		if (waterRisen == false && itemsOnDeck.Count >= 2) {
			Debug.Log ("OVER "); 
			StartCoroutine(startSinking ()); 
		} 
	}
 
 
 
	public void InvokeItemSpawn() {

		//randomly istanciate an item in a 

		int numberOfObjectsToInstanciate = 2; 
		List<SpawnableItem> _spawnableItems = getAvailibleSpawnablePosition(numberOfObjectsToInstanciate);

		for (int i = 0; i < _spawnableItems.Count; i++) {

			GameObject newObj = Instantiate (
				items[Random.Range(0,items.Length)], 
				_spawnableItems[i].transform.position,  
				Quaternion.identity);
			newObj.transform.SetParent(GameObject.FindGameObjectWithTag("boat").transform);
			itemsOnDeck.Add (newObj); 
		}

	}

	 public List<SpawnableItem> getAvailibleSpawnablePosition(int _itemsCount){
		List<SpawnableItem> _items = new List<SpawnableItem>();

		foreach(SpawnableItem spawnablePosition in spawnablePositions)
		{
			spawnablePosition.isNotAvailible = false;
			foreach(GameObject itemOnDeck in itemsOnDeck)
			{
				if ( itemOnDeck.GetComponent<SphereCollider>().bounds.Contains(spawnablePosition.transform.position) )
					spawnablePosition.isNotAvailible = true;
			}
		}

		for(int index = 0; index < spawnablePositions.Length; index++){
			if(!spawnablePositions[index].isNotAvailible){
				spawnablePositions[index].isNotAvailible = true;
				_items.Add(spawnablePositions[index]);
			}
			if(_items.Count>=_itemsCount){
				break;
			}
		}

		if (_items.Count < 2){
			Debug.Log("The deck is full");
			uiController.showLoseScreen();
		}
		return _items;
	}

	public IEnumerator startSinking () {
		//Vector3 direction = new Vector3 (0,0,0); 
		Debug.Log ("StartRise "); 
		float speed = 0.1f;

		//raiseWater ();
		waterRisen = true;
		yield return null; 
	} 
	void raiseWater(){
		Vector3 targetHeight = water.transform.position;
		targetHeight.y = -0.46f;

		var positionProperty = new PositionTweenProperty( targetHeight );

		var config = new GoTweenConfig();
		config.addTweenProperty( positionProperty );

		config.onComplete ((AbstractGoTween abs) => {
			
		});
		var tween = new GoTween( water.transform, 2f, config );

		Go.addTween( tween );
	}

 
}
