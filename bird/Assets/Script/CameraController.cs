using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject follow;
	
	// Update is called once per frame
	void Update () {
		float targetObjectX = follow.transform.position.x;
		
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX;
		transform.position = newCameraPosition;
	}
}
