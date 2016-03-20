using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textCloud : MonoBehaviour {

	public GameObject fam;
	public float xMultt = 1f;

	public string[] sentences;

	private Image render;
	private Text text;
	private family info;
	private bool showText = false;
	private AudioSource audio = null;

	const int minCoint = 1;
	const float showTime = 5;

	void Start () {
		render = GetComponent<Image> ();
		text = transform.GetChild (0).GetComponent<Text> ();
		info = fam.GetComponent<family> ();
		audio = GetComponent<AudioSource> ();

		render.enabled = false;
		text.text = "";
	}


	IEnumerator cloudLoop(){

		//prepare
		showText = true;
		render.enabled = true;
		render.color = new Color (1f, 1f, 1f, 0f);
		text.color = new Color (text.color.r, text.color.g, text.color.b, 0f);
		text.text = sentences[Random.Range(0, sentences.Length)];
		float time = 0f;
		Vector3 startPos = render.transform.position;
		audio.Play ();

		//fade in
		while (time < 1f) {

			render.color = new Color (1f, 1f, 1f, time);
			text.color = new Color (text.color.r, text.color.g, text.color.b, time);

			render.transform.position = startPos + new Vector3 (((1f-time)*-150)*xMultt,(1f-time)*-50,0);
			render.transform.localScale = new Vector3 (time, time, time);

			time += Time.deltaTime*6;
			yield return null;
		}

		//make sure size and position is correct
		render.color = new Color (1f, 1f, 1f, 1f);
		text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
		render.transform.position = startPos;
		render.transform.localScale = new Vector3 (1, 1, 1);
		time = 1f;

		//wait for a ammount of time
		yield return new WaitForSeconds (showTime);

		//fade away
		while (time > 0f) {

			render.color = new Color (1f, 1f, 1f, time);
			text.color = new Color (text.color.r, text.color.g, text.color.b, time);
			render.transform.position = startPos + new Vector3 (((1f-time)*-150)*xMultt,(1f-time)*-50,0);
			render.transform.localScale = new Vector3 (time, time, time);

			time -= Time.deltaTime*6;
			yield return null;
		}


		//done
		showText = false;
		render.enabled = false;
		text.text = "";
		render.transform.position = startPos;


	}

	void Update () {
		if (!showText && info.coins <= minCoint) {
			StartCoroutine (cloudLoop ());
		}
	}
}
