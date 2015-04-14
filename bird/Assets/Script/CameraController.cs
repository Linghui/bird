using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject targetObject;
	private float distanceToTarget;

	void Start(){
		distanceToTarget = transform.position.x - targetObject.transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		float targetObjectX = targetObject.transform.position.x;
		
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX + distanceToTarget;
		transform.position = newCameraPosition;
	}
}
