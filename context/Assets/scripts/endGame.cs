using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {

	public GameObject background;

	public GameObject winScreen1;
	public GameObject winScreen2;

	private bool playing = true;

	public void setWinner(int deadFamID){
		if (playing) {
			background.SetActive (true);

			if (deadFamID == 1) {
				winScreen2.SetActive (true);
			} else {
				winScreen1.SetActive (true);
			}

			playing = false;
		}
	}
}
