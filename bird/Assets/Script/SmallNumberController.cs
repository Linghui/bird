using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmallNumberController : MonoBehaviour {
	
	public GameObject[] numberList;
	private List<GameObject> liveNumber = new List<GameObject>();
	private int width;
	private string showingNumber = "";
	private Transform parent;
	void Start(){
		width = numberList[0].GetComponent<SpriteRenderer>().sprite.texture.width;
		Debug.Log ("number width " + width);
		parent = transform.parent;
	}
	
	public void showNumber(string number){
		if (number == null || showingNumber == number) {
			return;
		} else {
			Debug.Log("show " + number);
			showingNumber = number;
			
			if(liveNumber.Count > 0 ){
				Debug.Log("delete");
				GameObject[] templ = liveNumber.ToArray();
				for(int index = 0; index < templ.Length; index++){
					liveNumber.Remove(templ[index]);
					Destroy(templ[index]);
				}
			}
			
			
			for(int index = number.Length - 1; index >= 0; index--){
				Debug.Log("create");
				string single = number.Substring(index, 1);
				int singleNum = int.Parse(single);
				GameObject numberPre = numberList[singleNum];
				GameObject numObj = Instantiate(numberPre) as GameObject;
				
				numObj.GetComponent<SpriteRenderer>().sortingLayerName = "ui";
				numObj.GetComponent<SpriteRenderer>().sortingOrder = 5;
				numObj.transform.SetParent(transform);
				
				numObj.transform.position =  new Vector3((number.Length - index - 1) * -0.15f + transform.position.x, transform.position.y,0);
				
				liveNumber.Add(numObj);
			}
			
		}
	}
}
