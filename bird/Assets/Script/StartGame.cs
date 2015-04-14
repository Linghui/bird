using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameController gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Clicked(){
		Debug.Log ("Clicked");
		gameController.enterGame ();
	}
}
