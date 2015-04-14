using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private RuntimePlatform platform;
	public string clickLayer;

	// Use this for initialization
	void Start () {
		platform = Application.platform;
	}
	
	// Update is called once per frame
	void Update () {
		
		uiClickDetect ();
	}

	
	void uiClickDetect(){
		
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					checkTouch(Input.GetTouch(0).position);
				}
			}
		}else 
			//		if(platform == RuntimePlatform.WindowsEditor)
		{
			//			Debug.Log ("Input.mousePosition " + Input.mousePosition);
			if(Input.GetMouseButtonDown(0)) {
				//				Debug.Log ("Input.mousePosition " + Input.mousePosition);
				checkTouch(Input.mousePosition);
			}
		}
	}

	
	void checkTouch(Vector3 pos){
		Vector3 wp  = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos  = new Vector2(wp.x, wp.y);
		LayerMask mask = LayerMask.NameToLayer(clickLayer);
		//		Debug.Log ("mask " + mask.value);
		
		
		Collider2D[] hit = Physics2D.OverlapPointAll(touchPos);
		
		if (hit != null && hit.Length > 0) {
			
			for(int index = 0; index < hit.Length ; index++){
				if(hit[index].gameObject.layer == mask.value){
					//					Debug.Log ("hit " + hit[index].gameObject.name);
					hit[index].transform.gameObject.SendMessage ("Clicked", 0, SendMessageOptions.DontRequireReceiver);
				}
			}
			
		} else {
			//			Debug.Log("????");
		}
	}
}
