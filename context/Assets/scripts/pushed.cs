using UnityEngine;
using System.Collections;

public class pushed : action {

	float speed = 10;
	bool stopPush = false;
	public AudioClip sound;
	public Sprite vallSprite;
	public Sprite downSprite;

	const float minDistance = 1.5f;

	public override void Run(int playerID, float distance, GameObject obj){
		if (distance < minDistance) {
			Vector2 dir = new Vector2 (transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y);
			StartCoroutine (move (dir));
			StartCoroutine (playerAnimation(obj));
		}
	}

	IEnumerator playerAnimation(GameObject obj){
		movement move = obj.GetComponent<movement> ();
		move.enabled = false;
		move.pushAnimation.RePlay ();
		move.pushAnimation.flipAnimation = (obj.transform.position.x > transform.position.x);
	

		yield return new WaitForSeconds (move.pushAnimation.speed*move.pushAnimation.frames.Length);

		move.enabled = true;
		move.pushAnimation.enabled = false;
	}

	IEnumerator move(Vector2 dir){
		float time = 0;
		float speedDown = 1f;
		stopPush = false;

		GetComponent<movement> ().enabled = false;
		GetComponent<AudioSource> ().PlayOneShot (sound);
		GetComponent<SpriteRenderer> ().sprite = vallSprite;
		GetComponent<SpriteRenderer> ().flipX = (dir.x > 0);
		turnAllAnimationsOff ();


		while (time < 0.5f && !stopPush) {
			transform.Translate (dir*Time.deltaTime*speed*speedDown);

			time += Time.deltaTime;
			speedDown -= (Time.deltaTime*2);
			yield return null;
		}

		GetComponent<SpriteRenderer> ().sprite = downSprite;
		GetComponent<SpriteRenderer> ().flipX = (dir.x > 0);
		yield return new WaitForSeconds (0.4f);


		GetComponent<movement> ().enabled = true;
		GetComponent<movement> ().onEndPush ();

	}

	void turnAllAnimationsOff(){
		animationManager[] anim = GetComponents<animationManager> ();
		for (int i = 0; i < anim.Length; i++)
			anim [i].enabled = false;
	}

	void OnCollisionEnter2D(Collision2D col){
		stopPush = true;
	}
}
