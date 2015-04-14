using UnityEngine;
using System.Collections;

public class MenuBirdController : MonoBehaviour {
	
	public GameObject bird;
	public float speed;
	public AudioSource die;
	public KickerController gameController;
	
	private float timer;
	private float y;
	private bool startPlaying = false;
	private bool living = true;
	private bool climbing = true;
	private Rigidbody2D rigidbody2D;
	// Use this for initialization
	void Start () {
		y = bird.transform.position.y;
		rigidbody2D = GetComponent<Rigidbody2D>();
		
		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.x = speed;
		rigidbody2D.velocity = newVelocity;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(!living){
			return;
		}
		if(collider.CompareTag("pipe")){
			die.Play();
		}
		gameController.gameOver ();
		living = false;
		GetComponent<Animator> ().enabled = false;
		
		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.x = 0;
		rigidbody2D.velocity = newVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (!startPlaying) {
			
			timer += Time.deltaTime * 8;
			float dis = Mathf.Sin (timer) / 14;
			bird.transform.position = new Vector2 (bird.transform.position.x, y + dis);
		} else {
			if(living){
				
				if (rigidbody2D.velocity.y > 0) {

				} else 
				{
					transform.Rotate(new Vector3(0,0,-90) * Mathf.Abs(rigidbody2D.velocity.y)/100);

				}
			}
		}
	}

	public void kick(){

		transform.rotation = Quaternion.identity;
		transform.Rotate(new Vector3(0,0,30));
		
		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.y = 3.5f;
		rigidbody2D.velocity = newVelocity;
	}
	
	public void stopFly(){
		startPlaying = true;
		rigidbody2D.isKinematic = false;

	}
}
