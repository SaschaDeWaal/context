using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class schoolCloud : MonoBehaviour {


	public GameObject player1;
	public GameObject player2;

	public string[] sentences;

	private float xMultt = 1f;
	private Image render;
	private Text text;
	private PlayerInfo info1;
	private PlayerInfo info2;
	private bool showText = false;
	private AudioSource audio = null;

	private bool enoughCoins1 = false;
	private bool enoughCoins2 = false;

	const int minCoint = 1;
	const float showTime = 5;

	void Start () {
		render = GetComponent<Image> ();
		text = transform.GetChild (0).GetComponent<Text> ();
		info1 = player1.GetComponent<PlayerInfo> ();
		info2 = player2.GetComponent<PlayerInfo> ();
		audio = GetComponent<AudioSource> ();

		render.enabled = false;
		text.text = "";

	}
	
	void Update () {
		if (info1.coins >= 3) {
			if (!enoughCoins1 && !showText && !info1.educated) {
				StartCoroutine (cloudLoop(0));

				enoughCoins1 = true;
			}
		} else {
			enoughCoins1 = false;
		}


		if (info2.coins >= 3) {
			if (!enoughCoins2 && !showText && !info2.educated) {
				StartCoroutine (cloudLoop(1));

				enoughCoins2 = true;
			}
		} else {
			enoughCoins2 = false;
		}
	}

	IEnumerator cloudLoop(int player){

		//prepare
		showText = true;
		render.enabled = true;
		render.color = new Color (1f, 1f, 1f, 0f);
		text.color = new Color (text.color.r, text.color.g, text.color.b, 0f);
		text.text = sentences[player];
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
}
