using UnityEngine;
using System.Collections;

public class MovingLayerController : MonoBehaviour {
	
	public float forwardMovementSpeed;
	private Rigidbody2D rigidbody2D;
	// Use this for initialization
	void Start () {
		
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.x = forwardMovementSpeed;
		GetComponent<Rigidbody2D>().velocity = newVelocity;
	}
}
