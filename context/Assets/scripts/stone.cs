using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stone : action {

	public int hits = 0;
	public GameObject ui;
	public AudioClip[] audio;

	private bool createdObject = false;
	private Vector3 orginalPos;

	const int minHitRange = 10;
	const int maxHitRange = 40;
	const int pay = 1;

	void Start () {

		//set things
		orginalPos = transform.position;
		
		//random hits
		hits = Random.Range (minHitRange, maxHitRange);

		//color
		float randomColor = Random.Range (0.4f, 1f);
		GetComponent<SpriteRenderer> ().color = new Color (randomColor, randomColor, randomColor);

		//set depth
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

	}

	void CreateHitUI(){
		
		//create object
		GameObject canvas = GameObject.FindGameObjectWithTag ("canvas");
		ui = Instantiate (ui, canvas.transform.position, canvas.transform.rotation) as GameObject;
		ui.transform.SetParent (canvas.transform);

		//reset position
		RectTransform rectTransform = ui.GetComponent<RectTransform> ();
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		//set position
		Vector2 pos = new Vector2 ((transform.position.x + 9f) / 18f, (transform.position.y + 5f) / 10f);
		rectTransform.anchorMin = pos - new Vector2 (0.05f, 0.05f);
		rectTransform.anchorMax = pos + new Vector2 (0.05f, 0.05f);
	}

	void ShowHits(){

		if (!createdObject) {
			CreateHitUI ();
			createdObject = true;
		}

		ui.GetComponent<Text> ().text = hits.ToString ();
	}

	void Done(GameObject obj ){
		ui.SetActive (false);
		transform.position = new Vector3 (10000,100000,10000);
		obj.GetComponent<PlayerInfo> ().AddCoin (pay);
		//StartCoroutine (comeBack());
	}

	IEnumerator comeBack(){
		yield return new WaitForSeconds (Random.Range(10, 30));

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		transform.position = orginalPos;
		float alpha = 0;
		//hits = Random.Range (minHitRange, maxHitRange);

		while (alpha < 1f) {
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
			alpha += Time.deltaTime*4;
			yield return null;
		}

	}

	public override void Run(int playerID, float distance, GameObject obj){
		if (distance < 1.5f) {
			if (obj.GetComponent<PlayerInfo> ().educated) {
				hits -= 2;
			} else {
				hits -= 1;
			}

			GetComponent<AudioSource> ().PlayOneShot (audio [Random.Range (0, audio.Length)]);

			ShowHits ();
			if (hits < 1)
				Done (obj);
		}
	}

}
