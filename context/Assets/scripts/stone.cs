using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stone : action {

	public int hits = 0;
	public GameObject ui;
	public AudioClip[] audio;

	private bool createdObject = false;
	private Vector3 orginalPos;
	private GameObject uiRef;

	const int minHitRange = 10;
	const int maxHitRange = 40;
	const int pay = 1;

	void Start () {

		//random hits
		hits = Random.Range (minHitRange, maxHitRange);

		//color
		float randomColor = Random.Range (0.4f, 1f);
		GetComponent<SpriteRenderer> ().color = new Color (randomColor, randomColor, randomColor);

		//set depth
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

		//set things
		orginalPos = transform.position;

	}

	void CreateHitUI(){
		
		//create object
		GameObject canvas = GameObject.FindGameObjectWithTag ("canvas");
		uiRef = Instantiate (ui, canvas.transform.position, canvas.transform.rotation) as GameObject;
		uiRef.transform.SetParent (canvas.transform);

		//reset position
		RectTransform rectTransform = uiRef.GetComponent<RectTransform> ();
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

		uiRef.GetComponent<Text> ().text = hits.ToString ();
	}

	void Done(GameObject obj ){
		Destroy (uiRef);
		obj.GetComponent<PlayerInfo> ().AddCoin (pay);
		obj.GetComponent<coinAnimation> ().removedStone (transform.position);
		transform.position = new Vector3 (10000,100000,10000);
		StartCoroutine (comeBack());
	}

	IEnumerator comeBack(){
		yield return new WaitForSeconds (Random.Range(8, 20));

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		transform.position = orginalPos;
		float alpha = 0;
		hits = Random.Range (minHitRange, maxHitRange);
		createdObject = false;

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
