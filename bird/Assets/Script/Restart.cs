using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public GameController gameController;

	// Use this for initialization
	void Clicked () {
		Debug.Log ("Clicked");
		gameController.enterGame ();
	}

}
