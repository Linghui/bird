using UnityEngine;
using System.Collections;

public class MovingLayerController : MonoBehaviour {
	
	public GameObject targetObject;
	public Camera camera;
//	public float forwardMovementSpeed;

	private Rigidbody2D rigidbody2D;
	private float distanceToTarget;
	// Use this for initialization
	void Start () {
		
		distanceToTarget = transform.position.x - targetObject.transform.position.x;
//		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		float targetObjectX = targetObject.transform.position.x;
		
		Vector3 newMovePosition = transform.position;
		newMovePosition.x = targetObjectX + distanceToTarget;
		transform.position = newMovePosition;

		
		Vector3 newCameraPosition = camera.transform.position;
		newCameraPosition.x = targetObjectX + distanceToTarget;
		camera.transform.position = newCameraPosition;
	}
}
