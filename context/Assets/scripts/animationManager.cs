using UnityEngine;
using System.Collections;

public class animationManager : MonoBehaviour {

	public Sprite[] frames;
	public float speed = 0.1f;
	public string name = "";

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
	
	IEnumerator loop(){


		while (true) {

			if (enabled) {
				frame++;
				if (frame >= frames.Length)
					frame = 0;

				render.sprite = frames [frame];
				render.flipX = flip;
			}

			yield return new WaitForSeconds (speed);
		}
	}
}
