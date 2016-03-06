using UnityEngine;
using System.Collections;

public class playerAction : MonoBehaviour {

	public int playerIndex;

	private bool down = false;

	const float maxActionDistance = 2f;


	void Update () {
		if (Input.GetAxisRaw ("Action Player " + playerIndex.ToString ()) != 0) {
			if (!down) RunAction ();
			down = true;
		} else {
			down = false;
		}
	}

	void RunAction(){

		//define varbiale
		action[] actions = Object.FindObjectsOfType<action>();
		action closedAction = null;
		float distance = 100f;

		//find closed action
		foreach(action act in actions){
			if (Vector3.Distance (transform.position, act.transform.position) < distance) {
				distance = Vector3.Distance (transform.position, act.transform.position);
				closedAction = act;
			}
		}

		//doAction
		if (closedAction != null && distance < maxActionDistance) {
			closedAction.Run (playerIndex, distance, gameObject);
		}
	}
}
