using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class GameController : MonoBehaviour {


	public string clickLayer;
	public GameObject cover;

	private RuntimePlatform platform;
	private SpriteRenderer coverRender;
	private bool startEntering = false;

	// Use this for initialization
	void Start () {
		platform = Application.platform;
		coverRender = cover.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (startEntering) {
			coverRender.material.color += new Color(0, 0, 0, 2f) * Time.deltaTime;

			Debug.Log ("color " + coverRender.material.color);
			if(coverRender.material.color.a > 1){
				Application.LoadLevel("GameScene");
			}
		} else {
			
			coverRender.material.color -= new Color(0, 0, 0, 1f) * Time.deltaTime;
			uiClickDetect ();
		}
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
//				Debug.Log ("hit " + hit[index].gameObject.name);
				if(hit[index].gameObject.layer == mask.value){
										
					hit[index].transform.gameObject.SendMessage ("Clicked", 0, SendMessageOptions.DontRequireReceiver);
				}
			}
			
		} else {
			//			Debug.Log("????");
		}
	}

	public void enterGame(){
		GetComponent<AudioSource> ().Play ();
		coverRender.material.color = new Color (1, 1, 1, 0);
		startEntering = true;
	}
}
