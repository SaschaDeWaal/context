using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
