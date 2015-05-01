using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	
	public GameObject finalScore;
	public GameObject best;
	public GameObject newBestIcon;

	public  void show(int score){
		finalScore.GetComponent<SmallNumberController> ().showNumber (score+"");


		int bestScore = PlayerPrefs.GetInt ("best score");

		if (score > bestScore) {
			PlayerPrefs.SetInt ("best score", score);
			PlayerPrefs.Save();
			best.GetComponent<SmallNumberController> ().showNumber (score + "");
			newBestIcon.SetActive(true);
		} else {
			best.GetComponent<SmallNumberController> ().showNumber (bestScore + "");
		}

	}
}
