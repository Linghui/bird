using UnityEngine;
using System.Collections;

public class MenuBirdController : MonoBehaviour {

	public GameObject bird;

	private float timer;
	private float y;


	// Use this for initialization
	void Start () {
		y = bird.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime * 8;
		float dis = Mathf.Sin (timer)/14;
		bird.transform.position = new Vector2( bird.transform.position.x, y + dis);




	}
}
