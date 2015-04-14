using UnityEngine;
using System.Collections;

public class MenuBirdController : MonoBehaviour {

	public GameObject bird;
	public float speed;

	private float timer;
	private float y;
	private bool startPlaying = false;
	private Rigidbody2D rigidbody2D;
	// Use this for initialization
	void Start () {
		y = bird.transform.position.y;
		rigidbody2D = GetComponent<Rigidbody2D>();
		
		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.x = speed;
		rigidbody2D.velocity = newVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		if(!startPlaying){
			
			timer += Time.deltaTime * 8;
			float dis = Mathf.Sin (timer)/14;
			bird.transform.position = new Vector2( bird.transform.position.x, y + dis);
//			bird.transform.position = new Vector2( bird.transform.position.x, y);
		}


	}

	public void stopFly(){
		startPlaying = true;
		rigidbody2D.isKinematic = false;
	}
}
