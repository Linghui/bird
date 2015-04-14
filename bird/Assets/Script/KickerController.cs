using UnityEngine;
using System.Collections;

public class KickerController : MonoBehaviour {
	
	public GameObject target;
	public AudioSource wing;
	public GameObject title;
	public GameObject tip;
	
	private RuntimePlatform platform;
	private bool startKicking = false;
	
	// Use this for initialization
	private Rigidbody2D rigidbody2D;
	void Start () {
		platform = Application.platform;
		rigidbody2D = target.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					kick();
				}
			}
		}else 
			//		if(platform == RuntimePlatform.WindowsEditor)
		{
			//			Debug.Log ("Input.mousePosition " + Input.mousePosition);
			if(Input.GetMouseButtonDown(0)) {
				//				Debug.Log ("Input.mousePosition " + Input.mousePosition);
				kick();
			}
		}
	}
	
	void kick(){
		if(!startKicking){
			
			MenuBirdController controller = target.GetComponent<MenuBirdController> ();
			controller.stopFly ();
			Debug.Log ("kick");
			
			title.SetActive(false);
			tip.SetActive(false);
			
			startKicking  = true;
		}
		
		wing.Play ();
		rigidbody2D.AddForce (new Vector2(0, 10f));
		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.y = 5f;
		rigidbody2D.velocity = newVelocity;
	}
}
