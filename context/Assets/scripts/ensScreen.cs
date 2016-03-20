using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class ensScreen : MonoBehaviour {

	void Start () {
		StartCoroutine (loop());
	}
	
	IEnumerator loop(){

		yield return new WaitForSeconds (2f);

		//wait until all keys are up
		while (Input.anyKey) {
			yield return null;
		}

		//wait until a keys is pressed
		while (!Input.anyKey) {
			yield return null;
		}
		Debug.Log ("Reload!");

		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
	}
}
