using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textCloud : MonoBehaviour {

	public GameObject fam;
	public float xMultt = 1f;

	public GameObject[] textObjects;

	private Image render;
	private family info;
	private bool showText = false;
	private AudioSource audio = null;

	const int minCoint = 1;
	const float showTime = 5;

	void Start () {
		render = GetComponent<Image> ();
		info = fam.GetComponent<family> ();
		audio = GetComponent<AudioSource> ();

		render.enabled = false;
		disableAllText ();
	}

	void disableAllText(){
		for (int i = 0; i < textObjects.Length; i++) {
			textObjects [i].SetActive (false);
		}
	}

	GameObject setText(){
		disableAllText ();
		GameObject ret = textObjects [Random.Range (0, textObjects.Length)];
		ret.SetActive (true);

		return ret;
	}


	IEnumerator cloudLoop(){

		//prepare
		showText = true;
		render.enabled = true;
		render.color = new Color (1f, 1f, 1f, 0f);
		GameObject tekst = setText();
		Image textImage = tekst.GetComponent<Image> ();
		textImage.color = new Color (1f, 1f, 1f, 0f); 

		float time = 0f;
		Vector3 startPos = render.transform.position;
		audio.Play ();

		//fade in
		while (time < 1f) {

			render.color = new Color (1f, 1f, 1f, time);
			textImage.color = new Color (1f, 1f, 1f, time);

			render.transform.position = startPos + new Vector3 (((1f-time)*-50)*xMultt,(1f-time)*-50,0);
			render.transform.localScale = new Vector3 (time*xMultt, time, time);

			time += Time.deltaTime*6;
			yield return null;
		}

		//make sure size and position is correct
		render.color = new Color (1f, 1f, 1f, 1f);
		textImage.color = new Color (1f, 1f, 1f, 1f);
		render.transform.position = startPos;
		render.transform.localScale = new Vector3 (1*xMultt, 1, 1);
		time = 1f;

		//wait for a ammount of time
		yield return new WaitForSeconds (showTime);

		//fade away
		while (time > 0f) {

			render.color = new Color (1f, 1f, 1f, time);
			textImage.color = new Color (1f, 1f, 1f, time);
			render.transform.position = startPos + new Vector3 (((1f-time)*-50)*xMultt,(1f-time)*-50,0);
			render.transform.localScale = new Vector3 (time*xMultt, time, time);

			time -= Time.deltaTime*6;
			yield return null;
		}


		//done
		showText = false;
		render.enabled = false;
		render.transform.position = startPos;


	}

	void Update () {
		if (!showText && info.coins <= minCoint) {
			StartCoroutine (cloudLoop ());
		}
	}
}
