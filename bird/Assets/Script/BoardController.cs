using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	
	public GameObject finalScore;
	public GameObject best;
	public GameObject newBestIcon;

	public GameObject level_1;
	public GameObject level_2;
	public GameObject level_3;

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

		if (score < 10) {

		} else if (score < 20) {
			level_1.SetActive (true);
		} else if (score < 30) {
			level_2.SetActive (true);
		} else {
			level_3.SetActive (true);
		} 

	}
}
