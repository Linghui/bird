using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

	KickerController kickerController;
	GameObject bird;
	private bool getPoint = false;

	void Start () {
		kickerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KickerController> ();
		bird = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(!getPoint && bird.transform.position.x > transform.position.x ){
			kickerController.getPoint();
			getPoint = true;
		}
	}
}
