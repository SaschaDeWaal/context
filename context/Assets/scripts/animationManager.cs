using UnityEngine;
using System.Collections;

public class animationManager : MonoBehaviour {

	public Sprite[] frames;
	public float speed = 0.1f;
	public string name = "";
	public bool loopAnimation = true;

	private bool flip = false;
	private int frame = 0;
	private SpriteRenderer render = null;

	public bool flipAnimation{
		get{
			return flip;
		}
		set{
			flip = value;
			if (render != null) {
				render.sprite = frames [frame];
				render.flipX = flip;
			}
		}
	}

	void Start () {

		render = GetComponent<SpriteRenderer> ();

		if (frames.Length > 0)
			StartCoroutine (loop());
	}

	void turnAllAnimationsOff(){
		animationManager[] anim = GetComponents<animationManager> ();
		for (int i = 0; i < anim.Length; i++)
			anim [i].enabled = false;
	}

	public void RePlay(){
		turnAllAnimationsOff ();
		frame = 0;
		GetComponent<SpriteRenderer> ().sprite = frames [0];
		enabled = true;
	}
	
	IEnumerator loop(){


		while (true) {

			if (enabled) {

				render.sprite = frames [frame];
				render.flipX = flip;


				frame++;
				if (frame >= frames.Length) {
					if (loopAnimation) {
						frame = 0;
					} else {
						frame = frames.Length - 1;
					}
				}
			}

			yield return new WaitForSeconds (speed);
		}
	}
}
