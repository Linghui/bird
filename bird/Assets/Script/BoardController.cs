using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	
	public GameObject finalScore;
	public GameObject best;
	public GameObject newBestIcon;

	public  void show(int score){
		finalScore.GetComponent<SmallNumberController> ().showNumber (score+"");


		int bestScore = PlayerPrefs.GetInt ("best");
		Debug.Log ("best score " + bestScore);

		if (score > bestScore) {
			PlayerPrefs.SetInt ("best", bestScore);
			PlayerPrefs.Save();
			best.GetComponent<SmallNumberController> ().showNumber (score + "");

		} else {
			best.GetComponent<SmallNumberController> ().showNumber (bestScore + "");
		}

	}
}
