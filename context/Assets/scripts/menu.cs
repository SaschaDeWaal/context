using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menu : MonoBehaviour {


	void Update () {
		if (Input.anyKeyDown) {
			Scene scene = SceneManager.GetActiveScene(); 
			SceneManager.LoadScene("game");
		}
	}
}
