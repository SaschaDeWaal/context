using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stone : action {

	public int hits = 0;
	public int startHits = 0;
	public GameObject ui;
	public AudioClip[] audio;
	public Sprite[] states;

	private bool createdObject = false;
	private Vector3 orginalPos;
	private GameObject uiRef;
	private int addReturnTime = 0;
	private int addHit = 5;
	private bool playingAnimation = false;

	const int minHitRange = 10;
	const int maxHitRange = 40;
	const int returnTime1Time = 4;
	const int addhit1Time = 4;
	const int pay = 1;

	void Start () {

		//random hits
		hits = Random.Range (minHitRange, maxHitRange);
		startHits = hits;

		//color
		//float randomColor = Random.Range (0.4f, 1f);
		//GetComponent<SpriteRenderer> ().color = new Color (randomColor, randomColor, randomColor);

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
		addReturnTime += returnTime1Time;
		addHit += addhit1Time;
		yield return new WaitForSeconds (Random.Range(8+addReturnTime, 20+addReturnTime));

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		transform.position = orginalPos;
		float alpha = 0;
		hits = Random.Range (minHitRange+addHit, maxHitRange+addHit);
		startHits = hits;
		setImage ();

		createdObject = false;

		while (alpha < 1f) {
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
			alpha += Time.deltaTime*4;
			yield return null;
		}

	}

	void setImage(){
		float per = 1f-(hits*1.0f) / (startHits*1.0f);
		int image = Mathf.RoundToInt(states.Length*per);
		image = Mathf.Clamp (image, 0, states.Length - 1);

		GetComponent<SpriteRenderer> ().sprite = states [image];
	}

	public override void Run(int playerID, float distance, GameObject obj){
		if (distance < 1.5f) {
			if (obj.GetComponent<PlayerInfo> ().educated) {
				hits -= 2;
			} else {
				hits -= 1;
			}

			if (!playingAnimation) StartCoroutine (hitAnimation(obj));

			//obj.GetComponent<movement> ().setHitAnim ();

			GetComponent<AudioSource> ().PlayOneShot (audio [Random.Range (0, audio.Length)]);
			setImage ();

			ShowHits ();
			if (hits < 1)
				Done (obj);
		}
	}

	IEnumerator hitAnimation(GameObject player){
		movement move = player.GetComponent<movement> ();
		move.enabled = false;
		move.hitAnimation.flipAnimation = !move.Dir;
		move.hitAnimation.RePlay ();
		playingAnimation = true;

		yield return new WaitForSeconds ((move.hitAnimation.speed*move.hitAnimation.frames.Length));

		player.GetComponent<movement> ().hitAnimation.enabled = false;
		player.GetComponent<movement> ().enabled = true;
		playingAnimation = false;
	}

}
