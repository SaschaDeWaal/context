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
			if (act.gameObject.name != gameObject.name && Vector2.Distance (new Vector2(transform.position.x, transform.position.y), new Vector2(act.transform.position.x, act.transform.position.y)) < distance) {
				distance = Vector2.Distance (new Vector2 (transform.position.x, transform.position.y), new Vector2 (act.transform.position.x, act.transform.position.y));
				closedAction = act;
			}
		}

		//doAction
		if (closedAction != null) {
			Debug.Log (gameObject.name + ": call action of " + closedAction.gameObject.name);
			closedAction.Run (playerIndex, distance, gameObject);
		}
	}
}
