using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public int playerIndex;

	const float speed = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	void move(){

		Vector3 dir = new Vector3 (Input.GetAxis("Horizontal Player " + playerIndex.ToString()), Input.GetAxis("Vertical Player " + playerIndex.ToString()));

		float reduce = Vector3.Distance (new Vector3(0,0,0), dir);

		if (reduce != 0) {
			reduce -= 1f;
			reduce = reduce / 2;
		}

		transform.Translate (new Vector3(dir.x*Time.deltaTime*speed, dir.y*Time.deltaTime*speed)*(1f-reduce));

		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, -8, 8), Mathf.Clamp(transform.position.y, -4.4f, 4.4f), transform.position.z);

	}
}
