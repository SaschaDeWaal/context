using UnityEngine;
using System.Collections;

public class pushed : action {

	float speed = 10;
	bool stopPush = false;
	public AudioClip sound;

	const float minDistance = 1.5f;

	public override void Run(int playerID, float distance, GameObject obj){
		if (distance < minDistance) {
			Vector2 dir = new Vector2 (transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y);
			StartCoroutine (move (dir));
		}
	}

	IEnumerator move(Vector2 dir){
		float time = 0;
		float speedDown = 1f;
		stopPush = false;

		GetComponent<movement> ().enabled = false;
		GetComponent<AudioSource> ().PlayOneShot (sound);


		while (time < 0.5f && !stopPush) {
			transform.Translate (dir*Time.deltaTime*speed*speedDown);

			time += Time.deltaTime;
			speedDown -= (Time.deltaTime*2);
			yield return null;
		}

		GetComponent<movement> ().enabled = true;

	}

	void OnCollisionEnter2D(Collision2D col){
		stopPush = true;
	}
}
