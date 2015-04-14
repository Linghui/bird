using UnityEngine;
using System.Collections;

public class KickerController : MonoBehaviour {
	
	public GameObject target;
	
	public AudioSource wing;
	public AudioSource hit;
	public AudioSource die;
	public AudioSource swoo;
	
	public GameObject title;
	public GameObject tip;
	
	public GameObject pipe;
	
	private bool isGameOver = false;
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
		
		touchDetect ();
		
		pipePlant ();
		
		if(isGameOver ){
			if(shakeTime > 0){
				
				shakeTime -= Time.deltaTime;
				float rx = Random.Range(-0.1f,0.1f);
				float ry = Random.Range(-0.1f,0.1f);
				
				Camera.main.transform.position = cameraP + new Vector3(rx,rx,0);
			} else {
				showDialog();
			}
		}
	}
	
	void touchDetect(){
		
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
	
	private float pipeTimer = 0f;
	public float pipeInterval;
	void pipePlant(){
		if(startKicking && !isGameOver){
			pipeTimer += Time.deltaTime;
			if(pipeTimer > pipeInterval){

				float ry = Random.Range(-0.5f,1.5f);
				pipeTimer = 0;
				
				GameObject pipeIns = Instantiate(pipe) as GameObject;
				pipeIns.transform.position = new Vector3(target.transform.position.x + 3,ry,0);
				
			}
		}
	}
	
	void kick(){
		
		if(isGameOver){
			return;
		}
		
		if(!startKicking){
			
			MenuBirdController controller = target.GetComponent<MenuBirdController> ();
			controller.stopFly ();
			Debug.Log ("kick");
			
			title.SetActive(false);
			tip.SetActive(false);
			
			startKicking  = true;
		}
		
		wing.Play ();
		target.GetComponent<MenuBirdController> ().kick ();
		
	}
	
	public void gameOver(){
		Debug.Log ("Game Over");
		if(isGameOver){
			return;
		}
		
		shakeCamera ();
		
		isGameOver = true;
		hit.Play ();
	}
	
	private bool dialogShown = false;
	public GameObject overTitle;
	public GameObject board;
	public GameObject btn;
	public float dialogTimer = 0;
	void showDialog(){
		//		if(!dialogShown){
		//			dialogShown = true;
		dialogTimer += Time.deltaTime;
		SpriteRenderer rend = overTitle.GetComponent<SpriteRenderer>();
		
		if(!overTitle.activeSelf){
			swoo.Play ();
			rend.material.color = new Color(1,1,1,0.5f);
		}
		overTitle.SetActive (true);
		
		if(overTitle.activeSelf && rend.material.color.a < 1){
			Debug.Log("fade in " + rend.material.color);
			rend.material.color += new Color(0, 0, 0, 1f) * Time.deltaTime;
		}
		if(rend.material.color.a > 1 && board.transform.position.y < 0 && dialogTimer > 1f){
			
			if(!board.activeSelf){
				swoo.Play ();
				btn.SetActive(true);
			}
			board.SetActive(true);
			
			board.transform.position += new Vector3(0,1f,0) * Time.deltaTime * 20;
		}
		
		
		//		}
	}
	
	private Vector3 cameraP;
	private float shakeTime = 0.3f;
	void shakeCamera(){
		cameraP = Camera.main.transform.position;
	}
}
